using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Report;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Report
{
    [TestClass]
    public class PatrolReportContractTests
    {
        [TestMethod]
        public void PatrolReportApiContainsSixSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(ReportApi.GetPatrolGridInfo), (Type)null, typeof(PatrolReportGridInfoResult)),
                (nameof(ReportApi.GetPatrolCorpStatus), typeof(PatrolReportCorpStatusRequest), typeof(PatrolReportCorpStatusResult)),
                (nameof(ReportApi.GetPatrolUserStatus), typeof(PatrolReportUserStatusRequest), typeof(PatrolReportUserStatusResult)),
                (nameof(ReportApi.GetPatrolCategoryStatistics), typeof(PatrolReportCategoryStatisticsRequest), typeof(PatrolReportCategoryStatisticsResult)),
                (nameof(ReportApi.GetPatrolOrderList), typeof(PatrolReportOrderListRequest), typeof(PatrolReportOrderListResult)),
                (nameof(ReportApi.GetPatrolOrderInfo), typeof(PatrolReportOrderInfoRequest), typeof(PatrolReportOrderInfoResult))
            };

            foreach (var contract in contracts)
            {
                var parameterTypes = contract.Item2 == null
                    ? new[] { typeof(string), typeof(int) }
                    : new[] { typeof(string), contract.Item2, typeof(int) };
                var syncMethod = typeof(ReportApi).GetMethod(contract.Item1, parameterTypes);
                var asyncMethod = typeof(ReportApi).GetMethod(contract.Item1 + "Async", parameterTypes);

                Assert.IsNotNull(syncMethod, contract.Item1);
                Assert.AreEqual(contract.Item3, syncMethod.ReturnType, contract.Item1);
                Assert.IsNotNull(asyncMethod, contract.Item1 + "Async");
                Assert.AreEqual(typeof(Task<>).MakeGenericType(contract.Item3), asyncMethod.ReturnType,
                    contract.Item1 + "Async");
            }
        }

        [TestMethod]
        public void PatrolReportApiUsesSixOfficialPathsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Report", "ReportApi.Patrol.cs"));
            var paths = new[]
            {
                "/cgi-bin/report/patrol/get_grid_info",
                "/cgi-bin/report/patrol/get_corp_status",
                "/cgi-bin/report/patrol/get_user_status",
                "/cgi-bin/report/patrol/category_statistic",
                "/cgi-bin/report/patrol/get_order_list",
                "/cgi-bin/report/patrol/get_order_info"
            };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, path), path);
            }

            foreach (var documentId in new[] { "93531", "93532", "93533", "93534", "93536", "93535" })
            {
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + documentId), documentId);
            }

            Assert.AreEqual(12, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(12, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(10, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(12, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(12, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(2, CountOccurrences(source, "CommonJsonSendType.GET"));
            Assert.AreEqual(5, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(5, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void PatrolReportRequestsMatchOfficialShapes()
        {
            var corpJson = JsonSerializer.Serialize(new PatrolReportCorpStatusRequest { grid_id = "grid-1" });
            var userJson = JsonSerializer.Serialize(new PatrolReportUserStatusRequest { userid = "zhangsan" });
            var categoryJson = JsonSerializer.Serialize(new PatrolReportCategoryStatisticsRequest
            {
                category_id = "category-1"
            });
            var listJson = JsonSerializer.Serialize(new PatrolReportOrderListRequest
            {
                begin_create_time = 5178368698L,
                begin_modify_time = 5178368799L,
                cursor = "NEXT",
                limit = 20
            });
            var infoJson = JsonSerializer.Serialize(new PatrolReportOrderInfoRequest { order_id = "order-1" });

            Assert.AreEqual("{\"grid_id\":\"grid-1\"}", corpJson);
            Assert.AreEqual("{\"userid\":\"zhangsan\"}", userJson);
            Assert.AreEqual("{\"category_id\":\"category-1\"}", categoryJson);
            StringAssert.Contains(listJson, "\"begin_create_time\":5178368698");
            StringAssert.Contains(listJson, "\"begin_modify_time\":5178368799");
            StringAssert.Contains(listJson, "\"cursor\":\"NEXT\"");
            StringAssert.Contains(listJson, "\"limit\":20");
            Assert.AreEqual("{\"order_id\":\"order-1\"}", infoJson);
        }

        [TestMethod]
        public void PatrolReportResultsPreserveNestedOrdersAndLargeValues()
        {
            var grid = JsonSerializer.Deserialize<PatrolReportGridInfoResult>(
                "{\"errcode\":0,\"grid_list\":[{\"grid_id\":\"grid-1\",\"grid_name\":\"高新区\"," +
                "\"grid_admin\":[\"zhangsan\",\"lisi\"]}]}");
            var corp = JsonSerializer.Deserialize<PatrolReportCorpStatusResult>(
                "{\"errcode\":0,\"to_be_assigned\":5178368698,\"processing\":2,\"added_today\":3," +
                "\"solved_today\":4,\"total_case\":5178368799,\"total_solved\":7}");
            var user = JsonSerializer.Deserialize<PatrolReportUserStatusResult>(
                "{\"errcode\":0,\"processing\":9,\"added_today\":10,\"solved_today\":11}");
            var category = JsonSerializer.Deserialize<PatrolReportCategoryStatisticsResult>(
                "{\"errcode\":0,\"dashboard_list\":[{\"category_id\":\"category-1\"," +
                "\"category_name\":\"市政设施\",\"category_level\":2,\"category_type\":1," +
                "\"total_case\":5178368899,\"total_solved\":12}]}");
            const string orderJson = "{\"errcode\":0,\"next_cursor\":\"NEXT\",\"order_list\":[{" +
                "\"order_id\":\"order-1\",\"desc\":\"井盖损坏\",\"urge_type\":1," +
                "\"case_name\":\"市政设施\",\"grid_name\":\"高新区\",\"grid_id\":\"grid-1\"," +
                "\"image_urls\":[\"https://example.test/1.jpg\"],\"video_media_ids\":[\"media-1\"]," +
                "\"create_time\":5178368698,\"location\":{\"name\":\"天府广场\"," +
                "\"address\":\"人民南路\",\"longitude\":104.063291,\"latitude\":30.547239}," +
                "\"processor_userids\":[\"worker-1\"],\"process_list\":[{\"process_type\":1," +
                "\"solve_userid\":\"worker-1\",\"process_desc\":\"已处理\",\"status\":2," +
                "\"solved_time\":5178368799,\"image_urls\":[\"https://example.test/2.jpg\"]," +
                "\"video_media_ids\":[\"media-2\"]}]}]}";
            var list = JsonSerializer.Deserialize<PatrolReportOrderListResult>(orderJson);
            var detail = JsonSerializer.Deserialize<PatrolReportOrderInfoResult>(
                "{\"errcode\":0,\"order_info\":" + JsonSerializer.Serialize(list.order_list[0]) + "}");

            Assert.IsNotNull(grid);
            Assert.AreEqual("lisi", grid.grid_list[0].grid_admin[1]);
            Assert.IsNotNull(corp);
            Assert.AreEqual(5178368698L, corp.to_be_assigned);
            Assert.AreEqual(5178368799L, corp.total_case);
            Assert.IsNotNull(user);
            Assert.AreEqual(11L, user.solved_today);
            Assert.IsNotNull(category);
            Assert.AreEqual(5178368899L, category.dashboard_list[0].total_case);
            Assert.IsNotNull(list);
            Assert.AreEqual("order-1", list.order_list[0].order_id);
            Assert.AreEqual(104.063291m, list.order_list[0].location.longitude);
            Assert.AreEqual(5178368799L, list.order_list[0].process_list[0].solved_time);
            Assert.IsNotNull(detail);
            Assert.AreEqual("worker-1", detail.order_info.processor_userids[0]);
        }

        [TestMethod]
        public void PatrolReportPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = typeof(PatrolReportGrid).Assembly.GetTypes()
                .Where(type => type.IsPublic && type.Namespace == typeof(PatrolReportGrid).Namespace &&
                               type.Name.StartsWith("PatrolReport", StringComparison.Ordinal))
                .ToArray();

            Assert.AreEqual(16, modelTypes.Length);
            foreach (var property in modelTypes.SelectMany(type => type.GetProperties(BindingFlags.Public |
                         BindingFlags.Instance | BindingFlags.DeclaredOnly)))
            {
                Assert.AreNotEqual(typeof(object), property.PropertyType, property.DeclaringType?.Name + "." + property.Name);
                if (property.PropertyType.IsGenericType)
                {
                    CollectionAssert.DoesNotContain(property.PropertyType.GetGenericArguments(), typeof(object));
                }
            }

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Report", "PatrolReportJson.cs"));
            var declarationCount = source.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.TrimStart())
                .Count(line => line.StartsWith("public class PatrolReport", StringComparison.Ordinal) ||
                               line.StartsWith("public ", StringComparison.Ordinal) &&
                               line.Contains("{ get; set; }", StringComparison.Ordinal));
            Assert.AreEqual(declarationCount, CountOccurrences(source, "/// <summary>"));
        }

        private static int CountOccurrences(string source, string value)
            => source.Split(new[] { value }, StringSplitOptions.None).Length - 1;

        private static string FindRepositoryRoot([CallerFilePath] string sourceFilePath = null)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(sourceFilePath));
            while (directory != null)
            {
                if (Directory.Exists(Path.Combine(directory.FullName, ".git")))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            Assert.Fail("Unable to locate repository root.");
            return null;
        }
    }
}
