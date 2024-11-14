/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc

    文件名：RequestMessageEvent_TemplateCardMenuEvent.cs
    文件功能描述：通用模板卡片右上角菜单事件推送


    创建标识：IcedMango - 20241114

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    ///     企业微信-模板卡片事件推送
    /// </summary>
    public class RequestMessageEvent_TemplateCardMenuEvent : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        ///     事件类型(template_card_menu_event，通用模板卡片右上角菜单事件推送)
        /// </summary>
        public override Event Event => Event.TEMPLATE_CARD_MENU_EVENT;
        
        /// <summary>
        ///     与发送模板卡片消息时指定的按钮btn:key值相同
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        ///     与发送模板卡片消息时指定的task_id相同
        /// </summary>
        public string TaskId { get; set; }
        
        /// <summary>
        ///     通用模板卡片的类型
        /// </summary>
        public TemplateCard_CardTypeEnum CardType { get; set; }
        
        /// <summary>
        ///     用于调用更新卡片接口的ResponseCode，72小时内有效，且只能使用一次
        /// </summary>
        public string ResponseCode { get; set; }
    }
}