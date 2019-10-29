﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin.Cache.Redis 注册类


    创建标识：Senparc - 20180609

    修改标识：Senparc - 20180802
    修改描述：当前类所有方法支持 .net standard 2.0

    修改标识：Senparc - 20191002
    修改描述：v2.7.102 RegisterDomainCache() 方法重命名为 ActivityDomainCache()

----------------------------------------------------------------*/

#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
using Microsoft.AspNetCore.Builder;
#endif

namespace Senparc.Weixin.Cache.Redis
{
    /// <summary>
    /// Senparc.Weixin.Cache.Redis 注册类
    /// </summary>
    public static class Register
    {
#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0

        /// <summary>
        /// 注册 Senparc.Weixin.Cache.Redis
        /// </summary>
        /// <param name="app"></param>
        public static IApplicationBuilder UseSenparcWeixinCacheRedis(this IApplicationBuilder app)
        {
            ActivityDomainCache();
            return app;
        }
#endif


        /// <summary>
        /// 激活领域缓存
        /// </summary>
        public static void ActivityDomainCache()
        {
            //通过调用 ContainerCacheStrategy，激活领域模型注册过程
            var cache = RedisContainerCacheStrategy.Instance;
            var cacheHashSet = RedisHashSetContainerCacheStrategy.Instance;
        }
    }
}
