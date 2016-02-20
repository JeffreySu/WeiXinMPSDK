/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageEvent_Location.cs
    文件功能描述：事件之微小店订单付款通知
    
    
    创建标识：Senparc - 20150211
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之微小店订单付款通知
    /// </summary>
    public class RequestMessageEvent_Merchant_Order : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.merchant_order; }
        }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SkuInfo { get; set; }
    }
}
