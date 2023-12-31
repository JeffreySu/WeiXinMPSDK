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

    文件名：WeixinRegister.cs
    文件功能描述：Senparc.Weixin 快捷注册流程（包括Thread、TraceLog等）


    创建标识：Senparc - 20180222

    修改标识：Senparc - 20180516
    修改描述：优化 RegisterService


    修改标识：Senparc - 20180607
    修改描述：Register 改名为 WeixinRegister

    修改标识：Senparc - 201806029
    修改描述：v5.0.3.1 修复 WeixinRegister.UseSenparcWeixin() 方法的 IsDebug 设置问题
 
    修改标识：Senparc - 20180705
    修改描述：v5.0.7 支持 CO2NET v0.1.7，为 WeixinRegister.UseSenparcWeixin() 方法提供自动注册扩展缓存的能力
 
    修改标识：Senparc - 20180706
    修改描述：v5.0.7.1 优化扩展缓存自动注册过程

    修改标识：Senparc - 20180707
    修改描述：v5.0.8.4 优化 WeixinRegister.UseSenparcWeixin() 提供 autoScanExtensionCacheStrategies 参数，
              可设置是否全局扫描扩展缓存（扫描会增加系统启动时间）
    修改描述：v5.0.10 UseSenparcWeixin() 添加 SenparcSetting 参数

    修改标识：Senparc - 20191002
    修改描述：v6.6.102 添加 UseSenparcWeixin() 新方法

    修改标识：Senparc - 20191005
    修改描述：v6.6.102 添加 UseSenparcWeixin() 包含 CO2NET 全局注册的新方法

    修改标识：Senparc - 20200228
    修改描述：v6.7.300 添加 CsRedis 的注册

    修改标识：Senparc - 20220224
    修改描述：完善 UseSenparcWeixin() 方法，为 null 值的 senparcWeixinSetting 自动获取值

    修改标识：Senparc - 20230614
    修改描述：v6.15.10 UseSenparcWeixin() 方法添加 autoCreateApi 参数，用于设置是自动生成微信接口的 API，默认为关闭

----------------------------------------------------------------*/

#if !NET462
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
#endif
using Microsoft.Extensions.Configuration;
using Senparc.CO2NET;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;

namespace Senparc.Weixin
{
    /// <summary>
    /// 微信注册
    /// </summary>
    public static class WeixinRegister
    {
        /// <summary>
        /// 开始 Senparc.Weixin SDK 初始化参数流程
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="senparcWeixinSetting">微信全局设置参数，必填</param>
        /// <param name="senparcSetting">用于提供 SenparcSetting.Cache_Redis_Configuration 和 Cache_Memcached_Configuration 两个参数，如果不使用这两种分布式缓存可传入null</param>
        /// <returns></returns>
        public static IRegisterService UseSenparcWeixin(this IRegisterService registerService, SenparcWeixinSetting senparcWeixinSetting, SenparcSetting senparcSetting = null)
        {
            senparcWeixinSetting = senparcWeixinSetting ?? new SenparcWeixinSetting();
            senparcSetting = (senparcSetting ?? CO2NET.Config.SenparcSetting) ?? new SenparcSetting();

            //Senparc.Weixin SDK 配置
            Senparc.Weixin.Config.SenparcWeixinSetting = senparcWeixinSetting;

            /* 扩展缓存注册开始 */
            var cacheTypes = "";//所有注册的扩展缓存

            // 微信的 本地 缓存
            var cache = LocalContainerCacheStrategy.Instance;//只要引用就可以被激活
            cacheTypes += typeof(LocalContainerCacheStrategy);

            var dt1 = SystemTime.Now;

            //官方扩展缓存注册

            //var officialTypes = new List<Type>() { typeof(LocalContainerCacheStrategy) };//官方提供的扩展缓存策略

            //自动注册 Redis 和 Memcached
            //Redis
            var redisConfiguration = senparcSetting.Cache_Redis_Configuration;
            if (!string.IsNullOrEmpty(redisConfiguration) &&
                /*缓存配置默认值，不启用*/
                redisConfiguration != "Redis配置" &&
                redisConfiguration != "#{Cache_Redis_Configuration}#")
            {
                try
                {
                    {
                        //StackExchange.Redis
                        var redisInstance = ReflectionHelper.GetStaticMember("Senparc.Weixin.Cache.Redis", "Senparc.Weixin.Cache.Redis", "RedisContainerCacheStrategy", "Instance");
                        if (redisInstance != null)
                        {
                            //officialTypes.Add(redisInstance.GetType());
                            cacheTypes += "\r\n" + redisInstance.GetType();
                        }
                    }
                    {
                        //CsRedis
                        var redisInstance = ReflectionHelper.GetStaticMember("Senparc.Weixin.Cache.CsRedis", "Senparc.Weixin.Cache.CsRedis", "RedisContainerCacheStrategy", "Instance");
                        if (redisInstance != null)
                        {
                            //officialTypes.Add(redisInstance.GetType());
                            cacheTypes += "\r\n" + redisInstance.GetType();
                        }
                    }
                }
                catch (Exception ex)
                {
                    WeixinTrace.WeixinExceptionLog(new Exceptions.WeixinException(ex.Message, ex));
                }
            }

            //Memcached
            var memcachedConfiguration = senparcSetting.Cache_Memcached_Configuration;
            if (!string.IsNullOrEmpty(memcachedConfiguration) &&
                /*缓存配置默认值，不启用*/
                memcachedConfiguration != "Memcached配置" &&
                memcachedConfiguration != "#{Cache_Memcached_Configuration}#")
            {
                try
                {
                    var memcachedInstance = ReflectionHelper.GetStaticMember("Senparc.Weixin.Cache.Memcached", "Senparc.Weixin.Cache.Memcached", "MemcachedContainerCacheStrategy", "Instance");
                    if (memcachedInstance != null)
                    {
                        //officialTypes.Add(memcachedInstance.GetType());
                        cacheTypes += "\r\n" + memcachedInstance.GetType();
                    }
                }
                catch (Exception ex)
                {
                    WeixinTrace.WeixinExceptionLog(new Exceptions.WeixinException(ex.Message, ex));
                }
            }

            var exCacheLog = $"微信扩展缓存注册总用时：{SystemTime.DiffTotalMS(dt1, "f4")}ms\r\n扩展缓存：{cacheTypes}";
            WeixinTrace.SendCustomLog("微信扩展缓存注册完成", exCacheLog);

            /* 扩展缓存注册结束 */

            return registerService;
        }

#if !NET462

        /// <summary>
        /// 开始 Senparc.Weixin SDK 初始化参数流程
        /// </summary>
        /// <param name="app">configuration source</param>
        /// <param name="senparcSetting">SenparcSetting 对象</param>
        /// <param name="senparcWeixinSetting">SenparcWeixinSetting 对象</param>
        /// <param name="globalRegisterConfigure">RegisterService 设置</param>
        /// <param name="weixinRegisterConfigure">SenparcWeixinSetting RegisterService 设置</param>
        /// <param name="autoScanExtensionCacheStrategies">是否自动扫描全局的扩展缓存（会增加系统启动时间）</param>
        /// <param name="extensionCacheStrategiesFunc"><para>需要手动注册的扩展缓存策略</para>
        /// <para>（LocalContainerCacheStrategy、RedisContainerCacheStrategy、MemcacheContainerCacheStrategy已经自动注册），</para>
        /// <para>如果设置为 null（注意：不适委托返回 null，是整个委托参数为 null），则自动使用反射扫描所有可能存在的扩展缓存策略</para></param>
        /// <returns></returns>
        /// <returns></returns>
        public static IRegisterService UseSenparcWeixin(this IConfigurationRoot app,
            SenparcSetting senparcSetting, SenparcWeixinSetting senparcWeixinSetting,
            Action<IRegisterService/*, SenparcSetting*/> globalRegisterConfigure,
            Action<IRegisterService, SenparcWeixinSetting> weixinRegisterConfigure,
             //CO2NET 全局设置
             bool autoScanExtensionCacheStrategies = false,
             Func<List<IDomainExtensionCacheStrategy>> extensionCacheStrategiesFunc = null
            )
        {
            //注册 CO2NET 全局
            var register = app.UseSenparcGlobal(senparcSetting, globalRegisterConfigure, autoScanExtensionCacheStrategies, extensionCacheStrategiesFunc);

            return WeixinRegister.UseSenparcWeixin(register.registerService, senparcWeixinSetting, senparcSetting);
        }
#endif

        #region v6.6.102+ 新方法

        /// <summary>
        /// 开始 Senparc.Weixin SDK 初始化参数流程
        /// </summary>
        /// <param name="registerService"></param>
        /// <param name="senparcWeixinSetting"></param>
        /// <param name="registerConfigure"></param>
        /// <returns></returns>
        public static IRegisterService UseSenparcWeixin(this IRegisterService registerService, SenparcWeixinSetting senparcWeixinSetting, Action<IRegisterService, SenparcWeixinSetting> registerConfigure
#if !NET462
            , IServiceProvider serviceProvider = null
#endif
            )
        {
#if !NET462
            //默认从 appsettings.json 中取
            if (senparcWeixinSetting == null && serviceProvider != null)
            {
                senparcWeixinSetting = serviceProvider.GetService<IOptions<SenparcWeixinSetting>>()!.Value;
            }
#endif

            var register = registerService.UseSenparcWeixin(senparcWeixinSetting, senparcSetting: null);

            //由于 registerConfigure 内可能包含了 app.UseSenparcWeixinCacheRedis() 等注册代码，需要在在 registerService.UseSenparcWeixin() 自动加载 Redis 后进行
            //因此必须在 registerService.UseSenparcWeixin() 之后执行。

            registerConfigure?.Invoke(registerService, senparcWeixinSetting);
            return register;
        }

        //从 v6.7.100 开始分离到 Senparc.Weixin.AspNet
        //#if NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP3_1 || NET6_0
        //        /// <summary>
        //        /// <para>开始包含 CO2NET 注册在内的 Senparc.Weixin SDK 初始化参数流程</para>
        //        /// <para>注意：本方法集成了 CON2ET 全局注册以及 Senparc.Weixin SDK 微信注册过程，提供给对代码行数有极限追求的开发者使用，常规情况下为了提高代码可读性和可维护性，并不推荐使用此方法。</para>
        //        /// </summary>
        //        /// <param name="registerService"></param>
        //        /// <param name="senparcWeixinSetting"></param>
        //        /// <param name="registerConfigure"></param>
        //        /// <returns></returns>
        //        public static IRegisterService UseSenparcWeixin(this IApplicationBuilder app,
        //#if NETSTANDARD2_0 || NETSTANDARD2_1
        //            IHostingEnvironment env,
        //#else
        //            IWebHostEnvironment env,
        //#endif
        //            SenparcSetting senparcSetting, SenparcWeixinSetting senparcWeixinSetting, Action<IRegisterService> globalRegisterConfigure, Action<IRegisterService> weixinRegisterConfigure,
        //             //CO2NET 全局设置
        //             bool autoScanExtensionCacheStrategies = false, Func<List<IDomainExtensionCacheStrategy>> extensionCacheStrategiesFunc = null
        //            )
        //        {
        //            //注册 CO2NET 全局
        //            var register = app.UseSenparcGlobal(env, senparcSetting, globalRegisterConfigure, autoScanExtensionCacheStrategies, extensionCacheStrategiesFunc);
        //            //注册微信
        //            register.UseSenparcWeixin(senparcWeixinSetting, weixinRegisterConfigure);
        //            return register;
        //        }
        //#endif
        #endregion
    }
}
