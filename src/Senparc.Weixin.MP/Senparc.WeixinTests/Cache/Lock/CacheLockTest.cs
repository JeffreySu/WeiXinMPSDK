using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Redis;

namespace Senparc.WeixinTests.Cache.Lock
{
    [TestClass]
    public class CacheLockTest
    {
        [TestMethod]
        public void LockTest()
        {
            /* 测试逻辑：
             * 20个异步线程同时进行，
             * 线程内有两组不同的ResourceName（"Test-0"，"Test-1"，模拟AccessTokenContainer和JsTicketContainer），
             * 每组ResourceName下面有两组分别为0和1的appId。
             * 同时运行这些线程，观察锁是否生效。
             * 失败的现象：1、同一个ResourceName、同一个appId下有两个线程同时获得锁
             *             2、不同的ResourceName、appId之间任意组合相互干扰；
             *             3、出现死锁；
             *             4、某线程始终无法获得锁（超时或一直运行）
             *             
             * 注意：超时导致的失败可能是由于设置的最大等待时间现对于测试的Thread.sleep太短。
             */

            bool useRedis = true;

            if (useRedis)
            {
                var redisConfiguration = "localhost:6379";
                RedisManager.ConfigurationOption = redisConfiguration;
                CacheStrategyFactory.RegisterContainerCacheStrategy(() => RedisContainerCacheStrategy.Instance);//Redis
            }

            var cache = CacheStrategyFactory.GetContainerCacheStragegyInstance();
            Random rnd = new Random();
            Parallel.For(0, 4, i =>
            {
                var appId = (i % 2).ToString();
                var resourceName = "Test-" + rnd.Next(0, 1);
                Console.WriteLine("线程 {0} / {1} : {2} 进入，准备尝试锁", Thread.CurrentThread.GetHashCode(), resourceName, appId);

                DateTime dt1 = DateTime.Now;
                using (var cacheLock = cache.InstanceCacheLockWrapper(resourceName, appId))
                {
                    var result = cacheLock.LockSuccessful
                        ? "成功"
                        :"【失败！】";

                    Console.WriteLine("线程 {0} / {1} : {2} 进入锁，等待时间：{3}ms，获得锁结果：{4}", Thread.CurrentThread.GetHashCode(), resourceName, appId, (DateTime.Now - dt1).TotalMilliseconds, result);

                    Thread.Sleep(500);
                }
                Console.WriteLine("线程 {0} / {1} : {2} 释放锁", Thread.CurrentThread.GetHashCode(), resourceName, appId);
            });
        }
    }
}
