#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2022 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2022 Senparc
  
    文件名：Register.cs
    文件功能描述：注册小程序信息
    
    
    创建标识：Senparc - 20170302

    修改标识：Senparc - 20180802
    修改描述：添加自动 根据 SenparcWeixinSetting 注册 RegisterWxOpenAccount() 方法
    
    修改标识：Senparc - 20191003
    修改描述：注册过程自动添加更多 SenparcSettingItem 信息
    
    修改标识：gokeiyou - 20201230
    修改描述：新建 WxOpen 专属的 AccessTokenContainer

----------------------------------------------------------------*/

using Senparc.Weixin.Exceptions;
using Senparc.CO2NET.RegisterServices;
using System;
using Senparc.Weixin.Entities;
using Senparc.Weixin.WxOpen.Containers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        [Obsolete("请统一使用Senparc.Weixin.WxOpen.Register.RegisterWxOpenAccount()方法进行注册！")]
        public static IRegisterService RegisterWxOpenAccount(this IRegisterService registerService, string appId, string appSercet, string name = null)
        {
            throw new WeixinException("请统一使用请统一使用Senparc.Weixin.WxOpen.Register.RegisterWxOpenAccount()方法进行注册！");
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
            //配置全局参数
            if (!string.IsNullOrWhiteSpace(name))
            {
                Config.SenparcWeixinSetting[name] = new SenparcWeixinSettingItem(weixinSettingForWxOpen);
            }
            AccessTokenContainer.Register(weixinSettingForWxOpen.WxOpenAppId, weixinSettingForWxOpen.WxOpenAppSecret, name ?? weixinSettingForWxOpen.ItemKey);
            return registerService;
        }

        #region 设置 ApiHandlerWapper 处理方法

        /// <summary>
        /// 设置所有使用了 ApiHandlerWapper 的接口，可以自动进入重试的 API 错误代码
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="invalidCredentialReturnCodes">可进入重试的 API 错误代码</param>
        /// <returns></returns>
        public static IRegisterService SetWxOpen_InvalidCredentialValues(this IRegisterService registerService, IEnumerable<ReturnCode> invalidCredentialReturnCodes)
        {
            WxOpenApiHandlerWapper.InvalidCredentialValues = invalidCredentialReturnCodes.Select(z => (int)z);
            return registerService;
        }

        #region AccessTokenContainer_GetFirstOrDefaultAppIdFunc

        /// <summary>
        /// 设置为 ApiHandlerWapper 服务的 AccessTokenContainer_GetFirstOrDefaultAppIdFunc 委托，默认为返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId(PlatformType.MP) 方法
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="func">自定义返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId(PlatformType.MP) 方法</param>
        /// <returns></returns>
        public static IRegisterService SetWxOpen_AccessTokenContainer_GetFirstOrDefaultAppIdFunc(this IRegisterService registerService, Func<string> func)
        {
            WxOpenApiHandlerWapper.AccessTokenContainer_GetFirstOrDefaultAppIdFunc = func;
            return registerService;
        }

        /// <summary>
        /// 设置为 ApiHandlerWapper 服务的 AccessTokenContainer_GetFirstOrDefaultAppIdFunc 委托，默认为返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId(PlatformType.WxOpen) 方法
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="func">自定义返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId(PlatformType.MP) 方法</param>
        /// <returns></returns>
        public static IRegisterService SetWxOpen_AccessTokenContainer_GetFirstOrDefaultAppIdFunc(this IRegisterService registerService, Func<Task<string>> func)
        {
            WxOpenApiHandlerWapper.AccessTokenContainer_GetFirstOrDefaultAppIdAsyncFunc = func;
            return registerService;
        }

        #endregion

        #region AccessTokenContainer_GetFirstOrDefaultAppIdFunc

        /// <summary>
        /// 设置为 ApiHandlerWapper 服务的 AccessTokenContainer_GetFirstOrDefaultAppIdFunc 委托，默认为返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId() 方法
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="func">自定义返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId() 方法</param>
        /// <returns></returns>
        public static IRegisterService SetWxOpen_AccessTokenContainer_GetFirstOrDefaultAppIdFunc(this IRegisterService registerService, Func<string, bool> func)
        {
            WxOpenApiHandlerWapper.AccessTokenContainer_CheckRegisteredFunc = func;
            return registerService;
        }

        /// <summary>
        /// 设置为 ApiHandlerWapper 服务的 AccessTokenContainer_GetFirstOrDefaultAppIdFunc 委托，默认为返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId() 方法
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="func">自定义返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId() 方法</param>
        /// <returns></returns>
        public static IRegisterService SetWxOpen_AccessTokenContainer_GetFirstOrDefaultAppIdFunc(this IRegisterService registerService, Func<string, Task<bool>> func)
        {
            WxOpenApiHandlerWapper.AccessTokenContainer_CheckRegisteredAsyncFunc = func;
            return registerService;
        }

        #endregion

        #region AccessTokenContainer_GetAccessTokenResultFunc

        /// <summary>
        /// 设置为 ApiHandlerWapper 服务的 AccessTokenContainer_GetAccessTokenResultFunc 委托，默认为返回 AccessTokenContainer 中的 GetAccessTokenResult() 方法
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="func">自定义返回 AccessTokenContainer 中的 AccessTokenResult GetAccessTokenResult(appId) 方法</param>
        /// <returns></returns>
        public static IRegisterService SetAccessTokenContainer_GetAccessTokenResultFunc(this IRegisterService registerService, Func<string, bool, IAccessTokenResult> func)
        {
            WxOpenApiHandlerWapper.AccessTokenContainer_GetAccessTokenResultFunc = func;
            return registerService;
        }

        /// <summary>
        /// 设置为 ApiHandlerWapper 服务的 AccessTokenContainer_GetAccessTokenResultFunc 委托，默认为返回 AccessTokenContainer 中的 GetAccessTokenResult() 方法
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="func">自定义返回 AccessTokenContainer 中的 AccessTokenResult GetAccessTokenResult(appId) 方法</param>
        /// <returns></returns>
        public static IRegisterService SetAccessTokenContainer_GetAccessTokenResultFunc(this IRegisterService registerService, Func<string, bool, Task<IAccessTokenResult>> func)
        {
            WxOpenApiHandlerWapper.AccessTokenContainer_GetAccessTokenResultAsyncFunc = func;
            return registerService;
        }

        #endregion

        #endregion
    }
}
