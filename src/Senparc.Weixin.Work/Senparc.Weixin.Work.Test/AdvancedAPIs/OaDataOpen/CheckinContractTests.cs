using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen;
using Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen.OaDataOpenJson;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.OaDataOpen
{
    [TestClass]
    public class CheckinContractTests
    {
        [TestMethod]
        public void CheckinApiCoversAllFifteenOfficialEndpoints()
        {
            var methods = typeof(OaDataOpenApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var methodName in new[]
            {
                nameof(OaDataOpenApi.GetCorpCheckinOption), nameof(OaDataOpenApi.GetCheckinOption),
                nameof(OaDataOpenApi.GetCheckinData), nameof(OaDataOpenApi.GetCheckinDayData),
                nameof(OaDataOpenApi.GetCheckinMonthData), nameof(OaDataOpenApi.GetCheckinScheduleList),
                nameof(OaDataOpenApi.SetCheckinScheduleList), nameof(OaDataOpenApi.PunchCorrection),
                nameof(OaDataOpenApi.AddCheckinRecord), nameof(OaDataOpenApi.AddCheckinUserFace),
                nameof(OaDataOpenApi.GetHardwareCheckinData), nameof(OaDataOpenApi.AddCheckinOption),
                nameof(OaDataOpenApi.UpdateCheckinOption), nameof(OaDataOpenApi.ClearCheckinOptionArrayField),
                nameof(OaDataOpenApi.DeleteCheckinOption)
            })
            {
                CollectionAssert.Contains(methods, methodName, methodName);
                CollectionAssert.Contains(methods, methodName + "Async", methodName + "Async");
            }
        }

        [TestMethod]
        public void CheckinApiUsesAllOfficialPaths()
        {
            var root = FindRepositoryRoot();
            var originalSource = File.ReadAllText(Path.Combine(root,
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "OaDataOpen", "OaDataOpenApi.cs"));
            var incrementalSource = File.ReadAllText(Path.Combine(root,
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "OaDataOpen", "OaDataOpenApi.CheckinP2.cs"));
            var source = originalSource + incrementalSource;

            foreach (var path in new[]
            {
                "/cgi-bin/checkin/getcorpcheckinoption", "/cgi-bin/checkin/getcheckinoption",
                "/cgi-bin/checkin/getcheckindata", "/cgi-bin/checkin/getcheckin_daydata",
                "/cgi-bin/checkin/getcheckin_monthdata", "/cgi-bin/checkin/getcheckinschedulist",
                "/cgi-bin/checkin/setcheckinschedulist", "/cgi-bin/checkin/punch_correction",
                "/cgi-bin/checkin/add_checkin_record", "/cgi-bin/checkin/addcheckinuserface",
                "/cgi-bin/hardware/get_hardware_checkin_data", "/cgi-bin/checkin/add_checkin_option",
                "/cgi-bin/checkin/update_checkin_option",
                "/cgi-bin/checkin/clear_checkin_option_array_field",
                "/cgi-bin/checkin/del_checkin_option"
            })
            {
                StringAssert.Contains(source, path);
            }
        }

        [TestMethod]
        public void ExistingCheckinApiSignaturesRemainAvailable()
        {
            Assert.IsNotNull(typeof(OaDataOpenApi).GetMethod(nameof(OaDataOpenApi.GetCheckinData),
                new[] { typeof(string), typeof(OaDataOpenApi.OpenCheckinDataType), typeof(DateTime),
                    typeof(DateTime), typeof(string[]), typeof(int) }));
            Assert.IsNotNull(typeof(OaDataOpenApi).GetMethod(nameof(OaDataOpenApi.GetCheckinDayData),
                new[] { typeof(string), typeof(DateTime), typeof(DateTime), typeof(string[]), typeof(int) }));
            Assert.IsNotNull(typeof(OaDataOpenApi).GetMethod(nameof(OaDataOpenApi.GetCheckinOption),
                new[] { typeof(string), typeof(DateTime), typeof(string[]), typeof(int) }));
            var addRecord = typeof(OaDataOpenApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Single(method => method.Name == nameof(OaDataOpenApi.AddCheckinRecord));

            Assert.AreEqual(typeof(WorkJsonResult), addRecord.ReturnType);
        }

        [TestMethod]
        public void RuleModelsCoverCurrentFieldsAndLargeValues()
        {
            var json = JsonSerializer.Serialize(new CheckinOptionRequest
            {
                effective_now = true,
                group = new Group
                {
                    groupid = 1,
                    groupname = "研发排班",
                    range = new CheckinRange
                    {
                        party_id = new[] { 4294967296L },
                        userid = new[] { "zhangsan" },
                        tagid = new[] { 4294967297L }
                    },
                    checkindate = new[]
                    {
                        new Checkindate
                        {
                            allow_flex = true,
                            biweekly = new CheckinBiweekly
                            {
                                enable_weekday_recurrence = true,
                                odd_workdays = new[] { 1, 2, 3, 4, 5 },
                                even_workdays = new[] { 1, 2, 3, 4 }
                            },
                            checkintime = new[]
                            {
                                new Checkintime
                                {
                                    time_id = 1,
                                    earliest_work_sec = 25200,
                                    latest_off_work_sec = 72000,
                                    rest_times = new[]
                                    {
                                        new CheckinRestTime
                                        {
                                            rest_begin_time = 43200,
                                            rest_end_time = 46800
                                        }
                                    }
                                }
                            }
                        }
                    },
                    ot_info_v2 = new CheckinOvertimeInfoV2
                    {
                        workdayconf = new CheckinOvertimeDayConfig
                        {
                            allow_ot = true,
                            checkin = new CheckinOvertimeModeConfig { ot_time_max = 14400 }
                        },
                        time_unit_config = new CheckinOvertimeUnitConfig
                        {
                            ot_time_unit = 2,
                            perday_duration_secs = 28800
                        }
                    },
                    buka_restriction = ulong.MaxValue,
                    open_face_live_detect = true,
                    buka_remind = new CheckinCorrectionReminder
                    {
                        open_remind = true,
                        buka_remind_day = 20,
                        buka_remind_month = 0
                    }
                }
            });

            StringAssert.Contains(json, "\"party_id\":[4294967296]");
            StringAssert.Contains(json, "\"tagid\":[4294967297]");
            StringAssert.Contains(json, "\"enable_weekday_recurrence\":true");
            StringAssert.Contains(json, "\"latest_off_work_sec\":72000");
            StringAssert.Contains(json, "\"ot_info_v2\"");
            StringAssert.Contains(json, "\"buka_restriction\":18446744073709551615");
        }

        [TestMethod]
        public void MonthAndScheduleModelsUseOfficialShapes()
        {
            var month = JsonSerializer.Deserialize<GetCheckinMonthDataJsonResult>(
                "{\"errcode\":0,\"datas\":[{\"base_info\":{\"acctid\":\"zhangsan\"," +
                "\"rule_info\":{\"groupid\":1,\"groupname\":\"研发\"}}," +
                "\"summary_info\":{\"work_days\":22,\"standard_work_sec\":633600}," +
                "\"exception_infos\":[{\"exception\":1,\"count\":2,\"duration\":600}]," +
                "\"sp_items\":[],\"overwork_info\":{\"workday_over_sec\":3600}}]}");
            var schedule = JsonSerializer.Deserialize<GetCheckinScheduleListJsonResult>(
                "{\"errcode\":0,\"schedule_list\":[{\"userid\":\"zhangsan\"," +
                "\"yearmonth\":202607,\"groupid\":1,\"schedule\":{\"scheduleList\":[{" +
                "\"day\":24,\"schedule_info\":{\"schedule_id\":2,\"schedule_name\":\"早班\"," +
                "\"time_section\":[{\"id\":1,\"work_sec\":32400,\"off_work_sec\":64800}]}}]}}]}");

            Assert.AreEqual(633600, month.datas[0].summary_info.standard_work_sec);
            Assert.AreEqual(2, month.datas[0].exception_infos[0].count);
            Assert.AreEqual("早班", schedule.schedule_list[0].schedule.scheduleList[0]
                .schedule_info.schedule_name);
            Assert.AreEqual(64800, schedule.schedule_list[0].schedule.scheduleList[0]
                .schedule_info.time_section[0].off_work_sec);
        }

        [TestMethod]
        public void WriteAndHardwareModelsPreserveExactFieldsAndLargeTimestamps()
        {
            var correctionJson = JsonSerializer.Serialize(new PunchCorrectionRequest
            {
                userid = "zhangsan",
                schedule_date_time = 4294967296L,
                schedule_checkin_time = 32400,
                checkin_time = 4294967297L,
                remark = "系统补卡"
            });
            var hardware = JsonSerializer.Deserialize<GetHardwareCheckinDataJsonResult>(
                "{\"errcode\":0,\"checkindata\":[{\"userid\":\"zhangsan\"," +
                "\"checkin_time\":4294967298,\"device_sn\":\"SN-1\",\"device_name\":\"前台\"}]}");
            var clearJson = JsonSerializer.Serialize(new ClearCheckinOptionArrayFieldRequest
            {
                groupid = 1,
                clear_field = new[] { 1, 2, 3, 4 },
                effective_now = true
            });

            StringAssert.Contains(correctionJson, "\"schedule_date_time\":4294967296");
            StringAssert.Contains(correctionJson, "\"checkin_time\":4294967297");
            Assert.AreEqual(4294967298L, hardware.checkindata[0].checkin_time);
            StringAssert.Contains(clearJson, "\"clear_field\":[1,2,3,4]");
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
