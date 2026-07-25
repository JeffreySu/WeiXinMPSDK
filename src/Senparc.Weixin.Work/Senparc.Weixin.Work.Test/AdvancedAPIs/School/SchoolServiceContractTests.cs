using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.School;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.School
{
    [TestClass]
    public class SchoolServiceContractTests
    {
        [TestMethod]
        public void SchoolApiContainsElevenServiceSyncAndAsyncPairsAndOfficialPaths()
        {
            var names = typeof(SchoolApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            foreach (var name in new[]
            {
                nameof(SchoolApi.GetTeacherHealthInfo), nameof(SchoolApi.GetStudentHealthInfo),
                nameof(SchoolApi.GetHealthQrCode), nameof(SchoolApi.GetSchoolLivingInfo),
                nameof(SchoolApi.GetSchoolWatchStatistics), nameof(SchoolApi.GetSchoolUnwatchStatistics),
                nameof(SchoolApi.GetSchoolWatchStatisticsV2), nameof(SchoolApi.GetSchoolUnwatchStatisticsV2),
                nameof(SchoolApi.GetSchoolPaymentResult), nameof(SchoolApi.GetSchoolTrade),
                nameof(SchoolApi.GetSchoolAllowScope)
            })
            {
                CollectionAssert.Contains(names, name);
                CollectionAssert.Contains(names, name + "Async");
            }

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "School", "SchoolApi.Service.cs"));
            foreach (var path in new[]
            {
                "/cgi-bin/school/user/get_teacher_customize_health_info",
                "/cgi-bin/school/user/get_student_customize_health_info",
                "/cgi-bin/school/user/get_health_qrcode",
                "/cgi-bin/school/living/get_living_info",
                "/cgi-bin/school/living/get_watch_stat",
                "/cgi-bin/school/living/get_unwatch_stat",
                "/cgi-bin/school/living/get_watch_stat_v2",
                "/cgi-bin/school/living/get_unwatch_stat_v2",
                "/cgi-bin/school/get_payment_result",
                "/cgi-bin/school/get_trade",
                "/cgi-bin/school/agent/get_allow_scope"
            })
            {
                StringAssert.Contains(source, path);
            }
            StringAssert.Contains(source, "livingId.AsUrlData()");
        }

        [TestMethod]
        public void HealthModelsPreserveQuestionsQrCodesAndPagination()
        {
            var requestJson = JsonSerializer.Serialize(new SchoolHealthQrCodeRequest
            {
                type = 1,
                userids = new List<string> { "teacher-1", "student-1" }
            });
            var result = JsonSerializer.Deserialize<SchoolHealthInfoResult>(
                "{\"errcode\":0,\"template_id\":\"template-1\",\"next_key\":\"next\"," +
                "\"ending\":0,\"health_infos\":[{\"userid\":\"student-1\"," +
                "\"health_qrcode_status\":1,\"self_submit\":1,\"report_values\":[{" +
                "\"question_id\":1,\"single_chose\":2,\"text\":\"ok\"}]," +
                "\"question_templates\":[{\"question_id\":1,\"question_type\":1," +
                "\"title\":\"status\",\"is_must_fill\":1,\"is_not_display\":0," +
                "\"option_list\":[{\"option_id\":2,\"option_text\":\"ok\"}]}]}]}");

            StringAssert.Contains(requestJson, "\"userids\":[\"teacher-1\",\"student-1\"]");
            Assert.IsNotNull(result);
            Assert.AreEqual("next", result.next_key);
            Assert.AreEqual("ok", result.health_infos[0].question_templates[0].option_list[0].option_text);
        }

        [TestMethod]
        public void LivingModelsPreserveLargeIdsTimesAndProtocolContainers()
        {
            var info = JsonSerializer.Deserialize<SchoolLivingInfoResult>(
                "{\"errcode\":0,\"living_info\":{\"theme\":\"class\"," +
                "\"living_start\":5178368698,\"living_duration\":3600," +
                "\"anchor_userid\":\"teacher-1\",\"living_range\":{" +
                "\"partyids\":[5178368799],\"group_names\":[\"group-1\"]}," +
                "\"viewer_num\":3,\"comment_num\":2,\"open_replay\":1," +
                "\"push_stream_url\":\"rtmp://example\"}}");
            var stats = JsonSerializer.Deserialize<SchoolLivingWatchResult>(
                "{\"errcode\":0,\"ending\":1,\"stat_infoes\":{\"students\":[{" +
                "\"student_userid\":\"student-1\",\"parent_userid\":\"parent-1\"," +
                "\"partyids\":[5178368799],\"watch_time\":60,\"is_comment\":1," +
                "\"enter_time\":5178368698,\"leave_time\":5178368798}],\"visitors\":[]}}");

            Assert.IsNotNull(info);
            Assert.AreEqual(5178368698L, info.living_info.living_start);
            Assert.AreEqual(5178368799L, info.living_info.living_range.partyids[0]);
            Assert.IsNotNull(stats);
            Assert.AreEqual(5178368798L, stats.stat_infoes.students[0].leave_time);
        }

        [TestMethod]
        public void LivingV2ModelsPreserveParentsVisitorsPaginationAndLargeValues()
        {
            var requestJson = JsonSerializer.Serialize(new SchoolLivingStatisticsV2Request
            {
                livingid = "living-1",
                next_cursor = "NEXT"
            });
            var watched = JsonSerializer.Deserialize<SchoolLivingWatchV2Result>(
                "{\"errcode\":0,\"has_more\":1,\"next_cursor\":\"NEXT-2\"," +
                "\"stat_info\":{\"students\":[{\"student_userid\":\"student-1\"," +
                "\"partyids\":[5178368799],\"watch_time\":60,\"enter_time\":5178368698," +
                "\"leave_time\":5178368798,\"is_comment\":1}],\"parents\":[{" +
                "\"parent_userid\":\"parent-1\",\"student_userid\":\"student-1\"," +
                "\"partyids\":[5178368799],\"watch_time\":60,\"enter_time\":5178368698," +
                "\"leave_time\":5178368798,\"is_comment\":0}],\"visitors\":[{" +
                "\"nickname\":\"visitor-1\",\"watch_time\":30,\"enter_time\":5178368698," +
                "\"leave_time\":5178368798,\"is_comment\":1}]}}");
            var unwatched = JsonSerializer.Deserialize<SchoolLivingUnwatchV2Result>(
                "{\"errcode\":0,\"has_more\":0,\"stat_info\":{\"students\":[{" +
                "\"student_userid\":\"student-2\",\"partyids\":[5178368899]}]," +
                "\"parents\":[{\"parent_userid\":\"parent-2\"," +
                "\"student_userid\":\"student-2\",\"partyids\":[5178368899]}]}}");

            StringAssert.Contains(requestJson, "\"livingid\":\"living-1\"");
            StringAssert.Contains(requestJson, "\"next_cursor\":\"NEXT\"");
            Assert.IsNotNull(watched);
            Assert.AreEqual(1, watched.has_more);
            Assert.AreEqual("NEXT-2", watched.next_cursor);
            Assert.AreEqual(5178368799L, watched.stat_info.parents[0].partyids[0]);
            Assert.AreEqual(5178368798L, watched.stat_info.visitors[0].leave_time);
            Assert.IsNotNull(unwatched);
            Assert.AreEqual("parent-2", unwatched.stat_info.parents[0].parent_userid);
            Assert.AreEqual(5178368899L, unwatched.stat_info.students[0].partyids[0]);
        }

        [TestMethod]
        public void PaymentAndAllowScopeModelsPreserveCurrentFields()
        {
            var payment = JsonSerializer.Deserialize<SchoolPaymentResult>(
                "{\"errcode\":0,\"project_name\":\"fee\",\"amount\":5178368698," +
                "\"payment_result\":[{\"student_userid\":\"student-1\",\"trade_state\":1," +
                "\"trade_no\":\"trade-1\",\"payer_parent_userid\":\"parent-1\"}]}");
            var scope = JsonSerializer.Deserialize<SchoolAllowScopeResult>(
                "{\"errcode\":0,\"allow_scope\":{\"students\":[{\"userid\":\"student-1\"}]," +
                "\"departments\":[5178368799]}}");

            Assert.IsNotNull(payment);
            Assert.AreEqual(5178368698L, payment.amount);
            Assert.AreEqual("parent-1", payment.payment_result[0].payer_parent_userid);
            Assert.IsNotNull(scope);
            Assert.AreEqual(5178368799L, scope.allow_scope.departments[0]);
        }

        private static string FindRepositoryRoot([CallerFilePath] string sourceFilePath = null)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(sourceFilePath));
            while (directory != null)
            {
                if (Directory.Exists(Path.Combine(directory.FullName, ".git"))) return directory.FullName;
                directory = directory.Parent;
            }
            Assert.Fail("Unable to locate repository root.");
            return null;
        }
    }
}
