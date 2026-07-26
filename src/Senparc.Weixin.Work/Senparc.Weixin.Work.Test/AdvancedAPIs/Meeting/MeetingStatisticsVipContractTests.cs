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
    public class MeetingStatisticsVipContractTests
    {
        [TestMethod]
        public void StatisticsAndVipApisContainSixSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(MeetingApi.GetMeetingStartStatistics), typeof(GetMeetingStartStatisticsRequest),
                    typeof(GetMeetingStartStatisticsResult)),
                (nameof(MeetingApi.SubmitMeetingVipBatchAddJob),
                    typeof(SubmitMeetingVipBatchAddJobRequest),
                    typeof(SubmitMeetingVipBatchAddJobResult)),
                (nameof(MeetingApi.GetMeetingVipBatchAddJobResult),
                    typeof(GetMeetingVipBatchAddJobResultRequest),
                    typeof(GetMeetingVipBatchAddJobResultResult)),
                (nameof(MeetingApi.SubmitMeetingVipBatchDeleteJob),
                    typeof(SubmitMeetingVipBatchDeleteJobRequest),
                    typeof(SubmitMeetingVipBatchDeleteJobResult)),
                (nameof(MeetingApi.GetMeetingVipBatchDeleteJobResult),
                    typeof(GetMeetingVipBatchDeleteJobResultRequest),
                    typeof(GetMeetingVipBatchDeleteJobResultResult)),
                (nameof(MeetingApi.GetMeetingVipList), typeof(GetMeetingVipListRequest),
                    typeof(GetMeetingVipListResult))
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
        public void StatisticsAndVipApisUseOfficialPathsDocumentsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingApi.StatisticsVip.cs"));
            var paths = new[]
            {
                "/cgi-bin/meeting/statistics/get_start_list",
                "/cgi-bin/meeting/vip/submit_batch_add_job",
                "/cgi-bin/meeting/vip/batch_add_job_result",
                "/cgi-bin/meeting/vip/submit_batch_del_job",
                "/cgi-bin/meeting/vip/batch_del_job_result",
                "/cgi-bin/meeting/vip/list"
            };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            Assert.AreEqual(2, CountOccurrences(source, "/document/path/99651"));
            Assert.AreEqual(4, CountOccurrences(source, "/document/path/99508"));
            Assert.AreEqual(4, CountOccurrences(source, "/document/path/99509"));
            Assert.AreEqual(2, CountOccurrences(source, "/document/path/99510"));
            Assert.AreEqual(12, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(12, CountOccurrences(source,
                "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(12, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(12, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(12, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(6, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(6, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void StartStatisticsPreservePagingAnd64BitTimestamps()
        {
            var request = new GetMeetingStartStatisticsRequest
            {
                type = 2,
                begin_time = 5000000000L,
                end_time = 5000000100L,
                cursor = "cursor-1",
                limit = 100
            };
            var result = JsonSerializer.Deserialize<GetMeetingStartStatisticsResult>(
                "{\"errcode\":0,\"meeting_list\":[{\"userid\":\"user-1\"," +
                "\"start_time\":5000000000}],\"next_cursor\":\"cursor-2\"}");

            using var document = JsonDocument.Parse(JsonSerializer.Serialize(request));
            Assert.AreEqual(2, document.RootElement.GetProperty("type").GetInt32());
            Assert.AreEqual(5000000000L, document.RootElement.GetProperty("begin_time").GetInt64());
            Assert.AreEqual(5000000100L, document.RootElement.GetProperty("end_time").GetInt64());
            Assert.AreEqual("cursor-1", document.RootElement.GetProperty("cursor").GetString());
            Assert.AreEqual(100, document.RootElement.GetProperty("limit").GetInt32());

            Assert.IsNotNull(result);
            Assert.AreEqual("user-1", result.meeting_list.Single().userid);
            Assert.AreEqual(5000000000L, result.meeting_list.Single().start_time);
            Assert.AreEqual("cursor-2", result.next_cursor);
        }

        [TestMethod]
        public void VipSubmitAndJobResultsPreserveMemberLists()
        {
            var addRequest = new SubmitMeetingVipBatchAddJobRequest
            {
                userid_list = new List<string> { "user-1", "user-2" }
            };
            var deleteRequest = new SubmitMeetingVipBatchDeleteJobRequest
            {
                userid_list = new List<string> { "user-3" }
            };
            var submitResult = JsonSerializer.Deserialize<SubmitMeetingVipBatchAddJobResult>(
                "{\"errcode\":0,\"jobid\":\"job-1\",\"invalid_userid_list\":[\"bad-user\"]}");
            var jobResult = JsonSerializer.Deserialize<GetMeetingVipBatchDeleteJobResultResult>(
                "{\"errcode\":0,\"job_result\":{\"succ_userid_list\":[\"user-3\"]," +
                "\"fail_userid_list\":[\"user-4\"]}}");

            using var addDocument = JsonDocument.Parse(JsonSerializer.Serialize(addRequest));
            using var deleteDocument = JsonDocument.Parse(JsonSerializer.Serialize(deleteRequest));
            Assert.AreEqual(2, addDocument.RootElement.GetProperty("userid_list").GetArrayLength());
            Assert.AreEqual("user-3", deleteDocument.RootElement.GetProperty("userid_list")[0]
                .GetString());
            Assert.IsNotNull(submitResult);
            Assert.AreEqual("job-1", submitResult.jobid);
            Assert.AreEqual("bad-user", submitResult.invalid_userid_list.Single());
            Assert.IsNotNull(jobResult);
            Assert.AreEqual("user-3", jobResult.job_result.succ_userid_list.Single());
            Assert.AreEqual("user-4", jobResult.job_result.fail_userid_list.Single());
        }

        [TestMethod]
        public void VipListPreservesBooleanMoreFlagAndCursor()
        {
            var result = JsonSerializer.Deserialize<GetMeetingVipListResult>(
                "{\"errcode\":0,\"userid_list\":[\"user-1\",\"user-2\"]," +
                "\"has_more\":true,\"next_cursor\":\"cursor-2\"}");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.userid_list.Count);
            Assert.IsTrue(result.has_more);
            Assert.AreEqual("cursor-2", result.next_cursor);
        }

        [TestMethod]
        public void StatisticsAndVipPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(GetMeetingStartStatisticsRequest), typeof(MeetingStartStatisticsItem),
                typeof(GetMeetingStartStatisticsResult), typeof(SubmitMeetingVipBatchAddJobRequest),
                typeof(SubmitMeetingVipBatchAddJobResult),
                typeof(GetMeetingVipBatchAddJobResultRequest), typeof(MeetingVipBatchJobResult),
                typeof(GetMeetingVipBatchAddJobResultResult),
                typeof(SubmitMeetingVipBatchDeleteJobRequest),
                typeof(SubmitMeetingVipBatchDeleteJobResult),
                typeof(GetMeetingVipBatchDeleteJobResultRequest),
                typeof(GetMeetingVipBatchDeleteJobResultResult), typeof(GetMeetingVipListRequest),
                typeof(GetMeetingVipListResult)
            };

            foreach (var property in modelTypes.SelectMany(type => type.GetProperties(BindingFlags.Public |
                         BindingFlags.Instance | BindingFlags.DeclaredOnly)))
            {
                Assert.AreNotEqual(typeof(object), property.PropertyType,
                    property.DeclaringType?.Name + "." + property.Name);
            }

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingStatisticsVipJson.cs"));
            var declarationCount = source.Split(new[] { '\r', '\n' },
                    StringSplitOptions.RemoveEmptyEntries)
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
