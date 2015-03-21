﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：Card_BaseInfoBase.cs
    文件功能描述：基本的卡券数据，所有卡券通用。作为 Card_BaseInfo和 的基类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 基本的卡券数据，所有卡券通用。作为 Card_BaseInfo和 的基类
    /// </summary>
    public class Card_BaseInfoBase
    {
        /// <summary>
        /// 卡券的商户logo，尺寸为300*300。
        /// 必填
        /// </summary>
        public string logo_url { get; set; }
        /// <summary>
        /// code 码展示类型
        /// 必填
        /// </summary>
        public Card_CodeType code_type { get; set; }
        /// <summary>
        /// 商户名字,字数上限为12 个汉字。（填写直接提供服务的商户名， 第三方商户名填写在source 字段）
        /// 必填
        /// </summary>
        public string brand_name { get; set; }
        /// <summary>
        /// 券名，字数上限为9 个汉字。(建议涵盖卡券属性、服务及金额)
        /// 必填
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 券名的副标题，字数上限为18个汉字。
        /// 非必填
        /// </summary>
        public string sub_title { get; set; }
        /// <summary>
        /// 券颜色。按色彩规范标注填写Color010-Color100
        /// 必填
        /// </summary>
        public string color { get; set; }
        /// <summary>
        /// 使用提醒，字数上限为9 个汉字。（一句话描述，展示在首页，示例：请出示二维码核销卡券）
        /// 必填
        /// </summary>
        public string notice { get; set; }
        /// <summary>
        /// 客服电话
        /// 非必填
        /// </summary>
        public string service_phone { get; set; }
        /// <summary>
        /// 第三方来源名，例如同程旅游、格瓦拉。
        /// 非必填
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 使用说明。长文本描述，可以分行，上限为1000 个汉字。
        /// 必填
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 每人使用次数限制
        /// 非必填
        /// </summary>
        public int use_limit { get; set; }
        /// <summary>
        /// 每人最大领取次数，不填写默认等于quantity。
        /// 非必填
        /// </summary>
        public int get_limit { get; set; }
        /// <summary>
        /// 是否自定义code 码。填写true或false，不填代表默认为false。
        /// 非必填
        /// </summary>
        public bool use_custom_code { get; set; }
        /// <summary>
        /// 是否指定用户领取，填写true或false。不填代表默认为否。
        /// 非必填
        /// </summary>
        public bool bind_openid { get; set; }
        /// <summary>
        /// 领取卡券原生页面是否可分享，填写true 或false，true 代表可分享。默认可分享。
        /// 非必填
        /// </summary>
        public bool can_share { get; set; }
        /// <summary>
        /// 卡券是否可转赠，填写true 或false,true 代表可转赠。默认可转赠。
        /// 非必填
        /// </summary>
        public bool can_give_friend { get; set; }
        /// <summary>
        /// 门店位置ID。商户需在mp 平台上录入门店信息或调用批量导入门店信息接口获取门店位置ID。
        /// 非必填
        /// </summary>
        public List<string> location_id_list { get; set; }
        /// <summary>
        /// 使用日期，有效期的信息
        /// 必填
        /// </summary>
        public Card_BaseInfo_DateInfo date_info { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public Card_BaseInfo_Sku sku { get; set; }
        /// <summary>
        /// 商户自定义cell 名称
        /// 非必填
        /// </summary>
        public Card_UrlNameType url_name_type { get; set; }
        /// <summary>
        /// 商户自定义url 地址，支持卡券页内跳转,跳转页面内容需与自定义cell 名称保持一致。
        /// 非必填
        /// </summary>
        public string custom_url { get; set; }
    }
}
