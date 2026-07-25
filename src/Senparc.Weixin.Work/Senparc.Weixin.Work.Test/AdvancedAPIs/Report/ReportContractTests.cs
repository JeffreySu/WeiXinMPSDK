using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Report;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Report
{
    [TestClass]
    public class ReportContractTests
    {
        [TestMethod]
        public void ReportApiContainsFiveSyncAndAsyncEntries()
        {
            var methodNames = typeof(ReportApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var syncMethodName in new[]
            {
                nameof(ReportApi.GetRecordList),
                nameof(ReportApi.GetRecordDetail),
                nameof(ReportApi.GetStatList),
                nameof(ReportApi.ExportDocument),
                nameof(ReportApi.GetExportDocumentResult)
            })
            {
                CollectionAssert.Contains(methodNames, syncMethodName, syncMethodName);
                CollectionAssert.Contains(methodNames, syncMethodName + "Async", syncMethodName + "Async");
            }
        }

        [TestMethod]
        public void ReportApiUsesOfficialPostPaths()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "Report", "ReportApi.cs"));
            var exportSource = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "Report",
                "ReportApi.Export.cs"));

            StringAssert.Contains(source, "/cgi-bin/oa/journal/get_record_list");
            StringAssert.Contains(source, "/cgi-bin/oa/journal/get_record_detail");
            StringAssert.Contains(source, "/cgi-bin/oa/journal/get_stat_list");
            StringAssert.Contains(exportSource, "/cgi-bin/oa/journal/export_doc");
            StringAssert.Contains(exportSource, "/cgi-bin/oa/journal/get_export_doc_result");
            Assert.AreEqual(2, CountOccurrences(source, "CommonJsonSendType.POST"));
            Assert.AreEqual(4, CountOccurrences(exportSource, "/document/path/96108"));
        }

        [TestMethod]
        public void ExportDocumentUsesOfficialFieldsAndStrongResults()
        {
            var exportJson = JsonSerializer.Serialize(new ExportReportDocumentRequest
            {
                journaluuid = "journal-1",
                docid = "doc-1"
            });
            var queryJson = JsonSerializer.Serialize(new GetReportExportDocumentResultRequest
            {
                jobid = "job-1"
            });
            var exportResult = JsonSerializer.Deserialize<ExportReportDocumentResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"jobid\":\"job-1\"}");
            var queryResult = JsonSerializer.Deserialize<GetReportExportDocumentResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"status\":2,\"url\":\"https://example.test/report.docx\"}");
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "Report",
                "ReportExportJson.cs"));

            Assert.AreEqual("{\"journaluuid\":\"journal-1\",\"docid\":\"doc-1\"}", exportJson);
            Assert.AreEqual("{\"jobid\":\"job-1\"}", queryJson);
            Assert.IsNotNull(exportResult);
            Assert.AreEqual("job-1", exportResult.jobid);
            Assert.IsNotNull(queryResult);
            Assert.AreEqual(2, queryResult.status);
            Assert.AreEqual("https://example.test/report.docx", queryResult.url);
            Assert.AreEqual(10, CountOccurrences(source, "/// <summary>"));
            Assert.IsFalse(new[]
            {
                typeof(ExportReportDocumentRequest), typeof(ExportReportDocumentResult),
                typeof(GetReportExportDocumentResultRequest), typeof(GetReportExportDocumentResult)
            }.SelectMany(type => type.GetProperties(BindingFlags.Public | BindingFlags.Instance |
                BindingFlags.DeclaredOnly)).Any(property => property.PropertyType == typeof(object)));
        }

        [TestMethod]
        public void RecordListUsesOfficialFieldsAndUnsignedPagingValues()
        {
            var requestJson = JsonSerializer.Serialize(new GetReportRecordListRequest
            {
                starttime = 4294967000,
                endtime = uint.MaxValue,
                cursor = 4000000000,
                limit = 100,
                filters = new List<ReportRecordFilter>
                {
                    new ReportRecordFilter { key = "template_id", value = "template-1" }
                }
            });
            var result = JsonSerializer.Deserialize<GetReportRecordListResult>(
                "{\"errcode\":0,\"journaluuid_list\":[\"journal-1\"]," +
                "\"next_cursor\":4294967295,\"endflag\":0}");

            StringAssert.Contains(requestJson, "\"starttime\":4294967000");
            StringAssert.Contains(requestJson, "\"cursor\":4000000000");
            StringAssert.Contains(requestJson, "\"key\":\"template_id\"");
            Assert.IsNotNull(result);
            Assert.AreEqual(uint.MaxValue, result.next_cursor);
            Assert.AreEqual("journal-1", result.journaluuid_list[0]);
        }

        [TestMethod]
        public void RecordDetailPreservesLargeIdsAndRecursiveTableContent()
        {
            const string json = """
                {
                  "errcode": 0,
                  "info": {
                    "journal_uuid": "journal-1",
                    "template_name": "日报",
                    "template_id": "template-1",
                    "report_time": 4294967296,
                    "submitter": { "userid": "zhangsan" },
                    "receivers": [{ "userid": "lisi" }],
                    "readed_receivers": [{ "userid": "lisi" }],
                    "apply_data": {
                      "contents": [{
                        "control": "Table",
                        "id": "table-1",
                        "title": [{ "text": "明细" }],
                        "value": {
                          "children": [{
                            "list": [{
                              "control": "Text",
                              "id": "text-1",
                              "title": [{ "text": "内容" }],
                              "value": { "text": "已完成" }
                            }]
                          }]
                        }
                      }]
                    },
                    "sys_journal_data": "<p>日报</p>",
                    "comments": [{
                      "commentid": 18446744073709551615,
                      "tocommentid": 9223372036854775808,
                      "comment_userinfo": { "userid": "wangwu" },
                      "content": "加油",
                      "comment_time": 4294967297
                    }]
                  }
                }
                """;

            var result = JsonSerializer.Deserialize<GetReportRecordDetailResult>(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(4294967296UL, result.info.report_time);
            Assert.AreEqual(ulong.MaxValue, result.info.comments[0].commentid);
            Assert.AreEqual(9223372036854775808UL, result.info.comments[0].tocommentid);
            Assert.AreEqual("已完成",
                result.info.apply_data.contents[0].value.children[0].list[0].value.text);
        }

        [TestMethod]
        public void AllSeventeenOfficialControlShapesAreStronglyTyped()
        {
            const string json = """
                {
                  "errcode": 0,
                  "info": {
                    "apply_data": {
                      "contents": [
                        { "control": "Text", "value": { "text": "单行" } },
                        { "control": "Textarea", "value": { "text": "多行" } },
                        { "control": "Number", "value": { "new_number": "12.50" } },
                        { "control": "Money", "value": { "new_money": "88.00" } },
                        { "control": "Date", "value": { "date": { "type": "day", "s_timestamp": "4294967296" } } },
                        { "control": "Selector", "value": { "selector": { "type": "multi", "options": [{ "key": "1", "value": [{ "text": "选项" }] }] } } },
                        { "control": "Contact", "value": { "members": [{ "userid": "user-1" }] } },
                        { "control": "Contact", "value": { "departments": [{ "openapi_id": "party-1" }] } },
                        { "control": "Tips", "value": {} },
                        { "control": "File", "value": { "files": [{ "file_id": "media-1" }] } },
                        { "control": "Table", "value": { "children": [{ "list": [{ "control": "Text", "value": { "text": "子项" } }] }] } },
                        { "control": "DateRange", "value": { "date_range": { "type": "halfday", "new_begin": 4294967296, "new_end": 4294967396, "new_duration": 100 } } },
                        { "control": "Location", "value": { "location": { "latitude": "30.547239", "longitude": "104.063291", "title": "成都", "address": "天府三街", "time": 4294967297 } } },
                        { "control": "Formula", "value": { "formula": { "value": "5.0" } } },
                        { "control": "SchoolContact", "value": { "students": [{ "name": "Jackie" }] } },
                        { "control": "SchoolContact", "value": { "classes": [{ "name": "1班" }] } },
                        { "control": "Doc", "value": { "docs": [{ "docid": "doc-1", "doc_url": "https://doc.weixin.qq.com/doc/doc-1" }] } },
                        { "control": "WedriveFile", "value": { "wedrive_files": [{ "fileid": "file-1" }] } }
                      ]
                    }
                  }
                }
                """;

            var values = JsonSerializer.Deserialize<GetReportRecordDetailResult>(json)
                .info.apply_data.contents.Select(content => content.value).ToArray();

            Assert.AreEqual("单行", values[0].text);
            Assert.AreEqual("多行", values[1].text);
            Assert.AreEqual("12.50", values[2].new_number);
            Assert.AreEqual("88.00", values[3].new_money);
            Assert.AreEqual("4294967296", values[4].date.s_timestamp);
            Assert.AreEqual("选项", values[5].selector.options[0].value[0].text);
            Assert.AreEqual("user-1", values[6].members[0].userid);
            Assert.AreEqual("party-1", values[7].departments[0].openapi_id);
            Assert.IsNotNull(values[8]);
            Assert.AreEqual("media-1", values[9].files[0].file_id);
            Assert.AreEqual("子项", values[10].children[0].list[0].value.text);
            Assert.AreEqual(4294967296UL, values[11].date_range.new_begin);
            Assert.AreEqual("30.547239", values[12].location.latitude);
            Assert.AreEqual("5.0", values[13].formula.value);
            Assert.AreEqual("Jackie", values[14].students[0].name);
            Assert.AreEqual("1班", values[15].classes[0].name);
            Assert.AreEqual("doc-1", values[16].docs[0].docid);
            Assert.AreEqual("file-1", values[17].wedrive_files[0].fileid);
            Assert.IsFalse(typeof(ReportControlValue).GetProperties()
                .Any(property => property.PropertyType == typeof(object)));
        }

        [TestMethod]
        public void StatisticsPreserveRangesAndUnsigned64BitTimes()
        {
            const string json = """
                {
                  "errcode": 0,
                  "stat_list": [{
                    "template_id": "template-1",
                    "template_name": "日报",
                    "report_range": {
                      "user_list": [{ "userid": "user-1" }],
                      "party_list": [{ "open_partyid": "party-1" }],
                      "tag_list": [{ "open_tagid": "tag-1" }]
                    },
                    "white_range": {
                      "user_list": [{ "userid": "user-2" }]
                    },
                    "receivers": {
                      "user_list": [{ "userid": "leader-1" }],
                      "tag_list": [{ "open_tagid": "tag-2" }],
                      "leader_list": [{ "level": 18446744073709551615 }]
                    },
                    "cycle_begin_time": 4294967296,
                    "cycle_end_time": 4294967297,
                    "stat_begin_time": 4294967298,
                    "stat_end_time": 18446744073709551615,
                    "report_list": [{
                      "user": { "userid": "user-1" },
                      "itemlist": [{ "journaluuid": "journal-1", "reporttime": 4294967299, "flag": 1 }]
                    }],
                    "unreport_list": [{ "user": { "userid": "user-3" }, "itemlist": [] }],
                    "report_type": 2
                  }]
                }
                """;

            var result = JsonSerializer.Deserialize<GetReportStatListResult>(json);
            var stat = result.stat_list[0];

            Assert.AreEqual("party-1", stat.report_range.party_list[0].open_partyid);
            Assert.AreEqual("tag-1", stat.report_range.tag_list[0].open_tagid);
            Assert.AreEqual(ulong.MaxValue, stat.receivers.leader_list[0].level);
            Assert.AreEqual(ulong.MaxValue, stat.stat_end_time);
            Assert.AreEqual(4294967299UL, stat.report_list[0].itemlist[0].reporttime);
            Assert.AreEqual("user-3", stat.unreport_list[0].user.userid);
        }

        private static int CountOccurrences(string source, string value)
            => source.Split(new[] { value }, StringSplitOptions.None).Length - 1;

        private static string FindRepositoryRoot([CallerFilePath] string sourceFilePath = null)
        {
            foreach (var startPath in new[]
            {
                Directory.GetCurrentDirectory(),
                AppContext.BaseDirectory,
                Path.GetDirectoryName(sourceFilePath)
            })
            {
                var directory = string.IsNullOrEmpty(startPath) ? null : new DirectoryInfo(startPath);
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
