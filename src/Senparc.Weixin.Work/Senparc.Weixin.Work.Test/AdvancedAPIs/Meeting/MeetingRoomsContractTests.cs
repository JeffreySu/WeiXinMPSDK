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
    public class MeetingRoomsContractTests
    {
        [TestMethod]
        public void RoomsApisContainTwelveSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(MeetingApi.BookMeetingRooms), typeof(BookMeetingRoomsRequest),
                    typeof(BookMeetingRoomsResult)),
                (nameof(MeetingApi.ReleaseMeetingRooms), typeof(ReleaseMeetingRoomsRequest),
                    typeof(WorkJsonResult)),
                (nameof(MeetingApi.GetMeetingRooms), typeof(GetMeetingRoomsRequest),
                    typeof(GetMeetingRoomsResult)),
                (nameof(MeetingApi.GetMeetingRoomInfo), typeof(GetMeetingRoomInfoRequest),
                    typeof(GetMeetingRoomInfoResult)),
                (nameof(MeetingApi.GetMeetingRoomConfig), typeof(GetMeetingRoomConfigRequest),
                    typeof(GetMeetingRoomConfigResult)),
                (nameof(MeetingApi.GetMeetingRoomMeetings), typeof(GetMeetingRoomMeetingsRequest),
                    typeof(GetMeetingRoomMeetingsResult)),
                (nameof(MeetingApi.GetMeetingRoomDevices), typeof(GetMeetingRoomDevicesRequest),
                    typeof(GetMeetingRoomDevicesResult)),
                (nameof(MeetingApi.GetMeetingRoomControllers), typeof(GetMeetingRoomControllersRequest),
                    typeof(GetMeetingRoomControllersResult)),
                (nameof(MeetingApi.GetMeetingRoomInventory), typeof(GetMeetingRoomInventoryRequest),
                    typeof(GetMeetingRoomInventoryResult)),
                (nameof(MeetingApi.CallMeetingRoom), typeof(CallMeetingRoomRequest),
                    typeof(CallMeetingRoomResult)),
                (nameof(MeetingApi.CancelMeetingRoomCall), typeof(CancelMeetingRoomCallRequest),
                    typeof(WorkJsonResult)),
                (nameof(MeetingApi.GetMeetingRoomResponseStatus),
                    typeof(GetMeetingRoomResponseStatusRequest),
                    typeof(GetMeetingRoomResponseStatusResult))
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
        public void RoomsApisUseOfficialPathsDocumentsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting",
                "MeetingApi.Rooms.cs"));
            var paths = new[]
            {
                "/cgi-bin/meeting/rooms/book",
                "/cgi-bin/meeting/rooms/release",
                "/cgi-bin/meeting/rooms/list",
                "/cgi-bin/meeting/rooms/get_info",
                "/cgi-bin/meeting/rooms/get_config",
                "/cgi-bin/meeting/rooms/list_meetings",
                "/cgi-bin/meeting/rooms/list_devices",
                "/cgi-bin/meeting/rooms/list_controllers",
                "/cgi-bin/meeting/rooms/get_inventory",
                "/cgi-bin/meeting/rooms/call",
                "/cgi-bin/meeting/rooms/cancel_call",
                "/cgi-bin/meeting/rooms/get_response_status"
            };
            var documents = new[]
            {
                "98791", "98792", "98795", "98793", "98802", "98796",
                "98798", "98799", "98809", "98804", "98805", "98806"
            };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            foreach (var document in documents)
            {
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + document), document);
            }

            Assert.AreEqual(24, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(24, CountOccurrences(source,
                "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(24, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(24, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(24, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(12, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(12, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void RoomsBookingListInfoAndConfigPreserveOfficialShapes()
        {
            var request = new BookMeetingRoomsRequest
            {
                meetingid = "meeting-1",
                meeting_room_id_list = new List<string> { "room-1", "room-2" },
                subject_visible = true
            };
            var listResult = JsonSerializer.Deserialize<GetMeetingRoomsResult>(
                "{\"errcode\":0,\"meeting_room_list\":[{\"meeting_room_id\":\"room-1\"," +
                "\"meeting_room_name\":\"A101\",\"meeting_room_location\":\"Suzhou\"," +
                "\"account_type\":1,\"active_code\":\"active-1\",\"participant_number\":20," +
                "\"meeting_room_status\":1,\"scheduled_status\":2,\"is_allow_call\":true}]," +
                "\"has_more\":true,\"next_cursor\":\"cursor-2\"}");
            var infoResult = JsonSerializer.Deserialize<GetMeetingRoomInfoResult>(
                "{\"errcode\":0,\"basic_info\":{\"rooms_id_list\":[\"rooms-1\"]," +
                "\"meeting_room_name\":\"A101\",\"city\":\"Suzhou\",\"building\":\"A\"," +
                "\"floor\":\"10\",\"participant_number\":20},\"account_info\":{" +
                "\"account_type\":1,\"valid_period\":\"2026-12-31\"},\"hardware_info\":{" +
                "\"monitor_frequency\":60,\"enable_video_mirror\":true},\"pmi_info\":{" +
                "\"pmi_code\":\"123456\",\"pmi_pwd\":\"654321\"},\"monitor_status\":1," +
                "\"scheduled_status\":2,\"is_allow_call\":true}");
            var configResult = JsonSerializer.Deserialize<GetMeetingRoomConfigResult>(
                "{\"errcode\":0,\"meeting_settings\":{\"water_mark\":1,\"auto_response\":2," +
                "\"caption\":true,\"room_pmi\":true,\"room_notification\":false}," +
                "\"record_settings\":{\"share_record\":1,\"download_record\":true}}");

            using var requestDocument = JsonDocument.Parse(JsonSerializer.Serialize(request));
            Assert.AreEqual("meeting-1", requestDocument.RootElement.GetProperty("meetingid")
                .GetString());
            Assert.AreEqual(2, requestDocument.RootElement.GetProperty("meeting_room_id_list")
                .GetArrayLength());
            Assert.IsTrue(requestDocument.RootElement.GetProperty("subject_visible").GetBoolean());

            Assert.IsNotNull(listResult);
            Assert.IsTrue(listResult.has_more);
            Assert.IsTrue(listResult.meeting_room_list.Single().is_allow_call);
            Assert.AreEqual(20, listResult.meeting_room_list.Single().participant_number);
            Assert.IsNotNull(infoResult);
            Assert.AreEqual("60", infoResult.hardware_info.monitor_frequency);
            Assert.AreEqual("123456", infoResult.pmi_info.pmi_code);
            Assert.IsTrue(infoResult.is_allow_call);
            Assert.IsNotNull(configResult);
            Assert.IsTrue(configResult.meeting_settings.caption);
            Assert.IsTrue(configResult.record_settings.download_record);
        }

        [TestMethod]
        public void RoomsMeetingsDevicesAndControllersPreservePagingAnd64BitFields()
        {
            var request = new GetMeetingRoomMeetingsRequest
            {
                rooms_id = "rooms-1",
                start_time = 5000000000L,
                end_time = 5000000100L,
                cursor = "cursor-1",
                limit = 50
            };
            var meetingResult = JsonSerializer.Deserialize<GetMeetingRoomMeetingsResult>(
                "{\"errcode\":0,\"meeting_info_list\":[{\"meetingid\":\"meeting-1\"," +
                "\"meeting_code\":\"123456\",\"subject\":\"Review\",\"meeting_type\":1," +
                "\"status\":\"meeting\",\"start_time\":5000000000," +
                "\"end_time\":5000000100}],\"has_more\":false,\"next_cursor\":\"\"}");
            var deviceResult = JsonSerializer.Deserialize<GetMeetingRoomDevicesResult>(
                "{\"errcode\":0,\"device_info_list\":[{\"rooms_id\":\"rooms-1\"," +
                "\"meeting_room_id\":\"room-1\",\"meeting_room_name\":\"A101\"," +
                "\"meeting_room_location\":\"Suzhou\",\"device_model\":\"Model-X\"," +
                "\"app_version\":\"1.0\",\"meeting_room_status\":1," +
                "\"device_monitor_info\":{\"camera_status\":true," +
                "\"microphone_status\":false,\"speaker_status\":true}}]," +
                "\"has_more\":true,\"next_cursor\":\"cursor-2\"}");
            var controllerResult = JsonSerializer.Deserialize<GetMeetingRoomControllersResult>(
                "{\"errcode\":0,\"controller_info_list\":[{\"rooms_id\":\"rooms-1\"," +
                "\"meeting_room_name\":\"A101\",\"meeting_room_location\":\"Suzhou\"," +
                "\"manufacture_name\":\"Vendor\",\"controller_name\":\"Controller-1\"," +
                "\"controller_model\":\"C1\",\"app_version\":\"1.0\"," +
                "\"framework_version\":\"2.0\",\"status\":1,\"ip_address\":\"127.0.0.1\"," +
                "\"mac_address\":\"00:00:00:00:00:00\",\"cpu_type\":\"ARM\"," +
                "\"cpu_usage\":\"10%\",\"network_type\":\"wifi\",\"mem_usage\":\"20%\"}]," +
                "\"has_more\":false,\"next_cursor\":\"\"}");

            using var requestDocument = JsonDocument.Parse(JsonSerializer.Serialize(request));
            Assert.AreEqual(5000000000L, requestDocument.RootElement.GetProperty("start_time")
                .GetInt64());
            Assert.AreEqual(5000000100L, requestDocument.RootElement.GetProperty("end_time")
                .GetInt64());
            Assert.IsNotNull(meetingResult);
            Assert.AreEqual(5000000000L, meetingResult.meeting_info_list.Single().start_time);
            Assert.IsNotNull(deviceResult);
            Assert.IsTrue(deviceResult.device_info_list.Single().device_monitor_info.camera_status);
            Assert.IsFalse(deviceResult.device_info_list.Single().device_monitor_info
                .microphone_status);
            Assert.IsNotNull(controllerResult);
            Assert.AreEqual("1", controllerResult.controller_info_list.Single().status);
            Assert.AreEqual("wifi", controllerResult.controller_info_list.Single().network_type);
        }

        [TestMethod]
        public void RoomsInventoryAndCallsPreserveMraAndResponseFields()
        {
            var callRequest = new CallMeetingRoomRequest
            {
                meetingid = "meeting-1",
                mra_address = new MeetingRoomMraAddress
                {
                    protocol = 1,
                    dial_string = "sip:room@example.com"
                }
            };
            var cancelRequest = new CancelMeetingRoomCallRequest
            {
                meetingid = "meeting-1",
                invite_id = "invite-1",
                meeting_room_id = "room-1"
            };
            var inventory = JsonSerializer.Deserialize<GetMeetingRoomInventoryResult>(
                "{\"errcode\":0,\"normal_count\":10,\"special_count\":2," +
                "\"normal_used_count\":8,\"special_used_count\":1," +
                "\"normal_expired_count\":1,\"special_expired_count\":0}");
            var response = JsonSerializer.Deserialize<GetMeetingRoomResponseStatusResult>(
                "{\"errcode\":0,\"status\":2,\"response_time\":\"2026/07/25 12:00:00\"}");

            using var callDocument = JsonDocument.Parse(JsonSerializer.Serialize(callRequest));
            using var cancelDocument = JsonDocument.Parse(JsonSerializer.Serialize(cancelRequest));
            var mra = callDocument.RootElement.GetProperty("mra_address");
            Assert.AreEqual(1, mra.GetProperty("protocol").GetInt32());
            Assert.AreEqual("sip:room@example.com", mra.GetProperty("dial_string").GetString());
            Assert.IsFalse(mra.TryGetProperty("DialogString", out _));
            Assert.AreEqual("invite-1", cancelDocument.RootElement.GetProperty("invite_id")
                .GetString());
            Assert.IsNotNull(inventory);
            Assert.AreEqual(10, inventory.normal_count);
            Assert.AreEqual(8, inventory.normal_used_count);
            Assert.IsNotNull(response);
            Assert.AreEqual(2, response.status);
            Assert.AreEqual("2026/07/25 12:00:00", response.response_time);
        }

        [TestMethod]
        public void RoomsPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(BookMeetingRoomsRequest), typeof(BookedMeetingRoomInfo),
                typeof(BookMeetingRoomsResult), typeof(ReleaseMeetingRoomsRequest),
                typeof(GetMeetingRoomsRequest), typeof(MeetingRoomListItem),
                typeof(GetMeetingRoomsResult), typeof(GetMeetingRoomInfoRequest),
                typeof(MeetingRoomBasicInfo), typeof(MeetingRoomAccountInfo),
                typeof(MeetingRoomHardwareInfo), typeof(MeetingRoomPmiInfo),
                typeof(GetMeetingRoomInfoResult), typeof(GetMeetingRoomConfigRequest),
                typeof(MeetingRoomMeetingSettings), typeof(MeetingRoomRecordSettings),
                typeof(GetMeetingRoomConfigResult), typeof(GetMeetingRoomMeetingsRequest),
                typeof(MeetingRoomMeetingInfo), typeof(GetMeetingRoomMeetingsResult),
                typeof(GetMeetingRoomDevicesRequest), typeof(MeetingRoomDeviceMonitorInfo),
                typeof(MeetingRoomDeviceInfo), typeof(GetMeetingRoomDevicesResult),
                typeof(GetMeetingRoomControllersRequest), typeof(MeetingRoomControllerInfo),
                typeof(GetMeetingRoomControllersResult), typeof(GetMeetingRoomInventoryRequest),
                typeof(GetMeetingRoomInventoryResult), typeof(MeetingRoomMraAddress),
                typeof(CallMeetingRoomRequest), typeof(CallMeetingRoomResult),
                typeof(CancelMeetingRoomCallRequest), typeof(GetMeetingRoomResponseStatusRequest),
                typeof(GetMeetingRoomResponseStatusResult)
            };

            foreach (var property in modelTypes.SelectMany(type => type.GetProperties(
                         BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)))
            {
                Assert.AreNotEqual(typeof(object), property.PropertyType,
                    property.DeclaringType?.Name + "." + property.Name);
            }

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting",
                "MeetingRoomsJson.cs"));
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
