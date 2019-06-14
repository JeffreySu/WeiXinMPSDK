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
        /// <summary>
        /// 剩余可添加地点个数
        /// </summary>
        public int left_apply_num { get; set; }

        /// <summary>
        /// 最大可添加地点个数
        /// </summary>
        public int max_apply_num { get; set; }

        /// <summary>
        /// 地址列表的 JSON 格式字符串
        /// </summary>
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
        /// <summary>
        /// 附近地点 ID
        /// </summary>
        public string poi_id { get; set; }

        /// <summary>
        /// 资质证件地址
        /// </summary>
        public string qualification_address { get; set; }

        /// <summary>
        /// 资质证件证件号
        /// </summary>
        public string qualification_num { get; set; }

        /// <summary>
        /// 地点审核状态
        /// 3	审核中
        /// 4	审核失败
        /// 5	审核通过
        /// </summary>
        public int audit_status { get; set; }

        /// <summary>
        /// 地点展示在附近状态
        /// 0	未展示
        /// 1	展示中
        /// </summary>
        public int display_status { get; set; }

        /// <summary>
        /// 审核失败原因，audit_status=4 时返回
        /// </summary>
        public string refuse_reason { get; set; }
    }
}
