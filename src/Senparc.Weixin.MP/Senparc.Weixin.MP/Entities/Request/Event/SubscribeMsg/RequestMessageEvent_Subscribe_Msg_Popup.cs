using System.Collections.Generic;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_Subscribe_Msg_Popup : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.subscribe_msg_popup_event; }
        }

        public SubscribeMsgPopupEventList SubscribeMsgPopupEvent { get; set; }
    }


    public class SubscribeMsgPopupEventList
    {
        public List<SubscribeMsgPopupEventItem> List { get; set; }
    }

    public class SubscribeMsgPopupEventItem
    {
        /// <summary>
        /// 模板 id（一次订阅可能有多条通知，带有多个 id）
        /// </summary>
        public string TemplateId { get; set; }
        /// <summary>
        /// 用户点击行为（同意、取消发送通知）
        /// accept 同意
        /// reject 取消
        /// </summary>
        public string SubscribeStatusString { get; set; }
        /// <summary>
        /// 场景 值
        /// 1	弹窗来自 H5 页面
        /// 2	弹窗来自图文消息
        /// </summary>
        public string PopupScene { get; set; }
    }
}