using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.WxOpen.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.WxOpen.Containers.Tests
{
    [TestClass()]
    public class SessionContainerTests:CommonApiTest
    {
        [TestMethod()]
        public void UpdateSessionTest()
        {
            var openId = "openid";
            var sessionKey = "sessionKey";
            var bag = SessionContainer.UpdateSession(null, openId, sessionKey);
            Console.WriteLine("bag.Key:{0}",bag.Key);
            Console.WriteLine("bag.ExpireTime:{0}",bag.ExpireTime);

            var key = bag.Key;

            Thread.Sleep(1000);
            var bag2 = SessionContainer.GetSession(key);
            Assert.IsNotNull(bag2);
            Console.WriteLine("bag2.ExpireTime:{0}", bag2.ExpireTime);


        }
    }
}