using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs.Tester
{
    public class MemberAuthJsonResult: WxJsonResult
    {
        public List<MemberAuthItem> members { get; set; }
    }

    public class MemberAuthItem
    {
        public string userstr { get; set; }
    }
}
