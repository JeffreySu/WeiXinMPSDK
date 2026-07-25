using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BrandCard;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Senparc.Weixin.TenPayV3.Test.Apis.BrandCard
{
    [TestClass]
    public class BrandCardContractTests
    {
        private static readonly IReadOnlyDictionary<string, string>
            OfficialEndpoints = new Dictionary<string, string>
            {
                [nameof(BrandCardApis.SubmitCardConfigAsync)] =
                    "v3/brand/card/card-configs",
                [nameof(BrandCardApis.PublishCardConfigAsync)] =
                    "v3/brand/card/card-configs/publish",
                [nameof(BrandCardApis.CancelCardConfigApplymentAsync)] =
                    "v3/brand/card/card-configs/cancel-applyment",
                [nameof(BrandCardApis.QueryCardConfigApplymentAsync)] =
                    "v3/brand/card/card-configs",
                [nameof(BrandCardApis.GetCardPreviewUrlAsync)] =
                    "v3/brand/card/card-configs/preview-url",
                [nameof(BrandCardApis.AddCardLinkAsync)] =
                    "v3/brand/card/card-links",
                [nameof(BrandCardApis.UnbindCardLinkAsync)] =
                    "v3/brand/card/card-links/unbind-card-link",
                [nameof(BrandCardApis.CancelCardLinkApplymentAsync)] =
                    "v3/brand/card/card-links/cancel-applyment",
                [nameof(BrandCardApis.QueryActiveCardLinksAsync)] =
                    "v3/brand/card/card-links",
                [nameof(BrandCardApis.QueryCardLinkApplymentByBusinessCodeAsync)] =
                    "v3/brand/card/card-links/business-code/"
            };

        [TestMethod]
        public void ApiSurfaceContainsAllTenOfficialEntries()
        {
            var methods = typeof(BrandCardApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.DeclaringType == typeof(BrandCardApis))
                .Select(method => method.Name)
                .Distinct()
                .ToArray();

            Assert.AreEqual(10, OfficialEndpoints.Count);
            CollectionAssert.AreEquivalent(OfficialEndpoints.Keys.ToArray(),
                methods);
        }

        [TestMethod]
        public void EveryMethodContainsCurrentOfficialEndpoint()
        {
            foreach (var endpoint in OfficialEndpoints)
            {
                var method = typeof(BrandCardApis).GetMethod(endpoint.Key,
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
            var buildQuery = typeof(BrandCardApis).GetMethod(
                "BuildBrandCardQuery",
                BindingFlags.NonPublic | BindingFlags.Static);
            var escape = typeof(BrandCardApis).GetMethod(
                "EscapeBrandCardValue",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(buildQuery);
            Assert.IsNotNull(escape);
            var path = (string)buildQuery.Invoke(null, new object[]
            {
                "v3/brand/card/card-links",
                new[]
                {
                    "brand_id", "123 456+7",
                    "payment_scene", "MINI_PROGRAM",
                    "optional", null,
                    "page_index", "1"
                }
            });

            Assert.AreEqual(
                "v3/brand/card/card-links?brand_id=123%20456%2B7&payment_scene=MINI_PROGRAM&page_index=1",
                path);
            Assert.AreEqual("business%20%2B%20code",
                escape.Invoke(null, new object[] { "business + code" }));
        }

        [TestMethod]
        public void CardConfigRequestPreservesNestedOfficialFields()
        {
            var json = JObject.Parse(JsonConvert.SerializeObject(
                new BrandCardConfigRequestData
                {
                    business_code = "brand_card_10001",
                    brand_id = "1234567",
                    brand_mini_program_info = new BrandCardMiniProgramInfo
                    {
                        appid = "wx1234567890abcdef",
                        default_jump_path = "pages/shop/index",
                        button_text = "前往小程序"
                    },
                    brand_customer_service = new BrandCardCustomerServiceInfo
                    {
                        customer_service_type = "CUSTOMIZE_MP",
                        customer_service_path = "pages/service/index",
                        appid = "wx1234567890abcdef"
                    },
                    service_list = new[]
                    {
                        new BrandCardServiceInfo
                        {
                            service_classify_name = "会员服务",
                            service_name = "会员中心",
                            service_jump_type = "JUMP_MINI_PROGRAM",
                            service_jump_path = "pages/member/index",
                            appid = "wx1234567890abcdef"
                        }
                    }
                }, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));

            Assert.AreEqual("pages/shop/index",
                json["brand_mini_program_info"]?["default_jump_path"]
                    ?.Value<string>());
            Assert.AreEqual("CUSTOMIZE_MP",
                json["brand_customer_service"]?["customer_service_type"]
                    ?.Value<string>());
            Assert.AreEqual("JUMP_MINI_PROGRAM",
                json["service_list"]?[0]?["service_jump_type"]
                    ?.Value<string>());
            Assert.IsNull(json["brand_customer_service"]?
                ["customer_service_phone"]);
        }

        [TestMethod]
        public void LinkRequestsPreserveSceneSpecificIdentifiersAndPagination()
        {
            var link = JObject.FromObject(new BrandCardLinkRequestData
            {
                business_code = "link_10001",
                brand_id = "123456",
                payment_scene = "PAYMENT_SCORE",
                service_id = "00005000000000548218251086296300"
            });
            var query = JObject.FromObject(
                new BrandCardActiveLinksQueryRequestData
                {
                    brand_id = "123456",
                    payment_scene = "MINI_PROGRAM",
                    page_index = 1,
                    page_size = 50
                });

            Assert.AreEqual("PAYMENT_SCORE",
                link["payment_scene"]?.Value<string>());
            Assert.AreEqual("00005000000000548218251086296300",
                link["service_id"]?.Value<string>());
            Assert.AreEqual(1, query["page_index"]?.Value<int>());
            Assert.AreEqual(50, query["page_size"]?.Value<int>());
        }

        [TestMethod]
        public void ResultModelsPreserveOfficialStatesAndActiveLinkList()
        {
            var config = JsonConvert
                .DeserializeObject<BrandCardConfigApplymentResultJson>(
                    "{\"business_code\":\"card_1\"," +
                    "\"applyment_state\":\"AUDIT_REJECTED\"," +
                    "\"reject_reason\":\"资料不完整\"," +
                    "\"actual_publish_time\":\"2026-07-25T12:00:00+08:00\"}");
            var link = JsonConvert
                .DeserializeObject<BrandCardLinkApplymentResultJson>(
                    "{\"business_code\":\"link_1\"," +
                    "\"payment_scene\":\"PAYMENT_CODE\"," +
                    "\"configuration_state\":\"WAITING_CONFIRMATION\"," +
                    "\"card_link_mchid\":\"1900000109\"}");
            var active = JsonConvert
                .DeserializeObject<BrandCardActiveLinksResultJson>(
                    "{\"brand_id\":\"123456\",\"total_num\":1," +
                    "\"active_link_list\":[{\"payment_scene\":\"MINI_PROGRAM\"," +
                    "\"appid_list\":[\"wx123\",\"wx456\"]}]," +
                    "\"page_index\":1,\"page_size\":10}");

            Assert.AreEqual("AUDIT_REJECTED", config.applyment_state);
            Assert.AreEqual("WAITING_CONFIRMATION", link.configuration_state);
            Assert.AreEqual("1900000109", link.card_link_mchid);
            Assert.AreEqual(1, active.total_num);
            CollectionAssert.AreEqual(new[] { "wx123", "wx456" },
                active.active_link_list[0].appid_list);
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(BrandCardLinkApplymentResultJson)));
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
    }
}
