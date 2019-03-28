/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin.MP 快捷注册流程


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180802
    修改描述：添加自动 根据 SenparcWeixinSetting 注册 RegisterMpAccount()、RegisterTenpayOld()、RegisterTenpayV3() 方法

----------------------------------------------------------------*/
using Senparc.Weixin.MP.Containers;
using Senparc.CO2NET.RegisterServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

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
