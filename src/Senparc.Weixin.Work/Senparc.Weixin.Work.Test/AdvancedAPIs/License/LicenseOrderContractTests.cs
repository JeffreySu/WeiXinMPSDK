using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.License;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.License
{
    [TestClass]
    public class LicenseOrderContractTests
    {
        [TestMethod]
        public void LicenseApiProvidesThirteenCurrentOrderSyncAndAsyncEntries()
        {
            var methodNames = typeof(LicenseApi)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            var syncMethods = new[]
            {
                nameof(LicenseApi.CreateOrder),
                nameof(LicenseApi.CreateRenewOrderJob),
                nameof(LicenseApi.SubmitRenewOrderJob),
                nameof(LicenseApi.ListOrder),
                nameof(LicenseApi.GetOrder),
                nameof(LicenseApi.ListOrderAccount),
                nameof(LicenseApi.CancelOrder),
                nameof(LicenseApi.CreateMultiCorpOrderJob),
                nameof(LicenseApi.SubmitMultiCorpOrderJob),
                nameof(LicenseApi.GetMultiCorpOrderJobResult),
                nameof(LicenseApi.GetUnionOrder),
                nameof(LicenseApi.SubmitBalancePaymentJob),
                nameof(LicenseApi.GetBalancePaymentJobResult)
            };
            foreach (var methodName in syncMethods)
            {
                CollectionAssert.Contains(methodNames, methodName, methodName);
                CollectionAssert.Contains(methodNames, methodName + "Async",
                    methodName + "Async");
            }

            var source = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "License",
                "LicenseApi.Order.cs");
            foreach (var path in new[]
            {
                "/cgi-bin/license/create_new_order",
                "/cgi-bin/license/create_renew_order_job",
                "/cgi-bin/license/submit_order_job",
                "/cgi-bin/license/list_order",
                "/cgi-bin/license/get_order",
                "/cgi-bin/license/list_order_account",
                "/cgi-bin/license/cancel_order",
                "/cgi-bin/license/create_new_order_job",
                "/cgi-bin/license/submit_new_order_job",
                "/cgi-bin/license/new_order_job_result",
                "/cgi-bin/license/get_union_order",
                "/cgi-bin/license/submit_pay_job",
                "/cgi-bin/license/pay_job_result"
            })
            {
                StringAssert.Contains(source, path);
            }

            foreach (var documentId in new[]
            {
                "97182", "97183", "97184", "97185", "97186", "97187",
                "98887", "98888", "99420"
            })
            {
                StringAssert.Contains(source, "/document/path/" + documentId);
            }

            Assert.IsFalse(source.Contains("?access_token={0}"));
        }

        [TestMethod]
        public void LicenseOrderRequestsPreserveCountsDurationsJobsAndPayment()
        {
            var request = new LicenseCreateOrderRequest
            {
                corpid = "ww-corp",
                buyer_userid = "buyer",
                account_count = new LicenseAccountCount
                {
                    base_count = 100,
                    external_contact_count = 20
                },
                account_duration = new LicenseAccountDuration
                {
                    months = 12,
                    days = 20
                }
            };
            using (var json = JsonDocument.Parse(JsonSerializer.Serialize(request)))
            {
                Assert.AreEqual(100, json.RootElement.GetProperty("account_count")
                    .GetProperty("base_count").GetInt32());
                Assert.AreEqual(12, json.RootElement.GetProperty("account_duration")
                    .GetProperty("months").GetInt32());
            }

            var multi = new LicenseCreateMultiCorpOrderJobRequest
            {
                jobid = "JOB-ID",
                buy_list = new List<LicenseMultiCorpBuyItem>
                {
                    new LicenseMultiCorpBuyItem
                    {
                        corpid = "ww-corp",
                        account_count = new LicenseAccountCount { base_count = 10 },
                        account_duration = new LicenseAccountDuration { months = 6 },
                        auto_active_status = 1
                    }
                }
            };
            using (var json = JsonDocument.Parse(JsonSerializer.Serialize(multi)))
            {
                Assert.AreEqual("JOB-ID",
                    json.RootElement.GetProperty("jobid").GetString());
                Assert.AreEqual(1, json.RootElement.GetProperty("buy_list")[0]
                    .GetProperty("auto_active_status").GetInt32());
            }

            var payment = new LicenseSubmitPaymentJobRequest
            {
                order_id = "ORDER-ID",
                payer_userid = "payer"
            };
            using (var json = JsonDocument.Parse(JsonSerializer.Serialize(payment)))
            {
                Assert.AreEqual("payer",
                    json.RootElement.GetProperty("payer_userid").GetString());
            }
        }

        [TestMethod]
        public void LicenseOrderResultsPreserveLargeAmountsTimesAndFailures()
        {
            var order = Newtonsoft.Json.JsonConvert
                .DeserializeObject<LicenseGetOrderResult>(
                    "{\"errcode\":0,\"order\":{\"order_id\":\"ORDER-ID\"," +
                    "\"order_type\":1,\"order_status\":2,\"corpid\":\"ww-corp\"," +
                    "\"price\":5000000000,\"account_count\":{" +
                    "\"base_count\":100,\"external_contact_count\":20}," +
                    "\"account_duration\":{\"months\":12,\"days\":20," +
                    "\"new_expire_time\":6178368698}," +
                    "\"create_time\":4178368698,\"pay_time\":5178368698}}");
            Assert.AreEqual(5000000000L, order.order.price);
            Assert.AreEqual(6178368698L,
                order.order.account_duration.new_expire_time);
            Assert.AreEqual(5178368698L, order.order.pay_time);

            var union = Newtonsoft.Json.JsonConvert
                .DeserializeObject<LicenseUnionOrderResult>(
                    "{\"errcode\":0,\"order\":{\"order_id\":\"UNION\"," +
                    "\"order_type\":1,\"order_status\":2," +
                    "\"price\":6000000000,\"create_time\":4178368698," +
                    "\"pay_time\":5178368698},\"has_more\":1," +
                    "\"next_cursor\":\"NEXT\",\"buy_list\":[{" +
                    "\"sub_order_id\":\"SUB\",\"corpid\":\"ww-corp\"," +
                    "\"account_count\":{\"base_count\":10}," +
                    "\"account_duration\":{\"months\":12}}]}");
            Assert.AreEqual(6000000000L, union.order.price);
            Assert.AreEqual("SUB", union.buy_list[0].sub_order_id);

            var payment = Newtonsoft.Json.JsonConvert
                .DeserializeObject<LicensePaymentJobResult>(
                    "{\"errcode\":0,\"status\":3,\"pay_job_result\":{" +
                    "\"errcode\":700001,\"errmsg\":\"partial failure\"," +
                    "\"fail_corp_list\":[{\"corpid\":\"ww-fail\"," +
                    "\"errcode\":700002,\"errmsg\":\"failed\"}]}}");
            Assert.AreEqual(3, payment.status);
            Assert.AreEqual("ww-fail",
                payment.pay_job_result.fail_corp_list[0].corpid);
        }

        [TestMethod]
        public void LicenseOrderModelsAreStronglyTypedAndFullyDocumented()
        {
            var source = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "License",
                "LicenseOrderJson.cs");
            var declarations = Regex.Matches(source,
                    @"^\s*public class ", RegexOptions.Multiline).Count +
                Regex.Matches(source,
                    @"^\s*public [^\r\n]+ \{ get; set; \}$",
                    RegexOptions.Multiline).Count;
            Assert.AreEqual(declarations, CountOccurrences(source, "/// <summary>"));
            Assert.IsFalse(source.Contains("object "));
            Assert.IsFalse(source.Contains("dynamic "));

            var types = typeof(LicenseCreateOrderRequest).Assembly.GetTypes()
                .Where(type => type.IsPublic && type.IsClass &&
                    type.Namespace == typeof(LicenseCreateOrderRequest).Namespace &&
                    type.Name.StartsWith("License", StringComparison.Ordinal))
                .ToArray();
            foreach (var type in types)
            {
                var untyped = type.GetProperties(BindingFlags.Public |
                        BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .FirstOrDefault(property => property.PropertyType == typeof(object));
                Assert.IsNull(untyped, $"{type.FullName}.{untyped?.Name}");
            }
        }

        private static string ReadRepositoryFile(params string[] pathParts)
            => File.ReadAllText(Path.Combine(
                new[] { FindRepositoryRoot() }.Concat(pathParts).ToArray()));

        private static int CountOccurrences(string value, string search)
            => value.Split(new[] { search }, StringSplitOptions.None).Length - 1;

        private static string FindRepositoryRoot(
            [CallerFilePath] string sourceFilePath = null)
        {
            foreach (var startPath in new[]
            {
                Directory.GetCurrentDirectory(),
                AppContext.BaseDirectory,
                Path.GetDirectoryName(sourceFilePath)
            })
            {
                var directory = string.IsNullOrEmpty(startPath)
                    ? null
                    : new DirectoryInfo(startPath);
                while (directory != null)
                {
                    if (Directory.Exists(Path.Combine(directory.FullName, ".git")))
                    {
                        return directory.FullName;
                    }

                    directory = directory.Parent;
                }
            }

            Assert.Fail("无法定位仓库根目录。");
            return null;
        }
    }
}
