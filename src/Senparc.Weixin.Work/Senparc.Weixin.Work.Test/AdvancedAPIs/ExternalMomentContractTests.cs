using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.External;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
{
    [TestClass]
    public class ExternalMomentContractTests
    {
        [TestMethod]
        public void MomentApisExposeSyncAndAsyncEntrypoints()
        {
            var methodNames = typeof(ExternalApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var methodName in new[]
            {
                nameof(ExternalApi.CreateMomentTask), nameof(ExternalApi.GetMomentTaskCreateResult),
                nameof(ExternalApi.GetMomentCustomerList), nameof(ExternalApi.GetMomentSendResult),
                nameof(ExternalApi.GetMomentComments), nameof(ExternalApi.ListMomentStrategies),
                nameof(ExternalApi.GetMomentStrategy), nameof(ExternalApi.GetMomentStrategyRange),
                nameof(ExternalApi.CreateMomentStrategy), nameof(ExternalApi.EditMomentStrategy),
                nameof(ExternalApi.DeleteMomentStrategy)
            })
            {
                CollectionAssert.Contains(methodNames, methodName, methodName);
                CollectionAssert.Contains(methodNames, methodName + "Async", methodName + "Async");
            }
        }

        [TestMethod]
        public void MomentApisUseAllOfficialPaths()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "External", "ExternalMomentApi.cs"));

            foreach (var path in new[]
            {
                "/cgi-bin/externalcontact/add_moment_task",
                "/cgi-bin/externalcontact/get_moment_task_result",
                "/cgi-bin/externalcontact/get_moment_customer_list",
                "/cgi-bin/externalcontact/get_moment_send_result",
                "/cgi-bin/externalcontact/get_moment_comments",
                "/cgi-bin/externalcontact/moment_strategy/list",
                "/cgi-bin/externalcontact/moment_strategy/get",
                "/cgi-bin/externalcontact/moment_strategy/get_range",
                "/cgi-bin/externalcontact/moment_strategy/create",
                "/cgi-bin/externalcontact/moment_strategy/edit",
                "/cgi-bin/externalcontact/moment_strategy/del"
            })
            {
                Assert.AreEqual(2, CountOccurrences(source, path + "\""), path);
            }
        }

        [TestMethod]
        public void MomentTaskModelsMatchCurrentOfficialContract()
        {
            var json = JsonSerializer.Serialize(new CreateMomentTaskRequest
            {
                text = new MomentTaskText { content = "hello" },
                attachments = new[]
                {
                    new MomentTaskAttachment
                    {
                        msgtype = "image", image = new MomentTaskImage { media_id = "media-1" }
                    }
                },
                visible_range = new MomentTaskVisibleRange
                {
                    sender_list = new MomentTaskSenderList
                    {
                        user_list = new[] { "user-1" }, department_list = new[] { 4294967296L }
                    }
                }
            });
            var result = JsonSerializer.Deserialize<MomentTaskCreateStatusResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"status\":3,\"type\":\"add_moment_task\"," +
                "\"result\":{\"errcode\":0,\"errmsg\":\"ok\",\"moment_id\":\"moment-1\"," +
                "\"invalid_sender_list\":{\"department_list\":[4294967296]}}}");

            StringAssert.Contains(json, "\"department_list\":[4294967296]");
            StringAssert.Contains(json, "\"msgtype\":\"image\"");
            Assert.AreEqual(3, result.status);
            Assert.AreEqual("moment-1", result.result.moment_id);
            Assert.AreEqual(4294967296L, result.result.invalid_sender_list.department_list[0]);
        }

        [TestMethod]
        public void MomentInteractionAndStrategyModelsPreserveLargeValues()
        {
            var comments = JsonSerializer.Deserialize<MomentCommentsResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"comment_list\":[{\"external_userid\":\"external-1\"," +
                "\"create_time\":4294967296}],\"like_list\":[{\"userid\":\"user-1\",\"create_time\":4294967297}]}");
            var strategy = JsonSerializer.Deserialize<MomentStrategyResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"strategy\":{\"strategy_id\":4294967298," +
                "\"parent_id\":4294967296,\"create_time\":4294967299," +
                "\"privilege\":{\"send_moment\":false}}}");

            Assert.AreEqual(4294967296L, comments.comment_list[0].create_time);
            Assert.AreEqual(4294967297L, comments.like_list[0].create_time);
            Assert.AreEqual(4294967298L, strategy.strategy.strategy_id);
            Assert.IsFalse(strategy.strategy.privilege.send_moment.Value);
        }

        private static int CountOccurrences(string value, string search)
        {
            return value.Split(new[] { search }, StringSplitOptions.None).Length - 1;
        }

        private static string FindRepositoryRoot([CallerFilePath] string sourceFilePath = null)
        {
            foreach (var startPath in new[]
            {
                Directory.GetCurrentDirectory(), AppContext.BaseDirectory, Path.GetDirectoryName(sourceFilePath)
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
