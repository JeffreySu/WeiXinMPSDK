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
        public void RecordApiContainsTenSyncAndAsyncEntries()
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
                    typeof(DeleteMeetingRecordFileResult)),
                (nameof(MeetingApi.GetMeetingRecordFile), typeof(GetMeetingRecordFileRequest),
                    typeof(GetMeetingRecordFileResult)),
                (nameof(MeetingApi.GetMeetingRecordFileList), typeof(GetMeetingRecordFileListRequest),
                    typeof(GetMeetingRecordFileListResult)),
                (nameof(MeetingApi.GetMeetingRecordTranscriptParagraphList),
                    typeof(GetMeetingRecordTranscriptParagraphListRequest),
                    typeof(GetMeetingRecordTranscriptParagraphListResult)),
                (nameof(MeetingApi.GetMeetingRecordTranscriptDetail),
                    typeof(GetMeetingRecordTranscriptDetailRequest),
                    typeof(GetMeetingRecordTranscriptDetailResult)),
                (nameof(MeetingApi.SearchMeetingRecordTranscript),
                    typeof(SearchMeetingRecordTranscriptRequest),
                    typeof(SearchMeetingRecordTranscriptResult))
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
                "/cgi-bin/meeting/record/delete_file",
                "/cgi-bin/meeting/record/get_file",
                "/cgi-bin/meeting/record/get_file_list",
                "/cgi-bin/meeting/record/transcript/get_paragraph_list",
                "/cgi-bin/meeting/record/transcript/get_detail",
                "/cgi-bin/meeting/record/transcript/search"
            };
            var documentIds = new[]
            {
                98192, 98209, 98208, 98206, 98207,
                98205, 98196, 98212, 98211, 98213
            };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            foreach (var documentId in documentIds)
            {
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + documentId),
                    documentId.ToString());
            }

            Assert.AreEqual(20, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(20, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(20, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(20, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(20, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(10, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(10, CountOccurrences(source, "=> PostAsync<"));
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
        public void CurrentRecordFileModelsPreserveAddressesLargeTimesAndSummaryShapes()
        {
            var request = new GetMeetingRecordFileListRequest
            {
                meeting_record_id = "record-1",
                meetingid = "meeting-1"
            };
            using var requestDocument = JsonDocument.Parse(JsonSerializer.Serialize(request));
            Assert.AreEqual("record-1",
                requestDocument.RootElement.GetProperty("meeting_record_id").GetString());

            var singleFile = Newtonsoft.Json.JsonConvert.DeserializeObject<GetMeetingRecordFileResult>(
                "{\"errcode\":0,\"record_file_id\":\"file-1\"," +
                "\"meetingid\":\"meeting-1\",\"meeting_code\":\"code-1\"," +
                "\"view_address\":\"https://view.test\"," +
                "\"download_address\":\"https://download.test\"," +
                "\"download_address_file_type\":\"mp4\"," +
                "\"audio_address\":\"https://audio.test\"," +
                "\"audio_address_file_type\":\"m4a\"," +
                "\"meeting_summary\":{\"download_address\":\"https://summary.test\"," +
                "\"file_type\":\"pdf\"},\"ai_meeting_transcripts\":[{" +
                "\"download_address\":\"https://transcript.test\",\"file_type\":\"txt\"}]," +
                "\"record_name\":\"录制1\",\"start_time\":\"5000000000\"," +
                "\"end_time\":\"6000000000\",\"meeting_record_name\":\"会议录制\"}");
            Assert.IsNotNull(singleFile);
            Assert.AreEqual(5000000000L, singleFile.start_time);
            Assert.AreEqual(6000000000L, singleFile.end_time);
            Assert.AreEqual("pdf", singleFile.meeting_summary[0].file_type);
            Assert.AreEqual("txt", singleFile.ai_meeting_transcripts[0].file_type);

            var fileList = Newtonsoft.Json.JsonConvert.DeserializeObject<GetMeetingRecordFileListResult>(
                "{\"errcode\":0,\"meeting_record_id\":\"record-1\"," +
                "\"meetingid\":\"meeting-1\",\"record_files\":[{" +
                "\"record_file_id\":\"file-1\",\"meeting_summary\":[{" +
                "\"download_address\":\"https://summary.test\",\"file_type\":\"docx\"}]}]}");
            Assert.IsNotNull(fileList);
            Assert.AreEqual("file-1", fileList.record_files[0].record_file_id);
            Assert.AreEqual("docx", fileList.record_files[0].meeting_summary[0].file_type);
        }

        [TestMethod]
        public void TranscriptModelsPreserveParagraphsWordsSearchAndMillisecondTimes()
        {
            var paragraphList = Newtonsoft.Json.JsonConvert
                .DeserializeObject<GetMeetingRecordTranscriptParagraphListResult>(
                    "{\"errcode\":0,\"audio_detect\":1,\"paragraphs\":[{" +
                    "\"pid\":\"11\",\"start_time\":5000000000," +
                    "\"end_time\":6000000000}]}");
            Assert.IsNotNull(paragraphList);
            Assert.AreEqual(5000000000L, paragraphList.paragraphs[0].start_time);

            var detail = Newtonsoft.Json.JsonConvert
                .DeserializeObject<GetMeetingRecordTranscriptDetailResult>(
                    "{\"errcode\":0,\"has_more\":true,\"transcripts\":{" +
                    "\"paragraphs\":[{\"pid\":\"11\",\"start_time\":5000000000," +
                    "\"end_time\":6000000000,\"speaker_info\":{\"userid\":\"USERID\"}," +
                    "\"sentences\":[{\"sid\":\"2\",\"start_time\":10," +
                    "\"end_time\":20,\"words\":[{\"wid\":\"3\",\"start_time\":11," +
                    "\"end_time\":12,\"text\":\"word\"}]}]}]," +
                    "\"keywords\":[\"nice\"],\"audio_detect\":1}}");
            Assert.IsNotNull(detail);
            Assert.IsTrue(detail.has_more);
            Assert.AreEqual("USERID", detail.transcripts.paragraphs[0].speaker_info.userid);
            Assert.AreEqual("word",
                detail.transcripts.paragraphs[0].sentences[0].words[0].text);

            var search = Newtonsoft.Json.JsonConvert
                .DeserializeObject<SearchMeetingRecordTranscriptResult>(
                    "{\"errcode\":0,\"hits\":[{\"pid\":\"11\",\"sid\":\"2\"," +
                    "\"offset\":10,\"length\":4}],\"timelines\":[{" +
                    "\"pid\":\"11\",\"sid\":\"2\",\"start_time\":5000000000}]}");
            Assert.IsNotNull(search);
            Assert.AreEqual(4, search.hits[0].length);
            Assert.AreEqual(5000000000L, search.timelines[0].start_time);
        }

        [TestMethod]
        public void CurrentRecordPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(GetMeetingRecordFileRequest), typeof(MeetingRecordDownloadFile),
                typeof(GetMeetingRecordFileResult), typeof(GetMeetingRecordFileListRequest),
                typeof(MeetingRecordPlaybackFile), typeof(GetMeetingRecordFileListResult),
                typeof(GetMeetingRecordTranscriptParagraphListRequest),
                typeof(MeetingRecordTranscriptParagraphSummary),
                typeof(GetMeetingRecordTranscriptParagraphListResult),
                typeof(GetMeetingRecordTranscriptDetailRequest),
                typeof(MeetingRecordTranscriptSpeaker), typeof(MeetingRecordTranscriptWord),
                typeof(MeetingRecordTranscriptSentence), typeof(MeetingRecordTranscriptParagraph),
                typeof(MeetingRecordTranscriptDetail),
                typeof(GetMeetingRecordTranscriptDetailResult),
                typeof(SearchMeetingRecordTranscriptRequest),
                typeof(MeetingRecordTranscriptSearchHit),
                typeof(MeetingRecordTranscriptTimeline),
                typeof(SearchMeetingRecordTranscriptResult)
            };

            foreach (var property in modelTypes.SelectMany(type => type.GetProperties(
                         BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)))
            {
                Assert.AreNotEqual(typeof(object), property.PropertyType,
                    property.DeclaringType?.Name + "." + property.Name);
                if (property.PropertyType.IsGenericType)
                {
                    CollectionAssert.DoesNotContain(property.PropertyType.GetGenericArguments(),
                        typeof(object));
                }
            }

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting",
                "MeetingRecordCurrentJson.cs"));
            var declarationCount = source.Split(new[] { '\r', '\n' },
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.TrimStart())
                .Count(line => line.StartsWith("public class ", StringComparison.Ordinal) ||
                               line.StartsWith("public ", StringComparison.Ordinal) &&
                               line.Contains("{ get; set; }", StringComparison.Ordinal));
            Assert.AreEqual(declarationCount, CountOccurrences(source, "/// <summary>"));
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
