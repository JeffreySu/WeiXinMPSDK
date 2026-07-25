using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.Weixin.TenPayV3.Apis.Apply4Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Senparc.Weixin.TenPayV3.Test.Apis.Apply4Subject
{
    [TestClass]
    public class Apply4SubjectCurrentContractTests
    {
        [TestMethod]
        public void CurrentApiSurfaceContainsFiveOfficialEntriesAndPreservesLegacyMethods()
        {
            var methodNames = typeof(Apply4SubjectApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(method => method.Name)
                .Distinct()
                .ToArray();

            foreach (var currentName in new[]
                     {
                         nameof(Apply4SubjectApis.SubmitApplymentAsync),
                         nameof(Apply4SubjectApis.CancelApplymentAsync),
                         nameof(Apply4SubjectApis.QueryApplymentAuditResultAsync),
                         nameof(Apply4SubjectApis.QueryMerchantAuthorizationStateAsync),
                         nameof(Apply4SubjectApis.UploadImageAsync)
                     })
            {
                CollectionAssert.Contains(methodNames, currentName);
            }

            foreach (var legacyName in new[]
                     {
                         nameof(Apply4SubjectApis.Apply4SubjectApplymentAsync),
                         nameof(Apply4SubjectApis.CancelApply4SubjectApplymentAsync),
                         nameof(Apply4SubjectApis.QueryApply4SubjectApplymentByIdAsync),
                         nameof(Apply4SubjectApis.QueryApply4SubjectApplymentByOutRequestNoAsync)
                     })
            {
                CollectionAssert.Contains(methodNames, legacyName);
            }
        }

        [TestMethod]
        public void CurrentMethodsContainOfficialEndpointFragments()
        {
            AssertMethodContainsLiteral(nameof(Apply4SubjectApis.SubmitApplymentAsync),
                "v3/apply4subject/applyment/");
            AssertMethodContainsLiteral(nameof(Apply4SubjectApis.CancelApplymentAsync), "/cancel");
            AssertMethodContainsLiteral(nameof(Apply4SubjectApis.QueryApplymentAuditResultAsync),
                "v3/apply4subject/applyment");
            AssertMethodContainsLiteral(
                nameof(Apply4SubjectApis.QueryMerchantAuthorizationStateAsync),
                "v3/apply4subject/applyment/merchants/");
            AssertMethodContainsLiteral(nameof(Apply4SubjectApis.UploadImageAsync),
                "v3/merchant/media/upload");
        }

        [TestMethod]
        public void QueryBuilderEncodesValuesAndOmitsMissingParameters()
        {
            var buildQuery = typeof(Apply4SubjectApis).GetMethod("BuildQuery",
                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(buildQuery);

            var query = (string)buildQuery.Invoke(null, new object[]
            {
                new Dictionary<string, object>
                {
                    ["applyment_id"] = 20000011111L,
                    ["business_code"] = "merchant + 1",
                    ["missing"] = null
                }
            });

            Assert.AreEqual("?applyment_id=20000011111&business_code=merchant%20%2B%201", query);
        }

        [TestMethod]
        public void SubmitRequestSerializesCurrentNestedMaterials()
        {
            var request = new Apply4SubjectApplicationRequestData
            {
                channel_id = "20001111",
                business_code = "1900013511_10000",
                contact_info = new Apply4SubjectApplicationContactInfo
                {
                    name = "encrypted-name",
                    mobile = "encrypted-mobile",
                    contact_type = "LEGAL"
                },
                subject_info = new Apply4SubjectApplicationSubjectInfo
                {
                    subject_type = "SUBJECT_TYPE_ENTERPRISE",
                    is_finance_institution = false,
                    business_licence_info = new Apply4SubjectBusinessLicenceInfo
                    {
                        licence_number = "914201123033363296",
                        licence_copy = "licence-media-id",
                        merchant_name = "测试企业",
                        legal_person = "测试法人",
                        company_address = "测试注册地址",
                        licence_valid_date = "[\"2017-10-28\",\"长期\"]"
                    },
                    special_operation_list = new List<Apply4SubjectSpecialOperationInfo>
                    {
                        new Apply4SubjectSpecialOperationInfo
                        {
                            category_id = 100,
                            operation_copy_list = new List<string> { "license-1" }
                        }
                    }
                },
                identification_info = new Apply4SubjectApplicationIdentificationInfo
                {
                    id_holder_type = "LEGAL",
                    identification_type = "IDENTIFICATION_TYPE_IDCARD",
                    identification_name = "encrypted-id-name",
                    identification_number = "encrypted-id-number"
                },
                addition_info = new Apply4SubjectApplicationAdditionInfo
                {
                    confirm_mchid_list = new List<string> { "1900000109" }
                }
            };

            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            StringAssert.Contains(json, "\"business_code\":\"1900013511_10000\"");
            StringAssert.Contains(json, "\"business_licence_info\"");
            StringAssert.Contains(json, "\"special_operation_list\"");
            StringAssert.Contains(json, "\"confirm_mchid_list\":[\"1900000109\"]");
            Assert.IsFalse(json.Contains("\"certificate_info\":"));
            Assert.IsFalse(json.Contains("\"ubo_info_list\":"));
        }

        [TestMethod]
        public void ResponseModelsPreserveLongIdAndOfficialStateNames()
        {
            var application = JsonConvert.DeserializeObject<Apply4SubjectApplicationResultJson>(
                "{\"applyment_id\":20000000011111}");
            var audit = JsonConvert.DeserializeObject<Apply4SubjectAuditResultJson>(
                "{\"applyment_state\":\"APPLYMENT_STATE_PASSED\"," +
                "\"qrcode_data\":\"base64-image\",\"reject_param\":\"merchant_name\"}");
            var authorization =
                JsonConvert.DeserializeObject<Apply4SubjectAuthorizationStateResultJson>(
                    "{\"authorize_state\":\"AUTHORIZE_STATE_AUTHORIZED\"}");

            Assert.AreEqual(20000000011111L, application.applyment_id);
            Assert.AreEqual("APPLYMENT_STATE_PASSED", audit.applyment_state);
            Assert.AreEqual("base64-image", audit.qrcode_data);
            Assert.AreEqual("AUTHORIZE_STATE_AUTHORIZED", authorization.authorize_state);
        }

        [TestMethod]
        public void EmptyBodyRequestCapabilityIsAvailableWithoutChangingExistingOverloads()
        {
            var methods = typeof(TenPayApiRequest).GetMethods(BindingFlags.Public | BindingFlags.Instance);

            Assert.AreEqual(2, methods.Count(method =>
                method.Name == nameof(TenPayApiRequest.RequestWithoutBodyAsync)));
            Assert.IsTrue(methods.Any(method => method.Name == nameof(TenPayApiRequest.RequestAsync) &&
                                                method.GetParameters().Length == 7));
        }

        private static void AssertMethodContainsLiteral(string methodName, string expected)
        {
            var methods = typeof(Apply4SubjectApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.Name == methodName)
                .ToArray();
            Assert.IsTrue(methods.Any(method =>
                    GetStringLiterals(method).Any(value => value.Contains(expected))),
                $"{methodName}: {expected}");
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
