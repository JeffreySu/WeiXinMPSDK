/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：StreamUtility.cs
    文件功能描述：流处理公共类
    
    
    创建标识：Senparc - 20150419
    
----------------------------------------------------------------*/
using System;
using System.IO;

namespace Senparc.Weixin.StreamUtility
{
    public static class StreamUtility
    {
        #region 同步方法

        /// <summary>
        /// 获取Stream的Base64字符串
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetBase64String(Stream stream)
        {
            byte[] arr = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(arr, 0, (int)stream.Length);
            return Convert.ToBase64String(arr, Base64FormattingOptions.None);
        }

        /// <summary>
        /// 将base64String反序列化到流，或保存成文件
        /// </summary>
        /// <param name="base64String"></param>
        /// <param name="savePath">如果为null则不保存</param>
        /// <returns></returns>
        public static Stream GetStreamFromBase64String(string base64String, string savePath)
        {
            byte[] bytes = Convert.FromBase64String(base64String);

            var memoryStream = new MemoryStream(bytes, 0, bytes.Length);
            memoryStream.Write(bytes, 0, bytes.Length);

            if (!string.IsNullOrEmpty(savePath))
            {
                SaveFileFromStream(memoryStream, savePath);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        /// <summary>
        /// 将memoryStream保存到文件
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="savePath"></param>
        public static void SaveFileFromStream(MemoryStream memoryStream, string savePath)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
            using (var localFile = new FileStream(savePath, System.IO.FileMode.OpenOrCreate))
            {
                localFile.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
            }
        }

        #endregion


    }
}
