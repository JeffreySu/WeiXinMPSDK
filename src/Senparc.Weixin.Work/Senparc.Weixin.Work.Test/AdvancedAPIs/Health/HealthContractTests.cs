using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Health;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Health
{
    [TestClass]
    public class HealthContractTests
    {
        [TestMethod]
        public void HealthApiExposesFourSynchronousAndAsynchronousEndpoints()
        {
            var methods = typeof(HealthApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var methodName in new[]
            {
                nameof(HealthApi.GetStatistics), nameof(HealthApi.GetReportJobIds),
                nameof(HealthApi.GetReportJobInfo), nameof(HealthApi.GetReportAnswer)
            })
            {
                CollectionAssert.Contains(methods, methodName);
                CollectionAssert.Contains(methods, methodName + "Async");
            }
        }

        [TestMethod]
        public void HealthApiUsesOfficialPathsAndDocumentationLinks()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Health", "HealthApi.cs"));

            foreach (var path in new[]
            {
                "/cgi-bin/health/get_health_report_stat", "/cgi-bin/health/get_report_jobids",
                "/cgi-bin/health/get_report_job_info", "/cgi-bin/health/get_report_answer"
            })
            {
                Assert.AreEqual(1, CountOccurrences(source, path), path);
            }

            foreach (var documentId in new[] { "93676", "93677", "93678", "93679" })
            {
                Assert.AreEqual(2, CountOccurrences(source, "document/path/" + documentId), documentId);
            }

            Assert.AreEqual(9, CountOccurrences(source, "/// <summary>"));
        }

        [TestMethod]
        public void StatisticsAndJobIdModelsMatchOfficialSamples()
        {
            Assert.AreEqual(
                "{\"date\":\"2020-03-27\"}",
                JsonSerializer.Serialize(new HealthGetReportStatisticsRequest { date = "2020-03-27" }));
            Assert.AreEqual(
                "{\"offset\":0,\"limit\":100}",
                JsonSerializer.Serialize(new HealthGetReportJobIdsRequest { offset = 0, limit = 100 }));

            var statistics = Newtonsoft.Json.JsonConvert.DeserializeObject<HealthGetReportStatisticsResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"pv\":100,\"uv\":50}");
            var jobs = Newtonsoft.Json.JsonConvert.DeserializeObject<HealthGetReportJobIdsResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"ending\":1," +
                "\"jobids\":[\"jobid1\",\"jobid2\"]}");

            Assert.AreEqual(100, statistics.pv);
            Assert.AreEqual(50, statistics.uv);
            Assert.AreEqual(1, jobs.ending);
            Assert.AreEqual("jobid2", jobs.jobids[1]);
        }

        [TestMethod]
        public void JobAndAnswerModelsPreserveOfficialShapesLargeValuesAndComments()
        {
            var job = Newtonsoft.Json.JsonConvert.DeserializeObject<HealthGetReportJobInfoResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"job_info\":{" +
                "\"title\":\"职工收集任务\",\"creator\":\"creator_userid\",\"type\":1," +
                "\"apply_range\":{\"userids\":[\"userid1\"],\"partyids\":[4294967296]}," +
                "\"report_to\":{\"userids\":[\"userid2\"]},\"report_type\":1," +
                "\"skip_weekend\":0,\"finish_cnt\":10,\"question_templates\":[{" +
                "\"question_id\":1,\"title\":\"是否不适\",\"question_type\":2," +
                "\"is_required\":1,\"option_list\":[{\"option_id\":1," +
                "\"option_text\":\"有\"}]}]}}");
            var answers = Newtonsoft.Json.JsonConvert.DeserializeObject<HealthGetReportAnswerResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"answers\":[{" +
                "\"id_type\":1,\"userid\":\"userid2\",\"report_time\":5000000000," +
                "\"report_values\":[{\"question_id\":1,\"single_choice\":2},{" +
                "\"question_id\":2,\"text\":\"苏州市\"},{\"question_id\":3," +
                "\"multi_choice\":[1,3]},{\"question_id\":4," +
                "\"fileid\":[\"file-1\"]}]}]}");

            Assert.AreEqual(4294967296L, job.job_info.apply_range.partyids[0]);
            Assert.AreEqual(1, job.job_info.question_templates[0].is_required);
            Assert.AreEqual(5000000000L, answers.answers[0].report_time);
            Assert.AreEqual("file-1", answers.answers[0].report_values[3].fileid[0]);

            var modelSource = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "Health",
                "HealthJson.cs"));
            Assert.AreEqual(60, CountOccurrences(modelSource, "/// <summary>"));
            Assert.IsFalse(modelSource.Contains("object "));
        }

        private static int CountOccurrences(string value, string search)
        {
            return value.Split(new[] { search }, StringSplitOptions.None).Length - 1;
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
