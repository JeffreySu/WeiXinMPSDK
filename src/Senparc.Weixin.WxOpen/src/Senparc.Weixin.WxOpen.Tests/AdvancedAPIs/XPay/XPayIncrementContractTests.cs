using System.Linq;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.WxOpen.AdvancedAPIs.XPay;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.XPay
{
    [TestClass]
    public class XPayIncrementContractTests
    {
        [TestMethod]
        public void ApiSurfaceContainsCorrectedAndIncrementalSyncAsyncEntries()
        {
            var methods = typeof(XPayApi).GetMethods().Select(z => z.Name).ToArray();
            var expected = new[]
            {
                nameof(XPayApi.QueryBizBalance),
                nameof(XPayApi.QueryTransferAccount),
                nameof(XPayApi.BindTransferAccount),
                nameof(XPayApi.DownloadIosSettlementBill),
                nameof(XPayApi.QueryPunishmentReasons)
            };

            foreach (var method in expected)
            {
                CollectionAssert.Contains(methods, method);
                CollectionAssert.Contains(methods, method + "Async");
            }

            CollectionAssert.Contains(methods, nameof(XPayApi.BindTransferAccout));
            CollectionAssert.Contains(methods, nameof(XPayApi.BindTransferAccoutAsync));
        }

        [TestMethod]
        public void IosSettlementRequestPreservesSixDigitMonthFields()
        {
            var request = new DownloadIosSettlementBillRequestData
            {
                start_month = "202501",
                end_month = "202512"
            };

            using var document = JsonDocument.Parse(Serialize(request));

            Assert.AreEqual("202501", document.RootElement.GetProperty("start_month").GetString());
            Assert.AreEqual("202512", document.RootElement.GetProperty("end_month").GetString());
        }

        [TestMethod]
        public void IosSettlementResponseMapsBillListAndTemporaryUrl()
        {
            const string json = @"{
  ""errcode"": 0,
  ""errmsg"": """",
  ""bill_list"": [{
    ""month"": ""202501"",
    ""bill_url"": ""https://example.test/temporary-bill.csv""
  }]
}";

            var result = JsonConvert.DeserializeObject<DownloadIosSettlementBillJsonResult>(json);

            Assert.AreEqual("202501", result.bill_list[0].month);
            Assert.AreEqual("https://example.test/temporary-bill.csv", result.bill_list[0].bill_url);
        }

        [TestMethod]
        public void PunishmentReasonsMapsLimitationsAndRecoveryInstructions()
        {
            const string json = @"{
  ""errcode"": 0,
  ""appid"": ""wx123"",
  ""nickname"": ""测试小程序"",
  ""merchant_code"": ""1900000109"",
  ""limited_functions"": [""withdraw"", ""payment""],
  ""other_limited_functions"": ""其他能力"",
  ""recovery_specifications"": [{
    ""limitation_case_id"": ""case-1"",
    ""limitation_reason_type"": ""risk"",
    ""limitation_reason"": ""风险管控"",
    ""limitation_reason_describe"": ""需要补充材料"",
    ""relate_limitations"": [""withdraw""],
    ""recover_way"": ""merchant-platform"",
    ""recover_way_param"": ""appeal-1"",
    ""recover_help_url"": ""https://example.test/help"",
    ""limitation_action_type"": ""immediate"",
    ""limitation_start_date"": ""2025-01-01"",
    ""limitation_date"": ""2025-01-02""
  }]
}";

            var result = JsonConvert.DeserializeObject<QueryPunishmentReasonsJsonResult>(json);
            var recovery = result.recovery_specifications[0];

            Assert.AreEqual("1900000109", result.merchant_code);
            Assert.AreEqual("payment", result.limited_functions[1]);
            Assert.AreEqual("case-1", recovery.limitation_case_id);
            Assert.AreEqual("appeal-1", recovery.recover_way_param);
            Assert.IsInstanceOfType(recovery.relate_limitations, typeof(JArray));
        }

        [TestMethod]
        public void TransferAccountMapsAuditAndBindingStatusFields()
        {
            const string json = @"{
  ""errcode"": 0,
  ""acct_list"": [{
    ""transfer_account_name"": ""广告账户"",
    ""transfer_account_uid"": 123456,
    ""transfer_account_agency_id"": 987654,
    ""transfer_account_agency_name"": ""服务商"",
    ""state"": 2,
    ""bind_result"": 2,
    ""error_msg"": ""材料不完整""
  }]
}";

            var result = JsonConvert.DeserializeObject<QueryTransferAccountJsonResult>(json);
            var account = result.acct_list[0];

            Assert.AreEqual(2, account.state);
            Assert.AreEqual(2, account.bind_result);
            Assert.AreEqual("材料不完整", account.error_msg);
        }

        [TestMethod]
        public void BizBalanceMapsAmountAsCurrencyText()
        {
            const string json = @"{
  ""errcode"": 0,
  ""balance_available"": {
    ""amount"": ""128.50"",
    ""currency_code"": ""CNY""
  }
}";

            var result = JsonConvert.DeserializeObject<QueryBizBalanceJsonResult>(json);

            Assert.AreEqual("128.50", result.balance_available.amount);
            Assert.AreEqual("CNY", result.balance_available.currency_code);
        }

        private static string Serialize(object value)
        {
            return SerializerHelper.GetJsonString(value, new JsonSetting(ignoreNulls: true));
        }
    }
}
