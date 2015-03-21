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
    public class TenPayV3Test : CommonApiTest
    {

        protected string data = @"<xml><spbill_create_ip><![CDATA[8.8.8.8]]></spbill_create_ip>
<notify_url><![CDATA[www.weiweihi.com]]></notify_url>
<out_trade_no><![CDATA[1833431771763549]]></out_trade_no>
<appid><![CDATA[wxbe855a981c34aa3f]]></appid>
<total_fee>1</total_fee>
<nonce_str><![CDATA[D554F7BB7BE44A7267068A7DF88DDD20]]></nonce_str>
<sign><![CDATA[4A3E3CF7205C9A319010ABE2A49F4F65]]></sign>
<openid><![CDATA[o3IHxjrPzMVZIJOgYMH1PyoTW_Tg]]></openid>
<mch_id><![CDATA[10017762]]></mch_id>
<trade_type><![CDATA[JSAPI]]></trade_type>
<body><![CDATA[test]]></body>
</xml>";

        [TestMethod]
        public void UnifiedorderTest()
        {
            var result = TenPayV3.Unifiedorder(data);
            Console.Write(result);
            Assert.IsNotNull(result);
        }
    }
}
