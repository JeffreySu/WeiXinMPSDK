/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：Register.cs
    文件功能描述：Senparc.Weixin.Memcached.Redis 快捷注册流程


    创建标识：Senparc - 20180222

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.RegisterServices;

namespace Senparc.Weixin.Cache.Memcached
{
    public static class Register
    {

        /// <summary>
        /// 注册 Memcached 缓存信息
        /// </summary>
        /// <param name="registerService">RegisterService</param>
        /// <param name="memcachedConfig">memcached连接字符串列表</param>
        /// <param name="memcachedObjectCacheStrategyInstance">缓存策略的委托，第一个参数为 memcachedConfig</param>
        /// <returns></returns>
        public static IRegisterService RegisterCacheMemcached(this IRegisterService registerService,
            Dictionary<string, int> memcachedConfig,
            Func<Dictionary<string, int>, IObjectCacheStrategy> memcachedObjectCacheStrategyInstance)
        {
            MemcachedObjectCacheStrategy.RegisterServerList(memcachedConfig);

            //此处先执行一次委托，直接在下方注册结果，提高每次调用的执行效率
            IObjectCacheStrategy objectCacheStrategy = memcachedObjectCacheStrategyInstance(memcachedConfig);
            if (objectCacheStrategy != null)
            {
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => objectCacheStrategy);//Memcached
            }

            return registerService;
        }

    }
}
