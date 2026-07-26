using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.DeviceData;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.MessageHandlers;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.DeviceData
{
    [TestClass]
    public class DeviceDataContractTests
    {
        [TestMethod]
        public void DeviceDataApiExposesEightSynchronousAndAsynchronousEndpoints()
        {
            var methods = typeof(DeviceDataApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var methodName in new[]
            {
                nameof(DeviceDataApi.GetAuthInfo), nameof(DeviceDataApi.GetCheckinData),
                nameof(DeviceDataApi.GetTemperatureData), nameof(DeviceDataApi.GetAccessControlData),
                nameof(DeviceDataApi.GetAccessControlRule), nameof(DeviceDataApi.AddAccessControlRule),
                nameof(DeviceDataApi.ModifyAccessControlRule), nameof(DeviceDataApi.DeleteAccessControlRule)
            })
            {
                CollectionAssert.Contains(methods, methodName);
                CollectionAssert.Contains(methods, methodName + "Async");
            }
        }

        [TestMethod]
        public void DeviceDataApiUsesOfficialPostPathsDocumentationAndComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "DeviceData", "DeviceDataApi.cs"));

            foreach (var path in new[]
            {
                "/cgi-bin/devicedata/get_auth_info", "/cgi-bin/devicedata/get_checkin_data",
                "/cgi-bin/devicedata/get_temperature_data", "/cgi-bin/devicedata/get_accesscontrol_data",
                "/cgi-bin/devicedata/get_accesscontrol_rule", "/cgi-bin/devicedata/add_accesscontrol_rule",
                "/cgi-bin/devicedata/mod_accesscontrol_rule", "/cgi-bin/devicedata/del_accesscontrol_rule"
            })
            {
                Assert.AreEqual(1, CountOccurrences(source, path), path);
            }

            foreach (var documentId in new[]
            {
                "96097", "96027", "96028", "96029", "96030", "96031", "96221", "96227"
            })
            {
                Assert.AreEqual(2, CountOccurrences(source, "document/path/" + documentId), documentId);
            }

            Assert.AreEqual(17, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(2, CountOccurrences(source, "CommonJsonSendType.POST"));
        }

        [TestMethod]
        public void AuthAndDataModelsMatchOfficialSamplesAndPreserveLargeTimestamps()
        {
            Assert.AreEqual("{\"agentid\":1}",
                JsonSerializer.Serialize(new DeviceDataGetAuthInfoRequest { agentid = 1 }));

            var queryJson = JsonSerializer.Serialize(new DeviceDataQueryRequest
            {
                agentid = 10000,
                user_type = 0,
                begin_time = 4294967296L,
                end_time = 5000000000L,
                data_filter_type = 1,
                device_sn_list = new List<string> { "SN1", "SN2" },
                open_userid_list = new List<string> { "user-1" },
                cursor = "CURSOR",
                limit = 100
            });
            StringAssert.Contains(queryJson, "\"begin_time\":4294967296");
            StringAssert.Contains(queryJson, "\"end_time\":5000000000");
            StringAssert.Contains(queryJson, "\"open_userid_list\":[\"user-1\"]");

            var auth = Newtonsoft.Json.JsonConvert.DeserializeObject<DeviceDataGetAuthInfoResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"device_list\":{\"item\":[{" +
                "\"device_sn\":\"SN\",\"remark_name\":\"remark\",\"default_name\":\"default\"," +
                "\"model_name\":\"MODEL\",\"device_type\":1}]}}");
            var checkin = Newtonsoft.Json.JsonConvert.DeserializeObject<DeviceDataGetCheckinDataResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"checkindata\":{\"items\":[{" +
                "\"open_userid\":\"x1\",\"checkin_time\":5000000000,\"device_sn\":\"SN\"}]}," +
                "\"next_cursor\":\"NEXT\"}");
            var temperature = Newtonsoft.Json.JsonConvert.DeserializeObject<DeviceDataGetTemperatureDataResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"temperature_data\":{\"items\":[{" +
                "\"open_userid\":\"x1\",\"user_type\":0,\"timestamp\":5000000001," +
                "\"temperature\":\"36.7\",\"status\":0,\"device_sn\":\"SN\"}]}," +
                "\"next_cursor\":\"NEXT\"}");
            var access = Newtonsoft.Json.JsonConvert.DeserializeObject<DeviceDataGetAccessControlDataResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"accesscontrol_data\":{\"items\":[{" +
                "\"open_userid\":\"x1\",\"user_type\":0,\"timestamp\":5000000002," +
                "\"pass_type\":1,\"pass_method\":0,\"device_sn\":\"SN\"}]}," +
                "\"next_cursor\":\"NEXT\"}");

            Assert.AreEqual("MODEL", auth.device_list.item[0].model_name);
            Assert.AreEqual(5000000000L, checkin.checkindata.items[0].checkin_time);
            Assert.AreEqual("36.7", temperature.temperature_data.items[0].temperature);
            Assert.AreEqual(5000000001L, temperature.temperature_data.items[0].timestamp);
            Assert.AreEqual(5000000002L, access.accesscontrol_data.items[0].timestamp);
            Assert.AreEqual(0, access.accesscontrol_data.items[0].pass_method);
        }

        [TestMethod]
        public void AccessControlRuleModelsMatchOfficialSamplesAndRemainStronglyTyped()
        {
            var request = new DeviceDataModifyAccessControlRuleRequest
            {
                rule_id = "rule-1",
                rule_name = "工作日规则",
                device_sn_list = new List<string> { "SN" },
                pass_rule = new DeviceDataAccessControlPassRule
                {
                    rule_list = new List<string> { "9:00-10:00 * * 1-5 *" },
                    effect_open_userid_list = new List<DeviceDataAccessControlEffectUser>
                    {
                        new DeviceDataAccessControlEffectUser { open_userid = "user-1", user_type = 0 }
                    }
                }
            };
            var requestJson = JsonSerializer.Serialize(request);
            StringAssert.Contains(requestJson, "\"rule_id\":\"rule-1\"");
            StringAssert.Contains(requestJson, "\"rule_list\":[\"9:00-10:00 * * 1-5 *\"]");
            StringAssert.Contains(requestJson, "\"effect_open_userid_list\":[{\"open_userid\":\"user-1\"," +
                "\"user_type\":0}]");

            var rules = Newtonsoft.Json.JsonConvert.DeserializeObject<DeviceDataGetAccessControlRuleResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"pass_rule\":{\"items\":[{" +
                "\"rule_id\":\"rule-1\",\"name\":\"工作日规则\"," +
                "\"rule_list\":[\"9:00-10:00 * * 1-5 *\"],\"effect_time\":5000000000," +
                "\"effect_open_userid_list\":[{\"open_userid\":\"user-1\",\"user_type\":0}]}]}," +
                "\"remote_pass_rule\":{\"items\":[]}}");
            var addResult = Newtonsoft.Json.JsonConvert.DeserializeObject<DeviceDataAddAccessControlRuleResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"rule_id\":\"rule-1\"," +
                "\"invalid_list\":[\"bad-user\"]}");

            Assert.AreEqual(5000000000L, rules.pass_rule.items[0].effect_time);
            Assert.AreEqual("user-1", rules.pass_rule.items[0].effect_open_userid_list[0].open_userid);
            Assert.AreEqual("bad-user", addResult.invalid_list[0]);

            var modelSource = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "DeviceData",
                "DeviceDataJson.cs"));
            Assert.AreEqual(89, CountOccurrences(modelSource, "/// <summary>"));
            Assert.IsFalse(modelSource.Contains("object "));
        }

        [TestMethod]
        public void DeviceDataAuthChangeCallbackMapsToStrongTypeAndHandlerHooks()
        {
            var document = XDocument.Parse(@"<xml>
<SuiteId><![CDATA[ww-suite]]></SuiteId>
<AuthCorpId><![CDATA[ww-corp]]></AuthCorpId>
<InfoType><![CDATA[device_data_auth_change]]></InfoType>
<TimeStamp>5000000000</TimeStamp>
<AgentID>1000001</AgentID>
</xml>");
            var callback = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), document)
                as RequestMessageInfo_Device_Data_Auth_Change;
            var handlerMethods = typeof(WorkMessageHandler<>).GetMethods(
                    BindingFlags.Instance | BindingFlags.NonPublic)
                .Select(method => method.Name).ToArray();

            Assert.IsNotNull(callback);
            Assert.AreEqual(ThirdPartyInfo.DEVICE_DATA_AUTH_CHANGE, callback.InfoType);
            Assert.AreEqual("ww-suite", callback.SuiteId);
            Assert.AreEqual("ww-corp", callback.AuthCorpId);
            Assert.AreEqual("5000000000", callback.TimeStamp);
            Assert.AreEqual(1000001, callback.AgentID);
            CollectionAssert.Contains(handlerMethods, "OnThirdPartyEvent_DeviceDataAuthChange");
            CollectionAssert.Contains(handlerMethods, "OnThirdPartyEvent_DeviceDataAuthChangeAsync");

            var callbackSource = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "Entities", "Request",
                "ThirdPartyInfo", "RequestMessageInfo_Device_Data_Auth_Change.cs"));
            Assert.AreEqual(3, CountOccurrences(callbackSource, "/// <summary>"));
            StringAssert.Contains(callbackSource, "document/path/96103");
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
