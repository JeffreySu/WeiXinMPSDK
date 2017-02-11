using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.CacheTests
{
    [TestClass]
    public class CacheTests : CommonApiTest
    {
        private void LocalCacheDeadLockTestThreadFun()
        {
            UserApi.Info(base._appId, base._testOpenId);
        }

        [TestMethod]
        public void LocalCacheDeadLockTest()
        {
            //测试本地缓存死锁问题
            CacheStrategyFactory.RegisterObjectCacheStrategy(() => LocalObjectCacheStrategy.Instance);//Local

            List<Task> taskList = new List<Task>();
            var dt1 = DateTime.Now;
            for (int i = 0; i < 50; i++)
            {
                var lastTask = new Task(LocalCacheDeadLockTestThreadFun);
                lastTask.Start();
                taskList.Add(lastTask);
            }
            Task.WaitAll(taskList.ToArray());
            var dt2 = DateTime.Now;
            Console.Write("总耗时：{0}ms", (dt2 - dt1).TotalMilliseconds);
        }

        [TestMethod]
        public void LocalCacheDeadLockInGetTest()
        {
            //测试死锁发生
            //Task.Factory.StartNew(() =>
            //{
                var dt1 = DateTime.Now;
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("开始循环：{0}", i);
                    Senparc.Weixin.MP.AdvancedAPIs.UserApi.InfoAsync(base._appId, base._testOpenId);
                    Console.WriteLine("结束循环：{0}\r\n", i);
                }

                var dt2 = DateTime.Now;
                Console.Write("总耗时：{0}ms", (dt2 - dt1).TotalMilliseconds);

            //}).Wait();
        }

    }
}
