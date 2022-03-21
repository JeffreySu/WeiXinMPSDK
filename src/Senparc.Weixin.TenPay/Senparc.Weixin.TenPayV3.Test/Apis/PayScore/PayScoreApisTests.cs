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
            var result = payScoreApis.QueryPermissionByOpenidAsync(serviceId, TenPayV3Info.AppId, openId).GetAwaiter().GetResult();

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

        #region 微信支付分(公共API)

        CreateServiceOrderReturnJson createServiceOrderResult = null;

        /// <summary>
        /// 创建支付分订单测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_14.shtml
        /// </summary>
        public void CreateServiceOrderAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 服务单号
            var out_order_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"),
                         TenPayV3Util.BuildRandomStr(6));
            //服务时间段
            var time_range = new CreateServiceOrderRequestData.Time_Range(new TenpayDateTime(DateTime.Now), null, new TenpayDateTime(DateTime.Now.AddDays(1)), null);
            //付款项目列表
            var post_payments = new CreateServiceOrderRequestData.Post_Payment[] { new CreateServiceOrderRequestData.Post_Payment("SenparcUnitTest名称测试", 1, "单元测试1分", null) };
            //TODO:服务id获取 请阅读 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter3_1_1.shtml
            var serviceId = "{serviceId}";
            //订单风险金
            var risk_fund = new CreateServiceOrderRequestData.Risk_Fund("DEPOSIT", 1, "Senparc单元测试订单风险金");
            var requestData = new CreateServiceOrderRequestData(out_order_no, TenPayV3Info.AppId, serviceId, "SenparcUnitTest", post_payments, null, time_range, null, risk_fund, null, null, openId, null);
            PayScoreApis payScoreApis = new PayScoreApis();
            var createServiceOrderResult = payScoreApis.CreateServiceOrderAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 创建支付分订单测试：" + createServiceOrderResult.ToJson(true));

            Assert.IsNotNull(createServiceOrderResult);
            Assert.IsTrue(createServiceOrderResult.ResultCode.Success);
            Assert.IsTrue(createServiceOrderResult.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询支付分订单测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_15.shtml
        /// </summary>
        public void QueryServiceOrderAsyncTest()
        {
            //如果未创建支付分订单 则创建支付分订单
            if (createServiceOrderResult is null)
            {
                CreateServiceOrderAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            PayScoreApis payScoreApis = new PayScoreApis();

            try
            {
                // out_order_no query_id都不传入的情况
                _ = payScoreApis.QueryServiceOrderAsync(null, null, createServiceOrderResult.service_id, TenPayV3Info.AppId).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TenpayApiRequestException));
                Console.WriteLine(ex.Message);
                Assert.IsTrue(ex.Message.Contains("不允许都填写或都不填写"));
            }

            try
            {
                // out_order_no query_id一起传入情况
                _ = payScoreApis.QueryServiceOrderAsync(createServiceOrderResult.out_order_no, "queryId", createServiceOrderResult.service_id, TenPayV3Info.AppId).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TenpayApiRequestException));
                Console.WriteLine(ex.Message);
                Assert.IsTrue(ex.Message.Contains("不允许都填写或都不填写"));
            }

            var result = payScoreApis.QueryServiceOrderAsync(createServiceOrderResult.out_order_no, null, createServiceOrderResult.service_id, TenPayV3Info.AppId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询支付分订单测试：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 修改订单金额测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_17.shtml
        /// </summary>
        public void ModifyServiceOrderAsyncTest()
        {
            //如果未创建支付分订单 则创建支付分订单
            if (createServiceOrderResult is null)
            {
                CreateServiceOrderAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            //付款项目列表
            var post_payments = new ModifyServiceOrderRequestData.Post_Payment[] { new ModifyServiceOrderRequestData.Post_Payment("SenparcUnitTest名称测试", 1, "单元测试1分", null) };
            var requestData = new ModifyServiceOrderRequestData(createServiceOrderResult.out_order_no, TenPayV3Info.AppId, createServiceOrderResult.service_id, post_payments, null, 1, "SenparcUnitTest");

            PayScoreApis payScoreApis = new PayScoreApis();
            var result = payScoreApis.ModifyServiceOrderAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 修改订单金额测试：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 取消支付分订单测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_17.shtml
        /// </summary>
        public void CancelServiceOrderAsyncTest()
        {
            //如果未创建支付分订单 则创建支付分订单
            if (createServiceOrderResult is null)
            {
                CreateServiceOrderAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var requestData = new CancelServiceOrderRequestData(createServiceOrderResult.out_order_no, TenPayV3Info.AppId, createServiceOrderResult.service_id, "SenparcUnitTest");

            PayScoreApis payScoreApis = new PayScoreApis();
            var result = payScoreApis.CancelServiceOrderAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 取消支付分订单测试：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 完结支付分订单测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_18.shtml
        /// </summary>
        public void CompleteServiceOrderAsyncTest()
        {
            //如果未创建支付分订单 则创建支付分订单
            if (createServiceOrderResult is null)
            {
                CreateServiceOrderAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            //服务时间段
            var time_range = new CompleteServiceOrderRequestData.Time_Range(new TenpayDateTime(DateTime.Now), null, new TenpayDateTime(DateTime.Now.AddDays(1)), null);
            //付款项目列表
            var post_payments = new CompleteServiceOrderRequestData.Post_Payment[] { new CompleteServiceOrderRequestData.Post_Payment("SenparcUnitTest名称测试", 1, "单元测试1分", null) };
            var requestData = new CompleteServiceOrderRequestData(createServiceOrderResult.out_order_no, TenPayV3Info.AppId, createServiceOrderResult.service_id, post_payments, null, 1, time_range, null, null, null);

            PayScoreApis payScoreApis = new PayScoreApis();
            var result = payScoreApis.CompleteServiceOrderAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 完结支付分订单测试：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 商户发起催收扣款测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_19.shtml
        /// </summary>
        public void PayServiceOrderAsyncTest()
        {
            //如果未创建支付分订单 则创建支付分订单
            if (createServiceOrderResult is null)
            {
                CreateServiceOrderAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var requestData = new PayServiceOrderRequestData(createServiceOrderResult.out_order_no, TenPayV3Info.AppId, createServiceOrderResult.service_id);

            PayScoreApis payScoreApis = new PayScoreApis();
            var result = payScoreApis.PayServiceOrderAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 商户发起催收扣款测试：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 同步服务订单信息测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_20.shtml
        /// </summary>
        public void SyncPayServiceOrderAsyncTest()
        {
            //如果未创建支付分订单 则创建支付分订单
            if (createServiceOrderResult is null)
            {
                CreateServiceOrderAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var requestData = new SyncPayServiceOrderRequestData(createServiceOrderResult.out_order_no, TenPayV3Info.AppId, createServiceOrderResult.service_id, "Order_Paid", null);

            PayScoreApis payScoreApis = new PayScoreApis();


            try
            {
                // 场景类型为Order_Paid时，为必填项。
                _ = payScoreApis.QueryServiceOrderAsync(null, null, createServiceOrderResult.service_id, TenPayV3Info.AppId).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TenpayApiRequestException));
                Console.WriteLine(ex.Message);
                Assert.IsTrue(ex.Message.Contains("必填"));
            }

            //修正参数
            requestData.detail = new(new TenpayDateTime(DateTime.Now));

            var result = payScoreApis.SyncPayServiceOrderAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 同步服务订单信息测试：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        #endregion

        #region 支付即服务
        //TODO: 测试前参考 https://pay.weixin.qq.com/index.php/public/product/detail?pid=109

        /// <summary>
        /// 服务人员注册测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_1.shtml
        /// </summary>
        public void RegisterGuideAsyncTest()
        {
            //TODO: 完善信息
            //企业ID
            var corpid = "{corpid}";
            //门店ID
            var store_id = 123;
            //企业微信的员工ID
            var userid = "{userid}";
            //企业微信的员工姓名
            var name = "{name}";
            //手机号码
            var mobile = "{mobile}";
            //员工个人二维码
            var qr_code = "{qr_code}";
            //头像URL
            var avatar = "{avatar}";
            //群二维码URL
            var group_qrcode = "{	group_qrcode}";
            var requestData = new RegisterGuideRequestData(corpid, store_id, userid, name, mobile, qr_code, avatar, group_qrcode);
            PayScoreApis payScoreApis = new PayScoreApis();
            var createServiceOrderResult = payScoreApis.RegisterGuideAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 创建支付分订单测试：" + createServiceOrderResult.ToJson(true));

            Assert.IsNotNull(createServiceOrderResult);
            Assert.IsTrue(createServiceOrderResult.ResultCode.Success);
            Assert.IsTrue(createServiceOrderResult.VerifySignSuccess == true);//通过验证
        }

        #endregion
    }
}