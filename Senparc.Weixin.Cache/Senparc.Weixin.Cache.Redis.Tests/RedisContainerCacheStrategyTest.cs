using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.CacheUtility;

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

        [TestMethod]
        public void SingletonTest()
        {
            var cache2 = RedisContainerCacheStrategy.Instance;
            Assert.AreEqual(cache.GetHashCode(), cache2.GetHashCode());
        }

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

        [TestMethod]
        public void RemoveTest()
        {
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
