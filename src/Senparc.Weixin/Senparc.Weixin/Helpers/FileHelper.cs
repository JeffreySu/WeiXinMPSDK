/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：FileHelper.cs
    文件功能描述：处理文件
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20161108
    修改描述：将Senparc.Weixin.MP中的FileHelper转移到Senparc.Weixin中，并添加DownLoadFileFromUrl方法

----------------------------------------------------------------*/

using System.IO;

namespace Senparc.Weixin.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// 根据完整文件路径获取FileStream
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileStream GetFileStream(string fileName)
        {
            FileStream fileStream = null;
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                fileStream = new FileStream(fileName, FileMode.Open);
            }
            return fileStream;
        }

        /// <summary>
        /// 从Url下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fullFilePathAndName"></param>
        public static void DownLoadFileFromUrl(string url, string fullFilePathAndName)
        {
            using (FileStream fs = new FileStream(fullFilePathAndName, FileMode.OpenOrCreate))
            {
                HttpUtility.Get.Download(url, fs);
                fs.Flush(true);
            }
        }
    }
}
