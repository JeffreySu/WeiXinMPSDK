#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：WxFileJsonResult.cs
    文件功能描述：文件相关接口 返回结果
    
    
    创建标识：lishewen - 20190530
   
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    /// <summary>
    /// 获取文件上传链接 返回结果
    /// </summary>
    public class WxUploadFileJsonResult : WxJsonResult
    {
        /// <summary>
        /// 上传url
        /// </summary>
        public string url { get; set; }
        public string token { get; set; }
        public string authorization { get; set; }
        /// <summary>
        /// 文件ID
        /// </summary>
        public string file_id { get; set; }
        /// <summary>
        /// cos文件ID
        /// </summary>
        public string cos_file_id { get; set; }
    }

    /// <summary>
    /// 获取文件下载链接 返回结果
    /// </summary>
    public class WxDownloadFileJsonResult : WxJsonResult
    {
        /// <summary>
        /// 文件列表
        /// </summary>
        public Result_File_List[] file_list { get; set; }
    }

    /// <summary>
    /// 删除文件 返回结果
    /// </summary>
    public class WxDeleteFileJsonResult : WxJsonResult
    {
        /// <summary>
        /// 文件列表
        /// </summary>
        public Result_File_List[] delete_list { get; set; }
    }

    public class Result_File_List
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public string fileid { get; set; }
        /// <summary>
        /// 下载链接
        /// </summary>
        public string download_url { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 该文件错误信息
        /// </summary>
        public string errmsg { get; set; }
    }

    public class FileItem
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        public string fileid { get; set; }
        /// <summary>
        /// 下载链接有效期
        /// </summary>
        public int max_age { get; set; }
    }
}
