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
    public class WeixinExpressIntracityContractTests
    {
        [TestMethod]
        public void ApiSurfaceContainsAllSixteenSyncAndAsyncEntries()
        {
            var methods = typeof(WeixinExpressApi).GetMethods().Select(z => z.Name).ToArray();
            var expected = new[]
            {
                nameof(WeixinExpressApi.IntracityApply),
                nameof(WeixinExpressApi.IntracityCreateStore),
                nameof(WeixinExpressApi.IntracityQueryStore),
                nameof(WeixinExpressApi.IntracityUpdateStore),
                nameof(WeixinExpressApi.IntracityStoreCharge),
                nameof(WeixinExpressApi.IntracityStoreRefund),
                nameof(WeixinExpressApi.IntracityQueryFlow),
                nameof(WeixinExpressApi.IntracityBalanceQuery),
                nameof(WeixinExpressApi.IntracityPreAddOrder),
                nameof(WeixinExpressApi.IntracityAddOrder),
                nameof(WeixinExpressApi.IntracityQueryOrder),
                nameof(WeixinExpressApi.IntracityCancelOrder),
                nameof(WeixinExpressApi.IntracitySetPayMode),
                nameof(WeixinExpressApi.IntracityGetPayMode),
                nameof(WeixinExpressApi.IntracityGetCity),
                nameof(WeixinExpressApi.IntracityMockNotify)
            };

            foreach (var method in expected)
            {
                CollectionAssert.Contains(methods, method);
                CollectionAssert.Contains(methods, method + "Async");
            }
        }

        [TestMethod]
        public void StoreRequestsSerializeNestedAddressNumericPatternAndOmitNulls()
        {
            var createRequest = new WeixinExpressIntracityCreateStoreRequest
            {
                out_store_id = "store-1",
                store_name = "测试门店",
                order_pattern = 2,
                service_trans_prefer = "SFTC",
                address_info = new WeixinExpressIntracityAddressInfo
                {
                    province = "北京市",
                    city = "北京市",
                    area = "海淀区",
                    house = "测试路 1 号",
                    lat = 40.030613m,
                    lng = 116.354787m,
                    phone = "13800000000"
                }
            };
            var updateRequest = new WeixinExpressIntracityUpdateStoreRequest
            {
                keys = new WeixinExpressIntracityStoreKey { wx_store_id = "4000000000000042001" },
                content = new WeixinExpressIntracityStoreUpdateContent { order_pattern = 2 }
            };

            using var createDocument = JsonDocument.Parse(Serialize(createRequest));
            using var updateDocument = JsonDocument.Parse(Serialize(updateRequest));

            Assert.AreEqual(JsonValueKind.Number, createDocument.RootElement.GetProperty("order_pattern").ValueKind);
            Assert.AreEqual("北京市", createDocument.RootElement.GetProperty("address_info").GetProperty("city").GetString());
            Assert.IsFalse(createDocument.RootElement.GetProperty("address_info").TryGetProperty("street", out _));
            Assert.AreEqual(JsonValueKind.Number, updateDocument.RootElement.GetProperty("content").GetProperty("order_pattern").ValueKind);
            Assert.IsFalse(updateDocument.RootElement.GetProperty("keys").TryGetProperty("out_store_id", out _));
        }

        [TestMethod]
        public void StoreAndFlowResponsesMapOfficialTableAndExampleDifferences()
        {
            const string storeJson = @"{
  ""errcode"": 0,
  ""total"": 1,
  ""appid"": ""wx-appid"",
  ""store_list"": [{ ""wx_store_id"": ""4001"", ""city_id"": 440300, ""order_pattern"": 2 }]
}";
            const string flowJson = @"{
  ""errcode"": 0,
  ""total"": 1,
  ""flow_list"": [{
    ""flow_type"": 1,
    ""pay_order_id"": 2920020938702667776,
    ""pay_amount"": 5000,
    ""pay_status"": ""SUCCESS""
  }],
  ""total_pay_amt"": 5000
}";

            var storeResult = JsonConvert.DeserializeObject<WeixinExpressIntracityQueryStoreJsonResult>(storeJson);
            var flowResult = JsonConvert.DeserializeObject<WeixinExpressIntracityQueryFlowJsonResult>(flowJson);

            Assert.IsNotNull(storeResult);
            Assert.AreEqual("440300", storeResult.store_list[0].city_id);
            Assert.IsNotNull(flowResult);
            Assert.AreEqual(1, flowResult.total);
            Assert.AreEqual("2920020938702667776", flowResult.flow_list[0].pay_order_id);
            Assert.AreEqual(5000L, flowResult.total_pay_amt);
        }

        [TestMethod]
        public void BalanceResponseMapsArrayShapesAndLargeOrderIdentifier()
        {
            const string json = @"{
  ""errcode"": 0,
  ""appid"": ""wx-appid"",
  ""wx_store_id"": ""4001"",
  ""all_balance"": 10,
  ""balance_detail"": [{
    ""balance"": 10,
    ""service_trans_id"": ""DADA"",
    ""service_trans_name"": ""达达快送"",
    ""order_list"": [{
      ""payorder_id"": ""2978080038359269380"",
      ""unused_amt"": 10,
      ""charge_amt"": 10
    }]
  }]
}";

            var result = JsonConvert.DeserializeObject<WeixinExpressIntracityBalanceQueryJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(10L, result.all_balance);
            Assert.AreEqual("DADA", result.balance_detail[0].service_trans_id);
            Assert.AreEqual("2978080038359269380", result.balance_detail[0].order_list[0].payorder_id);
        }

        [TestMethod]
        public void PreAddAndAddOrderSerializeCargoListsAndOptionalFields()
        {
            var cargo = new WeixinExpressIntracityCargo
            {
                cargo_name = "榴莲披萨套餐",
                cargo_weight = 500,
                cargo_price = 5000,
                cargo_type = 1,
                cargo_num = 3,
                item_list = new List<WeixinExpressIntracityCargoItem>
                {
                    new WeixinExpressIntracityCargoItem
                    {
                        item_name = "八寸榴莲披萨",
                        item_pic_url = "https://example.com/pizza.png",
                        count = 1
                    }
                }
            };
            var request = new WeixinExpressIntracityAddOrderRequest
            {
                wx_store_id = "4001",
                store_order_id = "order-1",
                user_openid = "openid",
                user_lng = 116.353093m,
                user_lat = 40.01496m,
                user_address = "北京市海淀区测试路 1 号",
                user_name = "测试用户",
                user_phone = "13800000000",
                order_detail_path = "pages/order/detail",
                cargo = cargo
            };

            using var document = JsonDocument.Parse(Serialize(request));
            var root = document.RootElement;

            Assert.AreEqual(JsonValueKind.Number, root.GetProperty("user_lng").ValueKind);
            Assert.AreEqual("八寸榴莲披萨", root.GetProperty("cargo").GetProperty("item_list")[0].GetProperty("item_name").GetString());
            Assert.AreEqual(1, root.GetProperty("cargo").GetProperty("item_list")[0].GetProperty("count").GetInt32());
            Assert.IsFalse(root.TryGetProperty("callback_url", out _));
            Assert.IsFalse(root.TryGetProperty("use_sandbox", out _));
        }

        [TestMethod]
        public void QueryOrderMapsNestedFieldsAndItemNumExampleAlias()
        {
            const string json = @"{
  ""errcode"": 0,
  ""wx_order_id"": ""2000000000000042007"",
  ""order_status"": 30000,
  ""transporter_info"": { ""transporter_name"": ""张三"", ""transporter_phone"": ""13800000000"" },
  ""store_info"": { ""store_name"": ""测试门店"", ""lng"": 116.3, ""lat"": 40.0 },
  ""receiver_info"": { ""receiver_name"": ""李四"", ""address"": ""测试地址"" },
  ""cargo_info"": {
    ""cargo_name"": ""商品"",
    ""item_list"": [
      { ""item_name"": ""示例字段"", ""item_num"": 2 },
      { ""item_name"": ""参数表字段"", ""num"": 3 }
    ]
  }
}";

            var result = JsonConvert.DeserializeObject<WeixinExpressIntracityQueryOrderJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("张三", result.transporter_info.transporter_name);
            Assert.AreEqual("测试门店", result.store_info.store_name);
            Assert.AreEqual(2, result.cargo_info.item_list[0].num);
            Assert.AreEqual(3, result.cargo_info.item_list[1].num);
        }

        [TestMethod]
        public void PayModeAndCityResponsesMapConditionalAndArrayFields()
        {
            const string payModeJson = @"{
  ""errcode"": 0,
  ""pay_mode"": ""PAY_MODE_COMPONENT"",
  ""pay_component_appid"": ""wx-component""
}";
            const string cityJson = @"{
  ""errcode"": 0,
  ""support_list"": [{
    ""service_trans_id"": ""SFTC"",
    ""city_list"": [{ ""city_name"": ""北京市"", ""city_code"": 110000 }]
  }]
}";

            var payMode = JsonConvert.DeserializeObject<WeixinExpressIntracityGetPayModeJsonResult>(payModeJson);
            var city = JsonConvert.DeserializeObject<WeixinExpressIntracityGetCityJsonResult>(cityJson);

            Assert.IsNotNull(payMode);
            Assert.AreEqual("wx-component", payMode.pay_component_appid);
            Assert.IsNotNull(city);
            Assert.AreEqual(110000, city.support_list[0].city_list[0].city_code);
        }

        [TestMethod]
        public void CallbackModelsPreserveStatusTimestampsAndAcknowledgementFields()
        {
            const string notifyJson = @"{
  ""appid"": ""wx-appid"",
  ""wx_store_id"": ""4001"",
  ""wx_order_id"": ""4018734875633256960"",
  ""store_order_id"": ""order-1"",
  ""order_status"": 40000,
  ""status_change_time"": 1711458532,
  ""timestamp"": 1711458533,
  ""service_trans_id"": ""DADA"",
  ""sign"": ""signature""
}";
            var notify = JsonConvert.DeserializeObject<WeixinExpressIntracityOrderStatusNotify>(notifyJson);
            var response = new WeixinExpressIntracityNotifyResponse { return_code = 0, return_msg = "OK" };

            Assert.IsNotNull(notify);
            Assert.AreEqual(40000, notify.order_status);
            Assert.AreEqual(1711458532L, notify.status_change_time);

            using var responseDocument = JsonDocument.Parse(Serialize(response));
            Assert.AreEqual(0, responseDocument.RootElement.GetProperty("return_code").GetInt32());
            Assert.AreEqual("OK", responseDocument.RootElement.GetProperty("return_msg").GetString());
        }

        private static string Serialize(object value)
        {
            return SerializerHelper.GetJsonString(value, new JsonSetting(ignoreNulls: true));
        }
    }
}
