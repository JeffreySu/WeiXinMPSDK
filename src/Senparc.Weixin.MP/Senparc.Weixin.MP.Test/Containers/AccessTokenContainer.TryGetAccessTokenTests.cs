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
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Test.CommonAPIs;
using Senparc.CO2NET;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Cache.Redis;
using Senparc.CO2NET.Extensions;
//using Senparc.WeixinTests;

namespace Senparc.Weixin.MP.Test.Containers.Tests
{
    //测试未注册情况下， AccessTokenContainer.TryGetAccessToken 的执行情况
    //说明：单元测试始终能通过，但是在有UI的情况下情况会不一样，所以仍然需要在UI中进行测试
    [TestClass]
    public class TryGetAccessTokenTests : CommonApiTest
    {
        public TryGetAccessTokenTests()
            : base(false)//不注册
        {
        }

        [TestMethod]
        public void TryGetAccessTokenTest()
        {
            //    //清除注册信息
            //    AccessTokenContainer.RemoveFromCache(base._appId);

            //直接调用
            var result = AccessTokenContainer.TryGetAccessToken(base._appId, base._appSecret, false);
            Assert.IsNotNull(result);
            Console.WriteLine(result.ToJson());
        }
    }
}
