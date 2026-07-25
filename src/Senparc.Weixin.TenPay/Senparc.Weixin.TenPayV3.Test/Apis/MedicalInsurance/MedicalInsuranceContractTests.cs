using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.MedicalInsurance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Senparc.Weixin.TenPayV3.Test.Apis.MedicalInsurance
{
    [TestClass]
    public class MedicalInsuranceContractTests
    {
        [TestMethod]
        public void ApiSurfaceCoversOfficialReusableCapabilities()
        {
            var methods = typeof(MedicalInsuranceApis).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.DeclaringType == typeof(MedicalInsuranceApis))
                .Select(method => method.Name)
                .ToArray();

            CollectionAssert.AreEquivalent(new[]
            {
                nameof(MedicalInsuranceApis.CreateOrderAsync),
                nameof(MedicalInsuranceApis.QueryOrderByMixTradeNoAsync),
                nameof(MedicalInsuranceApis.QueryOrderByOutTradeNoAsync),
                nameof(MedicalInsuranceApis.NotifyRefundSuccessAsync),
                nameof(MedicalInsuranceApis.CreateMiniProgramPayPackage),
                nameof(MedicalInsuranceApis.CreateJsApiPayPackage)
            }, methods);

            Assert.IsNotNull(typeof(TenPayNotifyHandlerExtensions).GetMethod(
                nameof(TenPayNotifyHandlerExtensions.DecryptMedicalInsurancePayNotifyAsync)));
        }

        [TestMethod]
        public void ServerMethodsContainOfficialEndpointFragments()
        {
            AssertMethodContainsLiteral(nameof(MedicalInsuranceApis.CreateOrderAsync), "v3/med-ins/orders");
            AssertMethodContainsLiteral(nameof(MedicalInsuranceApis.QueryOrderByMixTradeNoAsync),
                "v3/med-ins/orders/mix-trade-no/");
            AssertMethodContainsLiteral(nameof(MedicalInsuranceApis.QueryOrderByOutTradeNoAsync),
                "v3/med-ins/orders/out-trade-no/");
            AssertMethodContainsLiteral(nameof(MedicalInsuranceApis.NotifyRefundSuccessAsync),
                "v3/med-ins/refunds/notify");
        }

        [TestMethod]
        public void QueryHelpersEncodeIdentifiersAndOptionalSubMerchant()
        {
            var escape = typeof(MedicalInsuranceApis).GetMethod("Escape",
                BindingFlags.NonPublic | BindingFlags.Static);
            var buildQuery = typeof(MedicalInsuranceApis).GetMethod("BuildSubMchIdQuery",
                BindingFlags.NonPublic | BindingFlags.Static);

            Assert.IsNotNull(escape);
            Assert.IsNotNull(buildQuery);
            Assert.AreEqual("mix%20%26%20no", escape.Invoke(null, new object[] { "mix & no" }));
            Assert.AreEqual(string.Empty, buildQuery.Invoke(null, new object[] { null }));
            Assert.AreEqual("?sub_mchid=sub%2B1",
                buildQuery.Invoke(null, new object[] { "sub+1" }));
        }

        [TestMethod]
        public void RequestSerializationPreservesServiceModeAndOmitsUnusedFields()
        {
            var request = new MedicalInsuranceOrderRequestData
            {
                mix_pay_type = "CASH_AND_INSURANCE",
                order_type = "REG_PAY",
                appid = "wx-sp",
                sub_appid = "wx-sub",
                sub_mchid = "1900000109",
                sub_openid = "openid-sub",
                total_fee = 5178368698,
                med_ins_gov_fee = 100,
                callback_url = "https://example.com/notify"
            };

            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            StringAssert.Contains(json, "\"sub_mchid\":\"1900000109\"");
            StringAssert.Contains(json, "\"total_fee\":5178368698");
            Assert.IsFalse(json.Contains("\"openid\""));
            Assert.IsFalse(json.Contains("\"relative\""));
        }

        [TestMethod]
        public void ResponseModelPreservesOfficialLongAmountsAndStringStates()
        {
            var result = JsonConvert.DeserializeObject<MedicalInsuranceOrderResultJson>(
                "{\"mix_trade_no\":\"mix-1\",\"mix_pay_status\":\"MIX_PAY_SUCCESS\"," +
                "\"total_fee\":5178368698,\"med_ins_gov_fee\":4294967296}");

            Assert.AreEqual("MIX_PAY_SUCCESS", result.mix_pay_status);
            Assert.AreEqual(5178368698L, result.total_fee);
            Assert.AreEqual(4294967296L, result.med_ins_gov_fee);
        }

        [TestMethod]
        public void RefundBodyDoesNotDuplicateQueryParameters()
        {
            var properties = typeof(MedicalInsuranceRefundNotifyRequestData).GetProperties()
                .Select(property => property.Name)
                .ToArray();

            Assert.IsFalse(properties.Contains("mix_trade_no"));
            Assert.IsFalse(properties.Contains("sub_mchid"));
            CollectionAssert.Contains(properties, "med_refund_total_fee");
            CollectionAssert.Contains(properties, "out_refund_no");
        }

        private static void AssertMethodContainsLiteral(string methodName, string expected)
        {
            var method = typeof(MedicalInsuranceApis).GetMethod(methodName,
                BindingFlags.Public | BindingFlags.Instance);
            Assert.IsNotNull(method, methodName);

            var stateMachine = method.GetCustomAttribute<AsyncStateMachineAttribute>()?.StateMachineType;
            var inspectedMethod = stateMachine?.GetMethod("MoveNext",
                                      BindingFlags.NonPublic | BindingFlags.Instance) ?? method;
            Assert.IsTrue(GetStringLiterals(inspectedMethod).Any(value => value.Contains(expected)),
                $"{methodName}: {expected}");
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
                    value = method.Module.ResolveString(BitConverter.ToInt32(bytes, index + 1));
                }
                catch (ArgumentException)
                {
                    continue;
                }

                yield return value;
            }
        }
    }
}
