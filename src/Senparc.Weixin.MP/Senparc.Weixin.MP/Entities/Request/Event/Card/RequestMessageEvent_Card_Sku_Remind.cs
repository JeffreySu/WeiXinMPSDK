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
    
    文件名：RequestMessageEvent_Card_Sku_Remind.cs
    文件功能描述：卡券库存报警事件：当某个card_id的初始库存数大于200且当前库存小于等于100时
    
    
    创建标识：
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_Card_Sku_Remind : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 进入会员卡
        /// </summary>
        public override Event Event
        {
            get { return Event.card_sku_remind; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 报警详细信息
        /// </summary>
        public string Detail { get; set; }

    }
}
