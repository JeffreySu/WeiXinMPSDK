using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Apis.PayScore;
using Senparc.Weixin.TenPayV3.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using Senparc.Weixin.TenPayV3.Test;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Xml.Linq;

namespace Senparc.Weixin.TenPayV3.Apis.Tests
{
    [TestClass()]
    public class PayScoreApisTests : BaseTenPayTest
    {
        string openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//换成测试人的 OpenId

        // TODO: 测试前请阅读
        // https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter3_1_1.shtml

        #region 微信支付分(免确认模式)

        /// <summary>
        /// 创单结单合并测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_1.shtml
        /// </summary>
        [TestMethod()]
        public void CreateDirectCompleteServiceOrderAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 服务单号
            var out_order_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"),
                         TenPayV3Util.BuildRandomStr(6));
            //服务时间段
            var time_range = new CreateDirectCompleteServiceOrderRequestData.Time_Range(new TenpayDateTime(DateTime.Now), null, new TenpayDateTime(DateTime.Now.AddDays(1)), null);
            //付款项目列表
            var post_payments = new CreateDirectCompleteServiceOrderRequestData.Post_Payment[] { new CreateDirectCompleteServiceOrderRequestData.Post_Payment("SenparcUnitTest名称测试", 1, "单元测试1分", null) };
            //TODO:服务id获取 请阅读 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter3_1_1.shtml
            var serviceId = "{serviceId}";
            var requestData = new CreateDirectCompleteServiceOrderRequestData(out_order_no, TenPayV3Info.AppId, openId, serviceId, "SenparcUnitTest", post_payments, null, time_range, null, 1, null, null, null, null);
            PayScoreApis payScoreApis = new PayScoreApis();
            var result = payScoreApis.CreateDirectCompleteServiceOrderAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 创单结单合并测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        #endregion

        #region 微信支付分(免确认预授权模式)

        /// <summary>
        /// 商户预授权测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_2.shtml
        /// </summary>
        [TestMethod()]
        public void GivePermissionAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 服务单号
            var authorization_code = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"),
                         TenPayV3Util.BuildRandomStr(6));
            //TODO:服务id获取 请阅读 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter3_1_1.shtml
            var serviceId = "{serviceId}";
            var requestData = new GivePermissionRequestData(serviceId, TenPayV3Info.AppId, authorization_code, serviceId);
            PayScoreApis payScoreApis = new PayScoreApis();
            var result = payScoreApis.GivePermissionAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 商户预授权测试测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询用户授权记录（授权协议号）测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_3.shtml
        /// </summary>
        [TestMethod()]
        public void QueryPermissionByAuthorizationCodeAsyncTest()
        {
            //TODO:服务id获取 请阅读 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter3_1_1.shtml
            var serviceId = "{serviceId}";
            //TODO:输入已经授权的authorization_code
            var authorization_code = "{authorization_code}";

            PayScoreApis payScoreApis = new PayScoreApis();
            var result = payScoreApis.QueryPermissionByAuthorizationCodeAsync(serviceId, authorization_code).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询用户授权记录（授权协议号）测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 解除用户授权关系（授权协议号）测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_4.shtml
        /// </summary>
        [TestMethod()]
        public void TerminatePermissionByAuthorizationCodeAsyncTest()
        {
            //TODO:服务id获取 请阅读 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter3_1_1.shtml
            var serviceId = "{serviceId}";
            //TODO:输入已经授权的authorization_code
            var authorization_code = "{authorization_code}";
            var requestData = new TerminatePermissionByAuthorizationCodeRequestData(serviceId, authorization_code, "Senparc接口单元测试");


            PayScoreApis payScoreApis = new PayScoreApis();
            var result = payScoreApis.TerminatePermissionByAuthorizationCodeAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 解除用户授权关系（授权协议号）测试：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询用户授权记录（openid）测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_5.shtml
        /// </summary>
        [TestMethod()]
        public void QueryPermissionByOpenidAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            //TODO:服务id获取 请阅读 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter3_1_1.shtml
            var serviceId = "{serviceId}";

            PayScoreApis payScoreApis = new PayScoreApis();
            var result = payScoreApis.QueryPermissionByOpenidAsync(serviceId, TenPayV3Info.AppId,openId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询用户授权记录（openid）测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 解除用户授权关系（openid）测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_6.shtml
        /// </summary>
        [TestMethod()]
        public void TerminatePermissionByOpenidAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            //TODO:服务id获取 请阅读 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter3_1_1.shtml
            var serviceId = "{serviceId}";
            var requestData = new TerminatePermissionByOpenidRequestData(openId, serviceId, TenPayV3Info.AppId, "Senparc接口单元测试");

            PayScoreApis payScoreApis = new PayScoreApis();
            var result = payScoreApis.TerminatePermissionByOpenidAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 解除用户授权关系（openid）测试：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }
        #endregion
    }
}