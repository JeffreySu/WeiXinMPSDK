using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.LinkedCorp;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.LinkedCorp
{
    [TestClass]
    public class LinkedCorpContractTests
    {
        [TestMethod]
        public void LinkedCorpApiExposesFiveSynchronousAndAsynchronousEndpoints()
        {
            var methods = typeof(LinkedCorpApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var methodName in new[]
            {
                nameof(LinkedCorpApi.GetAgentPermissionList), nameof(LinkedCorpApi.GetUser),
                nameof(LinkedCorpApi.GetSimpleUserList), nameof(LinkedCorpApi.GetUserList),
                nameof(LinkedCorpApi.GetDepartmentList)
            })
            {
                CollectionAssert.Contains(methods, methodName);
                CollectionAssert.Contains(methods, methodName + "Async");
            }
        }

        [TestMethod]
        public void LinkedCorpApiUsesOfficialPathsAndDocumentationLinks()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "LinkedCorp", "LinkedCorpApi.cs"));

            foreach (var path in new[]
            {
                "/cgi-bin/linkedcorp/agent/get_perm_list", "/cgi-bin/linkedcorp/user/get",
                "/cgi-bin/linkedcorp/user/simplelist", "/cgi-bin/linkedcorp/user/list",
                "/cgi-bin/linkedcorp/department/list"
            })
            {
                Assert.AreEqual(1, CountOccurrences(source, path), path);
            }

            foreach (var documentId in new[] { "93168", "93169", "93170", "93171", "93172" })
            {
                Assert.AreEqual(2, CountOccurrences(source, "document/path/" + documentId), documentId);
            }

            Assert.AreEqual(11, CountOccurrences(source, "/// <summary>"));
        }

        [TestMethod]
        public void PermissionAndDepartmentModelsMatchOfficialSamples()
        {
            Assert.AreEqual("{}", JsonSerializer.Serialize(new LinkedCorpAgentPermissionListRequest()));
            Assert.AreEqual(
                "{\"department_id\":\"LINKEDID/DEPARTMENTID\"}",
                JsonSerializer.Serialize(new LinkedCorpDepartmentListRequest
                {
                    department_id = "LINKEDID/DEPARTMENTID"
                }));

            var permission = Newtonsoft.Json.JsonConvert.DeserializeObject<LinkedCorpAgentPermissionListResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"userids\":[\"CORPID/USERID\"]," +
                "\"department_ids\":[\"LINKEDID/DEPARTMENTID\"]}");
            var departments = Newtonsoft.Json.JsonConvert.DeserializeObject<LinkedCorpDepartmentListResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"department_list\":[{" +
                "\"department_id\":\"4294967296\",\"department_name\":\"测试部门\"," +
                "\"parentid\":\"1\",\"order\":5000000000}]}" );

            Assert.AreEqual("CORPID/USERID", permission.userids[0]);
            Assert.AreEqual("LINKEDID/DEPARTMENTID", permission.department_ids[0]);
            Assert.AreEqual(4294967296L, departments.department_list[0].department_id);
            Assert.AreEqual(5000000000L, departments.department_list[0].order);
        }

        [TestMethod]
        public void UserModelsMatchOfficialSamplesAndHaveCompleteComments()
        {
            Assert.AreEqual(
                "{\"userid\":\"CORPID/USERID\"}",
                JsonSerializer.Serialize(new LinkedCorpUserGetRequest { userid = "CORPID/USERID" }));
            Assert.AreEqual(
                "{\"department_id\":\"LINKEDID/DEPARTMENTID\",\"fetch_child\":true}",
                JsonSerializer.Serialize(new LinkedCorpUserListRequest
                {
                    department_id = "LINKEDID/DEPARTMENTID",
                    fetch_child = true
                }));

            var simpleList = Newtonsoft.Json.JsonConvert.DeserializeObject<LinkedCorpSimpleUserListResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"userlist\":[{" +
                "\"userid\":\"zhangsan\",\"name\":\"张三\",\"department\":[\"LINKEDID/1\"]," +
                "\"corpid\":\"wwcorp\"}]}" );
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<LinkedCorpUserGetResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"user_info\":{" +
                "\"userid\":\"zhangsan\",\"name\":\"张三\",\"department\":[\"LINKEDID/1\"]," +
                "\"mobile\":\"+86 12345678901\",\"telephone\":\"10086\"," +
                "\"email\":\"zhangsan@example.com\",\"position\":\"开发\",\"corpid\":\"wwcorp\"," +
                "\"extattr\":{\"attrs\":[{\"name\":\"文本\",\"type\":0," +
                "\"text\":{\"value\":\"value\"}},{\"name\":\"网页\",\"type\":1," +
                "\"web\":{\"url\":\"https://work.weixin.qq.com/\",\"title\":\"官网\"}}]}}}");

            Assert.AreEqual("wwcorp", simpleList.userlist[0].corpid);
            Assert.AreEqual("LINKEDID/1", simpleList.userlist[0].department[0]);
            Assert.AreEqual("+86 12345678901", user.user_info.mobile);
            Assert.AreEqual("value", user.user_info.extattr.attrs[0].text.value);
            Assert.AreEqual("官网", user.user_info.extattr.attrs[1].web.title);

            var modelSource = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "LinkedCorp", "LinkedCorpJson.cs"));
            Assert.AreEqual(47, CountOccurrences(modelSource, "/// <summary>"));
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
