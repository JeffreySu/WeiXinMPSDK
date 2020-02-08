using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;

namespace Senparc.Weixin.Sample.NetCore3.Controllers
{
    /// <summary>
    /// 二维码批量下载
    /// </summary>
    public class QrCodeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string codesStr)
        {
            var codes = codesStr.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var i = 0;

            //限制个数
            if (codes.Length > 200)
            {
                codes = codes.Take(100).ToArray();
            }

            var qrCodeDir = Path.Combine(CO2NET.Config.RootDictionaryPath, "App_Data", "QrCode");
            if (!Directory.Exists(qrCodeDir))
            {
                Directory.CreateDirectory(qrCodeDir);
            }

            //定义文件名和路径
            var tempId = SystemTime.Now.ToString("yyyy-MM-dd-HHmmss");
            var tempDirName = $"{tempId}_{Guid.NewGuid().ToString("n")}";
            var tempZipFileName = $"{tempDirName}.zip";
            var tempZipFileFullPath = Path.Combine(qrCodeDir, tempZipFileName);
            var tempDir = Path.Combine(qrCodeDir, tempDirName);
            Directory.CreateDirectory(tempDir);//创建临时目录

            var readmeFile = Path.Combine(qrCodeDir, "readme.txt");
            System.IO.File.Copy(readmeFile, Path.Combine(tempDir, "readme.txt"));

            //便利所有二维码内容
            foreach (var code in codes)
            {
                if (code.IsNullOrEmpty())
                {
                    continue;
                }
                i++;

                var finalCode = code.Length > 100 ? code.Substring(0, 100) : code;//约束长度

                BitMatrix bitMatrix;
                bitMatrix = new MultiFormatWriter().encode(finalCode, BarcodeFormat.QR_CODE, 600, 600);
                var bw = new ZXing.BarcodeWriterPixelData();

                var pixelData = bw.Write(bitMatrix);
                var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }

                var fileName = Path.Combine(tempDir, $"{i}.jpg");

                var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                {
                    bitmap.Save(fileStream, System.Drawing.Imaging.ImageFormat.Png);
                    fileStream.Close();
                }
            }
            SenparcTrace.SendCustomLog("二维码生成结束", tempZipFileFullPath);

            var dt1 = SystemTime.Now;
            while (Directory.GetFiles(tempDir).Length < i + 1/* readme.txt */ && SystemTime.NowDiff(dt1) < TimeSpan.FromSeconds(30)/*最多等待时间*/)
            {
                Thread.Sleep(1000);
            }

            ZipFile.CreateFromDirectory(tempDir, tempZipFileFullPath, CompressionLevel.Fastest, false);

            var dt2 = SystemTime.Now;
            while (SystemTime.NowDiff(dt2) < TimeSpan.FromSeconds(10)/*最多等待时间*/)
            {
                FileStream fs = null;
                try
                {
                    fs = new FileStream(tempZipFileFullPath, FileMode.Open, FileAccess.Read, FileShare.None);
                }
                catch
                {
                    Thread.Sleep(500);
                    continue;
                }

                if (fs != null)
                {
                    return File(fs/*$"~/App_Data/QrCode/{tempZipFileName}"*/ /*tempZipFileFullPath*/, "application/x-zip-compressed", $"SenparcQrCode_{tempId}.zip");
                }
            }

            return Content("打包文件失败！");
        }
    }
}
