using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BusinessCircle;
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
    public class BusinessCircleApisTests : BaseTenPayTest
    {
        string openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//"olPjZjiGtsfaqOhUbOd2puy1wVvc";//换成测试人的 OpenId

        #region 智慧商圈接口

        /// <summary>
        /// 商圈积分同步接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_6_2.shtml
        /// </summary>
        [TestMethod()]
        public void CreateBusifavorStockAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var transaction_id = "{transaction_id}";// 输入微信支付推送的商圈内交易通知里携带的微信订单号
            var requestData = new NotifyBusinessCirclePointsRequestData(transaction_id, TenPayV3Info.AppId, openId, true, 1, new TenpayDateTime(DateTime.Now), null, null);

            var businessCircleApis = new BusinessCircleApis();

            var result = businessCircleApis.NotifyBusinessCirclePointsAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 商圈积分同步接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 商圈积分授权查询测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_6_4.shtml
        /// </summary>
        [TestMethod()]
        public void QueryProfitsharingAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var businessCircleApis = new BusinessCircleApis();
            var result = businessCircleApis.QueryUserAuthorizationAsync(TenPayV3Info.AppId, openId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 商圈积分授权查询测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        #endregion
    }
}