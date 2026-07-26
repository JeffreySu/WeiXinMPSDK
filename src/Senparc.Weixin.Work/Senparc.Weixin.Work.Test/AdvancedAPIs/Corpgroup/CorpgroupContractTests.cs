using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Corpgroup;
using Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Base;
using Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Corpgroup
{
    [TestClass]
    public class CorpgroupContractTests
    {
        [TestMethod]
        public void CorpgroupApiCoversAllNineteenOfficialEndpoints()
        {
            var methodNames = typeof(CorpgroupApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var methodName in new[]
            {
                nameof(CorpgroupApi.CorpListAppShareInfo), nameof(CorpgroupApi.CorpGetToken),
                nameof(CorpgroupApi.TransferSession), nameof(CorpgroupApi.UnionIdToExternalUserId),
                nameof(CorpgroupApi.UnionIdToPendingId), nameof(CorpgroupApi.CorpGetChainList),
                nameof(CorpgroupApi.CorpGetChainGroup), nameof(CorpgroupApi.CorpGetChainCorpInfoList),
                nameof(CorpgroupApi.CorpGetChainCorpInfo),
                nameof(CorpgroupApi.ImportChainContact), nameof(CorpgroupApi.GetResult),
                nameof(CorpgroupApi.CorpRemoveCorp), nameof(CorpgroupApi.CorpGetChainUserCustomId),
                nameof(CorpgroupApi.GetCorpSharedChainList), nameof(CorpgroupApi.RuleListIds),
                nameof(CorpgroupApi.RuleDeleteRule), nameof(CorpgroupApi.RuleGetRuleInfo),
                nameof(CorpgroupApi.RuleAddRule), nameof(CorpgroupApi.RuleModifyRule)
            })
            {
                CollectionAssert.Contains(methodNames, methodName, methodName);
                CollectionAssert.Contains(methodNames, methodName + "Async", methodName + "Async");
            }
        }

        [TestMethod]
        public void CorpgroupApiUsesAllOfficialPaths()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Corpgroup", "CorpgroupApi.cs"));

            foreach (var path in new[]
            {
                "/cgi-bin/corpgroup/corp/list_app_share_info", "/cgi-bin/corpgroup/corp/gettoken",
                "/cgi-bin/miniprogram/transfer_session", "/cgi-bin/corpgroup/unionid_to_external_userid",
                "/cgi-bin/corpgroup/unionid_to_pending_id", "/cgi-bin/corpgroup/corp/get_chain_list",
                "/cgi-bin/corpgroup/corp/get_chain_group", "/cgi-bin/corpgroup/corp/get_chain_corpinfo_list",
                "/cgi-bin/corpgroup/corp/get_chain_corpinfo",
                "/cgi-bin/corpgroup/import_chain_contact", "/cgi-bin/corpgroup/getresult",
                "/cgi-bin/corpgroup/corp/remove_corp", "/cgi-bin/corpgroup/corp/get_chain_user_custom_id",
                "/cgi-bin/corpgroup/get_corp_shared_chain_list", "/cgi-bin/corpgroup/rule/list_ids",
                "/cgi-bin/corpgroup/rule/delete_rule", "/cgi-bin/corpgroup/rule/get_rule_info",
                "/cgi-bin/corpgroup/rule/add_rule", "/cgi-bin/corpgroup/rule/modify_rule"
            })
            {
                StringAssert.Contains(source, path);
            }

            Assert.AreEqual(2, CountOccurrences(source, "/cgi-bin/corpgroup/rule/add_rule"));
            Assert.AreEqual(2, CountOccurrences(source, "/cgi-bin/corpgroup/corp/get_chain_group?access_token"));
            Assert.AreEqual(2, CountOccurrences(source, "/cgi-bin/corpgroup/corp/get_chain_corpinfo_list?access_token"));
            Assert.AreEqual(2, CountOccurrences(source, "/cgi-bin/corpgroup/corp/get_chain_corpinfo?access_token"));
        }

        [TestMethod]
        public void TransferSessionModelsMatchOfficialContract()
        {
            var requestJson = JsonSerializer.Serialize(new TransferSessionRequest
            {
                userid = "encrypted-user",
                session_key = "upstream-session"
            });
            var result = JsonSerializer.Deserialize<TransferSessionResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"userid\":\"downstream-user\"," +
                "\"session_key\":\"downstream-session\"}");

            StringAssert.Contains(requestJson, "\"userid\":\"encrypted-user\"");
            StringAssert.Contains(requestJson, "\"session_key\":\"upstream-session\"");
            Assert.AreEqual("downstream-user", result.userid);
            Assert.AreEqual("downstream-session", result.session_key);
        }

        [TestMethod]
        public void ChainGroupAndCorpInfoModelsMatchOfficialContract()
        {
            var groupRequestJson = JsonSerializer.Serialize(new GetChainGroupRequest
            {
                chain_id = "Chxxxxxx",
                groupid = 1
            });
            var listRequestJson = JsonSerializer.Serialize(new GetChainCorpInfoListRequest
            {
                chain_id = "Chxxxxxx",
                groupid = 1,
                need_pending = false,
                cursor = string.Empty,
                limit = 100
            });
            var detailRequestJson = JsonSerializer.Serialize(new GetChainCorpInfoRequest
            {
                chain_id = "Chxxxxxx",
                corpid = "wwxxxx",
                pending_corpid = "pending-wwxxxx"
            });

            Assert.AreEqual("{\"chain_id\":\"Chxxxxxx\",\"groupid\":1}", groupRequestJson);
            Assert.AreEqual(
                "{\"chain_id\":\"Chxxxxxx\",\"groupid\":1,\"need_pending\":false,\"cursor\":\"\",\"limit\":100}",
                listRequestJson);
            Assert.AreEqual(
                "{\"chain_id\":\"Chxxxxxx\",\"corpid\":\"wwxxxx\",\"pending_corpid\":\"pending-wwxxxx\"}",
                detailRequestJson);

            var groupResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GetChainGroupResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"groups\":[{\"groupid\":2," +
                "\"group_name\":\"一级经销商\",\"parentid\":1,\"order\":4294967296}]}" );
            var listResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GetChainCorpInfoListResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"has_more\":false,\"next_cursor\":\"next\"," +
                "\"group_corps\":[{\"groupid\":2,\"corpid\":\"wwxxxx\",\"pending_corpid\":\"pending\"," +
                "\"corp_name\":\"美馨粮油公司\",\"custom_id\":\"custom\",\"invite_userid\":\"zhangsan\"," +
                "\"is_joined\":1}]}" );
            var detailResult = Newtonsoft.Json.JsonConvert.DeserializeObject<GetChainCorpInfoResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"corp_name\":\"美馨粮油公司\"," +
                "\"qualification_status\":1,\"custom_id\":\"custom\",\"groupid\":1,\"is_joined\":false}");

            Assert.AreEqual(4294967296L, groupResult.groups[0].order);
            Assert.AreEqual("next", listResult.next_cursor);
            Assert.IsTrue(listResult.group_corps[0].is_joined);
            Assert.AreEqual("zhangsan", listResult.group_corps[0].invite_userid);
            Assert.AreEqual(1, detailResult.qualification_status);
            Assert.IsFalse(detailResult.is_joined);

            var modelSource = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Corpgroup", "Corp", "ChainCorpJson.cs"));
            Assert.AreEqual(38, CountOccurrences(modelSource, "/// <summary>"));
            Assert.IsFalse(modelSource.Contains("object "));
        }

        [TestMethod]
        public void ChangeChainCallbackMapsToStrongTypeAndHandlerHooks()
        {
            var document = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[ww-corp]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>4294967296</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[change_chain]]></Event>
<ChangeType><![CDATA[update_group]]></ChangeType>
<ChainId><![CDATA[chain-1]]></ChainId>
<GroupIds><GroupId>4294967296</GroupId><GroupId>6</GroupId></GroupIds>
<CorpIds><CorpId><![CDATA[corp-1]]></CorpId><CorpId><![CDATA[corp-2]]></CorpId></CorpIds>
</xml>");
            var callback = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), document)
                as RequestMessageEvent_Change_Chain;
            var handlerMethods = typeof(Senparc.Weixin.Work.MessageHandlers.WorkMessageHandler<>).GetMethods()
                .Select(method => method.Name).ToArray();

            Assert.IsNotNull(callback);
            Assert.AreEqual(Event.change_chain, callback.Event);
            Assert.AreEqual("update_group", callback.ChangeType);
            Assert.AreEqual("chain-1", callback.ChainId);
            Assert.AreEqual(4294967296L, callback.GroupIds.Items[0]);
            Assert.AreEqual("corp-2", callback.CorpIds.Items[1]);
            CollectionAssert.Contains(handlerMethods, "OnEvent_ChangeChainRequest");
            CollectionAssert.Contains(handlerMethods, "OnEvent_ChangeChainRequestAsync");
        }

        [TestMethod]
        public void ImportChainBatchJobReusesExistingGenericCallback()
        {
            var document = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[ww-corp]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>4294967296</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[batch_job_result]]></Event>
<BatchJob><JobId><![CDATA[job-1]]></JobId><JobType><![CDATA[import_chain_contact]]></JobType>
<ErrCode>0</ErrCode><ErrMsg><![CDATA[ok]]></ErrMsg></BatchJob>
</xml>");
            var callback = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), document)
                as RequestMessageEvent_Batch_Job_Result;

            Assert.IsNotNull(callback);
            Assert.AreEqual(Event.BATCH_JOB_RESULT, callback.Event);
            Assert.AreEqual("job-1", callback.BatchJob.JobId);
            Assert.AreEqual("import_chain_contact", callback.BatchJob.JobType);
            Assert.AreEqual(0, callback.BatchJob.ErrCode);
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
