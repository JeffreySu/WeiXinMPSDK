﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using Senparc.Weixin.MP.Test.CommonAPIs;
using System;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //需要通过微信支付审核的账户才能成功
    [TestClass]
    public class ProductTest : CommonApiTest
    {
        [TestMethod]
        public void AddProdectTest()
        {
            var addProductData = new AddProductData();
            var result = ProductApi.AddProduct("[appid]", addProductData);
            Console.Write(result);
            Assert.IsNotNull(result);
        }
    }
}