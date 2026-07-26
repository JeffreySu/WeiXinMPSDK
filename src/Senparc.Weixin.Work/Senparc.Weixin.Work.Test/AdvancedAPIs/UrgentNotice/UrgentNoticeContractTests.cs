using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.UrgentNotice;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.UrgentNotice
{
    [TestClass]
    public class UrgentNoticeContractTests
    {
        [TestMethod]
        public void UrgentNoticeApiContainsTwoSyncAndAsyncEntries()
        {
            var methodNames = typeof(UrgentNoticeApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            CollectionAssert.Contains(methodNames, nameof(UrgentNoticeApi.StartCall));
            CollectionAssert.Contains(methodNames, nameof(UrgentNoticeApi.StartCallAsync));
            CollectionAssert.Contains(methodNames, nameof(UrgentNoticeApi.GetCallState));
            CollectionAssert.Contains(methodNames, nameof(UrgentNoticeApi.GetCallStateAsync));
        }

        [TestMethod]
        public void UrgentNoticeApiUsesOfficialPostPaths()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "UrgentNotice", "UrgentNoticeApi.cs"));

            StringAssert.Contains(source, "/cgi-bin/pstncc/call");
            StringAssert.Contains(source, "/cgi-bin/pstncc/getstates");
            StringAssert.Contains(source, "CommonJsonSendType.POST");
        }

        [TestMethod]
        public void UrgentNoticeRequestsUseOfficialJsonFields()
        {
            var startJson = JsonSerializer.Serialize(new StartUrgentCallRequest
            {
                callee_userid = new List<string> { "james", "paul" }
            });
            var stateJson = JsonSerializer.Serialize(new GetUrgentCallStateRequest
            {
                callee_userid = "james",
                callid = "call-1"
            });

            StringAssert.Contains(startJson, "\"callee_userid\":[\"james\",\"paul\"]");
            StringAssert.Contains(stateJson, "\"callee_userid\":\"james\"");
            StringAssert.Contains(stateJson, "\"callid\":\"call-1\"");
        }

        [TestMethod]
        public void UrgentNoticeResultsPreserveOfficialFieldsAndLargeTimestamp()
        {
            var start = JsonSerializer.Deserialize<StartUrgentCallResult>(
                "{\"errcode\":0,\"states\":[{\"code\":0,\"callid\":\"call-1\",\"userid\":\"james\"}]}" );
            var state = JsonSerializer.Deserialize<GetUrgentCallStateResult>(
                "{\"errcode\":0,\"istalked\":1,\"calltime\":5178368698,\"talktime\":2,\"reason\":0}" );

            Assert.IsNotNull(start);
            Assert.AreEqual("call-1", start.states[0].callid);
            Assert.AreEqual("james", start.states[0].userid);
            Assert.IsNotNull(state);
            Assert.AreEqual(1, state.istalked);
            Assert.AreEqual(5178368698L, state.calltime);
            Assert.AreEqual(2, state.talktime);
            Assert.AreEqual(0, state.reason);
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
