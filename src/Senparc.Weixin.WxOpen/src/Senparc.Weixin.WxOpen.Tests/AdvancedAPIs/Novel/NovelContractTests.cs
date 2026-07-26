using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Novel;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.Novel
{
    [TestClass]
    public class NovelContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(NovelApi.CreateBook)] = "/wxa/book/createbook",
                [nameof(NovelApi.UpdateBook)] = "/wxa/book/updatebook",
                [nameof(NovelApi.DeleteBook)] = "/wxa/book/deletebook",
                [nameof(NovelApi.ListBooks)] = "/wxa/book/listbook",
                [nameof(NovelApi.GetBook)] = "/wxa/book/getbook",
                [nameof(NovelApi.CreateChapter)] = "/wxa/book/createchapter",
                [nameof(NovelApi.BatchCreateChapters)] = "/wxa/book/batchcreatechapter",
                [nameof(NovelApi.DeleteChapter)] = "/wxa/book/deletechapter",
                [nameof(NovelApi.ReplaceChapter)] = "/wxa/book/replacechapter",
                [nameof(NovelApi.ListChapters)] = "/wxa/book/listchapter",
                [nameof(NovelApi.GetChapter)] = "/wxa/book/getchapter",
                [nameof(NovelApi.ReorderChapter)] = "/wxa/book/reorderchapter",
                [nameof(NovelApi.UpdateChapterSequence)] = "/wxa/book/updatechapterseq",
                [nameof(NovelApi.AuditBook)] = "/wxa/book/auditbook",
                [nameof(NovelApi.AddBookAuthorization)] = "/wxa/book/addbookauth",
                [nameof(NovelApi.QueryBookAuthorization)] = "/wxa/book/querybookauth",
                [nameof(NovelApi.DeleteBookAuthorization)] = "/wxa/book/delbookauth",
                [nameof(NovelApi.AddAppAuthorization)] = "/wxa/book/addbookauthbyappid",
                [nameof(NovelApi.QueryAppAuthorization)] = "/wxa/book/querybookauthv2",
                [nameof(NovelApi.DeleteAppAuthorization)] = "/wxa/book/delbookauthbyappid",
                [nameof(NovelApi.SetPreviewSetting)] = "/wxa/business/novelreader/setpreviewsetting",
                [nameof(NovelApi.GetPreviewSetting)] = "/wxa/business/novelreader/getpreviewsetting",
                [nameof(NovelApi.SetRecommendedNovels)] = "/wxa/business/novelreader/setrecmdnovel"
            };

        [TestMethod]
        public void ApiSurfaceContainsTwentyThreeSyncAndAsyncEntries()
        {
            var methods = typeof(NovelApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name)
                .ToArray();

            Assert.AreEqual(23, OfficialEndpoints.Count);
            foreach (var method in OfficialEndpoints.Keys)
            {
                CollectionAssert.Contains(methods, method);
                CollectionAssert.Contains(methods, method + "Async");
            }

            Assert.AreEqual(46, methods.Length);
        }

        [TestMethod]
        public void EveryPublicEntryUsesItsOfficialEndpoint()
        {
            foreach (var pair in OfficialEndpoints)
            {
                var sync = typeof(NovelApi).GetMethod(pair.Key, BindingFlags.Public | BindingFlags.Static);
                var async = typeof(NovelApi).GetMethod(pair.Key + "Async", BindingFlags.Public | BindingFlags.Static);

                CollectionAssert.Contains(GetStringLiterals(sync).ToArray(), pair.Value, pair.Key);
                CollectionAssert.Contains(GetStringLiterals(async).ToArray(), pair.Value, pair.Key + "Async");
            }
        }

        [TestMethod]
        public void BookRequestsKeepOfficialFieldNamesAndOmitUnsetValues()
        {
            var create = new NovelCreateBookRequest
            {
                title = "香蕉牛奶",
                intro = "奇幻之旅",
                cover_media_id = "media-1",
                author = "作者",
                first_category_id = 10001,
                second_category_id = 10002,
                third_category_id = 10003,
                complete_status = 1,
                keyword_list = new[] { "奇幻" }
            };
            var update = new NovelUpdateBookRequest
            {
                book_id = "book-1",
                need_volume = false,
                chapter_id_list = new[] { "chapter-2", "chapter-1" }
            };

            using var createDocument = JsonDocument.Parse(Serialize(create));
            using var updateDocument = JsonDocument.Parse(Serialize(update));

            Assert.AreEqual(10003, createDocument.RootElement.GetProperty("third_category_id").GetInt32());
            Assert.AreEqual("奇幻", createDocument.RootElement.GetProperty("keyword_list")[0].GetString());
            Assert.IsFalse(createDocument.RootElement.TryGetProperty("original_id", out _));
            Assert.IsFalse(createDocument.RootElement.TryGetProperty("chapter_order_method", out _));
            Assert.IsFalse(updateDocument.RootElement.GetProperty("need_volume").GetBoolean());
            Assert.AreEqual("chapter-2", updateDocument.RootElement.GetProperty("chapter_id_list")[0].GetString());
            Assert.IsFalse(updateDocument.RootElement.TryGetProperty("volume_list", out _));
        }

        [TestMethod]
        public void ChapterContractsCoverNestedInputAndBothBatchResultNames()
        {
            var request = new NovelBatchCreateChaptersRequest
            {
                book_id = "book-1",
                chapter_list = new[]
                {
                    new NovelChapterInput
                    {
                        chapter_title = "第一章",
                        content = "正文",
                        seq = 100
                    }
                }
            };

            using var requestDocument = JsonDocument.Parse(Serialize(request));
            var exampleResult = JsonConvert.DeserializeObject<NovelBatchCreateChaptersJsonResult>(
                "{\"errcode\":0,\"chapter_id_list\":[\"c1\",\"c2\"],\"conflict_original_id_list\":[\"o1\"]}");
            var tableResult = JsonConvert.DeserializeObject<NovelBatchCreateChaptersJsonResult>(
                "{\"errcode\":0,\"chapter_id\":[\"c3\"]}");

            Assert.AreEqual("第一章", requestDocument.RootElement.GetProperty("chapter_list")[0].GetProperty("chapter_title").GetString());
            Assert.AreEqual(100L, requestDocument.RootElement.GetProperty("chapter_list")[0].GetProperty("seq").GetInt64());
            Assert.IsFalse(requestDocument.RootElement.GetProperty("chapter_list")[0].TryGetProperty("custom_info", out _));
            Assert.AreEqual("c2", exampleResult.chapter_id_list[1]);
            Assert.AreEqual("o1", exampleResult.conflict_original_id_list[0]);
            Assert.AreEqual("c3", tableResult.chapter_id[0]);
        }

        [TestMethod]
        public void BookAndChapterResponsesFollowObjectsAndStringsFromExamples()
        {
            const string bookJson = "{\"errcode\":0,\"book\":{"
                + "\"book_id\":\"b1\",\"title\":\"香蕉牛奶\",\"volume_list\":[{"
                + "\"volume_title\":\"第一卷\",\"start_index\":0,\"end_index\":2}],"
                + "\"audit_info\":{\"audit_status\":3,\"create_time\":1700000000}}}";
            const string chapterJson = "{\"errcode\":0,\"chapter\":{"
                + "\"book_id\":\"b1\",\"chapter_id\":\"c1\",\"chapter_title\":\"第一章\","
                + "\"content\":\"正文\",\"word_cnt\":2,\"volume_index\":-1}}";

            var book = JsonConvert.DeserializeObject<NovelGetBookJsonResult>(bookJson);
            var chapter = JsonConvert.DeserializeObject<NovelGetChapterJsonResult>(chapterJson);

            Assert.AreEqual("香蕉牛奶", book.book.title);
            Assert.AreEqual("第一卷", book.book.volume_list[0].volume_title);
            Assert.AreEqual(3, book.book.audit_info.audit_status);
            Assert.AreEqual("b1", chapter.chapter.book_id, "官方返回表漏列 book_id，但示例包含该字段。");
            Assert.AreEqual("正文", chapter.chapter.content, "官方返回表漏列 content，但示例包含该字段。");
        }

        [TestMethod]
        public void AuthorizationContractsSupportBookAndAccountQueryScenarios()
        {
            var add = new NovelAddBookAuthorizationRequest
            {
                books = new[]
                {
                    new NovelBookAuthorizationInput
                    {
                        book_id = "b1",
                        grantee_appid = "wx-grantee",
                        expire_time = 2147483646L
                    }
                }
            };
            var query = new NovelQueryAppAuthorizationRequest
            {
                type = 1,
                book_ids = new[] { "b1", "b2" }
            };

            using var addDocument = JsonDocument.Parse(Serialize(add));
            using var queryDocument = JsonDocument.Parse(Serialize(query));
            var result = JsonConvert.DeserializeObject<NovelQueryAppAuthorizationJsonResult>(
                "{\"errcode\":0,\"book_results\":[{\"book_id\":\"b1\","
                + "\"grantor_appid\":\"wx-a\",\"grantee_appid\":\"wx-b\",\"expire_time\":1700000000}],"
                + "\"next_cursor\":\"cursor-2\"}");

            Assert.AreEqual("wx-grantee", addDocument.RootElement.GetProperty("books")[0].GetProperty("grantee_appid").GetString());
            Assert.AreEqual(2147483646L, addDocument.RootElement.GetProperty("books")[0].GetProperty("expire_time").GetInt64());
            Assert.AreEqual("b2", queryDocument.RootElement.GetProperty("book_ids")[1].GetString());
            Assert.IsFalse(queryDocument.RootElement.TryGetProperty("count", out _), "官方按作品查询示例未提交 count。");
            Assert.AreEqual("b1", result.book_results[0].book_id);
            Assert.AreEqual("cursor-2", result.next_cursor);
        }

        [TestMethod]
        public void PreviewSettingUsesNestedShapeFromOfficialExample()
        {
            var request = new NovelSetPreviewSettingRequest
            {
                setting = new NovelPreviewSetting
                {
                    book_id = "book-1",
                    default_words = 240,
                    chapter_setting = new[]
                    {
                        new NovelChapterPreviewSetting { chapter_index = 1, words = 123 }
                    }
                }
            };

            using var document = JsonDocument.Parse(Serialize(request));
            var result = JsonConvert.DeserializeObject<NovelGetPreviewSettingJsonResult>(
                "{\"errcode\":0,\"setting\":{\"book_id\":\"book-1\",\"default_words\":240,"
                + "\"chapter_setting\":[{\"chapter_index\":1,\"words\":123}]}}");

            Assert.AreEqual("book-1", document.RootElement.GetProperty("setting").GetProperty("book_id").GetString());
            Assert.AreEqual(123, document.RootElement.GetProperty("setting").GetProperty("chapter_setting")[0].GetProperty("words").GetInt32());
            Assert.IsFalse(document.RootElement.TryGetProperty("book_id", out _), "设置接口应遵循官方示例的 setting 外层。");
            Assert.AreEqual(240, result.setting.default_words);
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
    }
}
