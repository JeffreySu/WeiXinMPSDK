using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Asynchronous;
using WorkAsynchronousApi = Senparc.Weixin.Work.AdvancedAPIs.AsynchronousApi;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Asynchronous
{
    [TestClass]
    public class RobotUserIdConversionContractTests
    {
        [TestMethod]
        public void ApiExposesBothOfficialRobotConversionPathsAndTokenSemantics()
        {
            var methods = typeof(WorkAsynchronousApi)
                .GetMethods(BindingFlags.Public | BindingFlags.Static);

            AssertMethod<BatchOpenUserIdToUserIdRequest, BatchOpenUserIdToUserIdResult>(
                methods, nameof(WorkAsynchronousApi.BatchOpenUserIdToUserId));
            AssertAsyncMethod<BatchOpenUserIdToUserIdRequest, BatchOpenUserIdToUserIdResult>(
                methods, nameof(WorkAsynchronousApi.BatchOpenUserIdToUserIdAsync));
            AssertMethod<ServiceBatchUserIdToOpenUserIdRequest,
                ServiceBatchUserIdToOpenUserIdResult>(methods,
                nameof(WorkAsynchronousApi.ServiceBatchUserIdToOpenUserId));
            AssertAsyncMethod<ServiceBatchUserIdToOpenUserIdRequest,
                ServiceBatchUserIdToOpenUserIdResult>(methods,
                nameof(WorkAsynchronousApi.ServiceBatchUserIdToOpenUserIdAsync));

            Assert.AreEqual("/cgi-bin/batch/openuserid_to_userid",
                GetPath("BatchOpenUserIdToUserIdPath"));
            Assert.AreEqual("/cgi-bin/service/batch/userid_to_openuserid",
                GetPath("ServiceBatchUserIdToOpenUserIdPath"));

            var source = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Asynchronous",
                "AsynchronousApi.UserIdConversion.cs");
            Assert.AreEqual(2, CountOccurrences(source, "?provider_access_token={0}"));
            Assert.AreEqual(2, CountOccurrences(source, "document/path/101521"));
            Assert.AreEqual(2, CountOccurrences(source, "document/path/97062"));
            Assert.AreEqual(6, CountOccurrences(source,
                "jsonSetting: UserIdConversionJsonSetting"));
        }

        [TestMethod]
        public void SelfBuiltApplicationModelsPreserveOfficialPayload()
        {
            var requestJson = JsonSerializer.Serialize(new BatchOpenUserIdToUserIdRequest
            {
                open_userid_list = new[] { "xxx", "yyy" }
            });
            StringAssert.Contains(requestJson,
                "\"open_userid_list\":[\"xxx\",\"yyy\"]");

            var result = JsonSerializer.Deserialize<BatchOpenUserIdToUserIdResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"userid_list\":[{" +
                "\"open_userid\":\"xxx\",\"userid\":\"aaa\"}]," +
                "\"invalid_open_userid_list\":[\"yyy\"]}");

            Assert.IsNotNull(result);
            Assert.AreEqual("xxx", result.userid_list[0].open_userid);
            Assert.AreEqual("aaa", result.userid_list[0].userid);
            CollectionAssert.AreEqual(new[] { "yyy" },
                result.invalid_open_userid_list);
        }

        [TestMethod]
        public void ProviderModelsPreserveRobotAndOpenCorpFields()
        {
            var requestJson = JsonSerializer.Serialize(
                new ServiceBatchUserIdToOpenUserIdRequest
                {
                    open_userid_list = new[] { "wojigengoegeojgoe", "wosgjeiogng" },
                    source_botid = "BOTID"
                });
            StringAssert.Contains(requestJson, "\"source_botid\":\"BOTID\"");
            StringAssert.Contains(requestJson,
                "\"open_userid_list\":[\"wojigengoegeojgoe\",\"wosgjeiogng\"]");

            var result = JsonSerializer.Deserialize<ServiceBatchUserIdToOpenUserIdResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"items\":[{" +
                "\"userid\":\"woxxxx\",\"open_userid\":\"wonewxxxx\"}]," +
                "\"open_corpid\":\"wpxnigenogneg\"," +
                "\"invalid_open_userid_list\":[\"userid\"]}");

            Assert.IsNotNull(result);
            Assert.AreEqual("woxxxx", result.items[0].userid);
            Assert.AreEqual("wonewxxxx", result.items[0].open_userid);
            Assert.AreEqual("wpxnigenogneg", result.open_corpid);
            CollectionAssert.AreEqual(new[] { "userid" },
                result.invalid_open_userid_list);
        }

        [TestMethod]
        public void NewPublicModelsAndFieldsHaveXmlComments()
        {
            var source = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Asynchronous",
                "RobotUserIdConversionJson.cs");
            var declarations = Regex.Matches(source,
                    @"^\s*public class ", RegexOptions.Multiline).Count +
                Regex.Matches(source,
                    @"^\s*public [^\r\n]+ \{ get; set; \}$", RegexOptions.Multiline).Count;

            Assert.AreEqual(declarations, CountOccurrences(source, "/// <summary>"));
            Assert.IsFalse(source.Contains("object "));
        }

        private static void AssertMethod<TRequest, TResult>(MethodInfo[] methods,
            string name)
        {
            var method = methods.Single(item => item.Name == name);
            Assert.AreEqual(typeof(TResult), method.ReturnType);
            Assert.AreEqual(typeof(TRequest), method.GetParameters()[1].ParameterType);
        }

        private static void AssertAsyncMethod<TRequest, TResult>(MethodInfo[] methods,
            string name)
        {
            var method = methods.Single(item => item.Name == name);
            Assert.AreEqual(typeof(Task<TResult>), method.ReturnType);
            Assert.AreEqual(typeof(TRequest), method.GetParameters()[1].ParameterType);
        }

        private static object GetPath(string fieldName)
            => typeof(WorkAsynchronousApi).GetField(fieldName,
                BindingFlags.NonPublic | BindingFlags.Static)?.GetRawConstantValue();

        private static string ReadRepositoryFile(params string[] path)
            => File.ReadAllText(Path.Combine(
                new[] { FindRepositoryRoot() }.Concat(path).ToArray()));

        private static int CountOccurrences(string value, string search)
            => value.Split(new[] { search }, StringSplitOptions.None).Length - 1;

        private static string FindRepositoryRoot(
            [CallerFilePath] string sourceFilePath = null)
        {
            foreach (var startPath in new[]
            {
                Directory.GetCurrentDirectory(), AppContext.BaseDirectory,
                Path.GetDirectoryName(sourceFilePath)
            })
            {
                var directory = string.IsNullOrEmpty(startPath)
                    ? null
                    : new DirectoryInfo(startPath);
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
