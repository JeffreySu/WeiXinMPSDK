/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：FileHelper.cs
    文件功能描述：根据完整文件路径获取FileStream
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
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

    }
}
