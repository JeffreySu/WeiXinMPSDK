using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Test.CommonAPIs.Tests
{
    //已测试通过
    [TestClass]
    public class AccessTokenContainerTest : CommonApiTest
    {
        [TestMethod]
        public void ContainerTest()
        {
            //注册
            AccessTokenContainer.Register(base._appId, base._appSecret);

            //获取Token完整结果（包括当前过期秒数）
            DateTime dt1 = DateTime.Now;
            var tokenResult = AccessTokenContainer.GetAccessTokenResult(base._appId);
            DateTime dt2 = DateTime.Now;

            Assert.IsNotNull(tokenResult);
            Console.WriteLine(tokenResult.access_token);
            Console.WriteLine("耗时：{0}毫秒", (dt2 - dt1).TotalMilliseconds);

            //只获取Token字符串
            dt1 = DateTime.Now;
            var token = AccessTokenContainer.GetAccessToken(base._appId);
            dt2 = DateTime.Now;
            Assert.AreEqual(tokenResult.access_token, token);
            Console.WriteLine(tokenResult.access_token);
            Console.WriteLine("耗时：{0}毫秒", (dt2 - dt1).TotalMilliseconds);

            //getNewToken
            {
                dt1 = DateTime.Now;
                token = AccessTokenContainer.TryGetAccessToken(base._appId, base._appSecret, false);
                dt2 = DateTime.Now;
                Assert.AreEqual(tokenResult.access_token, token);
                Console.WriteLine(tokenResult.access_token);

                Console.WriteLine("强制重新获取AccessToken");
                dt1 = DateTime.Now;
                token = AccessTokenContainer.TryGetAccessToken(base._appId, base._appSecret, true);
                dt2 = DateTime.Now;
                Assert.AreNotEqual(tokenResult.access_token, token);//如果微信服务器缓存，此处会相同
                Console.WriteLine(tokenResult.access_token);
                Console.WriteLine("耗时：{0}毫秒", (dt2 - dt1).TotalMilliseconds);
            }

            {
                tokenResult = AccessTokenContainer.GetAccessTokenResult(base._appId);
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

            var appId = AccessTokenContainer.GetFirstOrDefaultAppId();
            Assert.AreEqual(registeredAppId, appId);

            //注册多个AppId
            for (int i = 0; i < 100; i++)
            {
                AccessTokenContainer.Register("TestAppId_"+i,"TestAppSecret");
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
