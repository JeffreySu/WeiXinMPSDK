using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers.Serializers;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.AutoReply;
using Senparc.Weixin.TenPayV3.Apis.PayScore;
using Senparc.Weixin.Work.AdvancedAPIs.Media;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.WeixinTests.Helpers.Serializers
{
    [TestClass]
    public class WeixinJsonSerializerTests
    {
        [TestMethod]
        public void DeserializeWxJsonResult_UsesGeneratedMetadata()
        {
            var result = WeixinJsonSerializer.DeserializeWxJsonResult("{\"errcode\":0,\"errmsg\":\"ok\"}");

            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
            Assert.AreEqual("ok", result.errmsg);
        }

        [TestMethod]
        public void GetResult_UsesCallerProvidedGeneratedMetadata()
        {
            var result = Post.GetResult(
                "{\"value\":42}",
                WeixinJsonSerializerTestContext.Default.WeixinJsonSerializerTestPayload);

            Assert.AreEqual(42, result.Value);
        }

        [TestMethod]
        public void WorkMediaUploadResult_ReadsEnumStringAndPreservesNumericSerialization()
        {
            var result = Senparc.CO2NET.Helpers.SerializerHelper.GetObject<UploadTemporaryResultJson>(
                "{\"type\":\"image\",\"media_id\":\"media-id\",\"created_at\":123}");

            Assert.AreEqual(Senparc.Weixin.Work.UploadMediaFileType.image, result.type);
            using (var document = JsonDocument.Parse(JsonSerializer.Serialize(result)))
            {
                Assert.AreEqual(JsonValueKind.Number, document.RootElement.GetProperty("type").ValueKind);
                Assert.AreEqual(0, document.RootElement.GetProperty("type").GetInt32());
            }

            var numericResult = Senparc.CO2NET.Helpers.SerializerHelper.GetObject<UploadTemporaryResultJson>(
                "{\"type\":1,\"media_id\":\"media-id\",\"created_at\":123}");
            Assert.AreEqual(Senparc.Weixin.Work.UploadMediaFileType.voice, numericResult.type);

            var numericStringResult = Senparc.CO2NET.Helpers.SerializerHelper.GetObject<UploadTemporaryResultJson>(
                "{\"type\":\"2\",\"media_id\":\"media-id\",\"created_at\":123}");
            Assert.AreEqual(Senparc.Weixin.Work.UploadMediaFileType.video, numericStringResult.type);
        }

        [TestMethod]
        public void MpAutoReplyResult_ReadsStringEnumsAndPreservesNumericSerialization()
        {
            var result = Senparc.CO2NET.Helpers.SerializerHelper.GetObject<AddFriendAutoReplyInfo>(
                "{\"type\":\"text\",\"content\":\"hello\"}");

            Assert.AreEqual(Senparc.Weixin.MP.AutoReplyType.text, result.type);
            using (var document = JsonDocument.Parse(Senparc.CO2NET.Helpers.SerializerHelper.GetJsonString(result)))
            {
                Assert.AreEqual(JsonValueKind.Number, document.RootElement.GetProperty("type").ValueKind);
                Assert.AreEqual(0, document.RootElement.GetProperty("type").GetInt32());
            }
        }

        [TestMethod]
        public void EnumConverter_UsesEnumMemberAliasesAndWritesNumbers()
        {
            var result = Senparc.CO2NET.Helpers.SerializerHelper.GetObject<EnumMemberPayload>(
                "{\"value\":\"wire-value\"}");

            Assert.AreEqual(EnumMemberValue.Value, result.Value);
            using (var document = JsonDocument.Parse(Senparc.CO2NET.Helpers.SerializerHelper.GetJsonString(result)))
            {
                Assert.AreEqual(JsonValueKind.Number, document.RootElement.GetProperty("Value").ValueKind);
                Assert.AreEqual(7, document.RootElement.GetProperty("Value").GetInt32());
            }
        }

        [TestMethod]
        public void NewtonsoftIgnoreAttributes_AreHonoredBySystemTextJson()
        {
            var request = new AssignGuideRequestData("guide-id", "out-trade-no");

            using (var document = JsonDocument.Parse(JsonSerializer.Serialize(request)))
            {
                Assert.IsFalse(document.RootElement.TryGetProperty("guide_id", out _));
                Assert.AreEqual("out-trade-no", document.RootElement.GetProperty("out_trade_no").GetString());
            }
        }

        [TestMethod]
        public async Task SendAsync_UsesGeneratedMetadataAndInjectedHttpClient()
        {
            var handler = new JsonResponseHandler("{\"value\":7}");
            using var httpClient = new HttpClient(handler);

            var result = await CommonJsonSend.SendAsync(
                null,
                "https://unit.test/weixin",
                new WeixinJsonSerializerTestPayload { Value = 42 },
                WeixinJsonSerializerTestContext.Default.WeixinJsonSerializerTestPayload,
                WeixinJsonSerializerTestContext.Default.WeixinJsonSerializerTestPayload,
                contentType: "application/json; charset=utf-8",
                httpClient: httpClient);

            Assert.AreEqual(7, result.Value);
            Assert.AreEqual("{\"Value\":42}", handler.RequestBody);
            Assert.AreEqual("application/json", handler.RequestContentType.MediaType);
            Assert.AreEqual("utf-8", handler.RequestContentType.CharSet);
        }

        private sealed class JsonResponseHandler : HttpMessageHandler
        {
            private readonly string _responseJson;

            public JsonResponseHandler(string responseJson)
            {
                _responseJson = responseJson;
            }

            public string RequestBody { get; private set; }

            public MediaTypeHeaderValue RequestContentType { get; private set; }

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                RequestBody = request.Content == null
                    ? null
                    : await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                RequestContentType = request.Content?.Headers.ContentType;

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(_responseJson)
                };
            }
        }
    }

    public sealed class WeixinJsonSerializerTestPayload
    {
        public int Value { get; set; }
    }

    public sealed class EnumMemberPayload
    {
        [JsonConverter(typeof(JsonStringOrNumberEnumConverterFactory))]
        public EnumMemberValue Value { get; set; }
    }

    public enum EnumMemberValue
    {
        [EnumMember(Value = "wire-value")]
        Value = 7
    }

    [JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
    [JsonSerializable(typeof(WeixinJsonSerializerTestPayload))]
    internal partial class WeixinJsonSerializerTestContext : JsonSerializerContext
    {
    }
}
