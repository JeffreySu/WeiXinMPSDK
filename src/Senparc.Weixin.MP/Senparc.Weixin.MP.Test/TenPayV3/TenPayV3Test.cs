#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using Senparc.Weixin.MP.TenPayLib;
using Senparc.Weixin.MP.TenPayLibV3;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //需要通过微信支付审核的账户才能成功
    [TestClass]
    public class TenPayV3Test : CommonApiTest
    {

        protected string data = @"<xml><spbill_create_ip><![CDATA[8.8.8.8]]></spbill_create_ip>
<notify_url><![CDATA[www.weiweihi.com]]></notify_url>
<out_trade_no><![CDATA[1833431771763549]]></out_trade_no>
<appid><![CDATA[wxbe855a981c34aa3f]]></appid>
<total_fee>1</total_fee>
<nonce_str><![CDATA[D554F7BB7BE44A7267068A7DF88DDD20]]></nonce_str>
<sign><![CDATA[4A3E3CF7205C9A319010ABE2A49F4F65]]></sign>
<openid><![CDATA[o3IHxjrPzMVZIJOgYMH1PyoTW_Tg]]></openid>
<mch_id><![CDATA[10017762]]></mch_id>
<trade_type><![CDATA[JSAPI]]></trade_type>
<body><![CDATA[test]]></body>
</xml>";

        [TestMethod]
        public void UnifiedorderTest()
        {
            var result = TenPayV3.Unifiedorder(data);
            Console.Write(result);
            Assert.IsNotNull(result);
        }
    }
}
