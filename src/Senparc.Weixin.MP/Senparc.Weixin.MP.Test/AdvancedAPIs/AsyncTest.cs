using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Senparc.Weixin.MP.Test.CommonAPIs;
using System.Threading;
using System.Collections.Generic;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Cache.Redis;
using Senparc.Weixin.MP.Containers;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class AsyncTest : CommonApiTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var d1 = SystemTime.Now;
            Console.WriteLine("1. Start");

            bool finished = false;
            Thread thread = new Thread(() =>
            {
                Task.Factory.StartNew(async () =>
                {
                    var d2 = SystemTime.Now;
                    var tagJsonResult = await Senparc.Weixin.MP.AdvancedAPIs.UserTagApi.GetAsync(base._appId);
                    Console.WriteLine("3. tagJsonResult 1：" + string.Join(",", tagJsonResult.tags.Select(z => z.name)));
                    Console.WriteLine("4. 用时：" + (SystemTime.Now - d2).TotalMilliseconds + " ms");

                    return tagJsonResult;
                }).ContinueWith(async task =>
                {
                    var tagJsonResult = await task.Result;
                    Console.WriteLine("5. tagJsonResult 2：" + string.Join(",", tagJsonResult.tags.Select(z => z.name)));
                    finished = true;
                    //TODO：继续操作tagJsonResult
                });

                Console.WriteLine("2. StartNew Finished");

            });

            thread.Start();

            while (!finished)
            {
                Thread.Sleep(5);
            }

            Console.WriteLine("6. End：" + (SystemTime.Now - d1).TotalMilliseconds + " ms");

        }


        /// <summary>
        /// 并发测试
        /// </summary>
        [TestMethod]
        public void ConcurrentTesting()
        {
            var threadCount = 500;//同时运行线程数
            var finishedCount = 0;
            var threads = new List<Thread>();

            var apiTotalTime = 0D;//接口总耗时

            //指定缓存
            Senparc.Weixin.Cache.Redis.Register.RegisterDomainCache();//注册领域缓存
            CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);

            var cacheStrategy = CacheStrategyFactory.GetObjectCacheStrategyInstance();
            Console.WriteLine($"== 开始，缓存：{cacheStrategy.GetType()} ==");

            //先获取一次AccessToken（避免并发过程中获取）        ——无效
            var accessToken = AccessTokenContainer.GetAccessTokenResult(base._appId).access_token;

            object objLock = new object();

            for (int i = 0; i < threadCount; i++)
            {
                var index = i;
                var thread = new Thread(async () =>
                {
                    try
                    {
                        var dt0 = SystemTime.Now;
                        var threadName = Thread.CurrentThread.Name;

                        Console.WriteLine($"{SystemTime.Now.ToString("HH:mm:ss.ffffff")}\t[{threadName}] START");

                        //执行异步API
                        var result = await MP.AdvancedAPIs.QrCodeApi.CreateAsync(base._appId, 300, 100000 * 10 + index, QrCode_ActionName.QR_SCENE);

                        var apiRunTime = (SystemTime.Now - dt0).TotalMilliseconds;
                        Console.WriteLine($"{SystemTime.Now.ToString("HH:mm:ss.ffffff")}\t[{threadName}] RESULT - 耗时 {apiRunTime:###,###}ms - {result.url}");

                        apiTotalTime += apiRunTime;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{SystemTime.Now.ToString("HH:mm:ss.ffffff")}\t{ex.ToString()}");
                    }
                    finally
                    {
                        lock (objLock)
                        {
                            finishedCount++;
                        }
                    }
                });
                thread.Name = "T" + index.ToString("00");

                threads.Add(thread);
            }

            var dt1 = SystemTime.Now;
            foreach (var thread in threads)
            {
                thread.Start();//尽量在相近的时间开始
                //Thread.Sleep(500);//TODO：如果Sleep，则可以快速完成（没有阻塞）
            }

            while (finishedCount < threadCount)
            {
                //等待线程结束（为测试避免线程锁死，这里不使用信号灯）
            }

            var costTime = (SystemTime.Now - dt1).TotalMilliseconds;
            Console.WriteLine($"== 运行结束，总耗时 {costTime:###,###}ms，平均 {apiTotalTime / threadCount:###,###}ms ==");

        }
    }
}
