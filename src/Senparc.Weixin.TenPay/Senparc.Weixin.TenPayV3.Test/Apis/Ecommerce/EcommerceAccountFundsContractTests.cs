using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.TenPayV3.Apis.Ecommerce;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Senparc.Weixin.TenPayV3.Test.Apis.Ecommerce
{
    [TestClass]
    public class EcommerceAccountFundsContractTests
    {
        private static readonly IReadOnlyDictionary<string, string>
            OfficialEndpoints = new Dictionary<string, string>
            {
                [nameof(EcommerceApis.QuerySubMerchantBalanceAsync)] =
                    "v3/ecommerce/fund/balance/",
                [nameof(EcommerceApis.QuerySubMerchantDayEndBalanceAsync)] =
                    "v3/ecommerce/fund/enddaybalance/",
                [nameof(EcommerceApis.QueryPlatformBalanceAsync)] =
                    "v3/merchant/fund/balance/",
                [nameof(EcommerceApis.QueryPlatformDayEndBalanceAsync)] =
                    "v3/merchant/fund/dayendbalance/",
                [nameof(EcommerceApis.SubmitSubMerchantWithdrawalAsync)] =
                    "v3/ecommerce/fund/withdraw",
                [nameof(EcommerceApis.QuerySubMerchantWithdrawalByOutRequestNoAsync)] =
                    "v3/ecommerce/fund/withdraw/out-request-no/",
                [nameof(EcommerceApis.QuerySubMerchantWithdrawalByWithdrawIdAsync)] =
                    "v3/ecommerce/fund/withdraw/",
                [nameof(EcommerceApis.SubmitPlatformWithdrawalAsync)] =
                    "v3/merchant/fund/withdraw",
                [nameof(EcommerceApis.QueryPlatformWithdrawalByOutRequestNoAsync)] =
                    "v3/merchant/fund/withdraw/out-request-no/",
                [nameof(EcommerceApis.QueryPlatformWithdrawalByWithdrawIdAsync)] =
                    "v3/merchant/fund/withdraw/withdraw-id/",
                [nameof(EcommerceApis.SubmitSubMerchantDayEndWithdrawalAsync)] =
                    "v3/platsolution/ecommerce/withdraw/day-end-balance-withdraw",
                [nameof(EcommerceApis.QuerySubMerchantDayEndWithdrawalAsync)] =
                    "v3/platsolution/ecommerce/withdraw/day-end-balance-withdraw/out-request-no/",
                [nameof(EcommerceApis.QueryWithdrawalAbnormalBillAsync)] =
                    "v3/merchant/fund/withdraw/bill-type/"
            };

        [TestMethod]
        public void ApiSurfaceContainsAllThirteenCallableEntries()
        {
            var methods = typeof(EcommerceApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => OfficialEndpoints.ContainsKey(method.Name))
                .GroupBy(method => method.Name)
                .ToDictionary(group => group.Key, group => group.Count());

            CollectionAssert.AreEquivalent(OfficialEndpoints.Keys.ToArray(),
                methods.Keys.ToArray());
            Assert.IsTrue(methods.Values.All(count => count == 1));
        }

        [TestMethod]
        public void EveryCallableEntryContainsOfficialEndpoint()
        {
            foreach (var endpoint in OfficialEndpoints)
            {
                var method = typeof(EcommerceApis).GetMethod(endpoint.Key,
                    BindingFlags.Public | BindingFlags.Instance);

                Assert.IsNotNull(method, endpoint.Key);
                Assert.IsTrue(GetStringLiterals(method)
                        .Any(value => value.Contains(endpoint.Value)),
                    $"{endpoint.Key}: {endpoint.Value}");
            }
        }

        [TestMethod]
        public void PathAndQueryValuesAreUriEncodedAndNullValuesAreSkipped()
        {
            var escape = typeof(EcommerceApis).GetMethod(
                "EscapeAccountFundsValue",
                BindingFlags.NonPublic | BindingFlags.Static);
            var buildQuery = typeof(EcommerceApis).GetMethod(
                "BuildAccountFundsQuery",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(escape);
            Assert.IsNotNull(buildQuery);
            Assert.AreEqual("merchant%20%2B%20id",
                escape.Invoke(null, new object[] { "merchant + id" }));

            var path = (string)buildQuery.Invoke(null, new object[]
            {
                "v3/fund",
                new[]
                {
                    "date", "2026-07-24", "account_type", "BASIC + FEES",
                    "tar_type", null
                }
            });

            Assert.AreEqual(
                "v3/fund?date=2026-07-24&account_type=BASIC%20%2B%20FEES",
                path);
        }

        [TestMethod]
        public void WithdrawalRequestsUseOfficialFieldNames()
        {
            var subMerchant = JObject.FromObject(
                new EcommerceSubMerchantWithdrawalRequestData
                {
                    sub_mchid = "1900000109",
                    out_request_no = "withdraw20260724",
                    amount = 101,
                    remark = "结算款",
                    bank_memo = "平台提现",
                    account_type = "OPERATION",
                    notify_url = "https://example.com/notify"
                });
            var dayEnd = JObject.FromObject(
                new EcommerceSubMerchantDayEndWithdrawalRequestData
                {
                    sub_mchid = "1900000109",
                    out_request_no = "dayend20260724",
                    calculate_amount_type = "ALLOW_CURRENT_BALANCE",
                    reserve_amount = 100,
                    notify_url = "https://example.com/day-end-notify"
                });

            Assert.AreEqual(101, subMerchant["amount"]?.Value<int>());
            Assert.AreEqual("OPERATION",
                subMerchant["account_type"]?.Value<string>());
            Assert.AreEqual("https://example.com/notify",
                subMerchant["notify_url"]?.Value<string>());
            Assert.AreEqual("ALLOW_CURRENT_BALANCE",
                dayEnd["calculate_amount_type"]?.Value<string>());
            Assert.AreEqual(100, dayEnd["reserve_amount"]?.Value<int>());
        }

        [TestMethod]
        public void BalanceModelsPreserveOptionalPendingAmountAndAccountType()
        {
            var subMerchant =
                JsonConvert.DeserializeObject<EcommerceSubMerchantBalanceResultJson>(
                    "{\"sub_mchid\":\"1900000109\",\"available_amount\":10000," +
                    "\"pending_amount\":1,\"account_type\":\"BASIC\"}");
            var platform =
                JsonConvert.DeserializeObject<EcommercePlatformBalanceResultJson>(
                    "{\"available_amount\":20000}");

            Assert.AreEqual(1, subMerchant.pending_amount);
            Assert.AreEqual("BASIC", subMerchant.account_type);
            Assert.AreEqual(20000, platform.available_amount);
            Assert.IsNull(platform.pending_amount);
        }

        [TestMethod]
        public void StandardWithdrawalModelsPreserveStatusAndBankInformation()
        {
            var subMerchant =
                JsonConvert.DeserializeObject<EcommerceSubMerchantWithdrawalQueryResultJson>(
                    "{\"sp_mchid\":\"1900000001\",\"sub_mchid\":\"1900000109\"," +
                    "\"status\":\"REFUND\",\"withdraw_id\":\"W20260724\"," +
                    "\"out_request_no\":\"withdraw20260724\",\"amount\":101," +
                    "\"account_type\":\"OPERATION\",\"account_number\":\"1234\"," +
                    "\"account_bank\":\"ICBC\",\"bank_name\":\"工商银行\"}");
            var platform =
                JsonConvert.DeserializeObject<EcommercePlatformWithdrawalQueryResultJson>(
                    "{\"status\":\"FAIL\",\"withdraw_id\":\"W20260725\"," +
                    "\"out_request_no\":\"withdraw20260725\",\"amount\":202," +
                    "\"solution\":\"请核对结算账户\"}");

            Assert.AreEqual("REFUND", subMerchant.status);
            Assert.AreEqual("1900000109", subMerchant.sub_mchid);
            Assert.AreEqual("1234", subMerchant.account_number);
            Assert.AreEqual("请核对结算账户", platform.solution);
        }

        [TestMethod]
        public void DayEndWithdrawalModelPreservesAmountBreakdown()
        {
            var result =
                JsonConvert.DeserializeObject<EcommerceSubMerchantDayEndWithdrawalResultJson>(
                    "{\"sp_mchid\":\"1900000001\",\"sub_mchid\":\"1900000109\"," +
                    "\"status\":\"ABNORMAL\",\"withdraw_id\":\"D20260724\"," +
                    "\"out_request_no\":\"dayend20260724\",\"total_amount\":1000," +
                    "\"success_amount\":700,\"fail_amount\":200,\"refund_amount\":100}");

            Assert.AreEqual("ABNORMAL", result.status);
            Assert.AreEqual(1000, result.total_amount);
            Assert.AreEqual(700, result.success_amount);
            Assert.AreEqual(200, result.fail_amount);
            Assert.AreEqual(100, result.refund_amount);
        }

        [TestMethod]
        public void AbnormalBillAndNotificationContractsMatchOfficialValues()
        {
            var bill =
                JsonConvert.DeserializeObject<EcommerceWithdrawalAbnormalBillResultJson>(
                    "{\"hash_type\":\"SHA1\",\"hash_value\":\"abc123\"," +
                    "\"download_url\":\"https://api.mch.weixin.qq.com/file\"}");

            Assert.AreEqual("SHA1", bill.hash_type);
            Assert.AreEqual("abc123", bill.hash_value);
            Assert.AreEqual("MCHWITHDRAW.CHANGE",
                EcommerceWithdrawalNotificationTypes.EventType);
            Assert.AreEqual("mch_withdraw",
                EcommerceWithdrawalNotificationTypes.OriginalType);
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(EcommerceSubMerchantWithdrawalQueryResultJson)));
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(EcommercePlatformWithdrawalQueryResultJson)));
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(EcommerceSubMerchantDayEndWithdrawalResultJson)));
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
