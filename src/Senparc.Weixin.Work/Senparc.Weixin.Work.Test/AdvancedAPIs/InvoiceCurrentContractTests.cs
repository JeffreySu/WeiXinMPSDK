using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
{
    [TestClass]
    public class InvoiceCurrentContractTests
    {
        [TestMethod]
        public void InvoiceApiProvidesCurrentBatchQuerySyncAndAsyncEntries()
        {
            var methodNames = typeof(InvoiceApi)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            CollectionAssert.Contains(methodNames, nameof(InvoiceApi.GetInvoiceInfoBatch));
            CollectionAssert.Contains(methodNames,
                nameof(InvoiceApi.GetInvoiceInfoBatchAsync));
            CollectionAssert.Contains(methodNames, nameof(InvoiceApi.GetInvoiceListInfo));
            CollectionAssert.Contains(methodNames,
                nameof(InvoiceApi.GetInvoiceListInfoAsync));

            var source = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Invoice",
                "InvoiceApi.Batch.cs");
            StringAssert.Contains(source,
                "/cgi-bin/card/invoice/reimburse/getinvoiceinfobatch");
            StringAssert.Contains(source, "/document/path/90287");
            StringAssert.Contains(source, "CommonJsonSendType.POST");
            Assert.IsFalse(source.Contains("/getinvoicebatch"));
            Assert.AreEqual(3, CountOccurrences(source, "/// <summary>"));
        }

        [TestMethod]
        public void InvoiceBatchModelsPreserveOfficialAndExampleFields()
        {
            var request = new GetInvoiceInfoBatchRequest
            {
                item_list = new List<InvoiceItem>
                {
                    new InvoiceItem
                    {
                        card_id = "CARD-ID",
                        encrypt_code = "ENCRYPT-CODE"
                    }
                }
            };
            using (var requestJson = JsonDocument.Parse(JsonSerializer.Serialize(request)))
            {
                var item = requestJson.RootElement.GetProperty("item_list")[0];
                Assert.AreEqual("CARD-ID", item.GetProperty("card_id").GetString());
                Assert.AreEqual("ENCRYPT-CODE",
                    item.GetProperty("encrypt_code").GetString());
            }

            var result = Newtonsoft.Json.JsonConvert
                .DeserializeObject<GetInvoiceInfoBatchResultJson>(
                    "{\"errcode\":0,\"errmsg\":\"ok\",\"item_list\":[{" +
                    "\"card_id\":\"CARD-ID\",\"begin_time\":4178368698," +
                    "\"end_time\":5178368698,\"openid\":\"OPEN-ID\"," +
                    "\"type\":\"增值税电子普通发票\",\"payee\":\"收款方\"," +
                    "\"detail\":\"发票详情\",\"user_info\":{" +
                    "\"fee\":5000000000,\"billing_time\":4178368698," +
                    "\"pdf_url\":\"https://example/invoice.pdf\"," +
                    "\"trip_pdf_url\":\"https://example/trip.pdf\"," +
                    "\"reimburse_status\":\"INVOICE_REIMBURSE_INIT\"," +
                    "\"order_id\":\"ORDER-ID\",\"info\":[{" +
                    "\"name\":\"服务\",\"num\":2,\"unit\":\"项\"," +
                    "\"fee\":5000000000,\"price\":2500000000}]}}]}" );

            var invoice = result.item_list[0];
            Assert.AreEqual(5178368698L, invoice.end_time);
            Assert.AreEqual(5000000000L, invoice.user_info.fee);
            Assert.AreEqual("https://example/trip.pdf",
                invoice.user_info.trip_pdf_url);
            Assert.AreEqual("ORDER-ID", invoice.user_info.order_id);
            Assert.AreEqual(2500000000L, invoice.user_info.info[0].price);
        }

        [TestMethod]
        public void InvoiceBatchModelsAreStronglyTypedAndFullyDocumented()
        {
            var modelTypes = new[]
            {
                typeof(GetInvoiceInfoBatchRequest),
                typeof(GetInvoiceInfoBatchResultJson),
                typeof(GetInvoiceInfoBatchItem),
                typeof(InvoiceBatchUserInfo),
                typeof(InvoiceBatchProjectInfo)
            };
            foreach (var modelType in modelTypes)
            {
                Assert.IsFalse(modelType.GetProperties(BindingFlags.Public |
                    BindingFlags.Instance | BindingFlags.DeclaredOnly).Any(property =>
                    property.PropertyType == typeof(object)), modelType.FullName);
            }

            var source = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Invoice", "InvoiceJson",
                "InvoiceBatchJson.cs");
            var declarations = Regex.Matches(source,
                    @"^\s*public class ", RegexOptions.Multiline).Count +
                Regex.Matches(source,
                    @"^\s*public [^\r\n]+ \{ get; set; \}$",
                    RegexOptions.Multiline).Count;
            Assert.AreEqual(declarations, CountOccurrences(source, "/// <summary>"));
            Assert.IsFalse(source.Contains("object "));
            Assert.IsFalse(source.Contains("dynamic "));
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
