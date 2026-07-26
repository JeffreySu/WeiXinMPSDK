using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.Open.WxaAPIs;
using Senparc.Weixin.Open.WxaAPIs.Ams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Senparc.Weixin.Open.Test.WxaAPIs.Ams
{
    [TestClass]
    public class AmsContractTests
    {
        private static readonly IReadOnlyDictionary<string, AmsOfficialContract> OfficialContracts =
            new Dictionary<string, AmsOfficialContract>
            {
                [nameof(AmsApi.SetShareRatio)] = Contract("/wxa/setdefaultamsinfo", "set_share_ratio", "componentAccessToken"),
                [nameof(AmsApi.GetShareRatio)] = Contract("/wxa/getdefaultamsinfo", "get_share_ratio", "componentAccessToken"),
                [nameof(AmsApi.SetCustomShareRatio)] = Contract("/wxa/setdefaultamsinfo", "agency_set_custom_share_ratio", "componentAccessToken"),
                [nameof(AmsApi.GetCustomShareRatio)] = Contract("/wxa/getdefaultamsinfo", "agency_get_custom_share_ratio", "componentAccessToken"),
                [nameof(AmsApi.AgencyCheckCanOpenPublisher)] = Contract("/wxa/operationams", "agency_check_can_open_publisher", "authorizerAccessToken"),
                [nameof(AmsApi.AgencyCreatePublisher)] = Contract("/wxa/operationams", "agency_create_publisher", "authorizerAccessToken"),
                [nameof(AmsApi.AgencyCreateAdunit)] = Contract("/wxa/operationams", "agency_create_adunit", "authorizerAccessToken"),
                [nameof(AmsApi.AgencyUpdateAdunit)] = Contract("/wxa/operationams", "agency_update_adunit", "authorizerAccessToken"),
                [nameof(AmsApi.AgencyGetTmplType)] = Contract("/wxa/operationams", "agency_get_tmpl_type", "authorizerAccessToken"),
                [nameof(AmsApi.GetAgencyTmplIdList)] = Contract("/wxa/operationams", "get_agency_ad_unit_list", "authorizerAccessToken"),
                [nameof(AmsApi.SetCoverAdposStatus)] = Contract("/wxa/operationams", "agency_set_cover_adpos_status", "authorizerAccessToken"),
                [nameof(AmsApi.SetCoverAdposScene)] = Contract("/wxa/operationams", "agency_set_cover_adpos_scene", "authorizerAccessToken"),
                [nameof(AmsApi.GetCoverAdposStatus)] = Contract("/wxa/operationams", "agency_get_cover_adpos_status", "authorizerAccessToken"),
                [nameof(AmsApi.GetCoverAdposScene)] = Contract("/wxa/operationams", "agency_get_cover_adpos_scene", "authorizerAccessToken"),
                [nameof(AmsApi.GetAdunitList)] = Contract("/wxa/operationams", "agency_get_adunit_list", "authorizerAccessToken"),
                [nameof(AmsApi.GetAdunitCode)] = Contract("/wxa/operationams", "agency_get_adunit_code", "authorizerAccessToken"),
                [nameof(AmsApi.GetBlackList)] = Contract("/wxa/operationams", "agency_get_black_list", "authorizerAccessToken"),
                [nameof(AmsApi.SetBlackList)] = Contract("/wxa/operationams", "agency_set_black_list", "authorizerAccessToken"),
                [nameof(AmsApi.GetAmsCategoryBlackList)] = Contract("/wxa/operationams", "agency_get_mp_amscategory_blacklist", "authorizerAccessToken"),
                [nameof(AmsApi.SetAmsCategoryBlackList)] = Contract("/wxa/operationams", "agency_set_mp_amscategory_blacklist", "authorizerAccessToken"),
                [nameof(AmsApi.GetAdposGenenral)] = Contract("/wxa/operationams", "agency_get_adpos_genenral", "authorizerAccessToken"),
                [nameof(AmsApi.GetAdposDetail)] = Contract("/wxa/operationams", "agency_get_adunit_general", "authorizerAccessToken"),
                [nameof(AmsApi.GetAgencyAdsStat)] = Contract("/wxa/getdefaultamsinfo", "get_agency_ads_stat", "authorizerAccessToken"),
                [nameof(AmsApi.GetAgencyAdsDetail)] = Contract("/wxa/getdefaultamsinfo", "get_agency_ads_detail", "authorizerAccessToken"),
                [nameof(AmsApi.GetSettlement)] = Contract("/wxa/operationams", "agency_get_settlement", "authorizerAccessToken"),
                [nameof(AmsApi.GetAgencySettlement)] = Contract("/wxa/getdefaultamsinfo", "get_agency_settled_revenue", "componentAccessToken")
            };

        [TestMethod]
        public void ApiSurfaceContainsTwentySixOfficialSyncAndAsyncEntries()
        {
            var methods = typeof(AmsApi).GetMethods(BindingFlags.Public | BindingFlags.Static);

            Assert.AreEqual(26, OfficialContracts.Count);
            Assert.AreEqual(52, methods.Length, "26 个官方接口均应提供同步和异步入口。");

            foreach (var pair in OfficialContracts)
            {
                var sync = GetPublicMethod(pair.Key);
                var async = GetPublicMethod(pair.Key + "Async");

                Assert.IsNotNull(sync, pair.Key);
                Assert.IsNotNull(async, pair.Key + "Async");
                Assert.AreEqual(pair.Value.TokenParameterName, sync.GetParameters()[0].Name, pair.Key);
                Assert.AreEqual(pair.Value.TokenParameterName, async.GetParameters()[0].Name, pair.Key + "Async");
            }
        }

        [TestMethod]
        public void EveryPublicEntryUsesOfficialEndpointAndAction()
        {
            foreach (var pair in OfficialContracts)
            {
                AssertMethodContainsLiteral(pair.Key, pair.Value.Path);
                AssertMethodContainsLiteral(pair.Key, pair.Value.Action);
                AssertMethodContainsLiteral(pair.Key + "Async", pair.Value.Path);
                AssertMethodContainsLiteral(pair.Key + "Async", pair.Value.Action);
            }
        }

        [TestMethod]
        public void BuildUrlEncodesTokenAndAction()
        {
            var buildUrl = typeof(AmsApi).GetMethod("BuildUrl", BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(buildUrl);

            var url = (string)buildUrl.Invoke(null,
                new object[] { "token+空 格&x=1", "/wxa/operationams", "action/test" });

            StringAssert.Contains(url, "/wxa/operationams?action=action%2Ftest&access_token=");
            Assert.IsFalse(url.Contains("token+空 格&x=1"));
            StringAssert.Contains(url.ToUpperInvariant(), "%E7%A9%BA");
            StringAssert.Contains(url.ToUpperInvariant(), "%26X%3D1");
        }

        [TestMethod]
        public void RequestModelsPreserveOfficialStringAndOptionalShapes()
        {
            var create = new AmsCreateAdUnitRequest
            {
                name = "原生广告",
                type = "SLOT_ID_WEAPP_TEMPLATE",
                tmpl_id = "adunit-template-1"
            };
            var blackList = new AmsSetBlackListRequest
            {
                op = 1,
                list = "[{\"type\":1,\"id\":\"gh_test\"}]"
            };

            using var createDocument = JsonDocument.Parse(Serialize(create));
            using var blackListDocument = JsonDocument.Parse(Serialize(blackList));

            Assert.AreEqual("adunit-template-1", createDocument.RootElement.GetProperty("tmpl_id").GetString());
            Assert.IsFalse(createDocument.RootElement.TryGetProperty("tmpl_type", out _));
            Assert.IsFalse(createDocument.RootElement.TryGetProperty("unlock_reward_duration", out _));
            Assert.AreEqual(JsonValueKind.String, blackListDocument.RootElement.GetProperty("list").ValueKind,
                "官方要求 list 为 JSON 数组序列化后的字符串，而不是直接发送数组。");
            StringAssert.Contains(blackListDocument.RootElement.GetProperty("list").GetString(), "gh_test");
        }

        [TestMethod]
        public void ResponseModelsHandleBothOfficialErrorAndPayloadVariants()
        {
            var topLevel = JsonConvert.DeserializeObject<AmsShareRatioJsonResult>(
                "{\"ret\":2061,\"err_msg\":\"not configured\",\"share_ratio\":40}");
            var nested = JsonConvert.DeserializeObject<AmsAdUnitListJsonResult>(
                "{\"base_resp\":{\"ret\":0,\"err_msg\":\"ok\"},\"ad_unit\":[{" +
                "\"ad_unit_id\":\"adunit-1\",\"ad_unit_size\":[{\"height\":166,\"width\":582}]}],\"total_num\":1}");
            var blackList = JsonConvert.DeserializeObject<AmsBlackListJsonResult>(
                "{\"ret\":0,\"blacklist_biz\":[{\"id\":\"gh_test\",\"name\":\"测试\",\"icon\":\"icon.png\"}]}");

            Assert.AreEqual(2061, topLevel.ErrorCodeValue);
            Assert.AreEqual("not configured", topLevel.errmsg);
            Assert.AreEqual(0, nested.ErrorCodeValue);
            Assert.AreEqual("ok", nested.errmsg);
            Assert.AreEqual(582, nested.ad_unit[0].ad_unit_size[0].width,
                "官方参数表将 ad_unit_size 标为 object，但示例返回数组。");
            Assert.AreEqual("icon.png", blackList.blacklist_biz[0].icon,
                "官方参数表使用 url，示例使用 icon，模型应兼容示例字段。");
        }

        [TestMethod]
        public void DataAndSettlementModelsKeepNumericIncomeAndLongAmounts()
        {
            var detail = JsonConvert.DeserializeObject<AmsAgencyAdsDetailJsonResult>(
                "{\"ret\":0,\"list\":[{\"appid\":\"wx-test\",\"stat_item\":{" +
                "\"agency_income\":93883,\"publisher_income\":93883,\"ecpm\":6.790813743}}],\"total_num\":1}");
            var settlement = JsonConvert.DeserializeObject<AmsSettlementJsonResult>(
                "{\"base_resp\":{\"ret\":0,\"err_msg\":\"ok\"},\"revenue_all\":5178368698," +
                "\"settlement_list\":[{\"settled_revenue\":718926045,\"slot_revenue\":[{" +
                "\"slot_id\":\"SLOT_ID_WEAPP_BANNER\",\"slot_settled_revenue\":34139443}]}]}");

            Assert.AreEqual(93883L, detail.list[0].stat_item.agency_income,
                "官方参数表误标 boolean，HTTPS 示例返回金额数字。");
            Assert.AreEqual(6.790813743m, detail.list[0].stat_item.ecpm);
            Assert.AreEqual(5178368698L, settlement.revenue_all);
            Assert.AreEqual(34139443L, settlement.settlement_list[0].slot_revenue[0].slot_settled_revenue);
        }

        private static AmsOfficialContract Contract(string path, string action, string tokenParameterName)
        {
            return new AmsOfficialContract(path, action, tokenParameterName);
        }

        private static MethodInfo GetPublicMethod(string methodName)
        {
            return typeof(AmsApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .SingleOrDefault(method => method.Name == methodName);
        }

        private static void AssertMethodContainsLiteral(string methodName, string expected)
        {
            var method = GetPublicMethod(methodName);
            Assert.IsNotNull(method, methodName);
            Assert.IsTrue(GetStringLiterals(method).Contains(expected), $"{methodName}: {expected}");
        }

        private static IEnumerable<string> GetStringLiterals(MethodInfo method)
        {
            var bytes = method?.GetMethodBody()?.GetILAsByteArray();
            if (bytes == null)
            {
                yield break;
            }

            for (var index = 0; index <= bytes.Length - 5; index++)
            {
                if (bytes[index] != 0x72)
                {
                    continue;
                }

                string value;
                try
                {
                    value = method.Module.ResolveString(BitConverter.ToInt32(bytes, index + 1));
                }
                catch (ArgumentException)
                {
                    continue;
                }

                yield return value;
            }
        }

        private static string Serialize(object value)
        {
            return SerializerHelper.GetJsonString(value, new JsonSetting(ignoreNulls: true));
        }

        private sealed class AmsOfficialContract
        {
            public AmsOfficialContract(string path, string action, string tokenParameterName)
            {
                Path = path;
                Action = action;
                TokenParameterName = tokenParameterName;
            }

            public string Path { get; }
            public string Action { get; }
            public string TokenParameterName { get; }
        }
    }
}
