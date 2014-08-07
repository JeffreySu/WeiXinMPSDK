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

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //需要通过微信支付审核的账户才能成功
    [TestClass]
    public class WeixinPayTest : CommonApiTest
    {

        [TestMethod]
        public void NativePayTest()
        {
            var result = WeixinPay.NativePay("[appid]", "[timestamp]", "[noncestr]", "[productId]", "[sign]");
            Console.Write(result);
            //Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DelivernotifyTest()
        {
            var result = WeixinPay.Delivernotify("[appid]", "[openId]", "[transid]", "[out_trade_no]", "[deliver_timestamp]", "[deliver_status]", "[deliver_msg]", "[app_signature]", "sha1");
            Console.Write(result);
            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void OrderqueryTest()
        //{
        //    var result = WeixinPay.Orderquery("123123", DateTime.Now, "wx402b1453e023f85e", "1", "123123", "bank_type=WX&body=%e6%94%af%e4%bb%98%e6%b5%8b%e8%af%95&fee_type=1&input_charset=GBK&notify_url=http%3a%2f%2fweixin.qq.com&out_trade_no=7240b65810859cbf2a8d9f76a638c0a3&partner=1900000109&spbill_create_ip=196.168.1.1&total_fee=1&sign=F9E09E522CBFDD6F682D7C22AC1F90A4");
        //    Console.Write(result);
        //    Assert.IsNotNull(result);
        //}
    }
}
