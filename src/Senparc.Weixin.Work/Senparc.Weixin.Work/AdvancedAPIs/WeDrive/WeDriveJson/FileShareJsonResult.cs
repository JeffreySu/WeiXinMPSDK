using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 获取分享链接返回参数
    /// </summary>
    public class FileShareJsonResult:WorkJsonResult
    {
        /// <summary>
        /// 分享文件的链接
        /// </summary>
        public string share_url { get; set; }
    }
}
