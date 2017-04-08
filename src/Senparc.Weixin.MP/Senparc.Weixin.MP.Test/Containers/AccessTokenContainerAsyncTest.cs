﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.Containers.Tests
{
    //已测试通过
    [TestClass]
    public class AccessTokenContainerAsyncTest : CommonApiTest
    {
        [TestMethod]
        public void ContainerAsyncTest()
        {

            bool useRedis = true;

            if (useRedis)
            {
                var redisConfiguration = "localhost:6379";
                RedisManager.ConfigurationOption = redisConfiguration;
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);//Redis
            }
            
            //注册
            AccessTokenContainer.Register(base._appId, base._appSecret);

            //获取Token完整结果（包括当前过期秒数）
            DateTime dt1 = DateTime.Now;
            var tokenResult = AccessTokenContainer.GetAccessTokenResultAsync(base._appId).Result;
            DateTime dt2 = DateTime.Now;

            Assert.IsNotNull(tokenResult);
            Console.WriteLine(tokenResult.access_token);
            Console.WriteLine("耗时：{0}毫秒", (dt2 - dt1).TotalMilliseconds);

            //只获取Token字符串
            dt1 = DateTime.Now;
            var token = AccessTokenContainer.GetAccessTokenAsync(base._appId).Result;
            dt2 = DateTime.Now;
            Assert.AreEqual(tokenResult.access_token, token);
            Console.WriteLine(tokenResult.access_token);
            Console.WriteLine("耗时：{0}毫秒", (dt2 - dt1).TotalMilliseconds);

            //getNewToken
            {
                dt1 = DateTime.Now;
                token = AccessTokenContainer.TryGetAccessTokenAsync(base._appId, base._appSecret, false).Result;
                dt2 = DateTime.Now;
                Assert.AreEqual(tokenResult.access_token, token);
                Console.WriteLine(tokenResult.access_token);

                Console.WriteLine("强制重新获取AccessToken");
                dt1 = DateTime.Now;
                token = AccessTokenContainer.TryGetAccessTokenAsync(base._appId, base._appSecret, true).Result;
                dt2 = DateTime.Now;
                Assert.AreNotEqual(tokenResult.access_token, token);//如果微信服务器缓存，此处会相同
                Console.WriteLine(tokenResult.access_token);
                Console.WriteLine("耗时：{0}毫秒", (dt2 - dt1).TotalMilliseconds);
            }

            {
                tokenResult = AccessTokenContainer.GetAccessTokenResultAsync(base._appId).Result;
                Console.WriteLine("HashCode：{0}", tokenResult.GetHashCode());
                dt1 = DateTime.Now;
                var allItems = AccessTokenContainer.GetAllItems();
                dt2 = DateTime.Now;
                Assert.IsTrue(allItems.Count > 0);
                Assert.AreSame(tokenResult, allItems[0].AccessTokenResult);//证明缓存成功
                Console.WriteLine("All Items:{0}", allItems.Count);
                Console.WriteLine("HashCode：{0}", allItems[0].AccessTokenResult.GetHashCode());
                Console.WriteLine("耗时：{0}毫秒", (dt2 - dt1).TotalMilliseconds);
            }
        }

        [TestMethod]
        public void GetTokenResultTest()
        {
            //注册
            AccessTokenContainer.Register(base._appId, base._appSecret);

            //模拟多线程获取
            List<string> accessTokenList = new List<string>();
            int[] threads = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var accessTokenResultInit = AccessTokenContainer.GetAccessTokenResult(base._appId, false);//先获取一次
            Parallel.For(0, threads.Length, (i) =>
            {
                var accessTokenResult = AccessTokenContainer.GetAccessTokenResultAsync(base._appId, false).Result;
                accessTokenList.Add(accessTokenResult.access_token);//同时多次获取
                Console.WriteLine(accessTokenResult.access_token);
            });

            Assert.AreEqual(threads.Length, accessTokenList.Count());//只存在同一个Token，实际不会多次刷新
            Assert.AreEqual(1, accessTokenList.Distinct().Count());//只存在同一个Token，实际不会多次刷新
            Console.WriteLine(accessTokenList[0]);
        }

        [TestMethod]
        public void GetTokenResultAsyncTest()
        {
            //注册
            AccessTokenContainer.Register(base._appId, base._appSecret);
            //模拟多线程获取
            List<string> accessTokenList = new List<string>();
            int threadsCount = 5;//数字不易过大，否则超过微信允许同一个客户端同时并发数量，将可能看不到效果
            int round = 10;//测试多少轮

            //同步
            TimeSpan syncTime =new TimeSpan();
            var dt1 = DateTime.Now;
            for (int j = 0; j < round; j++)
            {
                Console.WriteLine("同步第{0}轮", j+1);
                Parallel.For(0, threadsCount, (i) =>
                {
                    var dts = DateTime.Now;
                    var accessTokenResult = AccessTokenContainer.GetAccessTokenResult(base._appId, true);
                    var dte = DateTime.Now;
                    accessTokenList.Add(accessTokenResult.access_token);//同时多次获取

                    Console.WriteLine("{0}同步线程耗时：{1}ms", i, (dte - dts).TotalMilliseconds);
                    syncTime = syncTime.Add((dte - dts));
                    //Console.WriteLine(accessTokenResult.access_token);
                });
            }
          
            var dt2 = DateTime.Now;
            Console.WriteLine("线程用时总和：{0}ms", syncTime.TotalMilliseconds);
            Console.WriteLine("{0}线程{1}轮同步时间：{2}ms", threadsCount, round,(dt2 - dt1).TotalMilliseconds);
            Console.WriteLine("=================");


            //异步
            TimeSpan asyncTime =new TimeSpan();
            var dt3 = DateTime.Now;
            for (int j = 0; j < round; j++)
            {
                Console.WriteLine("异步第{0}轮", j+1);
                Parallel.For(0, threadsCount, (i) =>
                {
                    var dts = DateTime.Now;
                    var accessTokenResult = AccessTokenContainer.GetAccessTokenResultAsync(base._appId, true).Result;
                    var dte = DateTime.Now;
                    accessTokenList.Add(accessTokenResult.access_token); //同时多次获取

                    Console.WriteLine("{0}异步线程耗时：{1}ms", i, (dte - dts).TotalMilliseconds);
                    asyncTime = syncTime.Add((dte - dts));
                    //Console.WriteLine(accessTokenResult.access_token);
                });
            }
            var dt4 = DateTime.Now;
            Console.WriteLine("线程用时总和：{0}ms", asyncTime.TotalMilliseconds);
            Console.WriteLine("{0}个线程{1}轮异步时间：{2}ms", threadsCount, round,(dt4 - dt3).TotalMilliseconds);

        }

        [TestMethod]
        public void GetFirstOrDefaultAppIdTest()
        {
            //此测试需要使用本地缓存进行测试

            var registeredAppId = base._appId;//已经注册的AppId

            var appId = AccessTokenContainer.GetFirstOrDefaultAppId();
            Assert.AreEqual(registeredAppId, appId);

            //注册多个AppId
            for (int i = 0; i < 100; i++)
            {
                AccessTokenContainer.Register("TestAppId_" + i, "TestAppSecret");
            }

            ////删除部分AppId
            //var collectionList = AccessTokenContainer.GetCollectionList();
            //for (int i = 10; i < 50; i++)
            //{
            //    collectionList.Remove("TestAppId_" + i);
            //}

            //appId = AccessTokenContainer.GetFirstOrDefaultAppId();
            //Assert.AreEqual(registeredAppId, appId);
        }
    }
}
