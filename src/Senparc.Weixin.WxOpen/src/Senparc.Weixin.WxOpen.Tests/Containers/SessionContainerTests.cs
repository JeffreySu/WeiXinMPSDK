#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.WxOpen.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.WxOpen.Tests;

namespace Senparc.Weixin.WxOpen.Containers.Tests
{
    [TestClass()]
    public class SessionContainerTests: WxOpenBaseTest
    {
        [TestMethod()]
        public void UpdateSessionTest()
        {
            var openId = "openid";
            var sessionKey = "sessionKey";
            var unionId = "unionId";
            var bag = SessionContainer.UpdateSession(null, openId, sessionKey, unionId);
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