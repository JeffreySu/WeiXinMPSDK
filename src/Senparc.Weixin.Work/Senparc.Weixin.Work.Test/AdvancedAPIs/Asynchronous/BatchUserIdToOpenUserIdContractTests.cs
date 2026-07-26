using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Asynchronous;
using WorkAsynchronousApi = Senparc.Weixin.Work.AdvancedAPIs.AsynchronousApi;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Asynchronous
{
    [TestClass]
    public class BatchUserIdToOpenUserIdContractTests
    {
        [TestMethod]
        public void ApiExposesSyncAndAsyncEntries()
        {
            var methods = typeof(WorkAsynchronousApi).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var syncMethod = methods.Single(method => method.Name == nameof(WorkAsynchronousApi.BatchUserIdToOpenUserId));
            var asyncMethod = methods.Single(method => method.Name == nameof(WorkAsynchronousApi.BatchUserIdToOpenUserIdAsync));

            Assert.AreEqual(typeof(BatchUserIdToOpenUserIdResult), syncMethod.ReturnType);
            Assert.AreEqual(typeof(Task<BatchUserIdToOpenUserIdResult>), asyncMethod.ReturnType);
            Assert.AreEqual(typeof(BatchUserIdToOpenUserIdRequest), syncMethod.GetParameters()[1].ParameterType);
            Assert.AreEqual(typeof(BatchUserIdToOpenUserIdRequest), asyncMethod.GetParameters()[1].ParameterType);
        }

        [TestMethod]
        public void ApiUsesOfficialPostPath()
        {
            var path = typeof(WorkAsynchronousApi).GetField("BatchUserIdToOpenUserIdPath",
                BindingFlags.NonPublic | BindingFlags.Static)?.GetRawConstantValue();

            Assert.AreEqual("/cgi-bin/batch/userid_to_openuserid", path);
        }

        [TestMethod]
        public void ModelsPreserveOfficialFields()
        {
            var requestJson = JsonSerializer.Serialize(new BatchUserIdToOpenUserIdRequest
            {
                userid_list = new[] { "aaa", "bbb" }
            });
            StringAssert.Contains(requestJson, "\"userid_list\":[\"aaa\",\"bbb\"]");

            var result = JsonSerializer.Deserialize<BatchUserIdToOpenUserIdResult>(
                "{\"errcode\":0,\"open_userid_list\":[{\"userid\":\"aaa\",\"open_userid\":\"xxxxx\"}]," +
                "\"invalid_userid_list\":[\"bbb\"]}");

            Assert.IsNotNull(result);
            Assert.AreEqual("aaa", result.open_userid_list[0].userid);
            Assert.AreEqual("xxxxx", result.open_userid_list[0].open_userid);
            CollectionAssert.AreEqual(new[] { "bbb" }, result.invalid_userid_list);
        }
    }
}
