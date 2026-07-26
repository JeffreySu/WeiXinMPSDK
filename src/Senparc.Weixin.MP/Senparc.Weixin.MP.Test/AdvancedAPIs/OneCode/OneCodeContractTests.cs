using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.MP.AdvancedAPIs.OneCode;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs.OneCode
{
    [TestClass]
    public class OneCodeContractTests
    {
        [TestMethod]
        public void ApiSurfaceContainsAllOfficialSyncAndAsyncEntries()
        {
            var methods = typeof(OneCodeApi).GetMethods().Select(z => z.Name).ToArray();

            CollectionAssert.Contains(methods, nameof(OneCodeApi.ApplyCodeQuery));
            CollectionAssert.Contains(methods, nameof(OneCodeApi.ApplyCodeQueryAsync));
            CollectionAssert.Contains(methods, nameof(OneCodeApi.ApplyCodeDownload));
            CollectionAssert.Contains(methods, nameof(OneCodeApi.ApplyCodeDownloadAsync));
            CollectionAssert.Contains(methods, nameof(OneCodeApi.CodeActive));
            CollectionAssert.Contains(methods, nameof(OneCodeApi.CodeActiveAsync));
            CollectionAssert.Contains(methods, nameof(OneCodeApi.CodeActiveQuery));
            CollectionAssert.Contains(methods, nameof(OneCodeApi.CodeActiveQueryAsync));
            CollectionAssert.Contains(methods, nameof(OneCodeApi.TicketToCode));
            CollectionAssert.Contains(methods, nameof(OneCodeApi.TicketToCodeAsync));
            CollectionAssert.Contains(methods, nameof(OneCodeApi.ApplyCode));
            CollectionAssert.Contains(methods, nameof(OneCodeApi.ApplyCodeAsync));
        }

        [TestMethod]
        public void OptionalQueryFieldsAreOmittedFromProductionJson()
        {
            var applyQuery = new ApplyCodeQueryRequest
            {
                isv_application_id = "external-application-id"
            };
            var activeQuery = new CodeActiveQueryRequest
            {
                code_url = "P.URL.CN/0U.JYJXP3HJI2C98A9O"
            };
            var setting = new JsonSetting(ignoreNulls: true);

            var applyJson = SerializerHelper.GetJsonString(applyQuery, setting);
            var activeJson = SerializerHelper.GetJsonString(activeQuery, setting);

            StringAssert.Contains(applyJson, "\"isv_application_id\":\"external-application-id\"");
            Assert.IsFalse(applyJson.Contains("\"application_id\":"));
            StringAssert.Contains(activeJson, "\"code_url\":\"P.URL.CN/0U.JYJXP3HJI2C98A9O\"");
            Assert.IsFalse(activeJson.Contains("code_index"));
            Assert.IsFalse(activeJson.Contains("\"code\":"));
        }

        [TestMethod]
        public void ApplyCodeQueryResponseMapsOfficialFields()
        {
            const string json = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""status"": ""FINISH"",
  ""application_id"": 581865877,
  ""isv_application_id"": ""external-id"",
  ""code_generate_list"": [{ ""code_start"": 0, ""code_end"": 49999 }],
  ""create_time"": 1784800000,
  ""update_time"": 1784803600
}";

            var result = JsonSerializer.Deserialize<ApplyCodeQueryJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual("FINISH", result.status);
            Assert.AreEqual(581865877L, result.application_id);
            Assert.AreEqual("external-id", result.isv_application_id);
            Assert.AreEqual(0L, result.code_generate_list[0].code_start);
            Assert.AreEqual(49999L, result.code_generate_list[0].code_end);
            Assert.AreEqual(1784803600L, result.update_time);
        }

        [TestMethod]
        public void ActiveCodeResponseMapsDocumentedAndExampleFields()
        {
            const string json = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""code"": ""123456789"",
  ""application_id"": 581865877,
  ""isv_application_id"": ""external-id"",
  ""activity_name"": ""summer"",
  ""product_brand"": ""Senparc"",
  ""product_title"": ""Demo Product"",
  ""product_code"": ""6900000000000"",
  ""wxa_appid"": ""wx-app-id"",
  ""wxa_path"": ""pages/index/index"",
  ""wxa_type"": 2,
  ""code_start"": 0,
  ""code_end"": 200
}";

            var activeResult = JsonSerializer.Deserialize<CodeActiveQueryJsonResult>(json);
            var ticketResult = JsonSerializer.Deserialize<TicketToCodeJsonResult>(json);

            Assert.IsNotNull(activeResult);
            Assert.AreEqual("123456789", activeResult.code);
            Assert.AreEqual("6900000000000", activeResult.product_code);
            Assert.AreEqual(2, activeResult.wxa_type);
            Assert.AreEqual(200L, activeResult.code_end);
            Assert.IsNotNull(ticketResult);
            Assert.AreEqual("summer", ticketResult.activity_name);
            Assert.AreEqual("wx-app-id", ticketResult.wxa_appid);
        }

        [TestMethod]
        public void RequestsUseOfficialJsonFieldNames()
        {
            var activeRequest = new CodeActiveRequest
            {
                application_id = 581865877,
                activity_name = "summer",
                product_brand = "Senparc",
                product_title = "Demo Product",
                product_code = "6900000000000",
                wxa_appid = "wx-app-id",
                wxa_path = "pages/index/index",
                wxa_type = 2,
                code_start = 0,
                code_end = 9999
            };
            var ticketRequest = new TicketToCodeRequest
            {
                openid = "openid",
                code_ticket = "ticket"
            };

            var activeJson = JsonSerializer.Serialize(activeRequest);
            var ticketJson = JsonSerializer.Serialize(ticketRequest);

            StringAssert.Contains(activeJson, "\"application_id\":581865877");
            StringAssert.Contains(activeJson, "\"product_code\":\"6900000000000\"");
            StringAssert.Contains(activeJson, "\"wxa_type\":2");
            StringAssert.Contains(activeJson, "\"code_end\":9999");
            StringAssert.Contains(ticketJson, "\"openid\":\"openid\"");
            StringAssert.Contains(ticketJson, "\"code_ticket\":\"ticket\"");
        }
    }
}
