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
    
    文件名：CardCreateResultJson.cs
    文件功能描述：创建卡券返回结果
    创建标识：Senparc - 20150211
    
    创建标识：Senparc - 20160520
    创建描述：添加开通券点账户返回结果
    
    创建描述：添加对优惠券批价的返回结果
    
    创建描述：查询券点余额返回结果
  
    创建描述：确认兑换库存返回结果
   
    创建描述：查询订单详情返回结果
   
    创建描述：查询券点流水详情返回结果
   
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150323
    修改描述：添加上传logo返回结果
 
    创建标识：Senparc - 20160808
    创建描述：添加Card_UpdateResultJson

    创建标识：Senparc - 20170110
    创建描述：CreateQRResultJson添加url和show_qrcode_url属性

----------------------------------------------------------------*/



using System.CodeDom;
using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 创建卡券返回结果
    /// </summary>
    public class CardCreateResultJson : WxJsonResult
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string card_id { get; set; }
    }
    /// <summary>
    /// 开通券点账户返回结果
    /// </summary>
    public class PayActiveResultJson : WxJsonResult
    {
        /// <summary>
        /// 奖励券点数量，以元为单位，微信卡券对每一个新开通券点账户的商户奖励200个券点，点击查看券点规则什么是券点？            
        /// </summary>
        public string reward { get; set; }
    }
    /// <summary>
    /// 对优惠券批价的返回结果
    /// </summary>
    public class GetpayPriceResultJson : WxJsonResult
    {
        /// <summary>
        /// 本次批价的订单号，用于下面的确认充值库存接口，仅对当前订单有效且仅可以使用一次，60s内可用于兑换库存。
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 本次需要支付的券点总额度
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 本次需要支付的免费券点额度
        /// </summary>
        public string free_coin { get; set; }
        /// <summary>
        /// 本次需要支付的付费券点额度
        /// </summary>
        public string pay_coin { get; set; }
    }
    /// <summary>
    /// 查询券点余额返回结果
    /// </summary>
    public class GetCoinsInfoResultJson : WxJsonResult
    {
        /// <summary>
        /// 免费券点数目
        /// </summary>
        public int free_coin { get; set; }
        /// <summary>
        /// 免费券点数目
        /// </summary>
        public int pay_coin { get; set; }
        /// <summary>
        /// 全部券点数目
        /// </summary>
        public int total_coin { get; set; }
    }
    /// <summary>
    /// 确认兑换库存返回结果
    /// </summary>
    public class PayRechargeResultJson : WxJsonResult
    {
        /// <summary>
        /// 本次支付的订单号，用于查询订单状态
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 支付二维码的的链接，开发者可以调用二维码生成的公开库转化为二维码显示在网页上，微信扫码支付
        /// </summary>
        public string qrcode_url { get; set; }
        /// <summary>
        /// 二维码的数据流，开发者可以使用写入一个文件的方法显示该二维码
        /// </summary>
        public string qrcode_buffer { get; set; }
    }
    /// <summary>
    /// 查询订单详情返回结果
    /// </summary>
    public class PayGetOrderResultJson : WxJsonResult
    {
        /// <summary>
        /// 订单信息结构体
        /// </summary>
        public PayGetOrder_Order_Info order_info { get; set; }

        public class PayGetOrder_Order_Info
        {
            /// <summary>
            /// 订单号
            /// </summary>
            public string order_id { get; set; }
            /// <summary>
            /// 订单状态，ORDER_STATUS_WAITING 等待支付 ORDER_STATUS_SUCC 支付成功 ORDER_STATUS_FINANCE_SUCC 加代币成功 ORDER_STATUS_QUANTITY_SUCC 加库存成功 ORDER_STATUS_HAS_REFUND 已退币 ORDER_STATUS_REFUND_WAITING 等待退币确认 ORDER_STATUS_ROLLBACK 已回退,系统失败 ORDER_STATUS_HAS_RECEIPT 已开发票
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// 订单创建时间
            /// </summary>
            public string create_time { get; set; }
            /// <summary>
            /// 支付完成时间
            /// </summary>
            public string pay_finish_time { get; set; }
            /// <summary>
            /// 支付描述，一般为微信支付充值
            /// </summary>
            public string desc { get; set; }
            /// <summary>
            /// 本次充值的付费券点数量，以元为单位
            /// </summary>
            public string free_coin_count { get; set; }
            /// <summary>
            /// 二维码的数据流，开发者可以使用写入一个文件的方法显示该二维码
            /// </summary>
            public string pay_coin_count { get; set; }
            /// <summary>
            /// 回退的免费券点
            /// </summary>
            public string refund_free_coin_count { get; set; }
            /// <summary>
            /// 回退的付费券点
            /// </summary>
            public string refund_pay_coin_count { get; set; }
            /// <summary>
            /// 支付人的openid
            /// </summary>
            public string openid { get; set; }
            /// <summary>
            /// 订单类型，ORDER_TYPE_WXPAY为充值
            /// </summary>
            public string order_type { get; set; }

        }
    }
    /// <summary>
    /// 查询券点流水详情返回结果
    /// </summary>
    public class GetOrderListResultJson : WxJsonResult
    {
        /// <summary>
        /// 符合条件的订单总数量
        /// </summary>
        public int total_num { get; set; }
        /// <summary>
        /// 显示的订单详情列表，根据offset和count来显示
        /// </summary>
        public List<GetOrderList_OrderList> order_list { get; set; }


    }
    public class GetOrderList_OrderList
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_id { get; set; }
        /// <summary>
        /// 订单状态，ORDER_STATUS_WAITING 等待支付 ORDER_STATUS_SUCC 支付成功 ORDER_STATUS_FINANCE_SUCC 加代币成功 ORDER_STATUS_QUANTITY_SUCC 加库存成功 ORDER_STATUS_HAS_REFUND 已退币 ORDER_STATUS_REFUND_WAITING 等待退币确认 ORDER_STATUS_ROLLBACK 已回退,系统失败 ORDER_STATUS_HAS_RECEIPT 已开发票
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public int create_time { get; set; }
        /// <summary>
        /// 支付完成时间
        /// </summary>
        public int pay_finish_time { get; set; }
        /// <summary>
        /// 支付描述，一般为微信支付充值
        /// </summary>
        public string desc { get; set; }
        /// <summary>
        /// 本次充值的付费券点数量，以元为单位
        /// </summary>
        public string free_coin_count { get; set; }
        /// <summary>
        /// 二维码的数据流，开发者可以使用写入一个文件的方法显示该二维码
        /// </summary>
        public string pay_coin_count { get; set; }
        /// <summary>
        /// 回退的免费券点
        /// </summary>
        public string refund_free_coin_count { get; set; }
        /// <summary>
        /// 回退的付费券点
        /// </summary>
        public string refund_pay_coin_count { get; set; }
        /// <summary>
        /// 支付人的openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 订单类型，ORDER_TYPE_WXPAY为充值
        /// </summary>
        public string order_type { get; set; }
    }

    /// <summary>
    /// 获取颜色列表返回结果
    /// </summary>
    public class GetColorsResultJson : WxJsonResult
    {
        /// <summary>
        /// 颜色列表
        /// </summary>
        public List<Card_Color> colors { get; set; }
    }

    public class Card_Color
    {
        /// <summary>
        /// 可以填入的color 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 对应的颜色数值
        /// </summary>
        public string value { get; set; }
    }
    /// <summary>
    /// 生成卡券二维码返回结果
    /// API：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1451025062&token=&lang=zh_CN
    /// </summary>
    public class CreateQRResultJson : WxJsonResult
    {
        /// <summary>
        /// 获取的二维码ticket，凭借此ticket调用通过ticket换取二维码接口可以在有效时间内换取二维码。
        /// </summary>
        public string ticket { get; set; }
        /// <summary>
        /// 二维码图片解析后的地址，开发者可根据该地址自行生成需要的二维码图片
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 二维码显示地址，点击后跳转二维码页面
        /// </summary>
        public string show_qrcode_url { get; set; }
    }

    /// <summary>
    /// 消耗code返回结果
    /// </summary>
    public class CardConsumeResultJson : WxJsonResult
    {
        public CardId card { get; set; }
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
    }

    public class CardId
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string card_id { get; set; }
    }

    /// <summary>
    /// code 解码
    /// </summary>
    public class CardDecryptResultJson : WxJsonResult
    {
        public string code { get; set; }
    }

    /// <summary>
    /// 上传logo返回结果
    /// </summary>
    public class Card_UploadLogoResultJson : WxJsonResult
    {
        public string url { get; set; }
    }

    public class Card_UpdateResultJson : WxJsonResult
    {
        ///
        /// 此次更新是否需要提审，true为需要，false为不需要。 
        ///
        public bool send_check { get; set; }
    }

    /// <summary>
    /// 获取开卡插件参数
    /// </summary>
    public class Card_GetUrlResultJson:WxJsonResult
    {
        /// <summary>
        /// 返回的url，内含调用开卡插件所需的参数
        /// </summary>
        public string url { get; set; }
    }
}
