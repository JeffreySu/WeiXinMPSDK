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
    public class EcommerceCrossBorderContractTests
    {
        private static readonly IReadOnlyDictionary<string, string>
            OfficialEndpoints = new Dictionary<string, string>
            {
                [nameof(EcommerceApis.QueryFundsToOverseaAvailableAmountAsync)] =
                    "v3/funds-to-oversea/transactions/",
                [nameof(EcommerceApis.ApplyFundsToOverseaAsync)] =
                    "v3/funds-to-oversea/orders",
                [nameof(EcommerceApis.QueryFundsToOverseaOrderAsync)] =
                    "v3/funds-to-oversea/orders/",
                [nameof(EcommerceApis.QueryFundsToOverseaBillDownloadUrlAsync)] =
                    "v3/funds-to-oversea/bill-download-url"
            };

        [TestMethod]
        public void ApiSurfaceContainsAllFourOfficialCrossBorderEndpoints()
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
        public void CrossBorderMethodsContainCurrentOfficialEndpointPaths()
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
        public void CrossBorderQueryValuesAreUriEncodedAndNullValuesAreSkipped()
        {
            var buildQuery = typeof(EcommerceApis).GetMethod(
                "BuildFundsToOverseaQuery",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(buildQuery);
            var path = (string)buildQuery.Invoke(null, new object[]
            {
                "v3/funds-to-oversea/bill-download-url",
                new[]
                {
                    "bill_date", "2026-07-25",
                    "sub_mchid", "1900 00+0109",
                    "optional", null
                }
            });

            Assert.AreEqual(
                "v3/funds-to-oversea/bill-download-url?bill_date=2026-07-25&sub_mchid=1900%2000%2B0109",
                path);
        }

        [TestMethod]
        public void ApplyRequestPreservesOfficialNestedFieldsAndLargeAmounts()
        {
            var request = JObject.FromObject(
                new EcommerceFundsToOverseaRequestData
                {
                    out_order_id = "merchant_1123123",
                    sub_mchid = "1900000109",
                    transaction_id = "4208450740201411110007820472",
                    amount = 5_000_000_000L,
                    foreign_currency = "USD",
                    goods_info = new[]
                    {
                        new EcommerceFundsToOverseaGoodsInfo
                        {
                            goods_name = "橘子",
                            goods_category = "食品/水果",
                            goods_unit_price = 2_500_000_000L,
                            goods_quantity = 2
                        }
                    },
                    seller_info = new EcommerceFundsToOverseaSellerInfo
                    {
                        oversea_business_name = "Oversea Company",
                        oversea_shop_name = "Oversea Shop",
                        seller_id = "seller-1"
                    },
                    express_info = new EcommerceFundsToOverseaExpressInfo
                    {
                        courier_number = "courier-1",
                        express_company_name = "Express Company"
                    },
                    payee_info = new EcommerceFundsToOverseaPayeeInfo
                    {
                        payee_id = "PAYEE-1"
                    },
                    presale_info = new EcommerceFundsToOverseaPresaleInfo
                    {
                        type = "BALANCE",
                        total_amount = 5_000_000_000L,
                        deposit_transaction_id = "deposit-transaction"
                    }
                });

            Assert.AreEqual(5_000_000_000L,
                request["amount"]?.Value<long>());
            Assert.AreEqual("食品/水果",
                request["goods_info"]?[0]?["goods_category"]?.Value<string>());
            Assert.AreEqual("Oversea Company",
                request["seller_info"]?["oversea_business_name"]?.Value<string>());
            Assert.AreEqual("courier-1",
                request["express_info"]?["courier_number"]?.Value<string>());
            Assert.AreEqual("PAYEE-1",
                request["payee_info"]?["payee_id"]?.Value<string>());
            Assert.AreEqual("deposit-transaction",
                request["presale_info"]?["deposit_transaction_id"]?.Value<string>());
        }

        [TestMethod]
        public void ResultModelsPreserveOptionalMoneyAndBillMetadata()
        {
            var order = JsonConvert
                .DeserializeObject<EcommerceFundsToOverseaOrderResultJson>(
                    "{\"out_order_id\":\"merchant-1\"," +
                    "\"result\":\"SUCCESS\",\"amount\":5000000000," +
                    "\"foreign_amount\":4900000000," +
                    "\"foreign_currency\":\"USD\"," +
                    "\"rate\":650000000,\"departure_amount\":4999999999," +
                    "\"fee\":1,\"charge_account_type\":\"BASIC\"}");
            var available = JsonConvert
                .DeserializeObject<EcommerceFundsToOverseaAvailableAmountResultJson>(
                    "{\"transaction_id\":\"4200\"," +
                    "\"available_abroad_amount\":5000000000}");
            var bill = JsonConvert
                .DeserializeObject<EcommerceFundsToOverseaBillResultJson>(
                    "{\"hash_type\":\"SHA1\",\"hash_value\":\"79bb0f45\"," +
                    "\"download_url\":\"https://api.mch.weixin.qq.com/file\"}");

            Assert.AreEqual(5_000_000_000L, order.amount);
            Assert.AreEqual(4_900_000_000L, order.foreign_amount);
            Assert.AreEqual(4_999_999_999L, order.departure_amount);
            Assert.AreEqual(5_000_000_000L,
                available.available_abroad_amount);
            Assert.AreEqual("SHA1", bill.hash_type);
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(EcommerceFundsToOverseaOrderResultJson)));
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(EcommerceFundsToOverseaBillResultJson)));
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
