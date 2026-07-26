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
    public class MeetingPollContractTests
    {
        [TestMethod]
        public void PollApiContainsEightSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(MeetingApi.CreateMeetingPollTheme), typeof(CreateMeetingPollThemeRequest),
                    typeof(CreateMeetingPollThemeResult)),
                (nameof(MeetingApi.UpdateMeetingPollTheme), typeof(UpdateMeetingPollThemeRequest),
                    typeof(UpdateMeetingPollThemeResult)),
                (nameof(MeetingApi.GetMeetingPollList), typeof(GetMeetingPollListRequest),
                    typeof(GetMeetingPollListResult)),
                (nameof(MeetingApi.GetMeetingPollThemeInfo), typeof(GetMeetingPollThemeInfoRequest),
                    typeof(GetMeetingPollThemeInfoResult)),
                (nameof(MeetingApi.GetMeetingPollDetail), typeof(GetMeetingPollDetailRequest),
                    typeof(GetMeetingPollDetailResult)),
                (nameof(MeetingApi.DeleteMeetingPoll), typeof(DeleteMeetingPollRequest),
                    typeof(DeleteMeetingPollResult)),
                (nameof(MeetingApi.StartMeetingPoll), typeof(StartMeetingPollRequest),
                    typeof(StartMeetingPollResult)),
                (nameof(MeetingApi.FinishMeetingPoll), typeof(FinishMeetingPollRequest),
                    typeof(FinishMeetingPollResult))
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
        public void PollApiUsesOfficialPathsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingApi.Poll.cs"));
            var paths = new[]
            {
                "/cgi-bin/meeting/poll/create_theme",
                "/cgi-bin/meeting/poll/update_theme",
                "/cgi-bin/meeting/poll/get_poll_list",
                "/cgi-bin/meeting/poll/get_theme_info",
                "/cgi-bin/meeting/poll/get_poll_detail",
                "/cgi-bin/meeting/poll/delete",
                "/cgi-bin/meeting/poll/start",
                "/cgi-bin/meeting/poll/finish"
            };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            for (var documentId = 98834; documentId <= 98841; documentId++)
            {
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + documentId),
                    documentId.ToString());
            }

            Assert.AreEqual(16, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(16, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(16, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(16, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(16, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(8, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(8, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void PollRequestsPreserveTopicQuestionsOperatorAndLifecycleIds()
        {
            var create = new CreateMeetingPollThemeRequest
            {
                meetingid = "meeting-1",
                operator_userid = "zhangsan",
                instance_id = 1,
                poll_topic = "季度满意度",
                poll_desc = "请选择",
                is_anony = 1,
                poll_questions = new List<MeetingPollQuestion>
                {
                    new MeetingPollQuestion
                    {
                        question_type = 1,
                        question_desc = "是否满意",
                        poll_option = new List<string> { "满意", "不满意" }
                    }
                }
            };
            using var createDocument = JsonDocument.Parse(JsonSerializer.Serialize(create));
            Assert.AreEqual("meeting-1", createDocument.RootElement.GetProperty("meetingid").GetString());
            Assert.AreEqual("zhangsan",
                createDocument.RootElement.GetProperty("operator_userid").GetString());
            Assert.AreEqual(1, createDocument.RootElement.GetProperty("instance_id").GetInt32());
            Assert.AreEqual(JsonValueKind.Number, createDocument.RootElement.GetProperty("is_anony").ValueKind);
            Assert.AreEqual("不满意", createDocument.RootElement.GetProperty("poll_questions")[0]
                .GetProperty("poll_option")[1].GetString());

            var update = new UpdateMeetingPollThemeRequest
            {
                meetingid = "meeting-1",
                operator_userid = "zhangsan",
                instance_id = 1,
                poll_theme_id = "theme-1",
                poll_topic = "更新后的主题",
                poll_questions = create.poll_questions
            };
            using var updateDocument = JsonDocument.Parse(JsonSerializer.Serialize(update));
            Assert.AreEqual("theme-1", updateDocument.RootElement.GetProperty("poll_theme_id").GetString());

            var delete = new DeleteMeetingPollRequest
            {
                meetingid = "meeting-1", operator_userid = "zhangsan", instance_id = 1,
                poll_id = "poll-1"
            };
            using var deleteDocument = JsonDocument.Parse(JsonSerializer.Serialize(delete));
            Assert.AreEqual("poll-1", deleteDocument.RootElement.GetProperty("poll_id").GetString());

            var finish = new FinishMeetingPollRequest
            {
                meetingid = "meeting-1", operator_userid = "zhangsan", instance_id = 1,
                poll_theme_id = "theme-1", poll_id = "poll-1"
            };
            using var finishDocument = JsonDocument.Parse(JsonSerializer.Serialize(finish));
            Assert.AreEqual("theme-1", finishDocument.RootElement.GetProperty("poll_theme_id").GetString());
            Assert.AreEqual("poll-1", finishDocument.RootElement.GetProperty("poll_id").GetString());
        }

        [TestMethod]
        public void PollResultsPreserveGroupingOptionsVotersAndNumericIds()
        {
            var list = JsonSerializer.Deserialize<GetMeetingPollListResult>(
                "{\"errcode\":0,\"polls_theme_info\":[{\"poll_theme_id\":\"theme-1\"," +
                "\"polls_info\":[{\"poll_id\":\"poll-1\",\"poll_topic\":\"满意度\"," +
                "\"status\":1,\"is_shared\":1,\"is_anony\":0}]}]}");
            var theme = JsonSerializer.Deserialize<GetMeetingPollThemeInfoResult>(
                "{\"errcode\":0,\"poll_theme_id\":\"theme-1\",\"poll_topic\":\"满意度\"," +
                "\"poll_desc\":\"请选择\",\"is_anony\":1,\"poll_question_data\":[{" +
                "\"question_type\":1,\"question_desc\":\"是否满意\",\"option_info\":[{" +
                "\"option_desc\":\"满意\"}]}]}");
            var detail = JsonSerializer.Deserialize<GetMeetingPollDetailResult>(
                "{\"errcode\":0,\"poll_theme_id\":\"theme-1\",\"poll_topic\":\"满意度\"," +
                "\"poll_desc\":\"请选择\",\"status\":1,\"is_shared\":0,\"is_anony\":1," +
                "\"vote_total_num\":10,\"poll_question_data\":[{\"question_id\":101," +
                "\"question_type\":1,\"question_desc\":\"是否满意\",\"option_info\":[{" +
                "\"option_id\":1111,\"option_desc\":\"满意\",\"option_num\":8,\"rate\":80," +
                "\"option_user\":[{\"userid\":\"zhangsan\",\"tmp_openid\":\"tmp-1\"}]}]}]}");
            var start = JsonSerializer.Deserialize<StartMeetingPollResult>(
                "{\"errcode\":0,\"poll_id\":\"poll-1\"}");

            Assert.IsNotNull(list);
            Assert.AreEqual("theme-1", list.polls_theme_info[0].poll_theme_id);
            Assert.AreEqual(1, list.polls_theme_info[0].polls_info[0].is_shared);
            Assert.AreEqual(0, list.polls_theme_info[0].polls_info[0].is_anony);
            Assert.IsNotNull(theme);
            Assert.AreEqual("满意", theme.poll_question_data[0].option_info[0].option_desc);
            Assert.IsNotNull(detail);
            Assert.AreEqual("101", detail.poll_question_data[0].question_id);
            Assert.AreEqual("1111", detail.poll_question_data[0].option_info[0].option_id);
            Assert.AreEqual(80, detail.poll_question_data[0].option_info[0].rate);
            Assert.AreEqual("zhangsan", detail.poll_question_data[0].option_info[0].option_user[0].userid);
            Assert.AreEqual("tmp-1", detail.poll_question_data[0].option_info[0].option_user[0].tmp_openid);
            Assert.IsNotNull(start);
            Assert.AreEqual("poll-1", start.poll_id);
        }

        [TestMethod]
        public void PollPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(MeetingPollOperatorRequest), typeof(MeetingPollQuestion),
                typeof(CreateMeetingPollThemeRequest), typeof(CreateMeetingPollThemeResult),
                typeof(UpdateMeetingPollThemeRequest), typeof(UpdateMeetingPollThemeResult),
                typeof(GetMeetingPollListRequest), typeof(MeetingPollSummary),
                typeof(MeetingPollThemeSummary), typeof(GetMeetingPollListResult),
                typeof(GetMeetingPollThemeInfoRequest), typeof(MeetingPollThemeOption),
                typeof(MeetingPollThemeQuestion), typeof(GetMeetingPollThemeInfoResult),
                typeof(GetMeetingPollDetailRequest), typeof(MeetingPollVoter),
                typeof(MeetingPollDetailOption), typeof(MeetingPollDetailQuestion),
                typeof(GetMeetingPollDetailResult), typeof(DeleteMeetingPollRequest),
                typeof(DeleteMeetingPollResult), typeof(StartMeetingPollRequest),
                typeof(StartMeetingPollResult), typeof(FinishMeetingPollRequest),
                typeof(FinishMeetingPollResult)
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
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingPollJson.cs"));
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
