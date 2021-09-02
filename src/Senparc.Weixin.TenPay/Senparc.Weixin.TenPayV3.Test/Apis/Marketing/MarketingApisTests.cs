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

        CreateStockReturnJson createStockResult = null;

        /// <summary>
        /// 创建代金券接口批次测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_1.shtml
        /// </summary>
        [TestMethod()]
        public void CreateStockTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // var sp_billno = 

            // 代金券批次发放使用规则 发放10张优惠券 预算1000分 每个用户限领10张 开启防刷
            var stock_use_rule = new CreateStockRequsetData.Stock_Use_Rule(10, 1000, null, 10, false, true);

            // 代金券使用规则 指明可使用本批次代金券的商户号
            var coupon_use_rule = new CreateStockRequsetData.Coupon_Use_Rule(null, null, null, null, null, null, null, new string[] { TenPayV3Info.MchId });

            // TODO:流水号?这样是否有效?
            var out_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));

            var requestData = new CreateStockRequsetData("单元测试代金券批次", "用于单元测试", TenPayV3Info.MchId, new TenpayDateTime(DateTime.Now), new TenpayDateTime(DateTime.Now.AddDays(1)), stock_use_rule, null, coupon_use_rule, true, "NORMAL", out_request_no);

            var marketingApis = new MarketingApis();
            createStockResult = marketingApis.CreateStock(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 创建代金券接口批次结果：" + createStockResult.ToJson(true));

            Assert.IsNotNull(createStockResult);
            Assert.IsTrue(createStockResult.ResultCode.Success);
            Assert.IsTrue(createStockResult.VerifySignSuccess == true);//通过验证
        }


        /// <summary>
        /// 激活代金券批次接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_3.shtml
        /// </summary>
        [TestMethod()]
        public void StartStockTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建代金券批次 则创建新的代金券批次
            if (createStockResult is null)
            {
                CreateStockTest();
            }

            var requestData = new StartStockRequsetData(TenPayV3Info.MchId);

            var marketingApis = new MarketingApis();
            var result = marketingApis.StartStock(createStockResult.stock_id, requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 激活代金券批次接口结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 发放代金券批次接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_2.shtml
        /// </summary>
        [TestMethod()]
        public void DistributeStockTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建代金券批次 则创建新的代金券批次
            if (createStockResult is null)
            {
                CreateStockTest();
            }

            var out_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));

            var requestData = new DistributeStockRequsetData(createStockResult.stock_id, out_request_no, TenPayV3Info.AppId, TenPayV3Info.MchId);

            var marketingApis = new MarketingApis();
            var result = marketingApis.DistributeStock(openId, requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 发放代金券批次接口结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }


        /// <summary>
        /// 暂停发放代金券批次接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_13.shtml
        /// </summary>
        [TestMethod()]
        public void PauseStockTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建代金券批次 则创建新的代金券批次
            if (createStockResult is null)
            {
                CreateStockTest();
            }

            var marketingApis = new MarketingApis();
            var result = marketingApis.PauseStock(createStockResult.stock_id, TenPayV3Info.MchId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 暂停发放代金券批次接口结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 重启发放代金券批次接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_14.shtml
        /// </summary>
        [TestMethod()]
        public void RestartStockTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建代金券批次 则创建新的代金券批次
            if (createStockResult is null)
            {
                CreateStockTest();
            }

            var marketingApis = new MarketingApis();
            var result = marketingApis.RestartStock(createStockResult.stock_id, TenPayV3Info.MchId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 重启发放代金券批次接口结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 条件查询代金券批次接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_4.shtml
        /// </summary>
        [TestMethod()]
        public void QueryStocksTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryStocks(0, 10, TenPayV3Info.MchId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 条件查询代金券批次接口结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询代金券批次详情接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_5.shtml
        /// </summary>
        [TestMethod()]
        public void QueryStockTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建代金券批次 则创建新的代金券批次
            if (createStockResult is null)
            {
                CreateStockTest();
            }

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryStock(createStockResult.stock_id, TenPayV3Info.MchId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询代金券批次详情接口结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询代金券详情接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_6.shtml
        /// </summary>
        [TestMethod()]
        public void QueryStockTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建代金券批次 则创建新的代金券批次
            if (createStockResult is null)
            {
                CreateStockTest();
            }

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryStock(createStockResult.stock_id, TenPayV3Info.MchId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询代金券批次详情接口结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }
        #endregion
    }
}