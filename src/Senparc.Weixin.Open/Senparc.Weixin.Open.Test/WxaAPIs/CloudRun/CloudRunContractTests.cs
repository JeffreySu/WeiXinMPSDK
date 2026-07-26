using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.Open.WxaAPIs;
using Senparc.Weixin.Open.WxaAPIs.CloudRun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Senparc.Weixin.Open.Test.WxaAPIs.CloudRun
{
    [TestClass]
    public class CloudRunContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(CloudRunApi.GetShareCloudbaseEnv)] = "/componenttcb/batchgetenvid",
                [nameof(CloudRunApi.ShareCloudbaseEnv)] = "/componenttcb/batchshareenv",
                [nameof(CloudRunApi.UnshareCloudbaseEnv)] = "/componenttcb/batchunshareenv",
                [nameof(CloudRunApi.GetWxCloudBaseRunEnvs)] = "/componenttcb/describeenvs",
                [nameof(CloudRunApi.CreateCloudbaseEnv)] = "/componenttcb/createcloudbaserunenv",
                [nameof(CloudRunApi.CreateCloudbaseService)] = "/componenttcb/establishcloudbaserunserver",
                [nameof(CloudRunApi.CreateCloudbaseServiceVersion)] = "/componenttcb/createcloudbaserunserverversion",
                [nameof(CloudRunApi.UpdateCloudbaseServiceVersion)] = "/componenttcb/rollupdatecloudbaserunserverversion",
                [nameof(CloudRunApi.DeleteCloudbaseServiceVersion)] = "/componenttcb/deletecloudbaserunserverversion",
                [nameof(CloudRunApi.ReleaseCloudbaseServiceVersion)] = "/componenttcb/releasecloudbaserunversion"
            };

        [TestMethod]
        public void ApiSurfaceContainsTenOfficialSyncAndAsyncEntries()
        {
            var methods = typeof(CloudRunApi).GetMethods(BindingFlags.Public | BindingFlags.Static);

            Assert.AreEqual(10, OfficialEndpoints.Count);
            Assert.AreEqual(20, methods.Length, "10 个官方接口均应提供同步和异步入口。");

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
        public void BuildUrlEncodesComponentAccessToken()
        {
            var buildUrl = typeof(CloudRunApi).GetMethod("BuildUrl", BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(buildUrl);

            var url = (string)buildUrl.Invoke(null,
                new object[] { "token+空 格&x=1", "/componenttcb/describeenvs" });

            StringAssert.Contains(url, "/componenttcb/describeenvs?access_token=");
            Assert.IsFalse(url.Contains("token+空 格&x=1"));
            StringAssert.Contains(url.ToUpperInvariant(), "%E7%A9%BA");
            StringAssert.Contains(url.ToUpperInvariant(), "%26X%3D1");
        }

        [TestMethod]
        public void ShareRequestsAlwaysIncludeCloudRunSourceType()
        {
            var getMethod = typeof(CloudRunApi).GetMethod("CreateGetShareRequest",
                BindingFlags.NonPublic | BindingFlags.Static);
            var shareMethod = typeof(CloudRunApi).GetMethod("CreateShareRequest",
                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(getMethod);
            Assert.IsNotNull(shareMethod);

            var getRequest = getMethod.Invoke(null, new object[] { new[] { "wx-app-1" } });
            var shareRequest = shareMethod.Invoke(null, new object[]
            {
                new[]
                {
                    new CloudRunEnvShareItem
                    {
                        env = "env-1",
                        appids = new List<string> { "wx-app-1" }
                    }
                }
            });

            using var getDocument = JsonDocument.Parse(Serialize(getRequest));
            using var shareDocument = JsonDocument.Parse(Serialize(shareRequest));

            Assert.AreEqual(1, getDocument.RootElement.GetProperty("source_type").GetInt32());
            Assert.AreEqual("wx-app-1", getDocument.RootElement.GetProperty("appids")[0].GetString());
            Assert.AreEqual(1, shareDocument.RootElement.GetProperty("source_type").GetInt32());
            Assert.AreEqual("env-1", shareDocument.RootElement.GetProperty("data")[0]
                .GetProperty("env").GetString());
        }

        [TestMethod]
        public void RequestModelsPreserveOfficialOptionalAndStringShapes()
        {
            var createEnv = new CloudRunCreateEnvRequest { alias = "demo-env" };
            var updateVersion = new CloudRunUpdateServiceVersionRequest
            {
                env_id = "env-1",
                version_name = "latest",
                cpu = "0.5",
                mem = "1",
                min_num = "0",
                max_num = "10",
                policy_threshold = "60"
            };

            using var envDocument = JsonDocument.Parse(Serialize(createEnv));
            using var versionDocument = JsonDocument.Parse(Serialize(updateVersion));

            Assert.IsFalse(envDocument.RootElement.TryGetProperty("vpc_id", out _));
            Assert.IsFalse(envDocument.RootElement.TryGetProperty("sub_net_ids", out _));
            Assert.AreEqual(JsonValueKind.String, versionDocument.RootElement.GetProperty("cpu").ValueKind,
                "官方更新接口示例以字符串传递资源规格。");
            Assert.AreEqual("0.5", versionDocument.RootElement.GetProperty("cpu").GetString());
            Assert.IsFalse(versionDocument.RootElement.TryGetProperty("rollback", out _));
        }

        [TestMethod]
        public void ResponseModelsPreserveNestedListsAndLongReleaseOrderId()
        {
            var share = JsonConvert.DeserializeObject<CloudRunGetShareEnvJsonResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"relation_data\":[{" +
                "\"appid\":\"wx-app-1\",\"env_list\":[\"env-1\"]}],\"err_list\":[]}");
            var envs = JsonConvert.DeserializeObject<CloudRunEnvListJsonResult>(
                "{\"errcode\":0,\"info_list\":[{\"env\":\"env-1\",\"alias\":\"生产环境\"," +
                "\"package_id\":\"pkg-1\"}]}");
            var release = JsonConvert.DeserializeObject<CloudRunReleaseJsonResult>(
                "{\"errcode\":0,\"result\":\"success\",\"release_order_id\":5178368698}");

            Assert.AreEqual("env-1", share.relation_data[0].env_list[0]);
            Assert.AreEqual("pkg-1", envs.info_list[0].package_id);
            Assert.AreEqual(5178368698L, release.release_order_id);
        }

        private static MethodInfo GetPublicMethod(string methodName)
        {
            return typeof(CloudRunApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
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
