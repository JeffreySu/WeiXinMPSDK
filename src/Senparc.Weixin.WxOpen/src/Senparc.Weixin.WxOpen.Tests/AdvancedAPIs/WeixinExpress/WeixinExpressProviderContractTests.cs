using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WeixinExpress;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.WeixinExpress
{
    [TestClass]
    public class WeixinExpressProviderContractTests
    {
        [TestMethod]
        public void ApiSurfaceContainsBothProviderSyncAndAsyncEntries()
        {
            var methods = typeof(WeixinExpressProviderApi).GetMethods().Select(z => z.Name).ToArray();

            CollectionAssert.Contains(methods, nameof(WeixinExpressProviderApi.QueryUserBinding));
            CollectionAssert.Contains(methods, nameof(WeixinExpressProviderApi.QueryUserBindingAsync));
            CollectionAssert.Contains(methods, nameof(WeixinExpressProviderApi.NotifyPath));
            CollectionAssert.Contains(methods, nameof(WeixinExpressProviderApi.NotifyPathAsync));
        }

        [TestMethod]
        public void UserQueryUsesOfficialPhoneField()
        {
            var request = new WeixinExpressUserQueryRequest { phone = "13800138000" };
            var json = SerializerHelper.GetJsonString(request, new JsonSetting(ignoreNulls: true));
            using var document = JsonDocument.Parse(json);

            Assert.AreEqual("13800138000", document.RootElement.GetProperty("phone").GetString());
        }

        [TestMethod]
        public void PathNotifySerializesNestedContactAndPathFields()
        {
            var request = new WeixinExpressPathNotifyRequest
            {
                sender = new WeixinExpressPathContact
                {
                    province = "江苏省",
                    city = "苏州市",
                    area = "工业园区"
                },
                receiver = new WeixinExpressPathContact
                {
                    name = "张三",
                    phone = "13800000000",
                    province = "上海市",
                    city = "上海市",
                    area = "浦东新区",
                    address = "测试路 1 号"
                },
                waybill_id = "waybill-id",
                path = new WeixinExpressPathNode
                {
                    action_time = 1784800000,
                    action_type = 200001,
                    action_msg = "运输中"
                },
                create_time = 1784700000
            };

            var json = SerializerHelper.GetJsonString(request, new JsonSetting(ignoreNulls: true));
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            Assert.AreEqual("waybill-id", root.GetProperty("waybill_id").GetString());
            Assert.AreEqual("张三", root.GetProperty("receiver").GetProperty("name").GetString());
            Assert.AreEqual(200001, root.GetProperty("path").GetProperty("action_type").GetInt32());
            Assert.AreEqual(1784700000, root.GetProperty("create_time").GetInt64());
            Assert.IsFalse(root.GetProperty("sender").TryGetProperty("name", out _));
            Assert.IsFalse(root.GetProperty("path").TryGetProperty("pickup_courier_name", out _));
        }

        [TestMethod]
        public void BindingResponseMapsExistFlag()
        {
            const string json = @"{ ""errcode"": 0, ""errmsg"": ""ok"", ""exist"": 1 }";

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<WeixinExpressUserBindingJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.exist);
        }
    }
}
