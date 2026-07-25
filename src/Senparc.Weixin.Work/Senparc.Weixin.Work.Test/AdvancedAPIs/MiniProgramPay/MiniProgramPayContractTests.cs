using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.MiniProgramPay;
using Senparc.Weixin.Work.Entities;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.MiniProgramPay
{
    [TestClass]
    public class MiniProgramPayContractTests
    {
        [TestMethod]
        public void MiniProgramPayApiContainsTenSyncAndAsyncEntries()
        {
            var methodNames = typeof(MiniProgramPayApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();
            var syncMethodNames = new[]
            {
                nameof(MiniProgramPayApi.UploadImage),
                nameof(MiniProgramPayApi.ApplyMerchant),
                nameof(MiniProgramPayApi.GetApplymentStatus),
                nameof(MiniProgramPayApi.CreateOrder),
                nameof(MiniProgramPayApi.GetOrder),
                nameof(MiniProgramPayApi.CloseOrder),
                nameof(MiniProgramPayApi.GetPaySign),
                nameof(MiniProgramPayApi.Refund),
                nameof(MiniProgramPayApi.GetRefundDetail),
                nameof(MiniProgramPayApi.GetBill)
            };

            foreach (var syncMethodName in syncMethodNames)
            {
                CollectionAssert.Contains(methodNames, syncMethodName, syncMethodName);
                CollectionAssert.Contains(methodNames, syncMethodName + "Async", syncMethodName + "Async");
            }
        }

        [TestMethod]
        public void MiniProgramPayApiUsesOfficialPathsAndVerbs()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(),
                "src", "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs",
                "MiniProgramPay", "MiniProgramPayApi.cs"));
            var paths = new[]
            {
                "/cgi-bin/miniapppay/upload_image",
                "/cgi-bin/miniapppay/apply_mch",
                "/cgi-bin/miniapppay/get_applyment_status",
                "/cgi-bin/miniapppay/create_order",
                "/cgi-bin/miniapppay/get_order",
                "/cgi-bin/miniapppay/close_order",
                "/cgi-bin/miniapppay/get_sign",
                "/cgi-bin/miniapppay/refund",
                "/cgi-bin/miniapppay/get_refund_detail",
                "/cgi-bin/miniapppay/get_bill"
            };

            foreach (var path in paths)
            {
                StringAssert.Contains(source, path);
            }

            StringAssert.Contains(source, "[\"name\"] = \"media\"");
            StringAssert.Contains(source, "CommonJsonSendType.POST");
            StringAssert.Contains(source, "CommonJsonSendType.GET");
        }

        [TestMethod]
        public void ApplymentAndOrderRequestsUseOfficialJsonFields()
        {
            var applyJson = JsonSerializer.Serialize(new ApplyMiniProgramPayMerchantRequest
            {
                out_request_no = "apply-1",
                organization_type = 0,
                business_license_info = new MiniProgramPayBusinessLicenseInfo
                {
                    cert_type = 1,
                    business_license_number = "license-1"
                },
                merchant_short_name = "Senparc",
                id_card_info = new MiniProgramPayIdCardInfo { id_card_number = "id-1" },
                contact_info = new MiniProgramPayContactInfo { mobile_phone = "13800000000" },
                account_info = new MiniProgramPayAccountInfo { account_number = "62220000" },
                sales_scene_info = new MiniProgramPaySalesSceneInfo
                {
                    type = 1,
                    store_url = "https://example.test/store",
                    address_code = "110100"
                },
                business_id = 100,
                userid = "jeffrey"
            });
            var orderJson = JsonSerializer.Serialize(new CreateMiniProgramPayOrderRequest
            {
                appid = "wx-app",
                mchid = "mch-1",
                out_trade_no = "order-1",
                description = "product",
                scenekey = "scene-1",
                amount = new MiniProgramPayOrderAmount { total = 100, currency = "CNY" },
                payer = new MiniProgramPayPayer { openid = "openid-1" },
                scene_info = new MiniProgramPaySceneInfo { payer_client_ip = "127.0.0.1" }
            });
            var signJson = JsonSerializer.Serialize(new GetMiniProgramPaySignRequest
            {
                appid = "wx-app",
                prepay_id = "prepay-1",
                sign_type = "RSA",
                nonce = "nonce",
                timestamp = 5178368698L
            });

            StringAssert.Contains(applyJson, "\"out_request_no\":\"apply-1\"");
            StringAssert.Contains(applyJson, "\"business_license_info\"");
            StringAssert.Contains(applyJson, "\"account_info\"");
            StringAssert.Contains(applyJson, "\"sales_scene_info\"");
            StringAssert.Contains(applyJson, "\"store_url\":\"https://example.test/store\"");
            StringAssert.Contains(orderJson, "\"scenekey\":\"scene-1\"");
            StringAssert.Contains(orderJson, "\"payer_client_ip\":\"127.0.0.1\"");
            StringAssert.Contains(signJson, "\"timestamp\":5178368698");
        }

        [TestMethod]
        public void ResultsPreserveOrderRefundAndBillContracts()
        {
            var order = JsonSerializer.Deserialize<GetMiniProgramPayOrderResult>(
                "{\"errcode\":0,\"mchid\":\"mch-1\",\"out_trade_no\":\"order-1\"," +
                "\"trade_type\":\"JSAPI\",\"trade_state\":\"SUCCESS\"," +
                "\"trade_state_desc\":\"paid\",\"success_time\":\"2026-07-25T10:00:00+08:00\"," +
                "\"amount\":{\"total\":100,\"payer_total\":90,\"currency\":\"CNY\"}," +
                "\"promotion_detail\":[{\"coupon_id\":\"coupon-1\",\"scope\":\"GLOBAL\"," +
                "\"type\":\"CASH\",\"amount\":10}]}" );
            var refund = JsonSerializer.Deserialize<GetMiniProgramPayRefundDetailResult>(
                "{\"errcode\":0,\"refund_id\":\"refund-1\",\"out_refund_no\":\"out-refund-1\"," +
                "\"transaction_id\":\"transaction-1\",\"out_trade_no\":\"order-1\"," +
                "\"status\":\"SUCCESS\",\"amount\":{\"refund\":50,\"payer_refund\":50," +
                "\"currency\":\"CNY\"}}" );
            var bill = JsonSerializer.Deserialize<GetMiniProgramPayBillResult>(
                "{\"errcode\":0,\"download_url\":\"https://example.test/bill\"," +
                "\"hash_type\":\"SHA1\",\"hash_value\":\"hash\",\"auth\":\"token\"}" );

            Assert.IsNotNull(order);
            Assert.AreEqual(90, order.amount.payer_total);
            Assert.AreEqual("coupon-1", order.promotion_detail[0].coupon_id);
            Assert.IsNotNull(refund);
            Assert.AreEqual("refund-1", refund.refund_id);
            Assert.AreEqual(50, refund.amount.payer_refund);
            Assert.IsNotNull(bill);
            Assert.AreEqual("token", bill.auth);
        }

        [TestMethod]
        public void PaymentNotificationsMapWithoutTraditionalMsgType()
        {
            var transaction = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), BuildNotificationDocument("TRANSACTION.SUCCESS"))
                as RequestMessageEvent_MiniProgramPay_Transaction;
            var refund = RequestMessageFactory.GetRequestEntity(
                new MessageContexts.DefaultWorkMessageContext(), BuildNotificationDocument("REFUND.SUCCESS"))
                as RequestMessageEvent_MiniProgramPay_Refund;

            Assert.IsNotNull(transaction);
            Assert.AreEqual(Event.pay_transaction, transaction.Event);
            Assert.AreEqual("AEAD_AES_256_GCM", transaction.resource.algorithm);
            Assert.AreEqual("associated", transaction.resource.associated_data);
            Assert.IsNotNull(refund);
            Assert.AreEqual(Event.pay_refund, refund.Event);
            Assert.AreEqual("REFUND.SUCCESS", refund.event_type);
        }

        [TestMethod]
        public void PaymentNotificationResourceDecryptsWithEncodingAesKey()
        {
            var key = RandomNumberGenerator.GetBytes(32);
            var encodingAesKey = Convert.ToBase64String(key).TrimEnd('=');
            var nonce = "0123456789ab";
            var associatedData = "transaction";
            var json = "{\"appid\":\"wx-app\",\"mchid\":\"mch-1\"," +
                       "\"out_trade_no\":\"order-1\",\"trade_state\":\"SUCCESS\"," +
                       "\"amount\":{\"total\":100,\"currency\":\"CNY\"}}";
            var plaintext = Encoding.UTF8.GetBytes(json);
            var ciphertext = new byte[plaintext.Length];
            var tag = new byte[16];
            using (var aesGcm = new AesGcm(key, tag.Length))
            {
                aesGcm.Encrypt(Encoding.UTF8.GetBytes(nonce), plaintext, ciphertext, tag,
                    Encoding.UTF8.GetBytes(associatedData));
            }

            var encrypted = new byte[ciphertext.Length + tag.Length];
            Buffer.BlockCopy(ciphertext, 0, encrypted, 0, ciphertext.Length);
            Buffer.BlockCopy(tag, 0, encrypted, ciphertext.Length, tag.Length);
            var notification = new RequestMessageEvent_MiniProgramPay_Transaction
            {
                resource = new MiniProgramPayNotificationResource
                {
                    algorithm = "AEAD_AES_256_GCM",
                    associated_data = associatedData,
                    nonce = nonce,
                    ciphertext = Convert.ToBase64String(encrypted)
                }
            };

            var result = notification.DecryptResource<MiniProgramPayTransactionNotification>(encodingAesKey);

            Assert.IsNotNull(result);
            Assert.AreEqual("wx-app", result.appid);
            Assert.AreEqual("order-1", result.out_trade_no);
            Assert.AreEqual(100, result.amount.total);
        }

        private static XDocument BuildNotificationDocument(string eventType)
            => XDocument.Parse($@"<xml>
<id><![CDATA[notification-1]]></id>
<create_time><![CDATA[2026-07-25T10:00:00+08:00]]></create_time>
<event_type><![CDATA[{eventType}]]></event_type>
<resource_type><![CDATA[encrypt-resource]]></resource_type>
<resource>
  <algorithm><![CDATA[AEAD_AES_256_GCM]]></algorithm>
  <ciphertext><![CDATA[ciphertext]]></ciphertext>
  <associated_data><![CDATA[associated]]></associated_data>
  <nonce><![CDATA[nonce]]></nonce>
</resource>
<summary><![CDATA[summary]]></summary>
</xml>");

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
