﻿/*----------------------------------------------------------------
    Copyright (C) 2022 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin.Work 快捷注册流程


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180802
    修改描述：添加自动 根据 SenparcWeixinSetting 注册 RegisterWorkAccount() 方法

    修改标识：Senparc - 20191003
    修改描述：注册过程自动添加更多 SenparcSettingItem 信息

----------------------------------------------------------------*/

using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work
{
    /// <summary>
    /// 注册企业微信
    /// </summary>
    public static class Register
    {
        /// <summary>
        /// 注册企业微信
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinCorpId">weixinCorpId</param>
        /// <param name="weixinCorpSecret">weixinCorpSecret</param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别</param>
        /// <param name="getSuiteTicketFunc">根据服务商应用获取服务商ticket</param>
        /// <param name="getSuiteByCorpPermanentCodeFunc">根据企业永久授权码获取关联的服务商应用和秘钥</param>
        /// <returns></returns>
        public static IRegisterService RegisterWorkAccount(this IRegisterService registerService, string weixinCorpId, string weixinCorpSecret, string name = null, 
            Func<string, Task<string>> getSuiteTicketFunc = null,
            Func<string, Task<FuncGetIdSecretResult>> getSuiteByCorpPermanentCodeFunc = null)
        {

            AccessTokenContainer.Register(weixinCorpId, weixinCorpSecret, name);
            if (getSuiteTicketFunc != null || getSuiteByCorpPermanentCodeFunc != null)
                ProviderTokenContainer.Register(weixinCorpId, weixinCorpSecret, getSuiteTicketFunc, getSuiteByCorpPermanentCodeFunc, name);
            return registerService;
        }

        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册第三方平台信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinSettingForWork">SenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 SenparcWeixinSetting.ItemKey </param>
        /// <param name="getSuiteTicketFunc">根据服务商应用获取服务商ticket</param>
        /// <param name="getSuiteByCorpPermanentCodeFunc">根据企业永久授权码获取关联的服务商应用和秘钥</param>
        /// <returns></returns>
        public static IRegisterService RegisterWorkAccount(this IRegisterService registerService, Weixin.Entities.ISenparcWeixinSettingForWork weixinSettingForWork,string name = null,
            Func<string, Task<string>> getSuiteTicketFunc = null,
            Func<string, Task<FuncGetIdSecretResult>> getSuiteByCorpPermanentCodeFunc = null)
        {
            //配置全局参数
            if (!string.IsNullOrWhiteSpace(name))
            {
                Config.SenparcWeixinSetting[name] = new SenparcWeixinSettingItem(weixinSettingForWork);
            }
            return RegisterWorkAccount(registerService, weixinSettingForWork.WeixinCorpId, weixinSettingForWork.WeixinCorpSecret, name ?? weixinSettingForWork.ItemKey, getSuiteTicketFunc, getSuiteByCorpPermanentCodeFunc);
        }
        #region 设置 ApiHandlerWapper 处理方法

        /// <summary>
        /// 设置所有使用了 ApiHandlerWapper 的接口，可以自动进入重试的 API 错误代码
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="invalidCredentialReturnCodes">可进入重试的 API 错误代码</param>
        /// <returns></returns>
        public static IRegisterService SetWork_InvalidCredentialValues(this IRegisterService registerService, IEnumerable<ReturnCode_Work> invalidCredentialReturnCodes)
        {
            ApiHandlerWapper.InvalidCredentialValues = invalidCredentialReturnCodes.Select(z => (int)z);
            return registerService;
        }

        #region AccessTokenContainer_GetFirstOrDefaultAppIdFunc

        /// <summary>
        /// 设置为 ApiHandlerWapper 服务的 AccessTokenContainer_GetFirstOrDefaultAppIdFunc 委托，默认为返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId(PlatformType.Work) 方法
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="func">自定义返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId(PlatformType.Work) 方法</param>
        /// <returns></returns>
        public static IRegisterService SetWork_AccessTokenContainer_GetFirstOrDefaultAppIdFunc(this IRegisterService registerService, Func<string> func)
        {
            ApiHandlerWapper.AccessTokenContainer_GetFirstOrDefaultAppIdFunc = func;
            return registerService;
        }

        /// <summary>
        /// 设置为 ApiHandlerWapper 服务的 AccessTokenContainer_GetFirstOrDefaultAppIdFunc 委托，默认为返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId(PlatformType.Work) 方法
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="func">自定义返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId(PlatformType.Work) 方法</param>
        /// <returns></returns>
        public static IRegisterService SetWork_AccessTokenContainer_GetFirstOrDefaultAppIdFunc(this IRegisterService registerService, Func<Task<string>> func)
        {
            ApiHandlerWapper.AccessTokenContainer_GetFirstOrDefaultAppIdAsyncFunc = func;
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
        public static IRegisterService SetWork_AccessTokenContainer_GetFirstOrDefaultAppIdFunc(this IRegisterService registerService, Func<string, bool> func)
        {
            ApiHandlerWapper.AccessTokenContainer_CheckRegisteredFunc = func;
            return registerService;
        }

        /// <summary>
        /// 设置为 ApiHandlerWapper 服务的 AccessTokenContainer_GetFirstOrDefaultAppIdFunc 委托，默认为返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId() 方法
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="func">自定义返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId() 方法</param>
        /// <returns></returns>
        public static IRegisterService SetWork_AccessTokenContainer_GetFirstOrDefaultAppIdFunc(this IRegisterService registerService, Func<string, Task<bool>> func)
        {
            ApiHandlerWapper.AccessTokenContainer_CheckRegisteredAsyncFunc = func;
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
        public static IRegisterService SetWork_AccessTokenContainer_GetAccessTokenResultFunc(this IRegisterService registerService, Func<string, bool, IAccessTokenResult> func)
        {
            ApiHandlerWapper.AccessTokenContainer_GetAccessTokenResultFunc = func;
            return registerService;
        }

        /// <summary>
        /// 设置为 ApiHandlerWapper 服务的 AccessTokenContainer_GetAccessTokenResultFunc 委托，默认为返回 AccessTokenContainer 中的 GetAccessTokenResult() 方法
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="func">自定义返回 AccessTokenContainer 中的 AccessTokenResult GetAccessTokenResult(appId) 方法</param>
        /// <returns></returns>
        public static IRegisterService SetWork_AccessTokenContainer_GetAccessTokenResultFunc(this IRegisterService registerService, Func<string, bool, Task<IAccessTokenResult>> func)
        {
            ApiHandlerWapper.AccessTokenContainer_GetAccessTokenResultAsyncFunc = func;
            return registerService;
        }

        #endregion

        #endregion
    }
}
