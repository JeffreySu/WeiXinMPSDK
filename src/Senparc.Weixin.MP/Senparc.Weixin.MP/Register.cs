/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin.MP 快捷注册流程


    创建标识：Senparc - 20180222

----------------------------------------------------------------*/
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.TenPayLib;
using Senparc.Weixin.MP.TenPayLibV3;
using Senparc.Weixin.RegisterServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP
{
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
        public static IRegisterService RegisterMpAccount(this IRegisterService registerService, string appId, string appSercet, string name = null)
        {
            AccessTokenContainer.Register(appId, appSercet, name);
            return registerService;
        }

        /// <summary>
        /// 注册公众号（或小程序）的JSApi（RegisterMpAccount注册过程中已包含）
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="appId">微信公众号后台的【开发】>【基本配置】中的“AppID(应用ID)”</param>
        /// <param name="appSecret">微信公众号后台的【开发】>【基本配置】中的“AppSecret(应用密钥)”</param>
        /// <param name="name">标记AccessToken名称（如微信公众号名称），帮助管理员识别</param>
        /// <returns></returns>
        public static IRegisterService RegisterMpJsApiTicket(this IRegisterService registerService, string appId, string appSercet, string name = null)
        {
            JsApiTicketContainer.Register(appId, appSercet, name);
            return registerService;
        }


        /// <summary>
        /// 注册微信支付Tenpay（注意：新注册账号请使用RegisterTenpayV3！
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <returns></returns>
        public static IRegisterService RegisterTenpayOld(this IRegisterService registerService, Func<TenPayInfo> tenPayInfo)
        {
            TenPayInfoCollection.Register(tenPayInfo());
            return registerService;
        }

        /// <summary>
        /// 注册微信支付TenpayV3
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <returns></returns>
        public static IRegisterService RegisterTenpayV3(this IRegisterService registerService, Func<TenPayV3Info> tenPayV3Info)
        {
            TenPayV3InfoCollection.Register(tenPayV3Info());
            return registerService;
        }
    }
}
