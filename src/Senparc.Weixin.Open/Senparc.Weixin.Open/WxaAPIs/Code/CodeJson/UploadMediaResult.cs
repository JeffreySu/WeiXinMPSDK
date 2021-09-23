using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class UploadMediaResult : WxJsonResult
    {
        public string type { get; set; }

        public string mediaid { get; set; }
    }
}
