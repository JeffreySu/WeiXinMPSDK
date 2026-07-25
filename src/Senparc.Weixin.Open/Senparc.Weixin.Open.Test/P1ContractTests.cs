using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.Open.WxOpenAPIs;
using Senparc.Weixin.Open.WxaAPIs;
using Senparc.Weixin.Open.WxaAPIs.P1;
using Senparc.Weixin.Open.WxaAPIs.WxaEmbedded;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Senparc.Weixin.Open.Test
{
    [TestClass]
    public class P1ContractTests
    {
        [TestMethod]
        public void P1ApiSurfaceContainsAll48SyncAndAsyncEntries()
        {
            var methodCount = 0;

            methodCount += AssertMethodPairs(typeof(ComponentOpenApi),
                nameof(ComponentOpenApi.StartPushTicket),
                nameof(ComponentOpenApi.GetComponentQuota),
                nameof(ComponentOpenApi.GetAuthorizerQuota),
                nameof(ComponentOpenApi.GetComponentRid),
                nameof(ComponentOpenApi.GetAuthorizerRid),
                nameof(ComponentOpenApi.ClearComponentQuota),
                nameof(ComponentOpenApi.ClearAuthorizerQuota),
                nameof(ComponentOpenApi.ClearComponentQuotaByAppSecret),
                nameof(ComponentOpenApi.CheckComponentCallback),
                nameof(ComponentOpenApi.CheckAuthorizerCallback),
                nameof(ComponentOpenApi.GetComponentApiDomainIp),
                nameof(ComponentOpenApi.GetAuthorizerApiDomainIp));
            methodCount += AssertMethodPairs(typeof(WxaManagementApi),
                nameof(WxaManagementApi.GetFetchDataSetting),
                nameof(WxaManagementApi.SetPreFetchDataSetting),
                nameof(WxaManagementApi.SetPeriodFetchDataSetting),
                nameof(WxaManagementApi.GetBindOpenAccountEntity),
                nameof(WxaManagementApi.GetSettingCategories),
                nameof(WxaManagementApi.GetCategoriesByType),
                nameof(WxaManagementApi.GetCategoryNames),
                nameof(WxaManagementApi.GetVisitStatus),
                nameof(WxaManagementApi.GetCodePrivacyInfo));
            methodCount += AssertMethodPairs(typeof(AuthAndIcpApi),
                nameof(AuthAndIcpApi.GetIcpMedia),
                nameof(AuthAndIcpApi.SubmitAuthAndIcp),
                nameof(AuthAndIcpApi.QueryAuthAndIcp));
            methodCount += AssertMethodPairs(typeof(WxaCapabilityApi),
                nameof(WxaCapabilityApi.ApplyLiveInfo),
                nameof(WxaCapabilityApi.ApplyLogisticsMessagePlugin),
                nameof(WxaCapabilityApi.ApplyLogisticsReturnPlugin),
                nameof(WxaCapabilityApi.ApplyLogisticsQueryPlugin));
            methodCount += AssertMethodPairs(typeof(WeDataApi),
                nameof(WeDataApi.GetLoginConfig),
                nameof(WeDataApi.SetLoginConfig),
                nameof(WeDataApi.GetPermissionList),
                nameof(WeDataApi.SetUserPermission),
                nameof(WeDataApi.QueryBindList),
                nameof(WeDataApi.UnbindUser),
                nameof(WeDataApi.Login));
            methodCount += AssertMethodPairs(typeof(WxaEmbeddedApi),
                nameof(WxaEmbeddedApi.AddEmbedded),
                nameof(WxaEmbeddedApi.DelEmbedded),
                nameof(WxaEmbeddedApi.DelAuthorize),
                nameof(WxaEmbeddedApi.GetList),
                nameof(WxaEmbeddedApi.GetOwnList),
                nameof(WxaEmbeddedApi.SetAuthorize));
            methodCount += AssertMethodPairs(typeof(ManagedOfficialAccountApi),
                nameof(ManagedOfficialAccountApi.GetLinkMiniprogram),
                nameof(ManagedOfficialAccountApi.LinkMiniprogram),
                nameof(ManagedOfficialAccountApi.UnlinkMiniprogram));
            methodCount += AssertMethodPairs(typeof(QrCodeJumpApi),
                nameof(QrCodeJumpApi.Get),
                nameof(QrCodeJumpApi.AddOrUpdate),
                nameof(QrCodeJumpApi.Publish),
                nameof(QrCodeJumpApi.Delete));

            Assert.AreEqual(48, methodCount, "Open P1 应提供 48 对同步和异步入口。");
        }

        [TestMethod]
        public void P1ImplementationsContainOfficialPathsTokensAndFields()
        {
            AssertSourceContains("ComponentAPIs/ComponentOpenApi.cs",
                "/cgi-bin/component/api_start_push_ticket",
                "/cgi-bin/openapi/quota/get", "/cgi-bin/openapi/rid/get",
                "/cgi-bin/openapi/quota/clear", "/cgi-bin/component/clear_quota/v2",
                "/cgi-bin/callback/check", "/cgi-bin/get_api_domain_ip",
                "component_appid", "component_secret", "appsecret", "cgi_path", "check_operator");
            AssertSourceContains("WxaAPIs/P1/P1Api.cs",
                "/wxa/fetchdatasetting", "/cgi-bin/open/sameentity",
                "/cgi-bin/wxopen/getcategory", "/cgi-bin/wxopen/getcategoriesbytype",
                "/wxa/get_category", "/wxa/getvisitstatus", "/wxa/security/get_code_privacy_info",
                "/wxa/icp/get_icp_media", "/wxa/sec/submit_auth_and_icp",
                "/wxa/sec/query_auth_and_icp", "/wxa/business/applyliveinfo",
                "/cgi-bin/express/delivery/open_msg/open_openmsg",
                "/cgi-bin/express/delivery/return/open_return",
                "/cgi-bin/express/delivery/open_msg/open_query_plugin",
                "/wedata/wedata_get_login_config", "/wedata/wedata_set_login_config",
                "/wedata/wedata_get_perm_list", "/wedata/wedata_set_user_perm",
                "/wedata/wedata_query_bind_list", "/wedata/wedata_unbind_user",
                "/wedata/wedata_login", "authorizerAccessToken", "componentAccessToken");
            AssertSourceContains("WxaAPIs/WxaEmbedded/WxaEmbeddedApi.cs",
                "/wxaapi/wxaembedded/add_embedded", "/wxaapi/wxaembedded/del_embedded",
                "/wxaapi/wxaembedded/del_authorize", "/wxaapi/wxaembedded/get_list",
                "/wxaapi/wxaembedded/get_own_list", "/wxaapi/wxaembedded/set_authorize");
            AssertSourceContains("WxOpenAPIs/ManagedOfficialAccountApi.cs",
                "/cgi-bin/wxopen/wxamplinkget", "/cgi-bin/wxopen/wxamplink",
                "/cgi-bin/wxopen/wxampunlink", "/cgi-bin/wxopen/qrcodejumpget",
                "/cgi-bin/wxopen/qrcodejumpadd", "/cgi-bin/wxopen/qrcodejumppublish",
                "/cgi-bin/wxopen/qrcodejumpdelete", "authorizerAccessToken",
                "notify_users", "show_profile", "prefix_list", "permit_sub_rule");

            AssertTokenParameter(typeof(ComponentOpenApi), nameof(ComponentOpenApi.GetComponentQuota),
                "componentAccessToken");
            AssertTokenParameter(typeof(ComponentOpenApi), nameof(ComponentOpenApi.GetAuthorizerQuota),
                "authorizerAccessToken");
            AssertTokenParameter(typeof(WeDataApi), nameof(WeDataApi.GetLoginConfig),
                "componentAccessToken");
            AssertTokenParameter(typeof(WeDataApi), nameof(WeDataApi.GetPermissionList),
                "authorizerAccessToken");
            AssertTokenParameter(typeof(ManagedOfficialAccountApi),
                nameof(ManagedOfficialAccountApi.GetLinkMiniprogram), "authorizerAccessToken");
        }

        [TestMethod]
        public void P1ResponseModelsPreserveOfficialWireShapesAndLargeTimestamps()
        {
            var quota = JsonConvert.DeserializeObject<ComponentQuotaGetJsonResult>(
                "{\"errcode\":0,\"quota\":{\"daily_limit\":100000,\"used\":1,\"remain\":99999}," +
                "\"rate_limit\":{\"call_count\":10,\"refresh_second\":60}}");
            var rid = JsonConvert.DeserializeObject<ComponentRidGetJsonResult>(
                "{\"errcode\":0,\"request\":{\"invoke_time\":5178368698,\"cost_in_ms\":12," +
                "\"request_url\":\"/cgi-bin/test\",\"client_ip\":\"127.0.0.1\"}}");
            var categories = JsonConvert.DeserializeObject<CategoriesByTypeJsonResult>(
                "{\"errcode\":0,\"categories_list\":{\"categories\":[{" +
                "\"id\":1,\"name\":\"工具\",\"level\":1,\"father\":0," +
                "\"children\":[{\"id\":2,\"name\":\"子类\",\"level\":2}] }]}}");
            var bindList = JsonConvert.DeserializeObject<WeDataBindListJsonResult>(
                "{\"errcode\":0,\"info\":[{\"uid\":\"u1\",\"create_time\":5178368698," +
                "\"update_time\":5178368799,\"is_bind\":1,\"perm\":[{\"perm_id\":\"p1\"}]}]}");
            var login = JsonConvert.DeserializeObject<WeDataLoginJsonResult>(
                "{\"errcode\":0,\"base_resp\":{\"ret\":0,\"err_msg\":\"ok\"}," +
                "\"redirect_url\":\"https://example.test\",\"expire_at\":5178368698}");
            var managedAccount = JsonConvert.DeserializeObject<WxaMpLinkGetJsonResult>(
                "{\"errcode\":0,\"wxopens\":{\"items\":[{\"appid\":\"wx1\",\"status\":1," +
                "\"func_infos\":[{\"id\":1,\"name\":\"微信认证\",\"status\":1}]}]}}");
            var qrCode = JsonConvert.DeserializeObject<QrCodeJumpGetJsonResult>(
                "{\"errcode\":0,\"rule_list\":[{\"prefix\":\"https://example.test/\"," +
                "\"path\":\"pages/index\",\"state\":1,\"open_version\":1}],\"total_count\":1}");
            var embedded = JsonConvert.DeserializeObject<GetListJsonResult>(
                "{\"errcode\":0,\"embedded_flag\":1,\"wxa_embedded_list\":[{" +
                "\"appid\":\"wx2\",\"create_time\":\"1784800000\",\"status\":1}]}");

            Assert.AreEqual(99999, quota.quota.remain);
            Assert.AreEqual(5178368698L, rid.request.invoke_time);
            Assert.AreEqual("子类", categories.categories_list.categories[0].children[0].name);
            Assert.AreEqual(5178368799L, bindList.info[0].update_time);
            Assert.AreEqual(5178368698L, login.expire_at);
            Assert.AreEqual("微信认证", managedAccount.wxopens.items[0].func_infos[0].name);
            Assert.AreEqual("pages/index", qrCode.rule_list[0].path);
            Assert.AreEqual("wx2", embedded.wxa_embedded_list[0].appid);
        }

        private static int AssertMethodPairs(Type type, params string[] syncMethodNames)
        {
            var methodNames = type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            foreach (var syncMethodName in syncMethodNames)
            {
                CollectionAssert.Contains(methodNames, syncMethodName, $"{type.Name}.{syncMethodName}");
                CollectionAssert.Contains(methodNames, syncMethodName + "Async",
                    $"{type.Name}.{syncMethodName}Async");
            }

            return syncMethodNames.Length;
        }

        private static void AssertTokenParameter(Type type, string methodName, string expectedName)
        {
            var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Assert.IsNotNull(method, $"{type.Name}.{methodName}");
            Assert.AreEqual(expectedName, method.GetParameters()[0].Name, methodName);
        }

        private static void AssertSourceContains(string relativePath, params string[] expectedValues)
        {
            var sourceRoot = Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Open", "Senparc.Weixin.Open");
            var source = File.ReadAllText(Path.Combine(sourceRoot,
                relativePath.Replace('/', Path.DirectorySeparatorChar)));

            foreach (var expectedValue in expectedValues)
            {
                Assert.IsTrue(source.Contains(expectedValue),
                    $"{relativePath} 缺少官方契约：{expectedValue}");
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
                        Directory.Exists(Path.Combine(directory.FullName, "src", "Senparc.Weixin.Open")))
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
