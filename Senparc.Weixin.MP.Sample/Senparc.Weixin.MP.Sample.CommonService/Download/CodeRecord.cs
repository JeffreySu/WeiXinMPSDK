using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Sample.CommonService.Download
{
    public class CodeRecord
    {
        public string Key { get; set; }
        public int QrCodeId { get; set; }
        public string Version { get; set; }
        public bool Used { get; set; }
    }
}
