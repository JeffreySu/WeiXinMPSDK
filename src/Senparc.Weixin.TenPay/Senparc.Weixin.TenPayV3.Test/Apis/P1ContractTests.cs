using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Apis.BasePay.Entities;
using Senparc.Weixin.TenPayV3.Apis.Complaint;
using Senparc.Weixin.TenPayV3.Apis.VehicleParking;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.Apis
{
    [TestClass]
    public class P1ContractTests
    {
        [TestMethod]
        public void P1ApiSurfaceContainsOfficialEntriesAndCompatibleOverloads()
        {
            AssertPublicMethods(typeof(BasePayApis),
                nameof(BasePayApis.ApplyAbnormalRefundAsync),
                nameof(BasePayApis.ApplyCombineAbnormalRefundAsync));
            AssertPublicMethods(typeof(ComplaintApis),
                nameof(ComplaintApis.UpdateRefundProgressAsync),
                nameof(ComplaintApis.ResponseImmediateServiceAsync),
                nameof(ComplaintApis.UploadImageAsync),
                nameof(ComplaintApis.DownloadImageAsync));
            AssertPublicMethods(typeof(VehicleParkingApis),
                nameof(VehicleParkingApis.CreateRepaymentData),
                nameof(VehicleParkingApis.CreateRepaymentPath));
            AssertPublicMethods(typeof(TenPayNotifyHandlerExtensions),
                nameof(TenPayNotifyHandlerExtensions.DecryptRefundNotifyAsync),
                nameof(TenPayNotifyHandlerExtensions.DecryptComplaintNotifyAsync),
                nameof(TenPayNotifyHandlerExtensions.DecryptParkingStateNotifyAsync),
                nameof(TenPayNotifyHandlerExtensions.DecryptParkingPayNotifyAsync),
                nameof(TenPayNotifyHandlerExtensions.DecryptParkingRefundNotifyAsync),
                nameof(TenPayNotifyHandlerExtensions.DecryptMedicalInsurancePayNotifyAsync));

            var requestMethods = typeof(TenPayApiRequest)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance);
            Assert.AreEqual(2, requestMethods.Count(method =>
                method.Name == nameof(TenPayApiRequest.RequestMultipartAsync)));
            Assert.AreEqual(2, requestMethods.Count(method =>
                method.Name == nameof(TenPayApiRequest.RequestWithoutBodyAsync)));
            Assert.IsTrue(requestMethods.Any(method =>
                method.Name == nameof(TenPayApiRequest.RequestAsync) &&
                method.GetParameters().Length == 6),
                "原有 RequestAsync 重载必须继续保留。");
        }

        [TestMethod]
        public void P1ImplementationsContainOfficialPathsAndTransportGuards()
        {
            AssertSourceContains("Apis/BasePay/BasePayApis.AbnormalRefund.cs",
                "v3/refund/domestic/refunds/", "/apply-abnormal-refund",
                "Uri.EscapeDataString(refundId");
            AssertSourceContains("Apis/Complaint/ComplaintApis.P1.cs",
                "v3/merchant-service/complaints-v2/", "/update-refund-progress",
                "/response-immediate-service", "v3/merchant-service/images/upload",
                "Uri.EscapeDataString(complaintId", "Uri.EscapeDataString(mediaId");
            AssertSourceContains("Apis/Complaint/ComplaintApis.cs",
                "RequestWithoutBodyAsync<ReturnJsonBase>", "ApiRequestMethod.DELETE");
            AssertSourceContains("HttpHandlers/TenPayApiRequest.cs",
                "resultCode.Additional = content;", "RequestMultipartAsync<T>",
                "RequestWithoutBodyAsync<T>");
            AssertSourceContains("TenPayV3Info.cs",
                "PublicKeyCacheDuration", "RefreshPublicKeysAfterMissAsync",
                "SemaphoreSlim", "TryGetValue(serialNumber");

            var requestSource = ReadSource("HttpHandlers/TenPayApiRequest.cs");
            var coreSource = requestSource.Substring(
                requestSource.IndexOf("private async Task<T> RequestAsyncCore", StringComparison.Ordinal));
            var checkSignIndex = coreSource.IndexOf("if (checkSign)", StringComparison.Ordinal);
            var signatureHeaderIndex = coreSource.IndexOf(
                "GetValues(\"Wechatpay-Timestamp\")", StringComparison.Ordinal);
            Assert.IsTrue(checkSignIndex >= 0 && signatureHeaderIndex > checkSignIndex,
                "关闭响应签名校验时不应读取 Wechatpay 签名响应头。");
        }

        [TestMethod]
        public void P1ModelsPreserveOfficialFieldsAndRepaymentEncoding()
        {
            var abnormalRefund = new AbnormalRefundRequestData
            {
                sub_mchid = "1900000109",
                out_refund_no = "refund-1",
                type = "BANKCARD",
                bank_type = "ICBC_DEBIT",
                bank_account = "encrypted-account",
                real_name = "encrypted-name"
            };
            var immediateService = new ImmediateServiceRequestData
            {
                complainted_mchid = "1900000109",
                idempotent_id = "stable-request-id",
                message = new ImmediateServiceMessage { sender_identity = "MERCHANT" }
            };

            var abnormalJson = JsonConvert.SerializeObject(abnormalRefund);
            var serviceJson = JsonConvert.SerializeObject(immediateService);
            StringAssert.Contains(abnormalJson, "\"out_refund_no\":\"refund-1\"");
            StringAssert.Contains(abnormalJson, "\"bank_account\":\"encrypted-account\"");
            StringAssert.Contains(serviceJson, "\"idempotent_id\":\"stable-request-id\"");
            Assert.IsTrue(typeof(RefundNotifyJson).IsAssignableFrom(typeof(ParkingRefundNotifyJson)));

            var repayment = VehicleParkingApis.CreateRepaymentData(
                "wx app", "mch+1", "open/id", "fixed nonce");
            var path = VehicleParkingApis.CreateRepaymentPath(repayment);
            Assert.AreEqual(
                "pages/invest_list/invest_list?mchid=mch%2B1&appid=wx%20app&nonce_str=fixed%20nonce&openid=open%2Fid",
                path);
            Assert.AreEqual("wx5e73c65404eee268", VehicleParkingApis.RepaymentMiniProgramAppId);
        }

        [TestMethod]
        public async Task FreshPublicKeyCacheReturnsKnownSerialWithoutNetworkRefresh()
        {
            var setting = CreateSetting();
            var info = new TenPayV3Info(setting);
            var keys = new PublicKeyCollection { ["serial-1"] = "public-key-1" };

            SetPrivateField(info, "_publicKeys", keys);
            SetPrivateField(info, "_publicKeysExpiresAtUtcTicks",
                DateTimeOffset.UtcNow.AddMinutes(30).UtcDateTime.Ticks);

            var publicKey = await info.GetPublicKeyAsync("serial-1", setting);
            Assert.AreEqual("public-key-1", publicKey);
        }

        private static SenparcWeixinSettingItem CreateSetting()
        {
            return new SenparcWeixinSettingItem
            {
                TenPayV3_AppId = "test-appid",
                TenPayV3_MchId = "test-mchid",
                TenPayV3_APIv3Key = "12345678901234567890123456789012",
                TenPayV3_PrivateKey = "test-private-key",
                EncryptionType = CertType.RSA
            };
        }

        private static void SetPrivateField(object instance, string fieldName, object value)
        {
            var field = instance.GetType().GetField(fieldName,
                BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(field, fieldName);
            field.SetValue(instance, value);
        }

        private static void AssertPublicMethods(Type type, params string[] names)
        {
            var methods = type.GetMethods(BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.Static).Select(method => method.Name).ToArray();
            foreach (var name in names)
            {
                CollectionAssert.Contains(methods, name, $"{type.Name}.{name}");
            }
        }

        private static void AssertSourceContains(string relativePath, params string[] values)
        {
            var source = ReadSource(relativePath);
            foreach (var value in values)
            {
                Assert.IsTrue(source.Contains(value), $"{relativePath} 缺少契约：{value}");
            }
        }

        private static string ReadSource(string relativePath)
        {
            var sourceRoot = Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.TenPay", "Senparc.Weixin.TenPayV3");
            return File.ReadAllText(Path.Combine(sourceRoot,
                relativePath.Replace('/', Path.DirectorySeparatorChar)));
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
                        Directory.Exists(Path.Combine(directory.FullName, "src", "Senparc.Weixin.TenPay")))
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
