using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Open.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.Open.Test;

namespace Senparc.Weixin.Open.CommonAPIs.Tests
{
    [TestClass()]
    public class ComponentContainerTests : BaseTest
    {


        [TestMethod()]
        public void RegisterTest()
        {
            Func<string, string> getComponentVerifyTicketFunc = s =>
            {
                //do something
                return base._ticket;
            };

            //ComponentContainer.Register(base._appId, base._appSecret, getComponentVerifyTicketFunc);

            var fullCollections = ComponentContainer.GetCollectionList();
            Assert.IsTrue(fullCollections.Count == 1);

            var container = fullCollections.Values.First();
            var bag = container.Values.First();
            Assert.IsTrue(container.Values.Count == 1);
            Assert.AreEqual(base._appId, bag.ComponentAppId);
            Assert.AreEqual(base._appSecret, bag.ComponentAppSecret);
            Assert.IsNotNull(bag.Key);
            Assert.AreEqual(base._appId, bag.Key);

            var ticket = ComponentContainer.TryGetComponentVerifyTicket(base._appId);
            Assert.AreEqual(base._ticket, ticket);
        }

        [TestMethod()]
        public void GetPreAuthCodeResultTest()
        {
            RegisterTest();//注册

            var preAuthCodeResult = ComponentContainer.GetPreAuthCodeResult(base._appId);
            Assert.IsTrue(preAuthCodeResult.expires_in > 0);

            SerializerHelper serializerHelper=new SerializerHelper();
            Console.Write(serializerHelper.GetJsonString(preAuthCodeResult));
        }
    }
}