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
using Senparc.Weixin.MP.Test.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Test.RedPackApiTest
{
    [TestClass]
    class RedPackApiTest : CommonApiTest
    {
        [TestMethod]
        public void SendNormalRedPackTest()
        {
            string nonceStr = "";
            string paySign = "";
            var result = RedPackApi.SendNormalRedPack(_appId, _mchId, _tenPayKey, _tenPayCertPath, _testOpenId, "测试红包发送者", "127.0.0.1",
                100, "祝福", "活动", "备注", out nonceStr, out paySign, null);
            Assert.IsNotNull(result);
            Console.Write(result);
            Console.Write(result.mch_billno);
            Assert.IsNotNull(nonceStr);
            Assert.IsNotNull(paySign);
        }

        [TestMethod]
        public void SearchRedPackTest()
        {
            var result = RedPackApi.SearchRedPack(_appId, _mchId, _tenPayKey, _tenPayCertPath, "10000098201411111234567890");
            Console.Write(result);
            Assert.IsNotNull(result);
        }
    }
}
