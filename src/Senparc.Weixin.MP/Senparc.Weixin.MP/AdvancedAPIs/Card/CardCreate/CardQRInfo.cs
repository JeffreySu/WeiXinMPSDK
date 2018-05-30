using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    public class QR_CARD_INFO
    {
        public string card_id { get; set; }

        public string code { get; set; }

        public string openid { get; set; }

        public bool is_unique_code { get; set; }

        public string outer_id { get; set; }

        public string outer_str { get; set; }
    }
}
