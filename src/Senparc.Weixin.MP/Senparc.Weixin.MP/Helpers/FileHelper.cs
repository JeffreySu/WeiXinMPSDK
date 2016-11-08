using System;
/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：FileHelper.cs
    文件功能描述：处理文件
    
    
    创建标识：Senparc - 20161108

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// 从Url下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fullFilePathAndName"></param>
        public void DownLoadFileFromUrl(string url, string fullFilePathAndName)
        {
            using (FileStream fs = new FileStream(fullFilePathAndName, FileMode.OpenOrCreate))
            {
                HttpUtility.Get.Download(url, fs);
                fs.Flush(true);
            }
        }
    }
}
