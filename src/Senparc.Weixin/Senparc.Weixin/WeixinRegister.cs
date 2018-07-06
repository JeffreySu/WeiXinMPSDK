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

----------------------------------------------------------------*/

#if NETCOREAPP2_0 || NETCOREAPP2_1
using Microsoft.Extensions.Options;
#endif
using Senparc.CO2NET.Cache;
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
        /// <returns></returns>
        public static IRegisterService UseSenparcWeixin(this IRegisterService registerService, SenparcWeixinSetting senparcWeixinSetting, Func<IList<IDomainExtensionCacheStrategy>> extensionCacheStrategiesFunc = null)
        {
            senparcWeixinSetting = senparcWeixinSetting ?? new SenparcWeixinSetting();

            //Senparc.Weixin SDK 配置
            Senparc.Weixin.Config.IsDebug = senparcWeixinSetting.IsDebug;
            Senparc.Weixin.Config.DefaultSenparcWeixinSetting = senparcWeixinSetting;

            // 微信的 本地 缓存
            var cache = LocalContainerCacheStrategy.Instance;//只要引用就可以被激活

            if (extensionCacheStrategiesFunc != null)
            {
                var containerCacheStrategies = extensionCacheStrategiesFunc();
                if (containerCacheStrategies != null)
                {
                    foreach (var cacheStrategy in containerCacheStrategies)
                    {
                        var exCache = cacheStrategy;//确保能运行到就行，会自动注册
                    }
                }
            }
            else
            {
                var officialTypes = new List<Type>() { typeof(LocalContainerCacheStrategy) };

                //自动注册 Redis 和 Memcached
                //Redis
                var redisConfiguration = senparcWeixinSetting.Cache_Redis_Configuration;
                if ((!string.IsNullOrEmpty(redisConfiguration) && redisConfiguration != "Redis配置"))
                {
                    try
                    {
                        var redisInstance = ReflectionHelper.GetStaticMember("Senparc.Weixin.Cache.Redis", "Senparc.Weixin.Cache.Redis", "RedisContainerCacheStrategy", "Instance");
                        officialTypes.Add(redisInstance.GetType());
                    }
                    catch (Exception ex)
                    {
                        WeixinTrace.WeixinExceptionLog(new Exceptions.WeixinException(ex.Message, ex));
                    }
                }

                //Memcached
                var memcachedConfiguration = senparcWeixinSetting.Cache_Memcached_Configuration;
                if ((!string.IsNullOrEmpty(memcachedConfiguration) && memcachedConfiguration != "Memcached配置"))
                {
                    try
                    {
                        var memcachedInstance = ReflectionHelper.GetStaticMember("Senparc.Weixin.Cache.Memcached", "Senparc.Weixin.Cache.Memcached", "MemcachedContainerCacheStrategy", "Instance");
                    officialTypes.Add(memcachedInstance.GetType());
                    }
                    catch (Exception ex)
                    {
                        WeixinTrace.WeixinExceptionLog(new Exceptions.WeixinException(ex.Message, ex));
                    }
                }

                //查找所有扩产缓存
                var types = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes()
                            .Where(t => !t.IsAbstract && !officialTypes.Contains(t) && t.GetInterfaces().Contains(typeof(IDomainExtensionCacheStrategy))))
                            .ToArray();

                foreach (var type in types)
                {
                    try
                    {
                       var exCache =  ReflectionHelper.GetStaticMember(type, "Instance");
                    }
                    catch (Exception ex)
                    {
                        WeixinTrace.WeixinExceptionLog(new Exceptions.WeixinException(ex.Message, ex));
                    }
                }
            }

            return registerService;
        }
    }
}
