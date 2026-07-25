using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.Mass;
using WorkMassApi = Senparc.Weixin.Work.AdvancedAPIs.MassApi;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.Mass
{
    [TestClass]
    public class MessageStatisticsContractTests
    {
        [TestMethod]
        public void MassApiExposesMessageStatisticsSyncAndAsyncEntries()
        {
            var methods = typeof(WorkMassApi).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var syncMethod = methods.Single(method => method.Name == nameof(WorkMassApi.GetMessageStatistics));
            var asyncMethod = methods.Single(method => method.Name == nameof(WorkMassApi.GetMessageStatisticsAsync));

            Assert.AreEqual(typeof(MessageStatisticsResult), syncMethod.ReturnType);
            Assert.AreEqual(typeof(Task<MessageStatisticsResult>), asyncMethod.ReturnType);
            Assert.AreEqual(typeof(MessageStatisticsRequest), syncMethod.GetParameters()[1].ParameterType);
            Assert.AreEqual(typeof(MessageStatisticsRequest), asyncMethod.GetParameters()[1].ParameterType);
        }

        [TestMethod]
        public void MessageStatisticsUsesOfficialPostPath()
        {
            var path = typeof(WorkMassApi).GetField("GetMessageStatisticsPath",
                BindingFlags.NonPublic | BindingFlags.Static)?.GetRawConstantValue();

            Assert.AreEqual("/cgi-bin/message/get_statistics", path);
        }

        [TestMethod]
        public void MessageStatisticsModelsPreserveOfficialFields()
        {
            var requestJson = JsonSerializer.Serialize(new MessageStatisticsRequest { time_type = 2 });
            StringAssert.Contains(requestJson, "\"time_type\":2");

            var result = JsonSerializer.Deserialize<MessageStatisticsResult>(
                "{\"errcode\":0,\"statistics\":[{\"agentid\":1000002," +
                "\"app_name\":\"通知应用\",\"count\":2147483000}]}");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.statistics.Length);
            Assert.AreEqual(1000002, result.statistics[0].agentid);
            Assert.AreEqual("通知应用", result.statistics[0].app_name);
            Assert.AreEqual(2147483000, result.statistics[0].count);
        }
    }
}
