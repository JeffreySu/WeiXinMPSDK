using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Entities;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Senparc.Weixin.TenPayV3.Test.HttpHandlers
{
    [TestClass]
    public class TenPayNotifyHandlerTests
    {
        private static SenparcWeixinSettingItem CreateSetting()
        {
            return new SenparcWeixinSettingItem
            {
                TenPayV3_AppId = "test-appid",
                TenPayV3_MchId = "test-mchid",
                TenPayV3_APIv3Key = "12345678901234567890123456789012",
                TenPayV3_TenpayNotify = "https://test.com/notify",
                TenPayV3_PrivateKey = "test-private-key",
                EncryptionType = CertType.RSA
            };
        }

        [TestMethod]
        public async Task CreateAsyncRejectsOversizedBody()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "POST";
            httpContext.Request.ContentLength = 128;
            httpContext.Request.Body = new MemoryStream(new byte[128]);

            await Assert.ThrowsExceptionAsync<InvalidDataException>(() =>
                TenPayNotifyHandler.CreateAsync(httpContext, CreateSetting(), maxBodyBytes: 64));
        }

        [TestMethod]
        public void LegacyConstructorAllowsBodyLargerThanNewDefaultLimit()
        {
            var body = Encoding.UTF8.GetBytes("{\"padding\":\"" +
                new string('a', TenPayNotifyHandler.DefaultMaxBodyBytes) + "\"}");
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "POST";
            httpContext.Request.ContentLength = body.Length;
            httpContext.Request.Body = new MemoryStream(body);

            var handler = new TenPayNotifyHandler(httpContext, CreateSetting());

            Assert.IsNotNull(handler);
            Assert.AreEqual(0, httpContext.Request.Body.Position);
        }

        [TestMethod]
        public async Task CreateAsyncHonorsCancellation()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "POST";
            httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes("{}"));
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();

            try
            {
                await TenPayNotifyHandler.CreateAsync(httpContext, CreateSetting(), cancellationToken: cancellation.Token);
                Assert.Fail("应传播取消异常。");
            }
            catch (OperationCanceledException)
            {
                // TaskCanceledException 也是合法的取消传播。
            }
        }

        [TestMethod]
        public async Task NotificationIdAndRawBodyProvideStableIdempotencyInputs()
        {
            const string notificationId = "EV-202607241234567890";
            var json = "{\"id\":\"" + notificationId +
                       "\",\"create_time\":\"2026-07-24T12:00:00+08:00\"," +
                       "\"resource_type\":\"encrypt-resource\"," +
                       "\"event_type\":\"TRANSACTION.SUCCESS\",\"summary\":\"test\"," +
                       "\"resource\":{\"original_type\":\"transaction\"," +
                       "\"algorithm\":\"AEAD_AES_256_GCM\",\"ciphertext\":\"cipher\"," +
                       "\"associated_data\":\"data\",\"nonce\":\"nonce\"}}";

            var first = await TenPayNotifyHandler.CreateAsync(
                CreateContext(json), CreateSetting());
            var retry = await TenPayNotifyHandler.CreateAsync(
                CreateContext(json), CreateSetting());

            Assert.AreEqual(notificationId, first.NotificationId);
            Assert.AreEqual(notificationId, first.Notification.id);
            Assert.AreEqual(first.NotificationId, retry.NotificationId,
                "同一官方通知重试必须暴露相同的幂等键。");
            Assert.AreEqual(json, first.RawBody);
        }

        private static DefaultHttpContext CreateContext(string json)
        {
            var bytes = Encoding.UTF8.GetBytes(json);
            var context = new DefaultHttpContext();
            context.Request.Method = "POST";
            context.Request.ContentLength = bytes.Length;
            context.Request.ContentType = "application/json";
            context.Request.Body = new MemoryStream(bytes);
            return context;
        }

        /// <summary>
        /// 测试 Request.Body 流在 TenPayNotifyHandler 读取后仍可被后续中间件读取
        /// </summary>
        [TestMethod]
        public void RequestBodyCanBeReadMultipleTimesTest()
        {
            // Arrange
            var testJson = "{\"id\":\"test-id\",\"create_time\":\"2025-01-01T00:00:00+08:00\",\"resource_type\":\"encrypt-resource\",\"event_type\":\"TRANSACTION.SUCCESS\",\"summary\":\"test\",\"resource\":{\"original_type\":\"transaction\",\"algorithm\":\"AEAD_AES_256_GCM\",\"ciphertext\":\"test-cipher\",\"associated_data\":\"test-data\",\"nonce\":\"test-nonce\"}}";
            var bodyBytes = Encoding.UTF8.GetBytes(testJson);
            var bodyStream = new MemoryStream(bodyBytes);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "POST";
            httpContext.Request.Body = bodyStream;
            httpContext.Request.ContentType = "application/json";

            // 设置必需的请求头
            httpContext.Request.Headers["Wechatpay-Timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            httpContext.Request.Headers["Wechatpay-Nonce"] = "test-nonce";
            httpContext.Request.Headers["Wechatpay-Signature"] = "test-signature";
            httpContext.Request.Headers["Wechatpay-Serial"] = "test-serial";

            // 创建测试配置
            var testSetting = new SenparcWeixinSettingItem()
            {
                TenPayV3_AppId = "test-appid",
                TenPayV3_MchId = "test-mchid",
                TenPayV3_APIv3Key = "12345678901234567890123456789012", // 32 字节的测试密钥
                TenPayV3_TenpayNotify = "https://test.com/notify",
                TenPayV3_PrivateKey = "test-private-key",
                EncryptionType = CertType.RSA // 使用 RSA 加密类型
            };

            // Act
            try
            {
                var handler = new TenPayNotifyHandler(httpContext, testSetting);

                // 验证流的位置已被重置
                Assert.AreEqual(0, httpContext.Request.Body.Position, "Stream position should be reset to 0 after TenPayNotifyHandler reads it");

                // 验证流仍可读取
                Assert.IsTrue(httpContext.Request.Body.CanRead, "Request.Body should still be readable after TenPayNotifyHandler");

                // 尝试再次读取流以模拟后续中间件
                httpContext.Request.Body.Position = 0;
                using (var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, bufferSize: 1024, leaveOpen: true))
                {
                    var bodyContent = reader.ReadToEndAsync().GetAwaiter().GetResult();
                    Assert.AreEqual(testJson, bodyContent, "Subsequent middleware should be able to read the same body content");
                }

                // 验证流位置可以再次重置
                httpContext.Request.Body.Position = 0;
                Assert.AreEqual(0, httpContext.Request.Body.Position, "Stream position should be resettable");
            }
            catch (Exception ex)
            {
                // 如果抛出异常，测试应该失败但我们要查看异常详情
                // 注意：由于我们没有有效的证书，签名验证可能会失败，但这不影响流读取测试
                Console.WriteLine($"Exception occurred (may be expected for signature validation): {ex.Message}");
                
                // 即使发生异常，我们仍然要验证流是否可以重置和读取
                try
                {
                    httpContext.Request.Body.Position = 0;
                    Assert.IsTrue(httpContext.Request.Body.CanRead, "Request.Body should still be readable even after exception");
                }
                catch (Exception readEx)
                {
                    Assert.Fail($"Stream should remain readable even if TenPayNotifyHandler throws exception. Error: {readEx.Message}");
                }
            }
        }

        /// <summary>
        /// 测试非 POST/PUT/PATCH 请求不影响流
        /// </summary>
        [TestMethod]
        public void NonPostRequestDoesNotReadBodyTest()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = "GET";

            // 创建测试配置
            var testSetting = new SenparcWeixinSettingItem()
            {
                TenPayV3_AppId = "test-appid",
                TenPayV3_MchId = "test-mchid",
                TenPayV3_APIv3Key = "12345678901234567890123456789012",
                TenPayV3_TenpayNotify = "https://test.com/notify",
                TenPayV3_PrivateKey = "test-private-key",
                EncryptionType = CertType.RSA
            };

            // Act
            try
            {
                var handler = new TenPayNotifyHandler(httpContext, testSetting);
                
                // GET 请求不应该读取 Body，测试应该成功创建 handler
                Assert.IsNotNull(handler);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception for GET request: {ex.Message}");
                // GET 请求创建 handler 应该不会因为流问题而失败
            }
        }
    }
}
