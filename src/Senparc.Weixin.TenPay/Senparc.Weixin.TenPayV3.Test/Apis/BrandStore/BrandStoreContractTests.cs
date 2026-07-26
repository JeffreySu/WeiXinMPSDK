using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BrandStore;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.Apis.BrandStore
{
    [TestClass]
    public class BrandStoreContractTests
    {
        private static readonly IReadOnlyDictionary<string, string>
            OfficialEndpoints = new Dictionary<string, string>
            {
                [nameof(BrandStoreApis.CreateBrandStoreAsync)] =
                    "brand/store/brandstores",
                [nameof(BrandStoreApis.QueryBrandStoreAsync)] =
                    "brand/store/brandstores/",
                [nameof(BrandStoreApis.QueryBrandStoresAsync)] =
                    "brand/store/brandstores",
                [nameof(BrandStoreApis.UpdateBrandStoreAsync)] =
                    "brand/store/brandstores/",
                [nameof(BrandStoreApis.DeleteBrandStoreAsync)] =
                    "brand/store/brandstores/",
                [nameof(BrandStoreApis.CloseBrandStoreAsync)] = "/close",
                [nameof(BrandStoreApis.ResumeBrandStoreAsync)] = "/resume",
                [nameof(BrandStoreApis.BindRecipientAsync)] =
                    "/bindrecipient",
                [nameof(BrandStoreApis.UnbindRecipientAsync)] =
                    "/unbindrecipient"
            };

        [TestMethod]
        public void ApiSurfaceContainsAllNineOfficialEntries()
        {
            var methods = typeof(BrandStoreApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.DeclaringType == typeof(BrandStoreApis))
                .Select(method => method.Name)
                .Distinct()
                .ToArray();

            Assert.AreEqual(9, OfficialEndpoints.Count);
            CollectionAssert.AreEquivalent(OfficialEndpoints.Keys.ToArray(),
                methods);
        }

        [TestMethod]
        public void EveryMethodContainsCurrentOfficialEndpoint()
        {
            foreach (var endpoint in OfficialEndpoints)
            {
                var method = typeof(BrandStoreApis).GetMethod(endpoint.Key,
                    BindingFlags.Public | BindingFlags.Instance);

                Assert.IsNotNull(method, endpoint.Key);
                Assert.IsTrue(GetStringLiterals(method)
                        .Any(value => value.Contains(endpoint.Value)),
                    $"{endpoint.Key}: {endpoint.Value}");
            }
        }

        [TestMethod]
        public void QueryAndPathValuesAreEncodedAndNullValuesAreSkipped()
        {
            var buildQuery = typeof(BrandStoreApis).GetMethod(
                "BuildBrandStoreQuery",
                BindingFlags.NonPublic | BindingFlags.Static);
            var escape = typeof(BrandStoreApis).GetMethod(
                "EscapeBrandStoreValue",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(buildQuery);
            Assert.IsNotNull(escape);
            var path = (string)buildQuery.Invoke(null, new object[]
            {
                "brand/store/brandstores",
                new[]
                {
                    "store_state", "OPEN + READY",
                    "offset", "0",
                    "optional", null,
                    "limit", "200"
                }
            });

            Assert.AreEqual(
                "brand/store/brandstores?store_state=OPEN%20%2B%20READY&offset=0&limit=200",
                path);
            Assert.AreEqual("store%20%2B%20id",
                escape.Invoke(null, new object[] { "store + id" }));
        }

        [TestMethod]
        public void HttpMethodsMatchPatchAndBodylessOfficialContracts()
        {
            var update = typeof(BrandStoreApis).GetMethod(
                nameof(BrandStoreApis.UpdateBrandStoreAsync));
            var delete = typeof(BrandStoreApis).GetMethod(
                nameof(BrandStoreApis.DeleteBrandStoreAsync));
            var close = typeof(BrandStoreApis).GetMethod(
                nameof(BrandStoreApis.CloseBrandStoreAsync));
            var resume = typeof(BrandStoreApis).GetMethod(
                nameof(BrandStoreApis.ResumeBrandStoreAsync));

            CollectionAssert.Contains(GetCalledMethodNames(update).ToArray(),
                "PatchAsync");
            CollectionAssert.Contains(GetCalledMethodNames(delete).ToArray(),
                "RequestWithoutBodyAsync");
            CollectionAssert.Contains(GetCalledMethodNames(close).ToArray(),
                "RequestWithoutBodyAsync");
            CollectionAssert.Contains(GetCalledMethodNames(resume).ToArray(),
                "RequestWithoutBodyAsync");
        }

        [TestMethod]
        public void CreateUpdateAndRecipientRequestsPreserveOfficialFields()
        {
            var create = JObject.Parse(JsonConvert.SerializeObject(
                new BrandStoreCreateRequestData
                {
                    store_basics = new BrandStoreBasics
                    {
                        store_reference_id = "store_10001",
                        branch_name = "科技园店"
                    },
                    store_address = new BrandStoreAddress
                    {
                        address_code = "440305",
                        address_detail = "科技园南区 1 号",
                        address_complements = "一层 101",
                        longitude = "113.946900",
                        latitude = "22.540700"
                    },
                    store_business = new BrandStoreBusiness
                    {
                        service_phone = "0755-12345678|400-123-4567",
                        business_hours = "周一至周日 09:00-22:00"
                    }
                }, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));
            var update = JObject.Parse(JsonConvert.SerializeObject(
                new BrandStoreUpdateRequestData
                {
                    store_basics = new BrandStoreBasics
                    {
                        branch_name = "科技园旗舰店"
                    }
                }, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));
            var bind = JObject.FromObject(
                new BrandStoreBindRecipientRequestData
                {
                    mchid = "1230000109",
                    company_name = "腾讯科技（深圳）有限公司"
                });
            var unbind = JObject.FromObject(
                new BrandStoreUnbindRecipientRequestData
                {
                    mchid = "1230000109"
                });

            Assert.AreEqual("store_10001",
                create["store_basics"]?["store_reference_id"]?.Value<string>());
            Assert.AreEqual("440305",
                create["store_address"]?["address_code"]?.Value<string>());
            Assert.AreEqual("0755-12345678|400-123-4567",
                create["store_business"]?["service_phone"]?.Value<string>());
            Assert.AreEqual("科技园旗舰店",
                update["store_basics"]?["branch_name"]?.Value<string>());
            Assert.IsNull(update["store_address"]);
            Assert.AreEqual("腾讯科技（深圳）有限公司",
                bind["company_name"]?.Value<string>());
            Assert.AreEqual(1, unbind.Properties().Count());
        }

        [TestMethod]
        public void ResultModelsPreserveStoreAuditAndRecipientStates()
        {
            var list = JsonConvert.DeserializeObject<BrandStoreListResultJson>(
                "{\"data\":[{\"store_id\":\"1234567890123456\"," +
                "\"store_state\":\"OPEN\",\"audit_state\":\"REJECTED\"," +
                "\"review_reject_reason\":\"地址不完整\"," +
                "\"store_recipient\":[{\"mchid\":\"1230000109\"," +
                "\"company_name\":\"示例主体\"," +
                "\"recipient_state\":\"CONFIRMING\"}]}]," +
                "\"offset\":0,\"limit\":20,\"total_count\":5000000000}");
            var state = JsonConvert
                .DeserializeObject<BrandStoreStateResultJson>(
                    "{\"store_id\":\"1234567890123456\"," +
                    "\"store_state\":\"CLOSED\"}");
            var unbind = JsonConvert
                .DeserializeObject<BrandStoreUnbindRecipientResultJson>(
                    "{\"failed_reason\":\"管理员拒绝解绑\"}");

            Assert.AreEqual(5000000000L, list.total_count);
            Assert.AreEqual("REJECTED", list.data[0].audit_state);
            Assert.AreEqual("CONFIRMING",
                list.data[0].store_recipient[0].recipient_state);
            Assert.AreEqual("CLOSED", state.store_state);
            Assert.AreEqual("管理员拒绝解绑", unbind.failed_reason);
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(BrandStoreResultJson)));
        }

        [TestMethod]
        public async Task BrandAuthenticationUsesDedicatedSchemeAndHeaders()
        {
            using var rsa = RSA.Create(2048);
            var credentials = new TenPayBrandApiCredentials(
                "BRAND123456", "BRAND_SERIAL_001",
                rsa.ExportPkcs8PrivateKeyPem(), "PUB_KEY_ID_001",
                rsa.ExportSubjectPublicKeyInfoPem());
            var firstRequest = TenPayApiRequest.CreateForBrand(credentials);
            var secondRequest = TenPayApiRequest.CreateForBrand(credentials);
            var clientField = typeof(TenPayApiRequest).GetField("_client",
                BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.AreEqual("WECHATPAY-BRAND-SHA256-RSA2048",
                TenPayBrandApiCredentials.AuthorizationType);
            Assert.IsNotNull(clientField);
            Assert.AreSame(clientField.GetValue(firstRequest),
                clientField.GetValue(secondRequest));
            var lazyClient = (Lazy<HttpClient>)clientField.GetValue(firstRequest);
            Assert.AreEqual("PUB_KEY_ID_001",
                lazyClient.Value.DefaultRequestHeaders
                    .GetValues("Wechatpay-Serial").Single());

            var constructor = typeof(TenPayHttpHandler).GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic, null,
                new[] { typeof(TenPayBrandApiCredentials) }, null);
            var buildAuth = typeof(TenPayHttpHandler).GetMethod(
                "BuildAuthAsync",
                BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.IsNotNull(constructor);
            Assert.IsNotNull(buildAuth);
            using var handler = (TenPayHttpHandler)constructor.Invoke(
                new object[] { credentials });
            using var message = new HttpRequestMessage(HttpMethod.Post,
                "https://api.mch.weixin.qq.com/brand/store/brandstores")
            {
                Content = new StringContent("{\"store_address\":{}}",
                    Encoding.UTF8, "application/json")
            };
            var auth = await (Task<string>)buildAuth.Invoke(handler,
                new object[] { message });

            StringAssert.Contains(auth, "brand_id=\"BRAND123456\"");
            StringAssert.Contains(auth, "serial_no=\"BRAND_SERIAL_001\"");
            Assert.IsFalse(auth.Contains("mchid="));
        }

        private static IEnumerable<string> GetStringLiterals(MethodInfo method)
        {
            var bytes = method.GetMethodBody()?.GetILAsByteArray();
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
                    value = method.Module.ResolveString(
                        BitConverter.ToInt32(bytes, index + 1));
                }
                catch (ArgumentException)
                {
                    continue;
                }

                yield return value;
            }
        }

        private static IEnumerable<string> GetCalledMethodNames(
            MethodInfo method)
        {
            var bytes = method.GetMethodBody()?.GetILAsByteArray();
            if (bytes == null)
            {
                yield break;
            }

            for (var index = 0; index <= bytes.Length - 5; index++)
            {
                if (bytes[index] != 0x28 && bytes[index] != 0x6F)
                {
                    continue;
                }

                MethodBase calledMethod;
                try
                {
                    calledMethod = method.Module.ResolveMethod(
                        BitConverter.ToInt32(bytes, index + 1),
                        method.DeclaringType?.GetGenericArguments(),
                        method.GetGenericArguments());
                }
                catch (Exception exception) when (
                    exception is ArgumentException ||
                    exception is BadImageFormatException)
                {
                    continue;
                }

                yield return calledMethod.Name;
            }
        }
    }
}
