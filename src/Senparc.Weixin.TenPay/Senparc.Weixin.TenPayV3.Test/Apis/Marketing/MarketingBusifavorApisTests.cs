using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis.Marketing;
using Senparc.Weixin.TenPayV3.Entities;
using System;

namespace Senparc.Weixin.TenPayV3.Apis.Tests
{
    public partial class MarketingApisTests
    {
        #region 商家券接口

        CreateBusifavorStockReturnJson createBusifavorStockResult = null;
        DistributeStockReturnJson distributeBusifavorStockResult = null;
        QueryBusifavorPayReceiptsReturnJson queryBusifavorPayReceiptsReturnJson = null;

        /// <summary>
        /// 创建商家券接口批次测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_1.shtml
        /// </summary>
        [TestMethod()]
        public void CreateBusifavorStockAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 商家券使用规则 即刻开始 发1小时 生效1天 满100减10
            var coupon_use_rule = new CreateBusifavorStockRequestData.Coupon_Use_Rule(new(new TenpayDateTime(DateTime.Now), new TenpayDateTime(DateTime.Now.AddHours(1)), 1, null, null, null), new(100, 10), null, null, "OFF_LINE", null, null);
            // 商家券发放规则
            var stock_send_rule = new CreateBusifavorStockRequestData.Stock_Send_Rule(10, 1, 10, true);

            // TODO:流水号?这样是否有效?
            var out_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));

            var requestData = new CreateBusifavorStockRequestData("Senparc微信支付V3商家券测试", TenPayV3Info.MchId, null, "微信支付V3商家券测试使用", "NORMAL", coupon_use_rule, stock_send_rule, out_request_no, null, null, "WECHATPAY_MODE", null, false);

            var marketingApis = new MarketingApis();
            createBusifavorStockResult = marketingApis.CreateBusifavorStockRequestDataAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 创建商家券接口批次结果：" + createBusifavorStockResult.ToJson(true));

            Assert.IsNotNull(createBusifavorStockResult);
            Assert.IsTrue(createBusifavorStockResult.ResultCode.Success);
            Assert.IsTrue(createBusifavorStockResult.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询商家券批次详情接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_2.shtml
        /// </summary>
        [TestMethod()]
        public void QueryBusifavorStockAsyncTest()
        {
            if (createBusifavorStockResult is null)
            {
                CreateBusifavorStockAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryBusifavorStockAsync(createBusifavorStockResult.stock_id).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询商家券批次详情接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 核销商家券接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_3.shtml
        /// </summary>
        [TestMethod()]
        public void UseBusifavorCouponAsyncTest()
        {
            if (createBusifavorStockResult is null)
            {
                CreateBusifavorStockAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // TODO:流水号?这样是否有效?
            var use_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));

            var coupon_code = "coupon_code";// TODO: 发券似乎还是V2接口 或者 微信支付平台流量场景发放 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter5_2_1.shtml

            var requestData = new UseBusifavorCouponRequestData(coupon_code, createBusifavorStockResult.stock_id, TenPayV3Info.AppId, new TenpayDateTime(DateTime.Now), use_request_no, openId);

            var marketingApis = new MarketingApis();
            var result = marketingApis.UseBusifavorCouponAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 核销商家券接口批次结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 根据过滤条件查询商家券用户券接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_4.shtml
        /// </summary>
        [TestMethod()]
        public void QueryBusifavorCouponsAsyncTest()
        {
            if (createBusifavorStockResult is null)
            {
                CreateBusifavorStockAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryBusifavorCouponsAsync(openId, TenPayV3Info.AppId, createBusifavorStockResult.stock_id, null, null, null, null).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 根据过滤条件查询商家券用户券接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询用户单张券详情接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_4.shtml
        /// </summary>
        [TestMethod()]
        public void QueryBusifavorCouponAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var coupon_code = "coupon_code";// TODO: 发券似乎还是V2接口 或者 微信支付平台流量场景发放 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter5_2_1.shtml

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryBusifavorCouponAsync(coupon_code, TenPayV3Info.AppId, openId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询用户单张券详情接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 上传预存code接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_6.shtml
        /// </summary>
        [TestMethod()]
        public void SetBusifavorCouponCodesAsyncTest()
        {
            if (createBusifavorStockResult is null)
            {
                CreateBusifavorStockAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // TODO:流水号?这样是否有效?
            var upload_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));

            string[] coupon_code_list = { "" }; //TODO: 此处需要商家已有自己的优惠券系统生成code_list
            var requestData = new SetBusifavorCouponCodesRequestData(createBusifavorStockResult.stock_id, coupon_code_list, upload_request_no);

            var marketingApis = new MarketingApis();
            var result = marketingApis.SetBusifavorCouponCodesAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 上传预存code结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 设置商家券事件通知地址接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_7.shtml
        /// </summary>
        [TestMethod()]
        public void SetBusifavorSetNotifyUrlAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var notify_url = "senparc.com/SetBusifavorCouponCodesAsyncTest";// TODO:这个url我随便设置的
            var requestData = new SetBusifavorSetNotifyUrlRequestData(TenPayV3Info.MchId, notify_url);

            var marketingApis = new MarketingApis();
            var result = marketingApis.SetBusifavorSetNotifyUrlAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 设置商家券事件通知地址接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询商家券事件通知地址接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_8.shtml
        /// </summary>
        [TestMethod()]
        public void QueryBusifavorNotifyUrlAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryBusifavorNotifyUrlAsync(TenPayV3Info.MchId).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询商家券事件通知地址接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 关联订单信息接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_9.shtml
        /// </summary>
        [TestMethod()]
        public void AssociateBusifavorAsyncTest()
        {
            if (createBusifavorStockResult is null)
            {
                CreateBusifavorStockAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var coupon_code = "coupon_code";// TODO: 发券似乎还是V2接口 或者 微信支付平台流量场景发放 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter5_2_1.shtml
            // TODO:流水号?这样是否有效?
            var out_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));
            var out_trade_no = "";//TODO:  这里应该填上已有订单的out_trade_no

            var requestData = new AssociateBusifavorRequestData(createBusifavorStockResult.stock_id, coupon_code, out_trade_no, out_request_no);

            var marketingApis = new MarketingApis();
            var result = marketingApis.AssociateBusifavorAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 关联订单信息接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 取消关联订单信息接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_10.shtml
        /// </summary>
        [TestMethod()]
        public void DisassociateBusifavorAsyncTest()
        {
            if (createBusifavorStockResult is null)
            {
                CreateBusifavorStockAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var coupon_code = "coupon_code";// TODO: 发券似乎还是V2接口 或者 微信支付平台流量场景发放 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter5_2_1.shtml
            // TODO:流水号?这样是否有效?
            var out_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));
            var out_trade_no = "";//TODO:  这里应该填上已有订单的out_trade_no

            var requestData = new DisassociateBusifavorRequestData(createBusifavorStockResult.stock_id, coupon_code, out_trade_no, out_request_no);

            var marketingApis = new MarketingApis();
            var result = marketingApis.DisassociateBusifavorAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 取消关联订单信息接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 修改批次预算接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_11.shtml
        /// </summary>
        [TestMethod()]
        public void ModifyBusifavorStockBudgetAsyncTest()
        {
            if (createBusifavorStockResult is null)
            {
                CreateBusifavorStockAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // TODO:流水号?这样是否有效?
            var modify_budget_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));
            var requestData = new ModifyBusifavorStockBudgetRequestData(20, null, null, null, modify_budget_request_no);

            var marketingApis = new MarketingApis();
            var result = marketingApis.ModifyBusifavorStockBudgetAsync(createBusifavorStockResult.stock_id, requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 修改批次预算接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 修改商家券基本信息接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_12.shtml
        /// </summary>
        [TestMethod()]
        public void ModifyBusifavorStockInformationAsyncTest()
        {
            if (createBusifavorStockResult is null)
            {
                CreateBusifavorStockAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // TODO:流水号?这样是否有效?
            var out_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));
            var requestData = new ModifyBusifavorStockInformationRequestData(null, "Senparc微信支付V3商家券测试-修改", null, null, out_request_no, null, null, null, null);

            var marketingApis = new MarketingApis();
            var result = marketingApis.ModifyBusifavorStockInformationAsync(createBusifavorStockResult.stock_id, requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 修改商家券基本信息接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 申请退券接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_13.shtml
        /// </summary>
        [TestMethod()]
        public void ReturnBusifavorCouponAsyncTest()
        {
            if (createBusifavorStockResult is null)
            {
                CreateBusifavorStockAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];


            var coupon_code = "coupon_code";// TODO: 发券似乎还是V2接口 或者 微信支付平台流量场景发放 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter5_2_1.shtml

            // TODO:流水号?这样是否有效?
            var return_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));
            var requestData = new ReturnBusifavorCouponRequestData(coupon_code, createBusifavorStockResult.stock_id, return_request_no);

            var marketingApis = new MarketingApis();
            var result = marketingApis.ReturnBusifavorCouponAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 申请退券接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 使券失效接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_14.shtml
        /// </summary>
        [TestMethod()]
        public void DeactivateBusifavorCouponAsyncTest()
        {
            if (createBusifavorStockResult is null)
            {
                CreateBusifavorStockAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var coupon_code = "coupon_code";// TODO: 发券似乎还是V2接口 或者 微信支付平台流量场景发放 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter5_2_1.shtml

            // TODO:流水号?这样是否有效?
            var deactivate_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));
            var requestData = new DeactivateBusifavorCouponRequestData(coupon_code, createBusifavorStockResult.stock_id, deactivate_request_no, "Senparc接口单元测试");

            var marketingApis = new MarketingApis();
            var result = marketingApis.DeactivateBusifavorCouponAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 使券失效接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 营销补差付款接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_16.shtml
        /// </summary>
        [TestMethod()]
        public void PayBusifavorReceiptsAsyncTest()
        {
            if (createBusifavorStockResult is null)
            {
                CreateBusifavorStockAsyncTest();
            }

            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            var coupon_code = "coupon_code";// TODO: 发券似乎还是V2接口 或者 微信支付平台流量场景发放 https://pay.weixin.qq.com/wiki/doc/apiv3/open/pay/chapter5_2_1.shtml

            var transaction_id = "transaction_id";// TODO: 填入现有订单的transaction_id

            var payer_merchant = "payer_merchant";// TODO: 收款商户号 需要另一个收款的商户号
            // TODO:流水号?这样是否有效?
            var out_subsidy_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));
            var requestData = new PayBusifavorReceiptsRequestData(createBusifavorStockResult.stock_id, coupon_code, out_subsidy_no, transaction_id, payer_merchant, 100, "Senparc营销补差付款接口单元测试", out_subsidy_no);

            var marketingApis = new MarketingApis();
            var result = marketingApis.PayBusifavorReceiptsAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 营销补差付款接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询营销补差付款单详情接口
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_18.shtml
        /// </summary>
        [TestMethod()]
        public void QueryBusifavorPayReceiptsAsyncTest()
        {
            if (queryBusifavorPayReceiptsReturnJson is null)
            {
                PayBusifavorReceiptsAsyncTest();
            }

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryBusifavorPayReceiptsAsync(queryBusifavorPayReceiptsReturnJson.subsidy_receipt_id).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询营销补差付款单详情接口：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        #endregion
    }
}
