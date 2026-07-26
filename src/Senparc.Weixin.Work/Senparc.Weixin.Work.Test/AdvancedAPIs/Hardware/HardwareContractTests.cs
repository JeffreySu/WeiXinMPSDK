using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Hardware;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.MessageHandlers;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Hardware
{
    [TestClass]
    public class HardwareContractTests
    {
        [TestMethod]
        public void HardwareApiUsesOfficialPathProviderTokenAndDocument()
        {
            var methods = typeof(HardwareApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "Hardware",
                "HardwareApi.cs"));

            CollectionAssert.Contains(methods, nameof(HardwareApi.GetDeviceFeature));
            CollectionAssert.Contains(methods, nameof(HardwareApi.GetDeviceFeatureAsync));
            Assert.AreEqual(1, CountOccurrences(source,
                "/cgi-bin/hardware/get_device_feature?provider_access_token={0}"));
            Assert.AreEqual(2, CountOccurrences(source, "document/path/92739"));
            Assert.AreEqual(2, CountOccurrences(source, "CommonJsonSendType.POST"));
            Assert.AreEqual(3, CountOccurrences(source, "/// <summary>"));
        }

        [TestMethod]
        public void HardwareModelsPreserveProviderDefinedFeatureStringAndComments()
        {
            Assert.AreEqual("{\"device_sn\":\"SN\"}",
                JsonSerializer.Serialize(new HardwareGetDeviceFeatureRequest { device_sn = "SN" }));

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<HardwareGetDeviceFeatureResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\"," +
                "\"device_feature\":\"{\\\"printer_type\\\":\\\"CM12838-W2\\\"}\"}");

            Assert.AreEqual("{\"printer_type\":\"CM12838-W2\"}", result.device_feature);

            var modelSource = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "Hardware",
                "HardwareJson.cs"));
            Assert.AreEqual(4, CountOccurrences(modelSource, "/// <summary>"));
            Assert.IsFalse(modelSource.Contains("object "));
            StringAssert.Contains(modelSource, "结构由设备厂商定义");
        }

        [TestMethod]
        public void DeviceFeatureChangeCallbackMapsToStrongTypeAndHandlerHooks()
        {
            var document = XDocument.Parse(@"<xml>
<InfoType><![CDATA[device_feature_change]]></InfoType>
<TimeStamp>5000000000</TimeStamp>
<AuthCorpId><![CDATA[ww-corp]]></AuthCorpId>
<ServiceCorpId><![CDATA[ww-service]]></ServiceCorpId>
<DeviceSn><![CDATA[SN]]></DeviceSn>
</xml>");
            var callback = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), document)
                as RequestMessageInfo_Device_Feature_Change;
            var handlerMethods = typeof(WorkMessageHandler<>).GetMethods(
                    BindingFlags.Instance | BindingFlags.NonPublic)
                .Select(method => method.Name).ToArray();

            Assert.IsNotNull(callback);
            Assert.AreEqual(ThirdPartyInfo.DEVICE_FEATURE_CHANGE, callback.InfoType);
            Assert.AreEqual("5000000000", callback.TimeStamp);
            Assert.AreEqual("ww-corp", callback.AuthCorpId);
            Assert.AreEqual("ww-service", callback.ServiceCorpId);
            Assert.AreEqual("SN", callback.DeviceSn);
            CollectionAssert.Contains(handlerMethods, "OnThirdPartyEvent_DeviceFeatureChange");
            CollectionAssert.Contains(handlerMethods, "OnThirdPartyEvent_DeviceFeatureChangeAsync");

            var callbackSource = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "Entities", "Request",
                "ThirdPartyInfo", "RequestMessageInfo_Device_Feature_Change.cs"));
            Assert.AreEqual(5, CountOccurrences(callbackSource, "/// <summary>"));
            StringAssert.Contains(callbackSource, "document/path/90751");
        }

        private static int CountOccurrences(string value, string search)
        {
            return value.Split(new[] { search }, StringSplitOptions.None).Length - 1;
        }

        private static string FindRepositoryRoot([CallerFilePath] string sourceFilePath = null)
        {
            foreach (var startPath in new[]
            {
                Directory.GetCurrentDirectory(),
                AppContext.BaseDirectory,
                Path.GetDirectoryName(sourceFilePath)
            })
            {
                var directory = string.IsNullOrEmpty(startPath) ? null : new DirectoryInfo(startPath);
                while (directory != null)
                {
                    if (Directory.Exists(Path.Combine(directory.FullName, ".git")))
                    {
                        return directory.FullName;
                    }

                    directory = directory.Parent;
                }
            }

            Assert.Fail("无法定位仓库根目录。");
            return null;
        }
    }
}
