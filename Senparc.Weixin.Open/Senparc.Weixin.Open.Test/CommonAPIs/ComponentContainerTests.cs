using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Open.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.CommonAPIs.Tests
{
    [TestClass()]
    public class ComponentContainerTests
    {
        [TestMethod()]
        public void RegisterTest()
        {
            var componentAppId = "ComponentAppId";
            Func<string, string> getComponentVerifyTicketFunc = s =>
            {
                //do something
                return "tiket";
            };

            ComponentContainer.Register(componentAppId, "ComponentAppSecret",getComponentVerifyTicketFunc);

            var fullCollections = ComponentContainer.GetCollectionList();
            Assert.IsTrue(fullCollections.Count==1);

            var container = fullCollections.Values.First();
            var bag = container.Values.First();
            Assert.IsTrue(container.Values.Count==1);
            Assert.AreEqual(componentAppId, bag.ComponentAppId);
            Assert.AreEqual("ComponentAppSecret", bag.ComponentAppSecret);

            var ticket = ComponentContainer.TryGetComponentVerifyTicket(componentAppId);
            Assert.AreEqual("tiket", ticket);
        }
    }
}