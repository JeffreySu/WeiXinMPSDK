using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.ImmediateDelivery;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.ImmediateDelivery
{
    [TestClass]
    public class ImmediateDeliveryContractTests
    {
        [TestMethod]
        public void BusinessApiSurfaceContainsAllOfficialSyncAndAsyncEntries()
        {
            var methods = typeof(ImmediateDeliveryApi).GetMethods().Select(z => z.Name).ToArray();

            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.GetAllDeliveryCompanies));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.GetAllDeliveryCompaniesAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.PreAddOrder));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.PreAddOrderAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.GetBoundAccounts));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.GetBoundAccountsAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.PreCancelOrder));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.PreCancelOrderAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.OpenDelivery));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.OpenDeliveryAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.BindAccount));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.BindAccountAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.ReAddOrder));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.ReAddOrderAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.RealMockUpdateOrder));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.RealMockUpdateOrderAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.MockUpdateOrder));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.MockUpdateOrderAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.GetOrder));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.GetOrderAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.ConfirmReturn));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.ConfirmReturnAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.CancelOrder));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.CancelOrderAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.AddTips));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.AddTipsAsync));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.AddOrder));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryApi.AddOrderAsync));
        }

        [TestMethod]
        public void ProviderApiSurfaceContainsSyncAndAsyncEntries()
        {
            var methods = typeof(ImmediateDeliveryProviderApi).GetMethods().Select(z => z.Name).ToArray();

            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryProviderApi.UpdateOrderStatus));
            CollectionAssert.Contains(methods, nameof(ImmediateDeliveryProviderApi.UpdateOrderStatusAsync));
        }

        [TestMethod]
        public void AddOrderRequestUsesOfficialNestedJsonFieldsAndOmitsNulls()
        {
            var request = new ImmediateDeliveryAddOrderRequest
            {
                shopid = "shop-id",
                shop_order_id = "order-id",
                delivery_id = "SFTC",
                openid = "openid",
                receiver = new ImmediateDeliveryContact
                {
                    name = "张三",
                    city = "苏州市",
                    address = "工业园区",
                    address_detail = "1 幢 101",
                    phone = "13800000000",
                    lng = 120.71m,
                    lat = 31.30m
                },
                cargo = new ImmediateDeliveryCargo
                {
                    goods_value = 20.5m,
                    goods_weight = 1.2m,
                    cargo_first_class = "美食夜宵",
                    cargo_second_class = "快餐/地方菜"
                },
                order_info = new ImmediateDeliveryOrderInfo
                {
                    order_type = 0,
                    is_finish_code_needed = 1
                },
                shop = new ImmediateDeliveryShop
                {
                    wxa_path = "pages/order/detail",
                    goods_name = "测试商品",
                    goods_count = 1
                },
                delivery_token = "delivery-token",
                delivery_sign = "delivery-sign",
                shop_no = "shop-no"
            };

            var json = SerializerHelper.GetJsonString(request, new JsonSetting(ignoreNulls: true));
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            Assert.AreEqual("delivery-token", root.GetProperty("delivery_token").GetString());
            Assert.AreEqual("张三", root.GetProperty("receiver").GetProperty("name").GetString());
            Assert.AreEqual(20.5m, root.GetProperty("cargo").GetProperty("goods_value").GetDecimal());
            Assert.AreEqual(1, root.GetProperty("order_info").GetProperty("is_finish_code_needed").GetInt32());
            Assert.IsFalse(root.GetProperty("receiver").TryGetProperty("coordinate_type", out _));
            Assert.IsFalse(root.TryGetProperty("sub_biz_id", out _));
        }

        [TestMethod]
        public void ProviderRequestUsesWxTokenAndOfficialAgentFields()
        {
            var request = new ImmediateDeliveryProviderUpdateOrderRequest
            {
                wx_token = "wx-token",
                order_status = 102,
                waybill_id = "waybill-id",
                action_time = 1784800000,
                agent = new ImmediateDeliveryAgent
                {
                    name = "骑手",
                    phone = "13800000000",
                    is_phone_encrypted = 0
                },
                shopid = "shop-id",
                shop_order_id = "order-id",
                wxa_path = "pages/delivery/detail"
            };

            var json = SerializerHelper.GetJsonString(request, new JsonSetting(ignoreNulls: true));
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            Assert.AreEqual("wx-token", root.GetProperty("wx_token").GetString());
            Assert.AreEqual(102, root.GetProperty("order_status").GetInt32());
            Assert.AreEqual("骑手", root.GetProperty("agent").GetProperty("name").GetString());
            Assert.IsFalse(root.GetProperty("agent").TryGetProperty("lng", out _));
            Assert.IsFalse(root.TryGetProperty("expected_delivery_time", out _));
        }

        [TestMethod]
        public void ResponsesMapProviderCodesListsAndDocumentedInsuranceAlias()
        {
            const string companyJson = @"{
  ""resultcode"": 0,
  ""resultmsg"": ""ok"",
  ""list"": [{ ""delivery_id"": ""SFTC"", ""delivery_name"": ""顺丰同城"" }]
}";
            const string orderJson = @"{
  ""resultcode"": 0,
  ""resultmsg"": ""ok"",
  ""fee"": 10,
  ""insurancfee"": 1.5,
  ""waybill_id"": ""waybill-id"",
  ""order_status"": 101
}";

            var companies = JsonConvert.DeserializeObject<ImmediateDeliveryCompanyListJsonResult>(companyJson);
            var order = JsonConvert.DeserializeObject<ImmediateDeliveryAddOrderJsonResult>(orderJson);

            Assert.IsNotNull(companies);
            Assert.AreEqual(0, companies.resultcode);
            Assert.AreEqual("SFTC", companies.list[0].delivery_id);
            Assert.IsNotNull(order);
            Assert.AreEqual(1.5m, order.insurancfee);
            Assert.AreEqual("waybill-id", order.waybill_id);
            Assert.AreEqual(101, order.order_status);
        }
    }
}
