using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Memcached;
using Senparc.Weixin.Cache.Redis;

namespace Senparc.WeixinTests
{
    public class MutipleCacheTestWapper
    {
        /// <summary>
        /// 测试多种缓存
        /// </summary>
        public static void RunMutipleCache(Action action)
        {
            RunMutipleCache(action, CacheType.Local);
        }

        /// <summary>
        /// 测试多种缓存
        /// </summary>
        public static void RunMutipleCache(Action action, params CacheType[] cacheTypes)
        {
            List<IObjectCacheStrategy> cacheStrategies = new List<IObjectCacheStrategy>();

            foreach (var cacheType in cacheTypes)
            {
                switch (cacheType)
                {
                    case CacheType.Local:
                        cacheStrategies.Add(LocalObjectCacheStrategy.Instance);
                        break;
                    case CacheType.Redis:
                        cacheStrategies.Add(RedisObjectCacheStrategy.Instance);
                        break;
                    case CacheType.Memcached:
                        cacheStrategies.Add(MemcachedObjectCacheStrategy.Instance);
                        break;
                }
            }

            foreach (var objectCacheStrategy in cacheStrategies)
            {
                //原始缓存策越
                var originalCache = CacheStrategyFactory.GetObjectCacheStrategyInstance();

                Console.WriteLine("== 使用缓存策略：" + objectCacheStrategy.GetType().Name + " 开始 == ");

                //使用当前缓存策略
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => objectCacheStrategy);

                try
                {
                    action();//执行
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Console.WriteLine("== 使用缓存策略：" + objectCacheStrategy.GetType().Name + " 结束 == ");

                //还原缓存策略
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => originalCache);
            }
        }
    }
}
