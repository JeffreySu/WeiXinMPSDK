using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;

namespace Senparc.Weixin.Sample.NetCore3.Controllers
{
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

            var tempDir = Path.Combine(CO2NET.Config.RootDictionaryPath, "App_Data", "QrCode");
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            var tempDirName = Guid.NewGuid().ToString("n");
            var zipFileName = Path.Combine(tempDir, $"{tempDirName}.zip");
            tempDir = Path.Combine(tempDir, tempDirName);

            foreach (var code in codes)
            {
                i++;

                var finalCode = code.Length > 100 ? code.Substring(0, 100) : code;

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

                var fileName = Path.Combine(tempDir, $"{i}.jpg");//需要确保文件夹存在！
                SenparcTrace.SendCustomLog("二维码生成", fileName);

                var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                {
                    bitmap.Save(fileStream, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            
            ZipFile.CreateFromDirectory(tempDir, zipFileName, CompressionLevel.Fastest, false);


            return File(zipFileName, "application/x-zip-compressed", $"SenparcQrCode_{SystemTime.Now.ToString("yyyy-MM-dd-HHmmss")}");
        }
    }
}
