using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.TenPayV3.Apis.Apply4Sub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Senparc.Weixin.TenPayV3.Test.Apis.Apply4Sub
{
    [TestClass]
    public class Apply4SubCurrentContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(Apply4SubApis.SubmitApplymentAsync)] =
                    "v3/applyment4sub/applyment/",
                [nameof(Apply4SubApis.QueryApplymentByIdAsync)] =
                    "v3/applyment4sub/applyment/applyment_id/",
                [nameof(Apply4SubApis.QueryApplymentByBusinessCodeAsync)] =
                    "v3/applyment4sub/applyment/business_code/",
                [nameof(Apply4SubApis.ModifySettlementAsync)] =
                    "v3/apply4sub/sub_merchants/",
                [nameof(Apply4SubApis.QuerySettlementAsync)] =
                    "/settlement",
                [nameof(Apply4SubApis.QuerySettlementModificationAsync)] =
                    "/application/",
                [nameof(Apply4SubApis.UploadFileAsync)] =
                    "v3/merchant/media/upload",
                [nameof(Apply4SubApis.UploadVideoAsync)] =
                    "v3/merchant/media/video_upload"
            };

        [TestMethod]
        public void CurrentApiSurfaceContainsAllEightOfficialEntries()
        {
            var methods = typeof(Apply4SubApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => OfficialEndpoints.ContainsKey(method.Name))
                .GroupBy(method => method.Name)
                .ToDictionary(group => group.Key, group => group.Count());

            CollectionAssert.AreEquivalent(OfficialEndpoints.Keys.ToArray(),
                methods.Keys.ToArray());
            Assert.AreEqual(2, methods[nameof(Apply4SubApis.UploadFileAsync)]);
            Assert.AreEqual(2, methods[nameof(Apply4SubApis.UploadVideoAsync)]);
            Assert.IsTrue(OfficialEndpoints.Keys
                .Where(name => name != nameof(Apply4SubApis.UploadFileAsync) &&
                               name != nameof(Apply4SubApis.UploadVideoAsync))
                .All(name => methods[name] == 1));
        }

        [TestMethod]
        public void EveryCurrentEntryContainsOfficialEndpoint()
        {
            foreach (var endpoint in OfficialEndpoints)
            {
                var methods = typeof(Apply4SubApis)
                    .GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                BindingFlags.Instance)
                    .Where(method => method.Name == endpoint.Key)
                    .ToArray();

                Assert.IsTrue(methods.Any(method =>
                        GetStringLiterals(method).Any(value => value.Contains(endpoint.Value))),
                    $"{endpoint.Key}: {endpoint.Value}");
            }
        }

        [TestMethod]
        public void CurrentPathIdentifiersAndAccountRuleAreEncoded()
        {
            var escape = typeof(Apply4SubApis).GetMethod("EscapeCurrent",
                BindingFlags.NonPublic | BindingFlags.Static);
            var buildQuery = typeof(Apply4SubApis).GetMethod("BuildAccountNumberRuleQuery",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(escape);
            Assert.IsNotNull(buildQuery);
            Assert.AreEqual("merchant%20%2B%20id",
                escape.Invoke(null, new object[] { "merchant + id" }));
            Assert.AreEqual(string.Empty,
                buildQuery.Invoke(null, new object[] { null }));
            Assert.AreEqual("?account_number_rule=MASK%20%2B%20V2",
                buildQuery.Invoke(null, new object[] { "MASK + V2" }));
        }

        [TestMethod]
        public void CurrentApplymentRequestUsesOfficialNestedFieldNames()
        {
            var data = new Apply4SubCurrentApplymentRequestData
            {
                business_code = "1900013511_10000",
                contact_info = new Apply4SubCurrentContactInfo
                {
                    contact_type = "LEGAL",
                    contact_name = "encrypted-name"
                },
                subject_info = new Apply4SubCurrentSubjectInfo
                {
                    subject_type = "SUBJECT_TYPE_ENTERPRISE",
                    finance_institution = false,
                    identity_info = new Apply4SubCurrentIdentityInfo
                    {
                        owner = true,
                        id_card_info = new Apply4SubCurrentIdCardInfo
                        {
                            id_card_copy = "media-id"
                        }
                    }
                },
                business_info = new Apply4SubCurrentBusinessInfo
                {
                    merchant_shortname = "张三餐饮店",
                    sales_info = new Apply4SubCurrentSalesInfo
                    {
                        sales_scenes_type = new[] { "SALES_SCENES_STORE" }
                    }
                },
                settlement_info = new Apply4SubCurrentSettlementInfo
                {
                    settlement_id = "719",
                    credit_activities_rate = "0.54"
                },
                bank_account_info = new Apply4SubCurrentBankAccountInfo
                {
                    bank_account_type = "BANK_ACCOUNT_TYPE_CORPORATE"
                },
                addition_info = new Apply4SubCurrentAdditionInfo
                {
                    legal_person_video = "video-media-id"
                }
            };

            var json = JObject.FromObject(data);

            Assert.AreEqual("1900013511_10000", json["business_code"]?.Value<string>());
            Assert.IsNull(json["out_request_no"]);
            Assert.AreEqual("LEGAL", json["contact_info"]?["contact_type"]?.Value<string>());
            Assert.AreEqual("media-id",
                json["subject_info"]?["identity_info"]?["id_card_info"]?["id_card_copy"]
                    ?.Value<string>());
            Assert.AreEqual("SALES_SCENES_STORE",
                json["business_info"]?["sales_info"]?["sales_scenes_type"]?[0]
                    ?.Value<string>());
            Assert.AreEqual("0.54",
                json["settlement_info"]?["credit_activities_rate"]?.Value<string>());
            Assert.AreEqual("video-media-id",
                json["addition_info"]?["legal_person_video"]?.Value<string>());
        }

        [TestMethod]
        public void ModifySettlementRequestUsesFlatOfficialBody()
        {
            var json = JObject.FromObject(new Apply4SubModifySettlementRequestData
            {
                account_type = "ACCOUNT_TYPE_BUSINESS",
                account_bank = "工商银行",
                bank_name = "中国工商银行北京市分行",
                bank_branch_id = "402713354941",
                account_number = "encrypted-number",
                account_name = "encrypted-name"
            });

            Assert.AreEqual("ACCOUNT_TYPE_BUSINESS", json["account_type"]?.Value<string>());
            Assert.AreEqual("encrypted-number", json["account_number"]?.Value<string>());
            Assert.IsNull(json["account_info"]);
            Assert.IsNull(json["sub_mchid"]);
        }

        [TestMethod]
        public void CurrentApplymentQueryPreservesOfficialStateAndAuditFields()
        {
            var result = JsonConvert.DeserializeObject<Apply4SubCurrentApplymentQueryResultJson>(
                "{\"business_code\":\"1900013511_10000\",\"applyment_id\":2000002124775691," +
                "\"sub_mchid\":\"1234567890\",\"sign_url\":\"https://pay.weixin.qq.com/sign\"," +
                "\"applyment_state\":\"APPLYMENT_STATE_REJECTED\"," +
                "\"applyment_state_msg\":\"已驳回\",\"audit_detail\":[{" +
                "\"field\":\"id_card_copy\",\"field_name\":\"身份证复印件\"," +
                "\"reject_reason\":\"图片不清晰\"}]}" );

            Assert.AreEqual(2000002124775691L, result.applyment_id);
            Assert.AreEqual("APPLYMENT_STATE_REJECTED", result.applyment_state);
            Assert.AreEqual("id_card_copy", result.audit_detail.Single().field);
            Assert.AreEqual("图片不清晰", result.audit_detail.Single().reject_reason);
        }

        [TestMethod]
        public void SettlementModelsPreserveVerificationAndAuditStates()
        {
            var settlement = JsonConvert.DeserializeObject<Apply4SubSettlementResultJson>(
                "{\"account_type\":\"ACCOUNT_TYPE_BUSINESS\"," +
                "\"account_number\":\"62*************78\"," +
                "\"verify_result\":\"VERIFY_SUCCESS\"}");
            var modification =
                JsonConvert.DeserializeObject<Apply4SubSettlementModificationResultJson>(
                    "{\"account_name\":\"张*\",\"account_type\":\"ACCOUNT_TYPE_BUSINESS\"," +
                    "\"account_number\":\"62*************78\"," +
                    "\"verify_result\":\"AUDIT_FAIL\",\"verify_fail_reason\":\"卡号有误\"," +
                    "\"verify_finish_time\":\"2015-05-20T13:29:35+08:00\"}");

            Assert.AreEqual("VERIFY_SUCCESS", settlement.verify_result);
            Assert.AreEqual("AUDIT_FAIL", modification.verify_result);
            Assert.AreEqual("卡号有误", modification.verify_fail_reason);
            Assert.AreEqual("2015-05-20T13:29:35+08:00",
                modification.verify_finish_time);
        }

        [TestMethod]
        public void MultipartMediaTypeMatchesCurrentFileAndVideoFormats()
        {
            var getMediaType = typeof(TenPayApiRequest).GetMethod(
                "GetMultipartMediaType", BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(getMediaType);
            Assert.AreEqual("image/jpeg",
                getMediaType.Invoke(null, new object[] { "license.JPG" }));
            Assert.AreEqual("application/pdf",
                getMediaType.Invoke(null, new object[] { "material.pdf" }));
            Assert.AreEqual("video/mp4",
                getMediaType.Invoke(null, new object[] { "commitment.mp4" }));
            Assert.AreEqual("video/x-matroska",
                getMediaType.Invoke(null, new object[] { "commitment.mkv" }));
            Assert.AreEqual("application/octet-stream",
                getMediaType.Invoke(null, new object[] { "unknown.bin" }));
        }

        [TestMethod]
        public void LegacyApply4SubMethodsRemainAvailable()
        {
            var legacyNames = new[]
            {
                nameof(Apply4SubApis.Apply4SubApplymentAsync),
                nameof(Apply4SubApis.QueryApply4SubApplymentByIdAsync),
                nameof(Apply4SubApis.QueryApply4SubApplymentByOutRequestNoAsync),
                nameof(Apply4SubApis.ModifyApply4SubSettlementAsync),
                nameof(Apply4SubApis.QueryApply4SubSettlementAsync)
            };
            var publicNames = typeof(Apply4SubApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(method => method.Name)
                .ToArray();

            Assert.IsTrue(legacyNames.All(publicNames.Contains));
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
