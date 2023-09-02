using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 重命名返回参数
    /// </summary>
    public class FileRenameJsonResult:WorkJsonResult
    {
        /// <summary>
        /// 文件信息
        /// </summary>
        public FileInfo file { get; set; }
    }
}
