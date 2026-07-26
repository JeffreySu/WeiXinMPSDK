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
    public class MeetingContractTests
    {
        [TestMethod]
        public void MeetingApiContainsTwelveSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(MeetingApi.CreateMeeting), typeof(CreateMeetingRequest), typeof(CreateMeetingResult)),
                (nameof(MeetingApi.UpdateMeeting), typeof(UpdateMeetingRequest), typeof(UpdateMeetingResult)),
                (nameof(MeetingApi.GetUserMeetingIds), typeof(GetUserMeetingIdsRequest),
                    typeof(GetUserMeetingIdsResult)),
                (nameof(MeetingApi.GetMeetingInvitees), typeof(GetMeetingInviteesRequest),
                    typeof(GetMeetingInviteesResult)),
                (nameof(MeetingApi.SetMeetingInvitees), typeof(SetMeetingInviteesRequest),
                    typeof(SetMeetingInviteesResult)),
                (nameof(MeetingApi.CreateMeetingCustomerShortUrl), typeof(CreateMeetingCustomerShortUrlRequest),
                    typeof(CreateMeetingCustomerShortUrlResult)),
                (nameof(MeetingApi.GetMeetingCustomerShortUrls), typeof(GetMeetingCustomerShortUrlsRequest),
                    typeof(GetMeetingCustomerShortUrlsResult)),
                (nameof(MeetingApi.GetMeetingRealtimeAttendees), typeof(GetMeetingRealtimeAttendeesRequest),
                    typeof(GetMeetingRealtimeAttendeesResult)),
                (nameof(MeetingApi.GetMeetingAttendees), typeof(GetMeetingAttendeesRequest),
                    typeof(GetMeetingAttendeesResult)),
                (nameof(MeetingApi.GetCurrentMeetingWaitingRoomUsers), typeof(GetMeetingWaitingRoomUsersRequest),
                    typeof(GetCurrentMeetingWaitingRoomUsersResult)),
                (nameof(MeetingApi.GetMeetingWaitingRoomUsers), typeof(GetMeetingWaitingRoomUsersRequest),
                    typeof(GetMeetingWaitingRoomUsersResult)),
                (nameof(MeetingApi.GetMeetingQuality), typeof(GetMeetingQualityRequest),
                    typeof(GetMeetingQualityResult))
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
        public void MeetingApiUsesOfficialPathsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingApi.cs"));
            var paths = new[]
            {
                "/cgi-bin/meeting/create",
                "/cgi-bin/meeting/update",
                "/cgi-bin/meeting/get_user_meetingid",
                "/cgi-bin/meeting/get_invitees",
                "/cgi-bin/meeting/set_invitees",
                "/cgi-bin/meeting/create_customer_short_url",
                "/cgi-bin/meeting/get_customer_short_url",
                "/cgi-bin/meeting/get_realtime_attendee_list",
                "/cgi-bin/meeting/get_attendee_list",
                "/cgi-bin/meeting/waitingroom/get_current_user_list",
                "/cgi-bin/meeting/waitingroom/get_user_list",
                "/cgi-bin/meeting/get_quality"
            };
            var documentIds = new[]
            {
                "93706", "98148", "93710", "98154", "93707", "98714", "98160", "98162",
                "98818", "98819", "98157", "98156", "98163", "98164", "98821"
            };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            foreach (var documentId in documentIds)
            {
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + documentId), documentId);
            }

            Assert.AreEqual(25, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(24, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(24, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(24, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(24, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(12, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(12, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void MeetingRequestsPreserveNestedSettingsAndLongTimestamps()
        {
            var create = new CreateMeetingRequest
            {
                admin_userid = "zhangsan",
                title = "季度经营会",
                meeting_start = 6178368698L,
                meeting_duration = 3600,
                description = "季度经营复盘",
                location = "深圳总部",
                cal_id = "calendar-1",
                invitees = new MeetingInvitees
                {
                    userid = new List<string> { "lisi" },
                    device_sn = new List<string> { "device-1" }
                },
                guests = new List<MeetingGuest>
                {
                    new MeetingGuest { area = "86", phone_number = "13800000000", guest_name = "外部嘉宾" }
                },
                settings = new MeetingSettings
                {
                    password = "123456",
                    enable_waiting_room = true,
                    allow_enter_before_host = false,
                    enable_enter_mute = 2,
                    auto_record_type = "cloud",
                    hosts = new MeetingUserGroup { userid = new List<string> { "zhangsan" } },
                    ring_users = new MeetingUserGroup { userid = new List<string> { "lisi" } }
                },
                reminders = new MeetingReminder
                {
                    is_repeat = 1,
                    repeat_type = 2,
                    is_custom_repeat = 1,
                    repeat_until = 6179368698L,
                    repeat_day_of_week = new List<int> { 1, 3 },
                    remind_before = new List<int> { 300, 900 }
                },
                agentid = 1000002
            };

            using var createDocument = JsonDocument.Parse(JsonSerializer.Serialize(create));
            var root = createDocument.RootElement;
            Assert.AreEqual(6178368698L, root.GetProperty("meeting_start").GetInt64());
            Assert.AreEqual("device-1", root.GetProperty("invitees").GetProperty("device_sn")[0].GetString());
            Assert.AreEqual("13800000000", root.GetProperty("guests")[0].GetProperty("phone_number").GetString());
            Assert.IsTrue(root.GetProperty("settings").GetProperty("enable_waiting_room").GetBoolean());
            Assert.IsFalse(root.GetProperty("settings").GetProperty("allow_enter_before_host").GetBoolean());
            Assert.AreEqual("zhangsan", root.GetProperty("settings").GetProperty("hosts")
                .GetProperty("userid")[0].GetString());
            Assert.AreEqual(1, root.GetProperty("reminders").GetProperty("is_repeat").GetInt32());
            Assert.AreEqual(6179368698L, root.GetProperty("reminders").GetProperty("repeat_until").GetInt64());

            var update = new UpdateMeetingRequest
            {
                meetingid = "meeting-1",
                meeting_start = 6178468698L,
                meeting_duration = 7200,
                invitees = new MeetingInvitees { userid = new List<string> { "wangwu" } }
            };
            using var updateDocument = JsonDocument.Parse(JsonSerializer.Serialize(update));
            Assert.AreEqual("meeting-1", updateDocument.RootElement.GetProperty("meetingid").GetString());
            Assert.AreEqual(6178468698L, updateDocument.RootElement.GetProperty("meeting_start").GetInt64());

            var list = new GetUserMeetingIdsRequest
            {
                userid = "zhangsan",
                begin_time = 6178368698L,
                end_time = 6179368698L,
                limit = 100
            };
            using var listDocument = JsonDocument.Parse(JsonSerializer.Serialize(list));
            Assert.AreEqual("0", listDocument.RootElement.GetProperty("cursor").GetString());
            Assert.AreEqual(6179368698L, listDocument.RootElement.GetProperty("end_time").GetInt64());

            var setInvitees = new SetMeetingInviteesRequest
            {
                meetingid = "meeting-1",
                invitees = new List<MeetingInvitee>
                {
                    new MeetingInvitee { userid = "zhangsan" },
                    new MeetingInvitee { userid = "lisi" }
                }
            };
            using var inviteesDocument = JsonDocument.Parse(JsonSerializer.Serialize(setInvitees));
            Assert.AreEqual("meeting-1", inviteesDocument.RootElement.GetProperty("meetingid").GetString());
            Assert.AreEqual("lisi", inviteesDocument.RootElement.GetProperty("invitees")[1]
                .GetProperty("userid").GetString());

            var shortUrl = new CreateMeetingCustomerShortUrlRequest
            {
                meetingid = "meeting-1",
                customer_data = "customer-42"
            };
            using var shortUrlDocument = JsonDocument.Parse(JsonSerializer.Serialize(shortUrl));
            Assert.AreEqual("customer-42", shortUrlDocument.RootElement.GetProperty("customer_data").GetString());

            var attendeeRequest = new GetMeetingAttendeesRequest
            {
                meetingid = "meeting-1",
                sub_meetingid = "sub-meeting-1",
                start_time = 6178368698L,
                end_time = 6179368698L,
                cursor = "cursor-1",
                limit = 100
            };
            using var attendeeDocument = JsonDocument.Parse(JsonSerializer.Serialize(attendeeRequest));
            Assert.AreEqual(6178368698L, attendeeDocument.RootElement.GetProperty("start_time").GetInt64());
            Assert.AreEqual("sub-meeting-1", attendeeDocument.RootElement.GetProperty("sub_meetingid").GetString());

            var waitingRoomRequest = new GetMeetingWaitingRoomUsersRequest
            {
                meetingid = "meeting-1",
                cursor = "cursor-4",
                limit = 50
            };
            using var waitingRoomDocument = JsonDocument.Parse(JsonSerializer.Serialize(waitingRoomRequest));
            Assert.AreEqual("cursor-4", waitingRoomDocument.RootElement.GetProperty("cursor").GetString());
            Assert.AreEqual(50, waitingRoomDocument.RootElement.GetProperty("limit").GetInt32());

            var qualityRequest = new GetMeetingQualityRequest
            {
                meetingid = "meeting-1",
                sub_meetingid = "sub-meeting-1",
                start_time = 6178368698L,
                cursor = "cursor-5",
                limit = 20
            };
            using var qualityDocument = JsonDocument.Parse(JsonSerializer.Serialize(qualityRequest));
            Assert.AreEqual(6178368698L, qualityDocument.RootElement.GetProperty("start_time").GetInt64());
            Assert.AreEqual(20, qualityDocument.RootElement.GetProperty("limit").GetInt32());
        }

        [TestMethod]
        public void MeetingResultsPreserveLinksPaginationAndExcessUsers()
        {
            var create = JsonSerializer.Deserialize<CreateMeetingResult>(
                "{\"errcode\":0,\"meetingid\":\"meeting-1\",\"meeting_code\":\"123456789\"," +
                "\"meeting_link\":\"https://meeting.example/join\",\"excess_users\":[\"invalid-user\"]}");
            var update = JsonSerializer.Deserialize<UpdateMeetingResult>(
                "{\"errcode\":0,\"excess_users\":[\"invalid-user\"]}");
            var list = JsonSerializer.Deserialize<GetUserMeetingIdsResult>(
                "{\"errcode\":0,\"meetingid_list\":[\"meeting-1\",\"meeting-2\"]," +
                "\"next_cursor\":\"cursor-2\"}");
            var invitees = JsonSerializer.Deserialize<GetMeetingInviteesResult>(
                "{\"errcode\":0,\"invitees\":[{\"userid\":\"zhangsan\"}]," +
                "\"has_more\":true,\"next_cursor\":\"cursor-3\"}");
            var shortUrl = JsonSerializer.Deserialize<CreateMeetingCustomerShortUrlResult>(
                "{\"errcode\":0,\"meeting_short_url_customer_data\":{" +
                "\"meeting_short_url\":\"https://meeting.example/s/1\"," +
                "\"customer_data\":\"customer-42\"}}");
            var realtime = JsonSerializer.Deserialize<GetMeetingRealtimeAttendeesResult>(
                "{\"errcode\":0,\"attendees\":[{\"userid\":\"zhangsan\",\"tmp_openid\":\"tmp-1\"," +
                "\"instance_id\":1,\"role\":2,\"join_type\":3,\"join_time\":\"6178368698\"," +
                "\"audio_state\":true,\"video_state\":false,\"screen_shared_state\":true}]," +
                "\"has_more\":false}");
            var attendee = JsonSerializer.Deserialize<GetMeetingAttendeesResult>(
                "{\"errcode\":0,\"attendees\":[{\"userid\":\"zhangsan\",\"instance_id\":1," +
                "\"role\":2,\"join_type\":3,\"join_time\":\"6178368698\"," +
                "\"quit_time\":\"6178369999\",\"audio_state\":false,\"video_state\":false," +
                "\"screen_shared_state\":false,\"net\":\"wifi\",\"webinar_role\":1," +
                "\"customer_data\":\"customer-42\"}],\"has_more\":false}");
            var currentWaiting = JsonSerializer.Deserialize<GetCurrentMeetingWaitingRoomUsersResult>(
                "{\"errcode\":0,\"user_list\":[{\"userid\":\"zhangsan\",\"tmp_openid\":\"tmp-1\"," +
                "\"instance_id\":1,\"customer_data\":\"customer-42\"}],\"has_more\":false}");
            var waitingHistory = JsonSerializer.Deserialize<GetMeetingWaitingRoomUsersResult>(
                "{\"errcode\":0,\"user_list\":[{\"userid\":\"zhangsan\",\"instance_id\":1," +
                "\"join_time\":\"6178368698\",\"quit_time\":\"6178369999\"}],\"has_more\":false}");
            var quality = JsonSerializer.Deserialize<GetMeetingQualityResult>(
                "{\"errcode\":0,\"quality\":90,\"audio_quality\":91,\"video_quality\":92," +
                "\"screen_share_quality\":93,\"network_quality\":94,\"problems\":[\"packet_loss\"]," +
                "\"attendees\":[{\"userid\":\"zhangsan\",\"instance_id\":1,\"quality\":80," +
                "\"audio_quality\":81,\"video_quality\":82,\"screen_share_quality\":83," +
                "\"network_quality\":84,\"problems\":[\"jitter\"]}],\"has_more\":false}");

            Assert.IsNotNull(create);
            Assert.AreEqual("meeting-1", create.meetingid);
            Assert.AreEqual("123456789", create.meeting_code);
            Assert.AreEqual("invalid-user", create.excess_users[0]);
            Assert.IsNotNull(update);
            Assert.AreEqual("invalid-user", update.excess_users[0]);
            Assert.IsNotNull(list);
            Assert.AreEqual("meeting-2", list.meetingid_list[1]);
            Assert.AreEqual("cursor-2", list.next_cursor);
            Assert.IsNotNull(invitees);
            Assert.AreEqual("zhangsan", invitees.invitees[0].userid);
            Assert.IsTrue(invitees.has_more);
            Assert.AreEqual("cursor-3", invitees.next_cursor);
            Assert.IsNotNull(shortUrl);
            Assert.AreEqual("customer-42", shortUrl.meeting_short_url_customer_data.customer_data);
            Assert.IsNotNull(realtime);
            Assert.AreEqual(6178368698L, realtime.attendees[0].join_time);
            Assert.IsTrue(realtime.attendees[0].screen_shared_state);
            Assert.IsNotNull(attendee);
            Assert.AreEqual(6178369999L, attendee.attendees[0].quit_time);
            Assert.AreEqual("wifi", attendee.attendees[0].net);
            Assert.IsNotNull(currentWaiting);
            Assert.AreEqual("customer-42", currentWaiting.user_list[0].customer_data);
            Assert.IsNotNull(waitingHistory);
            Assert.AreEqual(6178368698L, waitingHistory.user_list[0].join_time);
            Assert.AreEqual(6178369999L, waitingHistory.user_list[0].quit_time);
            Assert.IsNotNull(quality);
            Assert.AreEqual(94, quality.network_quality);
            Assert.AreEqual("packet_loss", quality.problems[0]);
            Assert.AreEqual(82, quality.attendees[0].video_quality);
            Assert.AreEqual("jitter", quality.attendees[0].problems[0]);
        }

        [TestMethod]
        public void MeetingPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(MeetingInvitees), typeof(MeetingGuest), typeof(MeetingUserGroup), typeof(MeetingSettings),
                typeof(MeetingReminder), typeof(CreateMeetingRequest), typeof(CreateMeetingResult),
                typeof(UpdateMeetingRequest), typeof(UpdateMeetingResult), typeof(GetUserMeetingIdsRequest),
                typeof(GetUserMeetingIdsResult), typeof(MeetingInvitee), typeof(GetMeetingInviteesRequest),
                typeof(GetMeetingInviteesResult), typeof(SetMeetingInviteesRequest),
                typeof(SetMeetingInviteesResult), typeof(MeetingCustomerShortUrlData),
                typeof(CreateMeetingCustomerShortUrlRequest), typeof(CreateMeetingCustomerShortUrlResult),
                typeof(GetMeetingCustomerShortUrlsRequest), typeof(GetMeetingCustomerShortUrlsResult),
                typeof(GetMeetingRealtimeAttendeesRequest), typeof(MeetingRealtimeAttendee),
                typeof(GetMeetingRealtimeAttendeesResult), typeof(GetMeetingAttendeesRequest),
                typeof(MeetingAttendee), typeof(GetMeetingAttendeesResult),
                typeof(GetMeetingWaitingRoomUsersRequest), typeof(MeetingWaitingRoomCurrentUser),
                typeof(GetCurrentMeetingWaitingRoomUsersResult), typeof(MeetingWaitingRoomUser),
                typeof(GetMeetingWaitingRoomUsersResult), typeof(GetMeetingQualityRequest),
                typeof(MeetingQualityMetrics), typeof(MeetingQualityAttendee), typeof(GetMeetingQualityResult)
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
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingJson.cs"));
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
