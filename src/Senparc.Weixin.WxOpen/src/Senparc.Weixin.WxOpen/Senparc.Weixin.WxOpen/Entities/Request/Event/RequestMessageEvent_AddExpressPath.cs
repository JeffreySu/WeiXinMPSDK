using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.Entities
{
    public class RequestMessageEvent_AddExpressPath:RequestMessageEventBase,IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.add_express_path; }
        }
        /// <summary>
        /// 快递公司ID
        /// </summary>
        public string DeliveryID { get; set; }
        /// <summary>
        /// 运单ID
        /// </summary>
        public string WayBillId { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 轨迹版本号（整型）
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 轨迹节点数（整型）
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 轨迹列表
        /// </summary>
        public List<ActionModel> Actions { get; set; }
    }
    public class ActionModel
    {
        public long ActionTime { get; set; }
        public int ActionType { get; set; }
        public string ActionMsg { get; set; }
    }
}
