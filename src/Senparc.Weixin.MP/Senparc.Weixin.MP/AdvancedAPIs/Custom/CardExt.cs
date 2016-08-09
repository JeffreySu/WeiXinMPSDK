/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
 
    创建标识：Senparc - 20160808
    创建描述：创建CardExt
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class CardExt
    {
        public string card_id { get; set; }
        public string code { get; set; }
        public string openid { get; set; }
        public string expire_seconds { get; set; }
        public bool is_unique_code { get; set; }
        public string outer_str { get; set; }
    }
}
