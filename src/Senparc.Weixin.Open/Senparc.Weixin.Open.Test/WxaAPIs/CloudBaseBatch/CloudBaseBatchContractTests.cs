using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.Open.WxaAPIs;
using Senparc.Weixin.Open.WxaAPIs.CloudBaseBatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Senparc.Weixin.Open.Test.WxaAPIs.CloudBaseBatch
{
    [TestClass]
    public class CloudBaseBatchContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(CloudBaseBatchApi.ChangeTcbEnv)] = "/tcb/modifyenv",
                [nameof(CloudBaseBatchApi.SetCloudAccessToken)] = "/tcb/usecloudaccesstoken",
                [nameof(CloudBaseBatchApi.GetShareCloudbaseEnv)] = "/componenttcb/batchgetenvid",
                [nameof(CloudBaseBatchApi.GetTcbEnvList)] = "/componenttcb/describeenvs",
                [nameof(CloudBaseBatchApi.CreateTcbEnv)] = "/componenttcb/createenv",
                [nameof(CloudBaseBatchApi.ShareCloudbaseEnv)] = "/componenttcb/batchshareenv",
                [nameof(CloudBaseBatchApi.UnshareCloudbaseEnv)] = "/componenttcb/batchunshareenv",
                [nameof(CloudBaseBatchApi.BatchUploadCloudFunction)] = "/componenttcb/batchuploadscf",
                [nameof(CloudBaseBatchApi.UploadCloudFunctionConfig)] = "/componenttcb/updatescfconfig",
                [nameof(CloudBaseBatchApi.DeleteCloudFunction)] = "/componenttcb/deletescf",
                [nameof(CloudBaseBatchApi.GetCloudFunctionList)] = "/componenttcb/getscflist",
                [nameof(CloudBaseBatchApi.GetTriggers)] = "/componenttcb/gettriggers",
                [nameof(CloudBaseBatchApi.UpdateTriggers)] = "/componenttcb/batchupdatetriggers",
                [nameof(CloudBaseBatchApi.InvokeCloudFunction)] = "/tcb/invokecloudfunction",
                [nameof(CloudBaseBatchApi.UploadCloudFunctionCode)] = "/componenttcb/batchuploadscfcode",
                [nameof(CloudBaseBatchApi.DbImport)] = "/componenttcb/dbimport",
                [nameof(CloudBaseBatchApi.DbExport)] = "/componenttcb/dbexport",
                [nameof(CloudBaseBatchApi.GetMigrationState)] = "/componenttcb/dbmigrationstate",
                [nameof(CloudBaseBatchApi.DbAggregate)] = "/componenttcb/dbaggregate",
                [nameof(CloudBaseBatchApi.GetPermission)] = "/componenttcb/dbgetacl",
                [nameof(CloudBaseBatchApi.SetPermission)] = "/componenttcb/dbmodifyacl",
                [nameof(CloudBaseBatchApi.DbRecordManage)] = "/componenttcb/dbrecord",
                [nameof(CloudBaseBatchApi.DbIndexManage)] = "/componenttcb/dbindex",
                [nameof(CloudBaseBatchApi.GetUploadFileLink)] = "/componenttcb/uploadfile",
                [nameof(CloudBaseBatchApi.DeleteTcbFile)] = "/componenttcb/batchdeletefile",
                [nameof(CloudBaseBatchApi.GetTcbFile)] = "/componenttcb/getbucket",
                [nameof(CloudBaseBatchApi.GetDownloadFileLink)] = "/componenttcb/batchdownloadfile",
                [nameof(CloudBaseBatchApi.GetStaticStore)] = "/componenttcb/describestaticstore",
                [nameof(CloudBaseBatchApi.CreateStaticStore)] = "/componenttcb/createstaticstore",
                [nameof(CloudBaseBatchApi.GetUploadStaticStoreFile)] = "/componenttcb/staticuploadfile",
                [nameof(CloudBaseBatchApi.GetStaticStoreFile)] = "/componenttcb/staticfilelist"
            };

        [TestMethod]
        public void ApiSurfaceContainsThirtyOneOfficialSyncAndAsyncEntries()
        {
            var methods = typeof(CloudBaseBatchApi).GetMethods(BindingFlags.Public | BindingFlags.Static);

            Assert.AreEqual(31, OfficialEndpoints.Count);
            Assert.AreEqual(62, methods.Length, "31 个官方接口均应提供同步和异步入口。");

            foreach (var methodName in OfficialEndpoints.Keys)
            {
                var sync = GetPublicMethod(methodName);
                var async = GetPublicMethod(methodName + "Async");

                Assert.IsNotNull(sync, methodName);
                Assert.IsNotNull(async, methodName + "Async");
                Assert.AreEqual("componentAccessToken", sync.GetParameters()[0].Name, methodName);
                Assert.AreEqual("componentAccessToken", async.GetParameters()[0].Name, methodName + "Async");
            }
        }

        [TestMethod]
        public void EveryPublicEntryUsesOfficialEndpoint()
        {
            foreach (var pair in OfficialEndpoints)
            {
                AssertMethodContainsLiteral(pair.Key, pair.Value);
                AssertMethodContainsLiteral(pair.Key + "Async", pair.Value);
            }
        }

        [TestMethod]
        public void BuildUrlEncodesTokenAndInvokeIdentifiers()
        {
            var buildUrl = typeof(CloudBaseBatchApi).GetMethod("BuildUrl",
                BindingFlags.NonPublic | BindingFlags.Static);
            var buildInvokeUrl = typeof(CloudBaseBatchApi).GetMethod("BuildInvokeUrl",
                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(buildUrl);
            Assert.IsNotNull(buildInvokeUrl);

            var url = (string)buildUrl.Invoke(null,
                new object[] { "token+空 格&x=1", "/componenttcb/getbucket" });
            var invokeUrl = (string)buildInvokeUrl.Invoke(null,
                new object[] { "token", "/tcb/invokecloudfunction", "env & 1", "name+函数" });

            StringAssert.Contains(url, "/componenttcb/getbucket?access_token=");
            Assert.IsFalse(url.Contains("token+空 格&x=1"));
            StringAssert.Contains(url.ToUpperInvariant(), "%26X%3D1");
            StringAssert.Contains(invokeUrl.ToUpperInvariant(), "ENV%20%26%201");
            StringAssert.Contains(invokeUrl.ToUpperInvariant(), "NAME%2B");
            Assert.IsFalse(invokeUrl.Contains("name+函数"));
        }

        [TestMethod]
        public void EnvironmentHelpersAlwaysUseCloudDevelopmentSourceAndRunType()
        {
            var getMethod = typeof(CloudBaseBatchApi).GetMethod("CreateGetShareRequest",
                BindingFlags.NonPublic | BindingFlags.Static);
            var shareMethod = typeof(CloudBaseBatchApi).GetMethod("CreateShareRequest",
                BindingFlags.NonPublic | BindingFlags.Static);
            var createMethod = typeof(CloudBaseBatchApi).GetMethod("CreateEnvRequest",
                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(getMethod);
            Assert.IsNotNull(shareMethod);
            Assert.IsNotNull(createMethod);

            var getRequest = getMethod.Invoke(null, new object[] { new[] { "wx-app-1" } });
            var shareRequest = shareMethod.Invoke(null, new object[]
            {
                new[]
                {
                    new CloudBaseBatchEnvShareItem
                    {
                        env = "env-1",
                        appids = new List<string> { "wx-app-1" }
                    }
                }
            });
            var createRequest = createMethod.Invoke(null, new object[] { "demo" });

            using var getDocument = JsonDocument.Parse(Serialize(getRequest));
            using var shareDocument = JsonDocument.Parse(Serialize(shareRequest));
            using var createDocument = JsonDocument.Parse(Serialize(createRequest));

            Assert.AreEqual(0, getDocument.RootElement.GetProperty("source_type").GetInt32());
            Assert.AreEqual(0, shareDocument.RootElement.GetProperty("source_type").GetInt32());
            Assert.AreEqual("wx-app-1", getDocument.RootElement.GetProperty("appids")[0].GetString());
            Assert.AreEqual("env-1", shareDocument.RootElement.GetProperty("data")[0]
                .GetProperty("env").GetString());
            Assert.AreEqual("run", createDocument.RootElement.GetProperty("EnvType").GetString());
        }

        [TestMethod]
        public void OptionalRequestFieldsAreOmittedAndOfficialShapesArePreserved()
        {
            var config = new CloudBaseBatchFunctionConfigRequest
            {
                env = "env-1",
                functionname = "hello"
            };
            var file = new CloudBaseBatchFileListRequest { env = "env-1" };
            var import = new CloudBaseBatchDbImportRequest
            {
                env = "env-1",
                collection_name = "orders",
                file_path = "orders.json",
                file_type = 1,
                stop_on_error = true,
                conflict_mode = 2
            };

            using var configDocument = JsonDocument.Parse(Serialize(config));
            using var fileDocument = JsonDocument.Parse(Serialize(file));
            using var importDocument = JsonDocument.Parse(Serialize(import));

            Assert.IsFalse(configDocument.RootElement.TryGetProperty("memorysize", out _));
            Assert.IsFalse(configDocument.RootElement.TryGetProperty("public_net_config", out _));
            Assert.IsFalse(fileDocument.RootElement.TryGetProperty("prefix", out _));
            Assert.AreEqual(JsonValueKind.Number, importDocument.RootElement.GetProperty("file_type").ValueKind);
            Assert.AreEqual(JsonValueKind.True, importDocument.RootElement.GetProperty("stop_on_error").ValueKind);
        }

        [TestMethod]
        public void ResponseModelsPreserveOfficialNestedAndLongShapes()
        {
            var migration = JsonConvert.DeserializeObject<CloudBaseBatchMigrationStateJsonResult>(
                "{\"errcode\":0,\"status\":\"success\",\"record_success\":5178368698," +
                "\"record_fail\":2,\"file_url\":\"https://example.test/result\"}");
            var download = JsonConvert.DeserializeObject<CloudBaseBatchDownloadFileJsonResult>(
                "{\"errcode\":0,\"file_list\":[{\"fileid\":\"cloud://a\"," +
                "\"download_url\":\"https://example.test/a\",\"status\":0}]}");
            var store = JsonConvert.DeserializeObject<CloudBaseBatchStaticStoreJsonResult>(
                "{\"errcode\":0,\"Data\":[{\"env\":\"env-1\",\"regoin\":\"ap-shanghai\"}]}");
            var function = JsonConvert.DeserializeObject<CloudBaseBatchFunctionListJsonResult>(
                "{\"errcode\":0,\"total_count\":1,\"functions\":[{\"name\":\"hello\"," +
                "\"status_reason\":[{\"errcode\":\"0\",\"errmsg\":\"ok\"}]}]}");

            Assert.AreEqual(5178368698L, migration.record_success);
            Assert.AreEqual("cloud://a", download.file_list[0].fileid);
            Assert.AreEqual("ap-shanghai", store.data[0].regoin);
            Assert.AreEqual("0", function.functions[0].status_reason[0].errcode);
        }

        private static MethodInfo GetPublicMethod(string methodName)
        {
            return typeof(CloudBaseBatchApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .SingleOrDefault(method => method.Name == methodName);
        }

        private static void AssertMethodContainsLiteral(string methodName, string expected)
        {
            var method = GetPublicMethod(methodName);
            Assert.IsNotNull(method, methodName);
            Assert.IsTrue(GetStringLiterals(method).Contains(expected), $"{methodName}: {expected}");
        }

        private static IEnumerable<string> GetStringLiterals(MethodInfo method)
        {
            var bytes = method?.GetMethodBody()?.GetILAsByteArray();
            if (bytes == null)
            {
                yield break;
            }

            for (var index = 0; index <= bytes.Length - 5; index++)
            {
                if (bytes[index] != 0x72)
                {
                    continue;
                }

                string value;
                try
                {
                    value = method.Module.ResolveString(BitConverter.ToInt32(bytes, index + 1));
                }
                catch (ArgumentException)
                {
                    continue;
                }

                yield return value;
            }
        }

        private static string Serialize(object value)
        {
            return SerializerHelper.GetJsonString(value, new JsonSetting(ignoreNulls: true));
        }
    }
}
