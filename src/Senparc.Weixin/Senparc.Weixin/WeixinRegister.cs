/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

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
    
----------------------------------------------------------------*/

#if NETCOREAPP2_0 || NETCOREAPP2_1
using Microsoft.Extensions.Options;
#endif
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="senparcWeixinSetting"></param>
        /// <param name="extensionCacheStrategiesFunc"><para>需要注册的扩展缓存策略</para>
        /// <para>（LocalContainerCacheStrategy、RedisContainerCacheStrategy、MemcacheContainerCacheStrategy已经自动注册），</para>
        /// <para>如果设置为 null（注意：不适委托返回 null，是整个委托参数为 null），则自动使用反射扫描所有可能存在的扩展缓存策略</para></param>
        /// <param name="autoScanExtensionCacheStrategies">是否自动扫描全局的扩展缓存（会增加系统启动时间）</param>
        /// <returns></returns>
        public static IRegisterService UseSenparcWeixin(this IRegisterService registerService, SenparcWeixinSetting senparcWeixinSetting, Func<IList<IDomainExtensionCacheStrategy>> extensionCacheStrategiesFunc = null, bool autoScanExtensionCacheStrategies = false)
        {
            senparcWeixinSetting = senparcWeixinSetting ?? new SenparcWeixinSetting();

            //Senparc.Weixin SDK 配置
            Senparc.Weixin.Config.IsDebug = senparcWeixinSetting.IsDebug;
            Senparc.Weixin.Config.DefaultSenparcWeixinSetting = senparcWeixinSetting;

            var cacheTypes = "";//所有注册的扩展缓存

            // 微信的 本地 缓存
            var cache = LocalContainerCacheStrategy.Instance;//只要引用就可以被激活
            cacheTypes += typeof(LocalContainerCacheStrategy);

            if (extensionCacheStrategiesFunc != null)
            {
                var containerCacheStrategies = extensionCacheStrategiesFunc();
                if (containerCacheStrategies != null)
                {
                    foreach (var cacheStrategy in containerCacheStrategies)
                    {
                        var exCache = cacheStrategy;//确保能运行到就行，会自动注册
                        cacheTypes += "\r\n" + exCache.GetType();
                    }
                }
            }

            DateTime dt1 = DateTime.Now;

            //官方扩展缓存注册

            var officialTypes = new List<Type>() { typeof(LocalContainerCacheStrategy) };//官方提供的扩展缓存策略

            //自动注册 Redis 和 Memcached
            //Redis
            var redisConfiguration = senparcWeixinSetting.Cache_Redis_Configuration;
            //if ((!string.IsNullOrEmpty(redisConfiguration) && redisConfiguration != "Redis配置"))
            {
                try
                {
                    var redisInstance = ReflectionHelper.GetStaticMember("Senparc.Weixin.Cache.Redis", "Senparc.Weixin.Cache.Redis", "RedisContainerCacheStrategy", "Instance");
                    if (redisInstance != null)
                    {
                        officialTypes.Add(redisInstance.GetType());
                        cacheTypes += "\r\n" + redisInstance.GetType();
                    }
                }
                catch (Exception ex)
                {
                    WeixinTrace.WeixinExceptionLog(new Exceptions.WeixinException(ex.Message, ex));
                }
            }

            //Memcached
            var memcachedConfiguration = senparcWeixinSetting.Cache_Memcached_Configuration;
            //if ((!string.IsNullOrEmpty(memcachedConfiguration) && memcachedConfiguration != "Memcached配置"))
            {
                try
                {
                    var memcachedInstance = ReflectionHelper.GetStaticMember("Senparc.Weixin.Cache.Memcached", "Senparc.Weixin.Cache.Memcached", "MemcachedContainerCacheStrategy", "Instance");
                    if (memcachedInstance != null)
                    {
                        officialTypes.Add(memcachedInstance.GetType());
                        cacheTypes += "\r\n" + memcachedInstance.GetType();
                    }
                }
                catch (Exception ex)
                {
                    WeixinTrace.WeixinExceptionLog(new Exceptions.WeixinException(ex.Message, ex));
                }
            }

            var scanTypesCount = 0;
            if (autoScanExtensionCacheStrategies)
            {
                //查找所有扩展缓存
                var types = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a =>
                            {
                                try
                                {
                                    scanTypesCount++;
                                    var aTypes = a.GetTypes();
                                    return aTypes.Where(t => !t.IsAbstract && !officialTypes.Contains(t) && t.GetInterfaces().Contains(typeof(IDomainExtensionCacheStrategy)));
                                }
                                catch (Exception ex)
                                {
                                    WeixinTrace.SendCustomLog("UseSenparcWeixin() 自动扫描程序集异常：" + a.FullName, ex.ToString());
                                    return new List<Type>();//不能 return null
                                }
                            });

                if (types != null)
                {
                    foreach (var type in types)
                    {
                        if (type == null)
                        {
                            continue;
                        }
                        try
                        {
                            var exCache = ReflectionHelper.GetStaticMember(type, "Instance");
                            cacheTypes += "\r\n" + type;//由于数量不多，这里使用String，不使用StringBuilder
                        }
                        catch (Exception ex)
                        {
                            WeixinTrace.WeixinExceptionLog(new Exceptions.WeixinException(ex.Message, ex));
                        }
                    }
                }
            }

            DateTime dt2 = DateTime.Now;
            var exCacheLog = "注册总用时：{0}ms\r\n自动扫描程序集：{1}个\r\n扩展缓存：{2}".FormatWith((dt2 - dt1).TotalMilliseconds, scanTypesCount, cacheTypes);
            WeixinTrace.SendCustomLog("自动注册扩展缓存完成", exCacheLog);

            return registerService;
        }
    }
}
