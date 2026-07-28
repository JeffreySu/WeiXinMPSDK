using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BusinessCircle;
using Senparc.Weixin.TenPayV3.Apis.FundApp;
using Senparc.Weixin.TenPayV3.Apis.Security;
using Senparc.Weixin.TenPayV3.Attributes;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Senparc.Weixin.TenPayV3.Test.Apis
{
    [TestClass]
    public class OfficialMissingApiContractTests
    {
        [TestMethod]
        public void PublicSurfaceContainsFundAppBusinessCircleAndSecurityEntries()
        {
            AssertPublicMethods(typeof(FundAppApis),
                nameof(FundAppApis.PreTransferWithAuthorizationAsync),
                nameof(FundAppApis.CreateUserConfirmAuthorizationAsync),
                nameof(FundAppApis.QueryUserConfirmAuthorizationAsync),
                nameof(FundAppApis.TransferWithAuthorizationAsync),
                nameof(FundAppApis.CloseUserConfirmAuthorizationAsync));
            AssertPublicMethods(typeof(BusinessCircleApis),
                nameof(BusinessCircleApis.QueryPointsCommitStatusAsync),
                nameof(BusinessCircleApis.SyncParkingStateAsync));
            AssertPublicMethods(typeof(SecurityEchoApis),
                nameof(SecurityEchoApis.EchoAsync));
            AssertPublicMethods(typeof(FundAppNotifyHandlerExtensions),
                nameof(FundAppNotifyHandlerExtensions.DecryptFundAppTransferResultNotifyAsync),
                nameof(FundAppNotifyHandlerExtensions.DecryptFundAppAuthorizationResultNotifyAsync));
            AssertPublicMethods(typeof(SecurityEchoNotifyHandlerExtensions),
                nameof(SecurityEchoNotifyHandlerExtensions.DecryptSecurityEchoNotifyAsync));
        }

        [TestMethod]
        public void OfficialPathsAndSensitiveTransportRequirementsAreFixed()
        {
            AssertSourceContains("Apis/FundApp/FundAppApis.Authorization.cs",
                "pre-transfer-with-authorization",
                "user-confirm-authorization",
                "transfer-bills/transfer",
                "RequestWithoutBodyAsync<UserConfirmAuthorizationReturnJson>",
                "Wechatpay-Serial",
                "Uri.EscapeDataString");
            AssertSourceContains("Apis/BusinessCircle/BusinessCircleApis.cs",
                "points/commit_status", "v3/businesscircle/parkings",
                "Uri.EscapeDataString");
            AssertSourceContains("Apis/Security/SecurityEchoApis.cs",
                "v3/security/echo", "Wechatpay-Serial",
                "SecurityHelper.FieldEncrypt");
        }

        [TestMethod]
        public void ModelsExposeOfficialFieldsEventsAndEncryptionMarkers()
        {
            AssertFieldEncrypted(typeof(PreTransferWithAuthorizationRequestData),
                nameof(PreTransferWithAuthorizationRequestData.user_name));
            AssertFieldEncrypted(typeof(TransferWithAuthorizationRequestData),
                nameof(TransferWithAuthorizationRequestData.user_name));
            AssertFieldEncrypted(typeof(SecurityEchoRequestData),
                nameof(SecurityEchoRequestData.encrypted_echo_message));

            var parkingJson = JsonConvert.SerializeObject(
                new BusinessCircleParkingRequestData
                {
                    brandid = 1001,
                    appid = "wx-app",
                    openid = "openid",
                    plate_number = "苏A12345",
                    state = "IN",
                    time = "2026-07-28T12:00:00+08:00"
                });
            StringAssert.Contains(parkingJson, "\"plate_number\":\"苏A12345\"");
            StringAssert.Contains(parkingJson, "\"state\":\"IN\"");

            Assert.AreEqual("MCHTRANSFER.BILL.FINISHED",
                FundAppAuthorizationNotifyEventTypes.TransferBillFinished);
            Assert.AreEqual("MCHTRANSFER.AUTHORIZATION.CONFIRMED",
                FundAppAuthorizationNotifyEventTypes.AuthorizationConfirmed);
            Assert.AreEqual("MCHTRANSFER.AUTHORIZATION.CLOSED",
                FundAppAuthorizationNotifyEventTypes.AuthorizationClosed);
            Assert.AreEqual("mch_payment",
                FundAppAuthorizationNotifyEventTypes.TransferOriginalType);
            Assert.AreEqual("SECURITY_ECHO.SUCCESS",
                SecurityEchoNotifyEventTypes.Success);
            Assert.AreEqual("security", SecurityEchoNotifyEventTypes.OriginalType);
            Assert.IsNotNull(typeof(SecurityEchoNotifyJson).GetProperty(
                nameof(SecurityEchoNotifyJson.encrypt_echo_message)));
        }

        private static void AssertFieldEncrypted(Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName);
            Assert.IsNotNull(property, type.Name + "." + propertyName);
            Assert.IsTrue(property.GetCustomAttributes(typeof(FieldEncryptAttribute), true)
                .Any(), propertyName + " 必须标记 FieldEncryptAttribute。");
        }

        private static void AssertPublicMethods(Type type, params string[] names)
        {
            var methods = type.GetMethods(BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            foreach (var name in names)
            {
                CollectionAssert.Contains(methods, name, type.Name + "." + name);
            }
        }

        private static void AssertSourceContains(string relativePath,
            params string[] values)
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.TenPay", "Senparc.Weixin.TenPayV3",
                relativePath.Replace('/', Path.DirectorySeparatorChar)));
            foreach (var value in values)
            {
                Assert.IsTrue(source.Contains(value),
                    relativePath + " 缺少契约：" + value);
            }
        }

        private static string FindRepositoryRoot(
            [CallerFilePath] string sourceFilePath = null)
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
                        Directory.Exists(Path.Combine(directory.FullName, "src",
                            "Senparc.Weixin.TenPay")))
                    {
                        return directory.FullName;
                    }
                    directory = directory.Parent;
                }
            }

            throw new DirectoryNotFoundException(
                "无法定位 WeiXinMPSDK 仓库根目录。");
        }
    }
}
