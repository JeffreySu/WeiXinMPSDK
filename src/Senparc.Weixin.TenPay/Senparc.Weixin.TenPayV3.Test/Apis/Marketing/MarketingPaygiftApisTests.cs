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
        #region 支付有礼接口

        CreateUniqueThresholdActivityReturnJson createUniqueThresholdActivityResult = null;

        /// <summary>
        /// 创建全场满额送活动接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_2.shtml
        /// </summary>
        [TestMethod()]
        public void CreateUniqueThresholdActivityAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建支付有礼活动 则建立支付有礼活动
            if (createUniqueThresholdActivityResult is null)
            {
                CreateUniqueThresholdActivityAsyncTest();
            }

            // TODO:流水号?这样是否有效?
            var out_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));

            //TODO: 此接口测试依赖商户券接口和图片上传接口
            var activity_base_info = new CreateUniqueThresholdActivityRequestData.Activity_Base_Info("Senparc支付有礼单元测试活动", "活动副标题", "TODO:仅支持通过《图片上传API》接口获取的图片URL地址", null, new TenpayDateTime(DateTime.Now), new TenpayDateTime(DateTime.Now.AddHours(2)), null, out_request_no, "OFF_LINE_PAY", null, null);
            var award_send_rule = new CreateUniqueThresholdActivityRequestData.Award_Send_Rule(100, "SINGLE_COUPON", "BUSIFAVOR", new CreateUniqueThresholdActivityRequestData.Award_Send_Rule.Award_List[] { new(createBusifavorStockResult.stock_id, "TODO:原始图", "TODO:缩略图") }, "IN_SEVICE_COUPON_MERCHANT", null);
            var requestData = new CreateUniqueThresholdActivityRequestData(activity_base_info, award_send_rule, null);

            var marketingApis = new MarketingApis();

            createUniqueThresholdActivityResult = marketingApis.CreateUniqueThresholdActivityAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 创建全场满额送活动接口结果：" + createUniqueThresholdActivityResult.ToJson(true));

            Assert.IsNotNull(createUniqueThresholdActivityResult);
            Assert.IsTrue(createUniqueThresholdActivityResult.ResultCode.Success);
            Assert.IsTrue(createUniqueThresholdActivityResult.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询活动详情接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_4.shtml
        /// </summary>
        [TestMethod()]
        public void QueryPaygiftActivityAsyncTest()
        {
            // 如果还未创建支付有礼活动 则建立支付有礼活动
            if (createUniqueThresholdActivityResult is null)
            {
                CreateUniqueThresholdActivityAsyncTest();
            }

            var marketingApis = new MarketingApis();

            var result = marketingApis.QueryPaygiftActivityAsync(createUniqueThresholdActivityResult.activity_id).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询活动详情接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询活动发券商户号接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_5.shtml
        /// </summary>
        [TestMethod()]
        public void QueryPaygiftActivityMerchantsAsyncTest()
        {
            // 如果还未创建支付有礼活动 则建立支付有礼活动
            if (createUniqueThresholdActivityResult is null)
            {
                CreateUniqueThresholdActivityAsyncTest();
            }

            var marketingApis = new MarketingApis();

            var result = marketingApis.QueryPaygiftActivityMerchantsAsync(createUniqueThresholdActivityResult.activity_id).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询活动发券商户号接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 查询活动指定商品列表接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_6.shtml
        /// </summary>
        [TestMethod()]
        public void QueryPaygiftActivityGoodsAsyncTest()
        {
            // 如果还未创建支付有礼活动 则建立支付有礼活动
            if (createUniqueThresholdActivityResult is null)
            {
                CreateUniqueThresholdActivityAsyncTest();
            }

            var marketingApis = new MarketingApis();

            var result = marketingApis.QueryPaygiftActivityGoodsAsync(createUniqueThresholdActivityResult.activity_id).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询活动发券商户号接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 获取支付有礼活动列表接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_9.shtml
        /// </summary>
        [TestMethod()]
        public void QueryPaygiftActivitiesAsyncTest()
        {
            // 如果还未创建支付有礼活动 则建立支付有礼活动
            if (createUniqueThresholdActivityResult is null)
            {
                CreateUniqueThresholdActivityAsyncTest();
            }

            var marketingApis = new MarketingApis();

            var result = marketingApis.QueryPaygiftActivitiesAsync(createUniqueThresholdActivityResult.activity_id, "ACT_STATUS_UNKNOWN", "BUSIFAVOR").GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 获取支付有礼活动列表接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 新增活动发券商户号接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_8.shtml
        /// </summary>
        [TestMethod()]
        public void AddPaygiftActivityMerchantsAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建支付有礼活动 则建立支付有礼活动
            if (createUniqueThresholdActivityResult is null)
            {
                CreateUniqueThresholdActivityAsyncTest();
            }

            // TODO:流水号?这样是否有效?
            var out_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));

            var mchid = "{mchid}";//TODO: 此处需要填入新增加的mchid
            var requestData = new AddPaygiftActivityMerchantsRequestData(createUniqueThresholdActivityResult.activity_id, new string[] { mchid }, out_request_no);

            var marketingApis = new MarketingApis();
            var result = marketingApis.AddPaygiftActivityMerchantsAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 新增活动发券商户号接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 删除活动发券商户号接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_10.shtml
        /// </summary>
        [TestMethod()]
        public void DeletePaygiftActivitiyMerchantsAsyncTest()
        {
            var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);
            var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // 如果还未创建支付有礼活动 则建立支付有礼活动
            if (createUniqueThresholdActivityResult is null)
            {
                CreateUniqueThresholdActivityAsyncTest();
            }

            // TODO:流水号?这样是否有效?
            var delete_request_no = string.Format("{0}{1}{2}", TenPayV3Info.MchId/*10位*/, SystemTime.Now.ToString("yyyyMMddHHmmss"), TenPayV3Util.BuildRandomStr(6));

            var mchid = "{mchid}";//TODO: 此处需要填入要删除的mchid
            var requestData = new DeletePaygiftActivitiyMerchantsRequestData(createUniqueThresholdActivityResult.activity_id, new string[] { mchid }, delete_request_no);

            var marketingApis = new MarketingApis();
            var result = marketingApis.DeletePaygiftActivitiyMerchantsAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 删除活动发券商户号接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 终止支付有礼活动接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_7.shtml
        /// </summary>
        [TestMethod()]
        public void TerminatePaygiftActivityAsyncTest()
        {
            // 如果还未创建支付有礼活动 则建立支付有礼活动
            if (createUniqueThresholdActivityResult is null)
            {
                CreateUniqueThresholdActivityAsyncTest();
            }

            var marketingApis = new MarketingApis();

            var result = marketingApis.TerminatePaygiftActivityAsync(createUniqueThresholdActivityResult.activity_id).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 终止支付有礼活动接口测试结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        #endregion
    }
}
