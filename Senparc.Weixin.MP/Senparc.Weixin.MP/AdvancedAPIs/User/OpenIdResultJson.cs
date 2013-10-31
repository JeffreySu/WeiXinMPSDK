using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
   public class OpenIdResultJson
    {
       public int total { get; set; }
       public int count { get; set; }
       public OpenIdResultJson_Data data { get; set; }
       public string next_openid { get; set; }
    }

    public class OpenIdResultJson_Data
    {
        public List<string> openid { get; set; }
    }
}
