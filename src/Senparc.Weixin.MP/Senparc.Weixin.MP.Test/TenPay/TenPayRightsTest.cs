using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.MP.TenPayLib;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //需要通过微信支付审核的账户才能成功
    [TestClass]
    public class TenPayRightsTest : CommonApiTest
    {
        [TestMethod]
        public void UpDateFeedBackTest()
        {
            var result = TenPayRights.UpDateFeedBack("[accessToken]", "[openId]", "[feedBackId]");
            Console.Write(result);
            Assert.IsNotNull(result);
        }
    }
}
