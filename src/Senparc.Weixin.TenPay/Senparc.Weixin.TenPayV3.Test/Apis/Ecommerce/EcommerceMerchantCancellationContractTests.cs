using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.TenPayV3.Apis.Ecommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Senparc.Weixin.TenPayV3.Test.Apis.Ecommerce
{
    [TestClass]
    public class EcommerceMerchantCancellationContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(EcommerceApis.ValidateMerchantCancellationAsync)] =
                    "v3/ecommerce/account/apply-cancel-withdraw/validate-cancel/",
                [nameof(EcommerceApis.ApplyCancelWithdrawAsync)] =
                    "v3/ecommerce/account/apply-cancel-withdraw",
                [nameof(EcommerceApis.QueryCancelWithdrawByOutRequestNoAsync)] =
                    "v3/ecommerce/account/apply-cancel-withdraw/out-request-no/",
                [nameof(EcommerceApis.QueryCancelWithdrawByApplymentIdAsync)] =
                    "v3/ecommerce/account/apply-cancel-withdraw/applyment-id/",
                [nameof(EcommerceApis.SubmitLegacyCancelApplicationAsync)] =
                    "v3/ecommerce/account/cancel-applications",
                [nameof(EcommerceApis.QueryLegacyCancelApplicationAsync)] =
                    "v3/ecommerce/account/cancel-applications/out-apply-no/",
                [nameof(EcommerceApis.UploadCancelApplicationImageAsync)] =
                    "v3/ecommerce/account/cancel-applications/media",
                [nameof(EcommerceApis.SubmitLegacyCancelWithdrawAsync)] =
                    "v3/mch_operate/risk/withdrawl-apply",
                [nameof(EcommerceApis.QueryLegacyCancelWithdrawByOutRequestNoAsync)] =
                    "v3/mch_operate/risk/withdrawl-apply/out-request-no/",
                [nameof(EcommerceApis.QueryLegacyCancelWithdrawByApplymentIdAsync)] =
                    "v3/mch_operate/risk/withdrawl-apply/applyment-id/"
            };

        [TestMethod]
        public void ApiSurfaceContainsAllTenOfficialEntries()
        {
            var methods = typeof(EcommerceApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => OfficialEndpoints.ContainsKey(method.Name))
                .GroupBy(method => method.Name)
                .ToDictionary(group => group.Key, group => group.Count());

            CollectionAssert.AreEquivalent(OfficialEndpoints.Keys.ToArray(),
                methods.Keys.ToArray());
            Assert.AreEqual(2,
                methods[nameof(EcommerceApis.UploadCancelApplicationImageAsync)]);
            Assert.IsTrue(OfficialEndpoints.Keys
                .Where(name => name !=
                               nameof(EcommerceApis.UploadCancelApplicationImageAsync))
                .All(name => methods[name] == 1));
        }

        [TestMethod]
        public void EveryEntryContainsOfficialEndpoint()
        {
            foreach (var endpoint in OfficialEndpoints)
            {
                var methods = typeof(EcommerceApis)
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(method => method.Name == endpoint.Key)
                    .ToArray();

                Assert.IsTrue(methods.Any(method =>
                        GetStringLiterals(method).Any(value => value.Contains(endpoint.Value))),
                    $"{endpoint.Key}: {endpoint.Value}");
            }
        }

        [TestMethod]
        public void PathIdentifiersAreUriEncoded()
        {
            var escape = typeof(EcommerceApis).GetMethod(
                "EscapeMerchantCancellationPath",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(escape);
            Assert.AreEqual("merchant%20%2B%20id",
                escape.Invoke(null, new object[] { "merchant + id" }));
        }

        [TestMethod]
        public void NewFlowRequestUsesOfficialNestedFields()
        {
            var data = new EcommerceApplyCancelWithdrawRequestData
            {
                sub_mchid = "1900000109",
                out_request_no = "P202410241010125346",
                withdraw = "APPLY_WITHDRAW",
                payee_info = new EcommerceCancelWithdrawPayeeInfo
                {
                    account_type = "ACCOUNT_TYPE_PERSONAL",
                    bank_account_info = new EcommerceCancelWithdrawBankAccountInfo
                    {
                        account_name = "encrypted-name",
                        account_bank = "工商银行",
                        bank_branch_name = "中国工商银行北京支行",
                        account_number = "encrypted-number"
                    },
                    identity_info = new EcommerceCancelWithdrawIdentityInfo
                    {
                        id_doc_type = "IDENTIFICATION_TYPE_ID_CARD",
                        identification_name = "encrypted-id-name",
                        identification_no = "encrypted-id-number"
                    }
                },
                proof_medias = new[]
                {
                    new EcommerceCancelWithdrawProofMedia
                    {
                        proof_media_type = "WITHDRAWAL_APPLICATION",
                        proof_media = "media-id"
                    }
                },
                additional_materials = new[] { "additional-media-id" }
            };

            var json = JObject.FromObject(data);

            Assert.AreEqual("P202410241010125346",
                json["out_request_no"]?.Value<string>());
            Assert.AreEqual("中国工商银行北京支行",
                json["payee_info"]?["bank_account_info"]?["bank_branch_name"]
                    ?.Value<string>());
            Assert.AreEqual("IDENTIFICATION_TYPE_ID_CARD",
                json["payee_info"]?["identity_info"]?["id_doc_type"]
                    ?.Value<string>());
            Assert.AreEqual("WITHDRAWAL_APPLICATION",
                json["proof_medias"]?[0]?["proof_media_type"]?.Value<string>());
            Assert.AreEqual("additional-media-id",
                json["additional_materials"]?[0]?.Value<string>());
        }

        [TestMethod]
        public void LegacyCancelApplicationUsesOfficialMaterialFields()
        {
            var json = JObject.FromObject(
                new EcommerceLegacyCancelApplicationRequestData
                {
                    sub_mchid = "1900000109",
                    out_apply_no = "2019061122222222122",
                    application_info = new[]
                    {
                        new EcommerceLegacyCancelApplicationMaterial
                        {
                            application_type = "SP_CANCEL_ACCOUNT_APPLICATION",
                            application_media_id = "media-id"
                        }
                    }
                });

            Assert.AreEqual("2019061122222222122",
                json["out_apply_no"]?.Value<string>());
            Assert.AreEqual("SP_CANCEL_ACCOUNT_APPLICATION",
                json["application_info"]?[0]?["application_type"]?.Value<string>());
            Assert.AreEqual("media-id",
                json["application_info"]?[0]?["application_media_id"]
                    ?.Value<string>());
        }

        [TestMethod]
        public void LegacyWithdrawRequestPreservesOldFlowFieldNames()
        {
            var json = JObject.FromObject(
                new EcommerceLegacyCancelWithdrawRequestData
                {
                    sub_mchid = "1900000109",
                    out_account_type = "BASIC_ACCOUNT",
                    amount = 101,
                    out_request_no = "2019061122222222122",
                    payee_type = "CONTRIBUTION_MERCHANT",
                    payee_mchid = "1900000109",
                    payee_info = new EcommerceLegacyCancelWithdrawPayeeInfo
                    {
                        account_type = "ACCOUNT_TYPE_PERSONAL",
                        bank_account_info =
                            new EcommerceLegacyCancelWithdrawBankAccountInfo
                            {
                                account_name = "encrypted-name",
                                account_bank = "工商银行",
                                bank_name = "中国工商银行北京支行",
                                account_number = "encrypted-number"
                            },
                        identity_info = new EcommerceLegacyCancelWithdrawIdentityInfo
                        {
                            id_doc_type = "IDENTIFICATION_TYPE_IDCARD",
                            identification_name = "encrypted-id-name",
                            identification_no = "encrypted-id-number"
                        }
                    },
                    proof_media_list = new EcommerceLegacyCancelWithdrawProofMediaList
                    {
                        proof_payee_media = new[]
                        {
                            new EcommerceLegacyCancelWithdrawProofMedia
                            {
                                proof_media_type = "LEGAL_ID_CARD",
                                proof_media = "proof-media-id"
                            }
                        }
                    },
                    additional_materials =
                        new EcommerceLegacyCancelWithdrawAdditionalMaterials
                        {
                            additional_media = new[] { "additional-media-id" }
                        }
                });

            Assert.AreEqual("中国工商银行北京支行",
                json["payee_info"]?["bank_account_info"]?["bank_name"]
                    ?.Value<string>());
            Assert.IsNull(
                json["payee_info"]?["bank_account_info"]?["bank_branch_name"]);
            Assert.AreEqual("IDENTIFICATION_TYPE_IDCARD",
                json["payee_info"]?["identity_info"]?["id_doc_type"]
                    ?.Value<string>());
            Assert.AreEqual("LEGAL_ID_CARD",
                json["proof_media_list"]?["proof_payee_media"]?[0]?
                    ["proof_media_type"]?.Value<string>());
            Assert.AreEqual("additional-media-id",
                json["additional_materials"]?["additional_media"]?[0]
                    ?.Value<string>());
        }

        [TestMethod]
        public void ValidationResultPreservesAccountsAndBlockReasons()
        {
            var result =
                JsonConvert.DeserializeObject<EcommerceCancellationValidationResultJson>(
                    "{\"sub_mchid\":\"1900000109\",\"merchant_state\":\"NORMAL\"," +
                    "\"validate_result\":\"NOT_ALLOW_CANCEL_WITHDRAW\"," +
                    "\"account_info\":[{\"out_account_type\":\"BASIC_ACCOUNT\",\"amount\":101}]," +
                    "\"block_reasons\":[{\"type\":\"FUNDS_PENDING_PROCESSING\"," +
                    "\"description\":\"商户资金待处理\"}]}");

            Assert.AreEqual("NOT_ALLOW_CANCEL_WITHDRAW", result.validate_result);
            Assert.AreEqual(101, result.account_info.Single().amount);
            Assert.AreEqual("FUNDS_PENDING_PROCESSING",
                result.block_reasons.Single().type);
        }

        [TestMethod]
        public void NewFlowQueryPreservesWithdrawAndConfirmationStates()
        {
            var result =
                JsonConvert.DeserializeObject<EcommerceCancelWithdrawQueryResultJson>(
                    "{\"applyment_id\":\"X202410241010125346\"," +
                    "\"out_request_no\":\"P202410241010125346\"," +
                    "\"cancel_state\":\"FUND_PROCESSING\"," +
                    "\"withdraw\":\"APPLY_WITHDRAW\"," +
                    "\"withdraw_state\":\"WITHDRAW_PROCESSING\"," +
                    "\"account_withdraw_result\":[{\"out_account_type\":\"BASIC_ACCOUNT\"," +
                    "\"pay_state\":\"PAY_SUCCEED\",\"state_description\":\"付款成功\"}]," +
                    "\"confirm_cancel\":{\"confirm_cancel_url\":\"https://pay.weixin.qq.com/confirm\"}}");

            Assert.AreEqual("FUND_PROCESSING", result.cancel_state);
            Assert.AreEqual("WITHDRAW_PROCESSING", result.withdraw_state);
            Assert.AreEqual("PAY_SUCCEED",
                result.account_withdraw_result.Single().pay_state);
            Assert.AreEqual("https://pay.weixin.qq.com/confirm",
                result.confirm_cancel.confirm_cancel_url);
        }

        [TestMethod]
        public void LegacyResponsesPreserveOfficialStateAndWithdrawlSpelling()
        {
            var cancel =
                JsonConvert.DeserializeObject<EcommerceLegacyCancelApplicationResultJson>(
                    "{\"out_apply_no\":\"abcd12345FEGH\",\"sub_mchid\":\"123456789\"," +
                    "\"reject_reason\":\"材料不完整\",\"cancel_state\":\"REJECTED\"," +
                    "\"update_time\":\"2023-01-20T13:29:35+08:00\"}");
            var withdraw =
                JsonConvert.DeserializeObject<EcommerceLegacyCancelWithdrawQueryResultJson>(
                    "{\"withdrawl_apply\":{\"applyment_id\":\"20220101332222\"," +
                    "\"out_request_no\":\"1234567\",\"state\":\"BANK_REFUNDED\"," +
                    "\"fail_reason\":\"银行卡信息有误\"," +
                    "\"modify_time\":\"2015-05-20T13:29:35+08:00\"}}");

            Assert.AreEqual("REJECTED", cancel.cancel_state);
            Assert.AreEqual("BANK_REFUNDED", withdraw.withdrawl_apply.state);
            Assert.AreEqual("银行卡信息有误", withdraw.withdrawl_apply.fail_reason);
        }

        [TestMethod]
        public void CancellationUploadUsesFileNameAndFileDigestMetaFields()
        {
            var requestType = typeof(TenPayApiRequest);
            var styleType = requestType.GetNestedType(
                "MultipartMetaFieldStyle", BindingFlags.NonPublic);
            var createMeta = requestType.GetMethod(
                "CreateMultipartMetaJson",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(styleType);
            Assert.IsNotNull(createMeta);
            var cancellationStyle = Enum.Parse(styleType,
                "FileNameAndFileDigest");
            var commonStyle = Enum.Parse(styleType,
                "FilenameAndSha256");
            var cancellationMeta = JObject.Parse((string)createMeta.Invoke(null,
                new[] { "cancel.PNG", "abc123", cancellationStyle }));
            var commonMeta = JObject.Parse((string)createMeta.Invoke(null,
                new[] { "license.PNG", "def456", commonStyle }));

            Assert.AreEqual("cancel.PNG",
                cancellationMeta["file_name"]?.Value<string>());
            Assert.AreEqual("abc123",
                cancellationMeta["file_digest"]?.Value<string>());
            Assert.IsNull(cancellationMeta["filename"]);
            Assert.AreEqual("license.PNG", commonMeta["filename"]?.Value<string>());
            Assert.AreEqual("def456", commonMeta["sha256"]?.Value<string>());
            Assert.IsNull(commonMeta["file_name"]);
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
