using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.BrandMemberCard;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.Apis.BrandMemberCard
{
    [TestClass]
    public class BrandMemberCardContractTests
    {
        private static readonly IReadOnlyDictionary<string, string>
            OfficialEndpoints = new Dictionary<string, string>
            {
                [nameof(BrandMemberCardApis.CreateCardAsync)] =
                    "brand/card-member/cards",
                [nameof(BrandMemberCardApis.QueryCardsAsync)] =
                    "brand/card-member/cards",
                [nameof(BrandMemberCardApis.QueryCardAsync)] =
                    "brand/card-member/cards/",
                [nameof(BrandMemberCardApis.UpdateCardAsync)] =
                    "brand/card-member/cards/",
                [nameof(BrandMemberCardApis.InvalidateCardAsync)] =
                    "/invalidate",
                [nameof(BrandMemberCardApis.QueryUserCardAsync)] =
                    "brand/card-member/user-cards/",
                [nameof(BrandMemberCardApis.QueryUserCardsAsync)] =
                    "brand/card-member/user-cards",
                [nameof(BrandMemberCardApis.UpdateUserCardAsync)] =
                    "brand/card-member/user-cards/",
                [nameof(BrandMemberCardApis.InvalidateUserCardAsync)] =
                    "/invalidate",
                [nameof(BrandMemberCardApis.CreatePreAuthTokenAsync)] =
                    "brand/card-member/pre-auth-tokens",
                [nameof(BrandMemberCardApis.ImportUserCardByOpenIdAsync)] =
                    "brand/card-member/user-cards/import-by-openid",
                [nameof(BrandMemberCardApis.ConfirmUserCardAsync)] =
                    "/confirm",
                [nameof(BrandMemberCardApis.CreateUserFeedAsync)] =
                    "brand/card-member/user-feeds",
                [nameof(BrandMemberCardApis.SyncUserPointsAsync)] =
                    "brand/card-member/user-points/sync",
                [nameof(BrandMemberCardApis.ConfirmPointExchangeCouponAsync)] =
                    "brand/card-member/user-points/exchange-coupon/confirm",
                [nameof(BrandMemberCardApis.UploadMemberImageAsync)] =
                    "brand/card-member/media/image-upload"
            };

        [TestMethod]
        public void ApiSurfaceContainsTemplateAndUserCardEntries()
        {
            var methods = typeof(BrandMemberCardApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method =>
                    method.DeclaringType == typeof(BrandMemberCardApis))
                .Select(method => method.Name)
                .Distinct()
                .ToArray();

            Assert.AreEqual(16, OfficialEndpoints.Count);
            CollectionAssert.AreEquivalent(OfficialEndpoints.Keys.ToArray(),
                methods);
        }

        [TestMethod]
        public void EveryMethodContainsCurrentOfficialEndpoint()
        {
            foreach (var endpoint in OfficialEndpoints)
            {
                var methods = typeof(BrandMemberCardApis).GetMethods(
                        BindingFlags.Public | BindingFlags.Instance)
                    .Where(method => method.Name == endpoint.Key)
                    .ToArray();

                Assert.IsTrue(methods.Length > 0, endpoint.Key);
                Assert.IsTrue(methods.SelectMany(GetStringLiterals)
                        .Any(value => value.Contains(endpoint.Value)),
                    $"{endpoint.Key}: {endpoint.Value}");
            }
        }

        [TestMethod]
        public void QueryAndPathValuesAreEncodedAndNullValuesAreSkipped()
        {
            var buildQuery = typeof(BrandMemberCardApis).GetMethod(
                "BuildBrandMemberCardQuery",
                BindingFlags.NonPublic | BindingFlags.Static);
            var escape = typeof(BrandMemberCardApis).GetMethod(
                "EscapeBrandMemberCardValue",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(buildQuery);
            Assert.IsNotNull(escape);
            var path = (string)buildQuery.Invoke(null, new object[]
            {
                "brand/card-member/cards",
                new[]
                {
                    "state", "CARD EFFECTIVE+PINNED",
                    "offset", "0",
                    "optional", null,
                    "limit", "20"
                }
            });

            Assert.AreEqual(
                "brand/card-member/cards?state=CARD%20EFFECTIVE%2BPINNED&offset=0&limit=20",
                path);
            Assert.AreEqual("card%20%2B%20id",
                escape.Invoke(null, new object[] { "card + id" }));
        }

        [TestMethod]
        public void UpdateAndInvalidateUseTheirOfficialHttpSemantics()
        {
            var update = typeof(BrandMemberCardApis).GetMethod(
                nameof(BrandMemberCardApis.UpdateCardAsync));
            var invalidate = typeof(BrandMemberCardApis).GetMethod(
                nameof(BrandMemberCardApis.InvalidateCardAsync));
            var updateUser = typeof(BrandMemberCardApis).GetMethod(
                nameof(BrandMemberCardApis.UpdateUserCardAsync));
            var invalidateUser = typeof(BrandMemberCardApis).GetMethod(
                nameof(BrandMemberCardApis.InvalidateUserCardAsync));

            CollectionAssert.Contains(GetCalledMethodNames(update).ToArray(),
                "PatchAsync");
            CollectionAssert.Contains(
                GetCalledMethodNames(invalidate).ToArray(),
                "RequestWithoutBodyAsync");
            CollectionAssert.Contains(
                GetCalledMethodNames(updateUser).ToArray(), "PatchAsync");
            CollectionAssert.Contains(
                GetCalledMethodNames(invalidateUser).ToArray(), "PostAsync");
        }

        [TestMethod]
        public void CreateAndUpdatePreserveNestedOfficialFields()
        {
            var create = JObject.Parse(JsonConvert.SerializeObject(
                new BrandMemberCardCreateRequestData
                {
                    out_request_no = "brand_10001_20260725",
                    appid = "wx1234567890abcdef",
                    card_type = "NORMAL",
                    card_title = "盛派会员",
                    card_color = "#1AAD19",
                    card_picture_url = "https://example.com/member.png",
                    code_mode = "SYSTEM_ALLOCATE",
                    code_type = "JUMP_MINI_PROGRAM",
                    code_jump_information =
                        new BrandMemberCardJumpInformation
                        {
                            jump_appid = "wx1234567890abcdef",
                            jump_path = "/pages/code/index"
                        },
                    benefits = "会员折扣、专属价",
                    notify_url = "https://example.com/member/notify",
                    need_pinned = true,
                    need_display_level = true,
                    init_level = "银卡",
                    service_phone = "400-123-4567",
                    legal_agreement = "会员服务协议",
                    valid_date_information =
                        new BrandMemberCardValidDateInformation
                        {
                            type = "FIX_TERM",
                            available_day_after_receive = 365
                        },
                    member_information =
                        new BrandMemberCardJumpInformation
                        {
                            jump_appid = "wx1234567890abcdef",
                            jump_path = "/pages/member/index"
                        },
                    purchase_information =
                        new BrandMemberCardPurchaseInformation
                        {
                            price = 5000000000L,
                            jump_appid = "wx1234567890abcdef",
                            jump_path = "/pages/purchase/index"
                        },
                    user_information = new BrandMemberCardUserInformation
                    {
                        common_field_list =
                            new[] { "USER_FORM_FLAG_NAME" },
                        custom_field_list = new[]
                        {
                            new BrandMemberCardCustomField
                            {
                                type = "RADIO",
                                name = "喜欢的运动",
                                values = new[] { "篮球", "跑步" }
                            }
                        }
                    }
                }, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));
            var update = JObject.Parse(JsonConvert.SerializeObject(
                new BrandMemberCardUpdateRequestData
                {
                    card_title = "盛派金卡",
                    need_pinned = false,
                    points_information =
                        new BrandMemberCardJumpInformation
                        {
                            jump_appid = "wx1234567890abcdef",
                            jump_path = "/pages/points/index"
                        }
                }, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));

            Assert.AreEqual("SYSTEM_ALLOCATE",
                create["code_mode"]?.Value<string>());
            Assert.AreEqual("/pages/code/index",
                create["code_jump_information"]?["jump_path"]
                    ?.Value<string>());
            Assert.AreEqual(365,
                create["valid_date_information"]?
                    ["available_day_after_receive"]?.Value<int>());
            Assert.AreEqual(5000000000L,
                create["purchase_information"]?["price"]?.Value<long>());
            Assert.AreEqual("RADIO",
                create["user_information"]?["custom_field_list"]?[0]?
                    ["type"]?.Value<string>());
            Assert.AreEqual("/pages/points/index",
                update["points_information"]?["jump_path"]
                    ?.Value<string>());
            Assert.IsNull(update["valid_date_information"]);
        }

        [TestMethod]
        public void ResultModelsPreserveTemplateStatesAndPagination()
        {
            var list = JsonConvert
                .DeserializeObject<BrandMemberCardListResultJson>(
                    "{\"data\":[{\"out_request_no\":\"request_1\"," +
                    "\"card_id\":\"card_1\",\"brand_id\":\"brand_1\"," +
                    "\"card_type\":\"NORMAL\"," +
                    "\"code_mode\":\"SYSTEM_ALLOCATE\"," +
                    "\"code_type\":\"QR_CODE\"," +
                    "\"need_pinned\":true," +
                    "\"need_display_level\":true," +
                    "\"state\":\"CARD_INVALID\"," +
                    "\"valid_date_information\":{\"type\":\"PERMANENT\"}," +
                    "\"create_time\":\"2026-07-25T10:00:00+08:00\"," +
                    "\"modify_time\":\"2026-07-25T11:00:00+08:00\"}]," +
                    "\"total_count\":5000000000," +
                    "\"offset\":0,\"limit\":20}");

            Assert.AreEqual(5000000000L, list.total_count);
            Assert.AreEqual("CARD_INVALID", list.data[0].state);
            Assert.AreEqual("SYSTEM_ALLOCATE", list.data[0].code_mode);
            Assert.AreEqual("PERMANENT",
                list.data[0].valid_date_information.type);
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(BrandMemberCardResultJson)));
        }

        [TestMethod]
        public void UserCardModelsPreserveEncryptedFieldsStatesAndPagination()
        {
            var update = JObject.Parse(JsonConvert.SerializeObject(
                new BrandMemberCardUserCardUpdateRequestData
                {
                    card_id = "card_1",
                    openid = "openid_1",
                    phone_number = "encrypted-phone",
                    level = "钻石会员",
                    valid_date_information =
                        new BrandMemberCardValidDateInformation
                        {
                            type = "FIX_TERM",
                            available_day_after_receive = 365
                        },
                    user_information =
                        new BrandMemberCardUserProfileInformation
                        {
                            common_field_list = new[]
                            {
                                new BrandMemberCardCommonFieldValue
                                {
                                    name = "USER_FORM_FLAG_NAME",
                                    value = "encrypted-name"
                                }
                            },
                            custom_field_list = new[]
                            {
                                new BrandMemberCardCustomFieldValue
                                {
                                    name = "喜欢的运动",
                                    user_chosen_values =
                                        new[] { "encrypted-choice" }
                                }
                            }
                        },
                    attach = "merchant-data"
                }, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));
            var list = JsonConvert
                .DeserializeObject<BrandMemberCardUserCardListResultJson>(
                    "{\"data\":[{\"user_card_code\":\"code_1\"," +
                    "\"card_id\":\"card_1\",\"openid\":\"openid_1\"," +
                    "\"brand_id\":\"brand_1\",\"card_type\":\"NORMAL\"," +
                    "\"phone_number\":\"encrypted-phone\"," +
                    "\"user_card_state\":\"EFFECTIVE\"," +
                    "\"valid_date_information\":{\"type\":\"PERMANENT\"}," +
                    "\"user_information\":{\"common_field_list\":[{" +
                    "\"name\":\"USER_FORM_FLAG_NAME\"," +
                    "\"value\":\"encrypted-name\"}]}}]," +
                    "\"total_count\":5000000000," +
                    "\"offset\":0,\"limit\":20}");

            Assert.AreEqual("encrypted-phone",
                update["phone_number"]?.Value<string>());
            Assert.AreEqual("encrypted-name",
                update["user_information"]?["common_field_list"]?[0]?
                    ["value"]?.Value<string>());
            Assert.AreEqual("encrypted-choice",
                update["user_information"]?["custom_field_list"]?[0]?
                    ["user_chosen_values"]?[0]?.Value<string>());
            Assert.AreEqual(5000000000L, list.total_count);
            Assert.AreEqual("EFFECTIVE", list.data[0].user_card_state);
            Assert.AreEqual("PERMANENT",
                list.data[0].valid_date_information.type);
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(BrandMemberCardUserCardResultJson)));
        }

        [TestMethod]
        public void RemainingRequestModelsPreserveCurrentOfficialFields()
        {
            var confirm = JObject.Parse(JsonConvert.SerializeObject(
                new BrandMemberCardUserCardConfirmRequestData
                {
                    user_card_confirm_state = "CREATE_CARD_SUCCESS",
                    card_id = "card_1",
                    openid = "openid_1",
                    phone_number = "encrypted-phone",
                    attach = "merchant-data"
                }));
            var points = JsonConvert
                .DeserializeObject<BrandMemberCardPointBalanceResultJson>(
                    "{\"out_request_no\":\"request_1\"," +
                    "\"brand_id\":\"brand_1\"," +
                    "\"card_id\":\"card_1\"," +
                    "\"openid\":\"openid_1\"," +
                    "\"user_card_code\":\"code_1\"," +
                    "\"point_balance\":5000000000}");
            var exchange = JsonConvert
                .DeserializeObject<BrandMemberCardPointExchangeResultJson>(
                    "{\"record_id\":\"record_1\"," +
                    "\"state\":\"POINT_EXCHANGE_COUPON_SUCCESS\"," +
                    "\"product_coupon_id\":\"coupon_1\"," +
                    "\"coupon_code\":\"code_1\"}");

            Assert.AreEqual("CREATE_CARD_SUCCESS",
                confirm["user_card_confirm_state"]?.Value<string>());
            Assert.AreEqual("encrypted-phone",
                confirm["phone_number"]?.Value<string>());
            Assert.AreEqual(5000000000L, points.point_balance);
            Assert.AreEqual("POINT_EXCHANGE_COUPON_SUCCESS", exchange.state);
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(BrandMemberCardPreAuthTokenResultJson)));
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(BrandMemberCardImageUploadResultJson)));
        }

        [TestMethod]
        public void ImageUploadUsesExactBrandMultipartMetaFields()
        {
            var requestType = typeof(TenPayApiRequest);
            var styleType = requestType.GetNestedType(
                "MultipartMetaFieldStyle", BindingFlags.NonPublic);
            var createMeta = requestType.GetMethod(
                "CreateMultipartMetaJson",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(styleType);
            Assert.IsNotNull(createMeta);
            var style = Enum.Parse(styleType,
                "FilenameAndFileDigest");
            var meta = JObject.Parse((string)createMeta.Invoke(null,
                new[] { "member.png", "sha256-value", style }));

            Assert.AreEqual("member.png",
                meta["filename"]?.Value<string>());
            Assert.AreEqual("sha256-value",
                meta["file_digest"]?.Value<string>());
            Assert.IsNull(meta["file_name"]);
            Assert.IsNull(meta["sha256"]);
        }

        [TestMethod]
        public async Task ImageUploadRejectsUnsupportedOrOversizedFiles()
        {
            var key = Convert.ToBase64String(new byte[] { 1 });
            var apis = new BrandMemberCardApis(
                new TenPayBrandApiCredentials("brand_1", "serial_1",
                    key, "PUB_KEY_ID_1", key));

            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                apis.UploadMemberImageAsync("member.gif",
                    new MemoryStream(new byte[1])));
            await Assert.ThrowsExceptionAsync<InvalidDataException>(() =>
                apis.UploadMemberImageAsync("member.png",
                    new MemoryStream(new byte[2 * 1024 * 1024 + 1])));
        }

        [TestMethod]
        public void BrandCallbacksExposeStrongTypesAndDedicatedCryptoPath()
        {
            var exchange = JsonConvert
                .DeserializeObject<BrandMemberCardPointExchangeNotifyJson>(
                    "{\"record_id\":\"record_1\"," +
                    "\"brand_id\":\"brand_1\"," +
                    "\"card_id\":\"card_1\"," +
                    "\"appid\":\"wx123\"," +
                    "\"openid\":\"openid_1\"," +
                    "\"user_card_code\":\"code_1\"," +
                    "\"exchange_coupon_template_id\":\"template_1\"," +
                    "\"deduct_points\":18446744073709551615," +
                    "\"product_coupon_id\":\"coupon_1\"," +
                    "\"product_coupon_stock_type\":" +
                    "\"PRODUCT_COUPON_STOCK_TYPE_BUSI_FAVOR\"," +
                    "\"stock_id\":\"stock_1\"}");
            var decryptMethod = typeof(TenPayNotifyHandler).GetMethods(
                    BindingFlags.Public | BindingFlags.Instance)
                .Single(method => method.Name ==
                    nameof(TenPayNotifyHandler.DecryptBrandGetObjectAsync));
            var calledMethods = GetCalledMethodNames(decryptMethod).ToArray();
            var extensionNames = typeof(
                    BrandMemberCardNotifyHandlerExtensions)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name)
                .ToArray();

            Assert.AreEqual(ulong.MaxValue, exchange.deduct_points);
            Assert.AreEqual("PRODUCT_COUPON_STOCK_TYPE_BUSI_FAVOR",
                exchange.product_coupon_stock_type);
            Assert.IsTrue(typeof(BrandMemberCardUserCardResultJson)
                .IsAssignableFrom(
                    typeof(BrandMemberCardUserCardNotifyJson)));
            CollectionAssert.Contains(calledMethods,
                "AesGcmDecryptCiphertext");
            CollectionAssert.Contains(calledMethods,
                "VerifyTenpaySign");
            CollectionAssert.Contains(extensionNames,
                nameof(BrandMemberCardNotifyHandlerExtensions
                    .DecryptBrandMemberCardUserCardNotifyAsync));
            CollectionAssert.Contains(extensionNames,
                nameof(BrandMemberCardNotifyHandlerExtensions
                    .DecryptBrandMemberCardPointExchangeNotifyAsync));
            CollectionAssert.Contains(extensionNames,
                nameof(BrandMemberCardNotifyHandlerExtensions
                    .DecryptBrandMemberCardPointSyncNotifyAsync));
            Assert.AreEqual("BRAND_MEMBER_CARD.USER_CARD.CREATE",
                BrandMemberCardNotifyEventTypes.UserCardCreate);
            Assert.AreEqual("point_coupon",
                BrandMemberCardNotifyEventTypes.PointCouponOriginalType);
        }

        [TestMethod]
        public async Task BrandCallbackDecryptsAndVerifiesWithDedicatedKeys()
        {
            const string brandApiKey =
                "12345678901234567890123456789012";
            const string resourceNonce = "0123456789ab";
            const string associatedData = "point_coupon";
            const string timestamp = "1784908800";
            const string headerNonce = "notify-nonce-20260725";
            const string publicKeyId = "PUB_KEY_ID_20260725";
            const string decryptedJson =
                "{\"record_id\":\"record_1\"," +
                "\"brand_id\":\"brand_1\"," +
                "\"card_id\":\"card_1\"," +
                "\"appid\":\"wx123\"," +
                "\"openid\":\"openid_1\"," +
                "\"user_card_code\":\"code_1\"," +
                "\"exchange_coupon_template_id\":\"template_1\"," +
                "\"deduct_points\":5000000000}";
            var ciphertext = EncryptAesGcm(brandApiKey, resourceNonce,
                associatedData, decryptedJson);
            var body = "{\"id\":\"EV-202607250001\"," +
                "\"create_time\":\"2026-07-25T12:00:00+08:00\"," +
                "\"event_type\":\"BRAND_MEMBER_CARD.POINT_EXCHANGE_COUPON\"," +
                "\"resource_type\":\"encrypt-resource\"," +
                "\"summary\":\"积分兑券\",\"resource\":{" +
                "\"original_type\":\"point_coupon\"," +
                "\"algorithm\":\"AEAD_AES_256_GCM\"," +
                "\"ciphertext\":\"" + ciphertext + "\"," +
                "\"associated_data\":\"" + associatedData + "\"," +
                "\"nonce\":\"" + resourceNonce + "\"}}";

            using var rsa = RSA.Create(2048);
            var signature = Convert.ToBase64String(rsa.SignData(
                Encoding.UTF8.GetBytes(
                    $"{timestamp}\n{headerNonce}\n{body}\n"),
                HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            var context = CreateNotifyContext(body, timestamp, headerNonce,
                signature, publicKeyId);
            var handler = await TenPayNotifyHandler.CreateAsync(context,
                new SenparcWeixinSettingItem
                {
                    EncryptionType = CertType.RSA
                });
            var credentials = new TenPayBrandApiCredentials("brand_1",
                "brand_serial_1", rsa.ExportPkcs8PrivateKeyPem(),
                publicKeyId, rsa.ExportSubjectPublicKeyInfoPem());

            var result = await handler
                .DecryptBrandMemberCardPointExchangeNotifyAsync(
                    brandApiKey, credentials);

            Assert.AreEqual("record_1", result.record_id);
            Assert.AreEqual(5000000000UL, result.deduct_points);
            Assert.IsTrue(result.VerifySignSuccess);
        }

        private static string EncryptAesGcm(string key, string nonce,
            string associatedData, string plaintext)
        {
            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            var ciphertext = new byte[plaintextBytes.Length];
            var tag = new byte[16];
            using (var aes = new AesGcm(Encoding.UTF8.GetBytes(key),
                tag.Length))
            {
                aes.Encrypt(Encoding.UTF8.GetBytes(nonce), plaintextBytes,
                    ciphertext, tag, Encoding.UTF8.GetBytes(associatedData));
            }

            var encrypted = new byte[ciphertext.Length + tag.Length];
            Buffer.BlockCopy(ciphertext, 0, encrypted, 0,
                ciphertext.Length);
            Buffer.BlockCopy(tag, 0, encrypted, ciphertext.Length,
                tag.Length);
            return Convert.ToBase64String(encrypted);
        }

        private static DefaultHttpContext CreateNotifyContext(string body,
            string timestamp, string nonce, string signature, string serial)
        {
            var bytes = Encoding.UTF8.GetBytes(body);
            var context = new DefaultHttpContext();
            context.Request.Method = "POST";
            context.Request.ContentLength = bytes.Length;
            context.Request.ContentType = "application/json";
            context.Request.Body = new MemoryStream(bytes);
            context.Request.Headers["Wechatpay-Timestamp"] = timestamp;
            context.Request.Headers["Wechatpay-Nonce"] = nonce;
            context.Request.Headers["Wechatpay-Signature"] = signature;
            context.Request.Headers["Wechatpay-Serial"] = serial;
            return context;
        }

        private static IEnumerable<string> GetStringLiterals(MethodInfo method)
        {
            var bytes = method.GetMethodBody()?.GetILAsByteArray();
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
                    value = method.Module.ResolveString(
                        BitConverter.ToInt32(bytes, index + 1));
                }
                catch (ArgumentException)
                {
                    continue;
                }

                yield return value;
            }
        }

        private static IEnumerable<string> GetCalledMethodNames(
            MethodInfo method)
        {
            var bytes = method.GetMethodBody()?.GetILAsByteArray();
            if (bytes == null)
            {
                yield break;
            }

            for (var index = 0; index <= bytes.Length - 5; index++)
            {
                if (bytes[index] != 0x28 && bytes[index] != 0x6F)
                {
                    continue;
                }

                MethodBase calledMethod;
                try
                {
                    calledMethod = method.Module.ResolveMethod(
                        BitConverter.ToInt32(bytes, index + 1),
                        method.DeclaringType?.GetGenericArguments(),
                        method.GetGenericArguments());
                }
                catch (Exception exception) when (
                    exception is ArgumentException ||
                    exception is BadImageFormatException)
                {
                    continue;
                }

                yield return calledMethod.Name;
            }
        }
    }
}
