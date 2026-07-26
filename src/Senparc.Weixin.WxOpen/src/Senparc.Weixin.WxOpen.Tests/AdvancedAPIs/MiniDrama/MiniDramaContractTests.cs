using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.WxOpen.AdvancedAPIs.MiniDrama;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.MessageContexts;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.MiniDrama
{
    [TestClass]
    public class MiniDramaContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(MiniDramaApi.SingleFileUpload)] = "/wxa/sec/vod/singlefileupload",
                [nameof(MiniDramaApi.PullUpload)] = "/wxa/sec/vod/pullupload",
                [nameof(MiniDramaApi.GetTask)] = "/wxa/sec/vod/gettask",
                [nameof(MiniDramaApi.ApplyUpload)] = "/wxa/sec/vod/applyupload",
                [nameof(MiniDramaApi.UploadPart)] = "/wxa/sec/vod/uploadpart",
                [nameof(MiniDramaApi.CommitUpload)] = "/wxa/sec/vod/commitupload",
                [nameof(MiniDramaApi.ListMedia)] = "/wxa/sec/vod/listmedia",
                [nameof(MiniDramaApi.GetMedia)] = "/wxa/sec/vod/getmedia",
                [nameof(MiniDramaApi.GetMediaLink)] = "/wxa/sec/vod/getmedialink",
                [nameof(MiniDramaApi.DeleteMedia)] = "/wxa/sec/vod/deletemedia",
                [nameof(MiniDramaApi.AuditDrama)] = "/wxa/sec/vod/auditdrama",
                [nameof(MiniDramaApi.ListDramas)] = "/wxa/sec/vod/listdramas",
                [nameof(MiniDramaApi.GetDrama)] = "/wxa/sec/vod/getdrama",
                [nameof(MiniDramaApi.SubmitReplaceDramaMedias)] = "/wxa/sec/vod/submitreplacedramamedias",
                [nameof(MiniDramaApi.ReplaceDramaMedia)] = "/wxa/sec/vod/replacedramamedia",
                [nameof(MiniDramaApi.ModifyDramaBasicInfo)] = "/wxa/sec/vod/modifydramabasicinfo",
                [nameof(MiniDramaApi.GetDramaLatestAuditInfo)] = "/wxa/sec/vod/getdramalatestauditinfo",
                [nameof(MiniDramaApi.GetCdnUsageData)] = "/wxa/sec/vod/getcdnusagedata",
                [nameof(MiniDramaApi.GetCdnLogs)] = "/wxa/sec/vod/getcdnlogs",
                [nameof(MiniDramaApi.ListPackages)] = "/wxa/sec/vod/listpackages",
                [nameof(MiniDramaApi.GetAuthorizedObjects)] = "/wxa/sec/vod/getauthorizedobjects",
                [nameof(MiniDramaApi.AuthorizeDrama)] = "/wxa/sec/vod/authorizedrama",
                [nameof(MiniDramaApi.DeauthorizeDrama)] = "/wxa/sec/vod/deauthorizedrama",
                [nameof(MiniDramaApi.GetAuthorizeObjects)] = "/wxa/sec/vod/getauthorizeobjects",
                [nameof(MiniDramaApi.AuthorizeApp)] = "/wxa/sec/vod/authorizeapp",
                [nameof(MiniDramaApi.DeauthorizeApp)] = "/wxa/sec/vod/deauthorizeapp",
                [nameof(MiniDramaApi.GetAuthorizeApps)] = "/wxa/sec/vod/getauthorizeapps",
                [nameof(MiniDramaApi.AuthorizeCopyright)] = "/wxa/sec/vod/authorizecopyright",
                [nameof(MiniDramaApi.DeauthorizeCopyright)] = "/wxa/sec/vod/deauthorizecopyright",
                [nameof(MiniDramaApi.GetCopyrightAuthorizationList)] = "/wxa/sec/vod/getcopyrightauthorizationlist",
                [nameof(MiniDramaApi.GetCopyrightAuthorizedList)] = "/wxa/sec/vod/getcopyrightauthorizedlist",
                [nameof(MiniDramaApi.SetPlayerDramaRecommendedSwitch)] = "/wxadrama/setplayerdramarecmdswitch",
                [nameof(MiniDramaApi.SetFlushDrama)] = "/wxadrama/developersetflushdrama",
                [nameof(MiniDramaApi.SetRecommendedDrama)] = "/wxadrama/developersetrecmddrama",
                [nameof(MiniDramaApi.PublishDrama)] = "/wxadrama/developerpublishdrama",
                [nameof(MiniDramaApi.GetPublishedDrama)] = "/wxadrama/developergetpublisheddrama",
                [nameof(MiniDramaApi.SetMonetization)] = "/wxadrama/developersetiaadrama",
                [nameof(MiniDramaApi.GetMonetization)] = "/wxadrama/developergetiaadrama",
                [nameof(MiniDramaApi.BatchProcessPromotion)] = "/wxadrama/batchprocessdramapromotion",
                [nameof(MiniDramaApi.GetFinderEvent)] = "/wxadrama/getfinderevent"
            };

        [TestMethod]
        public void ApiSurfaceContainsFortyUniqueSyncAndAsyncEntries()
        {
            var methods = typeof(MiniDramaApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name)
                .ToArray();

            Assert.AreEqual(40, OfficialEndpoints.Count, "官方 41 个目录项包含一项重复路径，应为 40 个唯一接口。");
            foreach (var method in OfficialEndpoints.Keys)
            {
                CollectionAssert.Contains(methods, method);
                CollectionAssert.Contains(methods, method + "Async");
            }

            Assert.AreEqual(80, methods.Length);
        }

        [TestMethod]
        public void EveryPublicEntryUsesItsOfficialEndpoint()
        {
            foreach (var pair in OfficialEndpoints)
            {
                var sync = typeof(MiniDramaApi).GetMethod(pair.Key, BindingFlags.Public | BindingFlags.Static);
                var async = typeof(MiniDramaApi).GetMethod(pair.Key + "Async", BindingFlags.Public | BindingFlags.Static);

                CollectionAssert.Contains(GetStringLiterals(sync).ToArray(), pair.Value, pair.Key);
                CollectionAssert.Contains(GetStringLiterals(async).ToArray(), pair.Value, pair.Key + "Async");
            }
        }

        [TestMethod]
        public void MultipartBuildersUseOfficialFieldNamesAndOmitUnsetOptions()
        {
            var filesMethod = typeof(MiniDramaApi).GetMethod("CreateSingleFileUploadFiles", BindingFlags.NonPublic | BindingFlags.Static);
            var fieldsMethod = typeof(MiniDramaApi).GetMethod("CreateSingleFileUploadFields", BindingFlags.NonPublic | BindingFlags.Static);
            var partMethod = typeof(MiniDramaApi).GetMethod("CreateUploadPartFields", BindingFlags.NonPublic | BindingFlags.Static);

            var files = (Dictionary<string, string>)filesMethod.Invoke(null, new object[] { "/tmp/1.mp4", null });
            var fields = (Dictionary<string, string>)fieldsMethod.Invoke(null, new object[] { "测试剧 - 第1集", "MP4", null, null });
            var part = (Dictionary<string, string>)partMethod.Invoke(null, new object[] { "upload-1", 2, 1 });

            CollectionAssert.AreEquivalent(new[] { "media_data" }, files.Keys.ToArray());
            CollectionAssert.AreEquivalent(new[] { "media_name", "media_type" }, fields.Keys.ToArray());
            Assert.AreEqual("2", part["part_number"]);
            Assert.AreEqual("1", part["resource_type"]);
        }

        [TestMethod]
        public void RequestsKeepOfficialSnakeCaseAndOmitUnsetFields()
        {
            var request = new MiniDramaAuditDramaRequest
            {
                name = "测试剧",
                media_count = 1,
                media_id_list = new[] { 20001L },
                replace_media_list = new[] { new MiniDramaReplaceMediaItem { old = 20001, @new = 20002 } },
                copyright = new MiniDramaCopyrightInfo
                {
                    copyright_role = 1,
                    apply_for_copyright_protection = 1,
                    proof_of_production = new[] { "material-1" }
                },
                drama_type = 3,
                content_declared = 1,
                other_platform_publication_proof = new[] { "proof-1" }
            };

            using var document = JsonDocument.Parse(Serialize(request));
            var root = document.RootElement;

            Assert.IsFalse(root.TryGetProperty("drama_id", out _));
            Assert.AreEqual(20002L, root.GetProperty("replace_media_list")[0].GetProperty("new").GetInt64());
            Assert.AreEqual(1, root.GetProperty("copyright").GetProperty("apply_for_copyright_protection").GetInt32());
            Assert.AreEqual(3, root.GetProperty("drama_type").GetInt32());
            Assert.AreEqual("proof-1", root.GetProperty("other_platform_publication_proof")[0].GetString());
        }

        [TestMethod]
        public void MediaAndTaskResponsesMapNestedOfficialExamples()
        {
            const string taskJson = @"{
  ""errcode"":0,""task_info"":{""id"":8412368,""task_type"":1,""status"":3,
  ""create_time"":1682214878,""finish_time"":1682214907,""media_id"":28918028}}
";
            const string mediaJson = @"{
  ""errcode"":0,""media_info"":{""media_id"":28918028,""file_size"":""9849163"",
  ""duration"":120,""name"":""测试剧 - 第1集"",""audit_detail"":{""status"":3,
  ""evidence_material_id_list"":[""material-1""]}}}
";

            var task = JsonConvert.DeserializeObject<MiniDramaGetTaskJsonResult>(taskJson);
            var media = JsonConvert.DeserializeObject<MiniDramaGetMediaJsonResult>(mediaJson);

            Assert.AreEqual(28918028L, task.task_info.media_id);
            Assert.AreEqual(3, task.task_info.status);
            Assert.AreEqual("9849163", media.media_info.file_size);
            Assert.AreEqual("material-1", media.media_info.audit_detail.evidence_material_id_list[0]);
        }

        [TestMethod]
        public void CdnAndPackageModelsFollowExamplesInsteadOfIncorrectTableTypes()
        {
            const string logJson = "{\"errcode\":0,\"total_count\":1,\"domestic_cdn_logs\":[{"
                + "\"date\":\"2024-03-28\",\"name\":\"2024032819-example\","
                + "\"url\":\"https://example.test/log.gz\",\"start_time\":1711623600,"
                + "\"end_time\":1711627199}]}";
            const string packageJson = "{\"errcode\":0,\"package_list\":[{"
                + "\"order_id\":2921020570379436032,\"package_id\":\"ZY2921020620526534656\","
                + "\"all\":100000,\"used\":188}]}";

            var logs = JsonConvert.DeserializeObject<MiniDramaGetCdnLogsJsonResult>(logJson);
            var packages = JsonConvert.DeserializeObject<MiniDramaListPackagesJsonResult>(packageJson);

            Assert.AreEqual("2024-03-28", logs.domestic_cdn_logs[0].date);
            Assert.AreEqual("https://example.test/log.gz", logs.domestic_cdn_logs[0].url);
            Assert.AreEqual("2921020570379436032", packages.package_list[0].order_id);
        }

        [TestMethod]
        public void AuthorizationRequestsUseDocumentedExampleFieldNames()
        {
            var request = new MiniDramaDramaAuthorizationRequest
            {
                authorized_appid = "wx-authorized",
                drama_id = new[] { 100200L, 100205L },
                authz_expire_time = 0
            };
            var copyright = new MiniDramaCopyrightAuthorizationRequest
            {
                authorization_type = 1,
                authorized_subject_cert_no = "91320000TEST",
                drama_ids = new[] { 10001L }
            };

            using var authorizationDocument = JsonDocument.Parse(Serialize(request));
            using var copyrightDocument = JsonDocument.Parse(Serialize(copyright));

            Assert.AreEqual("wx-authorized", authorizationDocument.RootElement.GetProperty("authorized_appid").GetString());
            Assert.IsFalse(authorizationDocument.RootElement.TryGetProperty("authorized", out _));
            Assert.AreEqual("91320000TEST", copyrightDocument.RootElement.GetProperty("authorized_subject_cert_no").GetString());
            Assert.IsFalse(copyrightDocument.RootElement.TryGetProperty("authorized_appid", out _));
        }

        [TestMethod]
        public void PlayerModelsKeepBooleanSwitchAndStringDramaIds()
        {
            var switchRequest = new MiniDramaPlayerSwitchRequest { entry_type = 2002, switch_status = true };
            var promotion = new MiniDramaPromotionRequest
            {
                action_type = 2,
                list = new[] { new MiniDramaPlayerDramaIdentity { src_appid = "wx-source", drama_id = "123456" } }
            };

            using var switchDocument = JsonDocument.Parse(Serialize(switchRequest));
            using var promotionDocument = JsonDocument.Parse(Serialize(promotion));

            Assert.AreEqual(JsonValueKind.True, switchDocument.RootElement.GetProperty("switch_status").ValueKind);
            Assert.AreEqual("123456", promotionDocument.RootElement.GetProperty("list")[0].GetProperty("drama_id").GetString());
        }

        [TestMethod]
        public void MiniDramaEventsMapNestedUploadAndAuditPayloads()
        {
            const string uploadJson = @"{
  ""upload_event"":{""media_id"":20001,""source_context"":""abc12232"",""errcode"":0,""errmsg"":""OK""}}
";
            const string auditJson = @"{
  ""audit_event"":{""drama_id"":20001,""audit_detail"":{""status"":3,""audit_type"":0,
  ""create_time"":168625255,""audit_time"":168626255}}}
";

            var upload = JsonConvert.DeserializeObject<RequestMessageEvent_SecVodUpload>(uploadJson);
            var audit = JsonConvert.DeserializeObject<RequestMessageEvent_SecVodAudit>(auditJson);
            var context = new DefaultWxOpenMessageContext();
            var mappedUpload = context.GetRequestEntityMappingResult(RequestMsgType.Event,
                XDocument.Parse("<xml><Event>secvod_upload_event</Event></xml>"));
            var mappedAudit = context.GetRequestEntityMappingResult(RequestMsgType.Event,
                XDocument.Parse("<xml><Event>secvod_audit_event</Event></xml>"));

            Assert.AreEqual(Event.secvod_upload_event, upload.Event);
            Assert.AreEqual(20001L, upload.upload_event.media_id);
            Assert.AreEqual(Event.secvod_audit_event, audit.Event);
            Assert.AreEqual(3, audit.audit_event.audit_detail.status);
            Assert.IsInstanceOfType(mappedUpload, typeof(RequestMessageEvent_SecVodUpload));
            Assert.IsInstanceOfType(mappedAudit, typeof(RequestMessageEvent_SecVodAudit));
        }

        private static IEnumerable<string> GetStringLiterals(MethodInfo method)
        {
            var bytes = method?.GetMethodBody()?.GetILAsByteArray();
            if (bytes == null)
            {
                yield break;
            }

            for (var index = 0; index <= bytes.Length - 5; index++)
            {
                if (bytes[index] != 0x72)
                {
                    continue;
                }

                string value;
                try
                {
                    value = method.Module.ResolveString(BitConverter.ToInt32(bytes, index + 1));
                }
                catch (ArgumentException)
                {
                    continue;
                }

                yield return value;
            }
        }

        private static string Serialize(object value)
        {
            return SerializerHelper.GetJsonString(value, new JsonSetting(ignoreNulls: true));
        }
    }
}
