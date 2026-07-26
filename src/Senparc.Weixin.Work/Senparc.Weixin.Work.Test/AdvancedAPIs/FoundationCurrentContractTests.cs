using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.IdConvert;
using Senparc.Weixin.Work.AdvancedAPIs.OAuth2;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
{
    [TestClass]
    public class FoundationCurrentContractTests
    {
        [TestMethod]
        public void OAuth2ApiUsesCurrentAuthPathsAndProvidesTfaEntries()
        {
            var oauthSource = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "OAuth2", "OAuth2Api.cs");
            var tfaSource = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "OAuth2",
                "OAuth2Api.Tfa.cs");

            StringAssert.Contains(oauthSource, "/cgi-bin/auth/getuserinfo");
            StringAssert.Contains(oauthSource, "/cgi-bin/auth/getuserdetail");
            Assert.IsFalse(oauthSource.Contains("/cgi-bin/user/getuserinfo"));
            Assert.IsFalse(oauthSource.Contains("/cgi-bin/user/getuserdetail"));
            StringAssert.Contains(oauthSource, "/document/path/91023");
            StringAssert.Contains(oauthSource, "/document/path/95833");
            StringAssert.Contains(tfaSource, "/cgi-bin/auth/get_tfa_info");
            StringAssert.Contains(tfaSource, "/document/path/99499");
            Assert.AreEqual(3, CountOccurrences(tfaSource, "/// <summary>"));

            var methodNames = typeof(OAuth2Api)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            CollectionAssert.Contains(methodNames, nameof(OAuth2Api.GetUserId));
            CollectionAssert.Contains(methodNames, nameof(OAuth2Api.GetUserIdAsync));
            CollectionAssert.Contains(methodNames, nameof(OAuth2Api.GetUserDetail));
            CollectionAssert.Contains(methodNames, nameof(OAuth2Api.GetUserDetailAsync));
            CollectionAssert.Contains(methodNames, nameof(OAuth2Api.GetTfaInfo));
            CollectionAssert.Contains(methodNames, nameof(OAuth2Api.GetTfaInfoAsync));
        }

        [TestMethod]
        public void CommonAndIdConvertApisProvideCurrentFoundationEntries()
        {
            var commonMethods = typeof(CommonApi)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            var idConvertMethods = typeof(IdConvertApi)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            CollectionAssert.Contains(commonMethods, nameof(CommonApi.GetApiDomainIp));
            CollectionAssert.Contains(commonMethods, nameof(CommonApi.GetApiDomainIpAsync));
            CollectionAssert.Contains(idConvertMethods, nameof(IdConvertApi.ApplyMassCallTicket));
            CollectionAssert.Contains(idConvertMethods,
                nameof(IdConvertApi.ApplyMassCallTicketAsync));

            var domainSource = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "CommonAPIs", "CommonApi.DomainIp.cs");
            var massCallSource = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "IdConvert",
                "IdConvertApi.MassCallTicket.cs");
            StringAssert.Contains(domainSource, "/cgi-bin/get_api_domain_ip");
            StringAssert.Contains(domainSource, "/document/path/92520");
            StringAssert.Contains(domainSource, "CommonJsonSendType.GET");
            StringAssert.Contains(massCallSource,
                "/cgi-bin/corp/apply_mass_call_ticket");
            StringAssert.Contains(massCallSource, "/document/path/96168");
            StringAssert.Contains(massCallSource, "CommonJsonSendType.GET");
            Assert.AreEqual(3, CountOccurrences(domainSource, "/// <summary>"));
            Assert.AreEqual(3, CountOccurrences(massCallSource, "/// <summary>"));
        }

        [TestMethod]
        public void FoundationModelsPreserveCurrentFieldsAndDocumentation()
        {
            var tfaJson = JsonSerializer.Serialize(new GetTfaInfoRequest
            {
                code = "TFA-CODE"
            });
            StringAssert.Contains(tfaJson, "\"code\":\"TFA-CODE\"");

            var userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<GetUserInfoResult>(
                "{\"errcode\":0,\"userid\":\"zhangsan\"," +
                "\"user_ticket\":\"USER-TICKET\"," +
                "\"user_doc_ticket\":\"DOC-TICKET\"}");
            var userDetail = Newtonsoft.Json.JsonConvert
                .DeserializeObject<GetUserDetailResult>(
                    "{\"errcode\":0,\"userid\":\"zhangsan\"," +
                    "\"qr_code\":\"https://example/qr\"," +
                    "\"biz_mail\":\"user@example.com\",\"address\":\"address\"}");
            var tfa = Newtonsoft.Json.JsonConvert.DeserializeObject<GetTfaInfoResult>(
                "{\"errcode\":0,\"userid\":\"zhangsan\"," +
                "\"tfa_code\":\"TFA-AUTH\"}");
            var domain = Newtonsoft.Json.JsonConvert
                .DeserializeObject<GetApiDomainIpResult>(
                    "{\"errcode\":0,\"ip_list\":[\"1.2.3.0/24\"]}");
            var ticket = Newtonsoft.Json.JsonConvert
                .DeserializeObject<ApplyMassCallTicketResult>(
                    "{\"errcode\":0,\"mass_call_ticket\":\"MASS-TICKET\"}");

            Assert.AreEqual("DOC-TICKET", userInfo.user_doc_ticket);
            Assert.AreEqual("https://example/qr", userDetail.qr_code);
            Assert.AreEqual("user@example.com", userDetail.biz_mail);
            Assert.AreEqual("address", userDetail.address);
            Assert.AreEqual("TFA-AUTH", tfa.tfa_code);
            Assert.AreEqual("1.2.3.0/24", domain.ip_list[0]);
            Assert.AreEqual("MASS-TICKET", ticket.mass_call_ticket);

            var resultSource = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "OAuth2", "OAuth2Result.cs");
            var domainResultSource = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "Entities", "JsonResult",
                "GetApiDomainIpResult.cs");
            var ticketResultSource = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "IdConvert",
                "MassCallTicketJson.cs");
            StringAssert.Contains(resultSource,
                "/// <summary>获取用户二次验证信息请求。</summary>");
            StringAssert.Contains(resultSource,
                "/// <summary>获取用户二次验证信息结果。</summary>");
            Assert.AreEqual(2,
                CountOccurrences(domainResultSource, "/// <summary>"));
            Assert.AreEqual(2,
                CountOccurrences(ticketResultSource, "/// <summary>"));
        }

        private static string ReadRepositoryFile(params string[] pathParts)
            => File.ReadAllText(Path.Combine(
                new[] { FindRepositoryRoot() }.Concat(pathParts).ToArray()));

        private static int CountOccurrences(string value, string search)
            => value.Split(new[] { search }, StringSplitOptions.None).Length - 1;

        private static string FindRepositoryRoot(
            [CallerFilePath] string sourceFilePath = null)
        {
            foreach (var startPath in new[]
            {
                Directory.GetCurrentDirectory(),
                AppContext.BaseDirectory,
                Path.GetDirectoryName(sourceFilePath)
            })
            {
                var directory = string.IsNullOrEmpty(startPath)
                    ? null
                    : new DirectoryInfo(startPath);
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
