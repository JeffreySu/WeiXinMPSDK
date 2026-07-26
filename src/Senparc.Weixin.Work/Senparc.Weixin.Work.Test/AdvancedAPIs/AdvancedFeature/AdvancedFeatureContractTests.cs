using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.AdvancedFeature;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.AdvancedFeature
{
    [TestClass]
    public class AdvancedFeatureContractTests
    {
        [TestMethod]
        public void AdvancedFeatureApiContainsThreeSyncAndAsyncEntries()
        {
            var methodNames = typeof(AdvancedFeatureApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var syncMethodName in new[]
            {
                nameof(AdvancedFeatureApi.SetApprovalDetail),
                nameof(AdvancedFeatureApi.GetApplyIdList),
                nameof(AdvancedFeatureApi.GetApprovalInfo)
            })
            {
                CollectionAssert.Contains(methodNames, syncMethodName, syncMethodName);
                CollectionAssert.Contains(methodNames, syncMethodName + "Async", syncMethodName + "Async");
            }
        }

        [TestMethod]
        public void AdvancedFeatureApiUsesOfficialPostPaths()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "AdvancedFeature", "AdvancedFeatureApi.cs"));

            StringAssert.Contains(source, "/cgi-bin/advanced_feature/set_approval_detail");
            StringAssert.Contains(source, "/cgi-bin/advanced_feature/get_apply_id_list");
            StringAssert.Contains(source, "/cgi-bin/advanced_feature/get_approval_info");
            StringAssert.Contains(source, "CommonJsonSendType.POST");
        }

        [TestMethod]
        public void SetApprovalDetailUsesFullStronglyTypedProcess()
        {
            var json = JsonSerializer.Serialize(new SetAdvancedFeatureApprovalDetailRequest
            {
                approval_id = "approval-1",
                approval_status = 1,
                apply_id = "apply-1",
                approval_url = "https://example.com/approval-1",
                process_list = new AdvancedFeatureApprovalProcess
                {
                    node_list = new List<AdvancedFeatureApprovalNode>
                    {
                        new AdvancedFeatureApprovalNode
                        {
                            current_approvers = new List<string> { "leader-1", "leader-2" },
                            completed_approvers = new List<string> { "leader-0" },
                            node_apv_status = 1,
                            node_apv_rel = 3,
                            apv_update_time = 4294967296UL
                        }
                    }
                }
            });

            StringAssert.Contains(json, "\"approval_id\":\"approval-1\"");
            StringAssert.Contains(json, "\"approval_url\":\"https://example.com/approval-1\"");
            StringAssert.Contains(json, "\"current_approvers\":[\"leader-1\",\"leader-2\"]");
            StringAssert.Contains(json, "\"node_apv_status\":1");
            StringAssert.Contains(json, "\"node_apv_rel\":3");
            StringAssert.Contains(json, "\"apv_update_time\":4294967296");
        }

        [TestMethod]
        public void ApplyIdListUsesRequiredUserAndStringCursor()
        {
            var requestJson = JsonSerializer.Serialize(new GetAdvancedFeatureApplyIdListRequest
            {
                business_type = 3,
                userid = "zhangsan",
                limit = 200,
                cursor = "cursor-1",
                req_type = 1
            });
            var result = JsonSerializer.Deserialize<GetAdvancedFeatureApplyIdListResult>(
                "{\"errcode\":0,\"next_cursor\":\"cursor-2\"," +
                "\"apply_id_list\":[\"apply-1\",\"apply-2\"],\"has_more\":true}");

            StringAssert.Contains(requestJson, "\"business_type\":3");
            StringAssert.Contains(requestJson, "\"userid\":\"zhangsan\"");
            StringAssert.Contains(requestJson, "\"cursor\":\"cursor-1\"");
            Assert.IsNotNull(result);
            Assert.AreEqual("cursor-2", result.next_cursor);
            Assert.IsTrue(result.has_more);
            Assert.AreEqual("apply-2", result.apply_id_list[1]);
        }

        [TestMethod]
        public void ApprovalInfoPreservesLargeCreateTimeAndOfficialStates()
        {
            var result = JsonSerializer.Deserialize<GetAdvancedFeatureApprovalInfoResult>(
                "{\"errcode\":0,\"approval_info\":{" +
                "\"applicant\":\"zhangsan\",\"create_time\":4294967296," +
                "\"business_type\":4,\"approval_id\":\"approval-1\"," +
                "\"apply_id\":\"apply-1\",\"approval_url\":\"https://example.com/a\"," +
                "\"approval_status\":5,\"approval_type\":2," +
                "\"request_reason\":\"需要会议账号\"}}");

            Assert.IsNotNull(result);
            Assert.AreEqual(4294967296UL, result.approval_info.create_time);
            Assert.AreEqual(4U, result.approval_info.business_type);
            Assert.AreEqual(5U, result.approval_info.approval_status);
            Assert.AreEqual(2U, result.approval_info.approval_type);
            Assert.AreEqual("需要会议账号", result.approval_info.request_reason);
        }

        [TestMethod]
        public void AdvancedFeatureCallbacksMapToStrongTypesAndHandlerHooks()
        {
            var submitDoc = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[zhangsan]]></FromUserName>
<CreateTime>4294967296</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[submit_vip_account_approval]]></Event>
<ApplyReason><![CDATA[需要更多微盘空间]]></ApplyReason>
<BusinessType>3</BusinessType>
<ApplyId><![CDATA[apply-1]]></ApplyId>
<AgentID>1000001</AgentID>
</xml>");
            var finishDoc = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>4294967297</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[finish_vip_account_approval]]></Event>
<FinishType>2</FinishType>
<ApplyId><![CDATA[apply-1]]></ApplyId>
<AgentID>1000001</AgentID>
</xml>");

            var submitted = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), submitDoc)
                as RequestMessageEvent_Submit_Vip_Account_Approval;
            var finished = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), finishDoc)
                as RequestMessageEvent_Finish_Vip_Account_Approval;
            var handlerMethods = typeof(Senparc.Weixin.Work.MessageHandlers.WorkMessageHandler<>).GetMethods()
                .Select(method => method.Name).ToArray();

            Assert.IsNotNull(submitted);
            Assert.AreEqual(Event.submit_vip_account_approval, submitted.Event);
            Assert.AreEqual("需要更多微盘空间", submitted.ApplyReason);
            Assert.AreEqual(3U, submitted.BusinessType);
            Assert.AreEqual("apply-1", submitted.ApplyId);
            Assert.IsNotNull(finished);
            Assert.AreEqual(Event.finish_vip_account_approval, finished.Event);
            Assert.AreEqual(2U, finished.FinishType);
            Assert.AreEqual("apply-1", finished.ApplyId);
            CollectionAssert.Contains(handlerMethods, "OnEvent_SubmitVipAccountApprovalRequest");
            CollectionAssert.Contains(handlerMethods, "OnEvent_FinishVipAccountApprovalRequest");
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
