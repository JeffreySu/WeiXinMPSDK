#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
    
    文件名：RequestMessageEvent_UserAuthorizationCancellation.cs
    文件功能描述：事件之授权用户信息变更
    https://developers.weixin.qq.com/doc/offiaccount/OA_Web_Apps/authorization_change.html
    
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之授权用户信息变更
    /// </summary>
    public class RequestMessageEvent_UserAuthorizationCancellation : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.user_authorization_revoke; }
        }

        /// <summary>
        /// 授权用户OpenID
        /// </summary>
        public string OpenID { get; set; }

        /// <summary>
        /// 公众号的AppID
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// 用户撤回的H5授权信息，201:地址,202:发票信息,203:卡券信息,204:麦克风,205:昵称和头像,206:位置信息,207:选中的图片或视频
        /// </summary>
        public string RevokeInfo { get; set; }
    }
}
