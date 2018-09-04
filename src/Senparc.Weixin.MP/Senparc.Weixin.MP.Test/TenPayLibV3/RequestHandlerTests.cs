using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.TenPay;
using System;
using System.Collections.Generic;
using System.Text;
using Senparc.Weixin.TenPay.V3;

namespace Senparc.Weixin.MP.Test.vs2017.TenPayLibV3
{
    [TestClass]
    public class RequestHandlerTests
    {


        [TestMethod]
        public void CreateSignTest()
        {
            //文档：https://pay.weixin.qq.com/wiki/doc/api/micropay.php?chapter=4_3

            var requestHandler = new RequestHandler();
            requestHandler.SetParameter("appid", "wxd930ea5d5a258f4f");
            requestHandler.SetParameter("body", "test");
            requestHandler.SetParameter("device_info", "1000");
            requestHandler.SetParameter("mch_id", "10000100");
            requestHandler.SetParameter("nonce_str", "ibuaiVcKdpRxkhJA");

            var key = "192006250b4c09247ec02edce69f6a2d";

            //MD5加密签名
            var md5Sign = requestHandler.CreateMd5Sign("key", key);
            Assert.AreEqual("9A0A8659F005D6984697E2CA0A9CF3B7", md5Sign);




            var requestHandler2 = new RequestHandler();
            requestHandler2.SetParameter("appid", "wxd930ea5d5a258f4f");
            requestHandler2.SetParameter("body", "test");
            requestHandler2.SetParameter("device_info", "1000");
            requestHandler2.SetParameter("mch_id", "10000100");
            requestHandler2.SetParameter("nonce_str", "ibuaiVcKdpRxkhJA");

            //HMAC-SHA256加密签名
            var sha256Sign = requestHandler2.CreateSha256Sign("key", key);
            Assert.AreEqual("6A9AE1657590FD6257D693A078E1C3E4BB6BA4DC30B23E0EE2496E54170DACD6", sha256Sign);





        }
    }
}
