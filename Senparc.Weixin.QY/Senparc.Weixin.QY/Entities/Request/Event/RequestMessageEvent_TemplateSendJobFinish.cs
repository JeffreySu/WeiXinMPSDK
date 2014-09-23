using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 事件推送群发结果。
    /// 
    /// 由于群发任务提交后，群发任务可能在一定时间后才完成，因此，群发接口调用时，仅会给出群发任务是否提交成功的提示，若群发任务提交成功，则在群发任务结束时，会向开发者在公众平台填写的开发者URL（callback URL）推送事件。
    /// </summary>
    public class RequestMessageEvent_TemplateSendJobFinish : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.TEMPLATESENDJOBFINISH; }
        }

        /// <summary>
        /// 群发的结构，为“success”（送达成功）或“failed:user block”（送达由于用户拒收（用户设置拒绝接收公众号消息））或“failed: system failed”（送达由于其他原因失败）。
        /// </summary>
        public string Status { get; set; }
    }
}