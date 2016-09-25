using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Cache.Redis.Tests
{
    [Serializable]
    internal class TestContainerBag1 : BaseContainerBag
    {
        private DateTime _dateTime;

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { this.SetContainerProperty(ref _dateTime, value); }
        }
    }

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
            cache.InsertToCache(key, new TestContainerBag1()
            {
                DateTime = DateTime.Now,
                Name = "Jeffrey"
            });

            var item = cache.Get(key);
            Assert.IsNotNull(item);

            Console.WriteLine(item.GetHashCode());
            Console.WriteLine(item.Key);
            Console.WriteLine(item.CacheTime);

            var count2 = cache.GetCount();
            Assert.AreEqual(count + 1, count2);

            var storedItem = cache.Get(key);
            Assert.IsNotNull(storedItem);
            Console.WriteLine(storedItem.GetHashCode());
            Console.WriteLine(storedItem.CacheTime);
            Console.WriteLine(storedItem.Name);
            Console.WriteLine(storedItem.Key);
            Console.WriteLine(((TestContainerBag1)storedItem).DateTime);
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
            cache.InsertToCache(key, new TestContainerBag1()
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
            Assert.AreEqual(count, count3);
        }
    }
}
