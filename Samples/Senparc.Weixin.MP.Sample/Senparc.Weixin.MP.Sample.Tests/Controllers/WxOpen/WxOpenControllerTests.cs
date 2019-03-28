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


using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Sample.Controllers.WxOpen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.Sample.Tests;
using Senparc.Weixin.MP.Sample.Tests.Mock;
using Senparc.Weixin.WxOpen.Containers;

namespace Senparc.Weixin.MP.Sample.Controllers.WxOpen.Tests
{
    [TestClass()]
    public class WxOpenControllerTests : BaseTest
    {
        WxOpenController target;

        /// <summary>
        /// 初始化控制器及相关请求参数
        /// </summary>
        /// <param name="xmlFormat"></param>
        private void Init()
        {
            //target = StructureMap.ObjectFactory.GetInstance<WeixinController>();//使用IoC的在这里必须注入，不要直接实例化
            target = new WxOpenController();

            var inputStream = new MemoryStream();
            target.SetFakeControllerContext(inputStream);
        }

        [TestMethod()]
        public void DecodeEncryptedDataTest()
        {
            Init();

            var sessionId = "ABCDEFG";
            var sessionKey = "/mGmINZAe+7k6kNz32wxSw==";
            var encryptedData =
                "CFcsIXmH2r0v9ehjEhS+uUpJkr8qGQyt+Za3YkhjVNNA+xGj2WB2QFxDXdKVSzc10LukeB2maCxZCqpPQrWQx6CKF/VkEx96hXpPuBMpWBnnLzupoJpkRW9gJGRz7dcXDnqzstf2etRumDeAFDyjEKZ6bqs+KTE7qHauMsctxg4TXPbzzvWQm783j9PoWsCm/0A+aGNWCfZSFuJgi5G+LjTVqcGqP+mlAnLIFmgGLTo3vWrekz0//2vCMhgcgwKjPMR+VZTB7UItvnWfF4h4oOajcMuEiwTifaFkyn7l4NtLroMYjOfId16B6XCTK0BvPhTw9GI3wPMDopwWF2q3Op8M2fYWJuVGFKbrAZvVY/ILeIxYLaHuwHAOYULLre5Mg1kQpURlQ6I6e6GjraJUoL1BqsM38DayY5xRRFJsehZgrWkOySWICuN20Bte7+2N8D6PvhsaNyQz+4Lp4XY/Nn+clNGoM1v6aKTCv7PY2wo=";
            var iv = "ASJ0whjRyLK1tvgb7bAVSw==";

            var unionId = "";//TODO：需要添加真实的UnionId
            SessionContainer.UpdateSession(sessionId, "OpenId", sessionKey, unionId);


            var result = target.DecodeEncryptedData("userInfo", sessionId, encryptedData, iv);
            Assert.IsNotNull(result);

        }
    }
}