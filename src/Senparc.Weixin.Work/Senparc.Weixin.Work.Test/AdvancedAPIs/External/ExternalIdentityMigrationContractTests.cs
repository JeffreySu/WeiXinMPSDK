using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.External;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.External
{
    [TestClass]
    public class ExternalIdentityMigrationContractTests
    {
        [TestMethod]
        public void ExternalIdentityApiContainsSixSyncAndAsyncEntries()
        {
            var requestContracts = new[]
            {
                (nameof(ExternalApi.ConvertExternalContactUnionId),
                    typeof(ExternalContactUnionIdConvertRequest),
                    typeof(ExternalContactUnionIdConvertResult)),
                (nameof(ExternalApi.BatchMobileToExternalUserId),
                    typeof(BatchMobileToExternalUserIdRequest),
                    typeof(BatchMobileToExternalUserIdResult)),
                (nameof(ExternalApi.ConvertToServiceExternalUserId),
                    typeof(ServiceExternalUserIdConvertRequest),
                    typeof(ServiceExternalUserIdConvertResult)),
                (nameof(ExternalApi.GetNewExternalUserId), typeof(NewExternalUserIdRequest),
                    typeof(NewExternalUserIdResult)),
                (nameof(ExternalApi.GetNewGroupChatExternalUserId),
                    typeof(NewGroupChatExternalUserIdRequest), typeof(NewExternalUserIdResult))
            };

            foreach (var contract in requestContracts)
            {
                AssertSyncAndAsyncContract(contract.Item1, contract.Item2, contract.Item3);
            }

            var parameterTypes = new[] { typeof(string), typeof(int) };
            var syncMethod = typeof(ExternalApi).GetMethod(
                nameof(ExternalApi.GetCustomerAcquisitionAppPermit), parameterTypes);
            var asyncMethod = typeof(ExternalApi).GetMethod(
                nameof(ExternalApi.GetCustomerAcquisitionAppPermitAsync), parameterTypes);
            Assert.IsNotNull(syncMethod);
            Assert.AreEqual(typeof(CustomerAcquisitionAppPermitResult), syncMethod.ReturnType);
            Assert.IsNotNull(asyncMethod);
            Assert.AreEqual(typeof(Task<CustomerAcquisitionAppPermitResult>), asyncMethod.ReturnType);
        }

        [TestMethod]
        public void ExternalIdentityApiUsesOfficialPathsAndCompleteXmlComments()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "External",
                "ExternalIdentityMigrationApi.cs"));
            var paths = new[]
            {
                "/cgi-bin/externalcontact/unionid_to_external_userid",
                "/cgi-bin/externalcontact/batch_to_external_userid",
                "/cgi-bin/externalcontact/to_service_external_userid",
                "/cgi-bin/externalcontact/customer_acquisition_app/get_permit",
                "/cgi-bin/externalcontact/get_new_external_userid",
                "/cgi-bin/externalcontact/groupchat/get_new_external_userid"
            };

            foreach (var path in paths)
            {
                Assert.AreEqual(1, CountOccurrences(source, "\"" + path + "\""), path);
            }

            Assert.AreEqual(2, CountOccurrences(source, "/document/path/93274"));
            Assert.AreEqual(2, CountOccurrences(source, "/document/path/92506"));
            Assert.AreEqual(2, CountOccurrences(source, "/document/path/95195"));
            Assert.AreEqual(2, CountOccurrences(source, "/document/path/101146"));
            Assert.AreEqual(4, CountOccurrences(source, "/document/path/95327"));
            Assert.AreEqual(4, CountOccurrences(source, "/document/path/95435"));
            Assert.AreEqual(13, CountOccurrences(source, "/// <summary>"));
            Assert.AreEqual(12,
                CountOccurrences(source, "/// <param name=\"accessTokenOrAppKey\">"));
            Assert.AreEqual(10, CountOccurrences(source, "/// <param name=\"request\">"));
            Assert.AreEqual(12, CountOccurrences(source, "/// <param name=\"timeOut\">"));
            Assert.AreEqual(12, CountOccurrences(source, "/// <returns>"));
            Assert.AreEqual(5, CountOccurrences(source, "=> PostP1<"));
            Assert.AreEqual(5, CountOccurrences(source, "=> PostP1Async<"));
            Assert.AreEqual(1, CountOccurrences(source, "=> GetP1<"));
            Assert.AreEqual(1, CountOccurrences(source, "=> GetP1Async<"));
        }

        [TestMethod]
        public void ExternalIdentityRequestsPreserveOfficialJsonShapes()
        {
            var unionIdRequest = new ExternalContactUnionIdConvertRequest { unionid = "union-1" };
            using var unionIdDocument = JsonDocument.Parse(JsonSerializer.Serialize(unionIdRequest));
            Assert.AreEqual("union-1", unionIdDocument.RootElement.GetProperty("unionid").GetString());

            var mobileRequest = new BatchMobileToExternalUserIdRequest
            {
                mobiles = new List<string> { "10000000000", "10000000001" }
            };
            using var mobileDocument = JsonDocument.Parse(JsonSerializer.Serialize(mobileRequest));
            Assert.AreEqual("10000000001",
                mobileDocument.RootElement.GetProperty("mobiles")[1].GetString());

            var serviceRequest = new ServiceExternalUserIdConvertRequest
            {
                external_userid = "external-old"
            };
            using var serviceDocument = JsonDocument.Parse(JsonSerializer.Serialize(serviceRequest));
            Assert.AreEqual("external-old",
                serviceDocument.RootElement.GetProperty("external_userid").GetString());

            var newIdRequest = new NewExternalUserIdRequest
            {
                external_userid_list = new List<string> { "external-1", "external-2" }
            };
            using var newIdDocument = JsonDocument.Parse(JsonSerializer.Serialize(newIdRequest));
            Assert.AreEqual("external-2",
                newIdDocument.RootElement.GetProperty("external_userid_list")[1].GetString());

            var groupRequest = new NewGroupChatExternalUserIdRequest
            {
                chat_id = "chat-1",
                external_userid_list = new List<string> { "external-1" }
            };
            using var groupDocument = JsonDocument.Parse(JsonSerializer.Serialize(groupRequest));
            Assert.AreEqual("chat-1", groupDocument.RootElement.GetProperty("chat_id").GetString());
            Assert.AreEqual("external-1",
                groupDocument.RootElement.GetProperty("external_userid_list")[0].GetString());
        }

        [TestMethod]
        public void ExternalIdentityResultsPreserveMappingsFailuresAndPermitRanges()
        {
            var mobileResult = JsonSerializer.Deserialize<BatchMobileToExternalUserIdResult>(
                "{\"errcode\":0,\"success_list\":[{\"mobile\":\"10000000000\"," +
                "\"external_userid\":\"external-1\",\"foreign_key\":\"student-1\"}]," +
                "\"fail_list\":[{\"errcode\":60136,\"errmsg\":\"record not found\"," +
                "\"mobile\":\"10000000001\"}]}");
            var permitResult = JsonSerializer.Deserialize<CustomerAcquisitionAppPermitResult>(
                "{\"errcode\":0,\"user_list\":[\"zhangsan\"]," +
                "\"department_list\":[4294967296],\"tag_list\":[4294967297]}");
            var newIdResult = JsonSerializer.Deserialize<NewExternalUserIdResult>(
                "{\"errcode\":0,\"items\":[{\"external_userid\":\"external-old\"," +
                "\"new_external_userid\":\"external-new\"}]}");

            Assert.IsNotNull(mobileResult);
            Assert.AreEqual("external-1", mobileResult.success_list[0].external_userid);
            Assert.AreEqual("student-1", mobileResult.success_list[0].foreign_key);
            Assert.AreEqual(60136, mobileResult.fail_list[0].errcode);
            Assert.AreEqual("record not found", mobileResult.fail_list[0].errmsg);
            Assert.IsNotNull(permitResult);
            Assert.AreEqual("zhangsan", permitResult.user_list[0]);
            Assert.AreEqual(4294967296L, permitResult.department_list[0]);
            Assert.AreEqual(4294967297L, permitResult.tag_list[0]);
            Assert.IsNotNull(newIdResult);
            Assert.AreEqual("external-old", newIdResult.items[0].external_userid);
            Assert.AreEqual("external-new", newIdResult.items[0].new_external_userid);
        }

        [TestMethod]
        public void ExternalIdentitySingleValueResultsRemainStronglyTyped()
        {
            var unionIdResult = JsonSerializer.Deserialize<ExternalContactUnionIdConvertResult>(
                "{\"errcode\":0,\"external_userid\":\"external-union\"}");
            var serviceResult = JsonSerializer.Deserialize<ServiceExternalUserIdConvertResult>(
                "{\"errcode\":0,\"external_userid\":\"external-service\"}");

            Assert.IsNotNull(unionIdResult);
            Assert.AreEqual("external-union", unionIdResult.external_userid);
            Assert.IsNotNull(serviceResult);
            Assert.AreEqual("external-service", serviceResult.external_userid);
        }

        [TestMethod]
        public void ExternalIdentityPublicModelsAreStronglyTypedAndDocumented()
        {
            var modelTypes = new[]
            {
                typeof(ExternalContactUnionIdConvertRequest),
                typeof(ExternalContactUnionIdConvertResult),
                typeof(BatchMobileToExternalUserIdRequest),
                typeof(MobileExternalUserIdConvertItem),
                typeof(BatchMobileToExternalUserIdResult),
                typeof(ServiceExternalUserIdConvertRequest),
                typeof(ServiceExternalUserIdConvertResult),
                typeof(CustomerAcquisitionAppPermitResult),
                typeof(NewExternalUserIdRequest), typeof(NewGroupChatExternalUserIdRequest),
                typeof(NewExternalUserIdItem), typeof(NewExternalUserIdResult)
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

            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src",
                "Senparc.Weixin.Work", "Senparc.Weixin.Work", "AdvancedAPIs", "External",
                "ExternalIdentityMigrationJson.cs"));
            var declarationCount = source.Split(new[] { '\r', '\n' },
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.TrimStart())
                .Count(line => line.StartsWith("public class ", StringComparison.Ordinal) ||
                               line.StartsWith("public ", StringComparison.Ordinal) &&
                               line.Contains("{ get; set; }", StringComparison.Ordinal));
            Assert.AreEqual(declarationCount, CountOccurrences(source, "/// <summary>"));
        }

        private static void AssertSyncAndAsyncContract(string methodName, Type requestType,
            Type resultType)
        {
            var parameterTypes = new[] { typeof(string), requestType, typeof(int) };
            var syncMethod = typeof(ExternalApi).GetMethod(methodName, parameterTypes);
            var asyncMethod = typeof(ExternalApi).GetMethod(methodName + "Async", parameterTypes);

            Assert.IsNotNull(syncMethod, methodName);
            Assert.AreEqual(resultType, syncMethod.ReturnType, methodName);
            Assert.IsNotNull(asyncMethod, methodName + "Async");
            Assert.AreEqual(typeof(Task<>).MakeGenericType(resultType), asyncMethod.ReturnType,
                methodName + "Async");
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
