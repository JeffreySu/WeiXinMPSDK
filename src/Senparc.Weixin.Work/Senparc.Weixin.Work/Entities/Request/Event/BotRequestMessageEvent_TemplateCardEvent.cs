/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：BotRequestMessageEvent_TemplateCardEvent.cs
    文件功能描述：模板卡片事件推送(template_card_event)
    按钮交互、投票选择和多项选择模版卡片中的按钮点击后，
    企业微信会将相应事件发送给机器人

    注：在企业微信机器人中，模板卡片事件推送和通用模板卡片右上角菜单事件是同一个事件TEMPLATE_CARD_EVENT
    
    
    创建标识：Wang Qian - 20250804
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 模板卡片事件推送（template_card_event）
    /// </summary>
    public class BotRequestMessageEvent_TemplateCardEvent : BotRequestMessageEventBase
    {
        /// <summary>
        /// 事件对象
        /// </summary>
        public new Event_TemplateCard @event { get; set; }

        public class Event_TemplateCard
        {
            public string eventtype { get; set; } = "template_card_event";
            public Template_Card_Event template_card_event { get; set; }
        }

        public class Template_Card_Event
        {
            public string card_type { get; set; }
            public string event_key { get; set; }
            public string taskid { get; set; }
            public Selected_Items selected_items { get; set; }
        }

        public class Selected_Items
        {
            public List<Selected_Item> selected_item { get; set; }
        }

        public class Selected_Item
        {
            public string question_key { get; set; }
            public Option_Ids option_ids { get; set; }
        }

        public class Option_Ids
        {
            public List<string> option_id { get; set; }
        }
    }
}