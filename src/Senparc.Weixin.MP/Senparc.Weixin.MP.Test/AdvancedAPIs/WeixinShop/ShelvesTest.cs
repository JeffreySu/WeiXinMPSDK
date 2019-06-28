#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.MerChant;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

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
