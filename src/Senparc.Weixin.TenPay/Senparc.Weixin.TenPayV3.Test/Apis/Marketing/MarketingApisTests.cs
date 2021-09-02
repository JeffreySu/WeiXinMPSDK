using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Apis.Marketing;
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
    public class MarketingApisTests : BaseTenPayTest
    {
        string openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//换成测试人的 OpenId

        #region 代金券接口

        /// <summary>
        /// 创建代金券接口批次测试
        /// </summary>
        [TestMethod()]
        public void CreateStockTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // var sp_billno = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"),
            //            TenPayV3Util.BuildRandomStr(6));

            // 代金券批次发放使用规则 发放10张优惠券 预算1000分 每个用户限领10张 开启防刷
            var stock_use_rule = new CreateStockRequsetData.Stock_Use_Rule(10, 1000, null, 10, false, true);

            // 代金券使用规则 指明可使用本批次代金券的商户号
            var coupon_use_rule = new CreateStockRequsetData.Coupon_Use_Rule(null, null, null, null, null, null, null, new string[] { TenPayV3Info.MchId });

            // TODO:流水号?
            var out_request_no = "商户id+日期+流水号";

            var requestData = new CreateStockRequsetData("单元测试代金券批次", "用于单元测试", TenPayV3Info.MchId, new TenpayDateTime(DateTime.Now), new TenpayDateTime(DateTime.Now.AddDays(1)), stock_use_rule, null, coupon_use_rule, true, "NORMAL", out_request_no);

            var marketingApis = new MarketingApis();
            var result = marketingApis.CreateStock(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 创建代金券接口批次结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        #endregion
    }
}