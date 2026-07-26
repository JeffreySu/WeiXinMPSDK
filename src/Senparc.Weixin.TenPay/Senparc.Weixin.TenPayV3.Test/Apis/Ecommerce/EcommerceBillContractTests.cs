using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.TenPayV3.Apis.Ecommerce;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.Apis.Ecommerce
{
    [TestClass]
    public class EcommerceBillContractTests
    {
        private static readonly IReadOnlyDictionary<string, string>
            OfficialEndpoints = new Dictionary<string, string>
            {
                [nameof(EcommerceApis.ApplyAllSubMerchantFundflowBillAsync)] =
                    "v3/ecommerce/bill/fundflowbill",
                [nameof(EcommerceApis.ApplySingleSubMerchantFundflowBillAsync)] =
                    "v3/bill/sub-merchant-fundflowbill"
            };

        [TestMethod]
        public void ApiSurfaceContainsBillApplicationsAndDownloadOverloads()
        {
            var methods = typeof(EcommerceApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var applications = methods
                .Where(method => OfficialEndpoints.ContainsKey(method.Name))
                .GroupBy(method => method.Name)
                .ToDictionary(group => group.Key, group => group.Count());
            var downloads = methods.Where(method => method.Name ==
                nameof(EcommerceApis.DownloadEcommerceBillAsync)).ToArray();

            CollectionAssert.AreEquivalent(OfficialEndpoints.Keys.ToArray(),
                applications.Keys.ToArray());
            Assert.IsTrue(applications.Values.All(count => count == 1));
            Assert.AreEqual(2, downloads.Length);
            Assert.IsTrue(downloads.All(method =>
                method.ReturnType == typeof(Task<bool>)));
        }

        [TestMethod]
        public void BillApplicationsContainCurrentOfficialEndpoints()
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
        public void QueryValuesAreUriEncodedAndNullValuesAreSkipped()
        {
            var buildQuery = typeof(EcommerceApis).GetMethod(
                "BuildEcommerceBillQuery",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(buildQuery);
            var path = (string)buildQuery.Invoke(null, new object[]
            {
                "v3/ecommerce/bill/fundflowbill",
                new[]
                {
                    "bill_date", "2026-07-24",
                    "account_type", "ALL + FEES",
                    "tar_type", null
                }
            });

            Assert.AreEqual(
                "v3/ecommerce/bill/fundflowbill?bill_date=2026-07-24&account_type=ALL%20%2B%20FEES",
                path);
        }

        [TestMethod]
        public void RequestModelsUseCurrentDefaultsAndOfficialFields()
        {
            var all = JObject.FromObject(
                new EcommerceAllSubMerchantFundflowBillRequestData
                {
                    bill_date = "2026-07-24",
                    tar_type = "GZIP"
                });
            var single = JObject.FromObject(
                new EcommerceSingleSubMerchantFundflowBillRequestData
                {
                    sub_mchid = "1900000109",
                    bill_date = "2026-07-24",
                    account_type = "DEPOSIT",
                    algorithm = "SM4_GCM"
                });

            Assert.AreEqual("ALL", all["account_type"]?.Value<string>());
            Assert.AreEqual("AEAD_AES_256_GCM",
                all["algorithm"]?.Value<string>());
            Assert.AreEqual("1900000109",
                single["sub_mchid"]?.Value<string>());
            Assert.AreEqual("DEPOSIT",
                single["account_type"]?.Value<string>());
            Assert.AreEqual("SM4_GCM",
                single["algorithm"]?.Value<string>());
        }

        [TestMethod]
        public void ResultModelPreservesEncryptedBillMetadata()
        {
            var result = JsonConvert
                .DeserializeObject<EcommerceFundflowBillResultJson>(
                    "{\"download_bill_count\":1,\"download_bill_list\":[{" +
                    "\"bill_sequence\":1,\"hash_type\":\"SHA1\"," +
                    "\"hash_value\":\"79bb0f45\"," +
                    "\"download_url\":\"https://api.mch.weixin.qq.com/file\"," +
                    "\"encrypt_key\":\"encrypted-key\"," +
                    "\"nonce\":\"a8607ef79034c49c\"}]}");

            Assert.AreEqual(1, result.download_bill_count);
            Assert.AreEqual(1, result.download_bill_list.Single().bill_sequence);
            Assert.AreEqual("SHA1",
                result.download_bill_list.Single().hash_type);
            Assert.AreEqual("encrypted-key",
                result.download_bill_list.Single().encrypt_key);
            Assert.AreEqual("a8607ef79034c49c",
                result.download_bill_list.Single().nonce);
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(EcommerceFundflowBillResultJson)));
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
