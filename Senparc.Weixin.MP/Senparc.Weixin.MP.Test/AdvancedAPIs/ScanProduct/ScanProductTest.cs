using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.ScanProduct;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs.ScanProduct
{
    [TestClass]
    public class ScanProductTest : CommonApiTest
    {

        private static readonly string accessToken = "h9M-rPn_AeogJzylp7BCt24MlZRj81_UaR1P5nFLH7BIrp-UKnzrwJNUYyTftxM9IKrNKaKlmiG9MG4_XVff4xMLw3qGEvBAWecrptKEBnUaIgu1tnHbDgxG5ofgU_8PLKJfADAFED";

        private static readonly string goodKeyStr = "6954496900556";

        [TestMethod]
        public void GetMerchanstInfoTest()
        {
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.GetMerchanstInfo(accessToken);

            Assert.IsTrue(result.verified_list.Count > 0);
            //Assert.IsNotNull(result.Equals);
            //Assert.AreEqual("附近有什么川菜馆", result.query);
        }

        [TestMethod]
        public void AddTestUsers()
        {
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.ResetTestUserWhiteList(accessToken, new string[] { "oHDSGwPx5LpckuJEgkAufACgMux0" }, new string[] { "hugejile" });
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }

        [TestMethod]
        public void GetProductsTest()
        {
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.GetProductList(accessToken, 0, 100);
            Assert.IsTrue(result.total >= 0);
        }

        [TestMethod]
        public void GetProductQrCodeTest()
        {
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.GetProductQrCode(accessToken, goodKeyStr, ProductKeystandardOptions.Ean13, "test");
            Assert.IsNotNull(result.pic_url);
        }


        [TestMethod]
        public void CreateProductTest()
        {
            var product = new ProductModel
            {
                keystr = "6954496901195",
                brand_info = new Product_Brand_Info
                {
                    base_info = new Product_Brand_Base_Info
                    {
                         
                    }
                },
                keystandard = ProductKeystandardOptions.Ean13.ToString().ToLower()
            };
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.CreateProduct(accessToken, product);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }

        [TestMethod]
        public void GetProductTest()
        {
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.GetProduct(accessToken, goodKeyStr, ProductKeystandardOptions.Ean13);

        }

        [TestMethod]
        public void PublicProductTest()
        {
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.PublicProduct(accessToken, goodKeyStr, ProductKeystandardOptions.Ean13, ProductPublicStatus.Off);
        }

    }
}
