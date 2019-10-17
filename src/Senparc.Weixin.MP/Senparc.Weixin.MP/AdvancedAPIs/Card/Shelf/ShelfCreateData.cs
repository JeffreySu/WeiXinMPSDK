#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：CardUpdateData.cs
    文件功能描述：卡券更新需要的数据
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

----------------------------------------------------------------*/


using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 创建货架数据
    /// </summary>
    public class ShelfCreateData
    {
        /// <summary>
        /// 页面的banner图片链接，须调用。
        /// </summary>
        public string banner { get; set; }
        /// <summary>
        /// 页面的title。
        /// </summary>
        public string page_title { get; set; }
        /// <summary>
        /// 页面是否可以分享,填入true/false
        /// </summary>
        public bool can_share { get; set; }
        /// <summary>
        /// 投放页面的场景值；SCENE_NEAR_BY 附近 SCENE_MENU	自定义菜单 SCENE_QRCODE	二维码 SCENE_ARTICLE	公众号文章 SCENE_H5	h5页面 SCENE_IVR	自动回复 SCENE_CARD_CUSTOM_CELL	卡券自定义cell
        /// </summary>
        //[JsonConverter(typeof(StringEnumConverter))]
        public CardShelfCreate_Scene scene { get; set; }
        /// <summary>
        /// 卡券列表
        /// </summary>
        public List<ShelfCreateData_CardList> card_list { get; set; }
    }

    public class ShelfCreateData_CardList
    {
        /// <summary>
        /// 所要在页面投放的cardid
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 缩略图url
        /// </summary>
        public string thumb_url { get; set; }
    }

    /// <summary>
    /// 创建礼品卡货架数据
    /// </summary>
    public class GiftCardPageData
    {
        /// <summary>
        /// 货架id
        /// </summary>
        public int page_id { get; set; }
        /// <summary>
        /// 礼品卡货架名称
        /// </summary>
        public string page_title { get; set; }
        /// <summary>
        /// 是否支持一次购买多张及发送至群，填true或者false，若填true则支持，默认为false
        /// </summary>
        public bool support_multi { get; set; }
        /// <summary>
        /// 礼品卡货架是否支持买给自己
        /// </summary>
        public bool support_buy_for_self { get; set; }
        /// <summary>
        /// 礼品卡货架主题页顶部banner图片，须先将图片上传至CDN，建议尺寸为750px*630px
        /// </summary>
        public string banner_pic_url { get; set; }
        /// <summary>
        /// 主题结构体
        /// </summary>
        public List<GiftCardThemeItem> theme_list { get; set; }
        /// <summary>
        /// 主题分类列表
        /// </summary>
        public List<ThemeCategoryItem> category_list { get; set; }
        /// <summary>
        /// 商家地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 商家服务电话
        /// </summary>
        public string service_phone { get; set; }
        /// <summary>
        /// 商家使用说明，用于描述退款、发票等流程
        /// </summary>
        public string biz_description { get; set; }
        /// <summary>
        /// 该货架的订单是否支持开发票，填true或者false，若填true则需要调试文档2.2的流程，默认为false
        /// </summary>
        public bool need_receipt { get; set; }
        /// <summary>
        /// 商家自定义链接，用于承载退款、发票等流程
        /// </summary>
        public Cell cell_1 { get; set; }
        /// <summary>
        /// 商家自定义链接，用于承载退款、发票等流程
        /// </summary>
        public Cell cell_2 { get; set; }
    }

    /// <summary>
    /// 商户自定义服务入口结构体
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// 自定义入口名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 自定义入口链接
        /// </summary>
        public string url { get; set; }

    }

    /// <summary>
    /// 主题分类
    /// </summary>
    public class ThemeCategoryItem
    {
        /// <summary>
        /// 主题分类的名称
        /// </summary>
        public string title { get; set; }
    }

    /// <summary>
    /// 主题结构体
    /// </summary>
    public class GiftCardThemeItem
    {
        /// <summary>
        /// 主题的封面图片，须先将图片上传至CDN 大小控制在1000px*600px
        /// </summary>
        public string theme_pic_url { get; set; }
        /// <summary>
        /// 主题名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 主题title的颜色，直接传入色值
        /// </summary>
        public string title_color { get; set; }
        /// <summary>
        /// 该主题购买页是否突出商品名显示
        /// </summary>
        public bool show_sku_title_first { get; set; }
        /// <summary>
        /// 礼品卡列表，标识该主题可选择的面额
        /// </summary>
        public List<CommodityItem> item_list { get; set; }
        /// <summary>
        /// 封面列表
        /// </summary>
        public List<CardPicItem> pic_item_list { get; set; }
        /// <summary>
        /// 主题标号，对应category_list内的title字段， 若填写了category_list则每个主题必填该序号
        /// </summary>
        public int category_index { get; set; }
        /// <summary>
        /// 是否将当前主题设置为banner主题（主推荐）
        /// </summary>
        public bool is_banner { get; set; }
    }

    /// <summary>
    /// 商品结构体
    /// </summary>
    public class CommodityItem
    {
        /// <summary>
        /// 待上架的card_id
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 商品名，不填写默认为卡名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 商品缩略图，1000像素*600像素以下
        /// </summary>
        public string pic_url { get; set; }
        /// <summary>
        /// 商品简介
        /// </summary>
        public string desc { get; set; }
    }

    /// <summary>
    /// 卡面结构体
    /// </summary>
    public class CardPicItem
    {
        /// <summary>
        /// 卡面图片，须先将图片上传至CDN，大小控制在1000像素*600像素以下
        /// </summary>
        public string background_pic_url { get; set; }
        /// <summary>
        /// 自定义的卡面的标识
        /// </summary>
        public string outer_img_id { get; set; }
        /// <summary>
        /// 该卡面对应的默认祝福语，当用户没有编辑内容时会随卡默认填写为用户祝福内容
        /// </summary>
        public string default_gifting_msg { get; set; }

    }

    /// <summary>
    /// 下架礼品卡货架数据
    /// </summary>
    public class DownGiftCardPage
    {
        /// <summary>
        /// 需要下架的page_id
        /// </summary>
        public string page_id { get; set; }
        /// <summary>
        /// 是否下架所有货架
        /// </summary>
        public bool all { get; set; }
        /// <summary>
        /// 是否下架
        /// </summary>
        public bool maintain { get; set; }
    }

    /// <summary>
    /// 更新用户礼品卡信息数据
    /// </summary>
    public class UpdateUserGiftCardData
    {
        /// <summary>
        /// 卡券Code码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 支持商家激活时针对单个礼品卡分配自定义的礼品卡背景
        /// </summary>
        public string background_pic_url { get; set; }
        /// <summary>
        /// 商家自定义金额消耗记录，不超过14个汉字
        /// </summary>
        public string record_bonus { get; set; }
        /// <summary>
        /// 需要设置的余额全量值，传入的数值会直接显示。
        /// </summary>
        public int bonus { get; set; }
        /// <summary>
        /// 创建时字段custom_field1定义类型的最新数值，限制为4个汉字，12字节
        /// </summary>
        public string custom_field_value1 { get; set; }
        /// <summary>
        /// 创建时字段custom_field2定义类型的最新数值，限制为4个汉字，12字节
        /// </summary>
        public string custom_field_value2 { get; set; }
        /// <summary>
        /// 创建时字段custom_field3定义类型的最新数值，限制为4个汉字，12字节
        /// </summary>
        public string custom_field_value3 { get; set; }
        /// <summary>
        /// 控制本次积分变动后转赠入口是否出现
        /// </summary>
        public bool can_give_friend { get; set; }
    }
}