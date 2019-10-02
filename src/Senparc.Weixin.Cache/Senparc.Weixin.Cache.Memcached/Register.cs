/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin.Cache.Memcached 注册类


    创建标识：Senparc - 20180609

    修改标识：Senparc - 20180802
    修改描述：当前类所有方法支持 .net standard 2.0

    修改标识：Senparc - 20180802
    修改描述：v2.5.102 RegisterDomainCache() 方法重命名为 ActivityDomainCache()

----------------------------------------------------------------*/

#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
using Microsoft.AspNetCore.Builder;
#endif

namespace Senparc.Weixin.Cache.Memcached
{
    public static class Register
    {
#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
        /// <summary>
        /// 注册 Senparc.Weixin.Cache.Memcached
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseSenparcWeixinCacheMemcached(this IApplicationBuilder app)
        {
            app.UseEnyimMemcached();
            ActivityDomainCache();
            return app;
        }
#endif

        /// <summary>
        /// 激活领域缓存
        /// </summary>
        public static void ActivityDomainCache()
        {
            var cache = MemcachedContainerCacheStrategy.Instance;
        }

    }
}
