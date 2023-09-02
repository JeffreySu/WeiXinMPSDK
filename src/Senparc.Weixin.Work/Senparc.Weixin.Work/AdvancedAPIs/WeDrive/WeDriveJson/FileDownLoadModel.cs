using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 下载文件入口参数
    /// </summary>
    public class FileDownLoadModel
    {
        /// <summary>
        /// 文件fileid（只支持下载普通文件，不支持下载文件夹或微文档）
        /// </summary>
        public string fileid { get; set; }
        /// <summary>
        /// 微盘和文件选择器jsapi返回的selectedTicket。若填此参数，则不需要填fileid。
        /// </summary>
        public string selected_ticket { get; set; }
    }
}
