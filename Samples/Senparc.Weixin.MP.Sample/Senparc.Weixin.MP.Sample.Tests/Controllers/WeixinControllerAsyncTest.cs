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
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.Sample.Controllers;
using System.Web.Mvc;
using Senparc.Weixin.MP.Sample.Tests.Mock;
using System.Threading.Tasks;
using Senparc.Weixin.Helpers.Extensions;

namespace Senparc.Weixin.MP.Sample.Tests.Controllers
{
    [TestClass]
    public class WeixinControllerAsyncTest : WeixinControllerTest
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
            RequestUrl("http://localhost:65395/Weixin/ForTest");
        }

        [TestMethod]
        public void AsyncTest()
        {
            RequestUrl("http://localhost:65395/WeixinAsync/ForTest");
        }

        #region 异步 MessageHandler 事件方法测试

        protected void InitAsync(Controller targetAsync, Stream inputStreamAsync, string xmlFormat)
        {
            //target = StructureMap.ObjectFactory.GetInstance<WeixinController>();//使用IoC的在这里必须注入，不要直接实例化
            var xml = string.Format(xmlFormat, DateTimeHelper.GetWeixinDateTime(DateTime.Now));
            var bytes = System.Text.Encoding.UTF8.GetBytes(xml);

            inputStreamAsync.Write(bytes, 0, bytes.Length);
            inputStreamAsync.Flush();
            inputStreamAsync.Seek(0, SeekOrigin.Begin);

            targetAsync.SetFakeControllerContext(inputStreamAsync);
        }

        int threadsCount = 5;//同时并发的线程数
        int finishedThreadsCount = 0;
        object AsyncMessageHandlerTestLock = new object();

        [TestMethod]
        public void AsyncMessageHandlerTest()
        {
            List<Thread> threadsCollection = new List<Thread>();
            StringBuilder sb = new StringBuilder();

            var dt1 = DateTime.Now;
            for (int i = 0; i < threadsCount; i++)
            {
                Thread thread = new Thread(async p =>
                {
                    //按钮测试
                    var xml = string.Format(string.Format(xmlEvent_ClickFormat, "SubClickRoot_Text"), DateTimeHelper.GetWeixinDateTime(DateTime.Now.AddDays(i)),DateTime.Now.AddDays(i).Ticks);
                    
                    var timestamp = "itsafaketimestamp";
                    var nonce = "whateveryouwant";
                    var signature = Senparc.Weixin.MP.CheckSignature.GetSignature(timestamp, nonce, WeixinAsyncController.Token);
                    var postModel = new PostModel()
                    {
                        Signature = signature,
                        Timestamp = timestamp,
                        Nonce = nonce
                    };

                    WeixinAsyncController targetAsync = new WeixinAsyncController();
                    Stream streamAsync = new MemoryStream();
                    InitAsync(targetAsync, streamAsync, xml);//初始化

                    var dtt1 = DateTime.Now;
                    var actual = await targetAsync.MiniPost(postModel)
                    //.ContinueWith(z =>
                    //{
                    //    var e = z.Exception;
                    //    return z.Result;
                    //})
                    as FixWeixinBugWeixinResult;

                    var dtt2 = DateTime.Now;

                    Assert.IsNotNull(actual);

                    lock (AsyncMessageHandlerTestLock)
                    {
                        sb.AppendLine("线程：" + p);
                        sb.AppendFormat("开始时间：{0}，总时间：{1}ms\r\n",dtt1.ToString("HH:mm:ss.ffff"), (dtt2 - dtt1).TotalMilliseconds);
                        sb.AppendLine(actual.Content);

                        if (string.IsNullOrEmpty(actual.Content))
                        {
                            sb.AppendLine("actual.Content为空！");
                            Assert.Fail();
                        }

                        finishedThreadsCount++;
                    }
                })
                {
                    Name = "序列：" + i,
                };
                threadsCollection.Add(thread);
                thread.Start(thread.Name);
            }

            while (finishedThreadsCount < threadsCount)
            {

            }
            var dt2 = DateTime.Now;

            Console.WriteLine("总耗时：{0}ms", (dt2 - dt1).TotalMilliseconds);

            Console.WriteLine(sb.ToString());
        }

        #endregion
    }
}
