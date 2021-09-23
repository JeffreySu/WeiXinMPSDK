using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;

namespace Senparc.Weixin.Sample.CommonService.Utilities
{
    public static class QrCodeHelper
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static MemoryStream GerQrCodeStream(string url)
        {
            BitMatrix bitMatrix = new MultiFormatWriter().encode(url, BarcodeFormat.QR_CODE, 300, 300);
            var bw = new ZXing.BarcodeWriterPixelData();

            var pixelData = bw.Write(bitMatrix);
            var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            var fileStream = new MemoryStream();
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

            fileStream.Flush();//.net core 必须要加
            fileStream.Position = 0;//.net core 必须要加

            bitmap.Save(fileStream, System.Drawing.Imaging.ImageFormat.Png);

            fileStream.Seek(0, SeekOrigin.Begin);
            return fileStream;
        }

        /// <summary>
        /// 获取文字图片信息
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MemoryStream GetTextImageStream(string text)
        {
            MemoryStream fileStream = new MemoryStream();
            var fontSize = 14;
            var wordLength = 0;
            for (int i = 0; i < text.Length; i++)
            {
                byte[] bytes = Encoding.Default.GetBytes(text.Substring(i, 1));
                wordLength += bytes.Length > 1 ? 2 : 1;
            }
            using (var bitmap = new System.Drawing.Bitmap(wordLength * fontSize + 20, 14 + 40, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.ResetTransform();//重置图像
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    g.DrawString(text, new Font("宋体", fontSize, FontStyle.Bold), Brushes.White, 10, 10);
                    bitmap.Save(fileStream, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            fileStream.Seek(0, SeekOrigin.Begin);
            return fileStream;
        }
    }
}