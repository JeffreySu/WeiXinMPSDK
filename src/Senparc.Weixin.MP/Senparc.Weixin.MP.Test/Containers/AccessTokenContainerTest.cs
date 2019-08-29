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
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.CO2NET;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Cache.Redis;
using Senparc.CO2NET.Extensions;
//using Senparc.WeixinTests;

namespace Senparc.Weixin.MP.Test.Containers.Tests
{
    //已测试通过
    [TestClass]
    public class AccessTokenContainerTest : CommonApiTest
    {
        [TestMethod]
        public void ContainerTest()
        {
            MutipleCacheTestHelper.RunMutipleCache(() =>
            {
                //获取Token完整结果（包括当前过期秒数）
                var dt1 = SystemTime.Now;
                var tokenResult = AccessTokenContainer.GetAccessTokenResult(base._appId);
                var dt2 = SystemTime.Now;

                Assert.IsNotNull(tokenResult);
                Console.WriteLine(tokenResult.access_token);
                Console.WriteLine("耗时：{0}毫秒", (dt2 - dt1).TotalMilliseconds);

                if (base._useRedis)
                {
                    Thread.Sleep(2500);//等待缓存更新
                }

                //只获取Token字符串
                dt1 = SystemTime.Now;
                var token = AccessTokenContainer.GetAccessToken(base._appId);
                dt2 = SystemTime.Now;
                Assert.AreEqual(tokenResult.access_token, token);
                Console.WriteLine(tokenResult.access_token);
                Console.WriteLine("耗时：{0}毫秒", (dt2 - dt1).TotalMilliseconds);

                //getNewToken
                {
                    dt1 = SystemTime.Now;
                    token = AccessTokenContainer.TryGetAccessToken(base._appId, base._appSecret, false);
                    dt2 = SystemTime.Now;
                    Console.WriteLine(token);
                    Assert.AreEqual(tokenResult.access_token, token);

                    Console.WriteLine("强制重新获取AccessToken");
                    dt1 = SystemTime.Now;
                    token = AccessTokenContainer.TryGetAccessToken(base._appId, base._appSecret, true);
                    dt2 = SystemTime.Now;
                    Console.WriteLine(token);
                    Assert.AreNotEqual(tokenResult.access_token, token);//如果微信服务器缓存，此处会相同
                    Console.WriteLine("耗时：{0}毫秒", (dt2 - dt1).TotalMilliseconds);
                }

                {
                    tokenResult = AccessTokenContainer.GetAccessTokenResult(base._appId);
                    if (base._useRedis)
                    {
                        Thread.Sleep(2500);//等待缓存更新
                    }
                    Console.WriteLine("HashCode：{0}", tokenResult.GetHashCode());
                    dt1 = SystemTime.Now;
                    var allItems = AccessTokenContainer.GetAllItems();
                    dt2 = SystemTime.Now;
                    Assert.IsTrue(allItems.Count > 0);

                    //序列化
                    var d1 = StackExchangeRedisExtensions.Serialize(tokenResult);
                    var d2 = StackExchangeRedisExtensions.Serialize(allItems[0].AccessTokenResult);

                    Assert.AreEqual(String.Concat(d1), String.Concat(d2));//证明缓存成功
                    Console.WriteLine("All Items:{0}", allItems.Count);
                    Console.WriteLine("HashCode：{0}", allItems[0].AccessTokenResult.GetHashCode());
                    Console.WriteLine("耗时：{0}毫秒", (dt2 - dt1).TotalMilliseconds);
                }
            }, CacheType.Local, CacheType.Redis);
        }

        [TestMethod]
        public void GetTokenResultTest()
        {
            //注册
            AccessTokenContainer.Register(base._appId, base._appSecret);

            //模拟多线程获取
            List<string> accessTokenList = new List<string>();
            int[] treads = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Parallel.For(0, treads.Length, (i) =>
            {
                var accessTokenResult = AccessTokenContainer.GetAccessTokenResult(base._appId, false);
                accessTokenList.Add(accessTokenResult.access_token);//同时多次获取
            });

            Assert.AreEqual(treads.Length, accessTokenList.Count());//只存在同一个Token，实际不会多次刷新
            Assert.AreEqual(1, accessTokenList.Distinct().Count());//只存在同一个Token，实际不会多次刷新
            Console.WriteLine(accessTokenList[0]);
        }

        [TestMethod]
        public void GetFirstOrDefaultAppIdTest()
        {
            //此测试需要使用本地缓存进行测试

            var registeredAppId = base._appId;//已经注册的AppId

            var appId = AccessTokenContainer.GetFirstOrDefaultAppId(PlatformType.MP);
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


        [TestMethod]
        public void ReTryRegisterTest()
        {
            //第一步：注册（基类已完成）
            var accessTokenResult = AccessTokenContainer.GetAccessTokenResult(base._appId);
            Assert.IsNotNull(accessTokenResult);
            Assert.IsNotNull(accessTokenResult.access_token);
            Console.WriteLine(accessTokenResult.access_token);

            //第二步：清空注册信息
            var i = 0;


            //第三步：在不注册的情况下调用接口
            accessTokenResult = AccessTokenContainer.GetAccessTokenResult(base._appId, true);
            Assert.IsNotNull(accessTokenResult);
            Assert.IsNotNull(accessTokenResult.access_token);
            Console.WriteLine(accessTokenResult.access_token);
        }

        [TestMethod]
        public void RegisterToWeixinSettingTest()
        {
            var appId = Guid.NewGuid().ToString("n");
            var appSecret = Guid.NewGuid().ToString("n");
            var name = "公众号单元测试";
            AccessTokenContainer.Register(appId, appSecret, name);

            Assert.AreEqual(appId, Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinAppId);
            Assert.AreEqual(appSecret, Senparc.Weixin.Config.SenparcWeixinSetting.Items[name].WeixinAppSecret);
        }

        [TestMethod]
        public void TryGetAccessTokenTest()
        {
            //清除注册信息
            AccessTokenContainer.RemoveFromCache(base._appId);

            //直接调用
            var result = AccessTokenContainer.TryGetAccessToken(base._appId, base._appSecret, false);
            Assert.IsNotNull(result);
            Console.WriteLine(result.ToJson());
        }
    }
}
