using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.TenPayV3.Apis.FundApp;
using Senparc.Weixin.TenPayV3.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.Apis
{
    [TestClass]
    public class FundAppTransferTests
    {
        [TestMethod]
        public async Task TransferBillRejectsMissingPaymentPublicKeyBeforeSending()
        {
            var setting = new SenparcWeixinSettingItem
            {
                TenPayV3_TenPayPubKeyID = "PUB_KEY_ID_test"
            };
            var api = new FundAppApis(setting);

            var exception = await Assert.ThrowsExceptionAsync<TenpayApiRequestException>(
                () => api.TransferBillAsync(new TransferBillRequestData { out_bill_no = "bill1" }));
            StringAssert.Contains(exception.Message, "公钥 ID");
        }

        [TestMethod]
        public async Task TransferBillUsesConfiguredPublicKeyAndVerifiesResponse()
        {
            using var responseKey = RSA.Create(2048);
            var serial = "PUB_KEY_ID_configured";
            var setting = CreateSetting(responseKey, true, serial);
            var cleanup = CachePublicKey(setting, serial, responseKey, true);

            try
            {
                var (result, requestSerial) = await SendTransferAsync(
                    setting, serial, responseKey);

                Assert.AreEqual(serial, requestSerial);
                Assert.IsTrue(result.VerifySignSuccess == true);
            }
            finally
            {
                cleanup();
            }
        }

        [TestMethod]
        public async Task TransferBillUsesPlatformCertificateFallbackAndVerifiesResponse()
        {
            using var responseKey = RSA.Create(2048);
            var serial = "PLATFORM_CERT_SERIAL";
            var setting = CreateSetting(responseKey, false, null);
            var cleanup = CachePublicKey(setting, serial, responseKey, false);

            try
            {
                var (result, requestSerial) = await SendTransferAsync(
                    setting, serial, responseKey);

                Assert.AreEqual(serial, requestSerial);
                Assert.IsTrue(result.VerifySignSuccess == true);
            }
            finally
            {
                cleanup();
            }
        }

        [TestMethod]
        public async Task TransferBillDoesNotVerifyUnknownResponseSerial()
        {
            using var responseKey = RSA.Create(2048);
            var serial = "PUB_KEY_ID_known";
            var setting = CreateSetting(responseKey, true, serial);
            var cleanup = CachePublicKey(setting, serial, responseKey, true);

            try
            {
                var (result, requestSerial) = await SendTransferAsync(
                    setting, "PUB_KEY_ID_unknown", responseKey, true);

                Assert.AreEqual(serial, requestSerial);
                Assert.AreNotEqual(true, result.VerifySignSuccess);
            }
            finally
            {
                cleanup();
            }
        }

        [TestMethod]
        public async Task TransferBillDoesNotVerifyInvalidResponseSignature()
        {
            using var responseKey = RSA.Create(2048);
            using var invalidSigner = RSA.Create(2048);
            var serial = "PUB_KEY_ID_signature";
            var setting = CreateSetting(responseKey, true, serial);
            var cleanup = CachePublicKey(setting, serial, responseKey, true);

            try
            {
                var (result, requestSerial) = await SendTransferAsync(
                    setting, serial, invalidSigner);

                Assert.AreEqual(serial, requestSerial);
                Assert.IsFalse(result.VerifySignSuccess == true);
            }
            finally
            {
                cleanup();
            }
        }

        [TestMethod]
        public void CancelTransferUsesBodylessRequest()
        {
            var method = typeof(FundAppApis).GetMethod(nameof(FundAppApis.CancelTransferAsync));
            var stateMachine = method.GetCustomAttribute<AsyncStateMachineAttribute>().StateMachineType;
            var moveNext = stateMachine.GetMethod("MoveNext", BindingFlags.Instance | BindingFlags.NonPublic);
            var bytes = moveNext.GetMethodBody().GetILAsByteArray();
            var callsBodylessRequest = false;

            for (var index = 0; index <= bytes.Length - 5; index++)
            {
                if (bytes[index] != 0x28 && bytes[index] != 0x6F)
                    continue;

                try
                {
                    var called = moveNext.Module.ResolveMethod(BitConverter.ToInt32(bytes, index + 1));
                    if (called.Name == "RequestWithoutBodyAsync")
                        callsBodylessRequest = true;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is BadImageFormatException)
                {
                }
            }

            Assert.IsTrue(callsBodylessRequest);
        }

        private static SenparcWeixinSettingItem CreateSetting(
            RSA paymentKey, bool useConfiguredPublicKey, string serial)
        {
            var setting = new SenparcWeixinSettingItem
            {
                EncryptionType = CertType.RSA,
                TenPayV3_MchId = "test-" + Guid.NewGuid().ToString("N"),
                TenPayV3_PrivateKey = Convert.ToBase64String(
                    paymentKey.ExportPkcs8PrivateKey()),
                TenPayV3_SerialNumber = "MERCHANT_CERT_SERIAL",
                TenPayV3_TenPayPubKeyEnable = useConfiguredPublicKey
            };

            if (useConfiguredPublicKey)
            {
                setting.TenPayV3_TenPayPubKeyID = serial;
                setting.TenPayV3_TenPayPubKey =
                    paymentKey.ExportSubjectPublicKeyInfoPem();
            }

            return setting;
        }

        private static Action CachePublicKey(
            SenparcWeixinSettingItem setting, string serial, RSA publicKey,
            bool isPaymentPublicKey)
        {
            var key = TenPayHelper.GetRegisterKey(
                setting.TenPayV3_MchId, setting.TenPayV3_SubMchId);
            var collection = TenPayV3InfoCollection.Data;
            var hadPreviousValue = collection.TryGetValue(key, out var previousValue);
            var info = new TenPayV3Info(setting);
            var keyValue = isPaymentPublicKey
                ? Convert.ToBase64String(publicKey.ExportSubjectPublicKeyInfo())
                : CreatePlatformCertificate(publicKey);
            var publicKeys = new PublicKeyCollection
            {
                [serial] = keyValue
            };

            typeof(TenPayV3Info)
                .GetField("_publicKeys", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(info, publicKeys);
            typeof(TenPayV3Info)
                .GetField("_publicKeysExpiresAtUtcTicks", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(info, DateTime.UtcNow.AddHours(1).Ticks);
            collection[key] = info;

            return () =>
            {
                if (hadPreviousValue)
                {
                    collection[key] = previousValue;
                }
                else
                {
                    collection.Remove(key);
                }
            };
        }

        private static string CreatePlatformCertificate(RSA publicKey)
        {
            var request = new CertificateRequest(
                "CN=TenPay test platform certificate", publicKey,
                HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            using var certificate = request.CreateSelfSigned(
                DateTimeOffset.UtcNow.AddDays(-1), DateTimeOffset.UtcNow.AddDays(1));
            return Convert.ToBase64String(certificate.RawData);
        }

        private static async Task<(TransferBillReturnJson result, string requestSerial)>
            SendTransferAsync(SenparcWeixinSettingItem setting, string responseSerial,
                RSA responseSigner, bool expectCertificateFetch = false)
        {
            using var portListener = new TcpListener(IPAddress.Loopback, 0);
            portListener.Start();
            var port = ((IPEndPoint)portListener.LocalEndpoint).Port;
            portListener.Stop();

            using var listener = new HttpListener();
            var host = $"http://127.0.0.1:{port}";
            listener.Prefixes.Add(host + "/");
            listener.Start();

            var previousHost = Senparc.Weixin.Config.TenPayV3Host;
            Senparc.Weixin.Config.TenPayV3Host = host;
            var requestSerial = (string)null;
            var responseContent = "{\"out_bill_no\":\"bill1\",\"state\":\"ACCEPTED\"}";
            var serverTask = Task.Run(async () =>
            {
                var requestCount = expectCertificateFetch ? 2 : 1;
                for (var index = 0; index < requestCount; index++)
                {
                    var context = await listener.GetContextAsync();
                    if (context.Request.Url.AbsolutePath.EndsWith(
                        "/v3/certificates", StringComparison.Ordinal))
                    {
                        await WriteResponseAsync(context, "{\"data\":[]}");
                        continue;
                    }

                    requestSerial = context.Request.Headers["Wechatpay-Serial"];
                    var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
                    const string nonce = "response-nonce";
                    var signature = TenPaySignHelper.CreateSign(CertType.RSA,
                        $"{timestamp}\n{nonce}\n{responseContent}\n",
                        Convert.ToBase64String(responseSigner.ExportPkcs8PrivateKey()));

                    context.Response.Headers["Wechatpay-Timestamp"] = timestamp;
                    context.Response.Headers["Wechatpay-Nonce"] = nonce;
                    context.Response.Headers["Wechatpay-Signature"] = signature;
                    context.Response.Headers["Wechatpay-Serial"] = responseSerial;
                    await WriteResponseAsync(context, responseContent);
                }
            });

            try
            {
                var result = await new FundAppApis(setting).TransferBillAsync(
                    new TransferBillRequestData { out_bill_no = "bill1" });
                await serverTask;
                return (result, requestSerial);
            }
            finally
            {
                Senparc.Weixin.Config.TenPayV3Host = previousHost;
                listener.Stop();
            }
        }

        private static async Task WriteResponseAsync(
            HttpListenerContext context, string content)
        {
            var bytes = Encoding.UTF8.GetBytes(content);
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "application/json";
            context.Response.ContentLength64 = bytes.Length;
            await context.Response.OutputStream.WriteAsync(bytes, 0, bytes.Length);
            context.Response.Close();
        }

    }
}
