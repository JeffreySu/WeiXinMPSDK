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
    
    文件名：RequestMessageEvent_User_Authorize_Invoice.cs
    文件功能描述：事件之接收授权完成事件：用户授权完成后，执收单位的公众号会收到授权完成的事件，关于事件推送请参考接受callback推送
    
    
    创建标识：
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_User_Authorize_Invoice: RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.user_authorize_invoice; }
        }

        public string EventKey { get; set; }
        /// <summary>
        /// 授权成功的订单号
        /// </summary>
        public string SuccOrderId { get; set; }
        /// <summary>
        /// 授权失败的订单号
        /// </summary>
        public string FailOrderId { get; set; }
        /// <summary>
        /// 用于接收事件推送的公众号的AppId
        /// </summary>
        public string AuthorizeAppId { get; set; }
        /// <summary>
        /// 授权来源，web表示来自微信内H5
        /// </summary>
        public string Source { get; set; }
    }
}