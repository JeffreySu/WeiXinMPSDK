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
    public class EcommerceRefundContractTests
    {
        private static readonly IReadOnlyDictionary<string, string>
            OfficialEndpoints = new Dictionary<string, string>
            {
                [nameof(EcommerceApis.ApplyEcommerceRefundAsync)] =
                    "v3/ecommerce/refunds/apply",
                [nameof(EcommerceApis.QueryEcommerceRefundByRefundIdAsync)] =
                    "v3/ecommerce/refunds/id/",
                [nameof(EcommerceApis.QueryEcommerceRefundByOutRefundNoAsync)] =
                    "v3/ecommerce/refunds/out-refund-no/",
                [nameof(EcommerceApis.QueryEcommerceRefundAdvanceReturnAsync)] =
                    "/return-advance",
                [nameof(EcommerceApis.ReturnEcommerceRefundAdvanceAsync)] =
                    "/return-advance",
                [nameof(EcommerceApis.ApplyEcommerceAbnormalRefundAsync)] =
                    "/apply-abnormal-refund"
            };

        [TestMethod]
        public void ApiSurfaceContainsAllSixCallableEntries()
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
        public void EveryCallableEntryContainsOfficialEcommerceEndpoint()
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

            var abnormal = typeof(EcommerceApis).GetMethod(
                nameof(EcommerceApis.ApplyEcommerceAbnormalRefundAsync));
            Assert.IsFalse(GetStringLiterals(abnormal).Any(value =>
                value.Contains("v3/refund/domestic/refunds")));
        }

        [TestMethod]
        public void PathAndSubMerchantQueryValuesAreUriEncoded()
        {
            var escape = typeof(EcommerceApis).GetMethod(
                "EscapeEcommerceRefundValue",
                BindingFlags.NonPublic | BindingFlags.Static);
            var addSubMerchant = typeof(EcommerceApis).GetMethod(
                "AddEcommerceRefundSubMerchant",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(escape);
            Assert.IsNotNull(addSubMerchant);
            Assert.AreEqual("refund%2Fid%20%2B%201",
                escape.Invoke(null, new object[] { "refund/id + 1" }));

            var path = (string)addSubMerchant.Invoke(null, new object[]
            {
                "v3/ecommerce/refunds/id/refund%2Fid",
                "sub + id"
            });

            Assert.AreEqual(
                "v3/ecommerce/refunds/id/refund%2Fid?sub_mchid=sub%20%2B%20id",
                path);
        }

        [TestMethod]
        public void ApplyRequestPreservesNestedFundsAndOfficialFields()
        {
            var json = JObject.FromObject(new EcommerceRefundRequestData
            {
                sub_mchid = "1900000109",
                sp_appid = "wx-platform",
                sub_appid = "wx-sub",
                transaction_id = "4200002026072400001",
                out_refund_no = "refund20260724",
                reason = "重复支付",
                amount = new EcommerceRefundRequestAmount
                {
                    refund = 101,
                    total = 202,
                    currency = "CNY",
                    from = new[]
                    {
                        new EcommerceRefundFundsFrom
                        {
                            account = "AVAILABLE",
                            amount = 101
                        }
                    }
                },
                notify_url = "https://example.com/refund-notify",
                refund_account = "REFUND_SOURCE_PARTNER_ADVANCE",
                funds_account = "AVAILABLE"
            });

            Assert.AreEqual("wx-platform", json["sp_appid"]?.Value<string>());
            Assert.AreEqual(101, json["amount"]?["refund"]?.Value<int>());
            Assert.AreEqual("AVAILABLE",
                json["amount"]?["from"]?[0]?["account"]?.Value<string>());
            Assert.AreEqual(101,
                json["amount"]?["from"]?[0]?["amount"]?.Value<int>());
            Assert.AreEqual("REFUND_SOURCE_PARTNER_ADVANCE",
                json["refund_account"]?.Value<string>());
            Assert.AreEqual("AVAILABLE",
                json["funds_account"]?.Value<string>());
        }

        [TestMethod]
        public void RefundResultPreservesAmountAndPromotionDetails()
        {
            var result = JsonConvert.DeserializeObject<EcommerceRefundResultJson>(
                "{\"refund_id\":\"503000070120260724000001\"," +
                "\"out_refund_no\":\"refund20260724\"," +
                "\"transaction_id\":\"4200002026072400001\"," +
                "\"status\":\"PROCESSING\",\"channel\":\"ORIGINAL\"," +
                "\"refund_account\":\"REFUND_SOURCE_PARTNER_ADVANCE\"," +
                "\"funds_account\":\"AVAILABLE\"," +
                "\"amount\":{\"refund\":101,\"payer_refund\":91," +
                "\"discount_refund\":10,\"advance\":51,\"currency\":\"CNY\"," +
                "\"from\":[{\"account\":\"AVAILABLE\",\"amount\":101}]}," +
                "\"promotion_detail\":[{\"promotion_id\":\"coupon-1\"," +
                "\"scope\":\"GLOBAL\",\"type\":\"COUPON\"," +
                "\"amount\":20,\"refund_amount\":10}]}");

            Assert.AreEqual("PROCESSING", result.status);
            Assert.AreEqual(51, result.amount.advance);
            Assert.AreEqual("AVAILABLE", result.amount.from.Single().account);
            Assert.AreEqual("coupon-1",
                result.promotion_detail.Single().promotion_id);
            Assert.AreEqual(10,
                result.promotion_detail.Single().refund_amount);
            Assert.AreEqual("AVAILABLE", result.funds_account);
        }

        [TestMethod]
        public void AdvanceReturnResultPreservesAccountsAndStatus()
        {
            var result = JsonConvert
                .DeserializeObject<EcommerceRefundAdvanceReturnResultJson>(
                    "{\"refund_id\":\"503000070120260724000001\"," +
                    "\"advance_return_id\":\"AR202607240001\"," +
                    "\"return_amount\":51,\"payer_mchid\":\"1900000109\"," +
                    "\"payer_account\":\"BASIC\",\"payee_mchid\":\"1900000001\"," +
                    "\"payee_account\":\"OPERATION\",\"result\":\"SUCCESS\"," +
                    "\"success_time\":\"2026-07-24T12:00:00+08:00\"}");

            Assert.AreEqual("AR202607240001", result.advance_return_id);
            Assert.AreEqual(51, result.return_amount);
            Assert.AreEqual("BASIC", result.payer_account);
            Assert.AreEqual("OPERATION", result.payee_account);
            Assert.AreEqual("SUCCESS", result.result);
        }

        [TestMethod]
        public void NotificationContractPreservesMerchantAndAmountFields()
        {
            var result = JsonConvert.DeserializeObject<EcommerceRefundNotifyJson>(
                "{\"sp_mchid\":\"1900000001\",\"sub_mchid\":\"1900000109\"," +
                "\"out_trade_no\":\"order20260724\"," +
                "\"transaction_id\":\"4200002026072400001\"," +
                "\"out_refund_no\":\"refund20260724\"," +
                "\"refund_id\":\"503000070120260724000001\"," +
                "\"refund_status\":\"SUCCESS\"," +
                "\"refund_account\":\"REFUND_SOURCE_SUB_MERCHANT\"," +
                "\"amount\":{\"total\":202,\"refund\":101," +
                "\"payer_total\":182,\"payer_refund\":91}}");

            Assert.AreEqual("1900000001", result.sp_mchid);
            Assert.AreEqual("1900000109", result.sub_mchid);
            Assert.AreEqual("REFUND_SOURCE_SUB_MERCHANT",
                result.refund_account);
            Assert.AreEqual(182, result.amount.payer_total);
            Assert.AreEqual(91, result.amount.payer_refund);
            Assert.AreEqual("REFUND.SUCCESS",
                EcommerceRefundNotificationTypes.Success);
            Assert.AreEqual("REFUND.ABNORMAL",
                EcommerceRefundNotificationTypes.Abnormal);
            Assert.AreEqual("REFUND.CLOSED",
                EcommerceRefundNotificationTypes.Closed);
            Assert.AreEqual("refund",
                EcommerceRefundNotificationTypes.OriginalType);
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(EcommerceRefundResultJson)));
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(EcommerceRefundAdvanceReturnResultJson)));
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(EcommerceRefundNotifyJson)));
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
