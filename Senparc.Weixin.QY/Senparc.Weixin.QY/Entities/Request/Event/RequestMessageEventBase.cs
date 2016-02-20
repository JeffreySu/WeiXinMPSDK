/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageEventBase.cs
    文件功能描述：事件基类
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities
{
    public interface IRequestMessageEventBase : IRequestMessageBase
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

    public class RequestMessageEventBase : RequestMessageBase, IRequestMessageEventBase
    {
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

        ///// <summary>
        ///// 事件KEY值，与自定义菜单接口中KEY值对应，如果是View，则是跳转到的URL地址
        ///// </summary>
        //public string EventKey { get; set; }
    }
}
