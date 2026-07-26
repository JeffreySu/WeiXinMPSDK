/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：Program.cs
    文件功能描述：微信 SDK Native AOT 冒烟验证程序入口


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v1.0.0 新增核心序列化、HTTP 与微信支付模型 AOT 运行验证

----------------------------------------------------------------*/

using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Helpers.Serializers;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.TenPay.V3;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Senparc.Weixin.NativeAotSmoke;

internal static class Program
{
    private static async Task Main()
    {
        var payload = new SmokePayload { Value = 42 };
        var json = WeixinJsonSerializer.Serialize(payload, SmokeJsonContext.Default.SmokePayload);
        Ensure(json.Contains("42", StringComparison.Ordinal), "core serialize");

        var parsed = Post.GetResult("{\"value\":7}", SmokeJsonContext.Default.SmokePayload);
        Ensure(parsed.Value == 7, "core deserialize");

        using (var client = new HttpClient(new StaticJsonHandler("{\"value\":9}")))
        {
            var response = await CommonJsonSend.SendAsync(
                null,
                "https://unit.test/aot",
                payload,
                SmokeJsonContext.Default.SmokePayload,
                SmokeJsonContext.Default.SmokePayload,
                httpClient: client);
            Ensure(response.Value == 9, "CommonJsonSend");
        }

        var receiver = new TenpayV3ProfitShareingRequestData_ReceiverInfo
        {
            receiveType = TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type.PERSONAL_OPENID,
            account = "openid-aot",
            amount = 100,
            description = "aot"
        };
        var request = new TenpayV3ProtfitSharingRequestData(
            "app-id", "mch-id", null, null, "test-key", "nonce",
            "transaction-id", "order-no", new[] { receiver });
        var receiversJson = (string)request.PackageRequestHandler.GetAllParameters()["receivers"];
        Ensure(receiversJson.Contains("PERSONAL_OPENID", StringComparison.Ordinal), "TenPay receivers");

        var scene = new TenPayV3UnifiedorderRequestData_SceneInfo(
            true,
            new H5_Info_Android
            {
                type = "Android",
                app_name = "Senparc",
                package_name = "com.senparc.aot"
            });
        using var sceneDocument = JsonDocument.Parse(scene.ToString());
        Ensure(
            sceneDocument.RootElement.GetProperty("h5_info").GetProperty("package_name").GetString() == "com.senparc.aot",
            "TenPay H5 scene");

        Console.WriteLine("AOT_SMOKE_OK");
    }

    private static void Ensure(bool condition, string operation)
    {
        if (!condition)
        {
            throw new InvalidOperationException($"AOT smoke failed: {operation}");
        }
    }

    private sealed class StaticJsonHandler : HttpMessageHandler
    {
        private readonly string _json;

        public StaticJsonHandler(string json) => _json = json;

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(_json)
            });
        }
    }
}

internal sealed class SmokePayload
{
    public int Value { get; set; }
}

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
[JsonSerializable(typeof(SmokePayload))]
internal partial class SmokeJsonContext : JsonSerializerContext
{
}
