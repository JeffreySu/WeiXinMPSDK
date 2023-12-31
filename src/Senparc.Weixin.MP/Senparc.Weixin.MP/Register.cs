/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin.MP 快捷注册流程


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180802
    修改描述：添加自动 根据 SenparcWeixinSetting 注册 RegisterMpAccount()、RegisterTenpayOld()、RegisterTenpayV3() 方法

    修改标识：Senparc - 20191003
    修改描述：注册过程自动添加更多 SenparcSettingItem 信息

    修改标识：Senparc - 20210809
    修改描述：v16.14.2 Register 提供对 ApiHandlerWapper 委托的设置方法

----------------------------------------------------------------*/
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP
{
    /// <summary>
    /// 公众号账号信息注册
    /// </summary>
    public static class Register
    {
        /// <summary>
        /// 注册公众号（或小程序）信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="appId">微信公众号后台的【开发】>【基本配置】中的“AppID(应用ID)”</param>
        /// <param name="appSecret">微信公众号后台的【开发】>【基本配置】中的“AppSecret(应用密钥)”</param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别</param>
        /// <returns></returns>
        public static IRegisterService RegisterMpAccount(this IRegisterService registerService, string appId, string appSecret, string name)
        {
            AccessTokenContainer.Register(appId, appSecret, name);
            return registerService;
        }

        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册公众号信息（包括JsApi）
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinSettingForMP">SenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 weixinSettingForMP.ItemKey </param>
        /// <returns></returns>
        public static IRegisterService RegisterMpAccount(this IRegisterService registerService, ISenparcWeixinSettingForMP weixinSettingForMP, string name = null)
        {
            //配置全局参数
            if (!string.IsNullOrWhiteSpace(name))
            {
                Config.SenparcWeixinSetting[name] = new SenparcWeixinSettingItem(weixinSettingForMP);
            }
            return RegisterMpAccount(registerService, weixinSettingForMP.WeixinAppId, weixinSettingForMP.WeixinAppSecret, name ?? weixinSettingForMP.ItemKey);
        }

        /// <summary>
        /// 注册公众号（或小程序）的JSApi（RegisterMpAccount注册过程中已包含）
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="appId">微信公众号后台的【开发】>【基本配置】中的“AppID(应用ID)”</param>
        /// <param name="appSecret">微信公众号后台的【开发】>【基本配置】中的“AppSecret(应用密钥)”</param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别</param>
        /// <returns></returns>
        public static IRegisterService RegisterMpJsApiTicket(this IRegisterService registerService, string appId, string appSecret, string name)
        {
            JsApiTicketContainer.Register(appId, appSecret, name);
            return registerService;
        }

        #region 设置 ApiHandlerWapper 处理方法

        /// <summary>
        /// 设置所有使用了 ApiHandlerWapper 的接口，可以自动进入重试的 API 错误代码
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="invalidCredentialReturnCodes">可进入重试的 API 错误代码</param>
        /// <returns></returns>
        public static IRegisterService SetMP_InvalidCredentialValues(this IRegisterService registerService, IEnumerable<ReturnCode> invalidCredentialReturnCodes)
        {
            ApiHandlerWapper.InvalidCredentialValues = invalidCredentialReturnCodes.Select(z => (int)z);
            return registerService;
        }

        #region AccessTokenContainer_GetFirstOrDefaultAppIdFunc

        /// <summary>
        /// 设置为 ApiHandlerWapper 服务的 AccessTokenContainer_GetFirstOrDefaultAppIdFunc 委托，默认为返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId(PlatformType.MP) 方法
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="func">自定义返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId(PlatformType.MP) 方法</param>
        /// <returns></returns>
        public static IRegisterService SetMP_AccessTokenContainer_GetFirstOrDefaultAppIdFunc(this IRegisterService registerService, Func<string> func)
        {
            ApiHandlerWapper.AccessTokenContainer_GetFirstOrDefaultAppIdFunc = func;
            return registerService;
        }

        /// <summary>
        /// 设置为 ApiHandlerWapper 服务的 AccessTokenContainer_GetFirstOrDefaultAppIdFunc 委托，默认为返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId(PlatformType.MP) 方法
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="func">自定义返回 AccessTokenContainer 中的 GetFirstOrDefaultAppId(PlatformType.MP) 方法</param>
        /// <returns></returns>
        public static IRegisterService SetMP_AccessTokenContainer_GetFirstOrDefaultAppIdFunc(this IRegisterService registerService, Func<Task<string>> func)
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
        public static IRegisterService SetMP_AccessTokenContainer_GetFirstOrDefaultAppIdFunc(this IRegisterService registerService, Func<string, bool> func)
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
        public static IRegisterService SetMP_AccessTokenContainer_GetFirstOrDefaultAppIdFunc(this IRegisterService registerService, Func<string, Task<bool>> func)
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
        public static IRegisterService SetMP_AccessTokenContainer_GetAccessTokenResultFunc(this IRegisterService registerService, Func<string, bool, IAccessTokenResult> func)
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
        public static IRegisterService SetMP_AccessTokenContainer_GetAccessTokenResultFunc(this IRegisterService registerService, Func<string, bool, Task<IAccessTokenResult>> func)
        {
            ApiHandlerWapper.AccessTokenContainer_GetAccessTokenResultAsyncFunc = func;
            return registerService;
        }

        #endregion

        #endregion

        #region 过期方法
        /*
        
        /// <summary>
        /// 注册微信支付Tenpay（注意：新注册账号请使用RegisterTenpayV3！
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="tenPayInfo">微信支付（旧版本）参数</param>
        /// <param name="name">公众号唯一标识名称</param>
        /// <returns></returns>
        [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
        public static IRegisterService RegisterTenpayOld(this IRegisterService registerService, Func<TenPayInfo> tenPayInfo, string name)
        {
            TenPayInfoCollection.Register(tenPayInfo(), name);
            return registerService;
        }

        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册微信支付Tenpay（注意：新注册账号请使用RegisterTenpayV3！
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinSettingForOldTepay">ISenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 weixinSettingForOldTepay.ItemKey </param>
        /// <returns></returns>
        [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
        public static IRegisterService RegisterTenpayOld(this IRegisterService registerService, ISenparcWeixinSettingForOldTenpay weixinSettingForOldTepay, string name)
        {
            Func<TenPayInfo> func = () => new TenPayInfo(weixinSettingForOldTepay);
            return RegisterTenpayOld(registerService, func, name ?? weixinSettingForOldTepay.ItemKey);
        }

        /// <summary>
        /// 注册微信支付TenpayV3
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="tenPayV3Info">微信支付（新版本 V3）参数</param>
        /// <param name="name">公众号唯一标识名称</param>
        /// <returns></returns>
        [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
        public static IRegisterService RegisterTenpayV3(this IRegisterService registerService, Func<TenPayV3Info> tenPayV3Info, string name)
        {
            TenPayV3InfoCollection.Register(tenPayV3Info(), name);
            return registerService;
        }

        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册微信支付Tenpay（注意：新注册账号请使用RegisterTenpayV3！
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinSettingForTenpayV3">ISenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 SenparcWeixinSetting.ItemKey </param>
        /// <returns></returns>
        [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
        public static IRegisterService RegisterTenpayV3(this IRegisterService registerService, ISenparcWeixinSettingForTenpayV3 weixinSettingForTenpayV3, string name)
        {
            Func<TenPayV3Info> func = () => new TenPayV3Info(weixinSettingForTenpayV3);
            return RegisterTenpayV3(registerService, func, name ?? weixinSettingForTenpayV3.ItemKey);
        }
        */

        #endregion

    }
}
