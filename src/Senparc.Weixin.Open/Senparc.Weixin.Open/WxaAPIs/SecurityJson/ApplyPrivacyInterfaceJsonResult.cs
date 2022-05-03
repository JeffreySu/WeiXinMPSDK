using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class ApplyPrivacyInterfaceJsonResult : WxJsonResult
    {
        public uint audit_id { get; set; }
    }
}
