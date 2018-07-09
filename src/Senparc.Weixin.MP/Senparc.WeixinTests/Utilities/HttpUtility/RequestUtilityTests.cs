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
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.HttpUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.HttpUtility.Tests
{
    [TestClass()]
    public class RequestUtilityTests
    {
        [TestMethod()]
        public void SetHttpProxyTest()
        {
            //设置
            RequestUtility.SetHttpProxy("http://192.168.1.130", "8088", "username", "pwd");

            //清除
            RequestUtility.RemoveHttpProxy();
        }


        [TestMethod]
        public void PostTest()
        {
            var data = "Jeffrey";
            Stream stream = new MemoryStream();
            var bytes = Encoding.UTF8.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            var cookieContainer = new CookieContainer();
            var url = "http://localhost:65395/ForTest/PostTest";//使用.NET 4.5的Sample
            var result = RequestUtility.HttpPost(url,
                cookieContainer, stream, useAjax: true);

            Console.WriteLine(result);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void SenparcHttpResponseTest()
        {
            var data = "Jeffrey";
            Stream stream = new MemoryStream();
            var bytes = Encoding.UTF8.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            var cookieContainer = new CookieContainer();
            var url = "http://localhost:65395/ForTest/PostTest";//使用.NET 4.5的Sample
            var result = RequestUtility.HttpResponsePost(url,
                cookieContainer, stream, useAjax: true);

            Assert.IsNotNull(result);
#if !NET45
            var resultString = result.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine("resultString:{0}", resultString);
#endif
            var cookie = cookieContainer.GetCookies(new Uri("http://localhost:65395"));
            Console.WriteLine("TestCookie：{0}", cookie["TestCookie"]);
        }


    }
}