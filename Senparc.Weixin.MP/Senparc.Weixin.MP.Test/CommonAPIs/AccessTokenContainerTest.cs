using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.Test.CommonAPIs
{
    //已测试通过
    //[TestClass]
    public class AccessTokenContainerTest : CommonApiTest
    {
        [TestMethod]
        public void ContainerTest()
        {
            //注册
            AccessTokenContainer.Register(base._appId, base._appSecret);

            //获取Token完整结果（包括当前过期秒数）
            var tokenResult = AccessTokenContainer.GetTokenResult(base._appId);
            Assert.IsNotNull(tokenResult);

            //只获取Token字符串
            var token = AccessTokenContainer.GetToken(base._appId);
            Assert.AreEqual(tokenResult.access_token, token);

            //getNewToken
            {
                token = AccessTokenContainer.TryGetToken(base._appId, base._appSecret, false);
                Assert.AreEqual(tokenResult.access_token, token);

                token = AccessTokenContainer.TryGetToken(base._appId, base._appSecret, true);
                Assert.AreNotEqual(tokenResult.access_token, token);
            }

        }
    }
}
