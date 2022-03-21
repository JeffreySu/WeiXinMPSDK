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
        #region 委托营销接口
        BuildPartnershipsReturnJson buildPartnershipsResult = null;

        /// <summary>
        /// 激建立合作关系测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_3.shtml
        /// </summary>
        [TestMethod()]
        public void BuildPartnershipsAsyncTest()
        {
            // 如果还未创建代金券批次 则创建新的代金券批次
            if (createStockResult is null)
            {
                CreateStockAsyncTest();
            }
            //var key = TenPayHelper.GetRegisterKey(Config.SenparcWeixinSetting);

            //var TenPayV3Info = TenPayV3InfoCollection.Data[key];

            // TODO: 此处信息需要完善
            var parner = new BuildPartnershipsRequestData.Partner("{合作方类型}", "{合作方Appid}", "{合作方商户id}");
            var authorized_data = new BuildPartnershipsRequestData.Authorized_Data("FAVOR_STOCK", createStockResult.stock_id);

            var requestData = new BuildPartnershipsRequestData(parner, authorized_data);

            var marketingApis = new MarketingApis();

            try
            {
                // 合作类型设置为appid 但传入 "partner.merchant_id" 的情况
                buildPartnershipsResult = marketingApis.BuildPartnershipsAsync(requestData).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(TenpayApiRequestException));
                Console.WriteLine(ex.Message);
                Assert.IsTrue(ex.Message.Contains("为null！"));
            }

            // 修改参数
            requestData.partner.merchant_id = null;

            buildPartnershipsResult = marketingApis.BuildPartnershipsAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 建立合作关系接口结果：" + buildPartnershipsResult.ToJson(true));

            Assert.IsNotNull(buildPartnershipsResult);
            Assert.IsTrue(buildPartnershipsResult.ResultCode.Success);
            Assert.IsTrue(buildPartnershipsResult.VerifySignSuccess == true);//通过验证
        }


        /// <summary>
        /// 查询合作关系列表接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_5_3.shtml
        /// </summary>
        [TestMethod()]
        public void QueryPartnershipsAsyncTest()
        {
            // 如果还未创建建立合作关系 则建立合作关系
            if (buildPartnershipsResult is null)
            {
                BuildPartnershipsAsyncTest();
            }

            var requestData = new QueryPartnershipsRequestData(buildPartnershipsResult);

            var marketingApis = new MarketingApis();
            var result = marketingApis.QueryPartnershipsAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 查询合作关系列表接口结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }

        /// <summary>
        /// 终止合作关系接口测试
        /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_5_2.shtml
        /// </summary>
        [TestMethod()]
        public void TerminatePartnershipsAsyncTest()
        {
            // 如果还未创建建立合作关系 则建立合作关系
            if (buildPartnershipsResult is null)
            {
                BuildPartnershipsAsyncTest();
            }

            var requestData = new TerminatePartnershipsRequestData(buildPartnershipsResult);

            var marketingApis = new MarketingApis();
            var result = marketingApis.TerminatePartnershipsAsync(requestData).GetAwaiter().GetResult();

            Console.WriteLine("微信支付 V3 终止合作关系接口结果：" + result.ToJson(true));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ResultCode.Success);
            Assert.IsTrue(result.VerifySignSuccess == true);//通过验证
        }
        #endregion
    }
}
