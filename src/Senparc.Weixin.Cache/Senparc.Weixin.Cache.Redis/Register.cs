#if NETCOREAPP2_0 || NETCOREAPP2_1
using Microsoft.AspNetCore.Builder;
#endif

namespace Senparc.Weixin.Cache.Redis
{
    public static class Register
    {
#if NETCOREAPP2_0 || NETCOREAPP2_1

        /// <summary>
        /// 注册 Senparc.Weixin.Cache.Memcached
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseSenparcWeixinCacheRedis(this IApplicationBuilder app)
        {
            RegisterDomainCache();
            return app;
        }
#endif


        /// <summary>
        /// 注册领域缓存
        /// </summary>
        public static void RegisterDomainCache()
        {
            var cache = RedisContainerCacheStrategy.Instance;
        }
    }
}
