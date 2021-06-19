using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    /// <summary>
    ///  拉取官方服务标签结果
    /// </summary>
    public class GetNearbyOfficialServiceInfoJsonResult : WxJsonResult
    {
        public Service_Infos data { get; set; }
    }
    [Serializable]
    public class Service_Infos
    {
        public List<Service> srvice_infos { get; set; } = new List<Service>();
    }
    [Serializable]
    public class Service
    {
        public string icon_url { get; set; }

        public int type { get; set; }

        public int id { get; set; }

        public string name { get; set; }
    }
}
