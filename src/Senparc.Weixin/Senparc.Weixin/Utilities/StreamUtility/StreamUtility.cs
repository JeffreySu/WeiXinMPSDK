#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：StreamUtility.cs
    文件功能描述：流处理公共类
    
    
    创建标识：Senparc - 20150419
    
----------------------------------------------------------------*/



using System;
using System.IO;
using System.Threading.Tasks;

namespace Senparc.Weixin.StreamUtility
{
    /// <summary>
    /// 流工具类
    /// </summary>
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
#if NET35 || NET40 || NET45
            return Convert.ToBase64String(arr, Base64FormattingOptions.None);
#else
            return Convert.ToBase64String(arr);
#endif
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
            using (var localFile = new FileStream(savePath, FileMode.OpenOrCreate))
            {
                localFile.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
            }
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】获取Stream的Base64字符串
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<string> GetBase64StringAsync(Stream stream)
        {
            byte[] arr = new byte[stream.Length];
            stream.Position = 0;
            await stream.ReadAsync(arr, 0, (int)stream.Length);
#if NET35 || NET40 || NET45
            return Convert.ToBase64String(arr, Base64FormattingOptions.None);
#else
            return Convert.ToBase64String(arr);
#endif
        }

        /// <summary>
        /// 【异步方法】将base64String反序列化到流，或保存成文件
        /// </summary>
        /// <param name="base64String"></param>
        /// <param name="savePath">如果为null则不保存</param>
        /// <returns></returns>
        public static async Task<Stream> GetStreamFromBase64StringAsync(string base64String, string savePath)
        {
            byte[] bytes = Convert.FromBase64String(base64String);

            var memoryStream = new MemoryStream(bytes, 0, bytes.Length);
            await memoryStream.WriteAsync(bytes, 0, bytes.Length);

            if (!string.IsNullOrEmpty(savePath))
            {
                await SaveFileFromStreamAsync(memoryStream, savePath);
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        /// <summary>
        /// 【异步方法】将memoryStream保存到文件
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="savePath"></param>
        public static async Task SaveFileFromStreamAsync(MemoryStream memoryStream, string savePath)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
            using (var localFile = new FileStream(savePath, FileMode.OpenOrCreate))
            {
                await localFile.WriteAsync(memoryStream.ToArray(), 0, (int)memoryStream.Length);
            }
        }

        #endregion
#endif


    }
}
