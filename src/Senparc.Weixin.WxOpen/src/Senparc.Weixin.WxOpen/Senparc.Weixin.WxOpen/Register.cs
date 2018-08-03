#region Apache License Version 2.0
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
  
    文件名：Register.cs
    文件功能描述：注册小程序信息
    
    
    创建标识：Senparc - 20170302

    修改标识：Senparc - 20180802
    修改描述：添加自动 根据 SenparcWeixinSetting 注册 RegisterWxOpenAccount() 方法

----------------------------------------------------------------*/

using Senparc.Weixin.Exceptions;
using Senparc.CO2NET.RegisterServices;
using System;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen
{
    /// <summary>
    /// 注册小程序
    /// </summary>
    public static class Register
    {
        /// <summary>
        /// 注册小程序信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="appId">微信公众号后台的【开发】>【基本配置】中的“AppID(应用ID)”</param>
        /// <param name="appSecret">微信公众号后台的【开发】>【基本配置】中的“AppSecret(应用密钥)”</param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别</param>
        /// <returns></returns>
        [Obsolete("请统一使用Senparc.Weixin.MP.Register.RegisterMpAccount()方法进行注册！")]
        public static IRegisterService RegisterWxOpenAccount(this IRegisterService registerService, string appId, string appSercet, string name = null)
        {
            throw new WeixinException("请统一使用Senparc.Weixin.MP.Register.RegisterMpAccount()方法进行注册！");
        }

        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册小程序信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinSettingForWxOpen">SenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 SenparcWeixinSetting.ItemKey </param>
        /// <returns></returns>
        public static IRegisterService RegisterWxOpenAccount(this IRegisterService registerService, ISenparcWeixinSettingForWxOpen weixinSettingForWxOpen, string name = null)
        {
            AccessTokenContainer.Register(weixinSettingForWxOpen.WxOpenAppId, weixinSettingForWxOpen.WxOpenAppSecret, name ?? weixinSettingForWxOpen.ItemKey);
            return registerService;
        }

    }
}
