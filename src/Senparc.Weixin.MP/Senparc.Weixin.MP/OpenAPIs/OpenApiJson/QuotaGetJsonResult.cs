using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.OpenAPIs.OpenApiJson
{
    public class QuotaGetJsonResult : WxJsonResult
    {
        public Quota quota { get; set; }
    }

    public class Quota
    {
        public int daily_limit { get; set; }
        public int used { get; set; }
        public int remain { get; set; }
    }

}
