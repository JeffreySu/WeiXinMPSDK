#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
    
    文件名：FreePublishGetArticleResultJson.cs
    文件功能描述：获取已发布文章 返回结果

    
    创建标识：Senparc - 20200105

    修改标识：Senparc - 20220224
    修改描述：v6.14.4 优化 UseSenparcWeixin() 参数

    修改标识：Senparc - 20230614
    修改描述：v6.15.10 UseSenparcWeixin() 方法添加 autoCreateApi 参数，用于设置是自动生成微信接口的 API，默认为关闭

----------------------------------------------------------------*/


#if !NET462
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Senparc.CO2NET;
using Senparc.CO2NET.AspNet;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;

namespace Senparc.Weixin.AspNet
{
    public static class WeixinRegister
    {
        /// <summary>
        /// <para>开始包含 CO2NET 注册在内的 Senparc.Weixin SDK 初始化参数流程</para>
        /// <para>注意：本方法集成了 CON2ET 全局注册以及 Senparc.Weixin SDK 微信注册过程，提供给对代码行数有极限追求的开发者使用，常规情况下为了提高代码可读性和可维护性，并不推荐使用此方法。</para>
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostEnvironment</param>
        /// <param name="senparcSetting">SenparcSetting</param>
        /// <param name="senparcWeixinSetting">SenparcWeixinSetting</param>
        /// <param name="globalRegisterConfigure">CO2NET 全局注册委托</param>
        /// <param name="weixinRegisterConfigure">Senparc.Weixin 注册委托</param>
        /// <param name="autoScanExtensionCacheStrategies">是否启用自动扩展缓存扫描</param>
        /// <param name="extensionCacheStrategiesFunc">扩展内存委托</param>
        /// <returns></returns>
        public static IRegisterService UseSenparcWeixin(this IApplicationBuilder app,
            Microsoft.Extensions.Hosting.IHostEnvironment/*IHostingEnvironment*/ env,
            SenparcSetting senparcSetting, SenparcWeixinSetting senparcWeixinSetting,
            Action<IRegisterService/*, SenparcSetting*/> globalRegisterConfigure,
            Action<IRegisterService, SenparcWeixinSetting> weixinRegisterConfigure,
             //CO2NET 全局设置
             bool autoScanExtensionCacheStrategies = false,
             Func<List<IDomainExtensionCacheStrategy>> extensionCacheStrategiesFunc = null
            )
        {
            //注册 CO2NET 全局
            var register = app.UseSenparcGlobal(env, senparcSetting, globalRegisterConfigure, autoScanExtensionCacheStrategies, extensionCacheStrategiesFunc);

            //注册微信
            register.UseSenparcWeixin(senparcWeixinSetting, weixinRegisterConfigure, app.ApplicationServices);

            return register;
        }
    }
}
#endif
