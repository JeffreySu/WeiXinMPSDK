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
    
    文件名：RequestMessageEvent_Update_Member_Card.cs
    文件功能描述：事件之会员卡内容更新事件：会员卡积分余额发生变动时
    
    
    创建标识：
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_Update_Member_Card : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 卡券未通过审核
        /// </summary>
        public override Event Event
        {
            get { return Event.update_member_card; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// code序列号。
        /// </summary>
        public string UserCardCode { get; set; }
        /// <summary>
        /// 变动的积分值。
        /// </summary>
        public int ModifyBonus { get; set; }
        /// <summary>
        /// 变动的余额值。
        /// </summary>
        public int ModifyBalance { get; set; }
    }
}
