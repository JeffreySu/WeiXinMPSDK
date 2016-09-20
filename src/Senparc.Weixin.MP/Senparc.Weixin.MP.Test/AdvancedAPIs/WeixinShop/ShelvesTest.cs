using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.Weixin.MP.TenPayLib;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //需要通过微信支付审核的账户才能成功
    [TestClass]
    public class ShelvesTest : CommonApiTest
    {
        [TestMethod]
        public void AddShelvesTest()
        {
            var m1 = new M1(2, 50);
            var groupIds =new int[]{49, 50, 51, 52};
            var m2 = new M2(groupIds);
            var m3 = new M3(49, "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg");
            var imgs = new string[] { "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg", "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg", "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg", "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg" };
            var m4 = new M4(groupIds, imgs);
            var m5 = new M5(groupIds, "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg");
            var result = ShelfApi.AddShelves("accessToken", m1, m2, m3, m4, m5, "http://img0.bdstatic.com/img/image/shouye/dengni57.jpg", "测试货架");
            Console.Write(result);
            Assert.IsNotNull(result);
        }
    }
}
