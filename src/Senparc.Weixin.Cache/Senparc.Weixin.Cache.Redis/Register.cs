/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin.Cache.Redis 注册类


    创建标识：Senparc - 20180609

    修改标识：Senparc - 20180802
    修改描述：当前类所有方法支持 .net standard 2.0

----------------------------------------------------------------*/

#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1
using Microsoft.AspNetCore.Builder;
#endif

namespace Senparc.Weixin.Cache.Redis
{
    /// <summary>
    /// Senparc.Weixin.Cache.Redis 注册类
    /// </summary>
    public static class Register
    {
#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1

        /// <summary>
        /// 注册 Senparc.Weixin.Cache.Redis
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
