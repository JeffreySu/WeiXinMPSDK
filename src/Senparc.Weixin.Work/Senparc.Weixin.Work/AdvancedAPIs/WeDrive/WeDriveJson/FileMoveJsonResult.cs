using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 将文件移动到指定位置返回参数
    /// </summary>
    public class FileMoveJsonResult:WorkJsonResult
    {
        /// <summary>
        /// 文件列表
        /// </summary>
        public FileList file_list { get; set;; }
    }
}
