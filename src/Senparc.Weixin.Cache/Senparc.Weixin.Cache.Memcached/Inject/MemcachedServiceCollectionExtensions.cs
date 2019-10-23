/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：MemcachedServiceCollectionExtensions.cs
    文件功能描述：Memcached 依赖注入设置。


    创建标识：Senparc - 20180222

----------------------------------------------------------------*/


#if NET45 || NET461

#else
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Senparc.Weixin.Cache.Memcached;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MemcachedServiceCollectionExtensions
    {
        /// <summary>
        /// .NET Core下设置依赖注入（暂时请用CO2NET对应 AddSenparcMemcached 方法）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddWeixinMemcached(this IServiceCollection services, Action<MemcachedClientOptions> setupAction)
        {
            return services;
            //services.AddSingleton<MemcachedObjectCacheStrategy, MemcachedObjectCacheStrategy>();
            //services.AddSingleton<MemcachedContainerStrategy, MemcachedContainerStrategy>();
            //return services.AddEnyimMemcached(setupAction);
        }
    }
}

#endif