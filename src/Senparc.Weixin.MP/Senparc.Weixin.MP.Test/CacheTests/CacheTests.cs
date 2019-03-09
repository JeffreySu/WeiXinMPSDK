#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;
using System.Threading;
using Senparc.CO2NET.Cache;

namespace Senparc.Weixin.MP.Test.CacheTests
{
    [TestClass]
    public class CacheTests : CommonApiTest
    {
        private void LocalCacheDeadLockTestThreadFun()
        {
            var result = UserApi.Info(base._appId, base._testOpenId);
            Console.WriteLine(Thread.CurrentThread.GetHashCode() + "：" + Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }

        [TestMethod]
        public void LocalCacheDeadLockTest()
        {
            //测试本地缓存死锁问题：https://github.com/JeffreySu/WeiXinMPSDK/issues/402
           CacheStrategyFactory.RegisterObjectCacheStrategy(() => LocalObjectCacheStrategy.Instance);//Local

            List<Task> taskList = new List<Task>();
            var dt1 = SystemTime.Now;
            for (int i = 0; i < 200; i++)
            {
                var lastTask = new Task(LocalCacheDeadLockTestThreadFun);
                lastTask.Start();
                taskList.Add(lastTask);
            }
            Task.WaitAll(taskList.ToArray());
            var dt2 = SystemTime.Now;
            Console.Write("总耗时：{0}ms", (dt2 - dt1).TotalMilliseconds);
        }

        [TestMethod]
        public void LocalCacheDeadLockInGetTest()
        {
            //测试本地缓存死锁问题：https://github.com/JeffreySu/WeiXinMPSDK/issues/403
            CacheStrategyFactory.RegisterObjectCacheStrategy(() => LocalObjectCacheStrategy.Instance);//Local

            //测试死锁发生
            //Task.Factory.StartNew(() =>
            //{
            var dt1 = SystemTime.Now;
            for (int i = 0; i < 200; i++)
            {
                Console.WriteLine("开始循环：{0}", i);
                var result = Senparc.Weixin.MP.AdvancedAPIs.UserApi.InfoAsync(base._appId, base._testOpenId);
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result.Result));
            }

            var dt2 = SystemTime.Now;
            Console.Write("总耗时：{0}ms", (dt2 - dt1).TotalMilliseconds);

            //}).Wait();
        }
    }
}
