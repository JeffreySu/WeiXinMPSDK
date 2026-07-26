/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDocSmartDocumentContractTests.cs
    文件功能描述：企业微信智能文档内容管理契约测试


    创建标识：Senparc - 20260725

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.WeDoc;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.WeDoc
{
    [TestClass]
    public class WeDocSmartDocumentContractTests
    {
        [TestMethod]
        public void SmartDocumentApisExposeSeventeenSyncAndAsyncContracts()
        {
            var contracts = new[]
            {
                new { Name = nameof(WeDocApi.AddSmartDocumentPage), Request = typeof(WeDocSmartDocumentAddPageRequest), Result = typeof(WeDocSmartDocumentPageResult), Path = "/cgi-bin/wedoc/smartdoc/add_page", Doc = "101620" },
                new { Name = nameof(WeDocApi.UpdateSmartDocumentPage), Request = typeof(WeDocSmartDocumentUpdatePageRequest), Result = typeof(WeDocSmartDocumentPageResult), Path = "/cgi-bin/wedoc/smartdoc/update_page", Doc = "101621" },
                new { Name = nameof(WeDocApi.DeleteSmartDocumentPage), Request = typeof(WeDocSmartDocumentDeletePageRequest), Result = typeof(WorkJsonResult), Path = "/cgi-bin/wedoc/smartdoc/delete_page", Doc = "101622" },
                new { Name = nameof(WeDocApi.GetSmartDocumentPageHierarchy), Request = typeof(WeDocSmartDocumentRequest), Result = typeof(WeDocSmartDocumentPageHierarchyResult), Path = "/cgi-bin/wedoc/smartdoc/get_page_hierarchy", Doc = "101619" },
                new { Name = nameof(WeDocApi.AddSmartDocumentBlocks), Request = typeof(WeDocSmartDocumentAddBlocksRequest), Result = typeof(WeDocSmartDocumentBlocksResult), Path = "/cgi-bin/wedoc/smartdoc/add_blocks", Doc = "101623" },
                new { Name = nameof(WeDocApi.UpdateSmartDocumentBlocks), Request = typeof(WeDocSmartDocumentUpdateBlocksRequest), Result = typeof(WeDocSmartDocumentBlocksResult), Path = "/cgi-bin/wedoc/smartdoc/update_blocks", Doc = "101624" },
                new { Name = nameof(WeDocApi.DeleteSmartDocumentBlocks), Request = typeof(WeDocSmartDocumentDeleteBlocksRequest), Result = typeof(WorkJsonResult), Path = "/cgi-bin/wedoc/smartdoc/delete_blocks", Doc = "101625" },
                new { Name = nameof(WeDocApi.GetSmartDocumentBlocks), Request = typeof(WeDocSmartDocumentGetBlocksRequest), Result = typeof(WeDocSmartDocumentBlockListResult), Path = "/cgi-bin/wedoc/smartdoc/get_block_list", Doc = "101626" },
                new { Name = nameof(WeDocApi.CreateSmartDocumentExportTask), Request = typeof(WeDocSmartDocumentExportTaskRequest), Result = typeof(WeDocSmartDocumentExportTaskResult), Path = "/cgi-bin/wedoc/smartdoc/export_task", Doc = "101627" },
                new { Name = nameof(WeDocApi.GetSmartDocumentExportResult), Request = typeof(WeDocSmartDocumentExportResultRequest), Result = typeof(WeDocSmartDocumentExportResult), Path = "/cgi-bin/wedoc/smartdoc/get_export_result", Doc = "101627" },
                new { Name = nameof(WeDocApi.GetSmartDocumentDataSource), Request = typeof(WeDocSmartDocumentRequest), Result = typeof(WeDocSmartDocumentDataSourceResult), Path = "/cgi-bin/wedoc/smartdoc/get_smartsheet_info", Doc = "101628" },
                new { Name = nameof(WeDocApi.AddSmartDocumentDataTable), Request = typeof(WeDocSmartDocumentAddDataTableRequest), Result = typeof(WeDocSmartDocumentDataTableResult), Path = "/cgi-bin/wedoc/smartdoc/add_smartsheet", Doc = "101629" },
                new { Name = nameof(WeDocApi.DeleteSmartDocumentDataTable), Request = typeof(WeDocSmartDocumentDeleteDataTableRequest), Result = typeof(WorkJsonResult), Path = "/cgi-bin/wedoc/smartdoc/delete_smartsheet", Doc = "101630" },
                new { Name = nameof(WeDocApi.UpdateSmartDocumentDataTable), Request = typeof(WeDocSmartDocumentUpdateDataTableRequest), Result = typeof(WeDocSmartDocumentDataTableResult), Path = "/cgi-bin/wedoc/smartdoc/update_smartsheet", Doc = "101631" },
                new { Name = nameof(WeDocApi.PublishSmartDocument), Request = typeof(WeDocSmartDocumentPublishRequest), Result = typeof(WeDocSmartDocumentPublishResult), Path = "/cgi-bin/wedoc/smartdoc/publish", Doc = "101616" },
                new { Name = nameof(WeDocApi.CancelSmartDocumentPublish), Request = typeof(WeDocSmartDocumentRequest), Result = typeof(WorkJsonResult), Path = "/cgi-bin/wedoc/smartdoc/cancel_publish", Doc = "101617" },
                new { Name = nameof(WeDocApi.UpdateSmartDocumentPublishSetting), Request = typeof(WeDocSmartDocumentPublishSettingRequest), Result = typeof(WorkJsonResult), Path = "/cgi-bin/wedoc/smartdoc/publish_setting", Doc = "101618" }
            };

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "WeDoc",
                "WeDocApi.SmartDoc.cs"));
            foreach (var contract in contracts)
            {
                var syncMethod = typeof(WeDocApi).GetMethod(contract.Name,
                    BindingFlags.Public | BindingFlags.Static);
                var asyncMethod = typeof(WeDocApi).GetMethod(contract.Name + "Async",
                    BindingFlags.Public | BindingFlags.Static);
                Assert.IsNotNull(syncMethod, contract.Name);
                Assert.IsNotNull(asyncMethod, contract.Name + "Async");
                Assert.AreEqual(contract.Request, syncMethod.GetParameters()[1].ParameterType,
                    contract.Name);
                Assert.AreEqual(contract.Result, syncMethod.ReturnType, contract.Name);
                Assert.AreEqual(contract.Request, asyncMethod.GetParameters()[1].ParameterType,
                    contract.Name + "Async");
                Assert.AreEqual(contract.Result, asyncMethod.ReturnType.GenericTypeArguments.Single(),
                    contract.Name + "Async");
                Assert.IsTrue(source.Contains(contract.Path, StringComparison.Ordinal), contract.Path);
                Assert.IsTrue(source.Contains("document/path/" + contract.Doc,
                    StringComparison.Ordinal), contract.Doc);
            }

            Assert.AreEqual(34, CountOccurrences(source,
                "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(34, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(34, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(34, CountOccurrences(source, "/// <returns>"));
        }

        [TestMethod]
        public void SmartDocumentModelsPreserveBlocksLargeIdsAndOfficialResponseShapes()
        {
            var blocks = JsonConvert.DeserializeObject<WeDocSmartDocumentBlockListResult>(
                "{\"errcode\":0,\"blocks\":[{\"id\":\"BLOCK_ID\"," +
                "\"type\":\"BLOCK_TYPE_CODE\",\"content\":\"Console.WriteLine();\"," +
                "\"children\":[\"CHILD_ID\"],\"props\":{\"code_props\":{" +
                "\"code_language\":\"CODE_LANGUAGE_CSHARP\",\"code_wrap\":true}}}]," +
                "\"has_more\":\"true\",\"next_start\":201}");
            Assert.IsNotNull(blocks);
            Assert.AreEqual("CODE_LANGUAGE_CSHARP",
                blocks.blocks[0].props.code_props.code_language);
            Assert.AreEqual("CHILD_ID", blocks.blocks[0].children[0]);
            Assert.AreEqual("true", blocks.has_more);

            var publish = JsonConvert.DeserializeObject<WeDocSmartDocumentPublishResult>(
                "{\"errcode\":0,\"share_code\":\"SHARE_CODE\"," +
                "\"publish_url\":\"https://doc.weixin.qq.com/smartdoc/example\"," +
                "\"version\":5000000000,\"publish_time\":6000000000," +
                "\"publish_doc_title\":\"Title\"}");
            Assert.IsNotNull(publish);
            Assert.AreEqual(5000000000UL, publish.version);
            Assert.AreEqual(6000000000UL, publish.publish_time);

            var publishJson = JsonConvert.SerializeObject(new WeDocSmartDocumentPublishRequest
            {
                docid = "DOC_ID",
                publish_range = 4,
                auth_list = new List<WeDocSmartDocumentPublishAuth>
                {
                    new WeDocSmartDocumentPublishAuth { type = 1, userid = "USER_ID" },
                    new WeDocSmartDocumentPublishAuth { type = 2, departmentid = 5000000000UL }
                }
            });
            StringAssert.Contains(publishJson, "\"departmentid\":5000000000");

            var export = JsonConvert.DeserializeObject<WeDocSmartDocumentExportResult>(
                "{\"errcode\":0,\"task_done\":true,\"content\":\"# Title\\nBody\"}");
            Assert.IsNotNull(export);
            Assert.IsTrue(export.task_done);
            Assert.AreEqual("# Title\nBody", export.content);
        }

        [TestMethod]
        public void SmartDocumentPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = typeof(WeDocSmartDocumentRequest).Assembly.GetTypes()
                .Where(type => type.IsPublic &&
                               type.Namespace == typeof(WeDocSmartDocumentRequest).Namespace &&
                               type.Name.StartsWith("WeDocSmartDocument", StringComparison.Ordinal))
                .ToArray();
            var objectProperties = modelTypes
                .SelectMany(type => type.GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                                        BindingFlags.DeclaredOnly)
                    .Where(property => property.PropertyType == typeof(object) ||
                                       property.PropertyType.IsGenericType &&
                                       property.PropertyType.GetGenericArguments().Contains(typeof(object)))
                    .Select(property => type.Name + "." + property.Name))
                .ToArray();
            Assert.AreEqual(0, objectProperties.Length,
                "Smart document models must stay strongly typed: " +
                string.Join(", ", objectProperties));

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "WeDoc",
                "WeDocSmartDocumentJson.cs"));
            var declarationCount = source.Split(new[] { '\r', '\n' },
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.TrimStart())
                .Count(line => line.StartsWith("public class ", StringComparison.Ordinal) ||
                               line.StartsWith("public ", StringComparison.Ordinal) &&
                               line.Contains("{ get; set; }", StringComparison.Ordinal));
            Assert.AreEqual(declarationCount, CountOccurrences(source, "/// <summary>"));
            Assert.IsFalse(source.Contains(" dynamic ", StringComparison.Ordinal));
        }

        private static int CountOccurrences(string source, string value)
        {
            var count = 0;
            var startIndex = 0;
            while ((startIndex = source.IndexOf(value, startIndex,
                       StringComparison.Ordinal)) >= 0)
            {
                count++;
                startIndex += value.Length;
            }

            return count;
        }

        private static string FindRepositoryRoot(
            [CallerFilePath] string callerFilePath = null)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(callerFilePath)
                                              ?? AppContext.BaseDirectory);
            while (directory != null &&
                   !Directory.Exists(Path.Combine(directory.FullName, ".git")))
            {
                directory = directory.Parent;
            }

            return directory?.FullName
                   ?? throw new InvalidOperationException("Repository root was not found.");
        }
    }
}
