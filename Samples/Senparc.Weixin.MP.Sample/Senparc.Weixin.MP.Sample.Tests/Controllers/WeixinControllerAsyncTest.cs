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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.Weixin.MP.Sample.Tests.Controllers
{
    [TestClass]
    public class WeixinControllerAsyncTest
    {
        int requestNum = 100;
        ManualResetEvent allDone = new ManualResetEvent(false);

        private void RequestUrl(string address)
        {
            //运行此测试方法需要先准备好已经部署好且可以访问的网站，或运行VS的IIS Express。
            var url = new Uri(address);
            var finished = 0;
            for (int i = 0; i < requestNum; i++)
            {
                var request = WebRequest.Create(url);
                //request.Method = "POST";
                request.GetResponseAsync().ContinueWith(t =>
                {
                    var stream = t.Result.GetResponseStream();
                    using (TextReader tr = new StreamReader(stream))
                    {
                        Console.WriteLine(tr.ReadToEnd());
                    }

                    finished++;
                });

            }

            while (finished < requestNum)
            {
                //等待所有请求都返回结果
            }

        }

        [TestMethod]
        public void SyncTest()
        {
            RequestUrl("http://localhost:18666/Weixin/ForTest");
        }

        [TestMethod]
        public void AsyncTest()
        {
            RequestUrl("http://localhost:18666/WeixinAsync/ForTest");
        }
    }
}
