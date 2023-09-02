using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 文件信息返回参数
    /// </summary>
    public class FileInfoJsonRetult:WorkJsonResult
    {
        /// <summary>
        /// 文件信息
        /// </summary>
        public FileInfo file_info { get; set; }
    }
}
