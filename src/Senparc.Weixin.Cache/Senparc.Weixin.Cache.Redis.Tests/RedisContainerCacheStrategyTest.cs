using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.Weixin.Cache.Redis.Tests
{
    [TestClass]
    public class RedisContainerCacheStrategyTest
    {
        private RedisContainerCacheStrategy cache;
        public RedisContainerCacheStrategyTest()
        {
            cache = RedisContainerCacheStrategy.Instance;
        }

        /// <summary>
        /// 单例测试
        /// </summary>
        [TestMethod]
        public void SingletonTest()
        {
            var cache2 = RedisContainerCacheStrategy.Instance;
            Assert.AreEqual(cache.GetHashCode(), cache2.GetHashCode());
        }

        /// <summary>
        /// 插入测试
        /// </summary>
        [TestMethod]
        public void InsertToCacheTest()
        {
            var key = Guid.NewGuid().ToString();
            var count = cache.GetCount();
            cache.InsertToCache(key, new ContainerItemCollection()
            {

            });

            var item = cache.Get(key);
            Assert.IsNotNull(item);

            Console.WriteLine(item.CacheSetKey);
            Console.WriteLine(item.CreateTime);

            var count2 = cache.GetCount();
            Assert.AreEqual(count + 1, count2);
        }

        /// <summary>
        /// 删除测试
        /// </summary>
        [TestMethod]
        public void RemoveTest()
        {
            //CacheStrategyFactory.RegisterContainerCacheStrategy(() => RedisContainerCacheStrategy.Instance);//Redis

            var key = Guid.NewGuid().ToString();
            var count = cache.GetCount();
            cache.InsertToCache(key, new ContainerItemCollection()
            {

            });

            var item = cache.Get(key);
            Assert.IsNotNull(item);
            var count2 = cache.GetCount();
            Assert.AreEqual(count + 1, count2);

            cache.RemoveFromCache(key);
            item = cache.Get(key);
            Assert.IsNull(item);
            var count3 = cache.GetCount();
            Assert.AreEqual(count,count3);
        }
    }
}
