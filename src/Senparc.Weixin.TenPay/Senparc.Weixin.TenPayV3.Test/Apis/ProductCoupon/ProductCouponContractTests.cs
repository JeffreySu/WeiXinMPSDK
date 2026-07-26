using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.Marketing;
using Senparc.Weixin.TenPayV3.Apis.ProductCoupon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Senparc.Weixin.TenPayV3.Test.Apis.ProductCoupon
{
    [TestClass]
    public class ProductCouponContractTests
    {
        private static readonly string[] OfficialRequestMethods =
        {
            nameof(ProductCouponApis.CreateProductCouponAsync),
            nameof(ProductCouponApis.ModifyProductCouponAsync),
            nameof(ProductCouponApis.QueryProductCouponAsync),
            nameof(ProductCouponApis.DeactivateProductCouponAsync),
            nameof(ProductCouponApis.CreateStockAsync),
            nameof(ProductCouponApis.QueryStocksAsync),
            nameof(ProductCouponApis.QueryStockAsync),
            nameof(ProductCouponApis.ModifyStockAsync),
            nameof(ProductCouponApis.UpdateStockBudgetAsync),
            nameof(ProductCouponApis.DeactivateStockAsync),
            nameof(ProductCouponApis.AssociateStoresAsync),
            nameof(ProductCouponApis.QueryAssociatedStoresAsync),
            nameof(ProductCouponApis.DisassociateStoresAsync),
            nameof(ProductCouponApis.UploadCouponCodesAsync),
            nameof(ProductCouponApis.SendCouponAsync),
            nameof(ProductCouponApis.ConfirmCouponAsync),
            nameof(ProductCouponApis.PreSendCouponAsync),
            nameof(ProductCouponApis.UseCouponAsync),
            nameof(ProductCouponApis.QueryUserCouponAsync),
            nameof(ProductCouponApis.QueryUserCouponsAsync),
            nameof(ProductCouponApis.DeactivateUserCouponAsync),
            nameof(ProductCouponApis.ReturnUserCouponAsync),
            nameof(ProductCouponApis.QueryNotifyConfigAsync),
            nameof(ProductCouponApis.SetNotifyConfigAsync),
            nameof(ProductCouponApis.CreateImageGenerationTaskAsync),
            nameof(ProductCouponApis.QueryImageGenerationTaskAsync),
            nameof(ProductCouponApis.UploadImageAsync)
        };

        [TestMethod]
        public void ApiSurfaceKeepsAllTwentySevenSingleCouponRequests()
        {
            var methods = typeof(ProductCouponApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.DeclaringType ==
                    typeof(ProductCouponApis))
                .Select(method => method.Name)
                .Distinct()
                .ToArray();

            Assert.AreEqual(27, OfficialRequestMethods.Length);
            foreach (var expected in OfficialRequestMethods)
            {
                CollectionAssert.Contains(methods, expected);
            }
        }

        [TestMethod]
        public void RootAndComposedPathsMatchCurrentPartnerContract()
        {
            var root = typeof(ProductCouponApis).GetField("Root",
                BindingFlags.NonPublic | BindingFlags.Static);
            var productPath = typeof(ProductCouponApis).GetMethod(
                "ProductCouponPath",
                BindingFlags.NonPublic | BindingFlags.Static);
            var stockPath = typeof(ProductCouponApis).GetMethod(
                "StockPath", BindingFlags.NonPublic | BindingFlags.Static);
            var userPath = typeof(ProductCouponApis).GetMethod(
                "UserCouponPath",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.AreEqual("v3/marketing/partner/product-coupon",
                root.GetRawConstantValue());
            Assert.AreEqual(
                "v3/marketing/partner/product-coupon/product-coupons/" +
                "coupon%2F1",
                productPath.Invoke(null, new object[] { "coupon/1" }));
            Assert.AreEqual(
                "v3/marketing/partner/product-coupon/product-coupons/" +
                "coupon%2F1/stocks/stock%201",
                stockPath.Invoke(null,
                    new object[] { "coupon/1", "stock 1" }));
            Assert.AreEqual(
                "v3/marketing/partner/product-coupon/users/open%2Fid/" +
                "coupons/code%3F1",
                userPath.Invoke(null,
                    new object[] { "open/id", "code?1" }));
        }

        [TestMethod]
        public void QueryBuilderEncodesValuesAndSkipsNulls()
        {
            var buildQuery = typeof(ProductCouponApis).GetMethod(
                "BuildQuery", BindingFlags.NonPublic |
                              BindingFlags.Static);
            var path = (string)buildQuery.Invoke(null, new object[]
            {
                "v3/example",
                new[]
                {
                    "brand_id", "brand 1+2",
                    "page_token", "next/token?x=1",
                    "optional", null
                }
            });

            Assert.AreEqual(
                "v3/example?brand_id=brand%201%2B2&" +
                "page_token=next%2Ftoken%3Fx%3D1", path);
        }

        [TestMethod]
        public void CreateRequestSerializesCurrentNestedContract()
        {
            var request = new ProductCouponCreateRequestData
            {
                out_request_no = "request_1",
                scope = "SINGLE",
                type = "EXCHANGE",
                usage_mode = "SINGLE",
                single_usage_info = new ProductCouponSingleUsageInfo(),
                display_info = new ProductCouponDisplayInfo
                {
                    name = "商品兑换券",
                    combo_package_list = new[]
                    {
                        new ProductCouponComboPackage
                        {
                            name = "任选套餐",
                            pick_count = 1,
                            choice_list = new[]
                            {
                                new ProductCouponChoice
                                {
                                    name = "商品 A",
                                    price = 5000000000L,
                                    count = 1
                                }
                            }
                        }
                    }
                },
                stock = new ProductCouponStockCreateInfo
                {
                    coupon_code_mode = "UPLOAD",
                    stock_send_rule = new ProductCouponStockSendRule
                    {
                        max_count = 5000000001L,
                        max_count_per_user = 1
                    },
                    single_usage_rule = new ProductCouponSingleUsageRule
                    {
                        exchange_coupon = new ProductCouponExchangeRule
                        {
                            threshold = 0,
                            exchange_price = 1
                        }
                    }
                },
                brand_id = "brand_1"
            };

            var json = JObject.Parse(JsonConvert.SerializeObject(request));
            Assert.AreEqual("EXCHANGE", json["type"]?.Value<string>());
            Assert.AreEqual(5000000000L,
                json["display_info"]?["combo_package_list"]?[0]?
                    ["choice_list"]?[0]?["price"]?.Value<long>());
            Assert.AreEqual(5000000001L,
                json["stock"]?["stock_send_rule"]?["max_count"]?
                    .Value<long>());
            Assert.AreEqual("brand_1",
                json["brand_id"]?.Value<string>());
        }

        [TestMethod]
        public void UserCouponResultPreservesLongAmountsAndOrderFields()
        {
            var result = JsonConvert.DeserializeObject<
                ProductCouponUserCouponResultJson>(
                "{\"coupon_code\":\"code_1\"," +
                "\"coupon_state\":\"USED\"," +
                "\"single_usage_detail\":{" +
                "\"use_request_no\":\"use_1\"," +
                "\"associated_order_info\":{" +
                "\"transaction_id\":\"transaction_1\"}}," +
                "\"stock\":{\"stock_send_rule\":{" +
                "\"max_count\":5000000000," +
                "\"max_count_per_user\":1}}}");

            Assert.AreEqual("transaction_1",
                result.single_usage_detail.associated_order_info
                    .transaction_id);
            Assert.AreEqual(5000000000L,
                result.stock.stock_send_rule.max_count);
        }

        [TestMethod]
        public void NotificationsExposeBothCurrentDecryptionEntrypoints()
        {
            var methods = typeof(ProductCouponNotifyHandlerExtensions)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name)
                .ToArray();
            var notify = JsonConvert.DeserializeObject<
                ProductCouponSendNotifyJson>(
                "{\"brand_id\":\"brand_1\"," +
                "\"coupon_code\":\"code_1\"," +
                "\"phone_number\":\"13212345678\"}");

            CollectionAssert.Contains(methods,
                nameof(ProductCouponNotifyHandlerExtensions
                    .DecryptProductCouponSendNotifyAsync));
            CollectionAssert.Contains(methods,
                nameof(ProductCouponNotifyHandlerExtensions
                    .DecryptProductCouponImageGenerationNotifyAsync));
            Assert.AreEqual("PRODUCT_COUPON_SP.SEND",
                ProductCouponNotifyEventTypes.Send);
            Assert.AreEqual("code_1", notify.coupon_code);
        }

        [TestMethod]
        public void ImageUploadValidatesExtensionAndUsesBoundedMultipart()
        {
            var validate = typeof(ProductCouponApis).GetMethod(
                "ValidateImageFileName",
                BindingFlags.NonPublic | BindingFlags.Static);
            validate.Invoke(null, new object[] { "coupon.png" });
            var exception = Assert.ThrowsException<
                TargetInvocationException>(() =>
                validate.Invoke(null, new object[] { "coupon.gif" }));

            Assert.IsInstanceOfType(exception.InnerException,
                typeof(ArgumentException));
            Assert.IsNotNull(typeof(TenPayApiRequest).GetMethod(
                "RequestMultipartWithMaxSizeAsync",
                BindingFlags.NonPublic | BindingFlags.Instance));
        }

        [TestMethod]
        public void LegacyBusifavorSurfaceRemainsAvailable()
        {
            var methods = typeof(MarketingApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(method => method.Name)
                .ToArray();

            CollectionAssert.Contains(methods,
                nameof(MarketingApis.CreateBusifavorStockRequestDataAsync));
            CollectionAssert.Contains(methods,
                nameof(MarketingApis.UseBusifavorCouponAsync));
            CollectionAssert.Contains(methods,
                nameof(MarketingApis.ReturnBusifavorCouponAsync));
        }
    }
}
