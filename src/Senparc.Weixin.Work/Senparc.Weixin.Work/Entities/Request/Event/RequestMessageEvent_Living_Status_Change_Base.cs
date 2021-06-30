using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.Entities.Request.Event
{
    /// <summary>
    /// 直播回调事件
    /// </summary>
    public class RequestMessageEvent_Living_Status_Change_Base: RequestMessageEventBase
    {
        public override Work.Event Event
        {
            get { return Work.Event.LIVING_STATUS_CHANGE; }
        }
        /// <summary>
        /// 直播ID
        /// </summary>
        public string LivingId { get; set; }

        /// <summary>
        /// 直播状态 ，0：预约中，1：直播中，2：已结束，3：已过期，4：已取消
        /// </summary>
        public int Status { get; set; }
    }
}
