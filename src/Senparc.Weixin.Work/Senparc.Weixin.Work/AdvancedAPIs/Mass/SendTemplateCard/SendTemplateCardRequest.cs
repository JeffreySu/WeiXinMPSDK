#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright(C) 2023 Senparc
    
    文件名：SendTemplateCardRequest.cs
    文件功能描述：“发送模板卡片消息”接口请求信息
    
    
    创建标识：Senparc - 20220912

    修改标识：Senparc - 20230612
    修改描述：v3.15.20 增加更新模版卡片消息（PR #2850）

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Mass.SendTemplateCard
{
    /// <summary>
    /// “发送模板卡片消息”接口请求信息
    /// </summary>
    public class SendTemplateCardRequest
    {
        public string touser { get; set; }
        public string toparty { get; set; }
        public string totag { get; set; }
        public string msgtype { get; set; }
        public int agentid { get; set; }
        public Template_CardBase template_card { get; private set; }
        public int enable_id_trans { get; set; }
        public int enable_duplicate_check { get; set; }
        public int duplicate_check_interval { get; set; }

        SendTemplateCardRequest()
        {
            msgtype = "template_card";
        }

        public SendTemplateCardRequest(Template_CardBase templateCard) : this()
        {
            template_card = templateCard;
        }
    }

    #region 卡片数据

    #region 基类

    /// <summary>
    /// 所有卡片的公共属性
    /// </summary>
    public abstract class Template_CardBase
    {
        public string card_type { get; protected set; }
        public Source source { get; set; }
        public string task_id { get; set; }
        public Main_Title main_title { get; set; }

    }

    /// <summary>
    /// 带有跳转按钮等界面的卡片
    /// </summary>
    public abstract class Template_CardWithJumpListBase : Template_CardBase
    {
        public Action_Menu action_menu { get; set; }
        public Quote_Area quote_area { get; set; }
        //public Emphasis_Content emphasis_content { get; set; }
        //public string sub_title_text { get; set; }
        public Horizontal_Content_List[] horizontal_content_list { get; set; }
        public Jump_List[] jump_list { get; set; }
        public Card_Action card_action { get; set; }
    }

    #endregion

    #region 不同类型卡片

    /// <summary>
    /// 文本通知型
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/90236#%E6%96%87%E6%9C%AC%E9%80%9A%E7%9F%A5%E5%9E%8B</para>
    /// </summary>
    public class Template_Card_Text : Template_CardWithJumpListBase
    {
        public Emphasis_Content emphasis_content { get; set; }
        public string sub_title_text { get; set; }
        public Template_Card_Text()
        {
            base.card_type = "text_notice";
        }


        public class Emphasis_Content
        {
            public string title { get; set; }
            public string desc { get; set; }
        }

    }

    /// <summary>
    /// 图文展示型
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/90236#%E5%9B%BE%E6%96%87%E5%B1%95%E7%A4%BA%E5%9E%8B</para>
    /// </summary>
    public class Template_Card_NewsNotice : Template_CardWithJumpListBase
    {
        public Image_Text_Area image_text_area { get; set; }
        public Card_Image card_image { get; set; }
        public Template_Card_NewsNotice()
        {
            base.card_type = "news_notice";
        }


        public class Image_Text_Area
        {
            public int type { get; set; }
            public string url { get; set; }
            public string title { get; set; }
            public string desc { get; set; }
            public string image_url { get; set; }
        }

        public class Card_Image
        {
            public string url { get; set; }
            public float aspect_ratio { get; set; }
        }
    }

    /// <summary>
    /// 按钮交互型
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/90236#%E6%8C%89%E9%92%AE%E4%BA%A4%E4%BA%92%E5%9E%8B</para>
    /// </summary>
    public class Template_Card_ButtonInteraction : Template_CardWithJumpListBase
    {
        public Checkbox checkbox { get; set; }
        public Button_List[] button_list { get; set; }

        public string sub_title_text { get; set; }
        public Template_Card_ButtonInteraction()
        {
            base.card_type = "button_interaction";
        }


        public class Checkbox
        {
            public string question_key { get; set; }
            public Option_List[] option_list { get; set; }
            public int mode { get; set; }
        }

        public class Option_List
        {
            public string id { get; set; }
            public string text { get; set; }
            public bool is_checked { get; set; }
        }

        public class Button_List
        {
            public string text { get; set; }
            public string key { get; set; }

            public int type { get; set; }

            public int style { get; set; }

            public string url { get; set; }
        }

    }

    /// <summary>
    /// 投票选择型
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/90236#%E6%8A%95%E7%A5%A8%E9%80%89%E6%8B%A9%E5%9E%8B</para>
    /// </summary>
    public class Template_Card_VoteInteraction : Template_CardBase
    {

        public Checkbox checkbox { get; set; }
        public Submit_Button submit_button { get; set; }

        public Template_Card_VoteInteraction()
        {
            base.card_type = "vote_interaction";
        }


        public class Checkbox
        {
            public string question_key { get; set; }
            public Option_List[] option_list { get; set; }
            public int mode { get; set; }
        }

        public class Option_List
        {
            public string id { get; set; }
            public string text { get; set; }
            public bool is_checked { get; set; }
        }

        public class Submit_Button
        {
            public string text { get; set; }
            public string key { get; set; }
        }

    }

    /// <summary>
    /// 多项选择型
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/90236#%E5%A4%9A%E9%A1%B9%E9%80%89%E6%8B%A9%E5%9E%8B</para>
    /// </summary>
    public class Template_Card_MultipleInteraction : Template_CardBase
    {
        public Select_List[] select_list { get; set; }
        public Submit_Button submit_button { get; set; }

        public Template_Card_MultipleInteraction()
        {
            base.card_type = "multiple_interaction";
        }


        public class Submit_Button
        {
            public string text { get; set; }
            public string key { get; set; }
        }

        public class Select_List
        {
            public string question_key { get; set; }
            public string title { get; set; }
            public string selected_id { get; set; }
            public Option_List[] option_list { get; set; }
        }

        public class Option_List
        {
            public string id { get; set; }
            public string text { get; set; }
        }

    }

    public class Template_Card_Button : Template_CardBase
    {
        public Button button { get; set; }
        public class Button
        {
            public string replace_name { get; set; }
        }
    }
    #endregion

    #endregion

    public class Source
    {
        public string icon_url { get; set; }
        public string desc { get; set; }
        public int desc_color { get; set; }
    }

    public class Action_Menu
    {
        public string desc { get; set; }
        public Action_List[] action_list { get; set; }
    }

    public class Action_List
    {
        public string text { get; set; }
        public string key { get; set; }
    }

    public class Main_Title
    {
        public string title { get; set; }
        public string desc { get; set; }
    }

    public class Quote_Area
    {
        public int type { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string quote_text { get; set; }
    }

    public class Card_Action
    {
        public int type { get; set; }
        public string url { get; set; }
        public string appid { get; set; }
        public string pagepath { get; set; }
    }

    public class Horizontal_Content_List
    {
        public string keyname { get; set; }
        public string value { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public string media_id { get; set; }
        public string userid { get; set; }
    }

    public class Jump_List
    {
        public int type { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string appid { get; set; }
        public string pagepath { get; set; }
    }

}
