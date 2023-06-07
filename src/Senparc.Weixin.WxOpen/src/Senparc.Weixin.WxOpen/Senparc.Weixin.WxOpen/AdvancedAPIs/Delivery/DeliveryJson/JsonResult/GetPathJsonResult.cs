using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson.JsonResult
{
    /// <summary>
    /// 运单轨迹返回参数
    /// </summary>
    public class GetPathJsonResult:WxJsonResult
    {
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 快递公司 ID
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 运单 ID
        /// </summary>
        public string waybill_id { get; set; }
        /// <summary>
        /// 轨迹节点数量
        /// </summary>
        public int path_item_num { get; set; }
        /// <summary>
        /// 轨迹节点列表
        /// </summary>
        public List<PathItemListModel> path_item_list { get; set; }
    }
    /// <summary>
    /// 轨迹节点信息
    /// </summary>
    public class PathItemListModel
    {
        /// <summary>
        /// 轨迹节点 Unix 时间戳
        /// </summary>
        public long action_time { get; set; }
        /// <summary>
        /// 轨迹节点类型
        /// </summary>
        public int action_type { get; set; }
        /// <summary>
        /// 轨迹节点详情
        /// </summary>
        public string action_msg { get; set; }
    }
}
