using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.QrCodeJump
{
    public class DownloadJsonResult : WxJsonResult
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string file_name { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        public string file_content { get; set; }
    }
}
