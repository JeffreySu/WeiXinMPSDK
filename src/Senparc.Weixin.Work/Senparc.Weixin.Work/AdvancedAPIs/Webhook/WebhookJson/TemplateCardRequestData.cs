/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：WebhookApi.cs
    文件功能描述：Webhook群机器人相关Api
    
    
    创建标识：mc7246 - 20230211
    创建描述：Webhook 发送模板卡片请求信息

----------------------------------------------------------------*/

/*
    官方文档：https://developer.work.weixin.qq.com/document/path/91770#模版卡片类型
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Webhook
{
    /// <summary>
    /// 文本通知模版具体的模版卡片参数
    /// </summary>
    public class TemplateCardRequestData
    {
        /// <summary>
        /// 模版卡片的模版类型，文本通知模版卡片的类型为text_notice，图文展示模版卡片的类型为news_notice
        /// </summary>
        public string card_type { get; set; }

        /// <summary>
        /// 卡片来源样式信息，不需要来源样式可不填写
        /// </summary>
        public SourceInfo source { get; set; }

        /// <summary>
        /// 模版卡片的主要内容，包括一级标题和标题辅助信息
        /// </summary>
        public MainTitleInfo main_title { get; set; }

        /// <summary>
        /// 图文展示模板有效
        /// 图片样式
        /// </summary>
        public CardImage card_image { get; set; }

        /// <summary>
        /// 图文展示模板有效
        /// 左图右文样式
        /// </summary>
        public ImageTextArea image_text_area { get; set; }

        /// <summary>
        /// 关键数据样式
        /// </summary>
        public EmphasisContentInfo emphasis_content { get; set; }

        /// <summary>
        /// 引用文献样式，建议不与关键数据共用
        /// </summary>
        public QuoteAreaInfo quote_area { get; set; }

        /// <summary>
        /// 二级普通文本，建议不超过112个字。模版卡片主要内容的一级标题main_title.title和二级普通文本sub_title_text必须有一项填写
        /// </summary>
        public string sub_title_text { get; set; }

        /// <summary>
        /// 图文展示模板有效
        /// 卡片二级垂直内容，该字段可为空数组，但有数据的话需确认对应字段是否必填，列表长度不超过4
        /// </summary>
        public List<VerticalContentInfo> vertical_content_list = new List<VerticalContentInfo>();

        /// <summary>
        /// 二级标题+文本列表，该字段可为空数组，但有数据的话需确认对应字段是否必填，列表长度不超过6
        /// </summary>
        public List<HorizontalContentInfo> horizontal_content_list = new List<HorizontalContentInfo>();

        /// <summary>
        /// 跳转指引样式的列表，该字段可为空数组，但有数据的话需确认对应字段是否必填，列表长度不超过3
        /// </summary>
        public List<JumpInfo> jump_list = new List<JumpInfo>();

        /// <summary>
        /// 整体卡片的点击跳转事件，text_notice模版卡片中该字段为必填项
        /// </summary>
        public CardActionInfo card_action { get; set; }

    }

    /// <summary>
    /// 卡片来源样式信息
    /// </summary>
    public class SourceInfo
    {
        /// <summary>
        /// 来源图片的url
        /// </summary>
        public string icon_url { get; set; }

        /// <summary>
        /// 来源图片的描述，建议不超过13个字
        /// </summary>
        public string desc { get; set; }

        /// <summary>
        /// 来源文字的颜色，目前支持：0(默认) 灰色，1 黑色，2 红色，3 绿色
        /// </summary>
        public int desc_color { get; set; }

    }

    /// <summary>
    /// 模版卡片的主要内容，包括一级标题和标题辅助信息
    /// </summary>
    public class MainTitleInfo
    {
        /// <summary>
        /// 一级标题，建议不超过26个字。模版卡片主要内容的一级标题main_title.title和二级普通文本sub_title_text必须有一项填写
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 标题辅助信息，建议不超过30个字
        /// </summary>
        public string desc { get; set; }

    }

    /// <summary>
    /// 图文展示模板有效
    /// 图片样式
    /// </summary>
    public class CardImage
    {
        /// <summary>
        /// 图片的url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 图片的宽高比，宽高比要小于2.25，大于1.3，不填该参数默认1.3
        /// </summary>
        public float aspect_ratio { get; set; } = 1.3F;

    }

    /// <summary>
    /// 图文展示模板有效
    /// 左图右文样式
    /// </summary>
    public class ImageTextArea
    {
        /// <summary>
        /// 左图右文样式区域点击事件，0或不填代表没有点击事件，1 代表跳转url，2 代表跳转小程序
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 点击跳转的url，image_text_area.type是1时必填
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 点击跳转的小程序的appid，必须是与当前应用关联的小程序，image_text_area.type是2时必填
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 点击跳转的小程序的pagepath，image_text_area.type是2时选填
        /// </summary>
        public string pagepath { get; set; }

        /// <summary>
        /// 左图右文样式的标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 左图右文样式的描述
        /// </summary>
        public string desc { get; set; }

        /// <summary>
        /// 左图右文样式的图片url
        /// </summary>
        public string image_url { get; set; }
    }

    /// <summary>
    /// 关键数据样式
    /// </summary>
    public class EmphasisContentInfo
    {
        /// <summary>
        /// 关键数据样式的数据内容，建议不超过10个字
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 关键数据样式的数据描述内容，建议不超过15个字
        /// </summary>
        public string desc { get; set; }

    }

    /// <summary>
    /// 引用文献样式，建议不与关键数据共用
    /// </summary>
    public class QuoteAreaInfo
    {
        /// <summary>
        /// 引用文献样式区域点击事件，0或不填代表没有点击事件，1 代表跳转url，2 代表跳转小程序
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 点击跳转的url，quote_area.type是1时必填
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 点击跳转的小程序的appid，quote_area.type是2时必填
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 点击跳转的小程序的pagepath，quote_area.type是2时选填
        /// </summary>
        public string pagepath { get; set; }


        /// <summary>
        /// 引用文献样式的标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 引用文献样式的引用文案
        /// </summary>
        public string quote_text { get; set; }

    }

    /// <summary>
    /// 图文展示模板有效
    /// 卡片二级垂直内容，该字段可为空数组，但有数据的话需确认对应字段是否必填，列表长度不超过4
    /// </summary>
    public class VerticalContentInfo
    {
        /// <summary>
        /// 卡片二级标题，建议不超过26个字
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 二级普通文本，建议不超过112个字
        /// </summary>
        public string desc { get; set; }
    }

    /// <summary>
    /// 二级标题+文本列表
    /// </summary>
    public class HorizontalContentInfo
    {
        /// <summary>
        /// 链接类型，0或不填代表是普通文本，1 代表跳转url，2 代表下载附件，3 代表@员工
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 二级标题，建议不超过5个字
        /// </summary>
        public string keyname { get; set; }

        /// <summary>
        /// 二级文本，如果horizontal_content_list.type是2，该字段代表文件名称（要包含文件类型），建议不超过26个字
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 链接跳转的url，horizontal_content_list.type是1时必填
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 附件的media_id，horizontal_content_list.type是2时必填
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 被@的成员的userid，horizontal_content_list.type是3时必填
        /// </summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 跳转指引样式
    /// </summary>
    public class JumpInfo
    {
        /// <summary>
        /// 跳转链接类型，0或不填代表不是链接，1 代表跳转url，2 代表跳转小程序
        /// </summary>
        public int type { get; set; } = 0;

        /// <summary>
        /// 跳转链接的url，jump_list.type是1时必填
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 跳转链接的小程序的appid，jump_list.type是2时必填
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 跳转链接的小程序的pagepath，jump_list.type是2时选填
        /// </summary>
        public string pagepath { get; set; }

        /// <summary>
        /// 跳转链接样式的文案内容，建议不超过13个字
        /// </summary>
        public string title { get; set; }
    }

    /// <summary>
    /// 整体卡片的点击跳转事件，text_notice模版卡片中该字段为必填项
    /// </summary>
    public class CardActionInfo
    {
        /// <summary>
        /// 卡片跳转类型，1 代表跳转url，2 代表打开小程序。text_notice模版卡片中该字段取值范围为[1,2]
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 跳转事件的url，card_action.type是1时必填
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 跳转事件的小程序的appid，card_action.type是2时必填
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 跳转事件的小程序的pagepath，card_action.type是2时选填
        /// </summary>
        public string pagepath { get; set; }
    }


}
