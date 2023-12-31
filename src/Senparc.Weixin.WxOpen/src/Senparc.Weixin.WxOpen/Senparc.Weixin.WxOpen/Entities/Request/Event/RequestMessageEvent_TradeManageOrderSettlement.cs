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
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageEvent_TradeManageOrderSettlement.cs
    文件功能描述：事件之小程序发货信息事件推送
    
    
    创建标识：mc7246 - 20211209
    https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#八、相关消息推送

----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 订单将要结算或已经结算
    /// 订单完成发货时
    /// 订单结算时
    /// </summary>
    public class RequestMessageEvent_TradeManageOrderSettlement : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.trade_manage_order_settlement; }
        }

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string merchant_id { get; set; }

        /// <summary>
        /// 子商户号
        /// </summary>
        public string sub_merchant_id { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string merchant_trade_no { get; set; }

        /// <summary>
        /// 支付成功时间，秒级时间戳
        /// </summary>
        public long pay_time { get; set; }

        /// <summary>
        /// 发货时间，秒级时间戳
        /// </summary>
        public long shipped_time { get;set; }

        /// <summary>
        /// 预计结算时间，秒级时间戳。发货时推送才有该字段
        /// </summary>
        public long estimated_settlement_time { get; set; }

        /// <summary>
        /// 确认收货方式：1. 自动确认收货；2. 手动确认收货。结算时推送才有该字段
        /// </summary>
        public long confirm_receive_method { get; set; }

        /// <summary>
        /// 确认收货时间，秒级时间戳。结算时推送才有该字段
        /// </summary>
        public long confirm_receive_time { get; set; }

        /// <summary>
        /// 订单结算时间，秒级时间戳。结算时推送才有该字段
        /// </summary>
        public long settlement_time { get; set; }

    }
}
