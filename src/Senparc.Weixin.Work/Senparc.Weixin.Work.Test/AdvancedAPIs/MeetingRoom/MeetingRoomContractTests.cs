using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.MeetingRoom;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.MeetingRoom
{
    [TestClass]
    public class MeetingRoomContractTests
    {
        [TestMethod]
        public void MeetingRoomApiContainsElevenSyncAndAsyncEntries()
        {
            var methodNames = typeof(MeetingRoomApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            var syncMethodNames = new[]
            {
                nameof(MeetingRoomApi.AddMeetingRoom),
                nameof(MeetingRoomApi.GetMeetingRoomList),
                nameof(MeetingRoomApi.UpdateMeetingRoom),
                nameof(MeetingRoomApi.DeleteMeetingRoom),
                nameof(MeetingRoomApi.GetMeetingRoomBookingInfo),
                nameof(MeetingRoomApi.GetMeetingRoomBookingInfoByMeetingId),
                nameof(MeetingRoomApi.BookMeetingRoom),
                nameof(MeetingRoomApi.BookMeetingRoomBySchedule),
                nameof(MeetingRoomApi.BookMeetingRoomByMeeting),
                nameof(MeetingRoomApi.CancelMeetingRoomBooking),
                nameof(MeetingRoomApi.GetMeetingRoomBookingDetail)
            };

            foreach (var syncMethodName in syncMethodNames)
            {
                CollectionAssert.Contains(methodNames, syncMethodName, syncMethodName);
                CollectionAssert.Contains(methodNames, syncMethodName + "Async", syncMethodName + "Async");
            }
        }

        [TestMethod]
        public void MeetingRoomApiUsesElevenOfficialPostPaths()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "MeetingRoom", "MeetingRoomApi.cs"));
            var paths = new[]
            {
                "/cgi-bin/oa/meetingroom/add",
                "/cgi-bin/oa/meetingroom/list",
                "/cgi-bin/oa/meetingroom/edit",
                "/cgi-bin/oa/meetingroom/del",
                "/cgi-bin/oa/meetingroom/get_booking_info",
                "/cgi-bin/oa/meetingroom/get_booking_info_by_meeting_id",
                "/cgi-bin/oa/meetingroom/book",
                "/cgi-bin/oa/meetingroom/book_by_schedule",
                "/cgi-bin/oa/meetingroom/book_by_meeting",
                "/cgi-bin/oa/meetingroom/cancel_book",
                "/cgi-bin/oa/meetingroom/bookinfo/get"
            };

            foreach (var path in paths)
            {
                StringAssert.Contains(source, path);
            }

            StringAssert.Contains(source, "CommonJsonSendType.POST");
            Assert.AreEqual(2, CountOccurrences(source, "/document/path/93620"));
        }

        [TestMethod]
        public void MeetingRoomQueryByMeetingIdUsesOfficialShapeAndLargeValues()
        {
            var requestJson = JsonSerializer.Serialize(new GetMeetingRoomBookingInfoByMeetingIdRequest
            {
                meetingroom_id = 5178368698L,
                meeting_id = "meeting-1"
            });
            var result = JsonSerializer.Deserialize<GetMeetingRoomBookingInfoByMeetingIdResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"meetingroom_id\":5178368698,\"schedule\":{" +
                "\"meeting_id\":\"meeting-1\",\"schedule_id\":\"schedule-1\"," +
                "\"start_time\":5178368800,\"end_time\":5178370600," +
                "\"booker\":\"zhangsan\",\"status\":0}}");
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "MeetingRoom",
                "MeetingRoomJson.cs"));

            StringAssert.Contains(requestJson, "\"meetingroom_id\":5178368698");
            StringAssert.Contains(requestJson, "\"meeting_id\":\"meeting-1\"");
            Assert.IsNotNull(result);
            Assert.AreEqual(5178368698L, result.meetingroom_id);
            Assert.AreEqual("meeting-1", result.schedule.meeting_id);
            Assert.AreEqual(5178370600L, result.schedule.end_time);
            Assert.IsFalse(new[]
            {
                typeof(GetMeetingRoomBookingInfoByMeetingIdRequest),
                typeof(GetMeetingRoomBookingInfoByMeetingIdResult),
                typeof(MeetingRoomBookingSchedule)
            }.SelectMany(type => type.GetProperties(BindingFlags.Public | BindingFlags.Instance |
                BindingFlags.DeclaredOnly)).Any(property => property.PropertyType == typeof(object)));
            StringAssert.Contains(source, "/// <summary>根据会议 ID 查询会议室预定信息请求。</summary>");
            StringAssert.Contains(source, "/// <summary>关联会议 ID；通过会议预定或按会议 ID 查询时返回。</summary>");
        }

        [TestMethod]
        public void MeetingRoomManagementRequestsUseOfficialJsonFields()
        {
            var addJson = JsonSerializer.Serialize(new AddMeetingRoomRequest
            {
                name = "研发会议室",
                capacity = 12,
                city = "深圳",
                building = "腾讯大厦",
                floor = "18F",
                equipment = new List<int> { 1, 3 },
                coordinate = new MeetingRoomCoordinate { latitude = "22.5405", longitude = "113.9345" },
                range = new MeetingRoomRange
                {
                    user_list = new List<string> { "zhangsan" },
                    department_list = new List<long> { 5178368698L }
                }
            });

            using var addDocument = JsonDocument.Parse(addJson);
            Assert.AreEqual("研发会议室", addDocument.RootElement.GetProperty("name").GetString());
            StringAssert.Contains(addJson, "\"capacity\":12");
            StringAssert.Contains(addJson, "\"equipment\":[1,3]");
            StringAssert.Contains(addJson, "\"latitude\":\"22.5405\"");
            StringAssert.Contains(addJson, "\"department_list\":[5178368698]");
        }

        [TestMethod]
        public void MeetingRoomBookingModelsPreserveLargeIdsAndTimestamps()
        {
            var requestJson = JsonSerializer.Serialize(new BookMeetingRoomRequest
            {
                meetingroom_id = 5178368698L,
                subject = "周会",
                start_time = 5178368800L,
                end_time = 5178370600L,
                booker = "zhangsan",
                attendees = new List<string> { "lisi", "wangwu" }
            });
            var result = JsonSerializer.Deserialize<GetMeetingRoomBookingDetailResult>(
                "{\"errcode\":0,\"meetingroom_id\":5178368698,\"schedule\":{" +
                "\"booking_id\":\"booking-1\",\"master_booking_id\":\"master-1\"," +
                "\"schedule_id\":\"schedule-1\",\"start_time\":5178368800," +
                "\"end_time\":5178370600,\"booker\":\"zhangsan\",\"status\":3}}" );

            StringAssert.Contains(requestJson, "\"meetingroom_id\":5178368698");
            StringAssert.Contains(requestJson, "\"start_time\":5178368800");
            StringAssert.Contains(requestJson, "\"attendees\":[\"lisi\",\"wangwu\"]");
            Assert.IsNotNull(result);
            Assert.AreEqual(5178368698L, result.meetingroom_id);
            Assert.AreEqual(5178370600L, result.schedule.end_time);
            Assert.AreEqual("master-1", result.schedule.master_booking_id);
            Assert.AreEqual(3, result.schedule.status);
        }

        [TestMethod]
        public void MeetingRoomRecurringAndCancelModelsUseOfficialFields()
        {
            var scheduleJson = JsonSerializer.Serialize(new BookMeetingRoomByScheduleRequest
            {
                meetingroom_id = 1,
                schedule_id = "schedule-1",
                booker = "rocky"
            });
            var meetingJson = JsonSerializer.Serialize(new BookMeetingRoomByMeetingRequest
            {
                meetingroom_id = 1,
                meetingid = "meeting-1",
                booker = "rocky"
            });
            var cancelJson = JsonSerializer.Serialize(new CancelMeetingRoomBookingRequest
            {
                booking_id = "booking-1",
                keep_schedule = 1,
                cancel_date = 5178368698L
            });
            var result = JsonSerializer.Deserialize<BookMeetingRoomForRecurringResult>(
                "{\"errcode\":0,\"booking_id\":\"booking-1\",\"conflict_date\":[5178368698]}" );

            StringAssert.Contains(scheduleJson, "\"schedule_id\":\"schedule-1\"");
            StringAssert.Contains(meetingJson, "\"meetingid\":\"meeting-1\"");
            StringAssert.Contains(cancelJson, "\"keep_schedule\":1");
            StringAssert.Contains(cancelJson, "\"cancel_date\":5178368698");
            Assert.IsNotNull(result);
            Assert.AreEqual(5178368698L, result.conflict_date[0]);
        }

        [TestMethod]
        public void MeetingRoomCallbacksMapToStrongTypes()
        {
            var bookDoc = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>5178368698</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[book_meeting_room]]></Event>
<MeetingRoomId>5178368698</MeetingRoomId>
<BookingId><![CDATA[booking-1]]></BookingId>
</xml>");
            var cancelDoc = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>5178368698</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[cancel_meeting_room]]></Event>
<MeetingRoomId>5178368698</MeetingRoomId>
<BookingId><![CDATA[booking-2]]></BookingId>
</xml>");

            var booked = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), bookDoc) as RequestMessageEvent_Book_Meeting_Room;
            var cancelled = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), cancelDoc) as RequestMessageEvent_Cancel_Meeting_Room;

            Assert.IsNotNull(booked);
            Assert.AreEqual(Event.book_meeting_room, booked.Event);
            Assert.AreEqual(5178368698L, booked.MeetingRoomId);
            Assert.AreEqual("booking-1", booked.BookingId);
            Assert.IsNotNull(cancelled);
            Assert.AreEqual(Event.cancel_meeting_room, cancelled.Event);
            Assert.AreEqual(5178368698L, cancelled.MeetingRoomId);
            Assert.AreEqual("booking-2", cancelled.BookingId);
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
