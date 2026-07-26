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
    public class MeetingManagementContractTests
    {
        [TestMethod]
        public void ManagementApiContainsFiveSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(MeetingApi.CancelMeeting), typeof(CancelMeetingRequest), typeof(CancelMeetingResult)),
                (nameof(MeetingApi.GetMeetingInfo), typeof(GetMeetingInfoRequest), typeof(GetMeetingInfoResult)),
                (nameof(MeetingApi.CheckDeviceInMeeting), typeof(CheckDeviceInMeetingRequest),
                    typeof(CheckDeviceInMeetingResult)),
                (nameof(MeetingApi.GetMeetingGuests), typeof(GetMeetingGuestsRequest),
                    typeof(GetMeetingGuestsResult)),
                (nameof(MeetingApi.SetMeetingGuests), typeof(SetMeetingGuestsRequest),
                    typeof(SetMeetingGuestsResult))
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
        public void ManagementApiUsesOfficialPathsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingApi.Management.cs"));
            var paths = new[]
            {
                "/cgi-bin/meeting/cancel",
                "/cgi-bin/meeting/get_info",
                "/cgi-bin/meeting/check_device_in_meeting",
                "/cgi-bin/meeting/get_guests",
                "/cgi-bin/meeting/set_guests"
            };
            var documentIds = new[] { "93709", "98153", "93708", "98149", "99039", "99040" };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            foreach (var documentId in documentIds)
            {
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + documentId), documentId);
            }

            Assert.AreEqual(2,
                CountOccurrences(source, "固定协议记录的参考文档编号为 98164"));
            Assert.AreEqual(10, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(10, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(10, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(10, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(10, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(5, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(5, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void ManagementRequestsPreserveMeetingAndGuestFields()
        {
            var cancel = new CancelMeetingRequest
            {
                meetingid = "meeting-1",
                sub_meetingid = "sub-meeting-1"
            };
            using var cancelDocument = JsonDocument.Parse(JsonSerializer.Serialize(cancel));
            Assert.AreEqual("meeting-1", cancelDocument.RootElement.GetProperty("meetingid").GetString());
            Assert.AreEqual("sub-meeting-1",
                cancelDocument.RootElement.GetProperty("sub_meetingid").GetString());

            var info = new GetMeetingInfoRequest
            {
                meetingid = "meeting-1",
                meeting_code = "123456789",
                sub_meetingid = "sub-meeting-1"
            };
            using var infoDocument = JsonDocument.Parse(JsonSerializer.Serialize(info));
            Assert.AreEqual("123456789", infoDocument.RootElement.GetProperty("meeting_code").GetString());
            Assert.AreEqual("sub-meeting-1", infoDocument.RootElement.GetProperty("sub_meetingid").GetString());

            var checkDevice = new CheckDeviceInMeetingRequest
            {
                userid = "zhangsan",
                instance_id_list = new List<int> { 1, 2, 3 },
                meetingid_list = new List<string> { "meeting-1", "meeting-2" }
            };
            using var checkDeviceDocument = JsonDocument.Parse(JsonSerializer.Serialize(checkDevice));
            Assert.AreEqual("zhangsan",
                checkDeviceDocument.RootElement.GetProperty("userid").GetString());
            Assert.AreEqual(3,
                checkDeviceDocument.RootElement.GetProperty("instance_id_list")[2].GetInt32());
            Assert.AreEqual("meeting-2",
                checkDeviceDocument.RootElement.GetProperty("meetingid_list")[1].GetString());

            var getGuests = new GetMeetingGuestsRequest { meetingid = "meeting-1" };
            using var getGuestsDocument = JsonDocument.Parse(JsonSerializer.Serialize(getGuests));
            Assert.AreEqual("meeting-1", getGuestsDocument.RootElement.GetProperty("meetingid").GetString());

            var setGuests = new SetMeetingGuestsRequest
            {
                meetingid = "meeting-1",
                guests = new List<MeetingGuest>
                {
                    new MeetingGuest
                    {
                        area = "86", phone_number = "13800000000", guest_name = "外部嘉宾"
                    }
                }
            };
            using var setGuestsDocument = JsonDocument.Parse(JsonSerializer.Serialize(setGuests));
            Assert.AreEqual("86", setGuestsDocument.RootElement.GetProperty("guests")[0]
                .GetProperty("area").GetString());
            Assert.AreEqual("13800000000", setGuestsDocument.RootElement.GetProperty("guests")[0]
                .GetProperty("phone_number").GetString());
            Assert.AreEqual("外部嘉宾", setGuestsDocument.RootElement.GetProperty("guests")[0]
                .GetProperty("guest_name").GetString());
        }

        [TestMethod]
        public void ManagementResultsPreserveAttendeesSettingsAndLongTimestamps()
        {
            var checkDevice = JsonSerializer.Deserialize<CheckDeviceInMeetingResult>(
                "{\"errcode\":0,\"result_list\":[{" +
                "\"meetingid\":\"meeting-1\",\"instance_id\":1}]}" );
            var info = JsonSerializer.Deserialize<GetMeetingInfoResult>(
                "{\"errcode\":0,\"meetingid\":\"meeting-1\",\"admin_userid\":\"zhangsan\"," +
                "\"main_department\":\"4294967296\",\"title\":\"季度经营会\"," +
                "\"meeting_start\":\"6178368698\",\"meeting_duration\":3600," +
                "\"status\":2,\"meeting_type\":1,\"description\":\"季度复盘\"," +
                "\"location\":\"深圳总部\",\"cal_id\":\"calendar-1\",\"attendees\":{" +
                "\"member\":[{\"userid\":\"zhangsan\",\"status\":1," +
                "\"first_join_time\":\"6178368700\",\"last_quit_time\":\"6178369900\"," +
                "\"total_join_count\":2,\"cumulative_time\":1200}]," +
                "\"tmp_external_user\":[{\"tmp_external_userid\":\"external-1\",\"status\":2," +
                "\"first_join_time\":\"6178368800\",\"last_quit_time\":\"6178369800\"," +
                "\"total_join_count\":1,\"cumulative_time\":1000}]," +
                "\"device\":[{\"device_sn\":\"device-1\",\"status\":1}]}," +
                "\"guests\":[{\"area\":\"86\",\"phone_number\":\"13800000000\"," +
                "\"guest_name\":\"外部嘉宾\"}],\"settings\":{\"need_password\":true," +
                "\"password\":\"123456\",\"enable_waiting_room\":true," +
                "\"current_hosts\":{\"userid\":[\"zhangsan\"]}," +
                "\"co_hosts\":{\"userid\":[\"lisi\"]}},\"reminders\":{\"is_repeat\":1," +
                "\"repeat_type\":2,\"repeat_until\":\"6179368698\"}," +
                "\"meeting_code\":\"123456789\",\"meeting_link\":\"https://meeting.example/join\"," +
                "\"has_vote\":true,\"sub_meetings\":[{\"sub_meetingid\":\"sub-meeting-1\"," +
                "\"title\":\"第一场\",\"status\":1,\"start_time\":\"6178368698\"," +
                "\"end_time\":\"6178372298\",\"repeat_id\":\"repeat-1\"}]," +
                "\"has_more_sub_meeting\":1,\"remain_sub_meetings\":3," +
                "\"current_sub_meetingid\":\"sub-meeting-1\",\"sub_repeat_list\":[{" +
                "\"repeat_id\":\"repeat-1\",\"repeat_type\":2,\"is_custom_repeat\":1," +
                "\"repeat_interval\":1,\"repeat_day_of_week\":[1,3]," +
                "\"repeat_day_of_month\":[1],\"repeat_until_type\":2," +
                "\"repeat_until_count\":10,\"repeat_until\":\"6179368698\"}]}");
            var guests = JsonSerializer.Deserialize<GetMeetingGuestsResult>(
                "{\"errcode\":0,\"guests\":[{\"area\":\"86\"," +
                "\"phone_number\":\"13800000000\",\"guest_name\":\"外部嘉宾\"}]," +
                "\"meetingid\":\"meeting-1\",\"meeting_code\":\"123456789\"," +
                "\"title\":\"季度经营会\"}");

            Assert.IsNotNull(checkDevice);
            Assert.AreEqual("meeting-1", checkDevice.result_list[0].meetingid);
            Assert.AreEqual(1, checkDevice.result_list[0].instance_id);
            Assert.IsNotNull(info);
            Assert.AreEqual(4294967296L, info.main_department);
            Assert.AreEqual(6178368698L, info.meeting_start);
            Assert.AreEqual(6178368700L, info.attendees.member[0].first_join_time);
            Assert.AreEqual(6178369800L, info.attendees.tmp_external_user[0].last_quit_time);
            Assert.AreEqual("device-1", info.attendees.device[0].device_sn);
            Assert.AreEqual("外部嘉宾", info.guests[0].guest_name);
            Assert.IsTrue(info.settings.need_password);
            Assert.AreEqual("zhangsan", info.settings.current_hosts.userid[0]);
            Assert.AreEqual("lisi", info.settings.co_hosts.userid[0]);
            Assert.AreEqual(6179368698L, info.reminders.repeat_until);
            Assert.AreEqual(6178372298L, info.sub_meetings[0].end_time);
            Assert.AreEqual(1, info.has_more_sub_meeting);
            Assert.AreEqual(6179368698L, info.sub_repeat_list[0].repeat_until);
            Assert.IsNotNull(guests);
            Assert.AreEqual("meeting-1", guests.meetingid);
            Assert.AreEqual("123456789", guests.meeting_code);
            Assert.AreEqual("13800000000", guests.guests[0].phone_number);
        }

        [TestMethod]
        public void ManagementPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(CancelMeetingRequest), typeof(CancelMeetingResult), typeof(GetMeetingInfoRequest),
                typeof(MeetingInfoMemberAttendee), typeof(MeetingInfoExternalAttendee),
                typeof(MeetingInfoDeviceAttendee), typeof(MeetingInfoAttendees), typeof(MeetingInfoSettings),
                typeof(MeetingInfoSubMeeting), typeof(MeetingInfoSubRepeat), typeof(GetMeetingInfoResult),
                typeof(CheckDeviceInMeetingRequest), typeof(DeviceInMeetingItem),
                typeof(CheckDeviceInMeetingResult),
                typeof(GetMeetingGuestsRequest), typeof(GetMeetingGuestsResult), typeof(SetMeetingGuestsRequest),
                typeof(SetMeetingGuestsResult)
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
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingManagementJson.cs"));
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
