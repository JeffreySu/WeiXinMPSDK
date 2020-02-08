using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Trace;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
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

            var dt0 = SystemTime.Now;

            //QrCode 根目录
            var qrCodeDir = Path.Combine(CO2NET.Config.RootDictionaryPath, "App_Data", "QrCode");
            if (!Directory.Exists(qrCodeDir))
            {
                Directory.CreateDirectory(qrCodeDir);
            }

            //定义文件名和路径
            var tempId = SystemTime.Now.ToString("yyyy-MM-dd-HHmmss");//本次生成唯一Id
            var tempDirName = $"{tempId}_{Guid.NewGuid().ToString("n")}";//临时文件夹名
            var tempZipFileName = $"{tempDirName}.zip";//临时压缩文件名
            var tempZipFileFullPath = Path.Combine(qrCodeDir, tempZipFileName);//临时压缩文件完整路径
            var tempDir = Path.Combine(qrCodeDir, tempDirName);
            Directory.CreateDirectory(tempDir);//创建临时目录

            //说明文件
            var readmeFile = Path.Combine(qrCodeDir, "readme.txt");
            System.IO.File.Copy(readmeFile, Path.Combine(tempDir, "readme.txt"));

            //便利所有二维码内容
            foreach (var code in codes)
            {
                if (code.IsNullOrEmpty())
                {
                    continue;//过滤为空的段落
                }
                i++;//计数器

                
                var finalCode = code.Length > 100 ? code.Substring(0, 100) : code;//约束长度

                //二维码生成开始
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
                //二维码生成结束

                var fileName = Path.Combine(tempDir, $"{i}.jpg");//二维码文件名

                //保存二维码
                var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                {
                    bitmap.Save(fileStream, System.Drawing.Imaging.ImageFormat.Png);
                    fileStream.Close();
                }
            }
            SenparcTrace.SendCustomLog("二维码生成结束", $"耗时：{SystemTime.DiffTotalMS(dt0)} ms，临时文件：{tempZipFileFullPath}");//记录日志

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
                    return File(fs, "application/x-zip-compressed", $"SenparcQrCode_{tempId}.zip");
                    //TODO:删除临时文件
                }
            }

            return Content("打包文件失败！");
        }
    }
}
