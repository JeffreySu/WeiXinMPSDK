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
    public class MeetingRecordContractTests
    {
        [TestMethod]
        public void RecordApiContainsFiveSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(MeetingApi.GetMeetingRecordList), typeof(GetMeetingRecordListRequest),
                    typeof(GetMeetingRecordListResult)),
                (nameof(MeetingApi.GetMeetingRecordStatistics), typeof(GetMeetingRecordStatisticsRequest),
                    typeof(GetMeetingRecordStatisticsResult)),
                (nameof(MeetingApi.UpdateMeetingRecordSharingConfig),
                    typeof(UpdateMeetingRecordSharingConfigRequest),
                    typeof(UpdateMeetingRecordSharingConfigResult)),
                (nameof(MeetingApi.DeleteMeetingRecord), typeof(DeleteMeetingRecordRequest),
                    typeof(DeleteMeetingRecordResult)),
                (nameof(MeetingApi.DeleteMeetingRecordFile), typeof(DeleteMeetingRecordFileRequest),
                    typeof(DeleteMeetingRecordFileResult))
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
        public void RecordApiUsesOfficialPathsDocumentsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingApi.Record.cs"));
            var paths = new[]
            {
                "/cgi-bin/meeting/record/list",
                "/cgi-bin/meeting/record/get_statistics",
                "/cgi-bin/meeting/record/update_sharing_config",
                "/cgi-bin/meeting/record/delete",
                "/cgi-bin/meeting/record/delete_file"
            };
            var documentIds = new[] { 98192, 98209, 98208, 98206, 98207 };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            foreach (var documentId in documentIds)
            {
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + documentId),
                    documentId.ToString());
            }

            Assert.AreEqual(10, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(10, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(10, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(10, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(10, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(5, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(5, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void RecordListPreservesFiltersFilesSharingAndLargeValues()
        {
            var request = new GetMeetingRecordListRequest
            {
                meetingid = "meeting-1",
                meeting_code = "code-1",
                userid = "zhangsan",
                start_time = 16666666L,
                end_time = 178888888L,
                cursor = "cursor-1",
                limit = 10
            };
            using var requestDocument = JsonDocument.Parse(JsonSerializer.Serialize(request));
            Assert.AreEqual("meeting-1", requestDocument.RootElement.GetProperty("meetingid").GetString());
            Assert.AreEqual("code-1", requestDocument.RootElement.GetProperty("meeting_code").GetString());
            Assert.AreEqual("zhangsan", requestDocument.RootElement.GetProperty("userid").GetString());
            Assert.AreEqual(178888888L, requestDocument.RootElement.GetProperty("end_time").GetInt64());
            Assert.AreEqual("cursor-1", requestDocument.RootElement.GetProperty("cursor").GetString());

            var result = JsonSerializer.Deserialize<GetMeetingRecordListResult>(
                "{\"errcode\":0,\"record_meetings\":[{\"meeting_record_id\":\"record-1\"," +
                "\"meetingid\":\"meeting-1\",\"meeting_code\":\"code-1\"," +
                "\"host_user_id\":\"zhangsan\",\"title\":\"周会\"," +
                "\"meeting_start_time\":1788888888,\"state\":1,\"record_files\":[{" +
                "\"record_file_id\":\"file-1\",\"record_start_time\":1788888888," +
                "\"record_end_time\":1788889999,\"record_size\":5000000000," +
                "\"sharing_state\":1,\"sharing_url\":\"https://example.test/share\"," +
                "\"required_same_corp\":true,\"required_attendee\":true," +
                "\"password\":\"123456\",\"sharing_expire\":1999999999," +
                "\"allow_download\":true}]}],\"has_more\":true,\"next_cursor\":\"next-1\"}");

            Assert.IsNotNull(result);
            Assert.AreEqual("record-1", result.record_meetings[0].meeting_record_id);
            Assert.AreEqual("zhangsan", result.record_meetings[0].host_user_id);
            Assert.AreEqual(1788888888L, result.record_meetings[0].meeting_start_time);
            Assert.AreEqual(5000000000L, result.record_meetings[0].record_files[0].record_size);
            Assert.AreEqual(true, result.record_meetings[0].record_files[0].required_same_corp);
            Assert.AreEqual(true, result.record_meetings[0].record_files[0].allow_download);
            Assert.IsTrue(result.has_more);
            Assert.AreEqual("next-1", result.next_cursor);
        }

        [TestMethod]
        public void RecordStatisticsSharingAndDeleteRequestsPreserveOfficialShapes()
        {
            var statistics = new GetMeetingRecordStatisticsRequest
            {
                meetingid = "meeting-1", meeting_record_id = "record-1",
                start_time = 16666666L, end_time = 1788888888L
            };
            var sharing = new UpdateMeetingRecordSharingConfigRequest
            {
                meetingid = "meeting-1",
                meeting_record_id = "record-1",
                sharing_config = new MeetingRecordSharingConfig
                {
                    enable_sharing = true,
                    sharing_auth_type = 0,
                    enable_password = true,
                    password = "123456",
                    enable_sharing_expire = true,
                    sharing_expire = 1999999999L,
                    allow_download = true
                }
            };
            var deleteRecord = new DeleteMeetingRecordRequest
            {
                meetingid = "meeting-1", meeting_record_id = "record-1"
            };
            var deleteFile = new DeleteMeetingRecordFileRequest
            {
                meetingid = "meeting-1", record_file_id = "file-1"
            };

            using var statisticsDocument = JsonDocument.Parse(JsonSerializer.Serialize(statistics));
            using var sharingDocument = JsonDocument.Parse(JsonSerializer.Serialize(sharing));
            using var deleteRecordDocument = JsonDocument.Parse(JsonSerializer.Serialize(deleteRecord));
            using var deleteFileDocument = JsonDocument.Parse(JsonSerializer.Serialize(deleteFile));
            var statisticsResult = JsonSerializer.Deserialize<GetMeetingRecordStatisticsResult>(
                "{\"errcode\":0,\"summaries\":[{\"date\":\"2033-01-01\"," +
                "\"view_count\":12,\"download_count\":5}]}");

            Assert.AreEqual("record-1",
                statisticsDocument.RootElement.GetProperty("meeting_record_id").GetString());
            Assert.AreEqual(1788888888L,
                statisticsDocument.RootElement.GetProperty("end_time").GetInt64());
            var sharingConfig = sharingDocument.RootElement.GetProperty("sharing_config");
            Assert.AreEqual(JsonValueKind.True, sharingConfig.GetProperty("enable_sharing").ValueKind);
            Assert.AreEqual(0, sharingConfig.GetProperty("sharing_auth_type").GetInt32());
            Assert.AreEqual("123456", sharingConfig.GetProperty("password").GetString());
            Assert.AreEqual(1999999999L, sharingConfig.GetProperty("sharing_expire").GetInt64());
            Assert.AreEqual("record-1",
                deleteRecordDocument.RootElement.GetProperty("meeting_record_id").GetString());
            Assert.AreEqual("file-1",
                deleteFileDocument.RootElement.GetProperty("record_file_id").GetString());
            Assert.IsNotNull(statisticsResult);
            Assert.AreEqual("2033-01-01", statisticsResult.summaries[0].date);
            Assert.AreEqual(12, statisticsResult.summaries[0].view_count);
            Assert.AreEqual(5, statisticsResult.summaries[0].download_count);
        }

        [TestMethod]
        public void RecordPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(GetMeetingRecordListRequest), typeof(MeetingRecordFile),
                typeof(MeetingRecordInfo), typeof(GetMeetingRecordListResult),
                typeof(GetMeetingRecordStatisticsRequest), typeof(MeetingRecordStatisticsSummary),
                typeof(GetMeetingRecordStatisticsResult), typeof(MeetingRecordSharingConfig),
                typeof(UpdateMeetingRecordSharingConfigRequest),
                typeof(UpdateMeetingRecordSharingConfigResult), typeof(DeleteMeetingRecordRequest),
                typeof(DeleteMeetingRecordResult), typeof(DeleteMeetingRecordFileRequest),
                typeof(DeleteMeetingRecordFileResult)
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
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingRecordJson.cs"));
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
