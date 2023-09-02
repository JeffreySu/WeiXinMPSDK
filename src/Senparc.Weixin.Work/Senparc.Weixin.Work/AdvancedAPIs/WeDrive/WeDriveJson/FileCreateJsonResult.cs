using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 新建文件夹、文档
    /// </summary>
    public class FileCreateJsonResult:WorkJsonResult
    {
        /// <summary>
        /// 新建文件的fileid
        /// </summary>
        public string fileid { get; set; }
        /// <summary>
        /// 文档的访问链接，仅在新建文档时返回
        /// </summary>
        public string url { get; set; }
    }
}
