using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.Complaint;
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
    //TODO: 测试前请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter6_2_2.shtml
    [TestClass()]
    public class ComplaintApisTests : BaseTenPayTest
    {
        string openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//"olPjZjiGtsfaqOhUbOd2puy1wVvc";//换成测试人的 OpenId

        #region 消费者投诉2.0接口

        /// <summary>
        /// 查询投诉单列表测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_11.shtml
        /// </summary>
        [TestMethod()]
        public void QueryComplaintsAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var complaintApis = new ComplaintApis();
            var result = complaintApis.QueryComplaintsAsync(new TenpayDateTime(DateTime.Parse("2021-9-30")), new TenpayDateTime(DateTime.Now), TenPayV3Info.MchId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询投诉单列表测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询投诉协商历史接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_12.shtml
        /// </summary>
        [TestMethod()]
        public void QueryNegotiationHistorysAsyncTest()
        {
            // 此处输入投诉id
            var complaint_id = "{complaint_id}";

            var complaintApis = new ComplaintApis();
            var result = complaintApis.QueryNegotiationHistorysAsync(complaint_id).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询投诉协商历史接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 创建投诉通知回调地址接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_2.shtml
        /// </summary>
        [TestMethod()]
        public void CreateComplaintNotifyUrlAsyncTest()
        {
            // 此处输入notify_url
            var notify_url = "{notify_url}";
            var requestData = new CreateComplaintNotifyUrlRequestData(notify_url);

            var complaintApis = new ComplaintApis();
            var result = complaintApis.CreateComplaintNotifyUrlAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 创建投诉通知回调地址接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询投诉通知回调地址接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_3.shtml
        /// </summary>
        [TestMethod()]
        public void QueryComplaintNotifyUrlAsyncTest()
        {
            var complaintApis = new ComplaintApis();
            var result = complaintApis.QueryComplaintNotifyUrlAsync().GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询投诉通知回调地址接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 更新投诉通知回调地址接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_4.shtml
        /// </summary>
        [TestMethod()]
        public void ModifyComplaintNotifyUrlAsyncTest()
        {
            // 此处输入notify_url
            var notify_url = "{notify_url}";
            var requestData = new ModifyComplaintNotifyUrlRequestData(notify_url);

            var complaintApis = new ComplaintApis();
            var result = complaintApis.ModifyComplaintNotifyUrlAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 更新投诉通知回调地址接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 删除投诉通知回调地址接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_5.shtml
        /// </summary>
        [TestMethod()]
        public void DeleteComplaintNotifyUrlAsyncTest()
        {
            // 此处输入notify_url
            var complaintApis = new ComplaintApis();
            var result = complaintApis.DeleteComplaintNotifyUrlAsync().GetAwaiter().GetResult();

            // TODO: 此处唯一使用了DELETE动词 需要重点测试
            Console.WriteLine("微信支付 V3 删除投诉通知回调地址接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 提交回复接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_14.shtml
        /// </summary>
        [TestMethod()]
        public void ResponseAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 此处输入投诉id
            var complaint_id = "{complaint_id}";
            var requestData = new ResponseRequestData(complaint_id, TenPayV3Info.MchId, "Senparc提交回复单元测试", null);

            var complaintApis = new ComplaintApis();
            var result = complaintApis.ResponseAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 提交回复接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 反馈处理完成接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_15.shtml
        /// </summary>
        [TestMethod()]
        public void CompleteComplaintAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 此处输入投诉id
            var complaint_id = "{complaint_id}";
            var requestData = new CompleteComplaintRequestData(complaint_id, TenPayV3Info.MchId);

            var complaintApis = new ComplaintApis();
            var result = complaintApis.CompleteComplaintAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 反馈处理完成接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        #endregion
    }
}