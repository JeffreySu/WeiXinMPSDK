using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.DeliveryPlan;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Test.Apis.DeliveryPlan
{
    [TestClass]
    public class DeliveryPlanContractTests
    {
        private static readonly IReadOnlyDictionary<string, string>
            OfficialEndpoints = new Dictionary<string, string>
            {
                [nameof(DeliveryPlanApis.CreateDeliveryPlanAsync)] =
                    "v3/marketing/partner/delivery-plan/delivery-plans",
                [nameof(DeliveryPlanApis.QueryDeliveryPlansAsync)] =
                    "v3/marketing/partner/delivery-plan/delivery-plans/",
                [nameof(DeliveryPlanApis.UpdateDeliveryPlanAsync)] =
                    "v3/marketing/partner/delivery-plan/delivery-plans/",
                [nameof(DeliveryPlanApis.TerminateDeliveryPlanAsync)] =
                    "/terminate",
                [nameof(DeliveryPlanApis.SetDeliveryPlanNotifyUrlAsync)] =
                    "/notify-url"
            };

        [TestMethod]
        public void ApiSurfaceContainsAllFiveOfficialEntries()
        {
            var methods = typeof(DeliveryPlanApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.DeclaringType == typeof(DeliveryPlanApis))
                .Select(method => method.Name)
                .Distinct()
                .ToArray();

            Assert.AreEqual(5, OfficialEndpoints.Count);
            CollectionAssert.AreEquivalent(OfficialEndpoints.Keys.ToArray(),
                methods);
        }

        [TestMethod]
        public void EveryMethodContainsCurrentOfficialEndpoint()
        {
            foreach (var endpoint in OfficialEndpoints)
            {
                var method = typeof(DeliveryPlanApis).GetMethod(endpoint.Key,
                    BindingFlags.Public | BindingFlags.Instance);

                Assert.IsNotNull(method, endpoint.Key);
                Assert.IsTrue(GetStringLiterals(method)
                        .Any(value => value.Contains(endpoint.Value)),
                    $"{endpoint.Key}: {endpoint.Value}");
            }
        }

        [TestMethod]
        public void QueryAndPathValuesAreEncodedAndNullValuesAreSkipped()
        {
            var buildQuery = typeof(DeliveryPlanApis).GetMethod(
                "BuildDeliveryPlanQuery",
                BindingFlags.NonPublic | BindingFlags.Static);
            var escape = typeof(DeliveryPlanApis).GetMethod(
                "EscapeDeliveryPlanValue",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(buildQuery);
            Assert.IsNotNull(escape);
            var path = (string)buildQuery.Invoke(null, new object[]
            {
                "v3/marketing/partner/delivery-plan/delivery-plans/brand%20id/delivery-plans",
                new[]
                {
                    "page_size", "50",
                    "offset", "0",
                    "plan_state", "DELIVERING",
                    "audit_state", "AUDIT PASSED",
                    "optional", null,
                    "plan_id", "plan+1"
                }
            });

            Assert.AreEqual(
                "v3/marketing/partner/delivery-plan/delivery-plans/brand%20id/delivery-plans?page_size=50&offset=0&plan_state=DELIVERING&audit_state=AUDIT%20PASSED&plan_id=plan%2B1",
                path);
            Assert.AreEqual("brand%20%2B%20id",
                escape.Invoke(null, new object[] { "brand + id" }));
        }

        [TestMethod]
        public void CreateAndUpdateRequestsPreserveOfficialFieldsAndLargeCounts()
        {
            var create = JObject.Parse(JsonConvert.SerializeObject(
                new DeliveryPlanCreateRequestData
                {
                    out_request_no = "delivery-plan_10001",
                    brand_id = "123456",
                    product_coupon_id = "coupon_10001",
                    reuse_coupon_config = false,
                    plan_name = "暑期多次优惠",
                    total_count = 5000000000L,
                    user_limit = 20,
                    daily_limit = 2,
                    delivery_start_time = "2026-08-01T00:00:00+08:00",
                    delivery_end_time = "2026-08-31T23:59:59+08:00",
                    recommend_word = "摇一摇领优惠",
                    usage_mode = "PROGRESSIVE_BUNDLE",
                    stock_bundle_id = "bundle_10001"
                }, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));
            var update = JObject.FromObject(new DeliveryPlanUpdateRequestData
            {
                out_request_no = "delivery-plan-update_10001",
                modify_content = new DeliveryPlanModifyContent
                {
                    delivery_end_time = "2026-09-30T23:59:59+08:00",
                    total_count = 6000000000L,
                    recommend_word = "加量优惠"
                }
            });

            Assert.AreEqual(5000000000L,
                create["total_count"]?.Value<long>());
            Assert.AreEqual("PROGRESSIVE_BUNDLE",
                create["usage_mode"]?.Value<string>());
            Assert.AreEqual("bundle_10001",
                create["stock_bundle_id"]?.Value<string>());
            Assert.IsNull(create["stock_id"]);
            Assert.AreEqual(6000000000L,
                update["modify_content"]?["total_count"]?.Value<long>());
            Assert.AreEqual("加量优惠",
                update["modify_content"]?["recommend_word"]?.Value<string>());
        }

        [TestMethod]
        public void UpdateUsesPatchAndTerminateUsesPostWithoutBody()
        {
            var update = typeof(DeliveryPlanApis).GetMethod(
                nameof(DeliveryPlanApis.UpdateDeliveryPlanAsync));
            var terminate = typeof(DeliveryPlanApis).GetMethod(
                nameof(DeliveryPlanApis.TerminateDeliveryPlanAsync));

            Assert.IsNotNull(update);
            Assert.IsNotNull(terminate);
            CollectionAssert.Contains(GetCalledMethodNames(update).ToArray(),
                "PatchAsync");
            CollectionAssert.Contains(GetCalledMethodNames(terminate).ToArray(),
                "PostWithoutBodyAsync");
        }

        [TestMethod]
        public void ResultAndNotificationModelsPreserveOfficialContracts()
        {
            var list = JsonConvert
                .DeserializeObject<DeliveryPlanListResultJson>(
                    "{\"total_count\":5000000000,\"plan_list\":[{" +
                    "\"plan_id\":\"plan_1\",\"plan_state\":\"DELIVERING\"," +
                    "\"audit_state\":\"AUDIT_PASSED\"," +
                    "\"usage_mode\":\"SINGLE\",\"stock_id\":\"stock_1\"," +
                    "\"brand_id\":\"123456\",\"total_count\":5000000000," +
                    "\"user_limit\":10,\"daily_limit\":1," +
                    "\"reuse_coupon_config\":true}]}");
            var notification = JsonConvert
                .DeserializeObject<DeliveryPlanNotifyJson>(
                    "{\"plan_id\":\"plan_1\",\"plan_state\":\"PAUSED\"," +
                    "\"audit_state\":\"REJECTED\"," +
                    "\"change_reason\":\"审核资料需补充\"," +
                    "\"modify_time\":\"2026-07-25T12:00:00+08:00\"}");
            var extension = typeof(DeliveryPlanNotifyHandlerExtensions)
                .GetMethod("DecryptDeliveryPlanNotifyAsync",
                    BindingFlags.Public | BindingFlags.Static);

            Assert.AreEqual(5000000000L, list.total_count);
            Assert.AreEqual("DELIVERING", list.plan_list[0].plan_state);
            Assert.AreEqual("PAUSED", notification.plan_state);
            Assert.AreEqual("REJECTED", notification.audit_state);
            Assert.AreEqual("DELIVERY_PLAN.CHANGE",
                DeliveryPlanNotifyConstants.EventType);
            Assert.AreEqual("delivery_plan",
                DeliveryPlanNotifyConstants.OriginalType);
            Assert.IsNotNull(extension);
            Assert.AreEqual(typeof(Task<DeliveryPlanNotifyJson>),
                extension.ReturnType);
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(DeliveryPlanNotifyJson)));
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
