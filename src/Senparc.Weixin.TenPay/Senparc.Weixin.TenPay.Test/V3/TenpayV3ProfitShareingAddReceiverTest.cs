using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.TenPay.V3;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPay.Test.vs2017.V3
{
    [TestClass]
    public class TenpayV3ProfitShareingAddReceiverTest
    {
        [TestMethod]
        public void TestXml()
        {
            var req = new TenpayV3ProfitShareingAddReceiver(
               "wx2421b1c4370ec43b",
               "10000100",
               "wx2203b1494370e08cm",
               "1415701182",
               "192006250b4c09247ec02edce69f6a2d",
               TenPayV3Util.GetNoncestr(),
               new TenpayV3ProfitShareingAddReceiver_ReceiverInfo
                   {
                       receiveType = TenpayV3ProfitShareingAddReceiver_ReceiverInfo_Type.MERCHANT_ID,
                       account = "190001001",
                       name = "示例商户全称",
                       receiverRelationType = TenpayV3ProfitShareingAddReceiver_ReceiverInfo_RelationType.STORE_OWNER
                });
            var sign = req.Sign;
            var xml = req.PackageRequestHandler.ParseXML();
            Assert.IsTrue(req.Sign == "asgasdg");
        }
    }
}
