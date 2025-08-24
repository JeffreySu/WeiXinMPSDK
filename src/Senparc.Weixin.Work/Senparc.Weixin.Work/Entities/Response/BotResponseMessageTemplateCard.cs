/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotResponseMessageTemplateCard.cs
    文件功能描述：机器人响应回复模板卡片消息

    
    创建标识：Wang Qian - 20250805
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.NeuChar;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 模板卡片消息（未实现）
    /// </summary>
    public class BotResponseMessageTemplateCard : WorkBotResponseMessageBase
{
    public override string msgtype { get; set; } = "template_card";
    
    // 模板卡片主体
    public Template_Card template_card { get; set; }

    public class Template_Card
    {
        public string card_type { get; set; }
        public Source source { get; set; }
        public Main_Title main_title { get; set; }
        public List<Select_Item> select_list { get; set; }
        public Submit_Button submit_button { get; set; }
        public string task_id { get; set; }
    }

    public class Source
    {
        public string icon_url { get; set; }
        public string desc { get; set; }
    }

    public class Main_Title
    {
        public string title { get; set; }
        public string desc { get; set; }
    }

    public class Select_Item
    {
        public string question_key { get; set; }
        public string title { get; set; }
        public bool? disable { get; set; }   // 文档里可选时用可空
        public string selected_id { get; set; }
        public List<Option> option_list { get; set; }
    }

    public class Option
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    public class Submit_Button
    {
        public string text { get; set; }
        public string key { get; set; }
    }
}
}