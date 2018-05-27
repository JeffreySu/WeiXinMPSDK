#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Containers;
using System;

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
        private IContainerCacheStrategy cache;
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
            //调整测试的缓存策略
            cache = LocalContainerCacheStrategy.Instance;

            var key = Guid.NewGuid().ToString();
            var count = cache.GetCount();
            Console.WriteLine("count:" + count);


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
            Console.WriteLine("count2:" + count2);

            if (cache is RedisContainerCacheStrategy)
            {
                Console.WriteLine("Redis Cache");
                Assert.AreEqual(count, count2);//目前Redis缓存使用HashSet，反复测试不会发生变化，第一次会有变化
            }
            else
            {
                Console.WriteLine(cache.GetType() + " Cache");
                Assert.AreEqual(count + 1, count2);
            }

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
            Console.WriteLine("current count:" + count);
            Console.WriteLine("new item:" + key);
            cache.InsertToCache(key, new TestContainerBag1()
            {
                Key = key,
                Name = "Name"
            });

            var item = cache.Get(key);
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.Key);

            Console.WriteLine("read new item from redis:" + item.Key);

            var count2 = cache.GetCount();
            Assert.AreEqual(count + 1, count2);//如果这里报错，查看一下是否从其他的命名空间下面读取了

            cache.RemoveFromCache(key);
            item = cache.Get(key);
            Assert.IsNull(item);
            var count3 = cache.GetCount();
            Assert.AreEqual(count, count3);
        }
    }
}
