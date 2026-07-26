using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BrandApplyment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Senparc.Weixin.TenPayV3.Test.Apis.BrandApplyment
{
    [TestClass]
    public class BrandApplymentContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(BrandApplymentApis.SubmitApplymentAsync)] = "v3/brand/applyments",
                [nameof(BrandApplymentApis.QueryByBusinessCodeAsync)] =
                    "v3/brand/applyments/business-code/",
                [nameof(BrandApplymentApis.QueryByApplymentIdAsync)] =
                    "v3/brand/applyments/applyment-id/",
                [nameof(BrandApplymentApis.CancelApplymentAsync)] =
                    "v3/brand/applyments/cancel-applyment",
                [nameof(BrandApplymentApis.UploadImageAsync)] = "v3/merchant/media/upload"
            };

        [TestMethod]
        public void ApiSurfaceContainsFiveOfficialEntries()
        {
            var methodNames = typeof(BrandApplymentApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.DeclaringType == typeof(BrandApplymentApis))
                .Select(method => method.Name)
                .Distinct()
                .ToArray();

            Assert.AreEqual(5, OfficialEndpoints.Count);
            CollectionAssert.AreEquivalent(OfficialEndpoints.Keys.ToArray(), methodNames);
        }

        [TestMethod]
        public void EveryEntryContainsOfficialEndpoint()
        {
            foreach (var endpoint in OfficialEndpoints)
            {
                var methods = typeof(BrandApplymentApis)
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(method => method.Name == endpoint.Key)
                    .ToArray();

                Assert.IsTrue(methods.Any(method =>
                        GetStringLiterals(method).Any(value => value.Contains(endpoint.Value))),
                    $"{endpoint.Key}: {endpoint.Value}");
            }
        }

        [TestMethod]
        public void PathIdentifiersAreEncoded()
        {
            var escape = typeof(BrandApplymentApis).GetMethod("Escape",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(escape);
            Assert.AreEqual("brand%20%2B%20applyment",
                escape.Invoke(null, new object[] { "brand + applyment" }));
        }

        [TestMethod]
        public void SubmitRequestSerializesLatestTrademarkLists()
        {
            var request = new BrandApplymentRequestData
            {
                business_code = "brand_10001",
                admin_info = new BrandApplymentAdminInfo
                {
                    admin_name = "encrypted-name",
                    id_doc_type = "IDENTIFICATION_TYPE_MAINLAND_ID_CARD",
                    id_card_number = "encrypted-id"
                },
                subject_info = new BrandApplymentSubjectInfo
                {
                    subject_type = "SUBJECT_TYPE_ENTERPRISE",
                    subject_name = "测试企业",
                    unified_social_credit_code = "91310101MA1FPX1234"
                },
                brand_basic_info = new BrandApplymentBasicInfo
                {
                    brand_name = "测试品牌",
                    brand_logo = "logo-media-id"
                },
                trademark = new BrandApplymentTrademarkInfo
                {
                    trademark_exists = "TRADEMARK_EXISTS",
                    trademark_registration_certificate =
                        new BrandApplymentTrademarkCertificate
                        {
                            name = "测试品牌",
                            number = "TM10001",
                            valid_end_time = "2035-09-08",
                            international_class = "25",
                            holder = "测试企业",
                            certificate_list = new List<string> { "certificate-1", "certificate-2" },
                            license_list = new List<string> { "license-1" }
                        }
                }
            };

            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            StringAssert.Contains(json, "\"business_code\":\"brand_10001\"");
            StringAssert.Contains(json, "\"certificate_list\":[\"certificate-1\",\"certificate-2\"]");
            StringAssert.Contains(json, "\"license_list\":[\"license-1\"]");
            Assert.IsFalse(json.Contains("\"certificate\":"));
            Assert.IsFalse(json.Contains("\"no_trademark_addition_prove\":"));
        }

        [TestMethod]
        public void CancelRequestSupportsEitherOfficialIdentifier()
        {
            var byBusinessCode = JsonConvert.SerializeObject(new BrandApplymentCancelRequestData
            {
                business_code = "brand_10001"
            }, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var byApplymentId = JsonConvert.SerializeObject(new BrandApplymentCancelRequestData
            {
                applyment_id = "1111111111"
            }, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            Assert.AreEqual("{\"business_code\":\"brand_10001\"}", byBusinessCode);
            Assert.AreEqual("{\"applyment_id\":\"1111111111\"}", byApplymentId);
        }

        [TestMethod]
        public void QueryResponsePreservesOfficialStateFields()
        {
            var result = JsonConvert.DeserializeObject<BrandApplymentQueryResultJson>(
                "{\"applyment_id\":\"1111111111\",\"business_code\":\"brand_10001\"," +
                "\"applyment_state\":\"APPLYMENT_STATE_FINISH\"," +
                "\"applyment_state_desc\":\"资料审核通过\",\"brand_id\":\"12345678\"}");

            Assert.AreEqual("APPLYMENT_STATE_FINISH", result.applyment_state);
            Assert.AreEqual("资料审核通过", result.applyment_state_desc);
            Assert.AreEqual("12345678", result.brand_id);
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
                    value = method.Module.ResolveString(BitConverter.ToInt32(bytes, index + 1));
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
