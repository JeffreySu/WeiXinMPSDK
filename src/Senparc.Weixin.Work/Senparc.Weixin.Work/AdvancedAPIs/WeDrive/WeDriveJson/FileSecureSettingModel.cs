using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 修改文件安全设置入口参数
    /// </summary>
    public class FileSecureSettingModel
    {
        /// <summary>
        /// 文件fileid
        /// </summary>
        public string fileid { get; set; }
        /// <summary>
        /// 水印参数
        /// </summary>
        public Watermark watermark { get; set; }
    }
}
