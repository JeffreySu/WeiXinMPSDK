using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.WeixinExpress
{
    [TestClass]
    public class WeixinExpressInsuranceContractTests
    {
        [TestMethod]
        public void ApiSurfaceContainsAllElevenSyncAndAsyncEntries()
        {
            var methods = typeof(WeixinExpressApi).GetMethods().Select(z => z.Name).ToArray();

            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.OpenInsuranceFreight));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.OpenInsuranceFreightAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.QueryInsuranceFreightOpenStatus));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.QueryInsuranceFreightOpenStatusAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.CreateInsuranceFreightOrder));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.CreateInsuranceFreightOrderAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.ClaimInsuranceFreight));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.ClaimInsuranceFreightAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.CreateInsuranceChargeId));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.CreateInsuranceChargeIdAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.ApplyInsurancePay));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.ApplyInsurancePayAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.GetInsurancePayOrderList));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.GetInsurancePayOrderListAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.RefundInsurancePremium));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.RefundInsurancePremiumAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.GetInsuranceSummary));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.GetInsuranceSummaryAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.GetInsuranceOrderList));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.GetInsuranceOrderListAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.UpdateInsuranceNotifyFunds));
            CollectionAssert.Contains(methods, nameof(WeixinExpressApi.UpdateInsuranceNotifyFundsAsync));
        }

        [TestMethod]
        public void CreateOrderSerializesMoneyAddressesAndProductInfo()
        {
            var request = new WeixinExpressInsuranceCreateOrderRequest
            {
                openid = "openid",
                order_no = "4200000000000000000",
                pay_time = 1784800000,
                pay_amount = 999,
                delivery_no = "delivery-no",
                delivery_place = new WeixinExpressInsurancePlace
                {
                    province = "江苏省",
                    city = "苏州市",
                    county = "工业园区",
                    address = "测试路 1 号"
                },
                receipt_place = new WeixinExpressInsurancePlace
                {
                    province = "上海市",
                    city = "上海市",
                    county = "浦东新区",
                    address = "测试路 2 号"
                },
                product_info = new WeixinExpressInsuranceProductInfo
                {
                    order_path = "pages/order/detail",
                    goods_list = new List<WeixinExpressInsuranceGoodsItem>
                    {
                        new WeixinExpressInsuranceGoodsItem { name = "投保商品", url = "https://example.com/goods.png" }
                    }
                }
            };

            var json = SerializerHelper.GetJsonString(request, new JsonSetting(ignoreNulls: true));
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            Assert.AreEqual(999, root.GetProperty("pay_amount").GetInt64());
            Assert.AreEqual("苏州市", root.GetProperty("delivery_place").GetProperty("city").GetString());
            Assert.AreEqual("投保商品", root.GetProperty("product_info").GetProperty("goods_list")[0].GetProperty("name").GetString());
        }

        [TestMethod]
        public void ApplyPayKeepsLargeOrderIdAsString()
        {
            var request = new WeixinExpressInsuranceApplyPayRequest
            {
                order_id = "2850151276313431996"
            };

            var json = SerializerHelper.GetJsonString(request, new JsonSetting(ignoreNulls: true));
            using var document = JsonDocument.Parse(json);

            Assert.AreEqual(JsonValueKind.String, document.RootElement.GetProperty("order_id").ValueKind);
            Assert.AreEqual("2850151276313431996", document.RootElement.GetProperty("order_id").GetString());
        }

        [TestMethod]
        public void PayOrderListMapsNumericOrderIdIntoLosslessString()
        {
            const string json = @"{
  ""errcode"": 0,
  ""list"": [{
    ""order_id"": 2850151276313431996,
    ""order_status"": 5,
    ""total_price"": 1000,
    ""can_refund"": true,
    ""refund_status"": 1
  }],
  ""total"": 1
}";

            var result = JsonConvert.DeserializeObject<WeixinExpressInsurancePayOrderListJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("2850151276313431996", result.list[0].order_id);
            Assert.IsTrue(result.list[0].can_refund);
        }

        [TestMethod]
        public void PolicyRequestOmitsUnusedFiltersAndResponseMapsExampleExtension()
        {
            var request = new WeixinExpressInsuranceOrderListRequest
            {
                status_list = new List<int> { 2, 4, 5 },
                offset = 0,
                limit = 20,
                sort_direct = 1
            };
            var requestJson = SerializerHelper.GetJsonString(request, new JsonSetting(ignoreNulls: true));
            using var requestDocument = JsonDocument.Parse(requestJson);
            Assert.IsFalse(requestDocument.RootElement.TryGetProperty("openid", out _));
            Assert.AreEqual(1, requestDocument.RootElement.GetProperty("sort_direct").GetInt32());

            const string responseJson = @"{
  ""errcode"": 0,
  ""list"": [{
    ""order_no"": ""4200000000000000000"",
    ""policy_no"": ""policy-no"",
    ""status"": 2,
    ""premium"": 20,
    ""estimate_amount"": 1200,
    ""insurance_end_date"": ""2026-07-31 12:00:00""
  }],
  ""total"": 1
}";
            var result = JsonConvert.DeserializeObject<WeixinExpressInsuranceOrderListJsonResult>(responseJson);

            Assert.IsNotNull(result);
            Assert.AreEqual("policy-no", result.list[0].policy_no);
            Assert.AreEqual("2026-07-31 12:00:00", result.list[0].insurance_end_date);
        }

        [TestMethod]
        public void SummaryMapsPremiumFundsAndSafetyClosure()
        {
            const string json = @"{
  ""errcode"": 0,
  ""total"": 10,
  ""claim_num"": 3,
  ""claim_succ_num"": 2,
  ""premium"": 20,
  ""funds"": 5000,
  ""need_close"": false
}";

            var result = JsonConvert.DeserializeObject<WeixinExpressInsuranceSummaryJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(20, result.premium);
            Assert.AreEqual(5000, result.funds);
            Assert.IsFalse(result.need_close);
        }
    }
}
