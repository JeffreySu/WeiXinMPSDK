using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.School;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.MessageHandlers;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.School
{
    [TestClass]
    public class SchoolContractTests
    {
        [TestMethod]
        public void SchoolApiPreservesFirstTenSyncAndAsyncEntries()
        {
            var methodNames = typeof(SchoolApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var syncMethodName in new[]
            {
                nameof(SchoolApi.GetSubscribeQrCode),
                nameof(SchoolApi.SetSubscribeMode),
                nameof(SchoolApi.GetSubscribeMode),
                nameof(SchoolApi.SendNotification),
                nameof(SchoolApi.ConvertToOpenId),
                nameof(SchoolApi.CreateDepartment),
                nameof(SchoolApi.UpdateDepartment),
                nameof(SchoolApi.DeleteDepartment),
                nameof(SchoolApi.GetDepartmentList),
                nameof(SchoolApi.SetUpgradeInfo)
            })
            {
                CollectionAssert.Contains(methodNames, syncMethodName, syncMethodName);
                CollectionAssert.Contains(methodNames, syncMethodName + "Async", syncMethodName + "Async");
            }

            Assert.IsTrue(typeof(SchoolApi).GetMethods(BindingFlags.Public | BindingFlags.Static).Length >= 20);
        }

        [TestMethod]
        public void SchoolApiUsesOfficialPathsAndHttpMethods()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "School", "SchoolApi.cs"));

            foreach (var path in new[]
            {
                "/cgi-bin/externalcontact/get_subscribe_qr_code",
                "/cgi-bin/externalcontact/set_subscribe_mode",
                "/cgi-bin/externalcontact/get_subscribe_mode",
                "/cgi-bin/externalcontact/message/send",
                "/cgi-bin/externalcontact/convert_to_openid",
                "/cgi-bin/school/department/create",
                "/cgi-bin/school/department/update",
                "/cgi-bin/school/department/delete",
                "/cgi-bin/school/department/list",
                "/cgi-bin/school/set_upgrade_info"
            })
            {
                StringAssert.Contains(source, path, path);
            }

            StringAssert.Contains(source, "CommonJsonSendType.GET");
            StringAssert.Contains(source, "CommonJsonSendType.POST");
            StringAssert.Contains(source, "\"id=\" + departmentId");
        }

        [TestMethod]
        public void SubscribeAndOpenIdModelsUseOfficialFields()
        {
            var modeJson = JsonSerializer.Serialize(new SchoolSubscribeModeRequest { subscribe_mode = 1 });
            var convertJson = JsonSerializer.Serialize(new SchoolConvertToOpenIdRequest
            {
                external_userid = "wm_external"
            });
            var qrCode = JsonSerializer.Deserialize<SchoolSubscribeQrCodeResult>(
                "{\"errcode\":0,\"qrcode_big\":\"big\",\"qrcode_middle\":\"middle\"," +
                "\"qrcode_thumb\":\"thumb\"}");
            var openId = JsonSerializer.Deserialize<SchoolConvertToOpenIdResult>(
                "{\"errcode\":0,\"openid\":\"o123\"}");

            StringAssert.Contains(modeJson, "\"subscribe_mode\":1");
            StringAssert.Contains(convertJson, "\"external_userid\":\"wm_external\"");
            Assert.IsNotNull(qrCode);
            Assert.AreEqual("middle", qrCode.qrcode_middle);
            Assert.IsNotNull(openId);
            Assert.AreEqual("o123", openId.openid);
        }

        [TestMethod]
        public void NotificationModelsPreserveRecipientsContentAndLargeDepartmentIds()
        {
            var requestJson = JsonSerializer.Serialize(new SchoolNotificationRequest
            {
                to_parent_userid = new List<string> { "parent-1" },
                to_student_userid = new List<string> { "student-1" },
                to_party = new List<long> { 5178368698L },
                toall = 0,
                msgtype = "miniprogram",
                miniprogram = new SchoolMiniProgramMessage
                {
                    appid = "wx123",
                    pagepath = "pages/index",
                    title = "作业通知",
                    thumb_media_id = "media-1"
                },
                agentid = 1000002,
                enable_id_trans = 1,
                enable_duplicate_check = 1,
                duplicate_check_interval = 1800
            });
            var result = JsonSerializer.Deserialize<SchoolNotificationResult>(
                "{\"errcode\":0,\"invalid_external_user\":[\"wm_bad\"]," +
                "\"invalid_parent_userid\":[\"p_bad\"],\"invalid_student_userid\":[\"s_bad\"]," +
                "\"invalid_party\":[5178368698]}");

            StringAssert.Contains(requestJson, "\"to_parent_userid\":[\"parent-1\"]");
            StringAssert.Contains(requestJson, "\"to_party\":[5178368698]");
            StringAssert.Contains(requestJson, "\"pagepath\":\"pages/index\"");
            StringAssert.Contains(requestJson, "\"agentid\":1000002");
            StringAssert.Contains(requestJson, "\"enable_duplicate_check\":1");
            Assert.IsNotNull(result);
            Assert.AreEqual(5178368698L, result.invalid_party[0]);
        }

        [TestMethod]
        public void DepartmentRequestsPreserveOfficialFieldsAnd64BitIds()
        {
            var createJson = JsonSerializer.Serialize(new SchoolDepartmentCreateRequest
            {
                id = 5178368698L,
                name = "三年级一班",
                type = 2,
                parentid = 5178368799L,
                standard_grade = 3,
                register_year = 2024,
                order = 5178368899L,
                department_admins = new List<SchoolDepartmentAdministrator>
                {
                    new SchoolDepartmentAdministrator { userid = "teacher-1", type = 1, subject = "数学" }
                }
            });
            var updateJson = JsonSerializer.Serialize(new SchoolDepartmentUpdateRequest
            {
                id = 5178368698L,
                new_id = 5178368999L,
                department_admins = new List<SchoolDepartmentAdministratorOperation>
                {
                    new SchoolDepartmentAdministratorOperation
                    {
                        op = 1,
                        userid = "teacher-2",
                        type = 2,
                        subject = "英语"
                    }
                }
            });
            var upgradeJson = JsonSerializer.Serialize(new SchoolUpgradeInfoRequest
            {
                upgrade_switch = 1,
                upgrade_time = 5178369099L
            });

            StringAssert.Contains(createJson, "\"id\":5178368698");
            StringAssert.Contains(createJson, "\"parentid\":5178368799");
            StringAssert.Contains(createJson, "\"department_admins\":[");
            StringAssert.Contains(updateJson, "\"new_id\":5178368999");
            StringAssert.Contains(updateJson, "\"op\":1");
            StringAssert.Contains(upgradeJson, "\"upgrade_switch\":1");
            StringAssert.Contains(upgradeJson, "\"upgrade_time\":5178369099");
        }

        [TestMethod]
        public void DepartmentResultsPreserveCurrentFieldsAnd64BitIds()
        {
            var result = JsonSerializer.Deserialize<SchoolDepartmentListResult>(
                "{\"errcode\":0,\"departments\":[{\"id\":5178368698," +
                "\"name\":\"三年级一班\",\"type\":2,\"parentid\":5178368799," +
                "\"standard_grade\":3,\"register_year\":2024,\"order\":5178368899," +
                "\"department_admins\":[{\"userid\":\"teacher-1\",\"type\":1," +
                "\"subject\":\"数学\"}],\"is_graduated\":0,\"open_group_chat\":1," +
                "\"group_chat_id\":\"group-1\"}]}");

            Assert.IsNotNull(result);
            var department = result.departments[0];
            Assert.AreEqual(5178368698L, department.id);
            Assert.AreEqual(5178368799L, department.parentid);
            Assert.AreEqual(5178368899L, department.order);
            Assert.AreEqual(1, department.open_group_chat);
            Assert.AreEqual("teacher-1", department.department_admins[0].userid);
            Assert.AreEqual("group-1", department.group_chat_id);
        }

        [TestMethod]
        public void SchoolContactCallbackPreservesMemberAndDepartmentChangeTypes()
        {
            var studentDocument = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>5178368698</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[change_school_contact]]></Event>
<ChangeType><![CDATA[update_student]]></ChangeType>
<Id><![CDATA[student-1]]></Id>
</xml>");
            var studentRequest = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), studentDocument) as
                RequestMessageEvent_Change_School_Contact;

            Assert.IsNotNull(studentRequest);
            Assert.AreEqual(Event.change_school_contact, studentRequest.Event);
            Assert.AreEqual("update_student", studentRequest.ChangeType);
            Assert.AreEqual("student-1", studentRequest.Id);

            var departmentDocument = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>5178368698</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[change_school_contact]]></Event>
<ChangeType><![CDATA[create_deparmtment]]></ChangeType>
<Id><![CDATA[5000000000]]></Id>
</xml>");
            var departmentRequest = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), departmentDocument) as
                RequestMessageEvent_Change_School_Contact;

            Assert.IsNotNull(departmentRequest);
            Assert.AreEqual("create_deparmtment", departmentRequest.ChangeType);
            Assert.AreEqual("5000000000", departmentRequest.Id);
            Assert.IsNotNull(typeof(WorkMessageHandler<>).GetMethod(
                "OnEvent_ChangeSchoolContactRequest"));
            Assert.IsNotNull(typeof(WorkMessageHandler<>).GetMethod(
                "OnEvent_ChangeSchoolContactRequestAsync"));

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "Entities", "Request", "Event",
                "RequestMessageEvent_Change_School_Contact.cs"));
            StringAssert.Contains(source, "/document/path/92032");
            StringAssert.Contains(source, "/document/path/92052");
            Assert.AreEqual(4, source.Split(new[] { "/// <summary>" },
                StringSplitOptions.None).Length - 1);
            Assert.IsFalse(source.Contains("public object "));
            Assert.IsFalse(source.Contains("public dynamic "));
        }

        [TestMethod]
        public void SchoolModelsDoNotExposeObjectPayloads()
        {
            var objectProperties = typeof(SchoolApi).Assembly.GetTypes()
                .Where(type => type.Namespace == typeof(SchoolApi).Namespace)
                .SelectMany(type => type.GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                                       BindingFlags.DeclaredOnly))
                .Where(property => property.PropertyType == typeof(object))
                .Select(property => property.DeclaringType?.Name + "." + property.Name)
                .ToArray();

            Assert.AreEqual(0, objectProperties.Length,
                "家校沟通模型不应使用 object 作为协议字段：" + string.Join(", ", objectProperties));
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
