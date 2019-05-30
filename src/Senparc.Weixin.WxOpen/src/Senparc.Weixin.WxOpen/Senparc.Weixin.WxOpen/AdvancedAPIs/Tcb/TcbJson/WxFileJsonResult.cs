using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
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

    public class WxDownloadFileJsonResult : WxJsonResult
    {
        /// <summary>
        /// 文件列表
        /// </summary>
        public Result_File_List[] file_list { get; set; }
    }

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
