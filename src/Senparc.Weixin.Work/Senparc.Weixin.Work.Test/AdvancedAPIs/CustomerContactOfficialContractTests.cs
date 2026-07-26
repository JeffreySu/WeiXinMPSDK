using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Entities.Request.Event;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
{
    [TestClass]
    public class CustomerContactOfficialContractTests
    {
        private static readonly string[] OfficialApiPaths =
        {
            "/cgi-bin/externalcontact/add_contact_way",
            "/cgi-bin/externalcontact/add_corp_tag",
            "/cgi-bin/externalcontact/add_intercept_rule",
            "/cgi-bin/externalcontact/add_moment_task",
            "/cgi-bin/externalcontact/add_msg_template",
            "/cgi-bin/externalcontact/add_product_album",
            "/cgi-bin/externalcontact/add_strategy_tag",
            "/cgi-bin/externalcontact/batch/get_by_user",
            "/cgi-bin/externalcontact/cancel_groupmsg_send",
            "/cgi-bin/externalcontact/cancel_moment_task",
            "/cgi-bin/externalcontact/close_temp_chat",
            "/cgi-bin/externalcontact/contact_list",
            "/cgi-bin/externalcontact/customer_acquisition/create_link",
            "/cgi-bin/externalcontact/customer_acquisition/customer",
            "/cgi-bin/externalcontact/customer_acquisition/delete_link",
            "/cgi-bin/externalcontact/customer_acquisition/get",
            "/cgi-bin/externalcontact/customer_acquisition/get_chat_info",
            "/cgi-bin/externalcontact/customer_acquisition/list_link",
            "/cgi-bin/externalcontact/customer_acquisition/statistic",
            "/cgi-bin/externalcontact/customer_acquisition/update_link",
            "/cgi-bin/externalcontact/customer_acquisition_quota",
            "/cgi-bin/externalcontact/customer_strategy/create",
            "/cgi-bin/externalcontact/customer_strategy/del",
            "/cgi-bin/externalcontact/customer_strategy/edit",
            "/cgi-bin/externalcontact/customer_strategy/get",
            "/cgi-bin/externalcontact/customer_strategy/get_range",
            "/cgi-bin/externalcontact/customer_strategy/list",
            "/cgi-bin/externalcontact/del_contact_way",
            "/cgi-bin/externalcontact/del_corp_tag",
            "/cgi-bin/externalcontact/del_intercept_rule",
            "/cgi-bin/externalcontact/del_strategy_tag",
            "/cgi-bin/externalcontact/delete_product_album",
            "/cgi-bin/externalcontact/edit_corp_tag",
            "/cgi-bin/externalcontact/edit_strategy_tag",
            "/cgi-bin/externalcontact/get",
            "/cgi-bin/externalcontact/get_contact_way",
            "/cgi-bin/externalcontact/get_corp_tag_list",
            "/cgi-bin/externalcontact/get_follow_user_list",
            "/cgi-bin/externalcontact/get_groupmsg_list_v2",
            "/cgi-bin/externalcontact/get_groupmsg_send_result",
            "/cgi-bin/externalcontact/get_groupmsg_task",
            "/cgi-bin/externalcontact/get_intercept_rule",
            "/cgi-bin/externalcontact/get_intercept_rule_list",
            "/cgi-bin/externalcontact/get_moment_comments",
            "/cgi-bin/externalcontact/get_moment_customer_list",
            "/cgi-bin/externalcontact/get_moment_list",
            "/cgi-bin/externalcontact/get_moment_send_result",
            "/cgi-bin/externalcontact/get_moment_task",
            "/cgi-bin/externalcontact/get_moment_task_result",
            "/cgi-bin/externalcontact/get_product_album",
            "/cgi-bin/externalcontact/get_product_album_list",
            "/cgi-bin/externalcontact/get_strategy_tag_list",
            "/cgi-bin/externalcontact/get_unassigned_list",
            "/cgi-bin/externalcontact/get_user_behavior_data",
            "/cgi-bin/externalcontact/group_welcome_template/add",
            "/cgi-bin/externalcontact/group_welcome_template/del",
            "/cgi-bin/externalcontact/group_welcome_template/edit",
            "/cgi-bin/externalcontact/group_welcome_template/get",
            "/cgi-bin/externalcontact/groupchat/add_join_way",
            "/cgi-bin/externalcontact/groupchat/del_join_way",
            "/cgi-bin/externalcontact/groupchat/get",
            "/cgi-bin/externalcontact/groupchat/get_join_way",
            "/cgi-bin/externalcontact/groupchat/list",
            "/cgi-bin/externalcontact/groupchat/onjob_transfer",
            "/cgi-bin/externalcontact/groupchat/statistic",
            "/cgi-bin/externalcontact/groupchat/statistic_group_by_day",
            "/cgi-bin/externalcontact/groupchat/transfer",
            "/cgi-bin/externalcontact/groupchat/update_join_way",
            "/cgi-bin/externalcontact/list",
            "/cgi-bin/externalcontact/list_contact_way",
            "/cgi-bin/externalcontact/mark_tag",
            "/cgi-bin/externalcontact/moment_strategy/create",
            "/cgi-bin/externalcontact/moment_strategy/del",
            "/cgi-bin/externalcontact/moment_strategy/edit",
            "/cgi-bin/externalcontact/moment_strategy/get",
            "/cgi-bin/externalcontact/moment_strategy/get_range",
            "/cgi-bin/externalcontact/moment_strategy/list",
            "/cgi-bin/externalcontact/opengid_to_chatid",
            "/cgi-bin/externalcontact/remark",
            "/cgi-bin/externalcontact/remind_groupmsg_send",
            "/cgi-bin/externalcontact/resigned/transfer_customer",
            "/cgi-bin/externalcontact/resigned/transfer_result",
            "/cgi-bin/externalcontact/send_welcome_msg",
            "/cgi-bin/externalcontact/transfer_customer",
            "/cgi-bin/externalcontact/transfer_result",
            "/cgi-bin/externalcontact/update_contact_way",
            "/cgi-bin/externalcontact/update_intercept_rule",
            "/cgi-bin/externalcontact/update_product_album",
            "/cgi-bin/media/upload_attachment"
        };

        [TestMethod]
        public void AllOfficialCustomerContactApiPathsAreMapped()
        {
            Assert.AreEqual(89, OfficialApiPaths.Length);
            Assert.AreEqual(OfficialApiPaths.Length, OfficialApiPaths.Distinct().Count());

            var workProject = Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs");
            var sourceRoots = new[] { "External", "CustomerAcquisition", "Media" }
                .Select(directory => Path.Combine(workProject, directory));
            var source = string.Join("\n", sourceRoots
                .SelectMany(directory => Directory.GetFiles(directory, "*.cs", SearchOption.AllDirectories))
                .Select(File.ReadAllText));

            foreach (var path in OfficialApiPaths)
            {
                StringAssert.Contains(source, path, path);
            }
        }

        [TestMethod]
        public void AllThirteenOfficialCustomerContactCallbackKindsAreStronglyMapped()
        {
            var mappings = new Dictionary<(string EventName, string ChangeType), Type>
            {
                [("change_external_contact", "add_external_contact")] = typeof(RequestMessageEvent_Change_ExternalContact_Add),
                [("change_external_contact", "edit_external_contact")] = typeof(RequestMessageEvent_Change_ExternalContact_Modified),
                [("change_external_contact", "add_half_external_contact")] = typeof(RequestMessageEvent_Change_ExternalContact_Add_Half),
                [("change_external_contact", "del_external_contact")] = typeof(RequestMessageEvent_Change_ExternalContact_Del),
                [("change_external_contact", "del_follow_user")] = typeof(RequestMessageEvent_Change_ExternalContact_Del_FollowUser),
                [("change_external_contact", "transfer_fail")] = typeof(RequestMessageEvent_Change_ExternalContact_Transfer_Fail),
                [("change_external_chat", "create")] = typeof(RequestMessageEvent_Change_External_Chat_Create),
                [("change_external_chat", "update")] = typeof(RequestMessageEvent_Change_External_Chat_Update),
                [("change_external_chat", "dismiss")] = typeof(RequestMessageEvent_Change_External_Chat_Dismiss),
                [("change_external_tag", "create")] = typeof(RequestMessageEvent_Change_External_Tag_Create),
                [("change_external_tag", "update")] = typeof(RequestMessageEvent_Change_External_Tag_Update),
                [("change_external_tag", "delete")] = typeof(RequestMessageEvent_Change_External_Tag_Delete),
                [("change_external_tag", "shuffle")] = typeof(RequestMessageEvent_Change_External_Tag_Shuffle)
            };

            Assert.AreEqual(13, mappings.Count);
            foreach (var mapping in mappings)
            {
                var document = CreateCallbackDocument(mapping.Key.EventName, mapping.Key.ChangeType);
                var callback = RequestMessageFactory.GetRequestEntity(
                    new MessageContexts.DefaultWorkMessageContext(), document);

                Assert.IsInstanceOfType(callback, mapping.Value,
                    $"{mapping.Key.EventName}/{mapping.Key.ChangeType}");
            }
        }

        [TestMethod]
        public void TransferFailCallbackPreservesOfficialFieldsAndHandlerHooks()
        {
            var callback = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(),
                CreateCallbackDocument("change_external_contact", "transfer_fail"))
                as RequestMessageEvent_Change_ExternalContact_Transfer_Fail;
            var handlerMethods = typeof(Senparc.Weixin.Work.MessageHandlers.WorkMessageHandler<>)
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(method => method.Name).ToArray();

            Assert.IsNotNull(callback);
            Assert.AreEqual(ExternalContactChangeType.transfer_fail, callback.ChangeType);
            Assert.AreEqual("customer_refused", callback.FailReason);
            Assert.AreEqual("zhangsan", callback.UserID);
            Assert.AreEqual("external-user", callback.ExternalUserID);
            CollectionAssert.Contains(handlerMethods, "OnEvent_ChangeExternalContactTransferFailRequest");
            CollectionAssert.Contains(handlerMethods, "OnEvent_ChangeExternalContactTransferFailRequestAsync");
            CollectionAssert.Contains(handlerMethods, "OnThirdPartyEvent_ChangeExternalContactTransferFailRequest");
            CollectionAssert.Contains(handlerMethods, "OnThirdPartyEvent_ChangeExternalContactTransferFailRequestAsync");
        }

        private static XDocument CreateCallbackDocument(string eventName, string changeType)
        {
            return XDocument.Parse($@"<xml>
<ToUserName><![CDATA[ww-corp]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>1403610513</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[{eventName}]]></Event>
<ChangeType><![CDATA[{changeType}]]></ChangeType>
<FailReason><![CDATA[customer_refused]]></FailReason>
<UserID><![CDATA[zhangsan]]></UserID>
<ExternalUserID><![CDATA[external-user]]></ExternalUserID>
<ChatId><![CDATA[chat-id]]></ChatId>
<TagType><![CDATA[tag]]></TagType>
<Id><![CDATA[tag-id]]></Id>
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
