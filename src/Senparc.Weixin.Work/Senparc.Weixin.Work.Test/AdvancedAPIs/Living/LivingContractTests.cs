using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Living;
using Senparc.Weixin.Work.AdvancedAPIs.Living.LivingJson;
using Senparc.Weixin.Work.Entities.Request.Event;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Living
{
    [TestClass]
    public class LivingContractTests
    {
        [TestMethod]
        public void LivingApiCoversNineOfficialEndpoints()
        {
            var methods = typeof(LivingApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var methodName in new[]
            {
                nameof(LivingApi.Create), nameof(LivingApi.Modify), nameof(LivingApi.Cancel),
                nameof(LivingApi.DeleteReplayData), nameof(LivingApi.GetLivingCode),
                nameof(LivingApi.GetUserAllLivingid), nameof(LivingApi.GetLivingInfo),
                nameof(LivingApi.GetLivingWatchState), nameof(LivingApi.GetLivingShareInfo)
            })
            {
                CollectionAssert.Contains(methods, methodName, methodName);
                CollectionAssert.Contains(methods, methodName + "Async", methodName + "Async");
            }
        }

        [TestMethod]
        public void LivingApiUsesAllOfficialPaths()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "Living", "LivingApi.cs"));

            foreach (var path in new[]
            {
                "/cgi-bin/living/create", "/cgi-bin/living/modify", "/cgi-bin/living/cancel",
                "/cgi-bin/living/delete_replay_data", "/cgi-bin/living/get_living_code",
                "/cgi-bin/living/get_user_all_livingid", "/cgi-bin/living/get_living_info",
                "/cgi-bin/living/get_watch_stat", "/cgi-bin/living/get_living_share_info"
            })
            {
                StringAssert.Contains(source, path);
            }
        }

        [TestMethod]
        public void CreateAndModifyModelsPreserveOfficialFieldsAndLargeTimestamps()
        {
            var createJson = JsonSerializer.Serialize(new CreateLivingRequest
            {
                anchor_userid = "zhangsan",
                theme = "新品发布",
                living_start = 4294967296L,
                living_duration = 3600,
                type = 4,
                remind_time = 60,
                activity_cover_mediaid = "cover-media",
                activity_share_mediaid = "share-media",
                activity_detail = new LivingActivityDetail
                {
                    description = "活动直播简介",
                    image_list = new List<string> { "media-1", "media-2" }
                }
            });
            var modifyJson = JsonSerializer.Serialize(new ModifyLivingRequest
            {
                livingid = "living-1",
                living_start = 4294967297L,
                living_duration = 7200,
                remind_time = 300
            });
            var result = JsonSerializer.Deserialize<CreateLivingResult>(
                "{\"errcode\":0,\"livingid\":\"living-1\"}");

            StringAssert.Contains(createJson, "\"living_start\":4294967296");
            StringAssert.Contains(createJson, "\"activity_cover_mediaid\":\"cover-media\"");
            StringAssert.Contains(createJson, "\"image_list\":[\"media-1\",\"media-2\"]");
            StringAssert.Contains(modifyJson, "\"living_start\":4294967297");
            Assert.IsNotNull(result);
            Assert.AreEqual("living-1", result.livingid);
        }

        [TestMethod]
        public void WeChatViewingAndShareModelsUseOfficialFields()
        {
            var codeJson = JsonSerializer.Serialize(new GetLivingCodeRequest
            {
                livingid = "living-1",
                openid = "openid-1"
            });
            var shareJson = JsonSerializer.Serialize(new GetLivingShareInfoRequest
            {
                ww_share_code = "share-code"
            });
            var codeResult = JsonSerializer.Deserialize<GetLivingCodeResult>(
                "{\"errcode\":0,\"living_code\":\"living-code\"}");
            var shareResult = JsonSerializer.Deserialize<GetLivingShareInfoResult>(
                "{\"errcode\":0,\"livingid\":\"living-1\"," +
                "\"viewer_userid\":\"viewer-1\",\"viewer_external_userid\":\"external-1\"," +
                "\"invitor_userid\":\"invitor-1\",\"invitor_external_userid\":\"external-2\"}");

            StringAssert.Contains(codeJson, "\"openid\":\"openid-1\"");
            StringAssert.Contains(shareJson, "\"ww_share_code\":\"share-code\"");
            Assert.AreEqual("living-code", codeResult.living_code);
            Assert.AreEqual("viewer-1", shareResult.viewer_userid);
            Assert.AreEqual("external-2", shareResult.invitor_external_userid);
        }

        [TestMethod]
        public void ExistingLivingCallbackStillMapsAndHasHandlerHooks()
        {
            var document = XDocument.Parse(@"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>4294967296</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[living_status_change]]></Event>
<LivingId><![CDATA[living-1]]></LivingId>
<Status>2</Status>
<AgentID>1000001</AgentID>
</xml>");
            var callback = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), document)
                as RequestMessageEvent_Living_Status_Change_Base;
            var handlerMethods = typeof(Senparc.Weixin.Work.MessageHandlers.WorkMessageHandler<>).GetMethods()
                .Select(method => method.Name).ToArray();

            Assert.IsNotNull(callback);
            Assert.AreEqual(Event.LIVING_STATUS_CHANGE, callback.Event);
            Assert.AreEqual("living-1", callback.LivingId);
            Assert.AreEqual(2, callback.Status);
            CollectionAssert.Contains(handlerMethods, "OnEvent_Living_Status_ChangeRequest");
            CollectionAssert.Contains(handlerMethods, "OnEvent_Living_Status_ChangeRequestAsync");
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
