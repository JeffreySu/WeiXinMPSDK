using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;
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

        [TestMethod]
        public void GetCurrentSelfMenuInfoTest()
        {
            var result = CommonApi.GetCurrentSelfMenuInfo("75_eGhUgdbNzwM8IcroXssnKN4sP9QSWPcMJUJe5Ka3UTTisGP_UorAZYLb1UCZyQtdsp798EwLdbURX-cfdbacKB-IuDJ_QHMnjTHYANlch3h8_YRAw_oyfZ4mwf8LODdACAUXM");

            Console.WriteLine(result);
            if (result.ErrorCodeValue == 0)
            {
                Assert.IsTrue(true);
            }
            Assert.IsTrue(false);
        }
    }
}
