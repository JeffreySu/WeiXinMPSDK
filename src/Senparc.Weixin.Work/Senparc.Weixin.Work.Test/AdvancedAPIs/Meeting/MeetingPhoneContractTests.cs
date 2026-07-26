using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Meeting;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Meeting
{
    [TestClass]
    public class MeetingPhoneContractTests
    {
        [TestMethod]
        public void PhoneApiContainsThreeSyncAndAsyncEntries()
        {
            var contracts = new[]
            {
                (nameof(MeetingApi.CalloutMeetingPhones), typeof(CalloutMeetingPhonesRequest),
                    typeof(CalloutMeetingPhonesResult)),
                (nameof(MeetingApi.GetMeetingPhoneCalloutStatus), typeof(GetMeetingPhoneCalloutStatusRequest),
                    typeof(GetMeetingPhoneCalloutStatusResult)),
                (nameof(MeetingApi.GetMeetingPhoneTempOpenIds), typeof(GetMeetingPhoneTempOpenIdsRequest),
                    typeof(GetMeetingPhoneTempOpenIdsResult))
            };

            foreach (var contract in contracts)
            {
                var parameterTypes = new[] { typeof(string), contract.Item2, typeof(int) };
                var syncMethod = typeof(MeetingApi).GetMethod(contract.Item1, parameterTypes);
                var asyncMethod = typeof(MeetingApi).GetMethod(contract.Item1 + "Async", parameterTypes);

                Assert.IsNotNull(syncMethod, contract.Item1);
                Assert.AreEqual(contract.Item3, syncMethod.ReturnType, contract.Item1);
                Assert.IsNotNull(asyncMethod, contract.Item1 + "Async");
                Assert.AreEqual(typeof(Task<>).MakeGenericType(contract.Item3), asyncMethod.ReturnType,
                    contract.Item1 + "Async");
            }
        }

        [TestMethod]
        public void PhoneApiUsesOfficialPathsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingApi.Phone.cs"));
            var paths = new[]
            {
                "/cgi-bin/meeting/phone/callout",
                "/cgi-bin/meeting/phone/get_callout_status",
                "/cgi-bin/meeting/phone/get_tmp_openid"
            };
            var documentIds = new[] { "98823", "98824", "98825" };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            foreach (var documentId in documentIds)
            {
                Assert.AreEqual(2, CountOccurrences(source, "/document/path/" + documentId), documentId);
            }

            Assert.AreEqual(6, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(6, CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(6, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(6, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(6, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(3, CountOccurrences(source, "=> Post<"));
            Assert.AreEqual(3, CountOccurrences(source, "=> PostAsync<"));
        }

        [TestMethod]
        public void PhoneRequestsPreserveNumbersExtensionsAndPaging()
        {
            var callout = new CalloutMeetingPhonesRequest
            {
                meetingid = "meeting-1",
                phone_numbers = new List<MeetingPhoneCalloutTarget>
                {
                    new MeetingPhoneCalloutTarget
                    {
                        area = "86", phone = "13800000000", extension_number = "1001"
                    }
                }
            };
            using var calloutDocument = JsonDocument.Parse(JsonSerializer.Serialize(callout));
            Assert.AreEqual("meeting-1", calloutDocument.RootElement.GetProperty("meetingid").GetString());
            Assert.AreEqual(JsonValueKind.String,
                calloutDocument.RootElement.GetProperty("phone_numbers")[0].GetProperty("area").ValueKind);
            Assert.AreEqual("1001", calloutDocument.RootElement.GetProperty("phone_numbers")[0]
                .GetProperty("extension_number").GetString());

            var status = new GetMeetingPhoneCalloutStatusRequest
            {
                meetingid = "meeting-1",
                cursor = "cursor-1",
                limit = 100
            };
            using var statusDocument = JsonDocument.Parse(JsonSerializer.Serialize(status));
            Assert.AreEqual("cursor-1", statusDocument.RootElement.GetProperty("cursor").GetString());
            Assert.AreEqual(100, statusDocument.RootElement.GetProperty("limit").GetInt32());

            var tempOpenIds = new GetMeetingPhoneTempOpenIdsRequest
            {
                meetingid = "meeting-1",
                phone_numbers = new List<MeetingPhoneCalloutTarget>
                {
                    new MeetingPhoneCalloutTarget { area = "1", phone = "2025550100" }
                }
            };
            using var tempOpenIdsDocument = JsonDocument.Parse(JsonSerializer.Serialize(tempOpenIds));
            Assert.AreEqual("1", tempOpenIdsDocument.RootElement.GetProperty("phone_numbers")[0]
                .GetProperty("area").GetString());
            Assert.AreEqual("2025550100", tempOpenIdsDocument.RootElement.GetProperty("phone_numbers")[0]
                .GetProperty("phone").GetString());
        }

        [TestMethod]
        public void PhoneResultsAcceptNumericAreaCodesAndPreserveStatuses()
        {
            var callout = JsonSerializer.Deserialize<CalloutMeetingPhonesResult>(
                "{\"errcode\":0,\"phone_numbers\":[{\"area\":86,\"phone\":\"13800000000\"," +
                "\"extension_number\":\"1001\",\"status\":\"calling\"}]," +
                "\"invalid_phone_numbers\":[{\"area\":1,\"phone\":\"invalid\"," +
                "\"status\":\"invalid_number\"}]}");
            var status = JsonSerializer.Deserialize<GetMeetingPhoneCalloutStatusResult>(
                "{\"errcode\":0,\"phone_numbers\":[{\"area\":86,\"phone\":\"13800000000\"," +
                "\"extension_number\":\"1001\",\"status\":\"connected\"," +
                "\"tmp_openid\":\"tmp-1\"}],\"has_more\":true," +
                "\"next_cursor\":\"cursor-2\"}");
            var tempOpenIds = JsonSerializer.Deserialize<GetMeetingPhoneTempOpenIdsResult>(
                "{\"errcode\":0,\"tmp_openid_list\":[{\"area\":86," +
                "\"phone\":\"13800000000\",\"extension_number\":\"1001\"," +
                "\"tmp_openid\":\"tmp-1\"}]}");

            Assert.IsNotNull(callout);
            Assert.AreEqual("86", callout.phone_numbers[0].area);
            Assert.AreEqual("calling", callout.phone_numbers[0].status);
            Assert.AreEqual("1", callout.invalid_phone_numbers[0].area);
            Assert.AreEqual("invalid_number", callout.invalid_phone_numbers[0].status);
            Assert.IsNotNull(status);
            Assert.AreEqual("connected", status.phone_numbers[0].status);
            Assert.AreEqual("tmp-1", status.phone_numbers[0].tmp_openid);
            Assert.IsTrue(status.has_more);
            Assert.AreEqual("cursor-2", status.next_cursor);
            Assert.IsNotNull(tempOpenIds);
            Assert.AreEqual("86", tempOpenIds.tmp_openid_list[0].area);
            Assert.AreEqual("1001", tempOpenIds.tmp_openid_list[0].extension_number);
            Assert.AreEqual("tmp-1", tempOpenIds.tmp_openid_list[0].tmp_openid);
        }

        [TestMethod]
        public void PhonePublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(MeetingPhoneCalloutTarget), typeof(MeetingPhoneCalloutItem),
                typeof(CalloutMeetingPhonesRequest), typeof(CalloutMeetingPhonesResult),
                typeof(GetMeetingPhoneCalloutStatusRequest), typeof(MeetingPhoneCalloutStatusItem),
                typeof(GetMeetingPhoneCalloutStatusResult), typeof(GetMeetingPhoneTempOpenIdsRequest),
                typeof(MeetingPhoneTempOpenIdItem), typeof(GetMeetingPhoneTempOpenIdsResult)
            };

            foreach (var property in modelTypes.SelectMany(type => type.GetProperties(BindingFlags.Public |
                         BindingFlags.Instance | BindingFlags.DeclaredOnly)))
            {
                Assert.AreNotEqual(typeof(object), property.PropertyType,
                    property.DeclaringType?.Name + "." + property.Name);
                if (property.PropertyType.IsGenericType)
                {
                    CollectionAssert.DoesNotContain(property.PropertyType.GetGenericArguments(), typeof(object));
                }
            }

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "Meeting", "MeetingPhoneJson.cs"));
            var declarationCount = source.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.TrimStart())
                .Count(line => line.StartsWith("public class ", StringComparison.Ordinal) ||
                               line.StartsWith("public ", StringComparison.Ordinal) &&
                               line.Contains("{ get; set; }", StringComparison.Ordinal));
            Assert.AreEqual(declarationCount, CountOccurrences(source, "/// <summary>"));
        }

        private static int CountOccurrences(string source, string value)
            => source.Split(new[] { value }, StringSplitOptions.None).Length - 1;

        private static string FindRepositoryRoot([CallerFilePath] string sourceFilePath = null)
        {
            var directory = new DirectoryInfo(Path.GetDirectoryName(sourceFilePath));
            while (directory != null)
            {
                if (Directory.Exists(Path.Combine(directory.FullName, ".git")))
                {
                    return directory.FullName;
                }

                directory = directory.Parent;
            }

            Assert.Fail("Unable to locate repository root.");
            return null;
        }
    }
}
