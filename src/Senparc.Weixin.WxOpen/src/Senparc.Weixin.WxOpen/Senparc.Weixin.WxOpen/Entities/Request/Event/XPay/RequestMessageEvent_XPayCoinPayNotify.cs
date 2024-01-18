/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageEvent_XPay_Coin_Pay_Notify.cs
    文件功能描述：小程序虚拟支付 - 代币支付推送
    
    
    创建标识：Senparc - 20231130

----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 代币支付推送
    /// </summary>
    public class RequestMessageEvent_XPayCoinPayNotify : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.xpay_goods_deliver_notify; }
        }

        /// <summary>
        /// 用户openid
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 业务订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 环境配置 0：现网环境（也叫正式环境）1：沙箱环境
        /// </summary>
        public string Env { get; set; }

        /// <summary>
        /// 微信支付信息 非微信支付渠道可能没有
        /// </summary>
        public WeChatPayInfo WeChatPayInfo { get; set; }

        /// <summary>
        /// 道具参数信息
        /// </summary>
        public CoinInfo CoinInfo { get; set; }
    }

    /// <summary>
    /// 道具参数信息
    /// </summary>
    public class CoinInfo
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 物品原始价格 （单位：分）
        /// </summary>
        public int OrigPrice { get; set; }

        /// <summary>
        /// 物品实际支付价格（单位：分）
        /// </summary>
        public int ActualPrice { get; set; }

        /// <summary>
        /// 透传信息
        /// </summary>
        public string Attach { get; set; }

    }
}
