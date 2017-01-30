using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.Entities
{
    public class DecodeEntityBase
    {
        public Watermark watermark { get; set; }
    }

    public class Watermark
    {
        public string appid { get; set; }
        public string timestamp { get; set; }
    }
}
