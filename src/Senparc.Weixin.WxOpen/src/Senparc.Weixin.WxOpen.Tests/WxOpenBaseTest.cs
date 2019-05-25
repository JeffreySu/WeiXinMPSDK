using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Test.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.WxOpen.Tests
{
    public class WxOpenBaseTest : CommonApiTest
    {
        public WxOpenBaseTest()
        {
            AccessTokenContainer.Register(base._wxOpenAppId, base._wxOpenAppSecret, "小程序单元测试");
        }
    }
}
