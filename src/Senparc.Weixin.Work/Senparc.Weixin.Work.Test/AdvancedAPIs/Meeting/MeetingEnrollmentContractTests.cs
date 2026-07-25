using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Meeting;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Meeting
{
    [TestClass]
    public class MeetingEnrollmentContractTests
    {
        [TestMethod]
        public void EnrollmentApiContainsSevenSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(MeetingApi.SetMeetingEnrollmentConfig),
                    typeof(SetMeetingEnrollmentConfigRequest),
                    typeof(SetMeetingEnrollmentConfigResult)),
                (nameof(MeetingApi.GetMeetingEnrollmentConfig),
                    typeof(GetMeetingEnrollmentConfigRequest),
                    typeof(GetMeetingEnrollmentConfigResult)),
                (nameof(MeetingApi.QueryMeetingEnrollmentsByTempOpenIds),
                    typeof(QueryMeetingEnrollmentsByTempOpenIdsRequest),
                    typeof(QueryMeetingEnrollmentsByTempOpenIdsResult)),
                (nameof(MeetingApi.GetMeetingEnrollments), typeof(GetMeetingEnrollmentsRequest),
                    typeof(GetMeetingEnrollmentsResult)),
                (nameof(MeetingApi.ApproveMeetingEnrollments), typeof(ApproveMeetingEnrollmentsRequest),
                    typeof(ApproveMeetingEnrollmentsResult)),
                (nameof(MeetingApi.ImportMeetingEnrollments), typeof(ImportMeetingEnrollmentsRequest),
                    typeof(ImportMeetingEnrollmentsResult)),
                (nameof(MeetingApi.DeleteMeetingEnrollments), typeof(DeleteMeetingEnrollmentsRequest),
                    typeof(DeleteMeetingEnrollmentsResult))
            };

            foreach (var contract in contracts)
            {
                var parameterTypes = new[] { typeof(string), contract.Item2, typeof(int) };
                var syncMethod = typeof(MeetingApi).GetMethod(contract.Item1, parameterTypes);
                var asyncMethod = typeof(MeetingApi).GetMethod(contract.Item1 + "Async", parameterTypes);

                Assert.IsNotNull(syncMethod, contract.Item1);
                Assert.AreEqual(contract.Item3, syncMethod.ReturnType, contract.Item1);
                Assert.IsNotNull(asyncMethod, contract.Item1 + "Async");
                Assert.AreEqual(typeof(Task<>).MakeGenericType(contract.Item3), asyncMethod.ReturnType,
                    contract.Item1 + "Async");
            }
        }

        [TestMethod]
        public void EnrollmentApiUsesOfficialPathsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingApi.Enroll.cs"));
            var paths = new[]
            {
                "/cgi-bin/meeting/enroll/set_config",
                "/cgi-bin/meeting/enroll/get_config",
                "/cgi-bin/meeting/enroll/query_by_tmp_openid",
                "/cgi-bin/meeting/enroll/list",
                "/cgi-bin/meeting/enroll/approve",
                "/cgi-bin/meeting/enroll/import",
                "/cgi-bin/meeting/enroll/delete"
            };
            var documentIds = new[] { "98794", "98810", "98807", "98816", "98817" };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            foreach (var documentId in documentIds)
            {
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + documentId), documentId);
            }

            Assert.AreEqual(4,
                CountOccurrences(source, "固定协议记录的参考文档编号为 98821"));
            Assert.AreEqual(14, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(14, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(14, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(14, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(14, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(7, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(7, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void EnrollmentRequestsPreserveOfficialListShapes()
        {
            var config = new SetMeetingEnrollmentConfigRequest
            {
                meetingid = "meeting-1",
                approve_type = 1,
                is_collect_question = 1,
                no_registration_needed_for_staff = true,
                question_list = new List<MeetingEnrollmentQuestion>
                {
                    new MeetingEnrollmentQuestion
                    {
                        is_required = 1,
                        question_type = 2,
                        special_type = 0,
                        question_title = "所属团队",
                        option_list = new List<MeetingEnrollmentQuestionOption>
                        {
                            new MeetingEnrollmentQuestionOption { content = "SDK" }
                        }
                    }
                }
            };
            using var configDocument = JsonDocument.Parse(JsonSerializer.Serialize(config));
            Assert.AreEqual("meeting-1",
                configDocument.RootElement.GetProperty("meetingid").GetString());
            Assert.AreEqual("SDK", configDocument.RootElement.GetProperty("question_list")[0]
                .GetProperty("option_list")[0].GetProperty("content").GetString());
            Assert.IsTrue(configDocument.RootElement
                .GetProperty("no_registration_needed_for_staff").GetBoolean());

            var query = new QueryMeetingEnrollmentsByTempOpenIdsRequest
            {
                meetingid = "meeting-1",
                sorting_rules = 1,
                tmp_openid_list = new List<string> { "tmp-1", "tmp-2" }
            };
            using var queryDocument = JsonDocument.Parse(JsonSerializer.Serialize(query));
            Assert.AreEqual("meeting-1", queryDocument.RootElement.GetProperty("meetingid").GetString());
            Assert.AreEqual(1, queryDocument.RootElement.GetProperty("sorting_rules").GetInt32());
            Assert.AreEqual("tmp-2", queryDocument.RootElement.GetProperty("tmp_openid_list")[1].GetString());

            var list = new GetMeetingEnrollmentsRequest
            {
                meetingid = "meeting-1",
                status = 2,
                cursor = "cursor-1",
                limit = 100
            };
            using var listDocument = JsonDocument.Parse(JsonSerializer.Serialize(list));
            Assert.AreEqual(2, listDocument.RootElement.GetProperty("status").GetInt32());
            Assert.AreEqual("cursor-1", listDocument.RootElement.GetProperty("cursor").GetString());
            Assert.AreEqual(100, listDocument.RootElement.GetProperty("limit").GetInt32());

            var approve = new ApproveMeetingEnrollmentsRequest
            {
                meetingid = "meeting-1",
                action = 1,
                enroll_id_list = new List<string> { "1001", "1002" }
            };
            using var approveDocument = JsonDocument.Parse(JsonSerializer.Serialize(approve));
            Assert.AreEqual(1, approveDocument.RootElement.GetProperty("action").GetInt32());
            Assert.AreEqual("1002", approveDocument.RootElement.GetProperty("enroll_id_list")[1].GetString());

            var import = new ImportMeetingEnrollmentsRequest
            {
                meetingid = "meeting-1",
                enroll_list = new List<MeetingEnrollmentImportItem>
                {
                    new MeetingEnrollmentImportItem { userid = "zhangsan", nick_name = "张三" },
                    new MeetingEnrollmentImportItem
                    {
                        area = "86", phone_number = "13800000000", nick_name = "外部嘉宾"
                    }
                }
            };
            using var importDocument = JsonDocument.Parse(JsonSerializer.Serialize(import));
            Assert.AreEqual("zhangsan", importDocument.RootElement.GetProperty("enroll_list")[0]
                .GetProperty("userid").GetString());
            Assert.AreEqual("13800000000", importDocument.RootElement.GetProperty("enroll_list")[1]
                .GetProperty("phone_number").GetString());

            var delete = new DeleteMeetingEnrollmentsRequest
            {
                meetingid = "meeting-1",
                enroll_id_list = new List<MeetingEnrollmentDeleteItem>
                {
                    new MeetingEnrollmentDeleteItem { enroll_id = "1001" },
                    new MeetingEnrollmentDeleteItem { enroll_id = "1002" }
                }
            };
            using var deleteDocument = JsonDocument.Parse(JsonSerializer.Serialize(delete));
            Assert.AreEqual(JsonValueKind.Object,
                deleteDocument.RootElement.GetProperty("enroll_id_list")[0].ValueKind);
            Assert.AreEqual("1002", deleteDocument.RootElement.GetProperty("enroll_id_list")[1]
                .GetProperty("enroll_id").GetString());
        }

        [TestMethod]
        public void EnrollmentResultsPreserveMappingsAnswersAndCounts()
        {
            var setConfig = JsonSerializer.Deserialize<SetMeetingEnrollmentConfigResult>(
                "{\"errcode\":0,\"question_count\":12}");
            var getConfig = JsonSerializer.Deserialize<GetMeetingEnrollmentConfigResult>(
                "{\"errcode\":0,\"approve_type\":1,\"is_collect_question\":1," +
                "\"no_registration_needed_for_staff\":true,\"question_list\":[{" +
                "\"is_required\":1,\"question_type\":2,\"special_type\":0," +
                "\"question_title\":\"所属团队\",\"option_list\":[{" +
                "\"content\":\"SDK\"}]}]}" );
            var query = JsonSerializer.Deserialize<QueryMeetingEnrollmentsByTempOpenIdsResult>(
                "{\"errcode\":0,\"enroll_id_list\":[{\"tmp_openid\":\"tmp-1\",\"enroll_id\":\"1001\"}]}");
            var list = JsonSerializer.Deserialize<GetMeetingEnrollmentsResult>(
                "{\"errcode\":0,\"enroll_list\":[{\"enroll_id\":\"1001\",\"userid\":\"zhangsan\"," +
                "\"tmp_openid\":\"tmp-1\",\"enroll_time\":\"2026/07/25 10:00\"," +
                "\"enroll_source_type\":1,\"nick_name\":\"张三\",\"status\":2," +
                "\"enroll_code\":\"CODE-1\",\"answer_list\":[{\"answer_content\":[\"研发部\"]," +
                "\"is_required\":1,\"question_type\":2,\"special_type\":0," +
                "\"question_num\":1,\"question_title\":\"部门\"}]}]," +
                "\"has_more\":true,\"next_cursor\":\"cursor-2\"}");
            var approve = JsonSerializer.Deserialize<ApproveMeetingEnrollmentsResult>(
                "{\"errcode\":0,\"handled_count\":2}");
            var import = JsonSerializer.Deserialize<ImportMeetingEnrollmentsResult>(
                "{\"errcode\":0,\"total_count\":1,\"enroll_list\":[{\"userid\":\"zhangsan\"," +
                "\"nick_name\":\"张三\",\"enroll_id\":\"1001\",\"enroll_code\":\"CODE-1\"}]}");
            var delete = JsonSerializer.Deserialize<DeleteMeetingEnrollmentsResult>(
                "{\"errcode\":0,\"total_count\":2}");

            Assert.IsNotNull(setConfig);
            Assert.AreEqual(12, setConfig.question_count);
            Assert.IsNotNull(getConfig);
            Assert.IsTrue(getConfig.no_registration_needed_for_staff);
            Assert.AreEqual("SDK", getConfig.question_list[0].option_list[0].content);
            Assert.IsNotNull(query);
            Assert.AreEqual("tmp-1", query.enroll_id_list[0].tmp_openid);
            Assert.AreEqual("1001", query.enroll_id_list[0].enroll_id);
            Assert.IsNotNull(list);
            Assert.AreEqual("2026/07/25 10:00", list.enroll_list[0].enroll_time);
            Assert.AreEqual("研发部", list.enroll_list[0].answer_list[0].answer_content[0]);
            Assert.AreEqual("部门", list.enroll_list[0].answer_list[0].question_title);
            Assert.IsTrue(list.has_more);
            Assert.AreEqual("cursor-2", list.next_cursor);
            Assert.IsNotNull(approve);
            Assert.AreEqual(2, approve.handled_count);
            Assert.IsNotNull(import);
            Assert.AreEqual(1, import.total_count);
            Assert.AreEqual("CODE-1", import.enroll_list[0].enroll_code);
            Assert.IsNotNull(delete);
            Assert.AreEqual(2, delete.total_count);
        }

        [TestMethod]
        public void EnrollmentPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(MeetingEnrollmentQuestionOption), typeof(MeetingEnrollmentQuestion),
                typeof(SetMeetingEnrollmentConfigRequest), typeof(SetMeetingEnrollmentConfigResult),
                typeof(GetMeetingEnrollmentConfigRequest), typeof(GetMeetingEnrollmentConfigResult),
                typeof(QueryMeetingEnrollmentsByTempOpenIdsRequest), typeof(MeetingEnrollmentIdMapping),
                typeof(QueryMeetingEnrollmentsByTempOpenIdsResult), typeof(GetMeetingEnrollmentsRequest),
                typeof(MeetingEnrollmentAnswer), typeof(MeetingEnrollment), typeof(GetMeetingEnrollmentsResult),
                typeof(ApproveMeetingEnrollmentsRequest), typeof(ApproveMeetingEnrollmentsResult),
                typeof(MeetingEnrollmentImportItem), typeof(ImportMeetingEnrollmentsRequest),
                typeof(MeetingEnrollmentImportResultItem), typeof(ImportMeetingEnrollmentsResult),
                typeof(MeetingEnrollmentDeleteItem), typeof(DeleteMeetingEnrollmentsRequest),
                typeof(DeleteMeetingEnrollmentsResult)
            };

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
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingEnrollJson.cs"));
            var declarationCount = source.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.TrimStart())
                .Count(line => line.StartsWith("public class ", StringComparison.Ordinal) ||
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
