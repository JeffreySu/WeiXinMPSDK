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

        private static readonly string accessToken = "ENgTWMa473M3UJUhSIYrJxrv2e62XJIGaDwBeKOYqVjc7GMjfGJZug_pXgt6xO7hU8LYckr5l87gZuYmItry1EGWhkyvnV2cL6mfllLQiKyeF97B0p53cGX9aegBgFxUTOXcAFAOJS";

        private static readonly string goodKeyStr = "6954496901195";

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
            //var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.ResetTestUserWhiteList(accessToken, new string[] { "oHDSGwDp_PfBkapkXMVostXWM7dI", "oHDSGwEw2BcK11hk05xzvEZUBkcc", "oHDSGwDyy1tLIZtgko49m7NjpfI0" }, null);

            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.ResetTestUserWhiteList(accessToken, new string[] { }, new string[] { "wangzhangxiaoyu" });

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
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.GetProductQrCode(accessToken, goodKeyStr, ProductKeystandardOptions.ean13, "test");
            Assert.IsNotNull(result.pic_url);
        }

        [TestMethod]
        public void GetProductTest()
        {
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.GetProduct(accessToken, goodKeyStr, ProductKeystandardOptions.ean13);

            Assert.IsTrue(result.errcode == ReturnCode.请求成功);
        }

        [TestMethod]
        public void CreateProductTest()
        {
            //{"errcode":0,"errmsg":"ok","brand_tag_list":["稚优泉"],"verified_list":[{"verified_firm_code":69544969,"verified_cate_list":[{"verified_cate_id":538112978,"verified_cate_name":"彩妆\/香水\/美妆工具"}]}]}

            var keystr = "6954496901195";
            var product = new ProductModel
            {
                keystr = keystr,
                brand_info = new Product_Brand_Info
                {
                    base_info = new Product_Brand_Base_Info
                    {
                        brand_tag = "稚优泉",
                        category_id = "538112978",
                        thumb_url = "http://mmbiz.qpic.cn/mmbiz/hXn4njvzgZUT3A4VeW98Msv5lFP8ZKeg4l0D1WtKlZo1ibJasouW6cGOa18MGqw2X8cryNSIiarekty2tGiaKn7SA/0?wx_fmt=jpeg",
                        title = "测试商品",
                        color = "auto",
                        store_mgr_type = "auto",

                    },
                    action_info = new Product_Brand_Action_Info
                    {
                        action_list = new System.Collections.Generic.List<Product_Brand_Action_Info_Base>
                        { 
                            new Product_Brand_Action_Info_Price{ retail_price = "9.0" }
                        }
                    },
                    detail_info = new Product_Brand_Detail_Info
                    {
                        detail_list = new System.Collections.Generic.List<Product_Brand_Detail_Info_Desc>
                        {
                            new Product_Brand_Detail_Info_Desc{ title = "测试",  desc=""}
                        }
                    },
                    module_info = new Product_Brand_Module_Info
                    {

                    }
                },
                keystandard = ProductKeystandardOptions.ean13.ToString().ToLower()
            };
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.CreateProduct(accessToken, product);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);

            var codeResult = MP.AdvancedAPIs.ScanProduct.ScanProductApi.GetProductQrCode(accessToken, keystr, ProductKeystandardOptions.ean13);

            product.brand_info.base_info.title = "测试商品2";
            result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.UpdateProduct(accessToken, product);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);

            result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.DeleteProduct(accessToken, keystr, ProductKeystandardOptions.ean13);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }

        [TestMethod]
        public void PublicProductTest()
        {
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.PublicProduct(accessToken, goodKeyStr, ProductKeystandardOptions.ean13, ProductPublicStatus.Off);
        }

    }
}
