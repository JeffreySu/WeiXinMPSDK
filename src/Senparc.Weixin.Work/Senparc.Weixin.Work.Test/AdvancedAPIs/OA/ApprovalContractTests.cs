using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.OA;
using Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson;
using Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.OA
{
    [TestClass]
    public class ApprovalContractTests
    {
        [TestMethod]
        public void ApprovalApiCoversTwelveOfficialEndpoints()
        {
            var oaMethods = typeof(OaApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            var dataMethods = typeof(OaDataOpenApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var methodName in new[]
            {
                nameof(OaApi.GetTemplateDetail), nameof(OaApi.ApplyEvent),
                nameof(OaApi.GetApprovalInfo), nameof(OaApi.GetApprovalDetail),
                nameof(OaApi.VacationGetCorpConf), nameof(OaApi.VacationGetUserVacationQuota),
                nameof(OaApi.SetOneUserQuota), nameof(OaApi.ApprovalCreateTemplate),
                nameof(OaApi.ApprovalUpdateTemplate), nameof(OaApi.ApprovalCopyTemplate)
            })
            {
                CollectionAssert.Contains(oaMethods, methodName, methodName);
                CollectionAssert.Contains(oaMethods, methodName + "Async", methodName + "Async");
            }

            foreach (var methodName in new[]
            {
                nameof(OaDataOpenApi.GetApprovalData), nameof(OaDataOpenApi.GetOpenApprovalData)
            })
            {
                CollectionAssert.Contains(dataMethods, methodName, methodName);
                CollectionAssert.Contains(dataMethods, methodName + "Async", methodName + "Async");
            }
        }

        [TestMethod]
        public void ApprovalApiUsesAllOfficialPaths()
        {
            var root = FindRepositoryRoot();
            var oaSource = File.ReadAllText(Path.Combine(root,
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "OA", "OaApi.cs"));
            var dataSource = File.ReadAllText(Path.Combine(root,
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "OaDataOpen", "OaDataOpenApi.cs"));
            var source = oaSource + dataSource;

            foreach (var path in new[]
            {
                "/cgi-bin/oa/gettemplatedetail", "/cgi-bin/oa/applyevent",
                "/cgi-bin/oa/getapprovalinfo", "/cgi-bin/oa/getapprovaldetail",
                "/cgi-bin/oa/vacation/getcorpconf", "/cgi-bin/oa/vacation/getuservacationquota",
                "/cgi-bin/oa/vacation/setoneuserquota", "/cgi-bin/oa/approval/create_template",
                "/cgi-bin/oa/approval/update_template", "/cgi-bin/oa/approval/copytemplate",
                "/cgi-bin/corp/getapprovaldata",
                "/cgi-bin/corp/getopenapprovaldata"
            })
            {
                StringAssert.Contains(source, path);
            }
        }

        [TestMethod]
        public void CopyTemplateUsesOfficialFieldsAndCompleteComments()
        {
            var requestJson = JsonSerializer.Serialize(new ApprovalCopyTemplateRequest
            {
                open_template_id = "open-template-1"
            });
            var result = JsonSerializer.Deserialize<ApprovalCopyTemplateResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"template_id\":\"template-1\"}");
            var root = FindRepositoryRoot();
            var apiSource = File.ReadAllText(Path.Combine(root, "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "OA", "OaApi.cs"));
            var modelSource = File.ReadAllText(Path.Combine(root, "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "OA", "OAJson", "ApprovalCopyTemplateJson.cs"));

            Assert.AreEqual("{\"open_template_id\":\"open-template-1\"}", requestJson);
            Assert.IsNotNull(result);
            Assert.AreEqual("template-1", result.template_id);
            Assert.AreEqual(2, CountOccurrences(apiSource, "/document/path/92630"));
            Assert.AreEqual(4, CountOccurrences(modelSource, "/// <summary>"));
            Assert.IsFalse(typeof(ApprovalCopyTemplateRequest).GetProperties()
                .Any(property => property.PropertyType == typeof(object)));
            Assert.IsFalse(typeof(ApprovalCopyTemplateResult).GetProperties(BindingFlags.Public |
                    BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Any(property => property.PropertyType == typeof(object)));
        }

        [TestMethod]
        public void SystemApprovalCallbackParsesStringNumberAndCurrentProcessList()
        {
            var document = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[ww-corp]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>4294967296</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[sys_approval_change]]></Event>
<AgentID>3010040</AgentID>
<ApprovalInfo>
  <SpNoStr><![CDATA[202607240001]]></SpNoStr>
  <SpNo>202607240001</SpNo>
  <SpName><![CDATA[采购审批]]></SpName>
  <SpStatus>1</SpStatus>
  <TemplateId><![CDATA[template-1]]></TemplateId>
  <ApplyTime>1774540800</ApplyTime>
  <Applyer><UserId><![CDATA[zhangsan]]></UserId><Party><![CDATA[1]]></Party></Applyer>
  <ProcessList>
    <NodeList>
      <NodeType>1</NodeType><SpStatus>1</SpStatus><ApvRel>3</ApvRel>
      <SubNodeList>
        <UserInfo><UserId><![CDATA[lisi]]></UserId></UserInfo>
        <Speech><![CDATA[同意]]></Speech><SpYj>2</SpYj>
        <Sptime>4294967296</Sptime><MediaIds><![CDATA[media-1]]></MediaIds>
      </SubNodeList>
    </NodeList>
  </ProcessList>
  <StatuChangeEvent>13</StatuChangeEvent>
</ApprovalInfo>
</xml>");
            var callback = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), document)
                as RequestMessageEvent_SysApprovalChange;

            Assert.IsNotNull(callback);
            Assert.AreEqual(Event.SYS_APPROVAL_CHANGE, callback.Event);
            Assert.AreEqual("202607240001", callback.ApprovalInfo.SpNoStr);
            Assert.AreEqual((byte)13, callback.ApprovalInfo.StatuChangeEvent);
            Assert.AreEqual((byte)3, callback.ApprovalInfo.ProcessList.NodeLists[0].ApvRel);
            Assert.AreEqual("lisi", callback.ApprovalInfo.ProcessList.NodeLists[0]
                .SubNodeLists[0].UserInfo.UserId);
            Assert.AreEqual(4294967296UL, callback.ApprovalInfo.ProcessList.NodeLists[0]
                .SubNodeLists[0].Sptime);
        }

        [TestMethod]
        public void OpenApprovalCallbackPreservesLargeOperationTimeCompatibly()
        {
            var document = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[ww-corp]]></ToUserName>
<FromUserName><![CDATA[sys]]></FromUserName>
<CreateTime>4294967296</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[open_approval_change]]></Event>
<AgentID>3010040</AgentID>
<ApprovalInfo>
  <ThirdNo><![CDATA[third-1]]></ThirdNo><OpenSpName><![CDATA[付款]]></OpenSpName>
  <OpenTemplateId><![CDATA[template-1]]></OpenTemplateId><OpenSpStatus>1</OpenSpStatus>
  <ApplyTime>1774540800</ApplyTime><ApplyUserName><![CDATA[张三]]></ApplyUserName>
  <ApplyUserId><![CDATA[zhangsan]]></ApplyUserId><ApplyUserParty><![CDATA[产品部]]></ApplyUserParty>
  <ApprovalNodes><ApprovalNode><NodeStatus>1</NodeStatus><NodeAttr>1</NodeAttr><NodeType>1</NodeType>
    <Items><Item><ItemName><![CDATA[李四]]></ItemName><ItemUserid><![CDATA[lisi]]></ItemUserid>
      <ItemStatus>2</ItemStatus><ItemOpTime>4294967296</ItemOpTime></Item></Items>
  </ApprovalNode></ApprovalNodes><ApproverStep>0</ApproverStep>
</ApprovalInfo>
</xml>");
            var callback = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), document)
                as RequestMessageEvent_OpenApprovalChange;

            Assert.IsNotNull(callback);
            var item = callback.ApprovalInfo.ApprovalNodes[0].Items[0];
            Assert.AreEqual(4294967296UL, item.ItemOpTimeTimestamp);
            Assert.AreEqual(byte.MaxValue, item.ItemOpTime);
        }

        [TestMethod]
        public void TemplateModelsCoverCurrentConfigsAndRichTips()
        {
            var result = JsonSerializer.Deserialize<GetTemplateDetailResult>(
                "{\"errcode\":0,\"template_names\":[{\"text\":\"采购\",\"lang\":\"zh_CN\"}]," +
                "\"template_content\":{\"controls\":[{\"property\":{\"control\":\"Selector\",\"id\":\"selector-1\"}," +
                "\"config\":{\"date\":{\"type\":\"day\"},\"contact\":{\"type\":\"multi\",\"mode\":\"user\"}," +
                "\"selector\":{\"type\":\"single\",\"op_relations\":[{\"key\":\"option-1\"," +
                "\"relation_list\":[{\"related_control_id\":\"text-1\",\"action\":1}]}]}," +
                "\"table\":{\"children\":[{\"property\":{\"control\":\"Text\",\"id\":\"text-1\"}}]}," +
                "\"attendance\":{\"type\":3,\"date_range\":{\"type\":\"hour\"}}," +
                "\"vacation_list\":{\"item\":[{\"id\":1,\"name\":[{\"text\":\"年假\",\"lang\":\"zh_CN\"}]}]}," +
                "\"tips\":{\"tips_content\":[{\"text\":{\"sub_text\":[{\"type\":2,\"content\":{" +
                "\"link\":{\"title\":\"制度\",\"url\":\"https://work.weixin.qq.com\"}}}]},\"lang\":\"zh_CN\"}]}}}]}}");
            var config = result.template_content.controls[0].config;

            Assert.AreEqual("day", config.date.type);
            Assert.AreEqual("user", config.contact.mode);
            Assert.AreEqual("text-1", config.selector.op_relations[0].relation_list[0].related_control_id);
            Assert.AreEqual("Text", config.table.children[0].property.control);
            Assert.AreEqual("hour", config.attendance.date_range.type);
            Assert.AreEqual("年假", config.vacation_list.item[0].name[0].text);
            Assert.AreEqual("制度", config.tips.tips_content[0].text.sub_text[0].content.link.title);

            var requestJson = JsonSerializer.Serialize(new ApprovalCreateTemplateRequest
            {
                template_name = new List<ApprovalCreateTemplateRequest_TextAndLang>
                {
                    new ApprovalCreateTemplateRequest_TextAndLang { text = "采购", lang = "zh_CN" }
                },
                template_content = new ApprovalCreateTemplateRequest_TemplateContent
                {
                    controls = new List<ApprovalCreateTemplateRequest_TemplateContent_Controls>
                    {
                        new ApprovalCreateTemplateRequest_TemplateContent_Controls
                        {
                            property = new ApprovalCreateTemplateRequest_TemplateContent_Controls_Property
                            {
                                control = "Tips", id = "tips-1"
                            },
                            config = new ApprovalCreateTemplateRequest_TemplateContent_Controls_Config
                            {
                                tips = new ApprovalTemplateTipsConfig
                                {
                                    tips_content = new List<ApprovalTemplateTipsContent>
                                    {
                                        new ApprovalTemplateTipsContent
                                        {
                                            lang = "zh_CN",
                                            text = new ApprovalTemplateRichText
                                            {
                                                sub_text = new List<ApprovalTemplateRichTextSegment>
                                                {
                                                    new ApprovalTemplateRichTextSegment
                                                    {
                                                        type = 1,
                                                        content = new ApprovalTemplateRichTextSegmentContent
                                                        {
                                                            plain_text = new ApprovalTemplatePlainText
                                                            {
                                                                content = "请阅读制度"
                                                            }

                                                            [TestMethod]
                                                            public void GetApprovalInfoResultSupportsPaginationCursor()
                                                            {
                                                                var result = JsonSerializer.Deserialize<GetApprovalInfoResult>(
                                                                    "{\"errcode\":0,\"errmsg\":\"ok\",\"sp_no_list\":[\"202608050001\"],\"next_cursor\":100}");

                                                                Assert.IsNotNull(result);
                                                                Assert.AreEqual(1, result.sp_no_list.Count);
                                                                Assert.AreEqual("202608050001", result.sp_no_list[0]);
                                                                Assert.AreEqual(100, result.next_cursor);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });

            Assert.IsTrue(requestJson.Contains("\"tips_content\""));
            Assert.IsTrue(requestJson.Contains("\"plain_text\""));
            using (var requestDocument = JsonDocument.Parse(requestJson))
            {
                var plainText = requestDocument.RootElement
                    .GetProperty("template_content").GetProperty("controls")[0]
                    .GetProperty("config").GetProperty("tips").GetProperty("tips_content")[0]
                    .GetProperty("text").GetProperty("sub_text")[0]
                    .GetProperty("content").GetProperty("plain_text").GetProperty("content")
                    .GetString();
                Assert.AreEqual("请阅读制度", plainText);
            }
        }

        private static int CountOccurrences(string source, string value)
            => source.Split(new[] { value }, StringSplitOptions.None).Length - 1;

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
