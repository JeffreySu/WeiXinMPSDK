using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.ChainBrandProfitsharing;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Apis.Profitsharing;
using Senparc.Weixin.TenPayV3.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Senparc.Weixin.TenPayV3.Test.Apis.ChainBrandProfitsharing
{
    [TestClass]
    public class ChainBrandProfitsharingContractTests
    {
        private static readonly IReadOnlyDictionary<string, string>
            OfficialEndpoints = new Dictionary<string, string>
            {
                [nameof(ChainBrandProfitsharingApis.CreateOrderAsync)] =
                    "v3/brand/profitsharing/orders",
                [nameof(ChainBrandProfitsharingApis.QueryOrderAsync)] =
                    "v3/brand/profitsharing/orders",
                [nameof(ChainBrandProfitsharingApis.CreateReturnOrderAsync)] =
                    "v3/brand/profitsharing/returnorders",
                [nameof(ChainBrandProfitsharingApis.QueryReturnOrderAsync)] =
                    "v3/brand/profitsharing/returnorders",
                [nameof(ChainBrandProfitsharingApis.FinishOrderAsync)] =
                    "v3/brand/profitsharing/finish-order",
                [nameof(ChainBrandProfitsharingApis.QueryAmountsAsync)] =
                    "v3/brand/profitsharing/orders/",
                [nameof(ChainBrandProfitsharingApis.QueryBrandConfigAsync)] =
                    "v3/brand/profitsharing/brand-configs/",
                [nameof(ChainBrandProfitsharingApis.AddReceiverAsync)] =
                    "v3/brand/profitsharing/receivers/add",
                [nameof(ChainBrandProfitsharingApis.DeleteReceiverAsync)] =
                    "v3/brand/profitsharing/receivers/delete",
                [nameof(ChainBrandProfitsharingApis.ApplyBillAsync)] =
                    "v3/profitsharing/bills"
            };

        [TestMethod]
        public void ApiSurfaceCoversAllCurrentDirectoryCapabilities()
        {
            var methods = typeof(ChainBrandProfitsharingApis)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.DeclaringType ==
                    typeof(ChainBrandProfitsharingApis))
                .Select(method => method.Name)
                .Distinct()
                .ToArray();

            Assert.AreEqual(10, OfficialEndpoints.Count);
            Assert.AreEqual(11, methods.Length);
            CollectionAssert.Contains(methods,
                nameof(ChainBrandProfitsharingApis.DownloadBillAsync));
            foreach (var methodName in OfficialEndpoints.Keys)
            {
                CollectionAssert.Contains(methods, methodName);
            }
        }

        [TestMethod]
        public void EveryRequestMethodContainsCurrentOfficialEndpoint()
        {
            foreach (var endpoint in OfficialEndpoints)
            {
                var methods = typeof(ChainBrandProfitsharingApis)
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(method => method.Name == endpoint.Key)
                    .ToArray();

                Assert.IsTrue(methods.Length > 0, endpoint.Key);
                Assert.IsTrue(methods.SelectMany(GetStringLiterals)
                        .Any(value => value.Contains(endpoint.Value)),
                    $"{endpoint.Key}: {endpoint.Value}");
            }
        }

        [TestMethod]
        public void QueryValuesAreEncodedAndOptionalValuesAreSkipped()
        {
            var buildQuery = typeof(ChainBrandProfitsharingApis).GetMethod(
                "BuildQuery", BindingFlags.NonPublic |
                              BindingFlags.Static);
            var escape = typeof(ChainBrandProfitsharingApis).GetMethod(
                "Escape", BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(buildQuery);
            Assert.IsNotNull(escape);
            var path = (string)buildQuery.Invoke(null, new object[]
            {
                "v3/brand/profitsharing/orders",
                new[]
                {
                    "sub_mchid", "1900 001+02",
                    "transaction_id", "transaction/1",
                    "optional", null,
                    "out_order_no", "order?1"
                }
            });

            Assert.AreEqual(
                "v3/brand/profitsharing/orders?" +
                "sub_mchid=1900%20001%2B02&" +
                "transaction_id=transaction%2F1&" +
                "out_order_no=order%3F1", path);
            Assert.AreEqual("id%2Fwith%20space",
                escape.Invoke(null, new object[] { "id/with space" }));
        }

        [TestMethod]
        public void CurrentModelsPreserveStatusLongAmountsAndReturnNumber()
        {
            var order = JsonConvert
                .DeserializeObject<ChainBrandProfitsharingOrderResultJson>(
                    "{\"brand_mchid\":\"brand_1\"," +
                    "\"sub_mchid\":\"sub_1\"," +
                    "\"transaction_id\":\"transaction_1\"," +
                    "\"out_order_no\":\"order_1\"," +
                    "\"order_id\":\"wx_order_1\"," +
                    "\"status\":\"FINISHED\"," +
                    "\"finish_amount\":5000000000," +
                    "\"finish_description\":\"complete\"," +
                    "\"receivers\":[{\"type\":\"MERCHANT_ID\"," +
                    "\"account\":\"receiver_1\"," +
                    "\"amount\":5000000001," +
                    "\"result\":\"SUCCESS\"," +
                    "\"detail_id\":\"detail_1\"}]}");
            var returnOrder = JsonConvert.DeserializeObject<
                ChainBrandProfitsharingReturnOrderResultJson>(
                    "{\"out_return_no\":\"return_1\"," +
                    "\"return_no\":\"wx_return_1\"," +
                    "\"amount\":5000000002," +
                    "\"result\":\"SUCCESS\"}");

            Assert.AreEqual("FINISHED", order.status);
            Assert.AreEqual(5000000000L, order.finish_amount);
            Assert.AreEqual(5000000001L, order.receivers[0].amount);
            Assert.AreEqual("wx_return_1", returnOrder.return_no);
            Assert.AreEqual(5000000002L, returnOrder.amount);
            Assert.IsTrue(typeof(ReturnJsonBase).IsAssignableFrom(
                typeof(ChainBrandProfitsharingOrderResultJson)));
        }

        [TestMethod]
        public void RequestModelsUseCurrentFieldsAndEncryptedNames()
        {
            var create = JObject.Parse(JsonConvert.SerializeObject(
                new ChainBrandProfitsharingCreateOrderRequestData
                {
                    brand_mchid = "brand_1",
                    sub_mchid = "sub_1",
                    appid = "wx_app",
                    sub_appid = "wx_sub_app",
                    transaction_id = "transaction_1",
                    out_order_no = "order_1",
                    finish = true,
                    receivers = new[]
                    {
                        new ChainBrandProfitsharingReceiverRequestData
                        {
                            type = "MERCHANT_ID",
                            account = "receiver_1",
                            name = "encrypted-name",
                            amount = 5000000000L,
                            description = "share"
                        }
                    }
                }));
            var receiverName = typeof(
                    ChainBrandProfitsharingReceiverRequestData)
                .GetProperty(nameof(
                    ChainBrandProfitsharingReceiverRequestData.name));
            var addName = typeof(
                    ChainBrandProfitsharingAddReceiverRequestData)
                .GetProperty(nameof(
                    ChainBrandProfitsharingAddReceiverRequestData.name));

            Assert.AreEqual(true, create["finish"]?.Value<bool>());
            Assert.IsNull(create["unfreeze_unsplit"]);
            Assert.AreEqual(5000000000L,
                create["receivers"]?[0]?["amount"]?.Value<long>());
            Assert.IsNotNull(receiverName.GetCustomAttribute<
                FieldEncryptAttribute>());
            Assert.IsNotNull(addName.GetCustomAttribute<
                FieldEncryptAttribute>());
        }

        [TestMethod]
        public void BillDownloadUsesStreamingVerificationHelper()
        {
            var overload = typeof(ChainBrandProfitsharingApis).GetMethods(
                    BindingFlags.Public | BindingFlags.Instance)
                .Single(method => method.Name ==
                    nameof(ChainBrandProfitsharingApis.DownloadBillAsync) &&
                    method.GetParameters().Length == 4);

            CollectionAssert.Contains(GetCalledMethodNames(overload).ToArray(),
                "DownloadAndVerifyAsync");
        }

        [TestMethod]
        public void NotifyModelAndExtensionUseCurrentPartnerFields()
        {
            var notify = JsonConvert.DeserializeObject<
                ChainBrandProfitsharingNotifyJson>(
                    "{\"sp_mchid\":\"sp_1\"," +
                    "\"sub_mchid\":\"sub_1\"," +
                    "\"transaction_id\":\"transaction_1\"," +
                    "\"order_id\":\"order_1\"," +
                    "\"out_order_no\":\"out_order_1\"," +
                    "\"receiver\":{\"type\":\"MERCHANT_ID\"," +
                    "\"account\":\"receiver_1\"," +
                    "\"amount\":5000000000," +
                    "\"description\":\"share\"}," +
                    "\"success_time\":\"2026-07-25T12:00:00+08:00\"}");
            var extension = typeof(
                    ChainBrandProfitsharingNotifyHandlerExtensions)
                .GetMethod(nameof(
                    ChainBrandProfitsharingNotifyHandlerExtensions
                        .DecryptChainBrandProfitsharingNotifyAsync));

            Assert.AreEqual("sp_1", notify.sp_mchid);
            Assert.AreEqual("sub_1", notify.sub_mchid);
            Assert.AreEqual(5000000000L, notify.receiver.amount);
            Assert.AreEqual("TRANSACTION.SUCCESS",
                ChainBrandProfitsharingNotifyEventTypes.TransactionSuccess);
            Assert.AreEqual("profitsharing",
                ChainBrandProfitsharingNotifyEventTypes.OriginalType);
            CollectionAssert.Contains(
                GetCalledMethodNames(extension).ToArray(),
                nameof(TenPayNotifyHandler.DecryptGetObjectAsync));
        }

        [TestMethod]
        public void LegacyProfitsharingSurfaceRemainsAvailable()
        {
            var legacyMethods = typeof(ProfitsharingApis).GetMethods(
                    BindingFlags.Public | BindingFlags.Instance)
                .Select(method => method.Name)
                .ToArray();

            CollectionAssert.Contains(legacyMethods,
                nameof(ProfitsharingApis.CreateProfitsharingAsync));
            CollectionAssert.Contains(legacyMethods,
                nameof(ProfitsharingApis.QueryProfitsharingAsync));
            CollectionAssert.Contains(legacyMethods,
                nameof(ProfitsharingApis.ReturnProfitsharingAsync));
            CollectionAssert.Contains(legacyMethods,
                nameof(ProfitsharingApis.FinishProfitsharingAsync));
            CollectionAssert.Contains(legacyMethods,
                nameof(ProfitsharingApis.QueryProfitsharingBillsAsync));
        }

        private static IEnumerable<string> GetStringLiterals(
            MethodInfo method)
        {
            var stateMachine = method.GetCustomAttribute<
                AsyncStateMachineAttribute>();
            if (stateMachine != null)
            {
                method = stateMachine.StateMachineType.GetMethod(
                    "MoveNext", BindingFlags.NonPublic |
                                BindingFlags.Public |
                                BindingFlags.Instance);
            }

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
