using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Apis.ParkingReminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Senparc.Weixin.TenPayV3.Test.Apis.ParkingReminder
{
    [TestClass]
    public class ParkingReminderContractTests
    {
        private static readonly IReadOnlyDictionary<string, string> OfficialEndpoints =
            new Dictionary<string, string>
            {
                [nameof(ParkingReminderApis.SubmitApplicationAsync)] = "v3/parking/reminders/application",
                [nameof(ParkingReminderApis.QueryApplicationAsync)] = "v3/parking/reminders/application/query",
                [nameof(ParkingReminderApis.QueryApplicationListAsync)] = "v3/parking/reminders/applications",
                [nameof(ParkingReminderApis.WithdrawApplicationAsync)] = "v3/parking/reminders/application/withdraw",
                [nameof(ParkingReminderApis.SyncEntryAsync)] = "v3/parking/reminders/entry",
                [nameof(ParkingReminderApis.SyncExitAsync)] = "v3/parking/reminders/exit",
                [nameof(ParkingReminderApis.SyncPaymentAsync)] = "v3/parking/reminders/payment",
                [nameof(ParkingReminderApis.SyncExtensionPaymentAsync)] = "v3/parking/reminders/ext-payment",
                [nameof(ParkingReminderApis.QueryParkingLotAsync)] = "v3/parking/reminders/parking-lot",
                [nameof(ParkingReminderApis.QueryParkingFeeAsync)] = "v3/parking/reminders/parking-fee"
            };

        [TestMethod]
        public void ApiSurfaceContainsTenOfficialEntries()
        {
            var methods = typeof(ParkingReminderApis).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.DeclaringType == typeof(ParkingReminderApis))
                .Select(method => method.Name)
                .ToArray();

            Assert.AreEqual(10, OfficialEndpoints.Count);
            CollectionAssert.AreEquivalent(OfficialEndpoints.Keys.ToArray(), methods);
        }

        [TestMethod]
        public void EveryEntryContainsOfficialEndpoint()
        {
            foreach (var endpoint in OfficialEndpoints)
            {
                var method = typeof(ParkingReminderApis).GetMethod(endpoint.Key,
                    BindingFlags.Public | BindingFlags.Instance);
                Assert.IsNotNull(method, endpoint.Key);
                Assert.IsTrue(GetStringLiterals(method).Contains(endpoint.Value),
                    $"{endpoint.Key}: {endpoint.Value}");
            }
        }

        [TestMethod]
        public void QueryBuilderEncodesValuesAndOmitsMissingParameters()
        {
            var buildQuery = typeof(ParkingReminderApis).GetMethod("BuildQuery",
                BindingFlags.NonPublic | BindingFlags.Static);
            Assert.IsNotNull(buildQuery);

            var query = (string)buildQuery.Invoke(null, new object[]
            {
                new Dictionary<string, object>
                {
                    ["out_parking_lot_id"] = "lot + 1",
                    ["wx_parking_lot_id"] = null,
                    ["offset"] = 2
                }
            });

            Assert.AreEqual("?out_parking_lot_id=lot%20%2B%201&offset=2", query);
        }

        [TestMethod]
        public void ApplicationSerializationPreservesNestedChargingRules()
        {
            var request = new ParkingLotApplicationRequestData
            {
                parking_lot = new ParkingLotApplicationData
                {
                    parking_lot_name = "测试停车场",
                    out_parking_lot_id = "lot-1",
                    payment_path = "pages/pay/index",
                    charging_rule = new ParkingChargingRule
                    {
                        rule_type = "FIXED_INTERVAL",
                        fixed_interval_rule = new List<ParkingFixedIntervalRule>
                        {
                            new ParkingFixedIntervalRule
                            {
                                day_type = "WORKDAY",
                                interval_amount = 5178368698
                            }
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            StringAssert.Contains(json, "\"parking_lot\"");
            StringAssert.Contains(json, "\"fixed_interval_rule\"");
            StringAssert.Contains(json, "\"interval_amount\":5178368698");
            Assert.IsFalse(json.Contains("\"parking_sign_url\""));
        }

        [TestMethod]
        public void NotificationModelsPreserveLongAmountsAndProtocolNames()
        {
            var payment = new ParkingPaymentRequestData
            {
                out_serial_number = "serial-1",
                parking_id = "parking-1",
                parking_state = "OUT",
                pay_type = "WECHAT_PAY",
                total_amount = 5178368698,
                paid_amount = 4294967296
            };
            var json = JsonConvert.SerializeObject(payment);

            StringAssert.Contains(json, "\"out_serial_number\":\"serial-1\"");
            StringAssert.Contains(json, "\"total_amount\":5178368698");
            StringAssert.Contains(json, "\"paid_amount\":4294967296");
        }

        [TestMethod]
        public void ResponseModelsPreserveApplicationAndFeeShapes()
        {
            var applications = JsonConvert.DeserializeObject<ParkingLotApplicationListResultJson>(
                "{\"application_list\":[{\"parking_lot_audit_no\":\"audit-1\"," +
                "\"submit_time\":5178368698,\"wx_parking_lot_id\":\"wx-lot\"}]}");
            var fee = JsonConvert.DeserializeObject<ParkingFeeResultJson>(
                "{\"total_amount\":5178368698,\"payable_amount\":4294967296," +
                "\"parking_state\":\"PARKING\"}");

            Assert.AreEqual("audit-1", applications.application_list[0].parking_lot_audit_no);
            Assert.AreEqual(5178368698L, applications.application_list[0].submit_time);
            Assert.AreEqual(4294967296L, fee.payable_amount);
            Assert.AreEqual("PARKING", fee.parking_state);
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
