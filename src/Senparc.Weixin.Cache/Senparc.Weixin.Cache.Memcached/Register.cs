#if !NET45
using Microsoft.AspNetCore.Builder;

namespace Senparc.Weixin.Cache.Memcached
{
    public static class Register
    {
        /// <summary>
        /// 注册 Senparc.Weixin.Cache.Memcached
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseSenparcWeixinCacheMemcached(this IApplicationBuilder app)
        {
            app.UseEnyimMemcached();
            var cache = MemcachedContainerCacheStrategy.Instance;
            return app;
        }
    }
}
#endif