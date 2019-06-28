/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEventBase.cs
    文件功能描述：事件基类
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities
{
    public interface IRequestMessageEventBase : IWorkRequestMessageBase, IRequestMessageEvent
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        Event Event { get; }
        ///// <summary>
        ///// 事件KEY值，与自定义菜单接口中KEY值对应
        ///// </summary>
        //string EventKey { get; set; }
    }

    public class RequestMessageEventBase : WorkRequestMessageBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 企业应用的id，整型。可在应用的设置页面查看
        /// </summary>
        public int AgentID { get; set; }

        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Event; }
        }

        /// <summary>
        /// 事件类型
        /// </summary>
        public virtual Event Event
        {
            get { return Event.ENTER; }
        }


        /// <summary>
        /// 事件类型
        /// </summary>
        public virtual object EventType { get { return Event; } }

        /// <summary>
        /// 获取事件类型的字符串
        /// </summary>
        public string EventName { get { return EventType != null ? EventType.ToString() : null; } }

        ///// <summary>
        ///// 事件KEY值，与自定义菜单接口中KEY值对应，如果是View，则是跳转到的URL地址
        ///// </summary>
        //public string EventKey { get; set; }
    }
}
