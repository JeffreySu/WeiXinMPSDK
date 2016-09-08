using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.MP.TenPayLib;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //需要通过微信支付审核的账户才能成功
    [TestClass]
    public class TenPayTest : CommonApiTest
    {

        [TestMethod]
        public void NativePayTest()
        {
            var result = TenPay.NativePay("[appId]", "[timesTamp]", "[nonceStr]", "[productId]", "[sign]");
            Console.Write(result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DelivernotifyTest()
        {
            var result = TenPay.Delivernotify("[appId]", "[openId]", "[transId]", "[out_Trade_No]", "[deliver_TimesTamp]", "[deliver_Status]", "[deliver_Msg]", "[app_Signature]", "sha1");
            Console.Write(result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OrderqueryTest()
        {
            var result = TenPay.Orderquery("[appId]", "[package]", "[timesTamp]", "[app_Signature]", "[sign_Method]");
            Console.Write(result);
            Assert.IsNotNull(result);
        }
    }
}
