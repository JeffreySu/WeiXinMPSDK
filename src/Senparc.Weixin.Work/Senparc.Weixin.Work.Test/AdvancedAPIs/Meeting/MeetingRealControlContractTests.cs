using System;
using System.Collections.Generic;
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
    public class MeetingRealControlContractTests
    {
        [TestMethod]
        public void RealControlApiContainsNineSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(MeetingApi.SetMeetingRealControl), typeof(SetMeetingRealControlRequest),
                    typeof(SetMeetingRealControlResult)),
                (nameof(MeetingApi.SetMeetingCoHost), typeof(SetMeetingCoHostRequest),
                    typeof(SetMeetingCoHostResult)),
                (nameof(MeetingApi.MuteMeetingUser), typeof(MuteMeetingUserRequest),
                    typeof(MuteMeetingUserResult)),
                (nameof(MeetingApi.SwitchMeetingUserVideo), typeof(SwitchMeetingUserVideoRequest),
                    typeof(SwitchMeetingUserVideoResult)),
                (nameof(MeetingApi.CloseMeetingScreenShare), typeof(CloseMeetingScreenShareRequest),
                    typeof(CloseMeetingScreenShareResult)),
                (nameof(MeetingApi.SetMeetingNicknames), typeof(SetMeetingNicknamesRequest),
                    typeof(SetMeetingNicknamesResult)),
                (nameof(MeetingApi.ManageMeetingWaitingRoomUsers),
                    typeof(ManageMeetingWaitingRoomUsersRequest),
                    typeof(ManageMeetingWaitingRoomUsersResult)),
                (nameof(MeetingApi.KickoutMeetingUsers), typeof(KickoutMeetingUsersRequest),
                    typeof(KickoutMeetingUsersResult)),
                (nameof(MeetingApi.DismissMeeting), typeof(DismissMeetingRequest),
                    typeof(DismissMeetingResult))
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
        public void RealControlApiUsesOfficialPathsDocumentsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingApi.RealControl.cs"));
            var paths = new[]
            {
                "/cgi-bin/meeting/realcontrol/set",
                "/cgi-bin/meeting/realcontrol/set_cohost",
                "/cgi-bin/meeting/realcontrol/mute_user",
                "/cgi-bin/meeting/realcontrol/switch_user_video",
                "/cgi-bin/meeting/realcontrol/close_screen_share",
                "/cgi-bin/meeting/realcontrol/set_nicknames",
                "/cgi-bin/meeting/realcontrol/manage_waiting_room_users",
                "/cgi-bin/meeting/realcontrol/kickout_users",
                "/cgi-bin/meeting/realcontrol/dismiss"
            };
            var documentIds = new[] { 98175, 98180, 98184, 98189, 98185, 98188, 98186, 98181, 98187 };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            foreach (var documentId in documentIds)
            {
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + documentId),
                    documentId.ToString());
            }

            Assert.AreEqual(18, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(18, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(18, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(18, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(18, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(9, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(9, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void RealControlSettingsPreserveOfficialBooleanAndNumericFields()
        {
            var request = new SetMeetingRealControlRequest
            {
                meetingid = "meeting-1",
                mute_all = true,
                enable_enter_mute = 1,
                allow_unmute_self = true,
                meeting_locked = true,
                hide_meeting_code_password = true,
                allow_chat = 1,
                allow_share_screen = true,
                allow_external_user = true,
                play_ivr_on_join = true,
                enable_waiting_room = true
            };

            using var document = JsonDocument.Parse(JsonSerializer.Serialize(request));
            var root = document.RootElement;
            Assert.AreEqual("meeting-1", root.GetProperty("meetingid").GetString());
            Assert.AreEqual(JsonValueKind.True, root.GetProperty("mute_all").ValueKind);
            Assert.AreEqual(JsonValueKind.Number, root.GetProperty("enable_enter_mute").ValueKind);
            Assert.AreEqual(1, root.GetProperty("enable_enter_mute").GetInt32());
            Assert.AreEqual(JsonValueKind.True, root.GetProperty("allow_unmute_self").ValueKind);
            Assert.AreEqual(JsonValueKind.True, root.GetProperty("meeting_locked").ValueKind);
            Assert.AreEqual(JsonValueKind.True,
                root.GetProperty("hide_meeting_code_password").ValueKind);
            Assert.AreEqual(1, root.GetProperty("allow_chat").GetInt32());
            Assert.AreEqual(JsonValueKind.True, root.GetProperty("allow_share_screen").ValueKind);
            Assert.AreEqual(JsonValueKind.True, root.GetProperty("allow_external_user").ValueKind);
            Assert.AreEqual(JsonValueKind.True, root.GetProperty("play_ivr_on_join").ValueKind);
            Assert.AreEqual(JsonValueKind.True, root.GetProperty("enable_waiting_room").ValueKind);
        }

        [TestMethod]
        public void RealControlMemberOperationsPreserveParticipantsActionsAndDismissFlags()
        {
            var participant = new MeetingRealControlParticipant
            {
                tmp_openid = "tmp-1",
                instance_id = 1
            };
            var coHost = new SetMeetingCoHostRequest
            {
                meetingid = "meeting-1", action = true, operated_user = participant
            };
            var mute = new MuteMeetingUserRequest
            {
                meetingid = "meeting-1", option = true, operated_user = participant
            };
            var video = new SwitchMeetingUserVideoRequest
            {
                meetingid = "meeting-1", video = false, operated_user = participant
            };
            var closeShare = new CloseMeetingScreenShareRequest
            {
                meetingid = "meeting-1", operated_user = participant
            };
            var nicknames = new SetMeetingNicknamesRequest
            {
                meetingid = "meeting-1",
                operated_users = new List<MeetingRealControlNicknameParticipant>
                {
                    new MeetingRealControlNicknameParticipant
                    {
                        tmp_openid = "tmp-1", instance_id = 1, nickname = "主持人"
                    }
                }
            };
            var waitingRoom = new ManageMeetingWaitingRoomUsersRequest
            {
                meetingid = "meeting-1", operate_type = 1, allow_rejoin = true,
                operated_users = new List<MeetingRealControlParticipant> { participant }
            };
            var kickout = new KickoutMeetingUsersRequest
            {
                meetingid = "meeting-1", allow_rejoin = false,
                operated_users = new List<MeetingRealControlParticipant> { participant }
            };
            var dismiss = new DismissMeetingRequest
            {
                meetingid = "meeting-1", force_dismiss = 1, retrieve_code = 0
            };

            using var coHostDocument = JsonDocument.Parse(JsonSerializer.Serialize(coHost));
            using var muteDocument = JsonDocument.Parse(JsonSerializer.Serialize(mute));
            using var videoDocument = JsonDocument.Parse(JsonSerializer.Serialize(video));
            using var closeShareDocument = JsonDocument.Parse(JsonSerializer.Serialize(closeShare));
            using var nicknameDocument = JsonDocument.Parse(JsonSerializer.Serialize(nicknames));
            using var waitingRoomDocument = JsonDocument.Parse(JsonSerializer.Serialize(waitingRoom));
            using var kickoutDocument = JsonDocument.Parse(JsonSerializer.Serialize(kickout));
            using var dismissDocument = JsonDocument.Parse(JsonSerializer.Serialize(dismiss));

            Assert.AreEqual(JsonValueKind.True, coHostDocument.RootElement.GetProperty("action").ValueKind);
            Assert.AreEqual("tmp-1", coHostDocument.RootElement.GetProperty("operated_user")
                .GetProperty("tmp_openid").GetString());
            Assert.AreEqual(JsonValueKind.True, muteDocument.RootElement.GetProperty("option").ValueKind);
            Assert.AreEqual(JsonValueKind.False, videoDocument.RootElement.GetProperty("video").ValueKind);
            Assert.AreEqual(1, closeShareDocument.RootElement.GetProperty("operated_user")
                .GetProperty("instance_id").GetInt32());
            Assert.AreEqual("主持人", nicknameDocument.RootElement.GetProperty("operated_users")[0]
                .GetProperty("nickname").GetString());
            Assert.AreEqual(1, waitingRoomDocument.RootElement.GetProperty("operate_type").GetInt32());
            Assert.AreEqual(JsonValueKind.True,
                waitingRoomDocument.RootElement.GetProperty("allow_rejoin").ValueKind);
            Assert.AreEqual(JsonValueKind.False,
                kickoutDocument.RootElement.GetProperty("allow_rejoin").ValueKind);
            Assert.AreEqual(JsonValueKind.Number,
                dismissDocument.RootElement.GetProperty("force_dismiss").ValueKind);
            Assert.AreEqual(1, dismissDocument.RootElement.GetProperty("force_dismiss").GetInt32());
            Assert.AreEqual(0, dismissDocument.RootElement.GetProperty("retrieve_code").GetInt32());
        }

        [TestMethod]
        public void RealControlPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(MeetingRealControlRequest), typeof(MeetingRealControlParticipant),
                typeof(MeetingRealControlNicknameParticipant), typeof(SetMeetingRealControlRequest),
                typeof(SetMeetingRealControlResult), typeof(SetMeetingCoHostRequest),
                typeof(SetMeetingCoHostResult), typeof(MuteMeetingUserRequest),
                typeof(MuteMeetingUserResult), typeof(SwitchMeetingUserVideoRequest),
                typeof(SwitchMeetingUserVideoResult), typeof(CloseMeetingScreenShareRequest),
                typeof(CloseMeetingScreenShareResult), typeof(SetMeetingNicknamesRequest),
                typeof(SetMeetingNicknamesResult), typeof(ManageMeetingWaitingRoomUsersRequest),
                typeof(ManageMeetingWaitingRoomUsersResult), typeof(KickoutMeetingUsersRequest),
                typeof(KickoutMeetingUsersResult), typeof(DismissMeetingRequest),
                typeof(DismissMeetingResult)
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

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingRealControlJson.cs"));
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
