using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.MerchantGovernance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Senparc.Weixin.TenPayV3.Test.Apis.MerchantGovernance
{
    [TestClass]
    public class MerchantGovernanceContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(InactiveMerchantVerificationApis.StartVerificationAsync)] =
                    "v3/compliance/inactive-merchant-identity-verification/merchants",
                [nameof(InactiveMerchantVerificationApis.QueryVerificationAsync)] =
                    "v3/compliance/inactive-merchant-identity-verification/merchants/",
                [nameof(MerchantLimitationApis.QuerySubMerchantLimitationAsync)] =
                    "v3/mch-operation-manage/merchant-limitations/sub-mchid/"
            };

        [TestMethod]
        public void ApiSurfaceContainsThreeOfficialEntries()
        {
            var inactiveMethods = typeof(InactiveMerchantVerificationApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.DeclaringType == typeof(InactiveMerchantVerificationApis))
                .Select(method => method.Name)
                .ToArray();
            var limitationMethods = typeof(MerchantLimitationApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.DeclaringType == typeof(MerchantLimitationApis))
                .Select(method => method.Name)
                .ToArray();

            CollectionAssert.AreEquivalent(new[]
            {
                nameof(InactiveMerchantVerificationApis.StartVerificationAsync),
                nameof(InactiveMerchantVerificationApis.QueryVerificationAsync)
            }, inactiveMethods);
            CollectionAssert.AreEquivalent(new[]
            {
                nameof(MerchantLimitationApis.QuerySubMerchantLimitationAsync)
            }, limitationMethods);
        }

        [TestMethod]
        public void EveryEntryContainsOfficialEndpoint()
        {
            foreach (var endpoint in OfficialEndpoints)
            {
                var declaringType = endpoint.Key ==
                                    nameof(MerchantLimitationApis.QuerySubMerchantLimitationAsync)
                    ? typeof(MerchantLimitationApis)
                    : typeof(InactiveMerchantVerificationApis);
                var methods = declaringType
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
            foreach (var type in new[]
                     {
                         typeof(InactiveMerchantVerificationApis),
                         typeof(MerchantLimitationApis)
                     })
            {
                var escape = type.GetMethod("Escape",
                    BindingFlags.NonPublic | BindingFlags.Static);

                Assert.IsNotNull(escape);
                Assert.AreEqual("merchant%20%2B%20id",
                    escape.Invoke(null, new object[] { "merchant + id" }));
            }
        }

        [TestMethod]
        public void StartRequestSerializesOfficialSubMerchantField()
        {
            var json = JsonConvert.SerializeObject(
                new InactiveMerchantVerificationRequestData
                {
                    sub_mchid = "1900000000"
                });

            Assert.AreEqual("{\"sub_mchid\":\"1900000000\"}", json);
        }

        [TestMethod]
        public void VerificationResultPreservesOfficialStateFields()
        {
            var result = JsonConvert.DeserializeObject<InactiveMerchantVerificationResultJson>(
                "{\"sub_mchid\":\"1900000000\",\"verification_id\":\"28011678863778000000123124312\"," +
                "\"state\":\"FAIL\",\"fail_reason\":\"MATERIALS_ABNORMAL\"," +
                "\"create_time\":\"2020-01-01T00:00:00+08:00\"," +
                "\"finish_time\":\"2020-01-01T00:05:00+08:00\"}");

            Assert.AreEqual("1900000000", result.sub_mchid);
            Assert.AreEqual("FAIL", result.state);
            Assert.AreEqual("MATERIALS_ABNORMAL", result.fail_reason);
            Assert.AreEqual("2020-01-01T00:05:00+08:00", result.finish_time);
        }

        [TestMethod]
        public void LimitationResultPreservesNestedRecoverySpecification()
        {
            var result = JsonConvert.DeserializeObject<MerchantLimitationResultJson>(
                "{\"mchid\":\"123000110\",\"limited_functions\":[\"NO_TRANSACTION\"]," +
                "\"other_limited_functions\":\"关闭相册扫码支付\",\"recovery_specifications\":[{" +
                "\"limitation_case_id\":\"A20250819155047774441874\"," +
                "\"limitation_reason_type\":\"NO_TRADE\",\"relate_limitations\":[\"NO_TRANSACTION\"]," +
                "\"recover_way\":\"VERIFY_INACTIVE_MERCHANT_IDENTITY\"," +
                "\"recover_way_param\":\"100200300112233\"," +
                "\"limitation_action_type\":\"LIMIT_ACTION_TYPE_DELAY_CONTROL\"," +
                "\"limitation_start_date\":\"2025-06-08T10:34:56+08:00\"}]}");

            Assert.AreEqual("123000110", result.mchid);
            CollectionAssert.AreEqual(new[] { "NO_TRANSACTION" },
                result.limited_functions);
            Assert.AreEqual("A20250819155047774441874",
                result.recovery_specifications.Single().limitation_case_id);
            Assert.AreEqual("VERIFY_INACTIVE_MERCHANT_IDENTITY",
                result.recovery_specifications.Single().recover_way);
            Assert.AreEqual("LIMIT_ACTION_TYPE_DELAY_CONTROL",
                result.recovery_specifications.Single().limitation_action_type);
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
