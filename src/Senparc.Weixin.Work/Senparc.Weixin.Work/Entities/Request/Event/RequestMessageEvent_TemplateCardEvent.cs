/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc

    文件名：RequestMessageEvent_TemplateCardEvent.cs
    文件功能描述：企业微信-模板卡片事件推送


    创建标识：IcedMango - 20241114

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    ///     企业微信-模板卡片事件推送
    /// </summary>
    public class RequestMessageEvent_TemplateCardEvent : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        ///     事件类型(template_card_event，点击模板卡片按钮)
        /// </summary>
        public override Event Event => Event.TEMPLATE_CARD_EVENT;
        
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
        
        /// <summary>
        ///     
        /// </summary>
        [XmlArray("SelectedItems")]
        [XmlArrayItem("SelectedItem")]
        public List<TemplateCard_SelectedItem> SelectedItems { get; set; }
    }
    
    /// <summary>
    ///     通用模板卡片的类型
    /// </summary>
    public enum TemplateCard_CardTypeEnum
    {
        text_notice,
        news_notice,
        button_interaction,
        vote_interaction,
        multiple_interaction
    }
    
    public class TemplateCard_SelectedItem
    {
        /// <summary>
        ///     问题的key值
        /// </summary>
        [XmlElement("QuestionKey")]
        public string QuestionKey { get; set; }

        [XmlArray("OptionIds")]
        [XmlArrayItem("OptionId")]
        public List<string> OptionIds { get; set; }
    }
}