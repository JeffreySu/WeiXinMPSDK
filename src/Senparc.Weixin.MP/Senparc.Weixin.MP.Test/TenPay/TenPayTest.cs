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
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //需要通过微信支付审核的账户才能成功
    [TestClass]
    public class TenPayTest : CommonApiTest
    {

        [TestMethod]
        public void NativePayTest()
        {
            var result = Senparc.Weixin.TenPay.V2.TenPay.NativePay("[appId]", "[timesTamp]", "[nonceStr]", "[productId]", "[sign]");
            Console.Write(result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DelivernotifyTest()
        {
            var result = Senparc.Weixin.TenPay.V2.TenPay.Delivernotify("[appId]", "[openId]", "[transId]", "[out_Trade_No]", "[deliver_TimesTamp]", "[deliver_Status]", "[deliver_Msg]", "[app_Signature]", "sha1");
            Console.Write(result);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OrderqueryTest()
        {
            var result = Senparc.Weixin.TenPay.V2.TenPay.Orderquery("[appId]", "[accesstoken]","[package]", "[timesTamp]", "[app_Signature]", "[sign_Method]");
            Console.Write(result);
            Assert.IsNotNull(result);
        }
    }
}
