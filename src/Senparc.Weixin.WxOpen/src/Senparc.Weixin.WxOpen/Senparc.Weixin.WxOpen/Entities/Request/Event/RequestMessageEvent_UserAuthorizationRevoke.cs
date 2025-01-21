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
    
    文件名：RequestMessageEvent_UserAuthorizationRevoke.cs
    文件功能描述：事件之授权用户信息变更
    https://developers.weixin.qq.com/miniprogram/dev/framework/security.html#%E6%8E%88%E6%9D%83%E7%94%A8%E6%88%B7%E4%BF%A1%E6%81%AF%E5%8F%98%E6%9B%B4
        
----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 事件之授权用户信息变更
    /// </summary>
    public class RequestMessageEvent_UserAuthorizationRevoke : RequestMessageEventBase, IRequestMessageEventBase
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
        /// 小程序的AppID
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 用户撤回的授权信息，1:车牌号,2:地址,3:发票信息,4:蓝牙,5:麦克风,6:昵称和头像,7:摄像头,8:手机号,12:微信运动步数,13:位置信息,14:选中的图片或视频,15:选中的文件,16:邮箱地址,18:选择的位置信息,19:昵称输入键盘中选择的微信昵称,20:获取用户头像组件中选择的微信头像
        /// </summary>
        public string RevokeInfo { get; set; }

        /// <summary>
        /// 插件场景用户撤回，插件的AppID
        /// </summary>
        public string PluginID { get; set; }

        /// <summary>
        /// 插件场景用户撤回，撤回用户的OpenPID
        /// </summary>
        public string OpenPID { get; set; }
    }
}
