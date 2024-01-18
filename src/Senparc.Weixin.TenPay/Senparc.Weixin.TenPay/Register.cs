/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin.TenPay 快捷注册流程


    创建标识：Senparc - 20180826

    修改标识：Senparc - 20191003
    修改描述：注册过程自动添加更多 SenparcSettingItem 信息

    修改标识：15989221023 - 20200416
    修改描述：v1.5.402 添加 Version 参数 https://github.com/JeffreySu/WeiXinMPSDK/pull/2151

    修改标识：Senparc - 20201210
    修改描述：v1.6.101 删除 TenpayV3ProtfitRequestDataVersion 的定义，并非全局都需要用到

----------------------------------------------------------------*/

using Senparc.CO2NET.RegisterServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.TenPay.V3;
using Senparc.Weixin.TenPay.V2;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.TenPay
{
    public static class Register
    {
        /*
        /// <summary>
        /// 接口版本号 version 是 String(32) 1.0 新增字段，接口版本号，区分原接口，默认填写1.0。入参新增version后，则支付通知接口也将返回单品优惠信息字段promotion_detail，请确保支付通知的签名验证能通过。
        /// </summary>
        public static string TenpayV3ProtfitRequestDataVersion { get; set; } = "1.0";
        */

        /// <summary>
        /// 注册微信支付Tenpay（注意：新注册账号请使用RegisterTenpayV3！
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="tenPayInfo">微信支付（旧版本）参数</param>
        /// <param name="name">公众号唯一标识名称</param>
        /// <returns></returns>
        public static IRegisterService RegisterTenpayOld(this IRegisterService registerService, Func<TenPayInfo> tenPayInfo, string name)
        {
            TenPayInfoCollection.Register(tenPayInfo(), name);
            return registerService;
        }


        /// <summary>
        /// 根据 SenparcWeixinSetting 自动注册微信支付Tenpay（注意：新注册账号请使用RegisterTenpayV3！）
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="weixinSettingForOldTepay">ISenparcWeixinSetting</param>
        /// <param name="name">统一标识，如果为null，则使用 weixinSettingForOldTepay.ItemKey </param>
        /// <returns></returns>
        public static IRegisterService RegisterTenpayOld(this IRegisterService registerService, ISenparcWeixinSettingForOldTenpay weixinSettingForOldTepay, string name)
        {
            //配置全局参数
            if (!string.IsNullOrWhiteSpace(name))
            {
                Config.SenparcWeixinSetting[name] = new SenparcWeixinSettingItem(weixinSettingForOldTepay);
            }

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
        public static IRegisterService RegisterTenpayV3(this IRegisterService registerService, ISenparcWeixinSettingForTenpayV3 weixinSettingForTenpayV3, string name)
        {
            //配置全局参数
            if (!string.IsNullOrWhiteSpace(name))
            {
                Config.SenparcWeixinSetting[name] = new SenparcWeixinSettingItem(weixinSettingForTenpayV3);
            }

            Func<TenPayV3Info> func = () => new TenPayV3Info(weixinSettingForTenpayV3);
            return RegisterTenpayV3(registerService, func, name ?? weixinSettingForTenpayV3.ItemKey);
        }

    }
}
