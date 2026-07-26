using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.PayTool;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.PayTool
{
    [TestClass]
    public class PayToolContractTests
    {
        [TestMethod]
        public void PayToolApiCoversOfficialPathsProviderTokenAndDocuments()
        {
            var methods = typeof(PayToolApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            var expectedMethods = new[]
            {
                nameof(PayToolApi.GetInvoiceList), nameof(PayToolApi.GetInvoiceListAsync),
                nameof(PayToolApi.MarkInvoiceStatus), nameof(PayToolApi.MarkInvoiceStatusAsync),
                nameof(PayToolApi.OpenOrder), nameof(PayToolApi.OpenOrderAsync),
                nameof(PayToolApi.CloseOrder), nameof(PayToolApi.CloseOrderAsync),
                nameof(PayToolApi.GetOrderList), nameof(PayToolApi.GetOrderListAsync),
                nameof(PayToolApi.GetOrderDetail), nameof(PayToolApi.GetOrderDetailAsync)
            };
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "PayTool",
                "PayToolApi.cs"));

            foreach (var method in expectedMethods)
            {
                CollectionAssert.Contains(methods, method);
            }

            foreach (var path in new[]
            {
                "/cgi-bin/paytool/get_invoice_list",
                "/cgi-bin/paytool/mark_invoice_status",
                "/cgi-bin/paytool/open_order",
                "/cgi-bin/paytool/close_order",
                "/cgi-bin/paytool/get_order_list",
                "/cgi-bin/paytool/get_order_detail"
            })
            {
                Assert.AreEqual(1, CountOccurrences(source, path), path);
            }

            foreach (var documentId in new[] { "99436", "99437", "98045", "98046", "98053", "98054" })
            {
                Assert.AreEqual(2, CountOccurrences(source, "document/path/" + documentId),
                    documentId);
            }

            Assert.AreEqual(2, CountOccurrences(source, "?provider_access_token={0}"));
            Assert.AreEqual(13, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(2, CountOccurrences(source, "CommonJsonSendType.POST"));
            Assert.AreEqual(2, CountOccurrences(source,
                "jsonSetting: IgnoreNullJsonSetting"));
            Assert.AreEqual(2, CountOccurrences(source,
                "PayToolSignatureHelper.PrepareRequest"));
        }

        [TestMethod]
        public void SignatureMatchesOfficialSimpleExample()
        {
            const string secret =
                "at23pxnPBNQY3JiA8N5U1gabiQqxZwqH_Gihg7a_wrULmlOPVP-iiRjv9JWYPrDk";
            var request = new
            {
                orderid = "ord7",
                buyer_corpid = "ww66302cfadbdd3c64",
                buyer_userid = "invitetest",
                product_id = "product_id_xxx",
                product_name = "product_name_xxx",
                product_detail = "product_detail_xxx",
                unit_name = "台",
                unit_price = 1,
                num = 3,
                nonce_str = "129031823",
                ts = 1548302135L,
                sig = "mPOwVW/vQ74xN+b+Yu1KMa9RrmhKJaJjAtXHTof+EpU="
            };

            Assert.AreEqual("/WTXl/L2kJCYKJE5yY2JZvPq3rUjFf/pf39UhyJ2GUo=",
                PayToolSignatureHelper.CreateSignature(request, secret));
        }

        [TestMethod]
        public void SignatureRecursivelyFlattensArrayItemsAndSortsCompletePairs()
        {
            const string secret = "secret";
            var request = new
            {
                orderid = "i3khJ4dMv3",
                order_type = 1,
                credit_order_list = new[]
                {
                    new { credit_orderid = "CREDIT_ORDERID_1", unit_price = 100000, num = 1 },
                    new { credit_orderid = "CREDIT_ORDERID_2", unit_price = 90000, num = 2 }
                },
                appid = 2,
                buyer_corpid = "wwfedd7e5292d63a35",
                buyer_userid = "zhangsan",
                product_id = "xxxxxxxxxxx",
                product_name = "xxxxxxxxxxxxx",
                product_detail = "xxxxxxxxxxxx",
                unit_name = "台",
                nonce_str = "1287319372",
                ts = 1547719184L,
                sig = "ignored"
            };
            const string canonical =
                "appid=2&buyer_corpid=wwfedd7e5292d63a35&buyer_userid=zhangsan&" +
                "credit_orderid=CREDIT_ORDERID_1&credit_orderid=CREDIT_ORDERID_2&" +
                "nonce_str=1287319372&num=1&num=2&order_type=1&orderid=i3khJ4dMv3&" +
                "product_detail=xxxxxxxxxxxx&product_id=xxxxxxxxxxx&" +
                "product_name=xxxxxxxxxxxxx&ts=1547719184&unit_name=台&" +
                "unit_price=100000&unit_price=90000";

            Assert.AreEqual(ComputeSignature(canonical, secret),
                PayToolSignatureHelper.CreateSignature(request, secret));
        }

        [TestMethod]
        public void PrepareRequestCreatesAntiReplayFieldsAndPreservesPreSignedRequest()
        {
            var request = new PayToolCloseOrderRequest { order_id = "ORDERID" };
            PayToolSignatureHelper.PrepareRequest(request, "secret");

            Assert.AreEqual(32, request.nonce_str.Length);
            Assert.IsTrue(request.ts > 0);
            Assert.AreEqual(PayToolSignatureHelper.CreateSignature(request, "secret"),
                request.sig);

            var preSigned = new PayToolGetOrderDetailRequest
            {
                order_id = "ORDERID",
                nonce_str = "nonce",
                ts = 1548302135,
                sig = "pre-signed"
            };
            PayToolSignatureHelper.PrepareRequest(preSigned, null);
            Assert.AreEqual("pre-signed", preSigned.sig);

            Assert.ThrowsException<ArgumentException>(() =>
                PayToolSignatureHelper.PrepareRequest(
                    new PayToolCloseOrderRequest { order_id = "ORDERID" }, null));
        }

        [TestMethod]
        public void ModelsPreserveOfficialNamesLargeValuesAndStrongProductDetails()
        {
            var invoiceRequest = new PayToolGetInvoiceListRequest
            {
                start_time = 1680000000,
                end_time = 1680003600,
                cursor = "CURSOR",
                limit = 50
            };
            using (var requestJson = JsonDocument.Parse(JsonSerializer.Serialize(invoiceRequest)))
            {
                Assert.AreEqual(1680000000L,
                    requestJson.RootElement.GetProperty("start_time").GetInt64());
                Assert.AreEqual("CURSOR",
                    requestJson.RootElement.GetProperty("cursor").GetString());
            }

            var invoiceResult = Newtonsoft.Json.JsonConvert.DeserializeObject<PayToolGetInvoiceListResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"has_more\":1," +
                "\"next_cursor\":\"NEXT\",\"invoice_list\":[{" +
                "\"order_id\":\"ORDERID\",\"apply_time\":5000000000," +
                "\"paid_price\":6000000000,\"invoice_status\":2}]}");
            Assert.AreEqual(5000000000L, invoiceResult.invoice_list[0].apply_time);
            Assert.AreEqual(6000000000L, invoiceResult.invoice_list[0].paid_price);

            var detail = Newtonsoft.Json.JsonConvert.DeserializeObject<PayToolGetOrderDetailResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"pay_order\":{" +
                "\"order_id\":\"ORDERID\",\"creator\":\"CREATOR\"," +
                "\"income_amount\":5000000000,\"product_list\":{" +
                "\"third_app\":{\"order_type\":0,\"buy_info_list\":[{" +
                "\"suiteid\":\"SUITEID\",\"edition_id\":\"EDITION\"," +
                "\"origin_price\":5000000000,\"paid_price\":4000000000}]}}}}");
            Assert.AreEqual("CREATOR", detail.pay_order.creator);
            Assert.AreEqual(5000000000L, detail.pay_order.income_amount);
            Assert.AreEqual(4000000000L,
                detail.pay_order.product_list.third_app.buy_info_list[0].paid_price);

            var modelSource = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "PayTool",
                "PayToolJson.cs"));
            var declarations = Regex.Matches(modelSource,
                    @"^\s*public (?:abstract )?class ", RegexOptions.Multiline).Count +
                Regex.Matches(modelSource,
                    @"^\s*public [^\r\n]+ \{ get; set; \}$", RegexOptions.Multiline).Count;
            Assert.AreEqual(declarations, CountOccurrences(modelSource, "/// <summary>"));
            Assert.IsFalse(modelSource.Contains("object "));
        }

        private static string ComputeSignature(string canonical, string secret)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret)))
            {
                return Convert.ToBase64String(
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(canonical)));
            }
        }

        private static int CountOccurrences(string value, string search)
        {
            return value.Split(new[] { search }, StringSplitOptions.None).Length - 1;
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
