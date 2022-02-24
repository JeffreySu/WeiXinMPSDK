using Client.TenPayHttpClient.Signer;
using Microsoft.Extensions.DependencyInjection;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.TenPayHttpClient.Verifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    public static class Register
    {

        /// <summary>
        /// 注册微信支付TenpayV3
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="tenPayV3Info">微信支付（新版本 V3）参数</param>
        /// <param name="name">公众号唯一标识名称</param>
        /// <returns></returns>
        public static IRegisterService RegisterTenpayApiV3(this IRegisterService registerService, Func<TenPayV3Info> tenPayV3Info, string name)
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
        public static IRegisterService RegisterTenpayApiV3(this IRegisterService registerService, ISenparcWeixinSettingForTenpayV3 weixinSettingForTenpayV3, string name)
        {
            //配置全局参数
            if (!string.IsNullOrWhiteSpace(name))
            {
                Config.SenparcWeixinSetting[name] = new SenparcWeixinSettingItem(weixinSettingForTenpayV3);
            }

            Func<TenPayV3Info> func = () => new TenPayV3Info(weixinSettingForTenpayV3);


            return RegisterTenpayApiV3(registerService, func, name ?? weixinSettingForTenpayV3.ItemKey);
        }

        public static IServiceCollection AddTenpayApiV3Services(this IServiceCollection serviceCollection)
        {
            var services = serviceCollection;

            // 配置IHttpClientFactory
            services.AddHttpClient();
            // 配置加密算法
            services.AddSingleton<ISigner, SHA256WithRSASigner>();
            services.AddSingleton<IVerifier, SHA256WithRSAVerifier>();

            services.AddSingleton<TenPayHttpClient.TenPayHttpClient>();

            return services;
        }
    }
}
