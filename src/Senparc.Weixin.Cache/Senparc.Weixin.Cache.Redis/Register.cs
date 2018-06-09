#if !NET45
using Microsoft.AspNetCore.Builder;

namespace Senparc.Weixin.Cache.Redis
{
    public static class Register
    {
        /// <summary>
        /// 注册 Senparc.Weixin.Cache.Memcached
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseSenparcWeixinCacheRedis(this IApplicationBuilder app)
        {
            var cache = RedisContainerCacheStrategy.Instance;
            return app;
        }
    }
}
#endif