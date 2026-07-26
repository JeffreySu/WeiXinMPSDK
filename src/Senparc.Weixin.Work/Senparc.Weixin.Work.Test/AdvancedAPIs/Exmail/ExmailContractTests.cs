using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Exmail;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.MessageHandlers;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Exmail
{
    [TestClass]
    public class ExmailContractTests
    {
        [TestMethod]
        public void ExmailApiContainsTwentyFourSyncAndAsyncEntries()
        {
            var methodNames = typeof(ExmailApi)
                .GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(method => method.Name).ToArray();
            var syncMethodNames = new[]
            {
                nameof(ExmailApi.SendAppMail),
                nameof(ExmailApi.GetAppMailList),
                nameof(ExmailApi.ReadAppMail),
                nameof(ExmailApi.UpdateAppEmailAlias),
                nameof(ExmailApi.GetAppEmailAlias),
                nameof(ExmailApi.CreateGroup),
                nameof(ExmailApi.UpdateGroup),
                nameof(ExmailApi.DeleteGroup),
                nameof(ExmailApi.SearchGroups),
                nameof(ExmailApi.GetGroup),
                nameof(ExmailApi.CreatePublicMail),
                nameof(ExmailApi.UpdatePublicMail),
                nameof(ExmailApi.DeletePublicMail),
                nameof(ExmailApi.SearchPublicMail),
                nameof(ExmailApi.GetPublicMail),
                nameof(ExmailApi.GetPublicMailAuthCodeList),
                nameof(ExmailApi.DeletePublicMailAuthCode),
                nameof(ExmailApi.ActivateEmailAccount),
                nameof(ExmailApi.GetUserOptions),
                nameof(ExmailApi.UpdateUserOptions),
                nameof(ExmailApi.GetNewMailCount),
                nameof(ExmailApi.AddVipAccounts),
                nameof(ExmailApi.RemoveVipAccounts),
                nameof(ExmailApi.GetVipAccountList)
            };

            Assert.AreEqual(48, methodNames.Length);
            foreach (var syncMethodName in syncMethodNames)
            {
                CollectionAssert.Contains(methodNames, syncMethodName, syncMethodName);
                CollectionAssert.Contains(methodNames, syncMethodName + "Async", syncMethodName + "Async");
            }
        }

        [TestMethod]
        public void ExmailApiUsesTwentyFourCurrentPathsAndHttpMethods()
        {
            var directory = Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Exmail");
            var source = string.Join(Environment.NewLine,
                Directory.GetFiles(directory, "ExmailApi*.cs").Select(File.ReadAllText));
            var paths = new[]
            {
                "/cgi-bin/exmail/app/compose_send",
                "/cgi-bin/exmail/app/get_mail_list",
                "/cgi-bin/exmail/app/read_mail",
                "/cgi-bin/exmail/app/update_email_alias",
                "/cgi-bin/exmail/app/get_email_alias",
                "/cgi-bin/exmail/group/create",
                "/cgi-bin/exmail/group/update",
                "/cgi-bin/exmail/group/delete",
                "/cgi-bin/exmail/group/search",
                "/cgi-bin/exmail/group/get",
                "/cgi-bin/exmail/publicmail/create",
                "/cgi-bin/exmail/publicmail/update",
                "/cgi-bin/exmail/publicmail/delete",
                "/cgi-bin/exmail/publicmail/search",
                "/cgi-bin/exmail/publicmail/get",
                "/cgi-bin/exmail/publicmail/get_auth_code_list",
                "/cgi-bin/exmail/publicmail/delete_auth_code",
                "/cgi-bin/exmail/account/act_email",
                "/cgi-bin/exmail/useroption/get",
                "/cgi-bin/exmail/useroption/update",
                "/cgi-bin/exmail/mail/get_newcount",
                "/cgi-bin/exmail/vip/batch_add",
                "/cgi-bin/exmail/vip/batch_del",
                "/cgi-bin/exmail/vip/list"
            };

            foreach (var path in paths)
            {
                StringAssert.Contains(source, path);
            }

            Assert.AreEqual(24, paths.Distinct().Count());
            StringAssert.Contains(source, "CommonJsonSendType.POST");
            StringAssert.Contains(source, "CommonJsonSendType.GET");
        }

        [TestMethod]
        public void ComposeRequestPreservesRecipientsScheduleMeetingAndLargeTimestamps()
        {
            var request = new ExmailComposeSendRequest
            {
                to = new ExmailRecipient
                {
                    emails = new List<string> { "external@example.com" },
                    userids = new List<string> { "zhangsan" }
                },
                cc = new ExmailRecipient { userids = new List<string> { "lisi" } },
                subject = "release",
                content = "body",
                content_type = "text/html",
                attachment_list = new List<ExmailAttachment>
                {
                    new ExmailAttachment { file_name = "a.txt", content = "BASE64" }
                },
                schedule = new ExmailSchedule
                {
                    method = "request",
                    start_time = 5178368698L,
                    end_time = 5178372298L,
                    location = "Shenzhen",
                    reminders = new ExmailScheduleReminder
                    {
                        is_remind = 1,
                        remind_before_event_mins = 15,
                        is_repeat = 1,
                        repeat_day_of_week = new List<int> { 4, 5 },
                        repeat_until = 6178368698L
                    }
                },
                meeting = new ExmailMeeting
                {
                    option = new ExmailMeetingOption
                    {
                        enable_waiting_room = true,
                        water_mark_type = 1
                    },
                    hosts = new ExmailUserIdList { userids = new List<string> { "zhangsan" } },
                    meeting_admins = new ExmailUserIdList { userids = new List<string> { "lisi" } }
                },
                enable_id_trans = 1
            };

            var json = JsonSerializer.Serialize(request);

            StringAssert.Contains(json, "\"emails\":[\"external@example.com\"]");
            StringAssert.Contains(json, "\"userids\":[\"zhangsan\"]");
            StringAssert.Contains(json, "\"attachment_list\"");
            StringAssert.Contains(json, "\"start_time\":5178368698");
            StringAssert.Contains(json, "\"repeat_until\":6178368698");
            StringAssert.Contains(json, "\"meeting_admins\"");
            StringAssert.Contains(json, "\"enable_id_trans\":1");
        }

        [TestMethod]
        public void GroupModelsAndGetQueriesPreserveNestedListsAndEncoding()
        {
            var request = new ExmailGroupRequest
            {
                groupid = "group@example.com",
                groupname = "研发组",
                email_list = new ExmailStringList { list = new List<string> { "member@example.com" } },
                group_list = new ExmailStringList { list = new List<string> { "all@example.com" } },
                tag_list = new ExmailIntList { list = new List<int> { 2, 5 } },
                department_list = new ExmailLongList { list = new List<long> { 5178368698L } },
                allow_type = 3
            };
            var json = JsonSerializer.Serialize(request);
            var buildSearchUrl = typeof(ExmailApi).GetMethod("BuildGroupSearchUrl",
                BindingFlags.NonPublic | BindingFlags.Static);
            var searchUrl = (string)buildSearchUrl.Invoke(null,
                new object[] { "group+研发@example.com", true });

            StringAssert.Contains(json, "\"email_list\":{\"list\":[\"member@example.com\"]}");
            StringAssert.Contains(json, "\"tag_list\":{\"list\":[2,5]}");
            StringAssert.Contains(json, "\"department_list\":{\"list\":[5178368698]}");
            StringAssert.Contains(searchUrl, "fuzzy=1");
            StringAssert.Contains(searchUrl, "groupid=group%2B");
            StringAssert.Contains(searchUrl, "%40example.com");
        }

        [TestMethod]
        public void PublicMailModelsPreserveScopeAliasAuthCodeAndLargeTimestamps()
        {
            var request = new ExmailPublicMailUpdateRequest
            {
                id = 12,
                name = "support",
                userid_list = new ExmailStringList { list = new List<string> { "zhangsan" } },
                department_list = new ExmailLongList { list = new List<long> { 5178368698L } },
                alias_list = new ExmailStringList { list = new List<string> { "help@example.com" } },
                create_auth_code = 1,
                auth_code_info = new ExmailAuthCodeInfo { remark = "desktop" }
            };
            var json = JsonSerializer.Serialize(request);
            var result = JsonSerializer.Deserialize<ExmailAuthCodeListResult>(
                "{\"errcode\":0,\"auth_code_list\":[{\"auth_code_id\":7," +
                "\"remark\":\"desktop\",\"last_use_time\":5178368799," +
                "\"create_time\":5178368698}]}" );

            StringAssert.Contains(json, "\"userid_list\":{\"list\":[\"zhangsan\"]}");
            StringAssert.Contains(json, "\"alias_list\":{\"list\":[\"help@example.com\"]}");
            StringAssert.Contains(json, "\"create_auth_code\":1");
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.auth_code_list[0].auth_code_id);
            Assert.AreEqual(5178368799L, result.auth_code_list[0].last_use_time);
            Assert.AreEqual(5178368698L, result.auth_code_list[0].create_time);
        }

        [TestMethod]
        public void MailListAndUserOptionsPreserveOfficialIntegerAndNestedFields()
        {
            var mailList = JsonSerializer.Deserialize<ExmailAppMailListResult>(
                "{\"errcode\":0,\"mail_list\":[{\"mail_id\":\"mail-1\"}]," +
                "\"has_more\":1,\"next_cursor\":\"cursor-2\"}" );
            var options = JsonSerializer.Deserialize<ExmailUserOptionsResult>(
                "{\"errcode\":0,\"option\":{\"list\":[{\"type\":1,\"value\":\"0\"}," +
                "{\"type\":2,\"value\":\"1\"}]}}" );
            var accountJson = JsonSerializer.Serialize(new ExmailActivateAccountRequest
            {
                userid = "zhangsan",
                publicemail_id = 12,
                type = 1
            });

            Assert.IsNotNull(mailList);
            Assert.AreEqual("mail-1", mailList.mail_list[0].mail_id);
            Assert.AreEqual(1, mailList.has_more);
            Assert.AreEqual("cursor-2", mailList.next_cursor);
            Assert.IsNotNull(options);
            Assert.AreEqual("1", options.option.list[1].value);
            StringAssert.Contains(accountJson, "\"publicemail_id\":12");
        }

        [TestMethod]
        public void VipModelsPreserveBatchResultsBooleanPaginationAndLimits()
        {
            var requestJson = JsonSerializer.Serialize(new ExmailVipBatchRequest
            {
                userid_list = new List<string> { "zhangsan", "lisi", "wangwu" }
            });
            var batchResult = JsonSerializer.Deserialize<ExmailVipBatchResult>(
                "{\"errcode\":0,\"succ_userid_list\":[\"zhangsan\",\"lisi\"]," +
                "\"fail_userid_list\":[\"wangwu\"]}");
            var listRequestJson = JsonSerializer.Serialize(new ExmailVipListRequest
            {
                cursor = "CURSOR",
                limit = 200
            });
            var listResult = JsonSerializer.Deserialize<ExmailVipListResult>(
                "{\"errcode\":0,\"has_more\":true," +
                "\"next_cursor\":\"GNIJIGEO\"," +
                "\"userid_list\":[\"zhangsan\",\"lisi\"]}");

            StringAssert.Contains(requestJson,
                "\"userid_list\":[\"zhangsan\",\"lisi\",\"wangwu\"]");
            Assert.AreEqual("wangwu", batchResult.fail_userid_list[0]);
            StringAssert.Contains(listRequestJson, "\"limit\":200");
            Assert.IsTrue(listResult.has_more);
            Assert.AreEqual("GNIJIGEO", listResult.next_cursor);
            Assert.AreEqual("lisi", listResult.userid_list[1]);
        }

        [TestMethod]
        public void VipSurfaceUsesCurrentOfficialDocumentsAndCompleteXmlComments()
        {
            var root = FindRepositoryRoot();
            var apiSource = File.ReadAllText(Path.Combine(root, "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "Exmail", "ExmailApi.Vip.cs"));
            var modelSource = File.ReadAllText(Path.Combine(root, "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "Exmail", "ExmailVipJson.cs"));

            foreach (var documentId in new[] { "99316", "99317", "99318" })
            {
                StringAssert.Contains(apiSource, "/document/path/" + documentId);
            }

            Assert.AreEqual(7, apiSource.Split(new[] { "/// <summary>" },
                StringSplitOptions.None).Length - 1);
            Assert.AreEqual(12, modelSource.Split(new[] { "/// <summary>" },
                StringSplitOptions.None).Length - 1);
            Assert.IsFalse(apiSource.Contains("object "));
            Assert.IsFalse(apiSource.Contains("dynamic "));
            Assert.IsFalse(modelSource.Contains("object "));
            Assert.IsFalse(modelSource.Contains("dynamic "));

            var addMethod = typeof(ExmailApi).GetMethod(
                nameof(ExmailApi.AddVipAccounts));
            var removeMethod = typeof(ExmailApi).GetMethod(
                nameof(ExmailApi.RemoveVipAccounts));
            var listMethod = typeof(ExmailApi).GetMethod(
                nameof(ExmailApi.GetVipAccountList));
            Assert.AreEqual(typeof(ExmailVipBatchRequest),
                addMethod.GetParameters()[1].ParameterType);
            Assert.AreEqual(typeof(ExmailVipBatchResult),
                removeMethod.ReturnType);
            Assert.AreEqual(typeof(ExmailVipListResult), listMethod.ReturnType);
        }

        [TestMethod]
        public void EmailCallbacksMapToContextAndHandlerExtensions()
        {
            var repositoryRoot = FindRepositoryRoot();
            var publicMailSource = File.ReadAllText(Path.Combine(repositoryRoot, "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "Entities", "Request", "Event",
                "RequestMessageEvent_Public_Email_Change.cs"));
            StringAssert.Contains(publicMailSource, "/document/path/100180");
            Assert.AreEqual(5, publicMailSource.Split(new[] { "/// <summary>" },
                StringSplitOptions.None).Length - 1);

            var document = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>5178368698</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[app_email_change]]></Event>
<ChangeType><![CDATA[receive_email]]></ChangeType>
<Amount>2</Amount>
</xml>");

            var request = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), document) as
                RequestMessageEvent_App_Email_Change;
            var syncHandler = typeof(WorkMessageHandler<>).GetMethod("OnEvent_AppEmailChangeRequest");
            var asyncHandler = typeof(WorkMessageHandler<>).GetMethod("OnEvent_AppEmailChangeRequestAsync");

            Assert.IsNotNull(request);
            Assert.AreEqual(Event.app_email_change, request.Event);
            Assert.AreEqual("receive_email", request.ChangeType);
            Assert.AreEqual(2, request.Amount);
            Assert.IsNotNull(syncHandler);
            Assert.IsNotNull(asyncHandler);

            var publicMailDocument = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>5178368698</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[public_email_change]]></Event>
<ChangeType><![CDATA[receive_email]]></ChangeType>
<Id>1</Id>
<Amount>3</Amount>
</xml>");
            var publicMailRequest = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), publicMailDocument) as
                RequestMessageEvent_Public_Email_Change;
            var publicMailSyncHandler = typeof(WorkMessageHandler<>).GetMethod(
                "OnEvent_PublicEmailChangeRequest");
            var publicMailAsyncHandler = typeof(WorkMessageHandler<>).GetMethod(
                "OnEvent_PublicEmailChangeRequestAsync");

            Assert.IsNotNull(publicMailRequest);
            Assert.AreEqual(Event.public_email_change, publicMailRequest.Event);
            Assert.AreEqual("receive_email", publicMailRequest.ChangeType);
            Assert.AreEqual(1, publicMailRequest.Id);
            Assert.AreEqual(3, publicMailRequest.Amount);
            Assert.IsNotNull(publicMailSyncHandler);
            Assert.IsNotNull(publicMailAsyncHandler);
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
