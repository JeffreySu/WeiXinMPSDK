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
using Senparc.Weixin.Work.AdvancedAPIs.Contact;
using Senparc.Weixin.Work.AdvancedAPIs.CustomerAcquisition;
using Senparc.Weixin.Work.AdvancedAPIs.CustomerAcquisition.CustomerAcquisitionJson;
using Senparc.Weixin.Work.AdvancedAPIs.External;
using Senparc.Weixin.Work.AdvancedAPIs.Security;
using Senparc.Weixin.Work.AdvancedAPIs.SmartRobot;
using Senparc.Weixin.Work.AdvancedAPIs.WeChatCustomerService;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
{
    [TestClass]
    public class P1ContractTests
    {
        [TestMethod]
        public void P1ApiSurfaceContainsAll66SyncAndAsyncEntries()
        {
            var methodCount = 0;

            methodCount += AssertMethodPairs(typeof(ContactP1Api),
                nameof(ContactP1Api.ConvertTemporaryExternalUserId),
                nameof(ContactP1Api.CreateContactRules), nameof(ContactP1Api.GetContactRuleList),
                nameof(ContactP1Api.UpdateContactRules), nameof(ContactP1Api.DeleteContactRules),
                nameof(ContactP1Api.ExportMembers), nameof(ContactP1Api.ExportMemberDetails),
                nameof(ContactP1Api.ExportDepartments), nameof(ContactP1Api.ExportTagMembers),
                nameof(ContactP1Api.GetExportResult));
            methodCount += AssertMethodPairs(typeof(ExternalApi),
                nameof(ExternalApi.OnJobTransferGroupChat), nameof(ExternalApi.RemindGroupMessageSend),
                nameof(ExternalApi.CancelGroupMessageSend), nameof(ExternalApi.CancelMomentTask),
                nameof(ExternalApi.GetServedExternalContactList));
            methodCount += AssertMethodPairs(typeof(CustomerAcquisitionApi),
                nameof(CustomerAcquisitionApi.GetCustomers), nameof(CustomerAcquisitionApi.GetQuota),
                nameof(CustomerAcquisitionApi.GetStatistic), nameof(CustomerAcquisitionApi.GetChatInfo));
            methodCount += AssertMethodPairs(typeof(SecurityApi),
                nameof(SecurityApi.GetFileOperationRecords), nameof(SecurityApi.ImportTrustDevices),
                nameof(SecurityApi.GetTrustDeviceList), nameof(SecurityApi.GetTrustDevicesByUser),
                nameof(SecurityApi.DeleteTrustDevices), nameof(SecurityApi.ApproveTrustDevices),
                nameof(SecurityApi.RejectTrustDevices), nameof(SecurityApi.GetScreenOperationRecords),
                nameof(SecurityApi.AssignVipUsers), nameof(SecurityApi.GetAssignVipJobResult),
                nameof(SecurityApi.CancelVipUsers), nameof(SecurityApi.GetCancelVipJobResult),
                nameof(SecurityApi.GetVipUserList), nameof(SecurityApi.GetMemberOperationLogs),
                nameof(SecurityApi.GetAdminOperationLogs), nameof(SecurityApi.GetServerDomainIp));
            methodCount += AssertMethodPairs(typeof(SmartRobotApi),
                nameof(SmartRobotApi.Reply), nameof(SmartRobotApi.GetGroupChatList),
                nameof(SmartRobotApi.GetGroupChat), nameof(SmartRobotApi.UpdateGroupChat));
            methodCount += AssertMethodPairs(typeof(WeChatCustomerServiceApi),
                nameof(WeChatCustomerServiceApi.AddAccount), nameof(WeChatCustomerServiceApi.DeleteAccount),
                nameof(WeChatCustomerServiceApi.UpdateAccount), nameof(WeChatCustomerServiceApi.GetAccountList),
                nameof(WeChatCustomerServiceApi.AddContactWay), nameof(WeChatCustomerServiceApi.AddServicers),
                nameof(WeChatCustomerServiceApi.DeleteServicers), nameof(WeChatCustomerServiceApi.GetServicerList),
                nameof(WeChatCustomerServiceApi.GetServiceState), nameof(WeChatCustomerServiceApi.TransferServiceState),
                nameof(WeChatCustomerServiceApi.SyncMessages), nameof(WeChatCustomerServiceApi.SendMessage),
                nameof(WeChatCustomerServiceApi.SendMessageOnEvent),
                nameof(WeChatCustomerServiceApi.GetUpgradeServiceConfig),
                nameof(WeChatCustomerServiceApi.UpgradeService),
                nameof(WeChatCustomerServiceApi.CancelUpgradeService),
                nameof(WeChatCustomerServiceApi.BatchGetCustomers),
                nameof(WeChatCustomerServiceApi.GetCorpStatistic),
                nameof(WeChatCustomerServiceApi.GetServicerStatistic),
                nameof(WeChatCustomerServiceApi.AddKnowledgeGroup),
                nameof(WeChatCustomerServiceApi.DeleteKnowledgeGroup),
                nameof(WeChatCustomerServiceApi.UpdateKnowledgeGroup),
                nameof(WeChatCustomerServiceApi.GetKnowledgeGroupList),
                nameof(WeChatCustomerServiceApi.AddKnowledgeIntent),
                nameof(WeChatCustomerServiceApi.DeleteKnowledgeIntent),
                nameof(WeChatCustomerServiceApi.UpdateKnowledgeIntent),
                nameof(WeChatCustomerServiceApi.GetKnowledgeIntentList));

            Assert.AreEqual(66, methodCount, "Work P1 应提供 66 对同步和异步入口。");
        }

        [TestMethod]
        public void P1ImplementationsContainOfficialPaths()
        {
            AssertSourceContains("AdvancedAPIs/Contact/ContactP1Api.cs",
                "/cgi-bin/idconvert/convert_tmp_external_userid",
                "/cgi-bin/contactrule/create", "/cgi-bin/contactrule/list",
                "/cgi-bin/contactrule/update", "/cgi-bin/contactrule/delete",
                "/cgi-bin/export/simple_user", "/cgi-bin/export/user",
                "/cgi-bin/export/department", "/cgi-bin/export/taguser",
                "/cgi-bin/export/get_result");
            AssertSourceContains("AdvancedAPIs/External/ExternalP1Api.cs",
                "/cgi-bin/externalcontact/groupchat/onjob_transfer",
                "/cgi-bin/externalcontact/remind_groupmsg_send",
                "/cgi-bin/externalcontact/cancel_groupmsg_send",
                "/cgi-bin/externalcontact/cancel_moment_task",
                "/cgi-bin/externalcontact/contact_list");
            AssertSourceContains("AdvancedAPIs/CustomerAcquisition/CustomerAcquisitionP1Api.cs",
                "/cgi-bin/externalcontact/customer_acquisition/customer",
                "/cgi-bin/externalcontact/customer_acquisition_quota",
                "/cgi-bin/externalcontact/customer_acquisition/statistic",
                "/cgi-bin/externalcontact/customer_acquisition/get_chat_info");
            AssertSourceContains("AdvancedAPIs/Security/SecurityApi.cs",
                "/cgi-bin/security/get_file_oper_record", "/cgi-bin/security/trustdevice/import",
                "/cgi-bin/security/trustdevice/list", "/cgi-bin/security/trustdevice/get_by_user",
                "/cgi-bin/security/trustdevice/delete", "/cgi-bin/security/trustdevice/approve",
                "/cgi-bin/security/trustdevice/reject", "/cgi-bin/security/get_screen_oper_record",
                "/cgi-bin/security/vip/submit_batch_add_job",
                "/cgi-bin/security/vip/batch_add_job_result",
                "/cgi-bin/security/vip/submit_batch_del_job",
                "/cgi-bin/security/vip/batch_del_job_result", "/cgi-bin/security/vip/list",
                "/cgi-bin/security/member_oper_log/list", "/cgi-bin/security/admin_oper_log/list",
                "/cgi-bin/security/get_server_domain_ip");
            AssertSourceContains("AdvancedAPIs/SmartRobot/SmartRobotApi.cs",
                "responseUrl", "/cgi-bin/wedoc/smartsheet/groupchat/list",
                "/cgi-bin/wedoc/smartsheet/groupchat/get",
                "/cgi-bin/wedoc/smartsheet/groupchat/update");
            AssertSourceContains("AdvancedAPIs/WeChatCustomerService/WeChatCustomerServiceApi.cs",
                "/cgi-bin/kf/account/add", "/cgi-bin/kf/account/del", "/cgi-bin/kf/account/update",
                "/cgi-bin/kf/account/list", "/cgi-bin/kf/add_contact_way",
                "/cgi-bin/kf/servicer/add", "/cgi-bin/kf/servicer/del", "/cgi-bin/kf/servicer/list",
                "/cgi-bin/kf/service_state/get", "/cgi-bin/kf/service_state/trans",
                "/cgi-bin/kf/sync_msg", "/cgi-bin/kf/send_msg", "/cgi-bin/kf/send_msg_on_event",
                "/cgi-bin/kf/customer/get_upgrade_service_config",
                "/cgi-bin/kf/customer/upgrade_service", "/cgi-bin/kf/customer/cancel_upgrade_service",
                "/cgi-bin/kf/customer/batchget", "/cgi-bin/kf/get_corp_statistic",
                "/cgi-bin/kf/get_servicer_statistic", "/cgi-bin/kf/knowledge/add_group",
                "/cgi-bin/kf/knowledge/del_group", "/cgi-bin/kf/knowledge/mod_group",
                "/cgi-bin/kf/knowledge/list_group", "/cgi-bin/kf/knowledge/add_intent",
                "/cgi-bin/kf/knowledge/del_intent", "/cgi-bin/kf/knowledge/mod_intent",
                "/cgi-bin/kf/knowledge/list_intent");
        }

        [TestMethod]
        public void P1ResponseModelsPreserveOfficialFieldsAndLargeNumericValues()
        {
            var export = JsonSerializer.Deserialize<GetExportContactResult>(
                "{\"errcode\":0,\"status\":1,\"data_list\":[{\"url\":\"https://example.test/data\"," +
                "\"size\":5178368698,\"md5\":\"abc\"}]}");
            var served = JsonSerializer.Deserialize<ServedExternalContactListResult>(
                "{\"errcode\":0,\"info_list\":[{\"is_customer\":true,\"external_userid\":\"wo1\"," +
                "\"add_time\":5178368698}],\"next_cursor\":\"next\"}");
            var security = JsonSerializer.Deserialize<FileOperationRecordResult>(
                "{\"errcode\":0,\"has_more\":true,\"record_list\":[{\"time\":5178368698," +
                "\"userid\":\"zhangsan\",\"file_size\":5178368799," +
                "\"external_user\":{\"type\":1,\"name\":\"客户\"}}]}");
            var customerService = JsonSerializer.Deserialize<KfSyncMessageResult>(
                "{\"errcode\":0,\"has_more\":1,\"msg_list\":[{\"msgid\":\"m1\"," +
                "\"send_time\":5178368698,\"msgtype\":\"text\",\"text\":{\"content\":\"hello\"}}]}");

            Assert.AreEqual(5178368698L, export.data_list[0].size);
            Assert.AreEqual(5178368698L, served.info_list[0].add_time);
            Assert.AreEqual(5178368799L, security.record_list[0].file_size);
            Assert.AreEqual("客户", security.record_list[0].external_user.name);
            Assert.AreEqual(5178368698L, customerService.msg_list[0].send_time);
            Assert.AreEqual("hello", customerService.msg_list[0].text.content);
        }

        [TestMethod]
        public void WeChatCustomerServiceRequestUsesOfficialJsonFields()
        {
            var request = new KfSendMessageRequest
            {
                touser = "external-user",
                open_kfid = "wk-account",
                msgtype = "text",
                text = new KfTextMessage { content = "hello" }
            };

            var json = JsonSerializer.Serialize(request);

            StringAssert.Contains(json, "\"touser\":\"external-user\"");
            StringAssert.Contains(json, "\"open_kfid\":\"wk-account\"");
            StringAssert.Contains(json, "\"msgtype\":\"text\"");
            StringAssert.Contains(json, "\"content\":\"hello\"");
        }

        [TestMethod]
        public void CustomerAcquisitionRequestUsesOfficialJsonFields()
        {
            var request = new GetCustomerAcquisitionStatisticRequest
            {
                link_id = "ca-link",
                start_time = 1688140800,
                end_time = 1688486400
            };

            var json = JsonSerializer.Serialize(request);

            StringAssert.Contains(json, "\"link_id\":\"ca-link\"");
            StringAssert.Contains(json, "\"start_time\":1688140800");
            StringAssert.Contains(json, "\"end_time\":1688486400");
        }

        [TestMethod]
        public void CustomerAcquisitionCallbackMapsToStrongType()
        {
            var doc = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[ww-corp]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>1688140800</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[customer_acquisition]]></Event>
<ChangeType><![CDATA[message_from_customer]]></ChangeType>
<LinkId><![CDATA[ca-link]]></LinkId>
<ChatKey><![CDATA[chat-key]]></ChatKey>
</xml>");

            var result = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), doc) as RequestMessageEvent_Customer_Acquisition;

            Assert.IsNotNull(result);
            Assert.AreEqual(Event.CUSTOMER_ACQUISITION, result.Event);
            Assert.AreEqual("message_from_customer", result.ChangeType);
            Assert.AreEqual("ca-link", result.LinkId);
            Assert.AreEqual("chat-key", result.ChatKey);
        }

        [TestMethod]
        public void KfAccountAuthorizationCallbackMapsRepeatedAccounts()
        {
            var doc = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[ww-corp]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>1688140800</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[kf_account_auth_change]]></Event>
<AuthAddOpenKfId><![CDATA[wka]]></AuthAddOpenKfId>
<AuthAddOpenKfId><![CDATA[wkb]]></AuthAddOpenKfId>
<AuthDelOpenKfId><![CDATA[wkc]]></AuthDelOpenKfId>
</xml>");

            var result = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), doc) as RequestMessageEvent_Kf_Account_Auth_Change;

            Assert.IsNotNull(result);
            Assert.AreEqual(Event.KF_ACCOUNT_AUTH_CHANGE, result.Event);
            CollectionAssert.AreEqual(new List<string> { "wka", "wkb" }, result.AuthAddOpenKfId);
            CollectionAssert.AreEqual(new List<string> { "wkc" }, result.AuthDelOpenKfId);
        }

        private static int AssertMethodPairs(Type type, params string[] syncMethodNames)
        {
            var methodNames = type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            foreach (var syncMethodName in syncMethodNames)
            {
                CollectionAssert.Contains(methodNames, syncMethodName, $"{type.Name}.{syncMethodName}");
                CollectionAssert.Contains(methodNames, syncMethodName + "Async",
                    $"{type.Name}.{syncMethodName}Async");
            }

            return syncMethodNames.Length;
        }

        private static void AssertSourceContains(string relativePath, params string[] expectedValues)
        {
            var sourceRoot = Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work");
            var source = File.ReadAllText(Path.Combine(sourceRoot,
                relativePath.Replace('/', Path.DirectorySeparatorChar)));

            foreach (var expectedValue in expectedValues)
            {
                Assert.IsTrue(source.Contains(expectedValue),
                    $"{relativePath} 缺少官方契约：{expectedValue}");
            }
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
                if (string.IsNullOrEmpty(startPath))
                {
                    continue;
                }

                var directory = new DirectoryInfo(startPath);
                while (directory != null)
                {
                    if (Directory.Exists(Path.Combine(directory.FullName, ".git")) &&
                        Directory.Exists(Path.Combine(directory.FullName, "src", "Senparc.Weixin.Work")))
                    {
                        return directory.FullName;
                    }

                    directory = directory.Parent;
                }
            }

            throw new DirectoryNotFoundException("无法定位 WeiXinMPSDK 仓库根目录。");
        }
    }
}
