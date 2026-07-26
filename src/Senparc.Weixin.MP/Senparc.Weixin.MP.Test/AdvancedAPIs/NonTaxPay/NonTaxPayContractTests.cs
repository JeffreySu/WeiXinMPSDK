using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.MP.AdvancedAPIs.NonTaxPay;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs.NonTaxPay
{
    [TestClass]
    public class NonTaxPayContractTests
    {
        [TestMethod]
        public void ApiSurfaceContainsAllOfficialSyncAndAsyncEntries()
        {
            var methods = typeof(NonTaxPayApi).GetMethods().Select(z => z.Name).ToArray();

            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.QueryFee));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.QueryFeeAsync));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.UnifiedOrder));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.UnifiedOrderAsync));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.DownloadBill));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.DownloadBillAsync));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.NotifyInconsistentOrder));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.NotifyInconsistentOrderAsync));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.MockNotification));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.MockNotificationAsync));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.MockQueryFee));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.MockQueryFeeAsync));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.MicroPay));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.MicroPayAsync));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.GetOrderList));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.GetOrderListAsync));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.Refund));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.RefundAsync));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.GetOrder));
            CollectionAssert.Contains(methods, nameof(NonTaxPayApi.GetOrderAsync));
        }

        [TestMethod]
        public void OptionalRequestFieldsAreOmittedFromProductionJson()
        {
            var query = new NonTaxQueryFeeRequest
            {
                appid = "wx-app-id",
                service_id = 123,
                payment_notice_no = "notice-no",
                department_code = "department-code",
                region_code = "440000"
            };
            var refund = new NonTaxRefundRequest
            {
                appid = "wx-app-id",
                order_id = "order-id",
                reason = "duplicate payment"
            };
            var setting = new JsonSetting(ignoreNulls: true);

            var queryJson = SerializerHelper.GetJsonString(query, setting);
            var refundJson = SerializerHelper.GetJsonString(refund, setting);

            StringAssert.Contains(queryJson, "\"service_id\":123");
            Assert.IsFalse(queryJson.Contains("bank_id"));
            Assert.IsFalse(queryJson.Contains("payment_notice_type"));
            StringAssert.Contains(refundJson, "\"reason\":\"duplicate payment\"");
            Assert.IsFalse(refundJson.Contains("refund_fee"));
            Assert.IsFalse(refundJson.Contains("refund_out_id"));
        }

        [TestMethod]
        public void QueryFeeResponseMapsOfficialFields()
        {
            const string json = @"{
  ""errcode"": 0,
  ""errmsg"": ""通知书未缴款"",
  ""user_name"": ""叶*梅"",
  ""fee"": 20000,
  ""items"": [{
    ""no"": 1,
    ""item_id"": ""103050101200"",
    ""item_name"": ""交通违法罚款"",
    ""overdue"": 0,
    ""fee"": 20000
  }],
  ""payment_notice_no"": ""440204190185356"",
  ""department_code"": ""143605002004"",
  ""department_name"": ""测试执收单位"",
  ""payment_notice_type"": 1,
  ""region_code"": ""440000"",
  ""payment_notice_create_time"": 1508806661,
  ""payment_expire_date"": ""20261231""
}";

            var result = JsonSerializer.Deserialize<NonTaxQueryFeeJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(20000L, result.fee);
            Assert.AreEqual("440204190185356", result.payment_notice_no);
            Assert.AreEqual("103050101200", result.items[0].item_id);
            Assert.AreEqual(20000L, result.items[0].fee);
            Assert.AreEqual(1508806661L, result.payment_notice_create_time);
        }

        [TestMethod]
        public void OrderDetailResponseMapsNestedNotificationHistory()
        {
            const string json = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""appid"": ""wx-app-id"",
  ""openid"": ""openid"",
  ""order_id"": ""order-id"",
  ""create_time"": 1508847678,
  ""desc"": ""测试办证缴费"",
  ""fee"": 1,
  ""fee_type"": 1,
  ""status"": 3,
  ""items"": [{ ""no"": 1, ""item_id"": ""000001"", ""item_name"": ""办证缴费"", ""fee"": 1 }],
  ""partial_refund_info"": {
    ""refund_order_id"": ""refund-id"",
    ""refund_reason"": ""重复缴费"",
    ""refund_fee"": 1,
    ""refund_finish_time"": 1508848000,
    ""refund_out_id"": ""refund-out-id"",
    ""refund_status"": 5
  },
  ""notify_history"": [{
    ""appid"": ""third-app-id"",
    ""name"": ""测试财政"",
    ""notify_cnt"": 1,
    ""notify_detail"": [{
      ""notify_time"": 1524023367,
      ""ret"": 0,
      ""cost_time"": 39,
      ""wxnontaxstr"": ""random"",
      ""status"": 3,
      ""errcode"": 0,
      ""errmsg"": """"
    }]
  }]
}";

            var result = JsonSerializer.Deserialize<NonTaxGetOrderJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("order-id", result.order_id);
            Assert.AreEqual(3, result.status);
            Assert.AreEqual("refund-id", result.partial_refund_info.refund_order_id);
            Assert.AreEqual(5, result.partial_refund_info.refund_status);
            Assert.AreEqual("测试财政", result.notify_history[0].name);
            Assert.AreEqual("random", result.notify_history[0].notify_detail[0].wxnontaxstr);
        }

        [TestMethod]
        public void RequestModelsUseOfficialJsonFieldNames()
        {
            var unifiedOrder = new NonTaxUnifiedOrderRequest
            {
                appid = "wx-app-id",
                desc = "办证缴费",
                fee = 100,
                ip = "127.0.0.1",
                order_no = "business-order-no",
                department_code = "department-code",
                department_name = "department-name",
                region_code = "440000",
                items = new[]
                {
                    new NonTaxFeeItem { no = 1, item_id = "item-id", item_name = "item-name", fee = 100 }
                }.ToList(),
                payment_notice_create_time = 1784800000,
                scene = "biz"
            };
            var mock = new NonTaxMockRequest
            {
                appid = "wx-app-id",
                url = "https://example.com/nontax/notify"
            };

            var unifiedOrderJson = JsonSerializer.Serialize(unifiedOrder);
            var mockJson = JsonSerializer.Serialize(mock);

            StringAssert.Contains(unifiedOrderJson, "\"order_no\":\"business-order-no\"");
            StringAssert.Contains(unifiedOrderJson, "\"payment_notice_create_time\":1784800000");
            StringAssert.Contains(unifiedOrderJson, "\"items\":[{\"no\":1");
            StringAssert.Contains(mockJson, "\"version\":1");
            StringAssert.Contains(mockJson, "\"url\":\"https://example.com/nontax/notify\"");
        }
    }
}
