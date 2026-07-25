using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
{
    [TestClass]
    public class ContactOfficialContractTests
    {
        private static readonly string[] OfficialApiPaths =
        {
            "/cgi-bin/user/create",
            "/cgi-bin/user/get",
            "/cgi-bin/user/update",
            "/cgi-bin/user/delete",
            "/cgi-bin/user/batchdelete",
            "/cgi-bin/user/simplelist",
            "/cgi-bin/user/list",
            "/cgi-bin/user/convert_to_openid",
            "/cgi-bin/user/convert_to_userid",
            "/cgi-bin/user/authsucc",
            "/cgi-bin/batch/invite",
            "/cgi-bin/corp/get_join_qrcode",
            "/cgi-bin/user/getuserid",
            "/cgi-bin/user/get_userid_by_email",
            "/cgi-bin/user/list_id",
            "/cgi-bin/department/create",
            "/cgi-bin/department/update",
            "/cgi-bin/department/delete",
            "/cgi-bin/department/list",
            "/cgi-bin/department/simplelist",
            "/cgi-bin/department/get",
            "/cgi-bin/tag/create",
            "/cgi-bin/tag/update",
            "/cgi-bin/tag/delete",
            "/cgi-bin/tag/get",
            "/cgi-bin/tag/addtagusers",
            "/cgi-bin/tag/deltagusers",
            "/cgi-bin/tag/list",
            "/cgi-bin/contactrule/create",
            "/cgi-bin/contactrule/list",
            "/cgi-bin/contactrule/update",
            "/cgi-bin/contactrule/delete",
            "/cgi-bin/batch/syncuser",
            "/cgi-bin/batch/replaceuser",
            "/cgi-bin/batch/replaceparty",
            "/cgi-bin/batch/getresult",
            "/cgi-bin/export/simple_user",
            "/cgi-bin/export/user",
            "/cgi-bin/export/department",
            "/cgi-bin/export/taguser",
            "/cgi-bin/export/get_result"
        };

        [TestMethod]
        public void AllFortyOneOfficialContactApiPathsAreMapped()
        {
            Assert.AreEqual(41, OfficialApiPaths.Length);
            Assert.AreEqual(OfficialApiPaths.Length, OfficialApiPaths.Distinct().Count());

            var project = Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work");
            var sourceFiles = new[]
            {
                Path.Combine(project, "AdvancedAPIs", "MailList"),
                Path.Combine(project, "AdvancedAPIs", "Contact"),
                Path.Combine(project, "AdvancedAPIs", "Asynchronous"),
                Path.Combine(project, "CommonAPIs")
            }.SelectMany(directory => Directory.GetFiles(directory, "*.cs", SearchOption.AllDirectories));
            var source = string.Join("\n", sourceFiles.Select(File.ReadAllText));

            foreach (var path in OfficialApiPaths)
            {
                StringAssert.Contains(source, path, path);
            }
        }

        [TestMethod]
        public void CurrentContactGapsExposeSyncAndAsyncEntrypoints()
        {
            var methods = typeof(MailListApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            CollectionAssert.Contains(methods, nameof(MailListApi.GetJoinQrcode));
            CollectionAssert.Contains(methods, nameof(MailListApi.GetJoinQrcodeAsync));
            CollectionAssert.Contains(methods, nameof(MailListApi.GetDepartment));
            CollectionAssert.Contains(methods, nameof(MailListApi.GetDepartmentAsync));
        }

        [TestMethod]
        public void CurrentContactModelsPreserveUrlsLeadersAndLargeDepartmentIds()
        {
            var qrCode = JsonSerializer.Deserialize<GetJoinQrcodeResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"join_qrcode\":\"https://work.weixin.qq.com/join?a=1&amp;b=2\"}");
            var department = JsonSerializer.Deserialize<GetDepartmentResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"department\":{\"id\":4294967296," +
                "\"name\":\"研发中心\",\"name_en\":\"RD\",\"department_leader\":[\"zhangsan\",\"lisi\"]," +
                "\"parentid\":4294967297,\"order\":4294967298}}");

            Assert.AreEqual("https://work.weixin.qq.com/join?a=1&amp;b=2", qrCode.join_qrcode);
            Assert.AreEqual(4294967296L, department.department.id);
            Assert.AreEqual(4294967297L, department.department.parentid);
            Assert.AreEqual(4294967298L, department.department.order);
            Assert.AreEqual("lisi", department.department.department_leader[1]);
        }

        [TestMethod]
        public void ContactChangeCallbacksAndExportJobReuseStrongExistingRoutes()
        {
            var mappings = new Dictionary<string, Type>
            {
                ["create_user"] = typeof(RequestMessageEvent_Change_Contact_User_Create),
                ["update_user"] = typeof(RequestMessageEvent_Change_Contact_User_Update),
                ["delete_user"] = typeof(RequestMessageEvent_Change_Contact_User_Base),
                ["create_party"] = typeof(RequestMessageEvent_Change_Contact_Party_Create),
                ["update_party"] = typeof(RequestMessageEvent_Change_Contact_Party_Update),
                ["delete_party"] = typeof(RequestMessageEvent_Change_Contact_Party_Base),
                ["update_tag"] = typeof(RequestMessageEvent_Change_Contact_Tag_Update)
            };

            foreach (var mapping in mappings)
            {
                var callback = RequestMessageFactory.GetRequestEntity(
                    new MessageContexts.DefaultWorkMessageContext(), CreateContactCallback(mapping.Key));
                Assert.IsInstanceOfType(callback, mapping.Value, mapping.Key);
            }

            var exportCallback = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), XDocument.Parse(@"<xml>
<ToUserName><![CDATA[ww-corp]]></ToUserName><FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>1425284517</CreateTime><MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[batch_job_result]]></Event><BatchJob>
<JobId><![CDATA[job-id]]></JobId><JobType><![CDATA[export_user]]></JobType>
<ErrCode>0</ErrCode><ErrMsg><![CDATA[ok]]></ErrMsg></BatchJob></xml>"))
                as RequestMessageEvent_Batch_Job_Result;
            var methods = typeof(Senparc.Weixin.Work.MessageHandlers.WorkMessageHandler<>)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance).Select(method => method.Name).ToArray();

            Assert.IsNotNull(exportCallback);
            Assert.AreEqual("export_user", exportCallback.BatchJob.JobType);
            CollectionAssert.Contains(methods, "OnEvent_BatchJobResultRequest");
            CollectionAssert.Contains(methods, "OnEvent_BatchJobResultRequestAsync");
        }

        private static XDocument CreateContactCallback(string changeType)
        {
            return XDocument.Parse($@"<xml>
<ToUserName><![CDATA[ww-corp]]></ToUserName><FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>1403610513</CreateTime><MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[change_contact]]></Event><ChangeType><![CDATA[{changeType}]]></ChangeType>
<UserID><![CDATA[zhangsan]]></UserID><Id>4294967296</Id><TagId>4294967296</TagId>
</xml>");
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
