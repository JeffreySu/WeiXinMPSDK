﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：RequestMessageEvent_GiftCard_User_Accept.cs
    文件功能描述：用户领取礼品卡成功
    
    
    创建标识：Senparc - 20180906

----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件：用户领取礼品卡成功
    /// </summary>
    public class RequestMessageEvent_GiftCard_User_Accept : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件
        /// </summary>
        public override Event Event
        {
            get { return Event.giftcard_user_accept; }
        }

        /// <summary>
        /// 货架的id
        /// </summary>
        public string PageId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 礼品卡是否发送至群，true为是
        /// </summary>
        public string IsChatRoom { get; set; }
    }
}
