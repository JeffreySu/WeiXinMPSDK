using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Wxa;
using Senparc.Weixin.MP.AdvancedAPIs.Wxa.MerchantJson;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Test.CommonAPIs;
using System.IO;
using System.Linq;

namespace Senparc.Weixin.MP.Test.vs2017.AdvancedAPIs.Wxa
{
    [TestClass]
    public class WxaTests : CommonApiTest
    {
        [TestMethod]
        public void GetCategoryTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = WxaApi.GetMerchantCategoryResult(accessToken);
            Assert.IsNotNull(result.data);
            Assert.IsTrue(result.data.all_category_info.categories.Count() > 0);
        }

        [TestMethod]
        public void ApplyMerchantTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            //var fileResult = MediaApi.UploadTemporaryMedia(accessToken, UploadMediaFileType.image, "xx.png");
            //fileResult.media_id
            var result = WxaApi.ApplyMerchant(accessToken, new ApplyMerchantData()
            {
                first_catid = 317,//教育学校
                second_catid = 319,//幼儿园
                headimg_mediaid = "IZpRRVmI2f__n9jMy6RcJnsjuPZeEbKa_WJyMeTTE4tA656NPLwgz84tip8DAGr9",//替换为自己的fileResult.media_id,
                intro = "红星幼儿园",
                nickname = "红星幼儿园526422",//可能会出现名字已占用
            });
            Assert.IsTrue(result.errcode == 0);
        }

        [TestMethod]
        public void GetMerchantAuditInfoTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = WxaApi.GetMerchantAuditInfo(accessToken);
            Assert.IsNotNull(result.data);
        }

        [TestMethod]
        public void ModifyMerchantTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = WxaApi.ModifyMerchant(accessToken, "IZpRRVmI2f__n9jMy6RcJnsjuPZeEbKa_WJyMeTTE4tA656NPLwgz84tip8DAGr9", "红星幼儿园1");//mediaid需要替换wei为可用的
            Assert.IsTrue(result.errcode == 0);
        }

        [TestMethod]
        public void SearchMapPoi()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = WxaApi.SearchMapPoi(accessToken, 440300, "");
            Assert.IsTrue(result.errcode == 0);
        }

        [TestMethod]
        public void AddStoreTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            AddStoreJsonData data = new AddStoreJsonData
            {
                map_poi_id = "213213124",
                pic_list = new string[] { "123", "123" }.ToJson(),
                contract_phone = "40082008820",
                hour = "11:00-12:00",
                credential = "13245746543543654654",
            };
            var result = WxaApi.Addstore(accessToken, data);
            Assert.IsTrue(result.errcode == 0);//系统繁忙
            //TODO:后续门店管理接口测试
        }

        [TestMethod]
        public void GetDistrictTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = WxaApi.GetDistrict(accessToken);

            Assert.IsTrue(result.status == 0);
        }

        [TestMethod]
        public void CreateMapPoiTest()
        {
            CreateMapPoiData data = new CreateMapPoiData
            {
                name = "hardenzhang",
                longitude = "113.323753357",
                latitude = "23.0974903107",
                province = "广东省",
                city = "广州市",
                district = "海珠区",
                address = "TTT",
                category = "美食:中餐厅",
                telephone = "12345678901",
                photo = "http://mmbiz.qpic.cn/mmbiz_png/tW66AWE2K6ECFPcyAcIZTG8RlcR0sAqBibOm8gao5xOoLfIic9ZJ6MADAktGPxZI7MZLcadZUT36b14NJ2cHRHA/0?wx_fmt=png",
                license = "http://mmbiz.qpic.cn/mmbiz_png/tW66AWE2K6ECFPcyAcIZTG8RlcR0sAqBibOm8gao5xOoLfIic9ZJ6MADAktGPxZI7MZLcadZUT36b14NJ2cHRHA/0?wx_fmt=png",
                introduct = "test",
                districtid = "440105"
            };

            var result = WxaApi.CreateMapPoi(_appId, data);

            Assert.IsNull(result.error == null);
        }
    }
}
