using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.DataIntelligence
{
    [TestClass]
    public class ChatDataContractTests
    {
        [TestMethod]
        public void ChatDataApiProvidesThirtyOneCurrentSyncAndAsyncEntries()
        {
            var methodNames = typeof(ChatDataApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            var syncMethods = new[]
            {
                nameof(ChatDataApi.GetAuthorizedUserList),
                nameof(ChatDataApi.GetCorpAuthorization),
                nameof(ChatDataApi.SetPublicKey),
                nameof(ChatDataApi.SetReceiveCallback),
                nameof(ChatDataApi.SetLogLevel),
                nameof(ChatDataApi.UploadMedia),
                nameof(ChatDataApi.SyncMessages),
                nameof(ChatDataApi.GetGroupChat),
                nameof(ChatDataApi.GetSingleAgreeStatus),
                nameof(ChatDataApi.GetRoomAgreeStatus),
                nameof(ChatDataApi.AddAnalyzeTask),
                nameof(ChatDataApi.SubmitAnalyzeTask),
                nameof(ChatDataApi.GetAnalyzeTaskResult),
                nameof(ChatDataApi.OpenDebugMode),
                nameof(ChatDataApi.CloseDebugMode),
                nameof(ChatDataApi.GetDebugMode),
                nameof(ChatDataApi.CreateExportJob),
                nameof(ChatDataApi.GetExportJobStatus),
                nameof(ChatDataApi.SetSensitiveInfoConfig),
                nameof(ChatDataApi.GetSensitiveInfoConfig),
                nameof(ChatDataApi.CreateKeywordRule),
                nameof(ChatDataApi.UpdateKeywordRule),
                nameof(ChatDataApi.DeleteKeywordRule),
                nameof(ChatDataApi.GetKeywordRuleList),
                nameof(ChatDataApi.GetKeywordRuleDetail),
                nameof(ChatDataApi.GetKeywordHitMessageList),
                nameof(ChatDataApi.SyncCallProgram),
                nameof(ChatDataApi.CreateAsyncProgramTask),
                nameof(ChatDataApi.GetAsyncProgramResult),
                nameof(ChatDataApi.SearchChat),
                nameof(ChatDataApi.SearchMessage)
            };

            foreach (var methodName in syncMethods)
            {
                CollectionAssert.Contains(methodNames, methodName, methodName);
                CollectionAssert.Contains(methodNames, methodName + "Async", methodName + "Async");
            }
        }

        [TestMethod]
        public void ChatDataApiUsesCurrentOfficialPathsAndMultipartContract()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "DataIntelligence", "ChatDataApi.cs"));
            var paths = new[]
            {
                "/cgi-bin/chatdata/get_auth_user_list",
                "/cgi-bin/chatdata/get_corp_auth_info",
                "/cgi-bin/chatdata/set_public_key",
                "/cgi-bin/chatdata/set_receive_callback",
                "/cgi-bin/chatdata/set_log_level",
                "/cgi-bin/chatdata/upload_media",
                "/cgi-bin/chatdata/sync_msg",
                "/cgi-bin/chatdata/groupchat/get",
                "/cgi-bin/chatdata/getagreestatus/single",
                "/cgi-bin/chatdata/getagreestatus/room",
                "/cgi-bin/chatdata/analyze_task_add",
                "/cgi-bin/chatdata/analyze_task_submit",
                "/cgi-bin/chatdata/analyze_task_result",
                "/cgi-bin/chatdata/open_debug_mode",
                "/cgi-bin/chatdata/close_debug_mode",
                "/cgi-bin/chatdata/check_debug_mode",
                "/cgi-bin/chatdata/export/create_job",
                "/cgi-bin/chatdata/export/get_job_status",
                "/cgi-bin/chatdata/set_hide_sensitiveinfo_config",
                "/cgi-bin/chatdata/get_hide_sensitiveinfo_config",
                "/cgi-bin/chatdata/keyword/create_rule",
                "/cgi-bin/chatdata/keyword/update_rule",
                "/cgi-bin/chatdata/keyword/delete_rule",
                "/cgi-bin/chatdata/keyword/get_rule_list",
                "/cgi-bin/chatdata/keyword/get_rule_detail",
                "/cgi-bin/chatdata/keyword/get_hit_msg_list",
                "/cgi-bin/chatdata/sync_call_program",
                "/cgi-bin/chatdata/async_program_task",
                "/cgi-bin/chatdata/async_program_result",
                "/cgi-bin/chatdata/search_chat",
                "/cgi-bin/chatdata/search_msg"
            };

            foreach (var path in paths)
            {
                StringAssert.Contains(source, path);
            }

            StringAssert.Contains(source, "CommonJsonSendType.POST");
            StringAssert.Contains(source, "[\"name\"] = \"media\"");
            StringAssert.Contains(source, "&type=");
            Assert.IsFalse(source.Contains("/cgi-bin/chatdata/specapi/"),
                "2024-06-06 已下线的 specapi 不应新增为公开入口。");
        }

        [TestMethod]
        public void ChatDataRequestsPreserveProtocolFieldNames()
        {
            var publicKeyJson = JsonSerializer.Serialize(new ChatDataSetPublicKeyRequest
            {
                public_key = "PUBLIC KEY",
                public_key_ver = 2
            });
            var callJson = JsonSerializer.Serialize(new ChatDataProgramCallRequest
            {
                program_id = "program-1",
                ability_id = "ability-1",
                notify_id = "notification-1",
                request_data = "{\"input\":1}"
            });
            var syncJson = JsonSerializer.Serialize(new ChatDataSyncMessagesRequest
            {
                token = "token-1",
                cursor = "cursor-1",
                limit = 200
            });

            StringAssert.Contains(publicKeyJson, "\"public_key_ver\":2");
            StringAssert.Contains(callJson, "\"program_id\":\"program-1\"");
            StringAssert.Contains(callJson, "\"ability_id\":\"ability-1\"");
            StringAssert.Contains(callJson, "\"notify_id\":\"notification-1\"");
            StringAssert.Contains(syncJson, "\"cursor\":\"cursor-1\"");
        }

        [TestMethod]
        public void ChatDataModelsPreserveAuthorizationAndMessageContracts()
        {
            var authorization = JsonSerializer.Deserialize<ChatDataCorpAuthorizationResult>(
                "{\"errcode\":0,\"auth_edition_list\":[{\"edition\":2,\"status\":1," +
                "\"begin_time\":5178368698,\"end_time\":6178368698,\"msg_duration_days\":90," +
                "\"auth_user_count\":23,\"auth_scope\":{\"userid_list\":[\"u1\"]," +
                "\"department_id_list\":[5000000000],\"tag_id_list\":[6000000000]}}]}" );
            var messages = JsonSerializer.Deserialize<ChatDataSyncMessagesResult>(
                "{\"errcode\":0,\"has_more\":true,\"next_cursor\":\"next\"," +
                "\"msg_list\":[{\"msgid\":\"msg-1\",\"msgtype\":2," +
                "\"sender\":{\"type\":1,\"id\":\"u1\"},\"send_time\":5178368698," +
                "\"service_encrypt_info\":{\"encrypted_secret_key\":\"key\"," +
                "\"public_key_ver\":2}}]}" );

            Assert.IsNotNull(authorization);
            Assert.AreEqual(5000000000L,
                authorization.auth_edition_list[0].auth_scope.department_id_list[0]);
            Assert.AreEqual(6178368698L, authorization.auth_edition_list[0].end_time);
            Assert.IsNotNull(messages);
            Assert.AreEqual(5178368698L, messages.msg_list[0].send_time);
            Assert.AreEqual(2, messages.msg_list[0].service_encrypt_info.public_key_ver);
        }

        [TestMethod]
        public void ChatDataComplianceAndKeywordModelsPreserveProtocolContracts()
        {
            var requestJson = JsonSerializer.Serialize(new ChatDataSetSensitiveInfoConfigRequest
            {
                open_userid = "open-user-1",
                config = new ChatDataSensitiveInfoConfig
                {
                    hide_mobile = true,
                    hide_idcard = false,
                    hide_bankno = true
                }
            });
            var keywordJson = JsonSerializer.Serialize(new ChatDataKeywordRuleCreateRequest
            {
                name = "rule-1",
                keyword = new ChatDataKeywordWords { word_list = new[] { "secret" } },
                applicable_range = new ChatDataKeywordApplicableRange
                {
                    department = new ChatDataKeywordDepartmentList
                    {
                        id_list = new[] { 5000000000L }
                    }
                }
            });
            var result = JsonSerializer.Deserialize<ChatDataSingleAgreeStatusResult>(
                "{\"errcode\":0,\"agreeinfo\":[{\"open_userid\":\"u1\"," +
                "\"agree_status\":\"Agree\",\"status_change_time\":5178368698}]}" );

            StringAssert.Contains(requestJson, "\"open_userid\":\"open-user-1\"");
            StringAssert.Contains(requestJson, "\"hide_bankno\":true");
            StringAssert.Contains(keywordJson, "\"word_list\":[\"secret\"]");
            StringAssert.Contains(keywordJson, "\"id_list\":[5000000000]");
            Assert.IsNotNull(result);
            Assert.AreEqual(5178368698L, result.agreeinfo[0].status_change_time);
        }

        [TestMethod]
        public void ChatDataSearchModelsUseSixtyFourBitTimeRange()
        {
            var request = new ChatDataSearchMessageRequest
            {
                query_word = "keyword",
                start_time = 5178368698L,
                end_time = 6178368698L,
                chat_info = new ChatDataSearchMessageChatInfo
                {
                    chat_type = 1,
                    id_list = new[]
                    {
                        new ChatDataSearchMessageUser { open_userid = "open-user-1" }
                    }
                }
            };
            var json = JsonSerializer.Serialize(request);

            StringAssert.Contains(json, "\"start_time\":5178368698");
            StringAssert.Contains(json, "\"end_time\":6178368698");
            StringAssert.Contains(json, "\"chat_info\"");
            StringAssert.Contains(json, "\"open_userid\":\"open-user-1\"");
        }

        [TestMethod]
        public void ChatDataModelsDoNotExposeUntypedObjectProperties()
        {
            var modelTypes = typeof(ChatDataAuthorizedUserListRequest).Assembly.GetTypes()
                .Where(type => type.IsClass && type.IsPublic &&
                               type.Namespace == typeof(ChatDataAuthorizedUserListRequest).Namespace &&
                               type.Name.StartsWith("ChatData", StringComparison.Ordinal));

            foreach (var modelType in modelTypes)
            {
                var objectProperty = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                                             BindingFlags.DeclaredOnly)
                    .FirstOrDefault(property => property.PropertyType == typeof(object));
                Assert.IsNull(objectProperty, $"{modelType.FullName}.{objectProperty?.Name}");
            }
        }

        [TestMethod]
        public void LegacyDataIntelligenceEntriesRemainAvailable()
        {
            var legacyMethods = typeof(DataIntelligenceApi)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            CollectionAssert.Contains(legacyMethods, nameof(DataIntelligenceApi.GetConversationRecords));
            CollectionAssert.Contains(legacyMethods, nameof(DataIntelligenceApi.GetConversationRecordsAsync));
            CollectionAssert.Contains(legacyMethods, nameof(DataIntelligenceApi.GetMessageStatistics));
            CollectionAssert.Contains(legacyMethods, nameof(DataIntelligenceApi.GetMessageStatisticsAsync));
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
