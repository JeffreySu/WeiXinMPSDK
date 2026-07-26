using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sec;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.Sec
{
    [TestClass]
    public class OrderIncrementContractTests
    {
        [TestMethod]
        public void ApiSurfaceContainsFourSyncAndAsyncEntries()
        {
            var methods = typeof(Order).GetMethods().Select(z => z.Name).ToArray();
            var expected = new[]
            {
                nameof(Order.ReportSpecialOrder),
                nameof(Order.ApplyFamousBrand),
                nameof(Order.GetFamousBrandApplyStatus),
                nameof(Order.ApplyTradeTypeChange)
            };

            foreach (var method in expected)
            {
                CollectionAssert.Contains(methods, method);
                CollectionAssert.Contains(methods, method + "Async");
            }
        }

        [TestMethod]
        public void SpecialOrderOmitsDelayForTestOrderAndKeepsItForPresaleOrder()
        {
            var testOrder = new SpecialOrderRequest { order_id = "test-order", type = 2 };
            var presaleOrder = new SpecialOrderRequest { order_id = "presale-order", type = 1, delay_to = 1752035828 };

            using var testDocument = JsonDocument.Parse(Serialize(testOrder));
            using var presaleDocument = JsonDocument.Parse(Serialize(presaleOrder));

            Assert.IsFalse(testDocument.RootElement.TryGetProperty("delay_to", out _));
            Assert.AreEqual(1752035828L, presaleDocument.RootElement.GetProperty("delay_to").GetInt64());
        }

        [TestMethod]
        public void FamousBrandRequestPreservesCapitalApplicationAndMaterialArrays()
        {
            var request = new FamousBrandApplyRequest
            {
                Application = new FamousBrandApplication
                {
                    apply_for = 1,
                    audit_info = new FamousBrandAuditInfo
                    {
                        brand_name = "测试品牌",
                        brand_type = 4,
                        flagship_in_which_ec_platform = "淘宝",
                        ec_platform_proof_list = new List<string> { "media-1" },
                        other_material_list = new List<string> { "media-2" }
                    }
                }
            };

            using var document = JsonDocument.Parse(Serialize(request));
            var root = document.RootElement;

            Assert.IsTrue(root.TryGetProperty("Application", out var application));
            Assert.IsFalse(root.TryGetProperty("application", out _));
            Assert.AreEqual("测试品牌", application.GetProperty("audit_info").GetProperty("brand_name").GetString());
            Assert.AreEqual("media-1", application.GetProperty("audit_info").GetProperty("ec_platform_proof_list")[0].GetString());
            Assert.IsFalse(application.GetProperty("audit_info").TryGetProperty("authority_certified_proof_list", out _));
        }

        [TestMethod]
        public void FamousBrandStatusMapsProgressApplicationAndRejectReason()
        {
            const string json = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""progress"": { ""status"": 2 },
  ""application"": {
    ""apply_for"": 1,
    ""status"": 2,
    ""audit_info"": { ""audit_reason"": ""材料不完整"" }
  }
}";

            var result = JsonConvert.DeserializeObject<FamousBrandStatusJsonResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.progress.status);
            Assert.AreEqual(1, result.application.apply_for);
            Assert.AreEqual("材料不完整", result.application.audit_info.audit_reason);
        }

        [TestMethod]
        public void TradeTypeChangeSerializesTargetMaterialsAndReason()
        {
            var request = new TradeTypeChangeRequest
            {
                trade_type = 3,
                material_list = new List<TradeTypeMaterial>
                {
                    new TradeTypeMaterial { type = 1, media_id = "image-media" },
                    new TradeTypeMaterial { type = 2, media_id = "video-media" }
                },
                reason = "业务模式调整"
            };

            using var document = JsonDocument.Parse(Serialize(request));
            var root = document.RootElement;

            Assert.AreEqual(3, root.GetProperty("trade_type").GetInt32());
            Assert.AreEqual(2, root.GetProperty("material_list").GetArrayLength());
            Assert.AreEqual("video-media", root.GetProperty("material_list")[1].GetProperty("media_id").GetString());
            Assert.AreEqual("业务模式调整", root.GetProperty("reason").GetString());
        }

        private static string Serialize(object value)
        {
            return SerializerHelper.GetJsonString(value, new JsonSetting(ignoreNulls: true));
        }
    }
}
