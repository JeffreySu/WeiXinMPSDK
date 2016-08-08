using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Cache;

namespace Senparc.WeixinTests.Cache.Lock
{
    [TestClass]
    public class CacheLockTest
    {
        [TestMethod]
        public void LockTest()
        {
            var cache = CacheStrategyFactory.GetContainerCacheStragegyInstance();
            Random rnd = new Random();
            Parallel.For(0, 20, i =>
            {
                var appId = (i % 2).ToString();
                Console.WriteLine("线程 {0} - {1} 进入，准备尝试锁", Thread.CurrentThread.GetHashCode(), appId);
                using (cache.InstanceCacheLockWrapper("Test-" + rnd.Next(0, 2), appId))
                {
                    Console.WriteLine("线程 {0} - {1} 进入锁", Thread.CurrentThread.GetHashCode(), appId);
                    Thread.Sleep(500);
                }
                Console.WriteLine("线程 {0} - {1} 释放锁", Thread.CurrentThread.GetHashCode(), appId);
            });
        }
    }
}
