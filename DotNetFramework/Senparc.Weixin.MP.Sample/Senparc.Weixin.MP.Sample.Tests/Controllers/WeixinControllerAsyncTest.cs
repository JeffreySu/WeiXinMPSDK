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
