/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_Enter_Agent.cs
    文件功能描述：事件之用户进入应用的事件推送(enter_agent)
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 用户进入应用的事件推送(enter_agent)
    /// </summary>
    public class RequestMessageEvent_Enter_Agent : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.ENTER_AGENT; }
        }

        /// <summary>
        /// 事件KEY值，此事件该值为空
        /// </summary>
        public string EventKey { get; set; }
    }
}
