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
using Senparc.Weixin.MP.AdvancedAPIs.Analysis;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs
{
    //已经通过测试
    [TestClass]
    public class AutoReplyTest : CommonApiTest
    {
        [TestMethod]
        public void ArticleSummaryTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = AutoReplyApi.GetCurrentAutoreplyInfo(accessToken);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.errcode, ReturnCode.请求成功);
        }
    }
}
