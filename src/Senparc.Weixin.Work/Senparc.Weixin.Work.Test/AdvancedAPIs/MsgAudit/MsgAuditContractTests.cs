using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.MsgAudit;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.MsgAudit
{
    [TestClass]
    public class MsgAuditContractTests
    {
        [TestMethod]
        public void MsgAuditApiContainsFiveSyncAndAsyncEntries()
        {
            var methodNames = typeof(MsgAuditApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            CollectionAssert.Contains(methodNames, nameof(MsgAuditApi.GetRobotInfo));
            CollectionAssert.Contains(methodNames, nameof(MsgAuditApi.GetRobotInfoAsync));
            CollectionAssert.Contains(methodNames, nameof(MsgAuditApi.GetPermitUserList));
            CollectionAssert.Contains(methodNames, nameof(MsgAuditApi.GetPermitUserListAsync));
            CollectionAssert.Contains(methodNames, nameof(MsgAuditApi.GetGroupChat));
            CollectionAssert.Contains(methodNames, nameof(MsgAuditApi.GetGroupChatAsync));
            CollectionAssert.Contains(methodNames, nameof(MsgAuditApi.CheckSingleAgree));
            CollectionAssert.Contains(methodNames, nameof(MsgAuditApi.CheckSingleAgreeAsync));
            CollectionAssert.Contains(methodNames, nameof(MsgAuditApi.CheckRoomAgree));
            CollectionAssert.Contains(methodNames, nameof(MsgAuditApi.CheckRoomAgreeAsync));
        }

        [TestMethod]
        public void MsgAuditApiUsesOfficialPaths()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "MsgAudit",
                "MsgAuditApi.cs"));

            StringAssert.Contains(source, "/cgi-bin/msgaudit/get_robot_info");
            StringAssert.Contains(source, "/cgi-bin/msgaudit/get_permit_user_list");
            StringAssert.Contains(source, "/cgi-bin/msgaudit/groupchat/get");
            StringAssert.Contains(source, "/cgi-bin/msgaudit/check_single_agree");
            StringAssert.Contains(source, "/cgi-bin/msgaudit/check_room_agree");
            StringAssert.Contains(source, "CommonJsonSendType.POST");
            StringAssert.Contains(source, "CommonJsonSendType.GET");
            Assert.AreEqual(2, source.Split(new[] { "/document/path/91774" },
                StringSplitOptions.None).Length - 1);
            Assert.AreEqual(2, source.Split(new[] { "/// <param name=\"robotId\">" },
                StringSplitOptions.None).Length - 1);
        }

        [TestMethod]
        public void SingleAgreeRequestPreservesOfficialFieldSpelling()
        {
            var request = new CheckSingleAgreeRequest
            {
                info = new List<MsgAuditConversationInfo>
                {
                    new MsgAuditConversationInfo
                    {
                        userid = "zhangsan",
                        exteranalopenid = "wm-open-id"
                    }
                }
            };

            var json = JsonSerializer.Serialize(request);

            StringAssert.Contains(json, "\"userid\":\"zhangsan\"");
            StringAssert.Contains(json, "\"exteranalopenid\":\"wm-open-id\"");
            Assert.IsFalse(json.Contains("externalopenid"));
        }

        [TestMethod]
        public void GroupChatResultPreservesLargeTimestamps()
        {
            var result = JsonSerializer.Deserialize<GetGroupChatResult>(
                "{\"errcode\":0,\"roomname\":\"研发群\",\"creator\":\"zhangsan\"," +
                "\"room_create_time\":5178368698,\"notice\":\"发布提醒\"," +
                "\"members\":[{\"memberid\":\"lisi\",\"jointime\":5178368799}]}" );

            Assert.IsNotNull(result);
            Assert.AreEqual("研发群", result.roomname);
            Assert.AreEqual(5178368698L, result.room_create_time);
            Assert.AreEqual("lisi", result.members[0].memberid);
            Assert.AreEqual(5178368799L, result.members[0].jointime);
        }

        [TestMethod]
        public void PermitUsersAndAgreeStatusUseStrongTypes()
        {
            var permitUsers = JsonSerializer.Deserialize<GetPermitUserListResult>(
                "{\"errcode\":0,\"ids\":[\"zhangsan\",\"lisi\"]}");
            var agree = JsonSerializer.Deserialize<CheckAgreeResult>(
                "{\"errcode\":0,\"agreeinfo\":[{\"userid\":\"zhangsan\"," +
                "\"exteranalopenid\":\"wm-open-id\",\"agree_status\":\"Agree\"," +
                "\"status_change_time\":5178368899}]}" );

            Assert.IsNotNull(permitUsers);
            CollectionAssert.AreEqual(new[] { "zhangsan", "lisi" }, permitUsers.ids.ToArray());
            Assert.IsNotNull(agree);
            Assert.AreEqual("Agree", agree.agreeinfo[0].agree_status);
            Assert.AreEqual(5178368899L, agree.agreeinfo[0].status_change_time);
        }

        [TestMethod]
        public void RobotInfoUsesStrongTypesAndCompleteXmlComments()
        {
            var result = JsonSerializer.Deserialize<GetMsgAuditRobotInfoResult>(
                "{\"errcode\":0,\"data\":{\"robot_id\":\"robot-1\"," +
                "\"name\":\"服务机器人\",\"creator_userid\":\"zhangsan\"}}");

            Assert.IsNotNull(result);
            Assert.AreEqual("robot-1", result.data.robot_id);
            Assert.AreEqual("服务机器人", result.data.name);
            Assert.AreEqual("zhangsan", result.data.creator_userid);

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "MsgAudit",
                "MsgAuditJson.cs"));
            StringAssert.Contains(source, "/// <summary>获取或设置机器人 ID。</summary>");
            StringAssert.Contains(source, "/// <summary>获取或设置机器人名称。</summary>");
            StringAssert.Contains(source,
                "/// <summary>获取或设置创建机器人的成员 UserId。</summary>");
            StringAssert.Contains(source, "/// <summary>获取或设置机器人信息。</summary>");
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
