using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace Senparc.Weixin.MP.Sample.CommonService.Download
{
    public class Config
    {
        public int QrCodeId { get; set; }
        public List<string> Versions { get; set; }
        public int DownloadCount { get; set; }

    }
}
