using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.Meeting;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Meeting
{
    [TestClass]
    public class MeetingWebinarContractTests
    {
        [TestMethod]
        public void WebinarApisContainFourteenSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(MeetingApi.CreateWebinar), typeof(CreateWebinarRequest),
                    typeof(CreateWebinarResult)),
                (nameof(MeetingApi.UpdateWebinar), typeof(UpdateWebinarRequest),
                    typeof(WorkJsonResult)),
                (nameof(MeetingApi.CancelWebinar), typeof(CancelWebinarRequest),
                    typeof(WorkJsonResult)),
                (nameof(MeetingApi.GetWebinar), typeof(GetWebinarRequest),
                    typeof(GetWebinarResult)),
                (nameof(MeetingApi.GetWebinarGuests), typeof(GetWebinarGuestsRequest),
                    typeof(GetWebinarGuestsResult)),
                (nameof(MeetingApi.UpdateWebinarGuests), typeof(UpdateWebinarGuestsRequest),
                    typeof(WorkJsonResult)),
                (nameof(MeetingApi.UpdateWebinarWarmUp), typeof(UpdateWebinarWarmUpRequest),
                    typeof(WorkJsonResult)),
                (nameof(MeetingApi.SetWebinarEnrollmentConfig),
                    typeof(SetWebinarEnrollmentConfigRequest),
                    typeof(SetWebinarEnrollmentConfigResult)),
                (nameof(MeetingApi.GetWebinarEnrollmentConfig),
                    typeof(GetWebinarEnrollmentConfigRequest),
                    typeof(GetWebinarEnrollmentConfigResult)),
                (nameof(MeetingApi.QueryWebinarEnrollmentsByTempOpenIds),
                    typeof(QueryMeetingEnrollmentsByTempOpenIdsRequest),
                    typeof(QueryMeetingEnrollmentsByTempOpenIdsResult)),
                (nameof(MeetingApi.GetWebinarEnrollments),
                    typeof(GetMeetingEnrollmentsRequest), typeof(GetMeetingEnrollmentsResult)),
                (nameof(MeetingApi.ApproveWebinarEnrollments),
                    typeof(ApproveMeetingEnrollmentsRequest),
                    typeof(ApproveMeetingEnrollmentsResult)),
                (nameof(MeetingApi.ImportWebinarEnrollments),
                    typeof(ImportMeetingEnrollmentsRequest),
                    typeof(ImportMeetingEnrollmentsResult)),
                (nameof(MeetingApi.DeleteWebinarEnrollments),
                    typeof(DeleteMeetingEnrollmentsRequest),
                    typeof(DeleteMeetingEnrollmentsResult))
            };

            foreach (var contract in contracts)
            {
                var parameterTypes = new[] { typeof(string), contract.Item2, typeof(int) };
                var syncMethod = typeof(MeetingApi).GetMethod(contract.Item1, parameterTypes);
                var asyncMethod = typeof(MeetingApi).GetMethod(contract.Item1 + "Async",
                    parameterTypes);

                Assert.IsNotNull(syncMethod, contract.Item1);
                Assert.AreEqual(contract.Item3, syncMethod.ReturnType, contract.Item1);
                Assert.IsNotNull(asyncMethod, contract.Item1 + "Async");
                Assert.AreEqual(typeof(Task<>).MakeGenericType(contract.Item3),
                    asyncMethod.ReturnType, contract.Item1 + "Async");
            }
        }

        [TestMethod]
        public void WebinarApisUseFixedProtocolPathsDocumentsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting",
                "MeetingApi.Webinar.cs"));
            var paths = new[]
            {
                "/cgi-bin/meeting/webinar/create",
                "/cgi-bin/meeting/webinar/update",
                "/cgi-bin/meeting/webinar/cancel",
                "/cgi-bin/meeting/webinar/get",
                "/cgi-bin/meeting/webinar/list_guest",
                "/cgi-bin/meeting/webinar/update_guest_list",
                "/cgi-bin/meeting/webinar/update_warm_up",
                "/cgi-bin/meeting/webinar/enroll/set_config",
                "/cgi-bin/meeting/webinar/enroll/get_config",
                "/cgi-bin/meeting/webinar/enroll/query_by_tmp_openid",
                "/cgi-bin/meeting/webinar/enroll/list",
                "/cgi-bin/meeting/webinar/enroll/approve",
                "/cgi-bin/meeting/webinar/enroll/import",
                "/cgi-bin/meeting/webinar/enroll/delete"
            };
            var documents = new Dictionary<string, int>
            {
                ["98842"] = 2,
                ["98843"] = 4,
                ["98860"] = 2,
                ["98871"] = 2,
                ["98872"] = 2,
                ["98882"] = 2,
                ["98875"] = 2,
                ["98874"] = 2,
                ["98873"] = 2,
                ["98876"] = 2,
                ["98877"] = 2,
                ["98880"] = 2,
                ["98881"] = 2
            };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            foreach (var document in documents)
            {
                Assert.AreEqual(document.Value,
                    CountOccurrences(source, "/document/path/" + document.Key), document.Key);
            }

            Assert.AreEqual(28, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(28, CountOccurrences(source,
                "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(28, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(28, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(28, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(14, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(14, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void WebinarCreateUpdateAndDetailPreserveProtocolShapesAnd64BitTimes()
        {
            var request = new CreateWebinarRequest
            {
                admin_userid = "admin",
                title = "Product launch",
                sponsor = "Senparc",
                start_time = 5000000000L,
                end_time = 5000003600L,
                hosts = new List<WebinarHost> { new WebinarHost { userid = "host-1" } },
                admission_type = 1,
                enable_guest_invite_link = true,
                media_setting = new WebinarMediaSetting
                {
                    enable_enter_mute = true,
                    allow_unmute_self = false,
                    auto_record_type = "cloud"
                },
                enable_qa = true,
                sensitive_words = new List<string> { "secret" },
                playback_for_audience = true
            };
            var detail = JsonSerializer.Deserialize<GetWebinarResult>(
                "{\"errcode\":0,\"meetingid\":\"meeting-1\"," +
                "\"meeting_code\":\"123456\",\"title\":\"Product launch\"," +
                "\"sponsor\":\"Senparc\",\"start_time\":\"5000000000\"," +
                "\"end_time\":\"5000003600\",\"status\":\"meeting\"," +
                "\"hosts\":[{\"userid\":\"host-1\"}],\"admission_type\":1," +
                "\"enable_guest_invite_link\":true,\"media_setting\":{" +
                "\"enable_enter_mute\":true,\"allow_unmute_self\":false," +
                "\"auto_record_type\":\"cloud\"},\"enable_qa\":true," +
                "\"activity_page\":false,\"display_number_of_attendees\":1," +
                "\"playback_for_audience\":true,\"playback_url\":\"https://play\"," +
                "\"preparation_mode\":true,\"warm_up_picture\":\"https://image\"," +
                "\"warm_up_video\":\"https://video\"," +
                "\"allow_attendees_invite_others\":true}");

            using var document = JsonDocument.Parse(JsonSerializer.Serialize(request));
            Assert.AreEqual(5000000000L,
                document.RootElement.GetProperty("start_time").GetInt64());
            Assert.AreEqual("host-1", document.RootElement.GetProperty("hosts")[0]
                .GetProperty("userid").GetString());
            Assert.IsTrue(document.RootElement.GetProperty("media_setting")
                .GetProperty("enable_enter_mute").GetBoolean());
            Assert.IsFalse(document.RootElement.TryGetProperty("AdminUserId", out _));
            Assert.IsNotNull(detail);
            Assert.AreEqual(5000000000L, detail.start_time);
            Assert.AreEqual("host-1", detail.hosts.Single().userid);
            Assert.IsTrue(detail.media_setting.enable_enter_mute);
            Assert.IsTrue(detail.allow_attendees_invite_others);
        }

        [TestMethod]
        public void WebinarGuestsWarmUpAndEnrollmentConfigRemainStronglyTyped()
        {
            var guestsRequest = new UpdateWebinarGuestsRequest
            {
                meetingid = "meeting-1",
                guests = new List<WebinarGuest>
                {
                    new WebinarGuest
                    {
                        guest_type = 2,
                        area = "86",
                        phone_number = "13800138000",
                        guest_name = "Guest"
                    }
                }
            };
            var warmUpRequest = new UpdateWebinarWarmUpRequest
            {
                meetingid = "meeting-1",
                warm_up_picture = "https://image",
                warm_up_video = "https://video",
                allow_attendees_invite_others = true
            };
            var configRequest = new SetWebinarEnrollmentConfigRequest
            {
                meetingid = "meeting-1",
                approve_type = 1,
                is_collect_question = 1,
                no_registration_needed_for_staff = true,
                question_list = new List<WebinarEnrollmentQuestion>
                {
                    new WebinarEnrollmentQuestion
                    {
                        is_required = 1,
                        question_type = 2,
                        special_type = 0,
                        question_title = "Team",
                        option_list = new List<WebinarEnrollmentQuestionOption>
                        {
                            new WebinarEnrollmentQuestionOption { content = "SDK" }
                        }
                    }
                }
            };
            var configResult = JsonSerializer.Deserialize<GetWebinarEnrollmentConfigResult>(
                "{\"errcode\":0,\"approve_type\":1,\"is_collect_question\":1," +
                "\"question_list\":[{\"is_required\":1,\"question_type\":2," +
                "\"special_type\":0,\"question_title\":\"Team\"," +
                "\"option_list\":[{\"content\":\"SDK\"}]}]," +
                "\"no_registration_needed_for_staff\":true}");

            using var guestsDocument = JsonDocument.Parse(JsonSerializer.Serialize(guestsRequest));
            using var warmUpDocument = JsonDocument.Parse(JsonSerializer.Serialize(warmUpRequest));
            using var configDocument = JsonDocument.Parse(JsonSerializer.Serialize(configRequest));
            Assert.AreEqual("13800138000", guestsDocument.RootElement.GetProperty("guests")[0]
                .GetProperty("phone_number").GetString());
            Assert.IsTrue(warmUpDocument.RootElement
                .GetProperty("allow_attendees_invite_others").GetBoolean());
            Assert.AreEqual("SDK", configDocument.RootElement.GetProperty("question_list")[0]
                .GetProperty("option_list")[0].GetProperty("content").GetString());
            Assert.IsNotNull(configResult);
            Assert.IsTrue(configResult.no_registration_needed_for_staff);
            Assert.AreEqual("SDK", configResult.question_list.Single().option_list.Single().content);
        }

        [TestMethod]
        public void WebinarEnrollmentOperationsReuseCompatibleMeetingEnrollmentModels()
        {
            var query = new QueryMeetingEnrollmentsByTempOpenIdsRequest
            {
                meetingid = "meeting-1",
                sorting_rules = 1,
                tmp_openid_list = new List<string> { "tmp-1" }
            };
            var import = new ImportMeetingEnrollmentsRequest
            {
                meetingid = "meeting-1",
                enroll_list = new List<MeetingEnrollmentImportItem>
                {
                    new MeetingEnrollmentImportItem
                    {
                        area = "86",
                        phone_number = "13800138000",
                        nick_name = "Guest"
                    }
                }
            };
            var list = JsonSerializer.Deserialize<GetMeetingEnrollmentsResult>(
                "{\"errcode\":0,\"enroll_list\":[{\"enroll_id\":\"5000000000\"," +
                "\"tmp_openid\":\"tmp-1\",\"status\":1,\"answer_list\":[{" +
                "\"answer_content\":[\"SDK\"],\"is_required\":1," +
                "\"question_type\":2,\"special_type\":0,\"question_num\":1," +
                "\"question_title\":\"Team\"}]}],\"has_more\":false," +
                "\"next_cursor\":\"\"}");

            using var queryDocument = JsonDocument.Parse(JsonSerializer.Serialize(query));
            using var importDocument = JsonDocument.Parse(JsonSerializer.Serialize(import));
            Assert.AreEqual("tmp-1", queryDocument.RootElement.GetProperty("tmp_openid_list")[0]
                .GetString());
            Assert.AreEqual("13800138000", importDocument.RootElement.GetProperty("enroll_list")[0]
                .GetProperty("phone_number").GetString());
            Assert.IsNotNull(list);
            Assert.AreEqual("5000000000", list.enroll_list.Single().enroll_id);
            Assert.AreEqual("SDK", list.enroll_list.Single().answer_list.Single()
                .answer_content.Single());
        }

        [TestMethod]
        public void WebinarPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(WebinarHost), typeof(WebinarMediaSetting),
                typeof(CreateWebinarRequest), typeof(CreateWebinarResult),
                typeof(UpdateWebinarRequest), typeof(CancelWebinarRequest),
                typeof(GetWebinarRequest), typeof(GetWebinarResult),
                typeof(GetWebinarGuestsRequest), typeof(WebinarGuest),
                typeof(GetWebinarGuestsResult), typeof(UpdateWebinarGuestsRequest),
                typeof(UpdateWebinarWarmUpRequest), typeof(WebinarEnrollmentQuestionOption),
                typeof(WebinarEnrollmentQuestion), typeof(SetWebinarEnrollmentConfigRequest),
                typeof(SetWebinarEnrollmentConfigResult),
                typeof(GetWebinarEnrollmentConfigRequest),
                typeof(GetWebinarEnrollmentConfigResult)
            };

            foreach (var property in modelTypes.SelectMany(type => type.GetProperties(
                         BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)))
            {
                Assert.AreNotEqual(typeof(object), property.PropertyType,
                    property.DeclaringType?.Name + "." + property.Name);
            }

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting",
                "MeetingWebinarJson.cs"));
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
