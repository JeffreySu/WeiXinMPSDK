using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using Senparc.Weixin.TenPayV3.Test;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Senparc.Weixin.TenPayV3.Apis.Tests
{
    [TestClass()]
    public class BasePayApisTests : BaseTenPayTest
    {
        [TestMethod()]
        public void JsAPiAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var price = 100;
            var name = "单元测试-" + DateTime.Now.ToString();
            var openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//换成测试人的 OpenId
            var sp_billno = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"),
                         TenPayV3Util.BuildRandomStr(6));

            JsApiRequestData jsApiRequestData = new(new TenpayDateTime(DateTime.Now), new JsApiRequestData.Amount() { currency = "CNY", total = price },
                    TenPayV3Info.MchId, name, TenPayV3Info.TenPayV3Notify, new JsApiRequestData.Payer() { openid = openId }, sp_billno, null, TenPayV3Info.AppId,
                    null, null, null, null);

            var result = BasePayApis.JsApiAsync(jsApiRequestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 预支付结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
        }

        [TestMethod()]
        public void CertificatesTest()
        {
            var result = BasePayApis.CertificatesAsync().GetAwaiter().GetResult();
            Assert.IsNotNull(result);
            Console.WriteLine(result.ToJson(true));
            Assert.IsTrue(result.ResultCode.Success);
        }
    }
}