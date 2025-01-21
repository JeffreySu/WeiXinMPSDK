#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
  
    文件名：WeixinEntensions.cs
    文件功能描述：微信完整包的扩展方法
    
    
    创建标识：Senparc - 20240625

    修改标识：Senparc - 20240630
    修改描述：v2024.6.30 完善 UseSenparcWeixin() 方法

----------------------------------------------------------------*/

using Microsoft.AspNetCore.Builder;
using Senparc.CO2NET;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin.TenPay;
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.Work;
using Senparc.Weixin.WxOpen;


namespace Senparc.Weixin.All
{
    public static class WeixinEntensions
    {
        /// <summary>
        /// 开始 Senparc.Weixin SDK 初始化参数流程
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="senparcWeixinSetting">微信全局设置参数，必填</param>
        /// <param name="senparcSetting">用于提供 SenparcSetting.Cache_Redis_Configuration 和 Cache_Memcached_Configuration 两个参数，如果不使用这两种分布式缓存可传入null</param>
        /// <param name="autoRegisterAllPlatforms">是否从 appsettings.json 中自动注册所有平台（Open 平台除外）</param>
        /// <returns></returns>
        public static IRegisterService UseSenparcWeixin(this IRegisterService registerService, SenparcWeixinSetting senparcWeixinSetting, Action<IRegisterService, SenparcWeixinSetting> registerConfigure
          , bool autoRegisterAllPlatforms, IServiceProvider serviceProvider
          )
        {
            registerService.UseSenparcWeixin(senparcWeixinSetting, registerConfigure, serviceProvider);

            senparcWeixinSetting ??= Senparc.Weixin.Config.SenparcWeixinSetting;

            if (autoRegisterAllPlatforms)
            {
                //自动注册所有板块

                RegisterAllPlatforms(registerService, senparcWeixinSetting);

                if (senparcWeixinSetting.Items != null && senparcWeixinSetting.Items.Count > 0)
                {
                    foreach (var item in senparcWeixinSetting.Items)
                    {
                        RegisterAllPlatforms(registerService, item.Value);
                    }
                }
            }

            return registerService;
        }

        public static IRegisterService UseSenparcWeixin(this IApplicationBuilder app,
            Microsoft.Extensions.Hosting.IHostEnvironment/*IHostingEnvironment*/ env,
            SenparcSetting senparcSetting, SenparcWeixinSetting senparcWeixinSetting,
            Action<IRegisterService/*, SenparcSetting*/> globalRegisterConfigure,
            Action<IRegisterService, SenparcWeixinSetting> weixinRegisterConfigure,
            bool autoRegisterAllPlatforms,

             //CO2NET 全局设置
             bool autoScanExtensionCacheStrategies = false,
             Func<List<IDomainExtensionCacheStrategy>> extensionCacheStrategiesFunc = null
            )
        {
            //进行全局注册
            var registerService = Senparc.Weixin.AspNet.WeixinRegister.UseSenparcWeixin(app, env, senparcSetting, senparcWeixinSetting, globalRegisterConfigure, weixinRegisterConfigure, autoScanExtensionCacheStrategies, extensionCacheStrategiesFunc,
                useSenparcWeixin: false/* 下方手动执行 */);

            //进行自动注册
            //senparcWeixinSetting ??= Senparc.Weixin.Config.SenparcWeixinSetting;
            registerService.UseSenparcWeixin(senparcWeixinSetting, weixinRegisterConfigure, autoRegisterAllPlatforms, app.ApplicationServices);

            return registerService;
        }

        /// <summary>
        /// 是否已经设置为有效的参数
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        private static bool IsAvaliablePlatform(string appId)
        {
            return !appId.IsNullOrEmpty() && !appId.StartsWith("#{");
        }

        /// <summary>
        /// 根据信息填写情况，自动注册所有平台
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        private static IRegisterService RegisterAllPlatforms(IRegisterService registerService, SenparcWeixinSettingItem setting)
        {
            if (IsAvaliablePlatform(setting.WeixinAppId))
            {
                registerService.RegisterMpAccount(setting, "公众号自动注册");
            }

            if (IsAvaliablePlatform(setting.WxOpenAppId))
            {
                registerService.RegisterWxOpenAccount(setting, "小程序自动注册");
            }

            if (IsAvaliablePlatform(setting.WeixinCorpId))
            {
                registerService.RegisterWorkAccount(setting, "企业微信自动注册");
            }

            if (IsAvaliablePlatform(setting.WeixinPay_Key))
            {
                registerService.RegisterTenpayOld(setting, "微信支付V2自动注册");
            }

            if (IsAvaliablePlatform(setting.TenPayV3_MchId))
            {
                registerService.RegisterTenpayV3(setting, "微信支付V2（V3文档）自动注册");
            }

            if (IsAvaliablePlatform(setting.TenPayV3_APIv3Key))
            {
                registerService.RegisterTenpayApiV3(setting, "微信支付ApiV3自动注册");
            }


            return registerService;
        }
    }
}
