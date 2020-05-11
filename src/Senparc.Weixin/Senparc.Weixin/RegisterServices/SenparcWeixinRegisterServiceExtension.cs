#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2020 Senparc

    文件名：RegisterServiceExtension.cs
    文件功能描述：快捷注册类，RegisterService 扩展类


    创建标识：Senparc - 20180704

    修改标识：Senparc - 20191007
    修改描述：AddSenparcWeixinServices() 方法自动包含 AddSenparcGlobalServices() 注册过程

----------------------------------------------------------------*/

#if NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP3_1
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Senparc.CO2NET;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.HttpUtility;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
#endif

namespace Senparc.Weixin.RegisterServices
{
    /// <summary>
    /// 快捷注册类，RegisterService 扩展类
    /// </summary>
    public static class RegisterServiceExtension
    {
#if !NET45
        /// <summary>
        /// 注册 IServiceCollection，并返回 RegisterService，开始注册流程
        /// </summary>
        /// <param name="serviceCollection">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns></returns>
        public static IServiceCollection AddSenparcWeixinServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<SenparcWeixinSetting>(configuration.GetSection("SenparcWeixinSetting"));

            var services = serviceCollection;

            //全局注册 CO2NET
            if (!CO2NET.RegisterServices.RegisterServiceExtension.SenparcGlobalServicesRegistered)
            {
                services = services.AddSenparcGlobalServices(configuration);//自动注册 SenparcGlobalServices
            }

            //注册 HttpClient
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                //var serviceProvider = serviceCollection.BuildServiceProvider();
                var tenPayV3Setting = scope.ServiceProvider.GetService<IOptions<SenparcWeixinSetting>>().Value.TenpayV3Setting;

                var key = TenPayHelper.GetRegisterKey(tenPayV3Setting);

                services.AddCertHttpClient(key, tenPayV3Setting.TenPayV3_CertSecret, tenPayV3Setting.TenPayV3_CertPath);
            }

            return services;

            #region appsettings.json 中添加节点
            /*
             * appsettings.json 中添加节点：
  //Senparc.Weixin SDK 设置
    //Senparc.Weixin SDK 设置
  "SenparcWeixinSetting": {
    //微信全局
    "IsDebug": true,
    //公众号
    "Token": "weixin",
    "EncodingAESKey": "",
    "WeixinAppId": "WeixinAppId",
    "WeixinAppSecret": "WeixinAppSecret",
    //小程序
    "WxOpenAppId": "WxOpenAppId",
    "WxOpenAppSecret": "WxOpenAppSecret",
    //企业微信
    "WeixinCorpId": "WeixinCorpId",
    "WeixinCorpSecret": "WeixinCorpSecret",

    //微信支付
    //微信支付V2（旧版）
    "WeixinPay_PartnerId": "WeixinPay_PartnerId",
    "WeixinPay_Key": "WeixinPay_Key",
    "WeixinPay_AppId": "WeixinPay_AppId",
    "WeixinPay_AppKey": "WeixinPay_AppKey",
    "WeixinPay_TenpayNotify": "WeixinPay_TenpayNotify",
    //微信支付V3（新版）
    "TenPayV3_MchId": "TenPayV3_MchId",
    "TenPayV3_Key": "TenPayV3_Key",
    "TenPayV3_AppId": "TenPayV3_AppId",
    "TenPayV3_AppSecret": "TenPayV3_AppId",
    "TenPayV3_TenpayNotify": "TenPayV3_TenpayNotify",

    //开放平台
    "Component_Appid": "Component_Appid",
    "Component_Secret": "Component_Secret",
    "Component_Token": "Component_Token",
    "Component_EncodingAESKey": "Component_EncodingAESKey",

    //分布式缓存
    //"Cache_Redis_Configuration": "Redis配置", 
    "Cache_Redis_Configuration": "localhost:6379", 
    "Cache_Memcached_Configuration": "Memcached配置"
  }
  */
            #endregion
        }

        /// <summary>
        /// 注册 HttpClient 请求证书
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddCertHttpClient(this IServiceCollection services, string certName,string certPassword,string certPath)
        {
            try
            {
                #region 添加证书

                //添加注册

                if (!string.IsNullOrEmpty(certPath))
                {
                    if (File.Exists(certPath))
                    {
                        try
                        {
                            var cert = new X509Certificate2(certPath, certPassword, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                            var checkValidationResult = false;
                            //serviceCollection.AddHttpClient<SenparcHttpClient>(certName)
                            services.AddHttpClient(certName)
                                    .ConfigurePrimaryHttpMessageHandler(() =>
                                    {
                                        var httpClientHandler = HttpClientHelper.GetHttpClientHandler(null, RequestUtility.SenparcHttpClientWebProxy, System.Net.DecompressionMethods.None);

                                        httpClientHandler.ClientCertificates.Add(cert);

                                        if (checkValidationResult)
                                        {
                                            httpClientHandler.ServerCertificateCustomValidationCallback = new Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>(RequestUtility.CheckValidationResult);
                                        }

                                        return httpClientHandler;
                                    });
                            Senparc.CO2NET.Trace.SenparcTrace.SendCustomLog($"成功添加 cert 证书", $"certName:{certName},certPath:{certPath}");
                        }
                        catch (Exception ex)
                        {
                            Senparc.CO2NET.Trace.SenparcTrace.SendCustomLog($"添加微信支付证书发生异常", $"certName:{certName},certPath:{certPath}");
                            Senparc.CO2NET.Trace.SenparcTrace.BaseExceptionLog(ex);
                        }
                    }
                    else
                    {
                        Senparc.CO2NET.Trace.SenparcTrace.SendCustomLog($"已设置微信支付证书，但无法找到文件", $"certName:{certName},certPath:{certPath}");
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                //SenparcDI.ResetGlobalIServiceProvider(SenparcDI.GlobalServiceCollection);
            }

            return services;
        }
#endif
    }
}
