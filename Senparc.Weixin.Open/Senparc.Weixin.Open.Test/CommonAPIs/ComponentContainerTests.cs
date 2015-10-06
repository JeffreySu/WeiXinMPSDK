using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Open.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Open.Test;

namespace Senparc.Weixin.Open.CommonAPIs.Tests
{
    [TestClass()]
    public class ComponentContainerTests: BaseTest
    {


        [TestMethod()]
        public void RegisterTest()
        {
            Func<string, string> getComponentVerifyTicketFunc = s =>
            {
                //do something
                return "tiket";
            };

            ComponentContainer.Register(base._appId,base._appSecret, getComponentVerifyTicketFunc);

            var fullCollections = ComponentContainer.GetCollectionList();
            Assert.IsTrue(fullCollections.Count == 1);

            var container = fullCollections.Values.First();
            var bag = container.Values.First();
            Assert.IsTrue(container.Values.Count == 1);
            Assert.AreEqual(base.AppConfig, bag.ComponentAppId);
            Assert.AreEqual(base._appSecret, bag.ComponentAppSecret);
            Assert.IsNotNull(bag.Key);
            Assert.AreEqual(base._appId, bag.Key);

            var ticket = ComponentContainer.TryGetComponentVerifyTicket(base._appId);
            Assert.AreEqual("tiket", ticket);
        }

        [TestMethod()]
        public void GetPreAuthCodeResultTest()
        {
            RegisterTest();//注册


        }
    }
}