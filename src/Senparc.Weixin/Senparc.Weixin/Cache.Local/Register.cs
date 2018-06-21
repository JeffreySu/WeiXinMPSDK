#if NETCOREAPP2_0 || NETCOREAPP2_1
using Microsoft.AspNetCore.Builder;
#endif

namespace Senparc.Weixin.Cache.Local
{
    public static class Register
    {
#if NETCOREAPP2_0 || NETCOREAPP2_1

        /// <summary>
        /// 注册 Senparc.Weixin.Cache.Locals
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseSenparcWeixinCacheLocal(this IApplicationBuilder app)
        {
            RegisterDomainCache();
            return app;
        }
#endif
    }
}
