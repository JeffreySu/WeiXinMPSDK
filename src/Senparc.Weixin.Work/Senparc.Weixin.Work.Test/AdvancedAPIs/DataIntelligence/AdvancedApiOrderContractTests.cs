using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.DataIntelligence
{
    [TestClass]
    public class AdvancedApiOrderContractTests
    {
        [TestMethod]
        public void DataIntelligenceApiProvidesSixCurrentOrderSyncAndAsyncEntries()
        {
            var methodNames = typeof(DataIntelligenceApi)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            var syncMethods = new[]
            {
                nameof(DataIntelligenceApi.CreateAdvancedApiOrder),
                nameof(DataIntelligenceApi.CancelAdvancedApiOrder),
                nameof(DataIntelligenceApi.SubmitAdvancedApiOrderPayment),
                nameof(DataIntelligenceApi.GetAdvancedApiOrderList),
                nameof(DataIntelligenceApi.GetAdvancedApiOrder),
                nameof(DataIntelligenceApi.GetAdvancedApiCorpPurchaseInfo)
            };

            foreach (var methodName in syncMethods)
            {
                CollectionAssert.Contains(methodNames, methodName, methodName);
                CollectionAssert.Contains(methodNames, methodName + "Async",
                    methodName + "Async");
            }

            var source = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "DataIntelligence",
                "DataIntelligenceApi.Order.cs");
            var paths = new[]
            {
                "/cgi-bin/advanced_api/create_order",
                "/cgi-bin/advanced_api/cancel_order",
                "/cgi-bin/advanced_api/submit_pay",
                "/cgi-bin/advanced_api/list_order",
                "/cgi-bin/advanced_api/get_order",
                "/cgi-bin/advanced_api/get_corp_buy_info"
            };
            var documentIds = new[]
            {
                "100257", "100258", "100259", "100260", "100261", "100271"
            };

            foreach (var path in paths)
            {
                StringAssert.Contains(source, path);
            }

            foreach (var documentId in documentIds)
            {
                StringAssert.Contains(source, "/document/path/" + documentId);
            }

            StringAssert.Contains(source, "?provider_access_token={0}");
            StringAssert.Contains(source, "AdvancedApiOrderIgnoreNullJsonSetting");
            Assert.IsFalse(source.Contains("?access_token={0}"));
            Assert.AreEqual(13, CountOccurrences(source, "/// <summary>"));
        }

        [TestMethod]
        public void AdvancedApiOrderModelsPreserveOfficialFieldsAndLargeTimes()
        {
            var request = new AdvancedApiCreateOrderRequest
            {
                custom_corpid = "ww-customer",
                buyer_userid = "buyer",
                order_type = 3,
                chat_archive_api = new AdvancedApiCreateOrderChatArchive
                {
                    edition = 3,
                    old_edition = 2,
                    purchase_count = 100,
                    take_effect_time = 5178368698L,
                    old_edition_info = new AdvancedApiEditionInfo
                    {
                        begin_time = 4178368698L,
                        end_time = 5178368698L
                    }
                }
            };
            using (var requestJson = JsonDocument.Parse(JsonSerializer.Serialize(request)))
            {
                Assert.AreEqual(1,
                    requestJson.RootElement.GetProperty("advanced_api_type").GetInt32());
                Assert.AreEqual("ww-customer",
                    requestJson.RootElement.GetProperty("custom_corpid").GetString());
                Assert.AreEqual(5178368698L, requestJson.RootElement
                    .GetProperty("chat_archive_api")
                    .GetProperty("take_effect_time").GetInt64());
            }

            var list = Newtonsoft.Json.JsonConvert
                .DeserializeObject<AdvancedApiOrderListResult>(
                    "{\"errcode\":0,\"errmsg\":\"ok\",\"next_cursor\":\"NEXT\"," +
                    "\"has_more\":1,\"order_list\":[{\"order_id\":\"ORDER-1\"," +
                    "\"order_type\":3,\"order_status\":1," +
                    "\"create_time\":5178368698}]}");
            Assert.AreEqual(1, list.has_more);
            Assert.AreEqual("NEXT", list.next_cursor);
            Assert.AreEqual(5178368698L, list.order_list[0].create_time);

            var detail = Newtonsoft.Json.JsonConvert
                .DeserializeObject<AdvancedApiOrderDetailResult>(
                    "{\"errcode\":0,\"errmsg\":\"ok\",\"order\":{" +
                    "\"advanced_api_type\":1,\"order_id\":\"ORDER-1\"," +
                    "\"order_type\":3,\"order_status\":1," +
                    "\"custom_corpid\":\"encrypted-corpid\"," +
                    "\"create_time\":5178368698,\"buyer_userid\":\"buyer\"," +
                    "\"paid_price\":500000000,\"chat_archive_api\":{" +
                    "\"edition\":3,\"purchase_count\":100," +
                    "\"purchase_duration_days\":365," +
                    "\"take_effect_time\":5178368698," +
                    "\"end_time\":6178368698,\"original_price\":600000000}}}");
            Assert.AreEqual(500000000, detail.order.paid_price);
            Assert.AreEqual(6178368698L, detail.order.chat_archive_api.end_time);
            Assert.AreEqual(600000000, detail.order.chat_archive_api.original_price);

            var purchaseInfo = Newtonsoft.Json.JsonConvert
                .DeserializeObject<AdvancedApiCorpPurchaseInfoResult>(
                    "{\"errcode\":0,\"errmsg\":\"ok\"," +
                    "\"chat_archive_api_buy_info\":{\"edition_list\":[{" +
                    "\"edition\":3,\"purchase_count\":100," +
                    "\"begin_time\":5178368698,\"end_time\":6178368698}]}}");
            Assert.AreEqual(3,
                purchaseInfo.chat_archive_api_buy_info.edition_list[0].edition);
            Assert.AreEqual(6178368698L,
                purchaseInfo.chat_archive_api_buy_info.edition_list[0].end_time);
        }

        [TestMethod]
        public void AdvancedApiOrderModelsAreStronglyTypedAndFullyDocumented()
        {
            var modelTypes = typeof(AdvancedApiCreateOrderRequest).Assembly.GetTypes()
                .Where(type => type.IsClass && type.IsPublic &&
                    type.Namespace == typeof(AdvancedApiCreateOrderRequest).Namespace &&
                    type.Name.StartsWith("AdvancedApi", StringComparison.Ordinal))
                .ToArray();

            foreach (var modelType in modelTypes)
            {
                var untypedProperty = modelType
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                   BindingFlags.DeclaredOnly)
                    .FirstOrDefault(property => property.PropertyType == typeof(object));
                Assert.IsNull(untypedProperty,
                    $"{modelType.FullName}.{untypedProperty?.Name}");
            }

            var source = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "DataIntelligence",
                "AdvancedApiOrderJson.cs");
            var declarations = Regex.Matches(source,
                    @"^\s*public class ", RegexOptions.Multiline).Count +
                Regex.Matches(source,
                    @"^\s*public [^\r\n]+ \{ get; set; \}(?: = [^;]+;)?$",
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
