using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers.Serializers;
using Senparc.Weixin.HttpUtility;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

    [JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
    [JsonSerializable(typeof(WeixinJsonSerializerTestPayload))]
    internal partial class WeixinJsonSerializerTestContext : JsonSerializerContext
    {
    }
}
