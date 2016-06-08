using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class QrCodeResult : WxJsonResult
    {
        public string pic_url { get; set; }
        public string qrcode_url { get; set; }
    }
}
