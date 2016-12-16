﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.ShakeAround;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Test.CommonAPIs;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    [TestClass]
    public class ShakeAroundTest : CommonApiTest
    {
        [TestMethod]
        public void Register()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var testData = new RegisterData()
            {
                name = "苏震巍",
                phone_number = "13913718816",
                email = "zsu@senparc.com",
                //industry_id = RegisterData.ConvertIndustryId(IndustryId.其它_其它),
                qualification_cert_urls =
                       new List<string>() {
                  "http://shp.qpic.cn/wx_shake_bus/0/1428565236d03d864b7f43db9ce34df5f720509d0e/0",
                 "http://shp.qpic.cn/wx_shake_bus/0/1428565236d03d864b7f43db9ce34df5f720509d0e/0"
            },
                apply_reason = "test"
            };
            var result = ShakeAroundApi.Register(accessToken, testData, IndustryId.代运营商_代运营商);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void DeviceApplyTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            var result = ShakeAroundApi.DeviceApply(accessToken, 1, "测试", "测试");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        [TestMethod]
        public void UploadImageTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            string file = @"E:\测试.jpg";

            var result = ShakeAroundApi.UploadImage(accessToken, file);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }

        // [TestMethod]
        // public void DeviceListTest()
        // {
        // var accessToken = AccessTokenContainer.GetAccessToken(_appId);
        // var date = Senparc.Weixin.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Today);
        // var result = ShakeAroundApi.GroupUpdate(accessToken, date, "1");
        //Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        // Assert.IsNotNull(result.devices);
        // Assert.AreEqual("10097", result.devices[0].device_id);

        // }
    }
}