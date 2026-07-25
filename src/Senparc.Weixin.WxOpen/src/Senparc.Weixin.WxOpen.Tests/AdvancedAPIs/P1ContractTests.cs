using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.WxOpen.AdvancedAPIs;
using Senparc.Weixin.WxOpen.AdvancedAPIs.CustomService;
using Senparc.Weixin.WxOpen.AdvancedAPIs.DataCube;
using Senparc.Weixin.WxOpen.AdvancedAPIs.HardwareDevice;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Message;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Operation;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb;
using Senparc.Weixin.WxOpen.AdvancedAPIs.UrlScheme;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp;
using Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.Business.JsonResult;
using Senparc.Weixin.WxOpen.OpenAPIs;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs
{
    [TestClass]
    public class P1ContractTests
    {
        [TestMethod]
        public void P1ApiSurfaceContainsAll52SyncAndAsyncEntries()
        {
            AssertMethodPairs(typeof(OpenApi),
                nameof(OpenApi.ClearQuota), nameof(OpenApi.ClearQuotaByAppSecret),
                nameof(OpenApi.CallbackCheck), nameof(OpenApi.GetApiDomainIp));
            AssertMethodPairs(typeof(BusinessApi),
                nameof(BusinessApi.GetPluginOpenPid), nameof(BusinessApi.CheckEncryptedData),
                nameof(BusinessApi.GetUserEncryptKey), nameof(BusinessApi.ResetUserSessionKey));
            AssertMethodPairs(typeof(UrlSchemeApi), nameof(UrlSchemeApi.QueryScheme));
            AssertMethodPairs(typeof(UrlLinkApi), nameof(UrlLinkApi.Query));
            AssertMethodPairs(typeof(CustomerServiceBusinessApi),
                nameof(CustomerServiceBusinessApi.Register), nameof(CustomerServiceBusinessApi.Update),
                nameof(CustomerServiceBusinessApi.Get), nameof(CustomerServiceBusinessApi.List));
            AssertMethodPairs(typeof(KfWorkApi),
                nameof(KfWorkApi.GetBound), nameof(KfWorkApi.Bind), nameof(KfWorkApi.Unbind));
            AssertMethodPairs(typeof(UpdatableMessageApi),
                nameof(UpdatableMessageApi.CreateActivityId), nameof(UpdatableMessageApi.SetUpdatableMessage),
                nameof(UpdatableMessageApi.SetChatToolMessage));
            AssertMethodPairs(typeof(ServiceCardApi),
                nameof(ServiceCardApi.SetUserNotify), nameof(ServiceCardApi.SetUserNotifyExt),
                nameof(ServiceCardApi.GetUserNotify));
            AssertMethodPairs(typeof(DataCubeApi), nameof(DataCubeApi.GetPerformanceData));
            AssertMethodPairs(typeof(HardwareDeviceApi),
                nameof(HardwareDeviceApi.SendDeviceMessage), nameof(HardwareDeviceApi.GetSnTicket),
                nameof(HardwareDeviceApi.CreateIotGroup), nameof(HardwareDeviceApi.GetIotGroupInfo),
                nameof(HardwareDeviceApi.AddIotGroupDevices), nameof(HardwareDeviceApi.RemoveIotGroupDevices),
                nameof(HardwareDeviceApi.GetLicensePackageList), nameof(HardwareDeviceApi.ActivateLicenseDevices),
                nameof(HardwareDeviceApi.GetLicenseDeviceInfo));
            AssertMethodPairs(typeof(OperationApi),
                nameof(OperationApi.GetDomainInfo), nameof(OperationApi.GetPerformance),
                nameof(OperationApi.GetSceneList), nameof(OperationApi.GetVersionList),
                nameof(OperationApi.RealTimeLogSearch), nameof(OperationApi.GetFeedback),
                nameof(OperationApi.GetFeedbackMedia), nameof(OperationApi.GetJsErrDetail),
                nameof(OperationApi.GetJsErrList));
            AssertMethodPairs(typeof(TcbIncrementApi),
                nameof(TcbIncrementApi.AddDelayedFunctionTask), nameof(TcbIncrementApi.SendSmsV2),
                nameof(TcbIncrementApi.SendSms), nameof(TcbIncrementApi.CreateSendSmsTask),
                nameof(TcbIncrementApi.Report), nameof(TcbIncrementApi.DescribeSmsRecords),
                nameof(TcbIncrementApi.DescribeExtensionUploadInfo), nameof(TcbIncrementApi.GetStatistics),
                nameof(TcbIncrementApi.GetOpenData), nameof(TcbIncrementApi.GetVoipSign));
        }

        [TestMethod]
        public void P1ImplementationsContainOfficialPathsAndRequestFields()
        {
            AssertSourceContains("OpenAPIs/OpenApi.cs",
                "/cgi-bin/openapi/quota/clear", "/cgi-bin/callback/check",
                "/cgi-bin/get_api_domain_ip", "ClearQuotaByAppSecret", "cgi_path", "check_operator");
            AssertSourceContains("AdvancedAPIs/WxApp/Business/BusinessApi.cs",
                "/wxa/getpluginopenpid", "/wxa/business/checkencryptedmsg",
                "/wxa/business/getuserencryptkey", "/wxa/resetusersessionkey",
                "encrypted_msg_hash", "sig_method");
            AssertSourceContains("AdvancedAPIs/WxApp/UrlScheme/UrlSchemeApi.cs",
                "/wxa/queryscheme", "query_type");
            AssertSourceContains("AdvancedAPIs/WxApp/UrlLinkApi.cs",
                "/wxa/query_urllink", "url_link", "query_type");
            AssertSourceContains("AdvancedAPIs/CustomService/CustomerServiceBusinessApi.cs",
                "/cgi-bin/business/register", "/cgi-bin/business/update",
                "/cgi-bin/business/get", "/cgi-bin/business/list",
                "account_name", "business_id");
            AssertSourceContains("AdvancedAPIs/CustomService/KfWorkApi.cs",
                "/customservice/work/get", "/customservice/work/bind",
                "/customservice/work/unbind", "corpid");
            AssertSourceContains("AdvancedAPIs/Message/UpdatableMessageApi.cs",
                "/cgi-bin/message/wxopen/activityid/create",
                "/cgi-bin/message/wxopen/updatablemsg/send",
                "/cgi-bin/message/wxopen/chattoolmsg/send",
                "activity_id", "target_state");
            AssertSourceContains("AdvancedAPIs/Message/ServiceCardApi.cs",
                "/wxa/set_user_notify", "/wxa/set_user_notifyext", "/wxa/get_user_notify",
                "notify_type", "notify_code", "content_json", "ext_json");
            AssertSourceContains("AdvancedAPIs/DataCube/DataCubeApi.cs",
                "/wxa/business/performance/boot", "time", "module", "params");
            AssertSourceContains("AdvancedAPIs/HardwareDevice/HardwareDeviceApi.cs",
                "/cgi-bin/message/device/subscribe/send", "/wxa/getsnticket",
                "/wxa/business/group/createid", "/wxa/business/group/getinfo",
                "/wxa/business/group/adddevice", "/wxa/business/group/removedevice",
                "/wxa/business/license/getpkglist", "/wxa/business/license/activedevice",
                "/wxa/business/license/getdeviceinfo", "modelId", "to_openid_list");
            AssertSourceContains("AdvancedAPIs/Operation/OperationApi.cs",
                "/wxa/getwxadevinfo", "/wxaapi/log/get_performance",
                "/wxaapi/log/get_scene", "/wxaapi/log/get_client_version",
                "/wxaapi/userlog/userlog_search", "/wxaapi/feedback/list",
                "/cgi-bin/media/getfeedbackmedia", "/wxaapi/log/jserr_detail",
                "/wxaapi/log/jserr_list", "networktype", "traceId", "filterMsg");
            AssertSourceContains("AdvancedAPIs/Tcb/TcbIncrementApi.cs",
                "/tcb/adddelayedfunctiontask", "/tcb/sendsmsv2", "/tcb/sendsms",
                "/tcb/createsendsmstask", "/tcb/cloudbasereport", "/tcb/describesmsrecords",
                "/tcb/describeextensionuploadinfo", "/tcb/getstatistics",
                "/wxa/getopendata", "/wxa/getvoipsign",
                "PostData", "ExtensionFiles", "cloudid_list", "group_id");
        }

        [TestMethod]
        public void P1ResponseModelsMapOfficialFieldNamesAndNumericValues()
        {
            const string userJson = @"{
  ""errcode"": 0,
  ""vaild"": true,
  ""create_time"": 1784800000,
  ""key_info_list"": [{
    ""encrypt_key"": ""key"", ""version"": 2, ""expire_in"": 86400,
    ""iv"": ""iv"", ""create_time"": 1784800001
  }]
}";
            const string schemeJson = @"{
  ""errcode"": 0,
  ""scheme_info"": {
    ""appid"": ""wx123"", ""path"": ""pages/index/index"", ""query"": ""id=1"",
    ""create_time"": 1784800000, ""expire_time"": 1784886400, ""env_version"": ""release""
  },
  ""quota_info"": { ""remain_visit_quota"": 999 }
}";
            const string operationJson = @"{
  ""errcode"": 0,
  ""list"": [{
    ""record_id"": 9007199254740991, ""create_time"": 1784800000,
    ""content"": ""feedback"", ""phone"": 13800138000, ""openid"": ""openid"",
    ""nickname"": ""user"", ""head_url"": ""https://example.test/avatar"",
    ""type"": 1, ""mediaIds"": [""media-1""], ""systemInfo"": ""iOS""
  }],
  ""total_num"": 1
}";
            const string hardwareJson = @"{
  ""errcode"": 0,
  ""pkg_list"": [{
    ""pkg_id"": ""pkg-1"", ""pkg_type"": 1, ""start_time"": 1784800000,
    ""end_time"": 1816336000, ""pkg_status"": 1, ""used"": 2, ""all"": 100
  }],
  ""max_active_number"": 1000
}";
            const string smsJson = @"{
  ""SmsRecords"": [{
    ""Mobile"": ""+8613800138000"", ""Content"": ""message"", ""ContentSize"": 7,
    ""Fee"": 1, ""CreateTime"": ""2026-07-24 12:00:00"",
    ""ReceivedTime"": ""2026-07-24 12:00:01"", ""Status"": ""sent"", ""Remarks"": ""ok""
  }],
  ""TotalCount"": 1,
  ""RequestId"": ""request-1""
}";

            var user = JsonSerializer.Deserialize<GetUserEncryptKeyJsonResult>(userJson);
            var scheme = JsonSerializer.Deserialize<QuerySchemeJsonResult>(schemeJson);
            var operation = JsonSerializer.Deserialize<GetFeedbackJsonResult>(operationJson);
            var hardware = JsonSerializer.Deserialize<GetLicensePackageListJsonResult>(hardwareJson);
            var sms = JsonSerializer.Deserialize<DescribeSmsRecordsJsonResult>(smsJson);

            Assert.IsNotNull(user);
            Assert.AreEqual(2, user.key_info_list[0].version);
            Assert.AreEqual(86400L, user.key_info_list[0].expire_in);
            Assert.IsNotNull(scheme);
            Assert.AreEqual(999L, scheme.quota_info.remain_visit_quota);
            Assert.IsNotNull(operation);
            Assert.AreEqual(9007199254740991L, operation.list[0].record_id);
            Assert.AreEqual("media-1", operation.list[0].mediaIds[0]);
            Assert.IsNotNull(hardware);
            Assert.AreEqual(100L, hardware.pkg_list[0].all);
            Assert.AreEqual(1000L, hardware.max_active_number);
            Assert.IsNotNull(sms);
            Assert.AreEqual("sent", sms.SmsRecords[0].Status);
            Assert.AreEqual("request-1", sms.RequestId);
        }

        private static void AssertMethodPairs(Type type, params string[] syncMethodNames)
        {
            var methodNames = type.GetMethods().Select(method => method.Name).ToArray();
            foreach (var syncMethodName in syncMethodNames)
            {
                CollectionAssert.Contains(methodNames, syncMethodName);
                CollectionAssert.Contains(methodNames, syncMethodName + "Async");
            }
        }

        private static void AssertSourceContains(string relativePath, params string[] expectedValues)
        {
            var projectDirectory = Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.WxOpen", "src", "Senparc.Weixin.WxOpen", "Senparc.Weixin.WxOpen");
            var source = File.ReadAllText(Path.Combine(projectDirectory,
                relativePath.Replace('/', Path.DirectorySeparatorChar)));

            foreach (var expected in expectedValues)
            {
                StringAssert.Contains(source, expected, $"{relativePath} 缺少官方契约：{expected}");
            }
        }

        private static string FindRepositoryRoot()
        {
            foreach (var startPath in new[] { Directory.GetCurrentDirectory(), AppContext.BaseDirectory })
            {
                var directory = new DirectoryInfo(startPath);
                while (directory != null)
                {
                    if (Directory.Exists(Path.Combine(directory.FullName, ".git")) &&
                        Directory.Exists(Path.Combine(directory.FullName, "src", "Senparc.Weixin.WxOpen")))
                    {
                        return directory.FullName;
                    }

                    directory = directory.Parent;
                }
            }

            throw new DirectoryNotFoundException("无法定位 WeiXinMPSDK 仓库根目录。");
        }
    }
}
