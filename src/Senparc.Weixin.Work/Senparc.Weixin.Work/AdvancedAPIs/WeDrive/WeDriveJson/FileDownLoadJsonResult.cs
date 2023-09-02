using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 下载文件返回参数
    /// </summary>
    public class FileDownLoadJsonResult:WorkJsonResult
    {
        /// <summary>
        /// 下载请求url (有效期2个小时)
        /// </summary>
        public string download_url { get; set; }
        /// <summary>
        /// 下载请求带cookie的key
        /// </summary>
        public string cookie_name { get; set; }
        /// <summary>
        /// 下载请求带cookie的value
        /// </summary>
        public string cookie_value { get; set;}
    }
}
