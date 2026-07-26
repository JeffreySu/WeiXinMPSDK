using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.External;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
{
    [TestClass]
    public class ExternalCatalogContractTests
    {
        [TestMethod]
        public void CatalogAndInterceptApisExposeSyncAndAsyncEntrypoints()
        {
            var methodNames = typeof(ExternalApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var methodName in new[]
            {
                nameof(ExternalApi.OpenGidToChatId), nameof(ExternalApi.CreateProductAlbum),
                nameof(ExternalApi.GetProductAlbum), nameof(ExternalApi.GetProductAlbumList),
                nameof(ExternalApi.UpdateProductAlbum), nameof(ExternalApi.DeleteProductAlbum),
                nameof(ExternalApi.CreateInterceptRule), nameof(ExternalApi.GetInterceptRuleList),
                nameof(ExternalApi.GetInterceptRule), nameof(ExternalApi.UpdateInterceptRule),
                nameof(ExternalApi.DeleteInterceptRule)
            })
            {
                CollectionAssert.Contains(methodNames, methodName, methodName);
                CollectionAssert.Contains(methodNames, methodName + "Async", methodName + "Async");
            }

            var mediaMethods = typeof(MediaApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            CollectionAssert.Contains(mediaMethods, nameof(MediaApi.UploadAttachment));
            CollectionAssert.Contains(mediaMethods, nameof(MediaApi.UploadAttachmentAsync));
        }

        [TestMethod]
        public void CatalogAndInterceptApisUseOfficialPaths()
        {
            var root = FindRepositoryRoot();
            var source = File.ReadAllText(Path.Combine(root, "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "External", "ExternalCatalogApi.cs"));
            var mediaSource = File.ReadAllText(Path.Combine(root, "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Media", "MediaAttachmentApi.cs"));

            foreach (var path in new[]
            {
                "/cgi-bin/externalcontact/opengid_to_chatid",
                "/cgi-bin/externalcontact/add_product_album",
                "/cgi-bin/externalcontact/get_product_album",
                "/cgi-bin/externalcontact/get_product_album_list",
                "/cgi-bin/externalcontact/update_product_album",
                "/cgi-bin/externalcontact/delete_product_album",
                "/cgi-bin/externalcontact/add_intercept_rule",
                "/cgi-bin/externalcontact/get_intercept_rule_list",
                "/cgi-bin/externalcontact/get_intercept_rule",
                "/cgi-bin/externalcontact/update_intercept_rule",
                "/cgi-bin/externalcontact/del_intercept_rule"
            })
            {
                Assert.AreEqual(2, CountOccurrences(source, path + "\""), path);
            }

            Assert.AreEqual(2, CountOccurrences(mediaSource, "/cgi-bin/media/upload_attachment"));
            StringAssert.Contains(mediaSource, "[\"name\"] = \"media\"");
            StringAssert.Contains(mediaSource, "&media_type=");
            StringAssert.Contains(mediaSource, "&attachment_type=");
        }

        [TestMethod]
        public void ProductAlbumModelsPreservePriceAndLargeCreationTime()
        {
            var requestJson = JsonSerializer.Serialize(new ProductAlbumCreateRequest
            {
                description = "商品",
                price = 5000000,
                attachments = new[]
                {
                    new ProductAlbumAttachment
                    {
                        type = "image", image = new ProductAlbumImage { media_id = "media-1" }
                    }
                }
            });
            var result = JsonSerializer.Deserialize<ProductAlbumResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"product\":{\"product_id\":\"product-1\"," +
                "\"description\":\"商品\",\"price\":5000000,\"create_time\":4294967296," +
                "\"attachments\":[{\"type\":\"image\",\"image\":{\"media_id\":\"media-1\"}}]}}");

            StringAssert.Contains(requestJson, "\"price\":5000000");
            Assert.AreEqual(4294967296L, result.product.create_time);
            Assert.AreEqual("media-1", result.product.attachments[0].image.media_id);
        }

        [TestMethod]
        public void InterceptRuleModelsSupportBothOfficialSemanticShapes()
        {
            var createJson = JsonSerializer.Serialize(new InterceptRuleCreateRequest
            {
                rule_name = "规则",
                word_list = new[] { "敏感词" },
                semantics_list = new[] { 1, 2, 3 },
                intercept_type = 1,
                applicable_range = new InterceptRuleRange { department_list = new[] { 4294967296L } }
            });
            var detail = JsonSerializer.Deserialize<InterceptRuleResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"rule\":{\"rule_id\":\"rule-1\"," +
                "\"rule_name\":\"规则\",\"create_time\":4294967297,\"semantics_list\":[1]," +
                "\"extra_rule\":{\"semantics_list\":[2,3]},\"applicable_range\":{\"department_list\":[4294967296]}}}");

            StringAssert.Contains(createJson, "\"semantics_list\":[1,2,3]");
            Assert.AreEqual(4294967297L, detail.rule.create_time);
            Assert.AreEqual(1, detail.rule.semantics_list[0]);
            Assert.AreEqual(3, detail.rule.extra_rule.semantics_list[1]);
            Assert.AreEqual(4294967296L, detail.rule.applicable_range.department_list[0]);
            Assert.AreEqual(1, (int)ExternalAttachmentType.Moment);
            Assert.AreEqual(2, (int)ExternalAttachmentType.ProductAlbum);
        }

        private static int CountOccurrences(string value, string search)
        {
            return value.Split(new[] { search }, StringSplitOptions.None).Length - 1;
        }

        private static string FindRepositoryRoot([CallerFilePath] string sourceFilePath = null)
        {
            foreach (var startPath in new[]
            {
                Directory.GetCurrentDirectory(), AppContext.BaseDirectory, Path.GetDirectoryName(sourceFilePath)
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
