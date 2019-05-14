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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.TenPayLibV3;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.TenPay.V3;

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
            //这是已经过期的旧方法，新方法请参考相同方法名的重写方法
            var result = TenPayV3.Unifiedorder(data);
            Console.Write(result);
            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void GetSignKeyTest()
        {
            //沙箱验证流程参考：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=23_1
            //关注微信公众号：WXPayAssist

            Senparc.Weixin.Config.UseSandBoxPay = true;

            var nonceStr = TenPayV3Util.GetNoncestr();
            var dataInfo = new TenPayV3GetSignKeyRequestData(base._mchId, nonceStr, base._tenPayKey);
            var result = TenPayV3.GetSignKey(dataInfo);

            Console.WriteLine(result.ResultXml);
            Assert.IsTrue(result.IsReturnCodeSuccess());

            //继续测试一个订单接口
            MicroPayTest(result.sandbox_signkey, nonceStr);
        }

        private void MicroPayTest(string sandBoxKey, string nonceStr)
        {
            var deviceInfo = "Senparc POS 1";
            var body = "Senparc.Weixin SDK";
            var totalFee = 1;//金额必须符合用例要求
            var outTradeNo = SystemTime.Now.Ticks.ToString();

            string detail =
            @"{
    ""cost_price"": 1137600, 
    ""receipt_id"": ""wx123"", 
    ""goods_detail"": [
        {
            ""goods_id"": ""商品编码"",
            ""wxpay_goods_id"": ""1001"",
            ""goods_name"": """",
            ""quantity"": 1,
            ""price"": 528800
        }, 
        {
            ""goods_id"": ""商品编码"", 
            ""wxpay_goods_id"": ""1002"", 
            ""goods_name"": ""iPhone6s 32G"", 
            ""quantity"": 1, 
            ""price"": 608800
        }
    ]
}".Replace("\r", "").Replace("\n", "");

            var dataInfo = new TenPayV3MicroPayRequestData(base._appId, base._mchId, sandBoxKey,
                nonceStr, deviceInfo, body, detail, null, outTradeNo, totalFee.ToString(), "CNY", "127.0.0.1",
                null, null);

            var result = TenPayV3.MicroPay(dataInfo);

            Console.WriteLine(result.ResultXml);
            Assert.IsTrue(result.IsReturnCodeSuccess());

            //订单查询
            OrderQueryTest(sandBoxKey, nonceStr, result.transaction_id, result.out_trade_no);
        }

        private void OrderQueryTest(string sandBoxKey, string nonceStr,string transactionId,string outTradeNo)
        {
            var dataInfo = new TenPayV3OrderQueryRequestData(base._appId, base._mchId, transactionId, nonceStr, outTradeNo, sandBoxKey);
            var result = TenPayV3.OrderQuery(dataInfo);


            Console.WriteLine(result.ResultXml);
            Assert.IsTrue(result.IsReturnCodeSuccess());

            //下载对账单
            DownloadBillTest(sandBoxKey, nonceStr);
        }

        private void DownloadBillTest(string sandBoxKey, string nonceStr)
        {
            var dataInfo = new TenPayV3DownloadBillRequestData(base._appId, base._mchId, nonceStr, "Senparc POS 1",
                SystemTime.Now.ToString("yyyyMMdd"), "ALL", sandBoxKey);


            var result = TenPayV3.DownloadBill(dataInfo);

            Assert.IsTrue(!string.IsNullOrEmpty(result));

            Console.WriteLine(result);
        }
    }
}
