using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.TenPay.V3;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using LegacyTenPayV3 = Senparc.Weixin.TenPay.V3.TenPayV3;

namespace Senparc.Weixin.TenPay.Test.vs2017.V3
{
    [TestClass]
    public class EnterprisePaymentContractTests
    {
        private static readonly string[] EnterprisePaymentPaths =
        {
            "/mmpaymkttransfers/sendworkwxredpack",
            "/mmpaymkttransfers/queryworkwxredpack",
            "/mmpaymkttransfers/promotion/paywwsptrans2pocket",
            "/mmpaymkttransfers/promotion/querywwsptrans2pocket"
        };

        [TestMethod]
        public void ExistingEnterprisePaymentPathsRemainMappedAcrossTenPayModules()
        {
            Assert.AreEqual(4, EnterprisePaymentPaths.Length);
            Assert.AreEqual(EnterprisePaymentPaths.Length, EnterprisePaymentPaths.Distinct().Count());

            var project = Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.TenPay",
                "Senparc.Weixin.TenPay", "V3", "Universal");
            var source = File.ReadAllText(Path.Combine(project, "TenPayV3.cs")) + "\n" +
                         File.ReadAllText(Path.Combine(project, "RedPackApi", "WorkRedPackApi.cs"));

            foreach (var path in EnterprisePaymentPaths)
            {
                StringAssert.Contains(source, path, path);
            }
        }

        [TestMethod]
        public void EmployeePaymentExposesCompatibleSyncAndAsyncPairs()
        {
            var methods = typeof(LegacyTenPayV3).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            CollectionAssert.Contains(methods, nameof(LegacyTenPayV3.PayToWorker));
            CollectionAssert.Contains(methods, nameof(LegacyTenPayV3.PayToWorkerAsync));
            CollectionAssert.Contains(methods, nameof(LegacyTenPayV3.QueryPayLog));
            CollectionAssert.Contains(methods, nameof(LegacyTenPayV3.QueryPayLogAsync));

            var payMethod = typeof(LegacyTenPayV3).GetMethod(nameof(LegacyTenPayV3.PayToWorkerAsync),
                new[] { typeof(IServiceProvider), typeof(TenPayV3PayToWorkerRequestData), typeof(int) });
            var queryMethod = typeof(LegacyTenPayV3).GetMethod(nameof(LegacyTenPayV3.QueryPayLogAsync),
                new[] { typeof(IServiceProvider), typeof(TenPayV3GetTransferInfoRequestData), typeof(int) });

            Assert.IsNotNull(payMethod);
            Assert.IsNotNull(queryMethod);
            Assert.AreEqual(typeof(Task<TransfersResult>), payMethod.ReturnType);
            Assert.AreEqual(typeof(Task<GetTransferInfoResult>), queryMethod.ReturnType);
        }

        [TestMethod]
        public void EnterpriseRedPackExposesTrueAsyncPairsAndRequestMetadata()
        {
            var methods = typeof(WorkRedPackApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            CollectionAssert.Contains(methods, nameof(WorkRedPackApi.SendWorkRedPack));
            CollectionAssert.Contains(methods, nameof(WorkRedPackApi.SendWorkRedPackAsync));
            CollectionAssert.Contains(methods, nameof(WorkRedPackApi.SearchRedPack));
            CollectionAssert.Contains(methods, nameof(WorkRedPackApi.SearchRedPackAsync));

            var sendMethod = typeof(WorkRedPackApi).GetMethod(nameof(WorkRedPackApi.SendWorkRedPackAsync),
                new[]
                {
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(int), typeof(string), typeof(string), typeof(string), typeof(int),
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(CancellationToken)
                });
            var searchMethod = typeof(WorkRedPackApi).GetMethod(nameof(WorkRedPackApi.SearchRedPackAsync),
                new[]
                {
                    typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
                    typeof(CancellationToken)
                });

            Assert.IsNotNull(sendMethod);
            Assert.IsNotNull(searchMethod);
            Assert.AreEqual(typeof(Task<SendWorkRedPackResult>), sendMethod.ReturnType);
            Assert.AreEqual(typeof(Task<SearchRedPackResult>), searchMethod.ReturnType);

            var result = new SendWorkRedPackResult(new NormalRedPackResult(), "nonce", "pay", "work", "bill");
            Assert.AreEqual("nonce", result.NonceStr);
            Assert.AreEqual("pay", result.PaySign);
            Assert.AreEqual("work", result.WorkpaySign);
            Assert.AreEqual("bill", result.MchBillNo);
        }

        [TestMethod]
        public void SharedEnterpriseRedPackParsersPreserveSuccessAndFailureFields()
        {
            var sendParser = typeof(WorkRedPackApi).GetMethod("ParseSendWorkRedPackResult",
                BindingFlags.NonPublic | BindingFlags.Static);
            var searchParser = typeof(WorkRedPackApi).GetMethod("ParseSearchWorkRedPackResult",
                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(sendParser);
            Assert.IsNotNull(searchParser);

            var sendDocument = new XmlDocument();
            sendDocument.LoadXml("<xml><return_code>SUCCESS</return_code><result_code>FAIL</result_code>" +
                                 "<mch_billno>bill-1</mch_billno><mch_id>mch-1</mch_id>" +
                                 "<err_code>FAILED</err_code><err_code_des>failed</err_code_des>" +
                                 "<send_listid>list-1</send_listid></xml>");
            var sendResult = (NormalRedPackResult)sendParser.Invoke(null, new object[] { sendDocument });

            Assert.AreEqual("bill-1", sendResult.mch_billno);
            Assert.AreEqual("FAILED", sendResult.err_code);
            Assert.AreEqual("list-1", sendResult.send_listid);

            var searchDocument = new XmlDocument();
            searchDocument.LoadXml("<xml><return_code>SUCCESS</return_code><result_code>SUCCESS</result_code>" +
                                   "<mch_billno>bill-2</mch_billno><status>RECEIVED</status>" +
                                   "<total_amount>5178368698</total_amount><act_name>activity</act_name></xml>");
            var searchResult = (SearchRedPackResult)searchParser.Invoke(null, new object[] { searchDocument });

            Assert.IsTrue(searchResult.return_code);
            Assert.IsTrue(searchResult.result_code);
            Assert.AreEqual("bill-2", searchResult.mch_billno);
            Assert.AreEqual("5178368698", searchResult.total_amount);
            Assert.AreEqual("activity", searchResult.act_name);
        }

        [TestMethod]
        public void SharedEnterpriseRedPackParsersHandleIncompleteErrorResponses()
        {
            var sendParser = typeof(WorkRedPackApi).GetMethod("ParseSendWorkRedPackResult",
                BindingFlags.NonPublic | BindingFlags.Static);
            var searchParser = typeof(WorkRedPackApi).GetMethod("ParseSearchWorkRedPackResult",
                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(sendParser);
            Assert.IsNotNull(searchParser);

            var missingReturnCode = new XmlDocument();
            missingReturnCode.LoadXml("<xml><return_msg>transport failed</return_msg></xml>");
            var sendResult = (NormalRedPackResult)sendParser.Invoke(null, new object[] { missingReturnCode });
            Assert.IsNull(sendResult.return_code);
            Assert.AreEqual("transport failed", sendResult.return_msg);

            var missingResultCode = new XmlDocument();
            missingResultCode.LoadXml("<xml><return_code>SUCCESS</return_code>" +
                                      "<err_code>INCOMPLETE</err_code><err_code_des>missing result</err_code_des></xml>");
            var incompleteSendResult =
                (NormalRedPackResult)sendParser.Invoke(null, new object[] { missingResultCode });
            var searchResult = (SearchRedPackResult)searchParser.Invoke(null, new object[] { missingResultCode });

            Assert.AreEqual("INCOMPLETE", incompleteSendResult.err_code);
            Assert.AreEqual("missing result", incompleteSendResult.err_code_des);
            Assert.IsTrue(searchResult.return_code);
            Assert.IsFalse(searchResult.result_code);
            Assert.AreEqual("INCOMPLETE", searchResult.err_code);
        }

        [TestMethod]
        public void EnterprisePaymentCertificateAndStreamResourcesAreDeterministicallyDisposed()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.TenPay",
                "Senparc.Weixin.TenPay", "V3", "Universal", "TenPayV3.cs"));

            StringAssert.Contains(source,
                "using (X509Certificate2 cer = new X509Certificate2(cert, certPassword, storageFlags))");
            StringAssert.Contains(source,
                "await CertPostAsync(cert, certPassword, data, urlFormat, timeOut).ConfigureAwait(false)");
            StringAssert.Contains(source, "using (MemoryStream ms = new MemoryStream())");

            var redPackProject = Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.TenPay",
                "Senparc.Weixin.TenPay", "V3", "Universal", "RedPackApi");
            var asyncSource = File.ReadAllText(Path.Combine(redPackProject, "WorkRedPackApi.Async.cs"));
            var httpSource = File.ReadAllText(Path.Combine(redPackProject, "RedPackHttpUtility.cs"));
            StringAssert.Contains(asyncSource, "RedPackHttpUtility.PostXmlAsync");
            StringAssert.Contains(asyncSource, "CancellationToken cancellationToken");
            StringAssert.Contains(httpSource, "client.PostAsync(url, content, cancellationToken)");
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
