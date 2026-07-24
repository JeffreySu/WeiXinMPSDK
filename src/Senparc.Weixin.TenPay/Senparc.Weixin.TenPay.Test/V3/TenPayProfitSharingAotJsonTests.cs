using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.TenPay.V3;
using System.Text.Json;

namespace Senparc.Weixin.TenPay.Test.vs2017.V3
{
    [TestClass]
    public class TenPayProfitSharingAotJsonTests
    {
        [TestMethod]
        public void Constructor_UsesSystemTextJsonGeneratedMetadata()
        {
            var receiver = new TenpayV3ProfitShareingRequestData_ReceiverInfo
            {
                receiveType = TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type.PERSONAL_OPENID,
                account = "openid-1",
                amount = 100,
                description = "test"
            };

            var request = new TenpayV3ProtfitSharingRequestData(
                "app-id",
                "mch-id",
                null,
                null,
                "test-key",
                "nonce",
                "transaction-id",
                "order-no",
                new[] { receiver });

            var json = (string)request.PackageRequestHandler.GetAllParameters()["receivers"];
            using var document = JsonDocument.Parse(json);
            var item = document.RootElement[0];

            Assert.AreEqual("PERSONAL_OPENID", item.GetProperty("type").GetString());
            Assert.AreEqual("openid-1", item.GetProperty("account").GetString());
            Assert.AreEqual(100, item.GetProperty("amount").GetInt32());
            Assert.IsFalse(item.TryGetProperty("receiveType", out _));
        }

        [TestMethod]
        public void SceneInfo_UsesGeneratedMetadataForAndroid()
        {
            var sceneInfo = new TenPayV3UnifiedorderRequestData_SceneInfo(
                true,
                new H5_Info_Android
                {
                    type = "Android",
                    app_name = "Senparc App",
                    package_name = "com.senparc.app"
                });

            using var document = JsonDocument.Parse(sceneInfo.ToString());
            var h5Info = document.RootElement.GetProperty("h5_info");

            Assert.AreEqual("Android", h5Info.GetProperty("type").GetString());
            Assert.AreEqual("com.senparc.app", h5Info.GetProperty("package_name").GetString());
        }

        [TestMethod]
        public void SceneInfo_PreservesCustomH5InfoOutsideAot()
        {
            var sceneInfo = new TenPayV3UnifiedorderRequestData_SceneInfo(
                true,
                new CustomH5Info { type = "Custom", custom_value = "value" });

            using var document = JsonDocument.Parse(sceneInfo.ToString());
            var h5Info = document.RootElement.GetProperty("h5_info");

            Assert.AreEqual("Custom", h5Info.GetProperty("type").GetString());
            Assert.AreEqual("value", h5Info.GetProperty("custom_value").GetString());
        }

        private sealed class CustomH5Info : IH5_Info
        {
            public string type { get; set; }

            public string custom_value { get; set; }
        }
    }
}
