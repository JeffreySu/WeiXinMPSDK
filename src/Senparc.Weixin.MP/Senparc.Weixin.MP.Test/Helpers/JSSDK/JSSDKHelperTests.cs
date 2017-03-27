#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Helpers.Tests
{
    [TestClass()]
    public class JSSDKHelperTests
    {
        [TestMethod()]
        public void GetSignatureTest()
        {
            var result = JSSDKHelper.GetSignature("kgt8ON7yVITDhtdwci0qefK1QvDlwsAPwMnZOO_J0MxaUpuHtIU_IltC7zs3kfNOYTHEqeIEvEXZHbS3xXNx3g", "B7EE6F5F9AA5CD17CA1AEA43CE848496", "1474350784", "https://www.baidu.com");
            Assert.IsNotNull(result);
            Assert.AreEqual("3b1b4171bcfa0f0661be9c5474002d3eb25a3368", result);
            Console.WriteLine(result);
        }
    }
}