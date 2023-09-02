using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 指定位置上传文件返回参数
    /// </summary>
    public class FileUploadJsonResult:WorkJsonResult
    {
        /// <summary>
        /// 新建文件的fielid
        /// </summary>
        public string fileid { get; set; }
    }
}
