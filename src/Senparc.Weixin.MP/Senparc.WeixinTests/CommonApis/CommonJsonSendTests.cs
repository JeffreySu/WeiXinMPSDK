using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.WeixinTests.vs2017.CommonApis
{
    [TestClass]
    public class CommonJsonSendTests : BaseTest
    {
        [TestMethod]
        public void SendThrowTest()
        {
            var fakeAccessToken = "22_q1nIlrqQ1brYqvp-8xPV3eIWWSJ9LU_qCxs3AaTMVjv74WyD1XDovWi4SkMX6xfykOCMoobaVpzu-lspCsCWIo5u6DGw31tS3ZmFw4q7wEDkXOmBaTtCeQuhlvhsalkenoRCKTLckTnVCkXbTWZlCJANQT1";//错误的AccessToken
            try
            {
                //Senparc.Weixin.Config.ThrownWhenJsonResultFaild = true;//抛出异常（默认就是true）
                var result = Senparc.Weixin.MP.AdvancedAPIs.UrlApi.ShortUrl(fakeAccessToken, "long2short", "https://sdk.weixin.senparc.com");
                Console.WriteLine("不应该到达这里");
                Assert.Fail();//不应该到达这里
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine();


            Senparc.Weixin.Config.ThrownWhenJsonResultFaild = false;//不抛出异常
            var result2 = Senparc.Weixin.MP.AdvancedAPIs.UrlApi.ShortUrl(fakeAccessToken, "long2short", "https://sdk.weixin.senparc.com");
            Console.WriteLine(result2);

            Senparc.Weixin.Config.ThrownWhenJsonResultFaild = true;//还原
        }
    }
}
