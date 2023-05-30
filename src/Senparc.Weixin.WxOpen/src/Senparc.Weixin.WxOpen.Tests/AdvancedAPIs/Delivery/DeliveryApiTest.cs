using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson.JsonResult;
using Senparc.Weixin.WxOpen.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson;

namespace Senparc.Weixin.WxOpen.Tests.AdvancedAPIs.Delivery
{
    [TestClass]
    public class DeliveryApiTest:WxOpenBaseTest
    {
        [TestMethod]
        public void GetAllDelivery()
        {
            GetAllDeliveryJsonResult result = WxOpen.AdvancedAPIs.Delivery.DeliveryApi.GetAllDelivery(_wxOpenAppId);
            Assert.AreEqual(ReturnCode.请求成功,result);
        }
    }
}
