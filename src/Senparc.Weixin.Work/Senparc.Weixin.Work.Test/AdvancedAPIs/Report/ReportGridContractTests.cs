using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Report;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Report
{
    [TestClass]
    public class ReportGridContractTests
    {
        [TestMethod]
        public void ReportGridApiContainsNineSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(ReportApi.AddReportGrid), typeof(ReportGridAddRequest), typeof(ReportGridAddResult)),
                (nameof(ReportApi.UpdateReportGrid), typeof(ReportGridUpdateRequest), typeof(ReportGridUpdateResult)),
                (nameof(ReportApi.DeleteReportGrid), typeof(ReportGridDeleteRequest), typeof(ReportGridDeleteResult)),
                (nameof(ReportApi.GetReportGridList), typeof(ReportGridListRequest), typeof(ReportGridListResult)),
                (nameof(ReportApi.GetUserReportGridInfo), typeof(ReportGridUserInfoRequest), typeof(ReportGridUserInfoResult)),
                (nameof(ReportApi.AddReportGridCategory), typeof(ReportGridCategoryAddRequest), typeof(ReportGridCategoryAddResult)),
                (nameof(ReportApi.UpdateReportGridCategory), typeof(ReportGridCategoryUpdateRequest), typeof(ReportGridCategoryUpdateResult)),
                (nameof(ReportApi.DeleteReportGridCategory), typeof(ReportGridCategoryDeleteRequest), typeof(ReportGridCategoryDeleteResult)),
                (nameof(ReportApi.GetReportGridCategoryList), typeof(ReportGridCategoryListRequest), typeof(ReportGridCategoryListResult))
            };

            foreach (var contract in contracts)
            {
                var parameterTypes = new[] { typeof(string), contract.Item2, typeof(int) };
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
        public void ReportGridApiUsesNineOfficialPathsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Report", "ReportApi.Grid.cs"));
            var contracts = new[]
            {
                ("/cgi-bin/report/grid/add", "94478"),
                ("/cgi-bin/report/grid/update", "94479"),
                ("/cgi-bin/report/grid/delete", "94480"),
                ("/cgi-bin/report/grid/list", "94481"),
                ("/cgi-bin/report/grid/get_user_grid_info", "94482"),
                ("/cgi-bin/report/grid/add_cata", "94536"),
                ("/cgi-bin/report/grid/update_cata", "94537"),
                ("/cgi-bin/report/grid/delete_cata", "94538"),
                ("/cgi-bin/report/grid/list_cata", "94540")
            };

            foreach (var contract in contracts)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + contract.Item1 + "\""), contract.Item1);
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + contract.Item2), contract.Item2);
            }

            Assert.AreEqual(18, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(18, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(18, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(18, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(18, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(9, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(9, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void ReportGridRequestsMatchOfficialShapes()
        {
            var serializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var addJson = JsonSerializer.Serialize(new ReportGridAddRequest
            {
                grid_name = "高新区",
                grid_parent_id = "grid-parent",
                grid_admin = new List<string> { "zhangsan" },
                grid_member = new List<string> { "lisi", "invalid-user" }
            }, serializerOptions);
            var updateJson = JsonSerializer.Serialize(new ReportGridUpdateRequest
            {
                grid_id = "grid-1",
                grid_name = "高新区一网格",
                grid_parent_id = "grid-parent",
                grid_admin = new List<string> { "zhangsan" },
                grid_member = new List<string> { "lisi" }
            }, serializerOptions);
            var deleteJson = JsonSerializer.Serialize(new ReportGridDeleteRequest { grid_id = "grid-1" }, serializerOptions);
            var listJson = JsonSerializer.Serialize(new ReportGridListRequest { grid_id = "grid-parent" }, serializerOptions);
            var userJson = JsonSerializer.Serialize(new ReportGridUserInfoRequest { userid = "zhangsan" }, serializerOptions);
            var addCategoryJson = JsonSerializer.Serialize(new ReportGridCategoryAddRequest
            {
                category_name = "市政设施",
                level = 2,
                parent_category_id = "category-parent"
            }, serializerOptions);
            var updateCategoryJson = JsonSerializer.Serialize(new ReportGridCategoryUpdateRequest
            {
                category_id = "category-1",
                category_name = "道路设施",
                level = 2,
                parent_category_id = "category-parent"
            }, serializerOptions);
            var deleteCategoryJson = JsonSerializer.Serialize(new ReportGridCategoryDeleteRequest
            {
                category_id = "category-1"
            }, serializerOptions);
            var listCategoryJson = JsonSerializer.Serialize(new ReportGridCategoryListRequest(), serializerOptions);

            StringAssert.Contains(addJson, "\"grid_name\":\"高新区\"");
            StringAssert.Contains(addJson, "\"grid_parent_id\":\"grid-parent\"");
            StringAssert.Contains(addJson, "\"grid_admin\":[\"zhangsan\"]");
            StringAssert.Contains(addJson, "\"grid_member\":[\"lisi\",\"invalid-user\"]");
            StringAssert.Contains(updateJson, "\"grid_id\":\"grid-1\"");
            StringAssert.Contains(updateJson, "\"grid_name\":\"高新区一网格\"");
            Assert.IsTrue(deleteJson == "{\"grid_id\":\"grid-1\"}", deleteJson);
            Assert.IsTrue(listJson == "{\"grid_id\":\"grid-parent\"}", listJson);
            Assert.IsTrue(userJson == "{\"userid\":\"zhangsan\"}", userJson);
            StringAssert.Contains(addCategoryJson, "\"category_name\":\"市政设施\"");
            StringAssert.Contains(addCategoryJson, "\"level\":2");
            StringAssert.Contains(addCategoryJson, "\"parent_category_id\":\"category-parent\"");
            StringAssert.Contains(updateCategoryJson, "\"category_id\":\"category-1\"");
            Assert.IsTrue(deleteCategoryJson == "{\"category_id\":\"category-1\"}", deleteCategoryJson);
            Assert.IsTrue(listCategoryJson == "{}", listCategoryJson);
        }

        [TestMethod]
        public void ReportGridResultsPreserveOfficialListAndCategoryFieldNames()
        {
            var add = JsonSerializer.Deserialize<ReportGridAddResult>(
                "{\"errcode\":0,\"grid_id\":\"grid-1\",\"invalid_userids\":[\"invalid-user\"]}");
            var update = JsonSerializer.Deserialize<ReportGridUpdateResult>(
                "{\"errcode\":0,\"invalid_userids\":[\"invalid-user\"]}");
            var list = JsonSerializer.Deserialize<ReportGridListResult>(
                "{\"errcode\":0,\"grid_list\":[{\"grid_id\":\"grid-1\",\"grid_name\":\"高新区\"," +
                "\"grid_parent_id\":\"grid-parent\",\"grid_admin\":[\"zhangsan\"]," +
                "\"grid_member\":[\"lisi\"]}]}");
            var user = JsonSerializer.Deserialize<ReportGridUserInfoResult>(
                "{\"errcode\":0,\"manage_grids\":[{\"grid_id\":\"grid-1\",\"grid_name\":\"高新区\"}]," +
                "\"joined_grids\":[{\"grid_id\":\"grid-2\",\"grid_name\":\"天府新区\"}]}");
            var addCategory = JsonSerializer.Deserialize<ReportGridCategoryAddResult>(
                "{\"errcode\":0,\"category_id\":\"category-1\"}");
            var categories = JsonSerializer.Deserialize<ReportGridCategoryListResult>(
                "{\"errcode\":0,\"cata_list\":[{\"cata_id\":\"category-1\",\"cata_name\":\"市政设施\"," +
                "\"level\":1},{\"cata_id\":\"category-2\",\"cata_name\":\"道路设施\",\"level\":2," +
                "\"parent_cata_id\":\"category-1\"}]}");

            Assert.IsNotNull(add);
            Assert.AreEqual("grid-1", add.grid_id);
            Assert.AreEqual("invalid-user", add.invalid_userids[0]);
            Assert.IsNotNull(update);
            Assert.AreEqual("invalid-user", update.invalid_userids[0]);
            Assert.IsNotNull(list);
            Assert.AreEqual("grid-parent", list.grid_list[0].grid_parent_id);
            Assert.AreEqual("lisi", list.grid_list[0].grid_member[0]);
            Assert.IsNotNull(user);
            Assert.AreEqual("grid-1", user.manage_grids[0].grid_id);
            Assert.AreEqual("grid-2", user.joined_grids[0].grid_id);
            Assert.IsNotNull(addCategory);
            Assert.AreEqual("category-1", addCategory.category_id);
            Assert.IsNotNull(categories);
            Assert.AreEqual("市政设施", categories.cata_list[0].cata_name);
            Assert.AreEqual("category-1", categories.cata_list[1].parent_cata_id);
        }

        [TestMethod]
        public void ReportGridPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = typeof(ReportGridAddRequest).Assembly.GetTypes()
                .Where(type => type.IsPublic && type.Namespace == typeof(ReportGridAddRequest).Namespace &&
                               type.Name.StartsWith("ReportGrid", StringComparison.Ordinal))
                .ToArray();

            Assert.AreEqual(21, modelTypes.Length);
            foreach (var property in modelTypes.SelectMany(type => type.GetProperties(BindingFlags.Public |
                         BindingFlags.Instance | BindingFlags.DeclaredOnly)))
            {
                Assert.AreNotEqual(typeof(object), property.PropertyType,
                    property.DeclaringType?.Name + "." + property.Name);
                if (property.PropertyType.IsGenericType)
                {
                    CollectionAssert.DoesNotContain(property.PropertyType.GetGenericArguments(), typeof(object));
                }
            }

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Report", "ReportGridJson.cs"));
            var declarationCount = source.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.TrimStart())
                .Count(line => line.StartsWith("public class ReportGrid", StringComparison.Ordinal) ||
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
