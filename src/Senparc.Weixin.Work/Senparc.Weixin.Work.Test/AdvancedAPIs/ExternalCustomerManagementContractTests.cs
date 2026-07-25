using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.External;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
{
    [TestClass]
    public class ExternalCustomerManagementContractTests
    {
        [TestMethod]
        public void CustomerStrategyAndTransferApisExposeSyncAndAsyncEntrypoints()
        {
            var methodNames = typeof(ExternalApi).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Select(method => method.Name).ToArray();

            foreach (var methodName in new[]
            {
                nameof(ExternalApi.ListCustomerStrategies), nameof(ExternalApi.GetCustomerStrategy),
                nameof(ExternalApi.GetCustomerStrategyRange), nameof(ExternalApi.CreateCustomerStrategy),
                nameof(ExternalApi.EditCustomerStrategy), nameof(ExternalApi.DeleteCustomerStrategy),
                nameof(ExternalApi.TransferOnJobCustomers), nameof(ExternalApi.GetOnJobCustomerTransferResult),
                nameof(ExternalApi.GetUnassignedCustomers), nameof(ExternalApi.TransferResignedCustomers),
                nameof(ExternalApi.GetResignedCustomerTransferResult), nameof(ExternalApi.TransferResignedGroupChats)
            })
            {
                CollectionAssert.Contains(methodNames, methodName, methodName);
                CollectionAssert.Contains(methodNames, methodName + "Async", methodName + "Async");
            }
        }

        [TestMethod]
        public void CustomerStrategyAndTransferApisUseOfficialPaths()
        {
            var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "Senparc.Weixin.Work",
                "Senparc.Weixin.Work", "AdvancedAPIs", "External", "ExternalCustomerManagementApi.cs"));

            foreach (var path in new[]
            {
                "/cgi-bin/externalcontact/customer_strategy/list",
                "/cgi-bin/externalcontact/customer_strategy/get",
                "/cgi-bin/externalcontact/customer_strategy/get_range",
                "/cgi-bin/externalcontact/customer_strategy/create",
                "/cgi-bin/externalcontact/customer_strategy/edit",
                "/cgi-bin/externalcontact/customer_strategy/del",
                "/cgi-bin/externalcontact/transfer_customer",
                "/cgi-bin/externalcontact/transfer_result",
                "/cgi-bin/externalcontact/get_unassigned_list",
                "/cgi-bin/externalcontact/resigned/transfer_customer",
                "/cgi-bin/externalcontact/resigned/transfer_result",
                "/cgi-bin/externalcontact/groupchat/transfer"
            })
            {
                Assert.AreEqual(2, CountOccurrences(source, path + "\""), path);
            }
        }

        [TestMethod]
        public void CustomerStrategyModelsPreserveOptionalPrivilegesAndLargeIds()
        {
            var json = JsonSerializer.Serialize(new CustomerStrategyCreateRequest
            {
                parent_id = 4294967296L,
                strategy_name = "区域规则",
                privilege = new CustomerStrategyPrivilege
                {
                    share_customer = false,
                    manage_customer_tag = true
                },
                range = new[]
                {
                    new CustomerStrategyRangeNode { type = 2, partyid = 4294967297L }
                }
            });
            var result = JsonSerializer.Deserialize<CustomerStrategyResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"strategy\":{\"strategy_id\":4294967298," +
                "\"parent_id\":4294967296,\"strategy_name\":\"区域规则\",\"create_time\":4294967299," +
                "\"privilege\":{\"share_customer\":false,\"manage_customer_tag\":true}}}");

            StringAssert.Contains(json, "\"parent_id\":4294967296");
            StringAssert.Contains(json, "\"partyid\":4294967297");
            StringAssert.Contains(json, "\"share_customer\":false");
            Assert.AreEqual(4294967298L, result.strategy.strategy_id);
            Assert.AreEqual(4294967299L, result.strategy.create_time);
            Assert.IsFalse(result.strategy.privilege.share_customer.Value);
        }

        [TestMethod]
        public void CustomerTransferModelsPreserveLargeTimestamps()
        {
            var requestJson = JsonSerializer.Serialize(new OnJobCustomerTransferRequest
            {
                handover_userid = "old-user",
                takeover_userid = "new-user",
                external_userid = new[] { "external-1" },
                transfer_success_msg = "由新同事继续服务"
            });
            var result = JsonSerializer.Deserialize<CustomerTransferQueryResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"customer\":[{\"external_userid\":\"external-1\"," +
                "\"status\":2,\"takeover_time\":4294967296}],\"next_cursor\":\"next\"}");
            var unassigned = JsonSerializer.Deserialize<UnassignedCustomerListResult>(
                "{\"errcode\":0,\"errmsg\":\"ok\",\"info\":[{\"handover_userid\":\"old-user\"," +
                "\"external_userid\":\"external-1\",\"dimission_time\":4294967297}],\"is_last\":true}");

            StringAssert.Contains(requestJson, "\"external_userid\":[\"external-1\"]");
            Assert.AreEqual(4294967296L, result.customer[0].takeover_time);
            Assert.AreEqual(4294967297L, unassigned.info[0].dimission_time);
            Assert.IsTrue(unassigned.is_last);
        }

        private static int CountOccurrences(string value, string search)
        {
            return value.Split(new[] { search }, StringSplitOptions.None).Length - 1;
        }

        private static string FindRepositoryRoot([CallerFilePath] string sourceFilePath = null)
        {
            foreach (var startPath in new[]
            {
                Directory.GetCurrentDirectory(), AppContext.BaseDirectory, Path.GetDirectoryName(sourceFilePath)
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
