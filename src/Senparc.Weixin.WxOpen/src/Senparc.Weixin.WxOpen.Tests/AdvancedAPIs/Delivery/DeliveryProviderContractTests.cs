using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.Delivery
{
    [TestClass]
    public class DeliveryProviderContractTests
    {
        [TestMethod]
        public void ApiSurfaceContainsAllOfficialSyncAndAsyncEntries()
        {
            var methods = typeof(DeliveryProviderApi).GetMethods().Select(z => z.Name).ToArray();

            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.UpdateBusiness));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.UpdateBusinessAsync));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.UpdatePath));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.UpdatePathAsync));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.PreviewTemplate));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.PreviewTemplateAsync));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.GetContact));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.GetContactAsync));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.CancelOrder));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.CancelOrderAsync));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.UpdateOrderFee));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.UpdateOrderFeeAsync));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.RefundOrder));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.RefundOrderAsync));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.GetBill));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.GetBillAsync));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.UpdateComplaintResult));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.UpdateComplaintResultAsync));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.UpdateOrderStatus));
            CollectionAssert.Contains(methods, nameof(DeliveryProviderApi.UpdateOrderStatusAsync));
        }

        [TestMethod]
        public void OptionalRequestFieldsAreOmittedFromProductionJson()
        {
            var updateBusiness = new DeliveryProviderUpdateBusinessRequest
            {
                shop_app_id = "wx-shop-app-id",
                biz_id = "biz-id",
                result_code = 0
            };
            var updateFee = new DeliveryProviderUpdateOrderFeeRequest
            {
                token = "token",
                waybill_id = "waybill-id",
                need_pay = 2,
                fee = 100,
                original_fee = 100,
                base_fee = 100
            };
            var setting = new JsonSetting(ignoreNulls: true);

            var updateBusinessJson = SerializerHelper.GetJsonString(updateBusiness, setting);
            var updateFeeJson = SerializerHelper.GetJsonString(updateFee, setting);

            Assert.IsFalse(updateBusinessJson.Contains("result_msg"));
            Assert.IsFalse(updateFeeJson.Contains("insured_fee"));
            Assert.IsFalse(updateFeeJson.Contains("other_fee"));
            Assert.IsFalse(updateFeeJson.Contains("remark"));
            StringAssert.Contains(updateFeeJson, "\"need_pay\":2");
        }

        [TestMethod]
        public void ContactResponseMapsOfficialNestedFields()
        {
            const string json = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""waybill_id"": ""12345678901234567890"",
  ""sender"": {
    ""address"": ""广东省广州市海珠区测试地址"",
    ""name"": ""张三"",
    ""tel"": ""020-88888888"",
    ""mobile"": ""18666666666""
  },
  ""receiver"": {
    ""address"": ""广东省广州市天河区测试地址"",
    ""name"": ""王小蒙"",
    ""tel"": ""029-77777777"",
    ""mobile"": ""18610000000""
  }
}";

            var result = JsonSerializer.Deserialize<DeliveryProviderGetContactJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("12345678901234567890", result.waybill_id);
            Assert.AreEqual("张三", result.sender.name);
            Assert.AreEqual("18666666666", result.sender.mobile);
            Assert.AreEqual("广东省广州市天河区测试地址", result.receiver.address);
        }

        [TestMethod]
        public void PreviewTemplateResponseMapsRenderedContent()
        {
            const string json = @"{
  ""waybill_id"": ""1234567890123"",
  ""rendered_waybill_template"": ""PGh0bWw+dGVzdDwvaHRtbD4=""
}";

            var result = JsonSerializer.Deserialize<DeliveryProviderPreviewTemplateJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("1234567890123", result.waybill_id);
            Assert.AreEqual("PGh0bWw+dGVzdDwvaHRtbD4=", result.rendered_waybill_template);
        }

        [TestMethod]
        public void RequestsUseOfficialJsonFieldNames()
        {
            var status = new DeliveryProviderUpdateOrderStatusRequest
            {
                token = "token",
                waybill_id = "waybill-id",
                action_time = 1784800000,
                action_type = 300002,
                action_msg = "派送中",
                pickup_courier_name = "取件员",
                pickup_courier_phone = "13800000000",
                delivery_courier_name = "派件员",
                delivery_courier_phone = "13900000000"
            };
            var complaint = new DeliveryProviderUpdateComplaintResultRequest
            {
                token = "token",
                waybill_id = "waybill-id",
                result = "processed",
                desc = "已联系用户处理"
            };

            var statusJson = JsonSerializer.Serialize(status);
            var complaintJson = JsonSerializer.Serialize(complaint);
            using var statusDocument = JsonDocument.Parse(statusJson);
            using var complaintDocument = JsonDocument.Parse(complaintJson);
            var statusRoot = statusDocument.RootElement;
            var complaintRoot = complaintDocument.RootElement;

            Assert.AreEqual(300002, statusRoot.GetProperty("action_type").GetInt32());
            Assert.AreEqual("13800000000", statusRoot.GetProperty("pickup_courier_phone").GetString());
            Assert.AreEqual("派件员", statusRoot.GetProperty("delivery_courier_name").GetString());
            Assert.AreEqual("waybill-id", complaintRoot.GetProperty("waybill_id").GetString());
            Assert.IsFalse(complaintJson.Contains("access_token"));
        }
    }
}
