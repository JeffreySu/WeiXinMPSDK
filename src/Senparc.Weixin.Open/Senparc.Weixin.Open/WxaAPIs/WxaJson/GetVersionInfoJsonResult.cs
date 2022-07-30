using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class GetVersionInfoJsonResult : WxJsonResult
    {
        public Exp_Info exp_info { get; set; }
        public Release_Info release_info { get; set; }
    }

    public class Exp_Info
    {
        public int exp_time { get; set; }
        public string exp_version { get; set; }
        public string exp_desc { get; set; }
    }

    public class Release_Info
    {
        public int release_time { get; set; }
        public string release_version { get; set; }
        public string release_desc { get; set; }
    }

}
