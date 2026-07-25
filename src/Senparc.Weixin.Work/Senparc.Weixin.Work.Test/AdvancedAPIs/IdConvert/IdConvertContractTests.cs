using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.IdConvert;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.IdConvert
{
    [TestClass]
    public class IdConvertContractTests
    {
        [TestMethod]
        public void IdConvertApiContainsSevenSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(IdConvertApi.UnionIdToExternalUserId),
                    typeof(UnionIdToExternalUserIdRequest), typeof(UnionIdToExternalUserIdResult)),
                (nameof(IdConvertApi.BatchExternalUserIdToPendingId),
                    typeof(BatchExternalUserIdToPendingIdRequest),
                    typeof(BatchExternalUserIdToPendingIdResult)),
                (nameof(IdConvertApi.ConvertExternalTagId),
                    typeof(ExternalTagIdConvertRequest), typeof(ExternalTagIdConvertResult)),
                (nameof(IdConvertApi.ConvertOpenKfId),
                    typeof(OpenKfIdConvertRequest), typeof(OpenKfIdConvertResult)),
                (nameof(IdConvertApi.ApplyToUpgradeChatId),
                    typeof(ApplyToUpgradeChatIdRequest), typeof(ApplyToUpgradeChatIdResult)),
                (nameof(IdConvertApi.ConvertChatId),
                    typeof(ChatIdConvertRequest), typeof(ChatIdConvertResult)),
                (nameof(IdConvertApi.UpgradeChatIdForNewCorp),
                    typeof(UpgradeChatIdForNewCorpRequest), typeof(UpgradeChatIdForNewCorpResult))
            };

            foreach (var contract in contracts)
            {
                var parameterTypes = new[] { typeof(string), contract.Item2, typeof(int) };
                var syncMethod = typeof(IdConvertApi).GetMethod(contract.Item1, parameterTypes);
                var asyncMethod = typeof(IdConvertApi).GetMethod(contract.Item1 + "Async", parameterTypes);

                Assert.IsNotNull(syncMethod, contract.Item1);
                Assert.AreEqual(contract.Item3, syncMethod.ReturnType, contract.Item1);
                Assert.IsNotNull(asyncMethod, contract.Item1 + "Async");
                Assert.AreEqual(typeof(Task<>).MakeGenericType(contract.Item3), asyncMethod.ReturnType,
                    contract.Item1 + "Async");
            }
        }

        [TestMethod]
        public void IdConvertApiUsesOfficialPathsTokensAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "IdConvert",
                "IdConvertApi.cs"));
            var paths = new[]
            {
                "/cgi-bin/idconvert/unionid_to_external_userid",
                "/cgi-bin/idconvert/batch/external_userid_to_pending_id",
                "/cgi-bin/idconvert/external_tagid",
                "/cgi-bin/idconvert/open_kfid",
                "/cgi-bin/idconvert/apply_to_upgrade_chatid",
                "/cgi-bin/idconvert/chatid",
                "/cgi-bin/idconvert/upgrade_chatid_for_new_corp"
            };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            Assert.AreEqual(6, CountOccurrences(source, "/document/path/95926"));
            Assert.AreEqual(2, CountOccurrences(source, "/document/path/95900"));
            Assert.AreEqual(4, CountOccurrences(source, "/document/path/96169"));
            Assert.AreEqual(2, CountOccurrences(source, "/document/path/97064"));
            Assert.AreEqual(6, CountOccurrences(source, "/document/path/99601"));
            Assert.AreEqual(2, CountOccurrences(source, "?access_token={0}"));
            Assert.AreEqual(2, CountOccurrences(source, "?suite_access_token="));
            Assert.AreEqual(0, CountOccurrences(source, "Post<UpgradeChatIdForNewCorpResult>"));
            Assert.AreEqual(15, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(12,
                CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(2,
                CountOccurrences(source, "/// <param name=\"suiteAccessToken\">"));
            Assert.AreEqual(14, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(14, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(14, CountOccurrences(source, "/// <returns>"));
        }

        [TestMethod]
        public void IdConvertRequestsPreserveOfficialJsonShapes()
        {
            var unionIdRequest = new UnionIdToExternalUserIdRequest
            {
                unionid = "union-id", openid = "open-id", subject_type = 1
            };
            using var unionIdDocument = JsonDocument.Parse(JsonSerializer.Serialize(unionIdRequest));
            Assert.AreEqual("union-id", unionIdDocument.RootElement.GetProperty("unionid").GetString());
            Assert.AreEqual("open-id", unionIdDocument.RootElement.GetProperty("openid").GetString());
            Assert.AreEqual(1, unionIdDocument.RootElement.GetProperty("subject_type").GetInt32());

            var pendingRequest = new BatchExternalUserIdToPendingIdRequest
            {
                chat_id = "chat-1",
                external_userid = new List<string> { "external-1", "external-2" }
            };
            using var pendingDocument = JsonDocument.Parse(JsonSerializer.Serialize(pendingRequest));
            Assert.AreEqual("chat-1", pendingDocument.RootElement.GetProperty("chat_id").GetString());
            Assert.AreEqual("external-2",
                pendingDocument.RootElement.GetProperty("external_userid")[1].GetString());

            var externalTagRequest = new ExternalTagIdConvertRequest
            {
                external_tagid_list = new List<string> { "tag-1", "tag-2" }
            };
            using var tagDocument = JsonDocument.Parse(JsonSerializer.Serialize(externalTagRequest));
            Assert.AreEqual("tag-2",
                tagDocument.RootElement.GetProperty("external_tagid_list")[1].GetString());

            var openKfRequest = new OpenKfIdConvertRequest
            {
                open_kfid_list = new List<string> { "kf-1", "kf-2" }
            };
            using var kfDocument = JsonDocument.Parse(JsonSerializer.Serialize(openKfRequest));
            Assert.AreEqual("kf-1",
                kfDocument.RootElement.GetProperty("open_kfid_list")[0].GetString());

            var chatRequest = new ChatIdConvertRequest
            {
                chat_id_list = new List<string> { "chat-old-1", "chat-old-2" }
            };
            using var chatDocument = JsonDocument.Parse(JsonSerializer.Serialize(chatRequest));
            Assert.AreEqual("chat-old-2",
                chatDocument.RootElement.GetProperty("chat_id_list")[1].GetString());
        }

        [TestMethod]
        public void IdConvertResultsPreserveMappingsAndInvalidIds()
        {
            var unionIdResult = JsonSerializer.Deserialize<UnionIdToExternalUserIdResult>(
                "{\"errcode\":0,\"external_userid\":\"external-1\",\"pending_id\":\"pending-1\"}");
            var pendingResult = JsonSerializer.Deserialize<BatchExternalUserIdToPendingIdResult>(
                "{\"errcode\":0,\"result\":[{\"external_userid\":\"external-1\"," +
                "\"pending_id\":\"pending-1\"}]}");
            var tagResult = JsonSerializer.Deserialize<ExternalTagIdConvertResult>(
                "{\"errcode\":0,\"items\":[{\"external_tagid\":\"tag-1\"," +
                "\"open_external_tagid\":\"open-tag-1\"}]," +
                "\"invalid_external_tagid_list\":[\"tag-invalid\"]}");
            var kfResult = JsonSerializer.Deserialize<OpenKfIdConvertResult>(
                "{\"errcode\":0,\"items\":[{\"open_kfid\":\"kf-1\"," +
                "\"new_open_kfid\":\"new-kf-1\"}]," +
                "\"invalid_open_kfid_list\":[\"kf-invalid\"]}");
            var chatResult = JsonSerializer.Deserialize<ChatIdConvertResult>(
                "{\"errcode\":0,\"items\":[{\"chat_id\":\"chat-old\"," +
                "\"new_chat_id\":\"chat-new\"}]," +
                "\"invalid_chat_id_list\":[\"chat-invalid\"]}");

            Assert.IsNotNull(unionIdResult);
            Assert.AreEqual("external-1", unionIdResult.external_userid);
            Assert.AreEqual("pending-1", unionIdResult.pending_id);
            Assert.IsNotNull(pendingResult);
            Assert.AreEqual("pending-1", pendingResult.result[0].pending_id);
            Assert.IsNotNull(tagResult);
            Assert.AreEqual("open-tag-1", tagResult.items[0].open_external_tagid);
            Assert.AreEqual("tag-invalid", tagResult.invalid_external_tagid_list[0]);
            Assert.IsNotNull(kfResult);
            Assert.AreEqual("new-kf-1", kfResult.items[0].new_open_kfid);
            Assert.AreEqual("kf-invalid", kfResult.invalid_open_kfid_list[0]);
            Assert.IsNotNull(chatResult);
            Assert.AreEqual("chat-new", chatResult.items[0].new_chat_id);
            Assert.AreEqual("chat-invalid", chatResult.invalid_chat_id_list[0]);
        }

        [TestMethod]
        public void UpgradeTimeUsesSixtyFourBitIntegerAndEmptyRequestsRemainTyped()
        {
            const long timestamp = 6178368698L;
            var request = new ApplyToUpgradeChatIdRequest { upgrade_time = timestamp };
            using var document = JsonDocument.Parse(JsonSerializer.Serialize(request));

            Assert.AreEqual(timestamp, document.RootElement.GetProperty("upgrade_time").GetInt64());
            Assert.AreEqual(typeof(long),
                typeof(ApplyToUpgradeChatIdRequest).GetProperty(nameof(request.upgrade_time))?.PropertyType);
            Assert.AreEqual("{}", JsonSerializer.Serialize(new UpgradeChatIdForNewCorpRequest()));
        }

        [TestMethod]
        public void IdConvertPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(UnionIdToExternalUserIdRequest), typeof(UnionIdToExternalUserIdResult),
                typeof(BatchExternalUserIdToPendingIdRequest), typeof(ExternalUserIdPendingIdItem),
                typeof(BatchExternalUserIdToPendingIdResult), typeof(ExternalTagIdConvertRequest),
                typeof(ExternalTagIdConvertItem), typeof(ExternalTagIdConvertResult),
                typeof(OpenKfIdConvertRequest), typeof(OpenKfIdConvertItem),
                typeof(OpenKfIdConvertResult), typeof(ApplyToUpgradeChatIdRequest),
                typeof(ApplyToUpgradeChatIdResult), typeof(ChatIdConvertRequest),
                typeof(ChatIdConvertItem), typeof(ChatIdConvertResult),
                typeof(UpgradeChatIdForNewCorpRequest), typeof(UpgradeChatIdForNewCorpResult)
            };

            foreach (var property in modelTypes.SelectMany(type => type.GetProperties(BindingFlags.Public |
                         BindingFlags.Instance | BindingFlags.DeclaredOnly)))
            {
                Assert.AreNotEqual(typeof(object), property.PropertyType,
                    property.DeclaringType?.Name + "." + property.Name);
                if (property.PropertyType.IsGenericType)
                {
                    CollectionAssert.DoesNotContain(property.PropertyType.GetGenericArguments(), typeof(object));
                }
            }

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "IdConvert",
                "IdConvertJson.cs"));
            var declarationCount = source.Split(new[] { '\r', '\n' },
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.TrimStart())
                .Count(line => line.StartsWith("public class ", StringComparison.Ordinal) ||
                               line.StartsWith("public ", StringComparison.Ordinal) &&
                               line.Contains("{ get; set; }", StringComparison.Ordinal));
            Assert.AreEqual(declarationCount, CountOccurrences(source, "/// <summary>"));
        }

        private static int CountOccurrences(string source, string value)
            => source.Split(new[] { value }, StringSplitOptions.None).Length - 1;

        private static string FindRepositoryRoot([CallerFilePath] string sourceFilePath = null)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(sourceFilePath));
            while (directory != null)
            {
                if (Directory.Exists(Path.Combine(directory.FullName, ".git")))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            Assert.Fail("Unable to locate repository root.");
            return null;
        }
    }
}
