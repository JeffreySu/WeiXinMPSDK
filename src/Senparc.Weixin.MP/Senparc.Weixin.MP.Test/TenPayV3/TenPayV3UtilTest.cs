using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.TenPayLibV3;
using Senparc.Weixin.MP.Test.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.MP.Test.vs2017.TenPayV3
{

    [TestClass]
    public class TenPayV3UtilTest : CommonApiTest
    {
        [TestMethod]
        public void DecodeRefundReqInfoTest()
        {
            //数据来源：https://pay.weixin.qq.com/wiki/doc/api/native.php?chapter=9_16&index=11
            //var req_info = "T87GAHG17TGAHG1TGHAHAHA1Y1CIOA9UGJH1GAHV871HAGAGQYQQPOOJMXNBCXBVNMNMAJAA";
            var req_info = "QUJDRA==";//ABCD
            var mch_id = "10000100";

            var result = TenPayV3Util.DecodeRefundReqInfo(req_info, mch_id);
            Console.WriteLine(result);
            Assert.IsNotNull(result);
        }

    }
}
