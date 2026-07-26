using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.License;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.License
{
    [TestClass]
    public class LicenseAccountContractTests
    {
        [TestMethod]
        public void LicenseApiProvidesFourteenCurrentAccountAndSettingEntries()
        {
            var methodNames = typeof(LicenseApi)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            var syncMethods = new[]
            {
                nameof(LicenseApi.ActivateAccount),
                nameof(LicenseApi.BatchActivateAccount),
                nameof(LicenseApi.ActivateAccountByType),
                nameof(LicenseApi.GetActiveInfoByCode),
                nameof(LicenseApi.BatchGetActiveInfoByCode),
                nameof(LicenseApi.ListActivatedAccount),
                nameof(LicenseApi.GetActiveInfoByUser),
                nameof(LicenseApi.TransferAccount),
                nameof(LicenseApi.ShareActiveCode),
                nameof(LicenseApi.GetAppLicenseInfo),
                nameof(LicenseApi.SetAutoActiveStatus),
                nameof(LicenseApi.GetAutoActiveStatus),
                nameof(LicenseApi.QuerySupportPolicy),
                nameof(LicenseApi.GetAccountBalance)
            };
            foreach (var methodName in syncMethods)
            {
                CollectionAssert.Contains(methodNames, methodName, methodName);
                CollectionAssert.Contains(methodNames, methodName + "Async",
                    methodName + "Async");
            }

            var accountSource = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "License",
                "LicenseApi.Account.cs");
            var settingsSource = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "License",
                "LicenseApi.Settings.cs");
            var source = accountSource + settingsSource;
            var paths = new[]
            {
                "/cgi-bin/license/active_account",
                "/cgi-bin/license/batch_active_account",
                "/cgi-bin/license/active_account_by_type",
                "/cgi-bin/license/get_active_info_by_code",
                "/cgi-bin/license/batch_get_active_info_by_code",
                "/cgi-bin/license/list_actived_account",
                "/cgi-bin/license/get_active_info_by_user",
                "/cgi-bin/license/batch_transfer_license",
                "/cgi-bin/license/batch_share_active_code",
                "/cgi-bin/license/get_app_license_info",
                "/cgi-bin/license/set_auto_active_status",
                "/cgi-bin/license/get_auto_active_status",
                "/cgi-bin/license/support_policy_query",
                "/cgi-bin/service/get_account_balance"
            };
            foreach (var path in paths)
            {
                StringAssert.Contains(source, path);
            }

            foreach (var documentId in new[]
            {
                "97188", "97189", "97190", "97191", "97192", "97193",
                "97194", "97199", "97200", "97208", "100138"
            })
            {
                StringAssert.Contains(source, "/document/path/" + documentId);
            }

            var coreSource = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "License", "LicenseApi.cs");
            StringAssert.Contains(coreSource, "?provider_access_token={0}");
            StringAssert.Contains(coreSource, "CommonJsonSendType.GET");
            Assert.IsFalse(coreSource.Contains("?access_token={0}"));
            Assert.IsFalse(source.Contains("?access_token={0}"));
        }

        [TestMethod]
        public void LicenseRequestsPreserveOfficialCollectionsAndCurrentTypeActivation()
        {
            var batch = new LicenseBatchActivateAccountRequest
            {
                corpid = "ww-corp",
                active_list = new List<LicenseActivationItem>
                {
                    new LicenseActivationItem
                    {
                        active_code = "ACTIVE-CODE",
                        userid = "zhangsan"
                    }
                }
            };
            using (var json = JsonDocument.Parse(JsonSerializer.Serialize(batch)))
            {
                Assert.AreEqual("ww-corp",
                    json.RootElement.GetProperty("corpid").GetString());
                Assert.AreEqual("ACTIVE-CODE", json.RootElement
                    .GetProperty("active_list")[0]
                    .GetProperty("active_code").GetString());
            }

            var byType = new LicenseActivateAccountByTypeRequest
            {
                type = 2,
                corpid = "ww-corp",
                userid = "zhangsan"
            };
            using (var json = JsonDocument.Parse(JsonSerializer.Serialize(byType)))
            {
                Assert.AreEqual(2, json.RootElement.GetProperty("type").GetInt32());
            }

            var app = new LicenseGetAppInfoRequest
            {
                corpid = "ww-corp",
                suite_id = "suite-id",
                appid = 1000002
            };
            using (var json = JsonDocument.Parse(JsonSerializer.Serialize(app)))
            {
                Assert.AreEqual(1000002,
                    json.RootElement.GetProperty("appid").GetInt32());
            }
        }

        [TestMethod]
        public void LicenseResultsPreserveFlowsLargeTimesAndLargeBalance()
        {
            var detail = Newtonsoft.Json.JsonConvert
                .DeserializeObject<LicenseGetActiveInfoResult>(
                    "{\"errcode\":0,\"active_info\":{" +
                    "\"active_code\":\"CODE\",\"type\":2,\"status\":1," +
                    "\"userid\":\"zhangsan\",\"create_time\":4178368698," +
                    "\"active_time\":5178368698,\"expire_time\":6178368698," +
                    "\"merge_info\":{\"to_active_code\":\"NEW\"," +
                    "\"from_active_code\":\"OLD\"},\"share_info\":{" +
                    "\"to_corpid\":\"ww-to\",\"from_corpid\":\"ww-from\"}}}");
            Assert.AreEqual(6178368698L, detail.active_info.expire_time);
            Assert.AreEqual("OLD", detail.active_info.merge_info.from_active_code);
            Assert.AreEqual("ww-to", detail.active_info.share_info.to_corpid);

            var list = Newtonsoft.Json.JsonConvert
                .DeserializeObject<LicenseActivatedAccountListResult>(
                    "{\"errcode\":0,\"has_more\":1,\"next_cursor\":\"NEXT\"," +
                    "\"account_list\":[{\"userid\":\"zhangsan\",\"type\":1," +
                    "\"active_time\":5178368698,\"expire_time\":6178368698}]}");
            Assert.AreEqual(1, list.has_more);
            Assert.AreEqual(6178368698L, list.account_list[0].expire_time);

            var app = Newtonsoft.Json.JsonConvert
                .DeserializeObject<LicenseAppInfoResult>(
                    "{\"errcode\":0,\"license_status\":1,\"trail_info\":{" +
                    "\"start_time\":4178368698,\"end_time\":5178368698}," +
                    "\"license_check_time\":6178368698}");
            Assert.AreEqual(5178368698L, app.trail_info.end_time);
            Assert.AreEqual(6178368698L, app.license_check_time);

            var balance = Newtonsoft.Json.JsonConvert
                .DeserializeObject<LicenseAccountBalanceResult>(
                    "{\"errcode\":0,\"balance\":5000000000}");
            Assert.AreEqual(5000000000L, balance.balance);
        }

        [TestMethod]
        public void LicenseAccountModelsAreStronglyTypedAndFullyDocumented()
        {
            foreach (var fileName in new[]
            {
                "LicenseAccountJson.cs", "LicenseSettingsJson.cs"
            })
            {
                var source = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                    "Senparc.Weixin.Work", "AdvancedAPIs", "License", fileName);
                var declarations = Regex.Matches(source,
                        @"^\s*public class ", RegexOptions.Multiline).Count +
                    Regex.Matches(source,
                        @"^\s*public [^\r\n]+ \{ get; set; \}$",
                        RegexOptions.Multiline).Count;
                Assert.AreEqual(declarations,
                    CountOccurrences(source, "/// <summary>"), fileName);
                Assert.IsFalse(source.Contains("object "), fileName);
                Assert.IsFalse(source.Contains("dynamic "), fileName);
            }

            var modelTypes = typeof(LicenseActivateAccountRequest).Assembly.GetTypes()
                .Where(type => type.IsPublic && type.IsClass &&
                    type.Namespace == typeof(LicenseActivateAccountRequest).Namespace &&
                    type.Name.StartsWith("License", StringComparison.Ordinal))
                .ToArray();
            foreach (var modelType in modelTypes)
            {
                var untyped = modelType.GetProperties(BindingFlags.Public |
                        BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .FirstOrDefault(property => property.PropertyType == typeof(object));
                Assert.IsNull(untyped, $"{modelType.FullName}.{untyped?.Name}");
            }
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
