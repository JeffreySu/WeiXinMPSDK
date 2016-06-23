using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.ScanProduct;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs.ScanProduct
{
    [TestClass]
    public class ScanProductTest : CommonApiTest
    {

        private static readonly string accessToken = "ZI27u_iZTXIqhBSrJ5-pe4w8ePmYDdS5g_qt7dElfhBW2zdrnNRGGRAlqBckaPs26kWNN9rdpR_8KzwqrqY6N_smN7LDu3jFMKHae5TbehSjIFRGBu2NStIj2fEGbdgEMPXiAAAYBJ";

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

            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.ResetTestUserWhiteList(accessToken, new string[] { }, new string[] { "wangzhangxiaoyu", "hugejile1979" });

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
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.GetProductQrCode(accessToken, goodKeyStr, ProductKeyStandardOptions.ean13, null);
            Assert.IsNotNull(result.qrcode_url);
        }

        [TestMethod]
        public void GetProductTest()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new ScanProductActionListConverter() }
            };

            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.GetProduct(accessToken, "6954496901195", ProductKeyStandardOptions.ean13);

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
                keystandard = ProductKeyStandardOptions.ean13,
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
                            new Product_Brand_Action_Info_User{  appid ="gh_a15bcf30df02" },
                            new Product_Brand_Action_Info_Price{ retail_price = "9.0" },
                            new Product_Brand_Action_Info_Text{ text = "sadfasdfasdfasdf"},
                            new Product_Brand_Action_Info_Link{ name = "查看公众号", digest="hi", link = "https://mp.weixin.qq.com/bizmall/scan?action=home&productid=CAEQBBoNNjk0NTcwOTkxMDI0MA==&extinfo=&lang=zh_CN&scene=&from=singlemessage&isappinstalled=0&uin=MTU4MTE0MzU4MA%3D%3D&key=18e81ac7415f67c428a2b1f54afed729a440ecb8e3e3c6b434bfb0c5bbdba7d60dd65077789776c3e7089008c7e28562&devicetype=android-19&version=26031233&nettype=WIFI&pass_ticket=j%2BfIvH%2BDEoGo3q7RV7us93X%2FP9xoJXDIzZFfvEWlvPtk%2FzOLrXjEuQ7fGlmA5TS8"}
                        },

                    },
                    detail_info = new Product_Brand_Detail_Info
                    {
                        detail_list = new System.Collections.Generic.List<Product_Brand_Detail_Info_Desc>
                        {
                            new Product_Brand_Detail_Info_Desc{ title = "测试",  desc="asdfasdf"},
                            new Product_Brand_Detail_Info_Desc{ title = "测试",  desc="asdfasdf"},
                            new Product_Brand_Detail_Info_Desc{ title = "测试1",  desc="a1sdfasdf"}
                        },
                        banner_list = new List<Product_Brand_Detail_Info_Link>
                        {
                            new Product_Brand_Detail_Info_Link{ link = "http://mmbiz.qpic.cn/mmbiz/hXn4njvzgZUfSZ4n6t3DM4mRXQHWoN9jhkGSLCNNe38E9dFgcFQHBeLkymXbpC2wr51q6jkOUKGujCYDP3WorQ/0" }
                        }
                    },
                    module_info = new Product_Brand_Module_Info
                    {
                        module_list = new System.Collections.Generic.List<Product_Brand_Module_Info_Item_Base>
                        {
                            new Product_Brand_Module_Info_Item_AntiFake(){ native_show = "true" }
                        }
                    }
                },
            };



            //var sss = Newtonsoft.Json.JsonConvert.SerializeObject(product);
            //var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.CreateProduct(accessToken, product);
            //Assert.AreEqual(ReturnCode.请求成功, result.errcode);

            var codeResult = MP.AdvancedAPIs.ScanProduct.ScanProductApi.GetProductQrCode(accessToken, keystr, ProductKeyStandardOptions.ean13, "123456");

            product.brand_info.base_info.title = "测试商品2";
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.UpdateProduct(accessToken, product);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);

            result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.DeleteProduct(accessToken, keystr, ProductKeyStandardOptions.ean13);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }

        [TestMethod]
        public void PublicProductTest()
        {
            var result = MP.AdvancedAPIs.ScanProduct.ScanProductApi.PublicProduct(accessToken, goodKeyStr, ProductKeyStandardOptions.ean13, ProductPublicStatus.Off);
        }

    }
}
