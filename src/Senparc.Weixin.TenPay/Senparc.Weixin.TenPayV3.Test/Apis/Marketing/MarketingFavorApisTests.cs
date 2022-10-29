using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis.Marketing;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Tests
{
    public partial class MarketingApisTests
    {
        #region 代金券接口

        CreateStockReturnJson createStockResult = null;
        DistributeStockReturnJson distributeStockResult = null;

        /// <summary>
        /// 创建代金券接口批次测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_1.shtml
        /// </summary>
        [TestMethod()]
        public void CreateStockAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 代金券批次发放使用规则 发放10张优惠券 预算1000分 每个用户限领10张 开启防刷
            var stock_use_rule = new CreateStockRequsetData.Stock_Use_Rule(2, 4, null, 2, false, true);

            // 代金券使用规则 指明可使用本批次代金券的商户号
            var coupon_use_rule = new CreateStockRequsetData.Coupon_Use_Rule(null, null, null, null, null, null, null, new string[] { TenPayV3Info.MchId/*"1610625015"*/ });

            // TODO:流水号?这样是否有效?
            var out_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));

            var requestData = new CreateStockRequsetData("单元测试代金券批次", "用于单元测试", TenPayV3Info.MchId, new TenpayDateTime(DateTime.Now.AddMinutes(1)), new TenpayDateTime(DateTime.Now.AddMinutes(30)), stock_use_rule, null, coupon_use_rule, true, "NORMAL", out_request_no);

            
            /* 提示：
             * 使用此功能必须在后台【产品中心】开通【预充值代金券】功能！
             */

            var marketingApis = new MarketingApis();

            try
            {
                // stock_type = "NORMAL" 的情况
                createStockResult = marketingApis.CreateStockAsync(requestData).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TenpayApiRequestException));
                Console.WriteLine(ex.Message);
                Assert.IsTrue(ex.Message.Contains("必填"));
            }

            try
            {
                //修改参数
                requestData.coupon_use_rule.fixed_normal_coupon = new CreateStockRequsetData.Coupon_Use_Rule.Fixed_Normal_Coupon(6, 1);
                createStockResult = marketingApis.CreateStockAsync(requestData).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TenpayApiRequestException));
                Console.WriteLine(ex.Message);
                Assert.IsTrue(ex.Message.Contains("必须等于"));
            }

            try
            {
                //修改参数
                requestData.coupon_use_rule.fixed_normal_coupon.coupon_amount = 2;// coupon_amount = max_coupons * max_coupons_per_user
                createStockResult = marketingApis.CreateStockAsync(requestData).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TenpayApiRequestException));
                Console.WriteLine(ex.Message);
                Assert.IsTrue(ex.Message.Contains("必须小于等于"));
            }

            //修改参数
            requestData.coupon_use_rule.fixed_normal_coupon.transaction_minimum = 2;// coupon_amount = max_coupons * max_coupons_per_user
            createStockResult = marketingApis.CreateStockAsync(requestData).GetAwaiter().GetResult();


            Assert.IsTrue(createStockResult.ResultCode.Additional.Contains("可用商户不符合规则，请检查"));

            //修改参数
            requestData.no_cash = false;//只有当参数为 false 时，可以使用本商户，否则只能使用其他商户
            createStockResult = marketingApis.CreateStockAsync(requestData).GetAwaiter().GetResult();


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
        public void StartStockAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建代金券批次 则创建新的代金券批次
            if (createStockResult is null)
            {
                CreateStockAsyncTest();
            }

            var requestData = new StartStockRequsetData(TenPayV3Info.MchId);

            var marketingApis = new MarketingApis();
            var result = marketingApis.StartStockAsync(createStockResult.stock_id, requestData).GetAwaiter().GetResult();

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
        public void DistributeStockAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建代金券批次 则创建新的代金券批次 并且激活
            if (createStockResult is null)
            {
                StartStockAsyncTest();
            }

            var out_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));

            var requestData = new DistributeStockRequsetData(createStockResult.stock_id, out_request_no, TenPayV3Info.AppId, TenPayV3Info.MchId);

            var marketingApis = new MarketingApis();
            distributeStockResult = marketingApis.DistributeStockAsync(openId, requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 发放代金券批次接口结果：" + distributeStockResult.ToJson(true));

            Assert.IsNotNull(distributeStockResult);
            Assert.IsTrue(distributeStockResult.ResultCode.Success);
            Assert.IsTrue(distributeStockResult.VerifySignSuccess == true);//通过验证
        }


        /// <summary>
        /// 暂停发放代金券批次接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_13.shtml
        /// </summary>
        [TestMethod()]
        public void PauseStockAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建代金券批次 则创建新的代金券批次 并且激活
            if (createStockResult is null)
            {
                StartStockAsyncTest();
            }

            var marketingApis = new MarketingApis();
            var result = marketingApis.PauseStockAsync(createStockResult.stock_id, TenPayV3Info.MchId).GetAwaiter().GetResult();

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
        public void RestartStockAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建代金券批次 则创建新的代金券批次 激活 然后暂停发放
            if (createStockResult is null)
            {
                PauseStockAsyncTest();
            }

            var marketingApis = new MarketingApis();
            var result = marketingApis.RestartStockAsync(createStockResult.stock_id, TenPayV3Info.MchId).GetAwaiter().GetResult();

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
        public void QueryStocksAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryStocksAsync(0, 10, TenPayV3Info.MchId).GetAwaiter().GetResult();

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
        public void QueryStockAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建代金券批次 则创建新的代金券批次
            if (createStockResult is null)
            {
                CreateStockAsyncTest();
            }

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryStockAsync(createStockResult.stock_id, TenPayV3Info.MchId).GetAwaiter().GetResult();

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
        public void QueryCouponAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未发放代金券 则发放代金券
            if (distributeStockResult is null)
            {
                DistributeStockAsyncTest();
            }

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryCouponAsync(distributeStockResult.coupon_id, TenPayV3Info.AppId, openId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询代金券详情接口结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询代金券可用商户接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_7.shtml
        /// </summary>
        [TestMethod()]
        public void QueryMerchantsStockAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未发放代金券 则创建放代金券
            if (createStockResult is null)
            {
                CreateStockAsyncTest();
            }

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryMerchantsStockAsync(0, 50, TenPayV3Info.MchId, createStockResult.stock_id).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询代金券可用商户接口结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询代金券可用单品接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_8.shtml
        /// </summary>
        [TestMethod()]
        public void QueryItemsAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未发放代金券 则创建代金券
            if (createStockResult is null)
            {
                CreateStockAsyncTest();
            }

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryItemsAsync(0, 50, TenPayV3Info.MchId, createStockResult.stock_id).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询代金券可用单品接口结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 根据商户号查用户的券接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_9.shtml
        /// </summary>
        [TestMethod()]
        public void QueryCouponsAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未发放代金券 则创建代金券
            if (createStockResult is null)
            {
                CreateStockAsyncTest();
            }

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryCouponsAsync(openId, TenPayV3Info.AppId, createStockResult.stock_id, null, TenPayV3Info.MchId, null, null).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 下载批次核销明细结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 下载批次核销明细接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_10.shtml
        /// </summary>
        [TestMethod()]
        public void DownloadStockUseFlowAsyncTest()
        {
            // 如果还未发放代金券 则创建代金券
            //if (createStockResult is null)
            //{
            //    CreateStockAsyncTest();
            //}

            var stock_id = "15918471";//15918329,15918470  //改成上一天某一个ID //createStockResult.stock_id;

            var filePath = $"{stock_id}_{SystemTime.Now.ToString("HHmmss")}-StockUseFlow.csv";
            Console.WriteLine("FilePath:" + filePath);
            var fs = new FileStream(filePath, FileMode.OpenOrCreate);
            var marketingApis = new MarketingApis();

            var result = marketingApis.DownloadStockUseFlowAsync(stock_id, fs).GetAwaiter().GetResult();
            fs.Flush();

            Console.WriteLine("FileStream Length:" + fs.Length);
            fs.Close();


            Console.WriteLine("微信支付 V3 下载批次退款明细结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
            Assert.IsTrue(File.Exists(filePath));

            ////校验文件
            //var fileHash = CO2NET.Helpers.FileHelper.GetFileHash(filePath, result.hash_type);
            //Assert.AreEqual(result.hash_value.ToUpper(), fileHash);
        }

        /// <summary>
        /// 下载批次核销明细接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_11.shtml
        /// </summary>
        [TestMethod()]
        public void DownloadStockRefundFlowAsyncTest()
        {
            // 如果还未发放代金券 则创建代金券
            if (createStockResult is null)
            {
                CreateStockAsyncTest();
            }

            var stock_id = createStockResult.stock_id;

            var filePath = $"{stock_id}_{SystemTime.Now.ToString("HHmmss")}-StockRefundFlow.csv";
            Console.WriteLine("FilePath:" + filePath);
            var fs = new FileStream(filePath, FileMode.OpenOrCreate);
            var marketingApis = new MarketingApis();

            var result = marketingApis.DownloadStockRefundFlowAsync(stock_id, fs).GetAwaiter().GetResult();
            fs.Flush();

            Console.WriteLine("FileStream Length:" + fs.Length);
            fs.Close();


            Console.WriteLine("微信支付 V3 交易账单查询结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
            Assert.IsTrue(File.Exists(filePath));

            ////校验文件
            //var fileHash = CO2NET.Helpers.FileHelper.GetFileHash(filePath, result.hash_type);
            //Assert.AreEqual(result.hash_value.ToUpper(), fileHash);
        }

        /// <summary>
        /// 设置营销事件消息通知地址接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_12.shtml
        /// </summary>
        [TestMethod()]
        public void SetNotifyUrlAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var marketingApis = new MarketingApis();

            var notifyUrl = "https://sdk.weixin.senparc.com/TenPayApiV3/FavorNotifyCallback";
            var requestData = new SetNotifyUrlRequsetData(TenPayV3Info.MchId, notifyUrl, null);

            var result = marketingApis.SetNotifyUrlAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 设置营销事件消息通知地址结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }
        #endregion
    }
}
