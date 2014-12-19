using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.MP.TenPayLib;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class StoreTest : CommonApiTest
    {
        protected Store_Location _StoreLocation = new Store_Location()
            {
               business_name = "TIT 创意园1 号店",
               province = "广东省",
               city = "广州市",
               district = "海珠区",
               address = "中国广东省广州市海珠区艺苑路11 号",
               telephone = "020-89772059",
               category = "房产小区",
               longitude = "115.32375",
               latitude = "25.097486"
            };

        [TestMethod]
        public void StoreBatchAddTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);
            var data = new StoreLocationData()
                {
                    location_list = new List<Store_Location>()
                        {
                            _StoreLocation
                        },
                };

            var result = Store.StoreBatchAdd(accessToken, data);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void BatchGetTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_appId);

            var result = Store.BatchGet(accessToken, 0, 5);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }
    }
}
