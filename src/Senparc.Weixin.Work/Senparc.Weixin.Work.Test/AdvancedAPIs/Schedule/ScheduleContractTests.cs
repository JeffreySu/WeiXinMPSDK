using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.Calendar;
using Senparc.Weixin.Work.AdvancedAPIs.Schedule;
using Senparc.Weixin.Work.AdvancedAPIs.Schedule.ScheduleJson;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Schedule
{
    [TestClass]
    public class ScheduleContractTests
    {
        [TestMethod]
        public void CalendarAndScheduleApisCoverOfficialEntries()
        {
            var calendarMethods = typeof(CalendarApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            var scheduleMethods = typeof(ScheduleApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var methodName in new[] { "Add", "Update", "Get", "Del" })
            {
                CollectionAssert.Contains(calendarMethods, methodName, methodName);
                CollectionAssert.Contains(calendarMethods, methodName + "Async", methodName + "Async");
            }

            foreach (var methodName in new[]
            {
                "Add", "Update", "UpdateRepeat", "AddAttendees", "DelAttendees",
                "GetByCalendar", "Get", "Del"
            })
            {
                CollectionAssert.Contains(scheduleMethods, methodName, methodName);
                CollectionAssert.Contains(scheduleMethods, methodName + "Async", methodName + "Async");
            }
        }

        [TestMethod]
        public void UpdateRepeatUsesOfficialPathAndStronglyTypedOptions()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "Schedule", "ScheduleApi.cs"));
            var json = JsonSerializer.Serialize(new ScheduleUpdateData
            {
                skip_attendees = 1,
                op_mode = 2,
                op_start_time = 4294967296L,
                schedule = new ScheduleUpdate
                {
                    schedule_id = "schedule-1",
                    start_time = 1700000000,
                    end_time = 1700003600
                }
            });
            var result = JsonSerializer.Deserialize<UpdateScheduleJsonResult>(
                "{\"errcode\":0,\"schedule_id\":\"schedule-2\"}");

            StringAssert.Contains(source, "/cgi-bin/oa/schedule/update");
            StringAssert.Contains(json, "\"skip_attendees\":1");
            StringAssert.Contains(json, "\"op_mode\":2");
            StringAssert.Contains(json, "\"op_start_time\":4294967296");
            StringAssert.Contains(json, "\"schedule_id\":\"schedule-1\"");
            Assert.IsNotNull(result);
            Assert.AreEqual("schedule-2", result.schedule_id);
        }

        [TestMethod]
        public void ExistingUpdateSignaturesRemainAvailable()
        {
            var update = typeof(ScheduleApi).GetMethod(nameof(ScheduleApi.Update),
                new[] { typeof(string), typeof(ScheduleUpdate), typeof(int) });
            var updateAsync = typeof(ScheduleApi).GetMethod(nameof(ScheduleApi.UpdateAsync),
                new[] { typeof(string), typeof(ScheduleUpdate), typeof(int) });

            Assert.IsNotNull(update);
            Assert.AreEqual(typeof(WorkJsonResult), update.ReturnType);
            Assert.IsNotNull(updateAsync);
            Assert.AreEqual(typeof(Task<WorkJsonResult>), updateAsync.ReturnType);
        }

        [TestMethod]
        public void CalendarCallbacksMapToStrongTypes()
        {
            var deleted = ParseEvent("delete_calendar", "<CalId><![CDATA[calendar-1]]></CalId>")
                as RequestMessageEvent_Delete_Calendar;
            var modified = ParseEvent("modify_calendar", "<CalId><![CDATA[calendar-2]]></CalId>")
                as RequestMessageEvent_Modify_Calendar;

            Assert.IsNotNull(deleted);
            Assert.AreEqual(Event.delete_calendar, deleted.Event);
            Assert.AreEqual("calendar-1", deleted.CalId);
            Assert.IsNotNull(modified);
            Assert.AreEqual(Event.modify_calendar, modified.Event);
            Assert.AreEqual("calendar-2", modified.CalId);
        }

        [TestMethod]
        public void ScheduleCallbacksMapToStrongTypesAndHandlerHooks()
        {
            const string fields = "<CalId><![CDATA[calendar-1]]></CalId>" +
                                  "<ScheduleId><![CDATA[schedule-1]]></ScheduleId>";
            var modified = ParseEvent("modify_schedule", fields) as RequestMessageEvent_Modify_Schedule;
            var deleted = ParseEvent("delete_schedule", fields) as RequestMessageEvent_Delete_Schedule;
            var responded = ParseEvent("respond_schedule", fields) as RequestMessageEvent_Respond_Schedule;
            var handlerMethods = typeof(Senparc.Weixin.Work.MessageHandlers.WorkMessageHandler<>).GetMethods()
                .Select(method => method.Name).ToArray();

            Assert.IsNotNull(modified);
            Assert.AreEqual(Event.modify_schedule, modified.Event);
            Assert.AreEqual("calendar-1", modified.CalId);
            Assert.AreEqual("schedule-1", modified.ScheduleId);
            Assert.IsNotNull(deleted);
            Assert.AreEqual(Event.delete_schedule, deleted.Event);
            Assert.IsNotNull(responded);
            Assert.AreEqual(Event.respond_schedule, responded.Event);
            CollectionAssert.Contains(handlerMethods, "OnEvent_DeleteCalendarRequest");
            CollectionAssert.Contains(handlerMethods, "OnEvent_ModifyCalendarRequestAsync");
            CollectionAssert.Contains(handlerMethods, "OnEvent_ModifyScheduleRequest");
            CollectionAssert.Contains(handlerMethods, "OnEvent_DeleteScheduleRequestAsync");
            CollectionAssert.Contains(handlerMethods, "OnEvent_RespondScheduleRequest");
        }

        private static IWorkRequestMessageBase ParseEvent(string eventName, string fields)
        {
            var document = XDocument.Parse("<xml>" +
                "<ToUserName><![CDATA[toUser]]></ToUserName>" +
                "<FromUserName><![CDATA[fromUser]]></FromUserName>" +
                "<CreateTime>4294967296</CreateTime>" +
                "<MsgType><![CDATA[event]]></MsgType>" +
                "<Event><![CDATA[" + eventName + "]]></Event>" + fields + "</xml>");

            return RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), document);
        }

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
