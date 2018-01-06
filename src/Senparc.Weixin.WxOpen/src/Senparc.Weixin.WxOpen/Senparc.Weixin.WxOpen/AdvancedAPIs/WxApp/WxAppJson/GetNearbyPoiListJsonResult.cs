using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    /// <summary>
    /// 添加地点返回结果
    /// </summary>
    public class GetNearbyPoiListJsonResult : WxJsonResult
    {
        public Data data { get; set; }
    }

    [Serializable]
    public class Data
    {
        public int left_apply_num { get; set; }

        public int max_apply_num { get; set; }

        public PoiList data { get; set; }
    }

    [Serializable]
    public class PoiList
    {
        public List<PoiInfo> poi_list { get; set; }
    }

    /// <summary>
    /// 地点详情
    /// </summary>
    [Serializable]
    public class PoiInfo
    {
        public string poi_id { get; set; }

        public string qualification_address { get; set; }

        public string qualification_num { get; set; }

        public int audit_status { get; set; }

        public int display_status { get; set; }

        public string refuse_reason { get; set; }
    }
}
