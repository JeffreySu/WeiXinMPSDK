using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Analysis;
using Senparc.Weixin.MP.AdvancedAPIs.Draft;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs.Url;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.OpenAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class P1ContractTests
    {
        [TestMethod]
        public void P1ApiSurfaceContainsSyncAndAsyncEntries()
        {
            AssertMethodPair(typeof(CommonApi), nameof(CommonApi.CallbackCheck));
            AssertMethodPair(typeof(CommonApi), nameof(CommonApi.GetApiDomainIp));
            AssertMethodPair(typeof(CommonApi), nameof(CommonApi.ClearQuotaByAppSecret));
            AssertMethodPair(typeof(OpenApi), nameof(OpenApi.ClearQuota));
            AssertMethodPair(typeof(DraftApi), nameof(DraftApi.Switch));
            AssertMethodPair(typeof(ProductCardApi), nameof(ProductCardApi.GetProductCardInfo));
            AssertMethodPair(typeof(TemplateApi), nameof(TemplateApi.QueryBlockTemplateMessage));
            AssertMethodPair(typeof(UrlApi), nameof(UrlApi.GenerateShorten));
            AssertMethodPair(typeof(UrlApi), nameof(UrlApi.FetchShorten));
            AssertMethodPair(typeof(UserApi), nameof(UserApi.ChangeOpenId));
            AssertMethodPair(typeof(AnalysisApi), nameof(AnalysisApi.GetPublishedArticleRead));
            AssertMethodPair(typeof(AnalysisApi), nameof(AnalysisApi.GetPublishedArticleShare));
            AssertMethodPair(typeof(AnalysisApi), nameof(AnalysisApi.GetPublishedArticleBizSummary));
            AssertMethodPair(typeof(AnalysisApi), nameof(AnalysisApi.GetPublishedArticleTotalDetail));
        }

        [TestMethod]
        public void P1ImplementationsContainOfficialPathsAndRequestFields()
        {
            AssertSourceContains("CommonAPIs/CommonApi.cs",
                "/cgi-bin/callback/check",
                "/cgi-bin/get_api_domain_ip",
                "/cgi-bin/clear_quota/v2",
                "check_operator",
                "appsecret");
            AssertSourceContains("OpenAPIs/OpenApi.cs",
                "/cgi-bin/openapi/quota/clear",
                "cgi_path");
            AssertSourceContains("AdvancedAPIs/Draft/DraftApi.cs",
                "/cgi-bin/draft/switch",
                "checkonly=1");
            AssertSourceContains("AdvancedAPIs/Draft/ProductCardApi.cs",
                "/channels/ec/service/product/getcardinfo",
                "product_id",
                "article_type",
                "card_type");
            AssertSourceContains("AdvancedAPIs/TemplateMessage/TemplateApi.cs",
                "/wxa/sec/queryblocktmplmsg",
                "tmpl_msg_id",
                "largest_id");
            AssertSourceContains("AdvancedAPIs/Url/UrlApi.cs",
                "/cgi-bin/shorten/gen",
                "/cgi-bin/shorten/fetch",
                "long_data",
                "short_key");
            AssertSourceContains("AdvancedAPIs/User/UserApi.cs",
                "/cgi-bin/changeopenid",
                "from_appid",
                "openid_list");
            AssertSourceContains("AdvancedAPIs/Analysis/AnalysisApi.cs",
                "/datacube/getarticleread",
                "/datacube/getarticleshare",
                "/datacube/getbizsummary",
                "/datacube/getarticletotaldetail");
        }

        [TestMethod]
        public void P1ResponseModelsMapOfficialFields()
        {
            const string callbackJson = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""dns"": [{ ""ip"": ""203.0.113.10"", ""real_operator"": ""UNICOM"" }],
  ""ping"": [{ ""ip"": ""203.0.113.11"", ""from_operator"": ""CAP"", ""package_loss"": ""0%"", ""time"": ""12ms"" }]
}";
            const string changeOpenIdJson = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""result_list"": [{ ""ori_openid"": ""old-openid"", ""new_openid"": ""new-openid"", ""err_msg"": ""ok"" }]
}";
            const string articleJson = @"{
  ""list"": [{
    ""ref_date"": ""2026-07-23"",
    ""msgid"": ""10001_1"",
    ""detail"": { ""read_user"": 12, ""read_user_source"": [{ ""user_count"": 7, ""scene_desc"": ""公众号会话"" }] }
  }],
  ""is_delay"": false
}";
            const string blockedTemplateJson = @"{
  ""errcode"": 0,
  ""errmsg"": ""ok"",
  ""msginfo"": {
    ""id"": 2,
    ""tmpl_msg_id"": ""template-id"",
    ""title"": ""测试标题"",
    ""content"": ""测试内容"",
    ""send_timestamp"": 1788796187,
    ""openid"": ""openid""
  }
}";

            var callback = JsonSerializer.Deserialize<CallbackCheckJsonResult>(callbackJson);
            var changeOpenId = JsonSerializer.Deserialize<ChangeOpenIdJsonResult>(changeOpenIdJson);
            var article = JsonSerializer.Deserialize<PublishedArticleAnalysisResult<PublishedArticleReadItem>>(articleJson);
            var blockedTemplate = JsonSerializer.Deserialize<QueryBlockTemplateMessageResult>(blockedTemplateJson);

            Assert.IsNotNull(callback);
            Assert.AreEqual("UNICOM", callback.dns[0].real_operator);
            Assert.AreEqual("0%", callback.ping[0].package_loss);
            Assert.IsNotNull(changeOpenId);
            Assert.AreEqual("new-openid", changeOpenId.result_list[0].new_openid);
            Assert.IsNotNull(article);
            Assert.IsFalse(article.is_delay);
            Assert.AreEqual(12L, article.list[0].detail.read_user);
            Assert.AreEqual("公众号会话", article.list[0].detail.read_user_source[0].scene_desc);
            Assert.IsNotNull(blockedTemplate);
            Assert.AreEqual(2L, blockedTemplate.msginfo.id);
            Assert.AreEqual("template-id", blockedTemplate.msginfo.tmpl_msg_id);
        }

        private static void AssertMethodPair(Type type, string syncMethodName)
        {
            var methodNames = type.GetMethods().Select(method => method.Name).ToArray();
            CollectionAssert.Contains(methodNames, syncMethodName);
            CollectionAssert.Contains(methodNames, syncMethodName + "Async");
        }

        private static void AssertSourceContains(string relativePath, params string[] expectedValues)
        {
            var projectDirectory = Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.MP", "Senparc.Weixin.MP");
            var source = File.ReadAllText(Path.Combine(projectDirectory,
                relativePath.Replace('/', Path.DirectorySeparatorChar)));

            foreach (var expected in expectedValues)
            {
                StringAssert.Contains(source, expected, $"{relativePath} 缺少官方契约：{expected}");
            }
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
                if (string.IsNullOrEmpty(startPath))
                {
                    continue;
                }

                var directory = new DirectoryInfo(startPath);
                while (directory != null)
                {
                    if (Directory.Exists(Path.Combine(directory.FullName, ".git")) &&
                        Directory.Exists(Path.Combine(directory.FullName, "src", "Senparc.Weixin.MP")))
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
