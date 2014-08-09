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
using Senparc.Weixin.MP.WeixinPayLib;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //需要通过微信支付审核的账户才能成功
    [TestClass]
    public class WeixinShopTest : CommonApiTest
    {

        [TestMethod]
        public void AddProdectTest()
        {
            var addProductData = new ProductData();
            var result = WeixinShop.AddProduct("[appid]", addProductData);
            Console.Write(result);
            Assert.IsNotNull(result);
        }
    }
}
