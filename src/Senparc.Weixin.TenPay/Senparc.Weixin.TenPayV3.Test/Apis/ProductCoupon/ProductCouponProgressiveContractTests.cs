using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.ProductCoupon;
using System.Linq;
using System.Reflection;

namespace Senparc.Weixin.TenPayV3.Test.Apis.ProductCoupon
{
    [TestClass]
    public class ProductCouponProgressiveContractTests
    {
        private static readonly string[] UniqueRequestMethods =
        {
            nameof(ProductCouponApis.CreateStockBundleAsync),
            nameof(ProductCouponApis.ModifyStockBundleAsync),
            nameof(ProductCouponApis.UpdateStockBundleBudgetAsync),
            nameof(ProductCouponApis.AssociateStockBundleStoresAsync),
            nameof(ProductCouponApis.DisassociateStockBundleStoresAsync),
            nameof(ProductCouponApis.SendCouponBundleAsync),
            nameof(ProductCouponApis.PreSendCouponBundleAsync),
            nameof(ProductCouponApis.DeactivateUserCouponBundleAsync)
        };

        [TestMethod]
        public void ProgressiveSurfaceAddsEightUniqueRequests()
        {
            var methods = typeof(ProductCouponApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.DeclaringType ==
                    typeof(ProductCouponApis))
                .Select(method => method.Name)
                .Distinct()
                .ToArray();

            Assert.AreEqual(8, UniqueRequestMethods.Length);
            foreach (var expected in UniqueRequestMethods)
            {
                CollectionAssert.Contains(methods, expected);
            }
        }

        [TestMethod]
        public void ProgressivePathsEncodeIdentifiers()
        {
            var stockBundlePath = typeof(ProductCouponApis).GetMethod(
                "StockBundlePath",
                BindingFlags.NonPublic | BindingFlags.Static);
            var userCouponBundlePath = typeof(ProductCouponApis).GetMethod(
                "UserCouponBundlePath",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.AreEqual(
                "v3/marketing/partner/product-coupon/product-coupons/" +
                "coupon%2F1/stock-bundles/bundle%201",
                stockBundlePath.Invoke(null,
                    new object[] { "coupon/1", "bundle 1" }));
            Assert.AreEqual(
                "v3/marketing/partner/product-coupon/users/open%2Fid/" +
                "coupon-bundles/user%3Fbundle",
                userCouponBundlePath.Invoke(null,
                    new object[] { "open/id", "user?bundle" }));
        }

        [TestMethod]
        public void ProgressiveCreateRequestSerializesRuleLists()
        {
            var request = new ProductCouponCreateRequestData
            {
                usage_mode = "PROGRESSIVE_BUNDLE",
                progressive_bundle_usage_info =
                    new ProductCouponProgressiveBundleUsageInfo
                    {
                        count = 3,
                        interval_days = 1
                    },
                stock_bundle = new ProductCouponStockBundleCreateInfo
                {
                    coupon_code_mode = "UPLOAD",
                    stock_send_rule = new ProductCouponStockSendRule
                    {
                        max_count = 5000000001L,
                        max_count_per_user = 1
                    },
                    progressive_bundle_usage_rule =
                        new ProductCouponProgressiveBundleUsageRule
                        {
                            coupon_available_period =
                                new ProductCouponAvailablePeriod
                                {
                                    available_days = 7
                                },
                            normal_coupon_list = new[]
                            {
                                new ProductCouponNormalRule
                                {
                                    threshold = 100,
                                    discount_amount = 10
                                },
                                new ProductCouponNormalRule
                                {
                                    threshold = 100,
                                    discount_amount = 20
                                },
                                new ProductCouponNormalRule
                                {
                                    threshold = 100,
                                    discount_amount = 30
                                }
                            }
                        }
                },
                brand_id = "brand_1"
            };

            var json = JObject.Parse(JsonConvert.SerializeObject(request));
            Assert.AreEqual(3, json["progressive_bundle_usage_info"]?
                ["count"]?.Value<int>());
            Assert.AreEqual(3, json["stock_bundle"]?
                ["progressive_bundle_usage_rule"]?
                ["normal_coupon_list"]?.Count());
            Assert.AreEqual(5000000001L, json["stock_bundle"]?
                ["stock_send_rule"]?["max_count"]?.Value<long>());
        }

        [TestMethod]
        public void BundleRequestsUseOfficialIdentifiers()
        {
            var request = new ProductCouponSendBundleRequestData
            {
                product_coupon_id = "coupon_1",
                stock_bundle_id = "bundle_1",
                appid = "wx123",
                send_request_no = "send_1",
                brand_id = "brand_1"
            };
            var deactivate =
                new ProductCouponUserCouponBundleDeactivateRequestData
                {
                    product_coupon_id = "coupon_1",
                    stock_bundle_id = "bundle_1",
                    appid = "wx123",
                    out_request_no = "deactivate_1",
                    deactivate_reason = "活动结束",
                    brand_id = "brand_1"
                };

            var sendJson = JObject.Parse(
                JsonConvert.SerializeObject(request));
            var deactivateJson = JObject.Parse(
                JsonConvert.SerializeObject(deactivate));
            Assert.AreEqual("bundle_1",
                sendJson["stock_bundle_id"]?.Value<string>());
            Assert.AreEqual("deactivate_1",
                deactivateJson["out_request_no"]?.Value<string>());
        }

        [TestMethod]
        public void BundleResultsPreserveOrderedCouponAndStockLists()
        {
            var stockBundle = JsonConvert.DeserializeObject<
                ProductCouponStockBundleResultJson>(
                "{\"stock_bundle_id\":\"stock_bundle_1\"," +
                "\"stock_list\":[{\"stock_id\":\"stock_1\"}," +
                "{\"stock_id\":\"stock_2\"}]}");
            var userBundle = JsonConvert.DeserializeObject<
                ProductCouponUserCouponBundleResultJson>(
                "{\"user_coupon_bundle_id\":\"user_bundle_1\"," +
                "\"user_product_coupon_list\":[" +
                "{\"coupon_code\":\"code_1\"}]}");

            Assert.AreEqual("stock_2",
                stockBundle.stock_list[1].stock_id);
            Assert.AreEqual("code_1",
                userBundle.user_product_coupon_list[0].coupon_code);
        }

        [TestMethod]
        public void CreateResultDeserializesProgressiveBundle()
        {
            var result = JsonConvert.DeserializeObject<
                ProductCouponResultJson>(
                "{\"product_coupon_id\":\"coupon_1\"," +
                "\"usage_mode\":\"PROGRESSIVE_BUNDLE\"," +
                "\"progressive_bundle_usage_info\":{" +
                "\"count\":3,\"interval_days\":1}," +
                "\"stock_bundle\":{" +
                "\"stock_bundle_id\":\"bundle_1\"," +
                "\"stock_list\":[{\"stock_id\":\"stock_1\"}]}}");

            Assert.AreEqual(3,
                result.progressive_bundle_usage_info.count);
            Assert.AreEqual("bundle_1",
                result.stock_bundle.stock_bundle_id);
        }
    }
}
