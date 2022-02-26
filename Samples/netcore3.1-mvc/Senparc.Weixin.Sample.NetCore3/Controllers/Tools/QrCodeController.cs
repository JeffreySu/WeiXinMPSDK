using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Trace;
using System;
using System.Collections.Concurrent;
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
        private string[] GetCodes(string codesStr)
        {
            var codes = codesStr.Split(new[] { Environment.NewLine,"\r\n","\n" }, StringSplitOptions.RemoveEmptyEntries);
            //限制个数
            if (codes.Length > 200)
            {
                codes = codes.Take(200).ToArray();
            }
            return codes;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string codesStr)
        {
            var codes = GetCodes(codesStr);

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
            var i = 0;
            foreach (var code in codes)
            {
                if (code.IsNullOrEmpty())
                {
                    continue;//过滤为空的段落
                }
                i++;//计数器


                var finalCode = code.Length > 100 ? code.Substring(0, 100) : code;//约束长度

                //二维码生成开始
                BitMatrix bitMatrix;//定义像素矩阵对象
                bitMatrix = new MultiFormatWriter().encode(finalCode, BarcodeFormat.QR_CODE /*条码或二维码标准*/, 600/*宽度*/, 600/*高度*/);
                var bw = new ZXing.BarcodeWriterPixelData();

                var pixelData = bw.Write(bitMatrix);
                var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);//预绘制32bit标准的位图片

                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height)/*绘制矩形区域及偏移量*/,
                    System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                try
                {
                    //假设位图的 row stride = 4 字节 * 图片的宽度
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);//从内存 Unlock bitmap 对象
                }
                //二维码生成结束

                var fileName = Path.Combine(tempDir, $"{i}.jpg");//二维码文件名

                //保存二维码
                var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                {
                    bitmap.Save(fileStream, System.Drawing.Imaging.ImageFormat.Png);
                    fileStream.Close();//一定要关闭文件流
                }
            }
            SenparcTrace.SendCustomLog("二维码生成结束", $"耗时：{SystemTime.DiffTotalMS(dt0)} ms，临时文件：{tempZipFileFullPath}");//记录日志

            var dt1 = SystemTime.Now;
            while (Directory.GetFiles(tempDir).Length < i + 1/* readme.txt */ && SystemTime.NowDiff(dt1) < TimeSpan.FromSeconds(30)/*最多等待时间*/)
            {
                Thread.Sleep(1000);//重试等待时间
            }

            ZipFile.CreateFromDirectory(tempDir, tempZipFileFullPath, CompressionLevel.Fastest, false);//创建压缩文件

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

        /// <summary>
        /// 生成短址
        /// </summary>
        /// <param name="codesStr"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetShortUrl(string codesStr)
        {
            var codes = GetCodes(codesStr);

            ConcurrentDictionary<int, string> dic = new ConcurrentDictionary<int, string>();
            List<Task> tasks = new List<Task>();
            List<Exception> exceptions = new List<Exception>();
            var totalCount = codes.Length;
            var successCount = 0;
            var finishedCount = 0;

            for (int i = 0; i < totalCount; i++)
            {
                var index = i;
                try
                {
                    var task = Senparc.Weixin.MP.AdvancedAPIs.UrlApi.ShortUrlAsync(Config.SenparcWeixinSetting.MpSetting.WeixinAppId, "long2short", codes[index])
                    .ContinueWith(weixinResult =>
                    {
                        dic[index] = weixinResult.Result.short_url;
                        successCount++;
                    })
                    .ConfigureAwait(false);

                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                    dic[index] = "";
                }
                finally
                {
                    finishedCount++;
                }

                //tasks.Add(task);
            }

            //Task.WaitAll(tasks.ToArray(), TimeSpan.FromMinutes(1));
            var dt1 = SystemTime.Now;
            while (successCount < codes.Length && SystemTime.NowDiff(dt1).TotalSeconds < 60)
            {
                Thread.Sleep(1000);
            }

            var result = string.Join(Environment.NewLine, dic.OrderBy(z => z.Key).Select(z => z.Value));
            return Json(new { result = result, totalCount = totalCount, successCount = successCount, errors = string.Join(" , ", exceptions.Select(z => z.Message).ToArray()) });
        }
    }
}
