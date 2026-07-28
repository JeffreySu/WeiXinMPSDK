using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Containers.Tests;
using System.Threading.Tasks;

namespace Senparc.WeixinTests.Architecture
{
    [TestClass]
    public class P2ConcurrencyStressTests
    {
        [TestMethod]
        [TestCategory("Stress")]
        public void RegistrationCollectionSupportsConcurrentWritersAtCapacity()
        {
            const int registrationCount = 10_000;
            var registrations = new BaseContainerRegisterFuncCollection<TestContainerBag1>
            {
                MaximumCount = registrationCount
            };

            Parallel.For(0, registrationCount, index =>
                registrations[$"app-{index}"] = () => Task.FromResult(new TestContainerBag1()));

            Assert.AreEqual(registrationCount, registrations.Count);
        }
    }
}
