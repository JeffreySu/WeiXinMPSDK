using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Meeting;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Meeting
{
    [TestClass]
    public class MeetingMraContractTests
    {
        [TestMethod]
        public void MraApiContainsFourSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(MeetingApi.GetMeetingMraStatus), typeof(GetMeetingMraStatusRequest),
                    typeof(GetMeetingMraStatusResult)),
                (nameof(MeetingApi.SetMeetingMraDefaultLayout), typeof(SetMeetingMraDefaultLayoutRequest),
                    typeof(SetMeetingMraDefaultLayoutResult)),
                (nameof(MeetingApi.SetMeetingMraRaiseHand), typeof(SetMeetingMraRaiseHandRequest),
                    typeof(SetMeetingMraRaiseHandResult)),
                (nameof(MeetingApi.HangupMeetingMra), typeof(HangupMeetingMraRequest),
                    typeof(HangupMeetingMraResult))
            };

            foreach (var contract in contracts)
            {
                var parameterTypes = new[] { typeof(string), contract.Item2, typeof(int) };
                var syncMethod = typeof(MeetingApi).GetMethod(contract.Item1, parameterTypes);
                var asyncMethod = typeof(MeetingApi).GetMethod(contract.Item1 + "Async", parameterTypes);

                Assert.IsNotNull(syncMethod, contract.Item1);
                Assert.AreEqual(contract.Item3, syncMethod.ReturnType, contract.Item1);
                Assert.IsNotNull(asyncMethod, contract.Item1 + "Async");
                Assert.AreEqual(typeof(Task<>).MakeGenericType(contract.Item3), asyncMethod.ReturnType,
                    contract.Item1 + "Async");
            }
        }

        [TestMethod]
        public void MraApiUsesOfficialPathsDocumentsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingApi.Mra.cs"));
            var paths = new[]
            {
                "/cgi-bin/meeting/mra/query_status",
                "/cgi-bin/meeting/mra/set_default_layout",
                "/cgi-bin/meeting/mra/set_raise_hand",
                "/cgi-bin/meeting/mra/hangup"
            };
            var documentIds = new[] { 98786, 98787, 98788, 98789 };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            foreach (var documentId in documentIds)
            {
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + documentId),
                    documentId.ToString());
            }

            Assert.AreEqual(8, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(8, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(8, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(8, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(8, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(4, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(4, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void MraStatusPreservesRolesStatesAndOptionalWebinarRole()
        {
            var result = JsonSerializer.Deserialize<GetMeetingMraStatusResult>(
                "{\"errcode\":0,\"tmp_openid\":\"tmp-mra\",\"instance_id\":9,\"user_role\":2," +
                "\"webinar_member_role\":3,\"ip\":\"192.0.2.1\",\"name\":\"会议室连接器\"," +
                "\"audio_state\":true,\"video_state\":false,\"screen_shared_state\":true," +
                "\"default_layout\":4,\"raise_hands_state\":false}");

            Assert.IsNotNull(result);
            Assert.AreEqual("tmp-mra", result.tmp_openid);
            Assert.AreEqual(9, result.instance_id);
            Assert.AreEqual(2, result.user_role);
            Assert.AreEqual(3, result.webinar_member_role);
            Assert.AreEqual("192.0.2.1", result.ip);
            Assert.IsTrue(result.audio_state);
            Assert.IsFalse(result.video_state);
            Assert.IsTrue(result.screen_shared_state);
            Assert.AreEqual(4, result.default_layout);
            Assert.IsFalse(result.raise_hands_state);
        }

        [TestMethod]
        public void MraControlRequestsPreserveNestedTargetAndBooleanState()
        {
            var layout = new SetMeetingMraDefaultLayoutRequest
            {
                meetingid = "meeting-1",
                default_layout = 4,
                default_novideo_user = 2,
                mra = new MeetingMraTarget { tmp_openid = "tmp-mra" }
            };
            var raiseHand = new SetMeetingMraRaiseHandRequest
            {
                meetingid = "meeting-1",
                raise_hand = true,
                mra = new MeetingMraTarget { tmp_openid = "tmp-mra" }
            };
            var hangup = new HangupMeetingMraRequest
            {
                meetingid = "meeting-1",
                mra = new MeetingMraTarget { tmp_openid = "tmp-mra" }
            };

            using var layoutDocument = JsonDocument.Parse(JsonSerializer.Serialize(layout));
            using var raiseHandDocument = JsonDocument.Parse(JsonSerializer.Serialize(raiseHand));
            using var hangupDocument = JsonDocument.Parse(JsonSerializer.Serialize(hangup));

            Assert.AreEqual(4, layoutDocument.RootElement.GetProperty("default_layout").GetInt32());
            Assert.AreEqual(2, layoutDocument.RootElement.GetProperty("default_novideo_user").GetInt32());
            Assert.AreEqual("tmp-mra", layoutDocument.RootElement.GetProperty("mra")
                .GetProperty("tmp_openid").GetString());
            Assert.AreEqual(JsonValueKind.True,
                raiseHandDocument.RootElement.GetProperty("raise_hand").ValueKind);
            Assert.AreEqual("tmp-mra", hangupDocument.RootElement.GetProperty("mra")
                .GetProperty("tmp_openid").GetString());
        }

        [TestMethod]
        public void MraPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(MeetingMraTarget), typeof(GetMeetingMraStatusRequest),
                typeof(GetMeetingMraStatusResult), typeof(SetMeetingMraDefaultLayoutRequest),
                typeof(SetMeetingMraDefaultLayoutResult), typeof(SetMeetingMraRaiseHandRequest),
                typeof(SetMeetingMraRaiseHandResult), typeof(HangupMeetingMraRequest),
                typeof(HangupMeetingMraResult)
            };

            foreach (var property in modelTypes.SelectMany(type => type.GetProperties(BindingFlags.Public |
                         BindingFlags.Instance | BindingFlags.DeclaredOnly)))
            {
                Assert.AreNotEqual(typeof(object), property.PropertyType,
                    property.DeclaringType?.Name + "." + property.Name);
            }

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingMraJson.cs"));
            var declarationCount = source.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
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
