/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：OrderKey.cs
    文件功能描述：订单，需要上传物流信息的订单
    
    
    创建标识：Yaofeng - 20231026

----------------------------------------------------------------*/

namespace Senparc.Weixin.Open.WxaAPIs.SecOrder
{
    /// <summary>
    /// 订单，需要上传物流信息的订单
    /// </summary>
    public class OrderKey
    {
        /// <summary>
        /// 【必填】订单单号类型，用于确认需要上传详情的订单。枚举值1，使用下单商户号和商户侧单号；枚举值2，使用微信支付单号。
        /// </summary>
        public string order_number_type { get; set; }

        /// <summary>
        /// 【非必填】原支付交易对应的微信订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 【非必填】支付下单商户的商户号，由微信支付生成并下发。
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 【非必填】商户系统内部订单号，只能是数字、大小写字母`_-*`且在同一个商户号下唯一
        /// </summary>
        public string out_trade_no { get; set; }
    }
}