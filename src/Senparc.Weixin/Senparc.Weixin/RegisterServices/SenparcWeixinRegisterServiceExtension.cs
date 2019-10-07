#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc

    文件名：RegisterServiceExtension.cs
    文件功能描述：快捷注册类，RegisterService 扩展类


    创建标识：Senparc - 20180704

    修改标识：Senparc - 20191007
    修改描述：AddSenparcWeixinServices() 方法自动包含 AddSenparcGlobalServices() 注册过程

----------------------------------------------------------------*/

#if NETSTANDARD2_0 || NETCOREAPP3_0
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;
#endif

namespace Senparc.Weixin.RegisterServices
{
    /// <summary>
    /// 快捷注册类，RegisterService 扩展类
    /// </summary>
    public static class RegisterServiceExtension
    {
#if NETSTANDARD2_0 || NETCOREAPP3_0
        /// <summary>
        /// 注册 IServiceCollection，并返回 RegisterService，开始注册流程
        /// </summary>
        /// <param name="serviceCollection">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns></returns>
        public static IServiceCollection AddSenparcWeixinServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            if (!CO2NET.RegisterServices.RegisterServiceExtension.SenparcGlobalServicesRegistered)
            {
                serviceCollection.AddSenparcGlobalServices(configuration);//自动注册 SenparcGlobalServices
            }

            serviceCollection.Configure<SenparcWeixinSetting>(configuration.GetSection("SenparcWeixinSetting"));

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

            return serviceCollection;
        }
#endif
    }
}
