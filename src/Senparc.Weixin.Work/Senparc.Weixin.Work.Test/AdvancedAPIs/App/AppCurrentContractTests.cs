using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.App;
using Senparc.Weixin.Work.AdvancedAPIs.WorkBench;
using Senparc.Weixin.Work.AdvancedAPIs.WorkBench.WorkBenchJson;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.App
{
    [TestClass]
    public class AppCurrentContractTests
    {
        [TestMethod]
        public void AppApiExposesThreeCurrentSyncAndAsyncOperations()
        {
            var methods = typeof(AppApi).GetMethods(BindingFlags.Public | BindingFlags.Static);

            Assert.AreEqual(typeof(WorkJsonResult),
                methods.Single(method => method.Name == nameof(AppApi.MigrateToCustomizedApp)).ReturnType);
            Assert.AreEqual(typeof(Task<WorkJsonResult>),
                methods.Single(method => method.Name == nameof(AppApi.MigrateToCustomizedAppAsync)).ReturnType);
            Assert.AreEqual(typeof(GetAppPermissionsResult),
                methods.Single(method => method.Name == nameof(AppApi.GetAppPermissions)).ReturnType);
            Assert.AreEqual(typeof(Task<GetAppPermissionsResult>),
                methods.Single(method => method.Name == nameof(AppApi.GetAppPermissionsAsync)).ReturnType);
            Assert.AreEqual(typeof(GetAppAdminListResult),
                methods.Single(method => method.Name == nameof(AppApi.GetAppAdminList)).ReturnType);
            Assert.AreEqual(typeof(Task<GetAppAdminListResult>),
                methods.Single(method => method.Name == nameof(AppApi.GetAppAdminListAsync)).ReturnType);
        }

        [TestMethod]
        public void AppApiUsesOfficialPostPaths()
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Static;

            Assert.AreEqual("/cgi-bin/agent/migrate_to_customized_app",
                typeof(AppApi).GetField("MigrateToCustomizedAppPath", flags)?.GetRawConstantValue());
            Assert.AreEqual("/cgi-bin/agent/get_permissions",
                typeof(AppApi).GetField("GetAppPermissionsPath", flags)?.GetRawConstantValue());
            Assert.AreEqual("/cgi-bin/agent/get_admin_list",
                typeof(AppApi).GetField("GetAppAdminListPath", flags)?.GetRawConstantValue());
        }

        [TestMethod]
        public void AppModelsPreserveOfficialFields()
        {
            var requestJson = JsonSerializer.Serialize(new MigrateToCustomizedAppRequest
            {
                suite_access_token = "SUITE_ACCESS_TOKEN"
            });
            var permissions = JsonSerializer.Deserialize<GetAppPermissionsResult>(
                "{\"errcode\":0,\"app_permissions\":[\"customer_contact\",\"message\"]}");
            var admins = JsonSerializer.Deserialize<GetAppAdminListResult>(
                "{\"errcode\":0,\"admin\":[{\"userid\":\"zhangsan\",\"auth_type\":1}]}");

            StringAssert.Contains(requestJson, "\"suite_access_token\":\"SUITE_ACCESS_TOKEN\"");
            Assert.IsNotNull(permissions);
            Assert.AreEqual("message", permissions.app_permissions[1]);
            Assert.IsNotNull(admins);
            Assert.AreEqual("zhangsan", admins.admin[0].userid);
            Assert.AreEqual(1, admins.admin[0].auth_type);
        }

        [TestMethod]
        public void WorkBenchApiExposesBatchOperationAndStrongRequestShape()
        {
            var methods = typeof(WorkBenchApi).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var path = typeof(WorkBenchApi).GetField("BatchSetWorkBenchDataPath",
                BindingFlags.NonPublic | BindingFlags.Static)?.GetRawConstantValue();
            var json = JsonSerializer.Serialize(new BatchSetWorkBenchDataModel
            {
                agentid = 1000002,
                userid_list = new List<string> { "zhangsan", "lisi" },
                data = new BatchWorkBenchData
                {
                    type = "image",
                    image = new WorkBenchImageModel
                    {
                        url = "https://example.test/banner.png",
                        jump_url = "https://example.test/detail"
                    }
                }
            });

            Assert.AreEqual(typeof(WorkJsonResult),
                methods.Single(method => method.Name == nameof(WorkBenchApi.BatchSetWorkBenchData)).ReturnType);
            Assert.AreEqual(typeof(Task<WorkJsonResult>),
                methods.Single(method => method.Name == nameof(WorkBenchApi.BatchSetWorkBenchDataAsync)).ReturnType);
            Assert.AreEqual("/cgi-bin/agent/batch_set_workbench_data", path);
            StringAssert.Contains(json, "\"agentid\":1000002");
            StringAssert.Contains(json, "\"userid_list\":[\"zhangsan\",\"lisi\"]");
            StringAssert.Contains(json, "\"data\":{\"type\":\"image\"");
            StringAssert.Contains(json, "\"image\":");
        }
    }
}
