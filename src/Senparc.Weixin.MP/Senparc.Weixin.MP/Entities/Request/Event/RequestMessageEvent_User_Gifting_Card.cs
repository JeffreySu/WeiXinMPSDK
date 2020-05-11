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
    
    文件名：RequestMessageEvent_User_Gifting_Card.cs
    文件功能描述：卡券转赠事件推送
    
    
    创建标识：

----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_User_Gifting_Card : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 卡券未通过审核
        /// </summary>
        public override Event Event
        {
            get { return Event.user_gifting_card; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        ///接收卡券用户的openid 
        /// </summary>
        public string FriendUserName { get; set; }
        /// <summary>
        /// code序列号。
        /// </summary>
        public string UserCardCode { get; set; }
        /// <summary>
        /// 是否转赠退回，0代表不是，1代表是。
        /// </summary>
        public string IsReturnBack { get; set; }
        /// <summary>
        /// 是否是群转赠
        /// </summary>
        public string IsChatRoom { get; set; }
    }
}
