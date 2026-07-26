using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.ExternalPay;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.ExternalPay
{
    [TestClass]
    public class ExternalPayContractTests
    {
        [TestMethod]
        public void ExternalPayApiExposesSevenSynchronousAndAsynchronousEndpoints()
        {
            var methods = typeof(ExternalPayApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var methodName in new[]
            {
                nameof(ExternalPayApi.AddMerchant), nameof(ExternalPayApi.GetMerchant),
                nameof(ExternalPayApi.DeleteMerchant), nameof(ExternalPayApi.SetMerchantUseScope),
                nameof(ExternalPayApi.GetBillList), nameof(ExternalPayApi.GetPaymentInfo),
                nameof(ExternalPayApi.GetFundFlow)
            })
            {
                CollectionAssert.Contains(methods, methodName);
                CollectionAssert.Contains(methods, methodName + "Async");
            }
        }

        [TestMethod]
        public void ExternalPayApiUsesOfficialPathsAndDocumentationLinks()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "ExternalPay", "ExternalPayApi.cs"));

            foreach (var path in new[]
            {
                "/cgi-bin/externalpay/addmerchant", "/cgi-bin/externalpay/getmerchant",
                "/cgi-bin/externalpay/delmerchant", "/cgi-bin/externalpay/set_mch_use_scope",
                "/cgi-bin/externalpay/get_bill_list", "/cgi-bin/externalpay/get_payment_info",
                "/cgi-bin/externalpay/get_fund_flow"
            })
            {
                Assert.AreEqual(1, CountOccurrences(source, path), path);
            }

            Assert.AreEqual(8, CountOccurrences(source, "document/path/93666"));
            Assert.AreEqual(2, CountOccurrences(source, "document/path/93667"));
            Assert.AreEqual(2, CountOccurrences(source, "document/path/93727"));
            Assert.AreEqual(2, CountOccurrences(source, "document/path/95944"));
            Assert.AreEqual(2, CountOccurrences(source, "document/path/98100"));
            Assert.AreEqual(15, CountOccurrences(source, "/// <summary>"));
        }

        [TestMethod]
        public void MerchantModelsMatchOfficialSamplesAndKeepLargeDepartmentIds()
        {
            Assert.AreEqual(
                "{\"mch_id\":\"12334\",\"merchant_name\":\"test-merchant\"}",
                JsonSerializer.Serialize(new ExternalPayAddMerchantRequest
                {
                    mch_id = "12334",
                    merchant_name = "test-merchant"
                }));
            Assert.AreEqual(
                "{\"mch_id\":\"12334\"}",
                JsonSerializer.Serialize(new ExternalPayMerchantRequest { mch_id = "12334" }));

            var scopeRequest = new ExternalPaySetMerchantUseScopeRequest
            {
                mch_id = "12334",
                allow_use_scope = new ExternalPayUseScope
                {
                    user = new List<string> { "zhangsan", "lisi" },
                    partyid = new List<long> { 4294967296L },
                    tagid = new List<int> { 1, 2, 3 }
                }
            };
            using (var document = JsonDocument.Parse(JsonSerializer.Serialize(scopeRequest)))
            {
                Assert.AreEqual("12334", document.RootElement.GetProperty("mch_id").GetString());
                Assert.AreEqual(4294967296L, document.RootElement.GetProperty("allow_use_scope")
                    .GetProperty("partyid")[0].GetInt64());
            }

            var merchant = Newtonsoft.Json.JsonConvert.DeserializeObject<ExternalPayGetMerchantResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"bind_status\":0," +
                "\"mch_id\":\"12334\",\"merchant_name\":\"测试商户\"," +
                "\"allow_use_scope\":{\"user\":[\"zhangsan\"]," +
                "\"partyid\":[4294967296],\"tagid\":[1,2,3]}}");

            Assert.AreEqual("测试商户", merchant.merchant_name);
            Assert.AreEqual(4294967296L, merchant.allow_use_scope.partyid[0]);
        }

        [TestMethod]
        public void BillPaymentAndFundFlowModelsMatchOfficialSamplesAndHaveCompleteComments()
        {
            var bills = Newtonsoft.Json.JsonConvert.DeserializeObject<ExternalPayGetBillListResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"next_cursor\":\"CURSOR\"," +
                "\"bill_list\":[{\"transaction_id\":\"transaction-1\",\"bill_type\":1," +
                "\"trade_state\":1,\"pay_time\":5000000000,\"out_trade_no\":\"order-1\"," +
                "\"out_refund_no\":\"refund-1\",\"external_userid\":\"wm-user\"," +
                "\"total_fee\":100,\"payee_userid\":\"zhangsan\",\"payment_type\":1," +
                "\"mch_id\":\"12334\",\"remark\":\"备注\",\"commodity_list\":[{" +
                "\"description\":\"手机\",\"amount\":1}],\"total_refund_fee\":100," +
                "\"refund_list\":[{\"out_refund_no\":\"refund-1\"," +
                "\"refund_userid\":\"lisi\",\"refund_comment\":\"重复支付\"," +
                "\"refund_reqtime\":5000000001,\"refund_status\":1,\"refund_fee\":100}]," +
                "\"payer_info\":{\"name\":\"付款人\",\"phone\":\"13800000000\"," +
                "\"address\":\"苏州\"},\"miniprogram_info\":{\"appid\":\"wx-app\"," +
                "\"name\":\"收款小程序\"}}]}");
            var payment = Newtonsoft.Json.JsonConvert.DeserializeObject<ExternalPayGetPaymentInfoResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"bill_list\":[{" +
                "\"out_trade_no\":\"order-1\"},{\"out_trade_no\":\"order-2\"}]}");

            Assert.AreEqual(5000000000L, bills.bill_list[0].pay_time);
            Assert.AreEqual(5000000001L, bills.bill_list[0].refund_list[0].refund_reqtime);
            Assert.AreEqual("收款小程序", bills.bill_list[0].miniprogram_info.name);
            Assert.AreEqual("order-2", payment.bill_list[1].out_trade_no);

            var fundFlow = Newtonsoft.Json.JsonConvert
                .DeserializeObject<ExternalPayGetFundFlowResult>(
                    "{\"errcode\":0,\"next_cursor\":\"CURSOR\"," +
                    "\"fund_flow_list\":[{\"timestamp\":5178368698," +
                    "\"request_no\":\"flow-1\",\"transaction_type\":3," +
                    "\"fund_flow_type\":1,\"transaction_amount\":5000000000," +
                    "\"account_balance\":6000000000,\"out_trade_no\":\"order-1\"," +
                    "\"mch_id\":\"12334\",\"operator_userid\":\"zhangsan\"," +
                    "\"group_list\":[{\"group_name\":\"Rule1\"}]," +
                    "\"remark\":\"收款\"}]}" );
            Assert.AreEqual(5178368698L, fundFlow.fund_flow_list[0].timestamp);
            Assert.AreEqual(5000000000L, fundFlow.fund_flow_list[0].transaction_amount);
            Assert.AreEqual(6000000000L, fundFlow.fund_flow_list[0].account_balance);
            Assert.AreEqual("Rule1", fundFlow.fund_flow_list[0].group_list[0].group_name);

            var modelSource = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "ExternalPay",
                "ExternalPayJson.cs"));
            Assert.AreEqual(89, CountOccurrences(modelSource, "/// <summary>"));
            Assert.IsFalse(modelSource.Contains("object "));
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
