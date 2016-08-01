using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.TenPayLibV3;
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
                100, "祝福", "活动", "备注", out nonceStr, out paySign);
            Console.Write(result);
            Assert.IsNotNull(result);
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
