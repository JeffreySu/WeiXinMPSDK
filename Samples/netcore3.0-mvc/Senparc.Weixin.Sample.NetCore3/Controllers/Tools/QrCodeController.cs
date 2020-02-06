using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.MP.Sample.CommonService.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
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
            foreach (var code in codes)
            {
                i++;
                BitMatrix bitMatrix;
                bitMatrix = new MultiFormatWriter().encode(code, BarcodeFormat.QR_CODE, 600, 600);
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

                var fileName = Path.Combine(CO2NET.Config.RootDictionaryPath, "QrCodes", $"{i}.jpg");//需要确保文件夹存在！
                SenparcTrace.SendCustomLog("二维码生成", fileName);
                var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                {
                    bitmap.Save(fileStream, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            return Content("done.");
        }
    }
}
