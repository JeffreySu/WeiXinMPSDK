using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.NeuChar;
using Senparc.Weixin.WxOpen.AdvancedAPIs.B2B;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.MessageContexts;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.B2B
{
    [TestClass]
    public class B2BContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(B2BApi.ApplyRetailBusiness)] = "/wxa/business/retailbusinessapply",
                [nameof(B2BApi.BatchCreateRetail)] = "/wxa/business/batchcreateretail",
                [nameof(B2BApi.GetRetailInfo)] = "/wxa/business/getretailinfo",
                [nameof(B2BApi.GetRetailOpenIdList)] = "/wxa/business/getretailopenidlist",
                [nameof(B2BApi.SendRetailNotification)] = "/wxa/business/retailnotifybusiness",
                [nameof(B2BApi.GetRetailMessageList)] = "/wxa/business/getretailmessagelist",
                [nameof(B2BApi.RegisterMerchant)] = "/retail/B2b/retailregistermch",
                [nameof(B2BApi.UploadMerchantFile)] = "/retail/B2b/retailuploadmchfile",
                [nameof(B2BApi.GetMerchantApplication)] = "/retail/B2b/retailgetmchorder",
                [nameof(B2BApi.ApplyBankTransfer)] = "/retail/B2b/registeronlywqf",
                [nameof(B2BApi.CreateBankTransferLink)] = "/retail/B2b/createwqflink",
                [nameof(B2BApi.GetMerchantInfo)] = "/retail/B2b/getmchinfo",
                [nameof(B2BApi.SetMerchantProfitRate)] = "/retail/B2b/setmchprofitrate",
                [nameof(B2BApi.UpdateBankTransferFee)] = "/retail/B2b/updatewqfchargefee",
                [nameof(B2BApi.GetBankTransferFee)] = "/retail/B2b/getwqfchargefee",
                [nameof(B2BApi.GetOrder)] = "/retail/B2b/getorder",
                [nameof(B2BApi.CloseOrder)] = "/retail/B2b/closeb2border",
                [nameof(B2BApi.RefundOrder)] = "/retail/B2b/refund",
                [nameof(B2BApi.GetRefund)] = "/retail/B2b/getrefund",
                [nameof(B2BApi.GetAppKey)] = "/retail/B2b/getappkey",
                [nameof(B2BApi.DownloadBill)] = "/retail/B2b/downloadbill",
                [nameof(B2BApi.GetMerchantBalance)] = "/retail/B2b/getmchbalance",
                [nameof(B2BApi.Withdraw)] = "/retail/B2b/withdraw",
                [nameof(B2BApi.QueryWithdraw)] = "/retail/B2b/querywithdraw",
                [nameof(B2BApi.SetAutoWithdraw)] = "/retail/B2b/setautowithdraw",
                [nameof(B2BApi.AddProfitSharingAccount)] = "/retail/B2b/addprofitsharingaccount",
                [nameof(B2BApi.DeleteProfitSharingAccount)] = "/retail/B2b/delprofitsharingaccount",
                [nameof(B2BApi.QueryProfitSharingAccount)] = "/retail/B2b/queryprofitsharingaccount",
                [nameof(B2BApi.CreateProfitSharingOrder)] = "/retail/B2b/createprofitsharingorder",
                [nameof(B2BApi.QueryProfitSharingOrder)] = "/retail/B2b/queryprofitsharingorder",
                [nameof(B2BApi.QueryProfitSharingRemainingAmount)] = "/retail/B2b/queryprofitsharingremainamt",
                [nameof(B2BApi.FinishProfitSharingOrder)] = "/retail/B2b/finishprofitsharingorder",
                [nameof(B2BApi.RefundProfitSharing)] = "/retail/B2b/refundprofitsharing",
                [nameof(B2BApi.QueryRefundProfitSharingOrder)] = "/retail/B2b/queryrefundprofitsharingorder"
            };

        [TestMethod]
        public void ApiSurfaceContainsThirtyFourSyncAndAsyncEntries()
        {
            var methods = typeof(B2BApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name)
                .ToArray();

            Assert.AreEqual(34, OfficialEndpoints.Count);
            foreach (var method in OfficialEndpoints.Keys)
            {
                CollectionAssert.Contains(methods, method);
                CollectionAssert.Contains(methods, method + "Async");
            }
        }

        [TestMethod]
        public void EveryPublicEntryUsesItsOfficialEndpoint()
        {
            foreach (var pair in OfficialEndpoints)
            {
                var sync = typeof(B2BApi).GetMethod(pair.Key, BindingFlags.Public | BindingFlags.Static);
                var async = typeof(B2BApi).GetMethod(pair.Key + "Async", BindingFlags.Public | BindingFlags.Static);

                CollectionAssert.Contains(GetStringLiterals(sync).ToArray(), pair.Value, pair.Key);
                CollectionAssert.Contains(GetStringLiterals(async).ToArray(), pair.Value, pair.Key + "Async");
            }
        }

        [TestMethod]
        public void PaySignatureAppearsOnlyOnOfficialSignedEndpoints()
        {
            var expected = new HashSet<string>
            {
                nameof(B2BApi.GetOrder), nameof(B2BApi.CloseOrder), nameof(B2BApi.RefundOrder), nameof(B2BApi.GetRefund),
                nameof(B2BApi.DownloadBill), nameof(B2BApi.GetMerchantBalance), nameof(B2BApi.Withdraw),
                nameof(B2BApi.QueryWithdraw), nameof(B2BApi.SetAutoWithdraw), nameof(B2BApi.AddProfitSharingAccount),
                nameof(B2BApi.DeleteProfitSharingAccount), nameof(B2BApi.QueryProfitSharingAccount),
                nameof(B2BApi.CreateProfitSharingOrder), nameof(B2BApi.QueryProfitSharingOrder),
                nameof(B2BApi.QueryProfitSharingRemainingAmount), nameof(B2BApi.FinishProfitSharingOrder),
                nameof(B2BApi.RefundProfitSharing), nameof(B2BApi.QueryRefundProfitSharingOrder)
            };

            foreach (var methodName in OfficialEndpoints.Keys)
            {
                var method = typeof(B2BApi).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
                var hasPaySig = method.GetParameters().Any(parameter => parameter.Name == "paySig");
                Assert.AreEqual(expected.Contains(methodName), hasPaySig, methodName);
            }
        }

        [TestMethod]
        public void BuildUrlEscapesPaySignatureAndOmitsItWhenNotRequired()
        {
            var method = typeof(B2BApi).GetMethod("BuildUrl", BindingFlags.NonPublic | BindingFlags.Static);

            var unsigned = (string)method.Invoke(null, new object[] { "/retail/B2b/getappkey", null });
            var signed = (string)method.Invoke(null, new object[] { "/retail/B2b/getorder", "a+b/=" });

            Assert.IsTrue(unsigned == Config.ApiMpHost + "/retail/B2b/getappkey?access_token={0}");
            var signedPrefix = Config.ApiMpHost + "/retail/B2b/getorder?access_token={0}&pay_sig=";
            Assert.IsTrue(signed.StartsWith(signedPrefix, StringComparison.Ordinal));
            Assert.AreEqual("a+b/=", Uri.UnescapeDataString(signed.Substring(signedPrefix.Length)));
        }

        [TestMethod]
        public void StoreRequestsUseOfficialSnakeCaseAndOmitUnsetOptions()
        {
            var request = new B2BBatchCreateRetailRequest
            {
                retail_info_list = new List<B2BRetailPreEntry>
                {
                    new B2BRetailPreEntry
                    {
                        mobile_phone = "13712345678",
                        retail_name = "示例门店",
                        address_province = "广东省",
                        address_city = "广州市",
                        address_region = "海珠区",
                        address_street = "新港中路",
                        registration_number = "REG-1",
                        longitude = 113.32531,
                        business_type = new List<string> { "食品饮料" }
                    }
                }
            };

            using var document = JsonDocument.Parse(Serialize(request));
            var retail = document.RootElement.GetProperty("retail_info_list")[0];

            Assert.AreEqual("REG-1", retail.GetProperty("registration_number").GetString());
            Assert.AreEqual(113.32531, retail.GetProperty("longitude").GetDouble(), 0.000001);
            Assert.AreEqual("食品饮料", retail.GetProperty("business_type")[0].GetString());
            Assert.IsFalse(retail.TryGetProperty("latitude", out _));
            Assert.IsFalse(retail.TryGetProperty("sub_retail_type", out _));
        }

        [TestMethod]
        public void MerchantRegistrationPreservesExampleOnlySwitchesAndNumericOrganizationType()
        {
            var request = new B2BRegisterMerchantRequest
            {
                id_doc_type_num = 1,
                organization_type = 1,
                merchant_shortname = "示例商户",
                open_type = 1,
                ignore_same_entity = true,
                launch_poll_task = true,
                client_ip = "127.0.0.1"
            };

            using var document = JsonDocument.Parse(Serialize(request));
            var root = document.RootElement;

            Assert.AreEqual(1, root.GetProperty("organization_type").GetInt32());
            Assert.IsTrue(root.GetProperty("ignore_same_entity").GetBoolean());
            Assert.IsTrue(root.GetProperty("launch_poll_task").GetBoolean());
            Assert.IsFalse(root.TryGetProperty("id_doc_info", out _));
        }

        [TestMethod]
        public void MerchantApplicationMapsNestedPaymentAndBankTransferStatus()
        {
            const string json = @"{
  ""errcode"": 0,
  ""list"": [{
    ""status"": 6,
    ""inner_resp"": { ""sub_merchant_registration_status"": {
      ""applyment_state"": ""APPLYMENT_STATE_FINISHED"", ""sub_mchid"": ""1234567890"",
      ""account_validation"": { ""pay_amount"": 0.01 },
      ""audit_detail"": [{ ""param_name"": ""contact_info"", ""reject_reason"": ""资料不清晰"" }]
    }},
    ""wqf_register_statement"": { ""wqf_register_state"": 2, ""request_no"": ""MSE123"" },
    ""wx_pay_rate"": 40, ""wqf_certified_rate"": 22, ""bind_scene_status"": 6
  }],
  ""total"": 1
}";

            var result = JsonConvert.DeserializeObject<B2BGetMerchantApplicationJsonResult>(json);
            var item = result.list[0];

            Assert.AreEqual("1234567890", item.inner_resp.sub_merchant_registration_status.sub_mchid);
            Assert.AreEqual(0.01m, item.inner_resp.sub_merchant_registration_status.account_validation.pay_amount);
            Assert.AreEqual("资料不清晰", item.inner_resp.sub_merchant_registration_status.audit_detail[0].reject_reason);
            Assert.AreEqual("MSE123", item.wqf_register_statement.request_no);
        }

        [TestMethod]
        public void OrderAndRefundResponsesMapMoneyAndChannelDetails()
        {
            const string orderJson = @"{
  ""errcode"": 0, ""order_id"": ""o202307291"", ""pay_status"": ""ORDER_PAY_SUCC"",
  ""amount"": { ""order_amount"": 1300, ""payer_amount"": 1200, ""currency"": ""CNY"" },
  ""settle_status"": 2, ""platform_profit_fee"": 6
}";
            const string refundJson = @"{
  ""errcode"": 0, ""refund_id"": ""r202307281"", ""refund_status"": ""REFUND_SUCC"",
  ""amount"": { ""order_amount"": 1300, ""refund_amount"": 100, ""currency"": ""CNY"" },
  ""refund_channel_info"": { ""channel"": ""ORIGINAL"", ""user_received_account"": ""招商银行借记卡0000"", ""funds_account"": ""UNAVAILABLE"" }
}";

            var order = JsonConvert.DeserializeObject<B2BGetOrderJsonResult>(orderJson);
            var refund = JsonConvert.DeserializeObject<B2BGetRefundJsonResult>(refundJson);

            Assert.AreEqual(1300L, order.amount.order_amount);
            Assert.AreEqual(6L, order.platform_profit_fee);
            Assert.AreEqual(100L, refund.amount.refund_amount);
            Assert.AreEqual("招商银行借记卡0000", refund.refund_channel_info.user_received_account);
        }

        [TestMethod]
        public void ProfitSharingRequestsKeepRequiredRefundAmountAndOmitUnsetPaging()
        {
            var refundRequest = new B2BRefundProfitSharingRequest
            {
                out_trade_no = "trade-1",
                out_refund_no = "refund-1",
                payee_type = "PAYEE_TYPE_EXTERNAL_MERCHANT",
                payee_id = "165406451",
                mchid = "166321431",
                refund_amt = 98
            };
            var pagingRequest = new B2BQueryProfitSharingAccountRequest { limit = 100 };

            using var refundDocument = JsonDocument.Parse(Serialize(refundRequest));
            using var pagingDocument = JsonDocument.Parse(Serialize(pagingRequest));

            Assert.AreEqual(98L, refundDocument.RootElement.GetProperty("refund_amt").GetInt64());
            Assert.AreEqual("PAYEE_TYPE_EXTERNAL_MERCHANT", refundDocument.RootElement.GetProperty("payee_type").GetString());
            Assert.AreEqual(100, pagingDocument.RootElement.GetProperty("limit").GetInt32());
            Assert.IsFalse(pagingDocument.RootElement.TryGetProperty("offset", out _));
        }

        [TestMethod]
        public void RetailRefundNotificationMapsAllMoneyAndChannelFields()
        {
            const string json = @"{
  ""appid"": ""wx8888888888888888"", ""mchid"": ""1230000109"",
  ""out_refund_no"": ""refund-1"", ""refund_id"": ""r202307281"",
  ""out_trade_no"": ""trade-1"", ""order_id"": ""o202307291"",
  ""refund_amount"": 888, ""order_amount"": 1300,
  ""refund_from"": ""2"", ""refund_reason"": ""3"",
  ""create_time"": ""2023-07-30 17:04:23"", ""refund_time"": ""2023-07-30 17:04:28"",
  ""refund_status"": ""REFUND_SUCC"", ""wxpay_refund_id"": ""500001"",
  ""env"": 0, ""pay_channel"": 1, ""refund_desc"": ""退款完成""
}";
            var notification = JsonConvert.DeserializeObject<RequestMessageEvent_RetailRefundNotify>(json);
            var context = new DefaultWxOpenMessageContext();
            var mapped = context.GetRequestEntityMappingResult(RequestMsgType.Event,
                XDocument.Parse("<xml><Event>retail_refund_notify</Event></xml>"));

            Assert.AreEqual(Event.retail_refund_notify, notification.Event);
            Assert.AreEqual(888L, notification.refund_amount);
            Assert.AreEqual(1300L, notification.order_amount);
            Assert.AreEqual(1, notification.pay_channel);
            Assert.IsInstanceOfType(mapped, typeof(RequestMessageEvent_RetailRefundNotify));
        }

        private static IEnumerable<string> GetStringLiterals(MethodInfo method)
        {
            var body = method?.GetMethodBody();
            var bytes = body?.GetILAsByteArray();
            if (bytes == null)
            {
                yield break;
            }

            for (var index = 0; index <= bytes.Length - 5; index++)
            {
                if (bytes[index] != 0x72)
                {
                    continue;
                }

                string value;
                try
                {
                    value = method.Module.ResolveString(BitConverter.ToInt32(bytes, index + 1));
                }
                catch (ArgumentException)
                {
                    // Operand bytes can coincidentally contain the ldstr opcode; only valid metadata tokens matter.
                    continue;
                }

                yield return value;
            }
        }

        private static string Serialize(object value)
        {
            return SerializerHelper.GetJsonString(value, new JsonSetting(ignoreNulls: true));
        }
    }
}
