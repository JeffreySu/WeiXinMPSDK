using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.MailList;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.MailList
{
    [TestClass]
    public class MemberAuthorizationContractTests
    {
        [TestMethod]
        public void MemberAuthorizationApiExposesFourSyncAndAsyncOperations()
        {
            var methods = typeof(MailListApi).GetMethods(BindingFlags.Public | BindingFlags.Static);

            Assert.AreEqual(typeof(GetMemberAuthListResult),
                methods.Single(method => method.Name == nameof(MailListApi.GetMemberAuthList)).ReturnType);
            Assert.AreEqual(typeof(Task<GetMemberAuthListResult>),
                methods.Single(method => method.Name == nameof(MailListApi.GetMemberAuthListAsync)).ReturnType);
            Assert.AreEqual(typeof(CheckMemberAuthResult),
                methods.Single(method => method.Name == nameof(MailListApi.CheckMemberAuth)).ReturnType);
            Assert.AreEqual(typeof(Task<CheckMemberAuthResult>),
                methods.Single(method => method.Name == nameof(MailListApi.CheckMemberAuthAsync)).ReturnType);
            Assert.AreEqual(typeof(GetSelectedTicketUsersResult),
                methods.Single(method => method.Name == nameof(MailListApi.GetSelectedTicketUsers)).ReturnType);
            Assert.AreEqual(typeof(Task<GetSelectedTicketUsersResult>),
                methods.Single(method => method.Name == nameof(MailListApi.GetSelectedTicketUsersAsync)).ReturnType);
            Assert.AreEqual(typeof(WorkJsonResult),
                methods.Single(method => method.Name == nameof(MailListApi.SetTfaSuccess)).ReturnType);
            Assert.AreEqual(typeof(Task<WorkJsonResult>),
                methods.Single(method => method.Name == nameof(MailListApi.SetTfaSuccessAsync)).ReturnType);
        }

        [TestMethod]
        public void MemberAuthorizationApiUsesFourOfficialPostPaths()
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Static;
            var sourcePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(SourceFilePath()), "..", "..",
                "..", "Senparc.Weixin.Work", "AdvancedAPIs", "MailList", "MailListMemberAuthorizationApi.cs"));
            var source = File.ReadAllText(sourcePath);

            Assert.AreEqual("/cgi-bin/user/list_member_auth",
                typeof(MailListApi).GetField("GetMemberAuthListPath", flags)?.GetRawConstantValue());
            Assert.AreEqual("/cgi-bin/user/check_member_auth",
                typeof(MailListApi).GetField("CheckMemberAuthPath", flags)?.GetRawConstantValue());
            Assert.AreEqual("/cgi-bin/user/list_selected_ticket_user",
                typeof(MailListApi).GetField("GetSelectedTicketUsersPath", flags)?.GetRawConstantValue());
            Assert.AreEqual("/cgi-bin/user/tfa_succ",
                typeof(MailListApi).GetField("SetTfaSuccessPath", flags)?.GetRawConstantValue());
            Assert.AreEqual(8, CountOccurrences(source, "CommonJsonSendType.POST"));
            Assert.AreEqual(0, CountOccurrences(source, "CommonJsonSendType.GET"));
        }

        [TestMethod]
        public void MemberAuthorizationRequestsPreserveOfficialFields()
        {
            var listJson = JsonSerializer.Serialize(new GetMemberAuthListRequest
            {
                cursor = "NEXT_KEY",
                limit = 20
            });
            var checkJson = JsonSerializer.Serialize(new CheckMemberAuthRequest
            {
                open_userid = "wo-open-user"
            });
            var selectedJson = JsonSerializer.Serialize(new GetSelectedTicketUsersRequest
            {
                selected_ticket = "SELECTED_TICKET"
            });
            var tfaJson = JsonSerializer.Serialize(new TfaSuccessRequest
            {
                userid = "zhangsan",
                tfa_code = "TFA_CODE"
            });

            StringAssert.Contains(listJson, "\"cursor\":\"NEXT_KEY\"");
            StringAssert.Contains(listJson, "\"limit\":20");
            StringAssert.Contains(checkJson, "\"open_userid\":\"wo-open-user\"");
            StringAssert.Contains(selectedJson, "\"selected_ticket\":\"SELECTED_TICKET\"");
            StringAssert.Contains(tfaJson, "\"userid\":\"zhangsan\"");
            StringAssert.Contains(tfaJson, "\"tfa_code\":\"TFA_CODE\"");
        }

        [TestMethod]
        public void MemberAuthorizationResultsPreserveOfficialFields()
        {
            var list = JsonSerializer.Deserialize<GetMemberAuthListResult>(
                "{\"errcode\":0,\"next_cursor\":\"next\",\"member_auth_list\":[{\"open_userid\":\"wo-one\"}]}");
            var check = JsonSerializer.Deserialize<CheckMemberAuthResult>(
                "{\"errcode\":0,\"is_member_auth\":true}");
            var selected = JsonSerializer.Deserialize<GetSelectedTicketUsersResult>(
                "{\"errcode\":0,\"operator_open_userid\":\"wo-operator\",\"total\":3," +
                "\"open_userid_list\":[\"wo-one\",\"wo-two\"]," +
                "\"unauth_open_userid_list\":[\"wo-three\"]}");

            Assert.IsNotNull(list);
            Assert.AreEqual("next", list.next_cursor);
            Assert.AreEqual("wo-one", list.member_auth_list[0].open_userid);
            Assert.IsNotNull(check);
            Assert.IsTrue(check.is_member_auth);
            Assert.IsNotNull(selected);
            Assert.AreEqual("wo-operator", selected.operator_open_userid);
            Assert.AreEqual(3, selected.total);
            Assert.AreEqual("wo-two", selected.open_userid_list[1]);
            Assert.AreEqual("wo-three", selected.unauth_open_userid_list[0]);
        }

        private static int CountOccurrences(string source, string value)
        {
            var count = 0;
            var index = 0;
            while ((index = source.IndexOf(value, index, System.StringComparison.Ordinal)) >= 0)
            {
                count++;
                index += value.Length;
            }

            return count;
        }

        private static string SourceFilePath([CallerFilePath] string sourceFilePath = null)
            => sourceFilePath;
    }
}
