using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.Open.WxaAPIs;
using Senparc.Weixin.Open.WxaAPIs.CloudBase;
using Senparc.Weixin.Open.WxaAPIs.CloudBaseBatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Senparc.Weixin.Open.Test.WxaAPIs.CloudBase
{
    [TestClass]
    public class CloudBaseContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(CloudBaseApi.SetCloudAccessToken)] = "/tcb/usecloudaccesstoken",
                [nameof(CloudBaseApi.CreateCloudUser)] = "/tcb/createclouduser",
                [nameof(CloudBaseApi.GetCloudToken)] = "/tcb/getqcloudtoken",
                [nameof(CloudBaseApi.CheckMobileConfig)] = "/tcb/checkmobile",
                [nameof(CloudBaseApi.ChangeTcbEnv)] = "/tcb/modifyenv",
                [nameof(CloudBaseApi.CreateEnv)] = "/tcb/createenvandresource",
                [nameof(CloudBaseApi.GetEnvInfo)] = "/tcb/getenvinfo",
                [nameof(CloudBaseApi.ShareEnv)] = "/componenttcb/batchshareenv",
                [nameof(CloudBaseApi.SetCallBackConfig)] = "/tcb/setcallbackconfig",
                [nameof(CloudBaseApi.GetCallBackConfig)] = "/tcb/getcallbackconfig",
                [nameof(CloudBaseApi.InvokeCloudFunction)] = "/tcb/invokecloudfunction",
                [nameof(CloudBaseApi.CreateFunction)] = "/componenttcb/batchuploadscf",
                [nameof(CloudBaseApi.GetCodeSecret)] = "/tcb/getcodesecret",
                [nameof(CloudBaseApi.GetUploadSignature)] = "/tcb/getuploadsignature",
                [nameof(CloudBaseApi.GetFunctionList)] = "/tcb/listfunctions",
                [nameof(CloudBaseApi.GetFunctionLink)] = "/tcb/downloadfunction",
                [nameof(CloudBaseApi.UploadFunctionConfig)] = "/tcb/uploadfuncconfig",
                [nameof(CloudBaseApi.GetFunctionConfig)] = "/tcb/getfuncconfig",
                [nameof(CloudBaseApi.GetUploadTcbFileLink)] = "/tcb/uploadfile",
                [nameof(CloudBaseApi.DeleteTcbCloudFile)] = "/componenttcb/batchdeletefile",
                [nameof(CloudBaseApi.GetDownloadTcbFileLink)] = "/tcb/batchdownloadfile",
                [nameof(CloudBaseApi.AggregateDatabase)] = "/componenttcb/dbaggregate",
                [nameof(CloudBaseApi.GetDatabaseMigrateStatus)] = "/tcb/databasemigratequeryinfo",
                [nameof(CloudBaseApi.UpdateDatabaseRecord)] = "/tcb/databaseupdate",
                [nameof(CloudBaseApi.DbCollectionManage)] = "/componenttcb/dbcollection",
                [nameof(CloudBaseApi.AddDatabaseItem)] = "/tcb/databaseadd",
                [nameof(CloudBaseApi.AddDatabaseCollection)] = "/tcb/databasecollectionadd",
                [nameof(CloudBaseApi.DeleteDatabaseCollection)] = "/tcb/databasecollectiondelete",
                [nameof(CloudBaseApi.GetDatabaseCollection)] = "/tcb/databasecollectionget",
                [nameof(CloudBaseApi.GetDatabaseCount)] = "/tcb/databasecount",
                [nameof(CloudBaseApi.DeleteDatabaseItem)] = "/tcb/databasedelete",
                [nameof(CloudBaseApi.ExportDatabaseItem)] = "/componenttcb/dbexport",
                [nameof(CloudBaseApi.ImportDatabaseItem)] = "/componenttcb/dbimport",
                [nameof(CloudBaseApi.GetDatabaseRecord)] = "/tcb/databasequery",
                [nameof(CloudBaseApi.UpdateDatabaseIndex)] = "/tcb/updateindex",
                [nameof(CloudBaseApi.GetWechatPayList)] = "/tcb/wxpaylist",
                [nameof(CloudBaseApi.GetWechatPayAuth)] = "/tcb/wxpayopenauth"
            };

        private static readonly ISet<string> ComponentTokenMethods = new HashSet<string>
        {
            nameof(CloudBaseApi.ShareEnv),
            nameof(CloudBaseApi.CreateFunction),
            nameof(CloudBaseApi.DeleteTcbCloudFile),
            nameof(CloudBaseApi.AggregateDatabase),
            nameof(CloudBaseApi.DbCollectionManage),
            nameof(CloudBaseApi.ExportDatabaseItem),
            nameof(CloudBaseApi.ImportDatabaseItem)
        };

        [TestMethod]
        public void ApiSurfaceContainsThirtySevenOfficialSyncAndAsyncEntries()
        {
            var methods = typeof(CloudBaseApi).GetMethods(BindingFlags.Public | BindingFlags.Static);

            Assert.AreEqual(37, OfficialEndpoints.Count,
                "2026-07-24 官方普通代云开发目录包含 37 项，而不是旧审计中的 36 项。");
            Assert.AreEqual(74, methods.Length, "37 个官方接口均应提供同步和异步入口。");

            foreach (var methodName in OfficialEndpoints.Keys)
            {
                var sync = GetPublicMethod(methodName);
                var async = GetPublicMethod(methodName + "Async");
                var tokenName = ComponentTokenMethods.Contains(methodName)
                    ? "componentAccessToken"
                    : "authorizerAccessToken";

                Assert.IsNotNull(sync, methodName);
                Assert.IsNotNull(async, methodName + "Async");
                Assert.AreEqual(tokenName, sync.GetParameters()[0].Name, methodName);
                Assert.AreEqual(tokenName, async.GetParameters()[0].Name, methodName + "Async");
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
            var buildUrl = typeof(CloudBaseApi).GetMethod("BuildUrl",
                BindingFlags.NonPublic | BindingFlags.Static);
            var buildInvokeUrl = typeof(CloudBaseApi).GetMethod("BuildInvokeUrl",
                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(buildUrl);
            Assert.IsNotNull(buildInvokeUrl);

            var url = (string)buildUrl.Invoke(null,
                new object[] { "token+空 格&x=1", "/tcb/getenvinfo" });
            var invokeUrl = (string)buildInvokeUrl.Invoke(null,
                new object[] { "token", "/tcb/invokecloudfunction", "env & 1", "name+函数" });

            StringAssert.Contains(url, "/tcb/getenvinfo?access_token=");
            Assert.IsFalse(url.Contains("token+空 格&x=1"));
            StringAssert.Contains(url.ToUpperInvariant(), "%26X%3D1");
            StringAssert.Contains(invokeUrl.ToUpperInvariant(), "ENV%20%26%201");
            StringAssert.Contains(invokeUrl.ToUpperInvariant(), "NAME%2B");
            Assert.IsFalse(invokeUrl.Contains("name+函数"));
        }

        [TestMethod]
        public void EnvironmentHelpersPreserveOfficialFixedValues()
        {
            var createMethod = typeof(CloudBaseApi).GetMethod("CreateEnvRequest",
                BindingFlags.NonPublic | BindingFlags.Static);
            var shareMethod = typeof(CloudBaseApi).GetMethod("CreateShareRequest",
                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(createMethod);
            Assert.IsNotNull(shareMethod);

            var createRequest = createMethod.Invoke(null, new object[] { "demo" });
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

            using var createDocument = JsonDocument.Parse(Serialize(createRequest));
            using var shareDocument = JsonDocument.Parse(Serialize(shareRequest));

            Assert.AreEqual("CreatePostpayPackage", createDocument.RootElement.GetProperty("type").GetString());
            Assert.AreEqual("demo", createDocument.RootElement.GetProperty("alias").GetString());
            Assert.AreEqual(0, shareDocument.RootElement.GetProperty("source_type").GetInt32());
            Assert.AreEqual("wx-app-1", shareDocument.RootElement.GetProperty("data")[0]
                .GetProperty("appids")[0].GetString());
        }

        [TestMethod]
        public void OptionalFieldsAreOmittedAndProtocolCasingIsPreserved()
        {
            var callback = new CloudBaseCallbackConfig
            {
                function_config = new CloudBaseFunctionCallbackConfig
                {
                    enable = true,
                    callbacks = new List<CloudBaseFunctionCallbackItem>
                    {
                        new CloudBaseFunctionCallbackItem
                        {
                            msgType = "event",
                            @event = "user_enter_tempsession",
                            env = "env-1",
                            functionName = "callback",
                            enable = true
                        }
                    }
                }
            };
            var index = new CloudBaseUpdateIndexRequest
            {
                env = "env-1",
                collection_name = "orders",
                drop_indexes = new List<CloudBaseIndexDefinition>
                {
                    new CloudBaseIndexDefinition { name = "old_index" }
                }
            };
            var collection = new CloudBaseCollectionManageRequest
            {
                env = "env-1",
                action = "get"
            };

            using var callbackDocument = JsonDocument.Parse(Serialize(callback));
            using var indexDocument = JsonDocument.Parse(Serialize(index));
            using var collectionDocument = JsonDocument.Parse(Serialize(collection));

            Assert.IsFalse(callbackDocument.RootElement.TryGetProperty("container_config", out _));
            Assert.AreEqual("event", callbackDocument.RootElement.GetProperty("function_config")
                .GetProperty("callbacks")[0].GetProperty("msgType").GetString());
            Assert.AreEqual("callback", callbackDocument.RootElement.GetProperty("function_config")
                .GetProperty("callbacks")[0].GetProperty("functionName").GetString());
            Assert.IsFalse(indexDocument.RootElement.GetProperty("drop_indexes")[0]
                .TryGetProperty("unique", out _));
            Assert.IsFalse(collectionDocument.RootElement.TryGetProperty("collection_name", out _));
            Assert.IsFalse(collectionDocument.RootElement.TryGetProperty("limit", out _));
        }

        [TestMethod]
        public void ResponseModelsPreserveOfficialWireShapes()
        {
            var pay = JsonConvert.DeserializeObject<CloudBaseWechatPayListJsonResult>(
                "{\"errcode\":0,\"list\":[{\"merchant_code\":\"1900006511\"," +
                "\"mch_relation_state\":\"RELATION_BINDED\",\"jsapi_auth_state\":\"AUTH_AUTHORIZED\"}]}");
            var query = JsonConvert.DeserializeObject<CloudBaseDatabaseQueryJsonResult>(
                "{\"errcode\":0,\"pager\":{\"Offset\":1,\"Limit\":10,\"Total\":5178368698}," +
                "\"data\":[\"{\\\"_id\\\":\\\"id-1\\\"}\"]}");
            var callback = JsonConvert.DeserializeObject<CloudBaseCallbackConfigJsonResult>(
                "{\"errcode\":0,\"data\":{\"function_config\":{\"enable\":true," +
                "\"callbacks\":[{\"msgType\":\"image\",\"functionName\":\"receive\"}]}}}");
            var token = JsonConvert.DeserializeObject<CloudBaseQCloudTokenJsonResult>(
                "{\"errcode\":0,\"secretid\":\"id\",\"expired_time\":5178368698}");

            Assert.AreEqual("RELATION_BINDED", pay.list[0].mch_relation_state,
                "官方参数表写 number，但实际响应示例是字符串枚举。");
            Assert.AreEqual(5178368698L, query.pager.Total);
            Assert.AreEqual("image", callback.data.function_config.callbacks[0].msgType);
            Assert.AreEqual(5178368698L, token.expired_time);
        }

        private static MethodInfo GetPublicMethod(string methodName)
        {
            return typeof(CloudBaseApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
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
