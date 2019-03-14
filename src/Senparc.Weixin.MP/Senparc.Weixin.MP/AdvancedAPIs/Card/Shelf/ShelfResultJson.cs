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
    
    文件名：ShelfResultJson.cs
    文件功能描述：创建货架返回结果
    
    
    创建标识：Senparc - 20150907
----------------------------------------------------------------*/



using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 创建货架返回结果
    /// </summary>
    public class ShelfCreateResultJson : WxJsonResult
    {
        /// <summary>
        /// 货架链接。
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 货架ID。货架的唯一标识。
        /// </summary>
        public int page_id { get; set; }
    }

    /// <summary>
    /// 查询-礼品卡货架信息返回结果
    /// </summary>
    public class GetGiftCardPageInfoResultJson : WxJsonResult
    {
        /// <summary>
        /// 货架信息结构体
        /// </summary>
        public GiftCardPageData page { get; set; }
    }

    /// <summary>
    /// 查询-礼品卡货架列表返回结果
    /// </summary>
    public class GetGiftCardPageListResultJson : WxJsonResult
    {
        /// <summary>
        /// 礼品卡货架id列表
        /// </summary>
        public List<string> page_id_list { get; set; }
    }

    /// <summary>
    /// 下架-礼品卡货架返回结果
    /// </summary>
    public class DownGiftCardPageResultJson : WxJsonResult
    {
        /// <summary>
        /// 控制结果的结构体
        /// </summary>
        public ConTrolResult control_info { get; set; }
    }

    /// <summary>
    /// 控制结果的结构体
    /// </summary>
    public class ConTrolResult
    {
        /// <summary>
        /// 商户控制的该appid下所有货架的状态
        /// </summary>
        public string biz_control_type { get; set; }
        /// <summary>
        /// 系统控制的商家appid下所有的货架状态
        /// </summary>
        public string system_biz_control_type { get; set; }
        /// <summary>
        /// Page列表的结构体，为商户下所有page列表
        /// </summary>
        public List<PageListItem> list { get; set; }
    }

    /// <summary>
    /// 货架
    /// </summary>
    public class PageListItem
    {
        /// <summary>
        /// Page的唯一id
        /// </summary>
        public string page_id { get; set; }
        /// <summary>
        /// 商户控制的货架状态
        /// </summary>
        public string page_control_type { get; set; }
        /// <summary>
        /// 由系统控制的货架状态
        /// </summary>
        public string system_page_control_type { get; set; }
    }

    /// <summary>
    /// 申请微信支付礼品卡权限返回结果
    /// </summary>
    public class PayGiftCardResultJson : WxJsonResult
    {
        public string url { get; set; }
    }

    /// <summary>
    /// 单个礼品卡订单信息返回结果
    /// </summary>
    public class GiftCardOrderItemResultJson : WxJsonResult
    {
        /// <summary>
        /// 订单结构体
        /// </summary>
        public GiftCardOrder order { get; set; }
    }

    /// <summary>
    /// 订单结构体
    /// </summary>
    public class GiftCardOrder
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 货架的id
        /// </summary>
        public string page_id { get; set; }
        /// <summary>
        /// 微信支付交易订单号
        /// </summary>
        public string trans_id { get; set; }
        /// <summary>
        /// 订单创建时间，十位时间戳（utc+8）
        /// </summary>
        public int create_time { get; set; }
        /// <summary>
        /// 订单支付完成时间，十位时间戳（utc+8）
        /// </summary>
        public int pay_finish_time { get; set; }
        /// <summary>
        /// 全部金额，以分为单位
        /// </summary>
        public double total_price { get; set; }
        /// <summary>
        /// 购买者的openid
        /// </summary>
        public string open_id { get; set; }
        /// <summary>
        /// 接收者的openid
        /// </summary>
        public string accepter_openid { get; set; }
        /// <summary>
        /// 购买货架的渠道参数
        /// </summary>
        public string outer_str { get; set; }
        /// <summary>
        /// 该订单对应礼品卡是否发送至群
        /// </summary>
        public bool IsChatRoom { get; set; }
        /// <summary>
        /// 卡列表结构
        /// </summary>
        public List<GiftCardItem> card_list { get; set; }
    }

    /// <summary>
    /// 礼品卡
    /// </summary>
    public class GiftCardItem
    {
        /// <summary>
        /// 购买的卡card_id
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 该卡的价格
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// 用户获得的code
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 默认祝福语，当用户填入了祝福语时该字段为空
        /// </summary>
        public string default_gifting_msg { get; set; }
        /// <summary>
        /// 用户选择的背景图
        /// </summary>
        public string background_pic_url { get; set; }
        /// <summary>
        /// 自定义卡面说明
        /// </summary>
        public string outer_img_id { get; set; }
        /// <summary>
        /// 礼品卡发送至群时，领取者的openid
        /// </summary>
        public string accepter_openid { get; set; }

    }

    /// <summary>
    /// 批量查询礼品卡订单信息返回结果
    /// </summary>
    public class GiftCardOrderListResultJson : WxJsonResult
    {
        /// <summary>
        /// 总计订单数
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 订单列表结构
        /// </summary>
        public List<GiftCardOrder> order_list { get; set; }
    }

    /// <summary>
    /// 更新用户礼品卡信息返回结果
    /// </summary>
    public class UpdateUserGiftCardResultJson : WxJsonResult
    {
        /// <summary>
        /// 当前用户积分总额
        /// </summary>
        public int result_bonus { get; set; }
        /// <summary>
        /// 当前用户预存总金额
        /// </summary>
        public int result_balance { get; set; }
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
    }
}
