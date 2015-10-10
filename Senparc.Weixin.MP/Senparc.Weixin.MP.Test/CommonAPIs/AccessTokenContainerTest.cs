﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Test.CommonAPIs
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
            var tokenResult = AccessTokenContainer.GetAccessTokenResult(base._appId);
            Assert.IsNotNull(tokenResult);
            Console.WriteLine(tokenResult.access_token);

            //只获取Token字符串
            var token = AccessTokenContainer.GetAccessToken(base._appId);
            Assert.AreEqual(tokenResult.access_token, token);
            Console.WriteLine(tokenResult.access_token);

            //getNewToken
            {
                token = AccessTokenContainer.TryGetAccessToken(base._appId, base._appSecret, false);
                Assert.AreEqual(tokenResult.access_token, token);
                Console.WriteLine(tokenResult.access_token);

                token = AccessTokenContainer.TryGetAccessToken(base._appId, base._appSecret, true);
                Assert.AreNotEqual(tokenResult.access_token, token);//如果微信服务器缓存，此处会相同
                Console.WriteLine(tokenResult.access_token);
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
    }
}
