﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2020 Senparc
    
    文件名：RequestMessageEvent_Card_Pay_Order.cs
    文件功能描述：券点流水详情事件：当商户朋友的券券点发生变动时
    
    
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_Card_Pay_Order : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 进入会员卡
        /// </summary>
        public override Event Event
        {
            get { return Event.card_pay_order; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 商户自定义code值。非自定code推送为空串。
        /// </summary>
        public string UserCardCode { get; set; }
        /// <summary>
        /// 商户自定义二维码渠道参数，用于标识本次扫码打开会员卡来源来自于某个渠道值的二维码
        /// </summary>
        public string OuterStr { get; set; }
    }
}
