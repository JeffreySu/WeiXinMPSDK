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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.Weixin.MP.Test
{
    using Senparc.Weixin.MP;

    [TestClass]
    public class CheckSignatureTest
    {
        [TestMethod]
        public void GetSignatureTest()
        {
            //2013-01-12 09:12:00 118.244.133.118 GET /weixin signature=335b26997f05c82243aad6b10a2d1853637e71a8&echostr=5833258582299191661&timestamp=1357982061&nonce=1358161344 80 - 101.226.89.83 Mozilla/4.0 200 0 0 35
            {
                var signature = "335b26997f05c82243aad6b10a2d1853637e71a8";
                var echostr = "echostr";
                var timestamp = "1357982061";
                var nonce = "1358161344";
                var token = "weixin";

                var result = CheckSignature.GetSignature(timestamp, nonce, token);
                Assert.AreEqual(signature, result);
            }

            {
                var signature = "efa917e2445cc9f0aa4386ca92f96de5a242e24e";
                var echostr = "5837835896451489886";
                var timestamp = "1359208540";
                var nonce = "1359227084";
                var token = "huhujm";

                var result = CheckSignature.GetSignature(timestamp, nonce, token);
                Assert.AreEqual(signature, result);
            }
        }
    }
}
