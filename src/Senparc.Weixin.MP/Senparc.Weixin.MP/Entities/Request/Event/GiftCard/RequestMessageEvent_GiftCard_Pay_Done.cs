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
    
    文件名：RequestMessageEvent_GiftCard_Pay_Done.cs
    文件功能描述：用户购买礼品卡付款成功
    
    
    创建标识：Senparc - 20180906
    
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 用户购买礼品卡付款成功
    /// </summary>
    public class RequestMessageEvent_GiftCard_Pay_Done : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件：用户购买礼品卡付款成功
        /// </summary>
        public override Event Event
        {
            get { return Event.giftcard_pay_done; }
        }

        /// <summary>
        /// 货架的id
        /// </summary>
        public string PageId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
    }
}
