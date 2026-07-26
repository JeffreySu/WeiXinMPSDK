using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.AdvancedAPIs.WeChatCustomerService;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.WeChatCustomerService
{
    [TestClass]
    public class QualificationContractTests
    {
        [TestMethod]
        public void ApiExposesQualificationSyncAndAsyncEntries()
        {
            var methods = typeof(WeChatCustomerServiceApi).GetMethods(BindingFlags.Public | BindingFlags.Static);
            var syncMethod = methods.Single(method => method.Name == nameof(WeChatCustomerServiceApi.GetCorpQualification));
            var asyncMethod = methods.Single(method => method.Name == nameof(WeChatCustomerServiceApi.GetCorpQualificationAsync));

            Assert.AreEqual(typeof(KfCorpQualificationResult), syncMethod.ReturnType);
            Assert.AreEqual(typeof(Task<KfCorpQualificationResult>), asyncMethod.ReturnType);
        }

        [TestMethod]
        public void ApiUsesOfficialGetPath()
        {
            var path = typeof(WeChatCustomerServiceApi).GetField("GetCorpQualificationPath",
                BindingFlags.NonPublic | BindingFlags.Static)?.GetRawConstantValue();

            Assert.AreEqual("/cgi-bin/kf/get_corp_qualification", path);
        }

        [TestMethod]
        public void ResultPreservesOfficialQualificationField()
        {
            var result = JsonSerializer.Deserialize<KfCorpQualificationResult>(
                "{\"errcode\":0,\"wechat_channels_binding\":true}");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.wechat_channels_binding);
        }
    }
}
