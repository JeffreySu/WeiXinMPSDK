using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.TenPay.V3;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using LegacyTenPayV3 = Senparc.Weixin.TenPay.V3.TenPayV3;

namespace Senparc.Weixin.TenPay.Test.V3
{
    [TestClass]
    public class CustomsContractTests
    {
        [TestMethod]
        public void CustomsRequestCreatesSignedXmlAndOmitsNullOptionals()
        {
            var request = new CustomsDeclareOrderRequestData
            {
                appid = "wx-app",
                mch_id = "1900000109",
                transaction_id = "4200000001",
                customs = "GUANGZHOU_ZS",
                mch_customs_no = "customs-1",
                order_fee = 100,
                product_fee = 80,
                transport_fee = 20
            };

            var xml = XDocument.Parse(request.ToXml(
                "12345678901234567890123456789012"));
            var root = xml.Element("xml");
            Assert.IsNotNull(root);
            Assert.AreEqual("wx-app", root.Element("appid")?.Value);
            Assert.AreEqual("4200000001", root.Element("transaction_id")?.Value);
            Assert.AreEqual("100", root.Element("order_fee")?.Value);
            Assert.IsFalse(string.IsNullOrWhiteSpace(root.Element("sign")?.Value));
            Assert.IsNull(root.Element("action_type"));
            Assert.IsNull(root.Element("cert_id"));
            Assert.IsNull(root.Element("key"));
        }

        [TestMethod]
        public void CustomsQueryRequiresAtLeastOneOrderIdentifier()
        {
            var request = new CustomsDeclareQueryRequestData
            {
                appid = "wx-app",
                mch_id = "1900000109",
                customs = "GUANGZHOU_ZS"
            };

            Assert.ThrowsException<ArgumentException>(() =>
                request.ToXml("12345678901234567890123456789012"));
        }

        [TestMethod]
        public void CustomsQueryResultParsesOneBasedIndexedSubOrders()
        {
            const string xml = "<xml>" +
                "<return_code>SUCCESS</return_code>" +
                "<result_code>SUCCESS</result_code>" +
                "<transaction_id>4200000001</transaction_id>" +
                "<count>2</count>" +
                "<sub_order_no_1>sub-1</sub_order_no_1>" +
                "<sub_order_id_1>wx-sub-1</sub_order_id_1>" +
                "<mch_customs_no_1>customs-1</mch_customs_no_1>" +
                "<customs_1>GUANGZHOU_ZS</customs_1>" +
                "<order_fee_1>100</order_fee_1>" +
                "<state_1>SUCCESS</state_1>" +
                "<sub_order_no_2>sub-2</sub_order_no_2>" +
                "<sub_order_id_2>wx-sub-2</sub_order_id_2>" +
                "<mch_customs_no_2>customs-2</mch_customs_no_2>" +
                "<customs_2>SHANGHAI</customs_2>" +
                "<order_fee_2>200</order_fee_2>" +
                "<state_2>PROCESSING</state_2>" +
                "</xml>";

            var result = new CustomsDeclareQueryResult(xml);
            Assert.AreEqual(2, result.count);
            Assert.AreEqual(2, result.sub_orders.Count);
            Assert.AreEqual("sub-1", result.sub_orders[0].sub_order_no);
            Assert.AreEqual(100, result.sub_orders[0].order_fee);
            Assert.AreEqual("sub-2", result.sub_orders[1].sub_order_no);
            Assert.AreEqual("PROCESSING", result.sub_orders[1].state);
        }

        [TestMethod]
        public void CustomsQueryResultAlsoParsesZeroBasedIndexedSubOrders()
        {
            const string xml = "<xml>" +
                "<return_code>SUCCESS</return_code>" +
                "<result_code>SUCCESS</result_code>" +
                "<count>1</count>" +
                "<sub_order_no_0>sub-zero</sub_order_no_0>" +
                "<order_fee_0>300</order_fee_0>" +
                "<state_0>SUCCESS</state_0>" +
                "</xml>";

            var result = new CustomsDeclareQueryResult(xml);
            Assert.AreEqual(1, result.sub_orders.Count);
            Assert.AreEqual("sub-zero", result.sub_orders[0].sub_order_no);
            Assert.AreEqual(300, result.sub_orders[0].order_fee);
        }

        [TestMethod]
        public void CustomsPublicSurfaceContainsSyncAndAsyncEntries()
        {
            var methods = typeof(LegacyTenPayV3)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            CollectionAssert.Contains(methods, nameof(LegacyTenPayV3.CustomsDeclareOrder));
            CollectionAssert.Contains(methods, nameof(LegacyTenPayV3.CustomsDeclareOrderAsync));
            CollectionAssert.Contains(methods, nameof(LegacyTenPayV3.CustomsDeclareQuery));
            CollectionAssert.Contains(methods, nameof(LegacyTenPayV3.CustomsDeclareQueryAsync));
            CollectionAssert.Contains(methods, nameof(LegacyTenPayV3.CustomsRedeclare));
            CollectionAssert.Contains(methods, nameof(LegacyTenPayV3.CustomsRedeclareAsync));

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.TenPay", "Senparc.Weixin.TenPay", "V3", "Universal",
                "Customs", "TenPayV3.Customs.cs"));
            StringAssert.Contains(source, "/cgi-bin/mch/customs/customdeclareorder");
            StringAssert.Contains(source, "/cgi-bin/mch/customs/customdeclarequery");
            StringAssert.Contains(source, "/cgi-bin/mch/newcustoms/customdeclareredeclare");
            Assert.IsFalse(source.Contains("CertPost("),
                "海关报关接口不应强制使用商户证书。");
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
