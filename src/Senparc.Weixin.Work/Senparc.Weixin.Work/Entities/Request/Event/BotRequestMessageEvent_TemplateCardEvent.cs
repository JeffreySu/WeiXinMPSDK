/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageEvent_TemplateCardEvent.cs
    文件功能描述：模板卡片事件推送(template_card_event)
    按钮交互、投票选择和多项选择模版卡片中的按钮点击后，
    企业微信会将相应事件发送给机器人

    注：在企业微信机器人中，模板卡片事件推送和通用模板卡片右上角菜单事件是同一个事件TEMPLATE_CARD_EVENT
    
    
    创建标识：Wang Qian - 20250804
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 模板卡片事件推送
    /// </summary>
    public class BotRequestMessageEvent_TemplateCardEvent : BotRequestMessageEventBase, IBotRequestMessageEventBase, IBotRequestMessageEventKey
    {
        public override Event Event => Event.TEMPLATE_CARD_EVENT;

        /// <summary>
        /// 对应cardtype
        /// </summary>
        public TemplateCard_CardTypeEnum CardType { get; set; }

        /// <summary>
        /// 对应eventkey，用户点击的按钮交互模版卡片的按钮key
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 对应taskid，用户点击的交互模版卡片的task_id
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 对应selected_items，用户点击提交的选择框数据，参考SeletedItem结构说明
        /// </summary>
        [XmlArray("SelectedItems")]
        [XmlArrayItem("SelectedItem")]
        public List<TemplateCard_SelectedItem> SelectedItems { get; set; }


        
        
    }
}