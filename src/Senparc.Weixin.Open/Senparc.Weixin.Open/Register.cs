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
    
    
    创建标识：Senparc - 20180302

    修改标识：Senparc - 20180802
    修改描述：添加自动注册 RegisterOpenComponent() 方法

    修改标识：Senparc - 20180802
    修改描述：添加自动 根据 SenparcWeixinSetting 注册 RegisterOpenComponent() 方法

----------------------------------------------------------------*/


using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.Open.Containers;
using Senparc.CO2NET.RegisterServices;
using System;

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// 注册第三方平台
    /// </summary>
    public static class Register
    {
        /// <summary>
        /// 注册第三方平台信息
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentAppSecret"></param>
        /// <param name="getComponentVerifyTicketFunc">获取ComponentVerifyTicket的方法</param>
        /// <param name="getAuthorizerRefreshTokenFunc">从数据库中获取已存的AuthorizerAccessToken的方法</param>
        /// <param name="authorizerTokenRefreshedFunc">AuthorizerAccessToken更新后的回调</param>
        /// <param name="name">标记名称（如开放平台名称），帮助管理员识别</param>
        /// <returns></returns>
        public static IRegisterService RegisterOpenComponent(this IRegisterService registerService,
            string componentAppId, string componentAppSecret,
            Func<string, string> getComponentVerifyTicketFunc,
            Func<string, string, string> getAuthorizerRefreshTokenFunc,
            Action<string, string, RefreshAuthorizerTokenResult> authorizerTokenRefreshedFunc,
            string name = null)
        {
            ComponentContainer.Register(
                            componentAppId, componentAppSecret,
                            getComponentVerifyTicketFunc,
                            getAuthorizerRefreshTokenFunc,
                            authorizerTokenRefreshedFunc,
                            name);
            return registerService;
        }

        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册第三方平台信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="ISenparcWeixinSettingForOpen">SenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 SenparcWeixinSetting.ItemKey </param>
        /// <returns></returns>
        public static IRegisterService RegisterOpenComponent(this IRegisterService registerService, Weixin.Entities.ISenparcWeixinSettingForOpen weixinSettingForOpen, Func<string, string> getComponentVerifyTicketFunc,
            Func<string, string, string> getAuthorizerRefreshTokenFunc,
            Action<string, string, RefreshAuthorizerTokenResult> authorizerTokenRefreshedFunc,
            string name = null)
        {
            return RegisterOpenComponent(registerService, weixinSettingForOpen.Component_Appid, weixinSettingForOpen.Component_Secret,
                          getComponentVerifyTicketFunc,
                          getAuthorizerRefreshTokenFunc,
                          authorizerTokenRefreshedFunc,
                          name ?? weixinSettingForOpen.ItemKey);
        }

    }
}
