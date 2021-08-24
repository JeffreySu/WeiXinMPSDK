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
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Xml.Linq;

namespace Senparc.Weixin.TenPayV3.Apis.Tests
{
    [TestClass()]
    public class BasePayApisTests : BaseTenPayTest
    {

        [TestMethod()]
        public void CertificatesTest()
        {
            BasePayApis basePayApis = new BasePayApis();
            var certs = basePayApis.CertificatesAsync().GetAwaiter().GetResult();
            Assert.IsNotNull(certs);
            Console.WriteLine(certs.ToJson(true));
            Assert.IsTrue(certs.ResultCode.Success);
            Assert.IsNull(certs.VerifySignSuccess);//不参与验证

            Console.WriteLine();

            var tenpayV3Setting = Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
            var cert = certs.data.First();
            var pubKey = ApiSecurityHelper.AesGcmDecryptCiphertext(tenpayV3Setting.TenPayV3_APIv3Key, cert.encrypt_certificate.nonce,
                     cert.encrypt_certificate.associated_data, cert.encrypt_certificate.ciphertext);
            Console.WriteLine(pubKey);
            Assert.IsNotNull(pubKey);
        }

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

            JsApiRequestData jsApiRequestData = new(new TenpayDateTime(DateTime.Now.AddHours(1)), new JsApiRequestData.Amount() { currency = "CNY", total = price },
                    TenPayV3Info.MchId, name, TenPayV3Info.TenPayV3Notify, new JsApiRequestData.Payer() { openid = openId }, sp_billno, null, TenPayV3Info.AppId,
                    null, null, null, null);

            BasePayApis basePayApis = new BasePayApis();
            var result = basePayApis.JsApiAsync(jsApiRequestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 预支付结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证

        }

    }
}