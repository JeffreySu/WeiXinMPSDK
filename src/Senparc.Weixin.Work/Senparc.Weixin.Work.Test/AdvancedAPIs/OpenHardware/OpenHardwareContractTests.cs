using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.OpenHardware;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.OpenHardware
{
    [TestClass]
    public class OpenHardwareContractTests
    {
        [TestMethod]
        public void ApiProvidesTwentyTwoVerifiedAndTwoUnavailableVisitorSyncAndAsyncEntries()
        {
            var methodNames = typeof(OpenHardwareApi)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            var syncMethods = new[]
            {
                nameof(OpenHardwareApi.GetModelToken),
                nameof(OpenHardwareApi.GetDeviceSecret),
                nameof(OpenHardwareApi.GetDeviceToken),
                nameof(OpenHardwareApi.RegisterDevice),
                nameof(OpenHardwareApi.UnregisterDevice),
                nameof(OpenHardwareApi.GetDeviceDetail),
                nameof(OpenHardwareApi.ReportDeviceStatus),
                nameof(OpenHardwareApi.GetUserInfoByPage),
                nameof(OpenHardwareApi.GetUserInfoByIds),
                nameof(OpenHardwareApi.ReportFirmwareUpgradeResult),
                nameof(OpenHardwareApi.GenerateLoginQrCode),
                nameof(OpenHardwareApi.GenerateIdDynamicQrCode),
                nameof(OpenHardwareApi.ReportCheckinData),
                nameof(OpenHardwareApi.ReportTemperatureData),
                nameof(OpenHardwareApi.ReportAccessData),
                nameof(OpenHardwareApi.ReportBiometricInfoResult),
                nameof(OpenHardwareApi.ReportRemoteOpenResult),
                nameof(OpenHardwareApi.GetPrinterJobList),
                nameof(OpenHardwareApi.GetPrinterJobDownloadUrl),
                nameof(OpenHardwareApi.ReportPrinterJobStatus),
                nameof(OpenHardwareApi.PushScanFile),
                nameof(OpenHardwareApi.SetPrinterJobTransResult)
            };

            foreach (var methodName in syncMethods)
            {
                CollectionAssert.Contains(methodNames, methodName, methodName);
                CollectionAssert.Contains(methodNames, methodName + "Async",
                    methodName + "Async");
            }

            foreach (var methodName in new[]
            {
                nameof(OpenHardwareApi.GetVisitorByPage),
                nameof(OpenHardwareApi.GetVisitorByIds)
            })
            {
                CollectionAssert.Contains(methodNames, methodName, methodName);
                CollectionAssert.Contains(methodNames, methodName + "Async",
                    methodName + "Async");
            }
        }

        [TestMethod]
        public void ApiUsesCurrentOfficialPathsTokensAndDocumentReferences()
        {
            var source = string.Join("\n", new[]
            {
                "OpenHardwareApi.cs", "OpenHardwareApi.Device.cs",
                "OpenHardwareApi.Attendance.cs", "OpenHardwareApi.Printer.cs",
                "OpenHardwareApi.Visitor.cs"
            }.Select(fileName => ReadRepositoryFile("src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "OpenHardware", fileName)));
            var paths = new[]
            {
                "/cgi-bin/openhw/get_model_token",
                "/cgi-bin/openhw/get_device_secret",
                "/cgi-bin/openhw/get_device_token",
                "/cgi-bin/openhw/model/register_sn",
                "/cgi-bin/openhw/model/unregister_sn",
                "/cgi-bin/openhw/device/get_device_detail",
                "/cgi-bin/openhw/device/report_device_status",
                "/cgi-bin/openhw/device/get_userinfo_by_page",
                "/cgi-bin/openhw/device/get_userinfo_by_ids",
                "/cgi-bin/openhw/device/report_firmware_upgrade_result",
                "/cgi-bin/openhw/device/gen_login_qrcode",
                "/cgi-bin/openhw/device/gen_id_dynamic_qrcode",
                "/cgi-bin/openhw/device/report_checkin_data",
                "/cgi-bin/openhw/device/report_temperature_data",
                "/cgi-bin/openhw/device/report_access_data",
                "/cgi-bin/openhw/device/report_bio_info_result",
                "/cgi-bin/openhw/device/report_remote_open_result",
                "/cgi-bin/openhw/device/get_printer_job_list",
                "/cgi-bin/openhw/device/get_printer_job_download_url",
                "/cgi-bin/openhw/device/report_printer_job_status",
                "/cgi-bin/openhw/device/push_scan_file",
                "/cgi-bin/openhw/device/set_printer_job_trans_result"
            };
            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""),
                    path);
            }

            foreach (var documentId in new[]
            {
                "95992", "95993", "96022", "95980", "95981", "95982",
                "95983", "95984", "96037", "95999", "98023", "98024",
                "95985", "95986", "95997", "96000", "96048", "96407",
                "96408", "96409", "96410", "96412", "96060", "96061"
            })
            {
                StringAssert.Contains(source, "/document/path/" + documentId);
            }

            StringAssert.Contains(source, "?model_access_token={0}");
            StringAssert.Contains(source, "?device_access_token={0}");
            Assert.IsFalse(source.Contains("?access_token={0}"));
            Assert.IsFalse(source.Contains("/cgi-bin/openhw/device/get_visitor"),
                "访客接口路径尚无可验证正文，不应猜测 URL。");
        }

        [TestMethod]
        public async Task UnavailableVisitorApisThrowWithOfficialDocumentContext()
        {
            var pageException = Assert.ThrowsException<NotSupportedException>(() =>
                OpenHardwareApi.GetVisitorByPage("DEVICE-TOKEN",
                    new OpenHardwareGetVisitorByPageRequest()));
            StringAssert.Contains(pageException.Message, "全量获取访客数据");
            StringAssert.Contains(pageException.Message, "96060");
            StringAssert.Contains(pageException.Message, "暂无权限查看");

            var idsException = Assert.ThrowsException<NotSupportedException>(() =>
                OpenHardwareApi.GetVisitorByIds("DEVICE-TOKEN",
                    new OpenHardwareGetVisitorByIdsRequest()));
            StringAssert.Contains(idsException.Message, "获取指定访客数据");
            StringAssert.Contains(idsException.Message, "96061");

            var pageAsyncException = await Assert.ThrowsExceptionAsync<
                NotSupportedException>(() => OpenHardwareApi.GetVisitorByPageAsync(
                    "DEVICE-TOKEN", new OpenHardwareGetVisitorByPageRequest()));
            StringAssert.Contains(pageAsyncException.Message, "96060");

            var idsAsyncException = await Assert.ThrowsExceptionAsync<
                NotSupportedException>(() => OpenHardwareApi.GetVisitorByIdsAsync(
                    "DEVICE-TOKEN", new OpenHardwareGetVisitorByIdsRequest()));
            StringAssert.Contains(idsAsyncException.Message, "96061");
        }

        [TestMethod]
        public void ModelsPreserveCollectionsOptionalFieldsAndLargeValues()
        {
            var request = new OpenHardwareGetPrinterJobListRequest
            {
                open_userid = "OPEN-USER",
                status = 2,
                cursor = "NEXT",
                begin_time = 4178368698,
                end_time = 5178368698,
                jobid_list = new List<string> { "JOB-1", "JOB-2" }
            };
            using (var json = JsonDocument.Parse(JsonSerializer.Serialize(request)))
            {
                Assert.AreEqual("OPEN-USER",
                    json.RootElement.GetProperty("open_userid").GetString());
                Assert.AreEqual(5178368698L,
                    json.RootElement.GetProperty("end_time").GetInt64());
                Assert.AreEqual("JOB-2",
                    json.RootElement.GetProperty("jobid_list")[1].GetString());
            }

            var result = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OpenHardwareGetPrinterJobListResult>(
                    "{\"errcode\":0,\"next_cursor\":\"NEXT\"," +
                    "\"printer_job_list\":[{\"open_userid\":\"OPEN-USER\"," +
                    "\"createtime\":4178368698,\"submitted\":0," +
                    "\"state\":\"machine-02\",\"status\":2,\"errcode\":1," +
                    "\"errmsg\":\"缺纸\",\"doc_name\":\"a.pdf\"," +
                    "\"doc_size\":5000000000,\"jobid\":\"JOB-1\"," +
                    "\"setting_list\":[{\"key\":\"纸张大小\"," +
                    "\"value\":[\"A4\"]}]}]}");
            Assert.AreEqual(4178368698L,
                result.printer_job_list[0].createtime);
            Assert.AreEqual(5000000000L,
                result.printer_job_list[0].doc_size);
            Assert.AreEqual("A4",
                result.printer_job_list[0].setting_list[0].value[0]);

            var users = Newtonsoft.Json.JsonConvert
                .DeserializeObject<OpenHardwareGetUserInfoByPageResult>(
                    "{\"errcode\":0,\"perm_version\":7," +
                    "\"userinfo\":{\"useritems\":[{\"open_userid\":\"U1\"," +
                    "\"user_type\":1,\"user_name\":\"张三\",\"pass_rule\":{" +
                    "\"rule_list\":[{\"id\":5000000000,\"rule\":\"RULE\"," +
                    "\"effect_time\":5178368698}]}}]}}" );
            Assert.AreEqual(5000000000L,
                users.userinfo.useritems[0].pass_rule.rule_list[0].id);
            Assert.AreEqual(5178368698L,
                users.userinfo.useritems[0].pass_rule.rule_list[0].effect_time);
        }

        [TestMethod]
        public void PublicApiAndModelsAreStronglyTypedAndFullyDocumented()
        {
            foreach (var fileName in new[]
            {
                "OpenHardwareDeviceJson.cs", "OpenHardwareAttendanceJson.cs",
                "OpenHardwarePrinterJson.cs", "OpenHardwareVisitorJson.cs"
            })
            {
                var source = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                    "Senparc.Weixin.Work", "AdvancedAPIs", "OpenHardware", fileName);
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

            foreach (var fileName in new[]
            {
                "OpenHardwareApi.Device.cs", "OpenHardwareApi.Attendance.cs",
                "OpenHardwareApi.Printer.cs", "OpenHardwareApi.Visitor.cs"
            })
            {
                var source = ReadRepositoryFile("src", "Senparc.Weixin.Work",
                    "Senparc.Weixin.Work", "AdvancedAPIs", "OpenHardware", fileName);
                var declarations = Regex.Matches(source,
                    @"^\s*public static ", RegexOptions.Multiline).Count;
                Assert.AreEqual(declarations,
                    CountOccurrences(source, "/// <summary>"), fileName);
            }

            var modelTypes = typeof(OpenHardwareGetModelTokenRequest).Assembly
                .GetTypes().Where(type => type.IsPublic && type.IsClass &&
                    type.Namespace == typeof(OpenHardwareGetModelTokenRequest).Namespace &&
                    type.Name.StartsWith("OpenHardware", StringComparison.Ordinal));
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
