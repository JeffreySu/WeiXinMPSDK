using System;
using System.Collections.Generic;
using System.Linq;
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
        public void TestParallelThreadsRunAtSameTime()
        {
            var dt1 = DateTime.Now;
            Parallel.For(0, 100, i =>
            {
                Console.WriteLine("T:{0}，Use Time：{1}ms", Thread.CurrentThread.GetHashCode(),
                    (DateTime.Now - dt1).TotalMilliseconds);
                Thread.Sleep(20);
            });
            var dt2 = DateTime.Now;
            Console.WriteLine("Working Threads Count:{0}", 100 * 20 / (dt2 - dt1).TotalMilliseconds);
            //测试结果：同时运行的线程数约为4（平均3.6）
        }

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
            var threadsCount = 20M;
            int sleepMillionSeconds = 500;

            //初步估计需要重试时间
            var differentLocksCount = (2 /*appId*/* 2 /*resource*/) /* = diffrent locks count */;
            var runCycle = threadsCount / differentLocksCount /* = Run Cycle*/;
            var hopedTotalTime = runCycle * sleepMillionSeconds /* = Hoped Total Time */;
            var randomRetryDelay = (20 / 2) /*random retry delay*/;
            var retryTimes = hopedTotalTime / randomRetryDelay; /* = maxium retry times */;

            Console.WriteLine("differentLocksCount:{0}", differentLocksCount);
            Console.WriteLine("runCycle:{0}", runCycle);
            Console.WriteLine("hopedTotalTime:{0}ms", hopedTotalTime);
            Console.WriteLine("randomRetryDelay:{0}ms", randomRetryDelay);
            Console.WriteLine("retryTimes:{0}", retryTimes);
            Console.WriteLine("=====================");

            List<Thread> list = new List<Thread>();
            int runThreads = 0;

            for (int i = 0; i < (int)threadsCount; i++)
            {
                runThreads++;
                var thread = new Thread(() =>
                {
                    var appId = (i % 2).ToString();
                    var resourceName = "Test-" + rnd.Next(0, 2);
                    Console.WriteLine("线程 {0} / {1} : {2} 进入，准备尝试锁", Thread.CurrentThread.GetHashCode(), resourceName, appId);

                    DateTime dt1 = DateTime.Now;
                    using (var cacheLock = cache.InstanceCacheLockWrapper(resourceName, appId, (int)retryTimes, new TimeSpan(0, 0, 0, 0, 20)))
                    {
                        var result = cacheLock.LockSuccessful
                            ? "成功"
                            : "【失败！】";

                        Console.WriteLine("线程 {0} / {1} : {2} 进入锁，等待时间：{3}ms，获得锁结果：{4}", Thread.CurrentThread.GetHashCode(), resourceName, appId, (DateTime.Now - dt1).TotalMilliseconds, result);

                        Thread.Sleep(sleepMillionSeconds);
                    }
                    Console.WriteLine("线程 {0} / {1} : {2} 释放锁（{3}ms）", Thread.CurrentThread.GetHashCode(), resourceName, appId, (DateTime.Now - dt1).TotalMilliseconds);
                    runThreads--;
                });
                list.Add(thread);
            }

            list.ForEach(z => z.Start());

            while (runThreads > 0)
            {
                Thread.Sleep(10);
            }
            //Thread.Sleep((int)hopedTotalTime + 1000);
            //while (true)
            //{
            //    Thread.Sleep(10);
            //    if (list.Count(z => z.ThreadState == ThreadState.Aborted) == list.Count())
            //    {
            //        break;
            //    }
            //}

            //等待
            //Parallel.For(0, (int)threadsCount, i =>
            //{
            //    var appId = (i % 2).ToString();
            //    var resourceName = "Test-" + rnd.Next(0, 2);
            //    Console.WriteLine("线程 {0} / {1} : {2} 进入，准备尝试锁", Thread.CurrentThread.GetHashCode(), resourceName, appId);

            //    DateTime dt1 = DateTime.Now;
            //    using (var cacheLock = cache.InstanceCacheLockWrapper(resourceName, appId, (int)retryTimes, new TimeSpan(0, 0, 0, 0, 20)))
            //    {
            //        var result = cacheLock.LockSuccessful
            //            ? "成功"
            //            : "【失败！】";

            //        Console.WriteLine("线程 {0} / {1} : {2} 进入锁，等待时间：{3}ms，获得锁结果：{4}", Thread.CurrentThread.GetHashCode(), resourceName, appId, (DateTime.Now - dt1).TotalMilliseconds, result);

            //        Thread.Sleep(sleepMillionSeconds);
            //    }
            //    Console.WriteLine("线程 {0} / {1} : {2} 释放锁（{3}ms）", Thread.CurrentThread.GetHashCode(), resourceName, appId, (DateTime.Now - dt1).TotalMilliseconds);
            //});
        }
    }
}
