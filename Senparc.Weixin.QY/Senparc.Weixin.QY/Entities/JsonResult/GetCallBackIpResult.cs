using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.Entities
{
    public class GetCallBackIpResult : WxJsonResult
    {
        public string[] ip_list { get; set; }
    }
}
