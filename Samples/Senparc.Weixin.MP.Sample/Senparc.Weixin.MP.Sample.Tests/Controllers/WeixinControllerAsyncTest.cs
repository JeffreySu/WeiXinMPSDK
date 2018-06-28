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
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.MP.Entities;
using Senparc.CO2NET.Helpers;

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

        int threadsCount = 500;//同时并发的线程数，超过500可能会导致StringBuilder溢出
        int finishedThreadsCount = 0;
        object AsyncMessageHandlerTestLock = new object();

        [TestMethod]
        public void AsyncMessageHandlerTestForRepeatOmit()
        {
            AsyncMessageHandlerTest(true);
            AsyncMessageHandlerTest(false);
        }

        /// <summary>
        /// 是否测试去重（这里只是提供方便测试的条件，需要Controller内配合打开去重功能）
        /// </summary>
        /// <param name="testRepeatOmit"></param>
        [TestMethod]
        public void AsyncMessageHandlerTest(bool testRepeatOmit = true)
        {
            Console.WriteLine("设置去重：" + testRepeatOmit);

            List<Thread> threadsCollection = new List<Thread>();
            StringBuilder sb = new StringBuilder();
            var emptyContentCount = 0;//空消息数量（一般是因为去重了）
            var repeatedMessageCount = 0;//确定去重的消息数量
            var repeatedRequestMessage = new List<IRequestMessageBase>();//被去重的请求消息记录

            var dt1 = DateTime.Now;

            //每个线程的测试过程
            ParameterizedThreadStart task = async p =>
            {
                //注意：执行到这里的时候，i可能已经不再是创建对象时的i了。
                var param = p as object[];
                var threadName = param[0];
                var index = Convert.ToInt32(param[1]);

                string msgId = null;
                string openId = dt1.Ticks.ToString();//对每一轮测试进行分组，防止串数据
                if (testRepeatOmit)
                {
                    msgId = DateTime.Today.Ticks.ToString();
                    openId += "0";
                }
                else
                {
                    //MsgId，保证每次时间不一样
                    msgId = (DateTime.Today.Ticks + index).ToString();
                    //OpenId后缀，可以模拟不同人发送，也可以模拟对人多发：i%10
                    //如果需要测试去重功能，可以将index改为固定值，并且将MsgId设为固定值
                    openId += index.ToString();
                }

                //按钮测试
                var xml = string.Format(string.Format(xmlEvent_ClickFormat, "SubClickRoot_Text"),
                    ////确保每次时间戳不同
                    DateTimeHelper.GetWeixinDateTime(DateTime.Now.AddHours(index)),
                    msgId, openId);

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
                var actual = await targetAsync.Post(postModel) as FixWeixinBugWeixinResult;

                var dtt2 = DateTime.Now;

                Assert.IsNotNull(actual);

                lock (AsyncMessageHandlerTestLock)
                {
                    sb.AppendLine("线程：" + threadName);
                    sb.AppendFormat("开始时间：{0}，总时间：{1}ms\r\n", dtt1.ToString("HH:mm:ss.ffff"), (dtt2 - dtt1).TotalMilliseconds);

                    if (string.IsNullOrEmpty(actual.Content))
                    {
                        sb.AppendLine("actual.Content为空！！！！！！！！！！！！！！");
                        emptyContentCount++;
                    }
                    else if (targetAsync.MessageHandler.ResponseMessage is ResponseMessageText)
                    {
                        sb.AppendLine((targetAsync.MessageHandler.ResponseMessage as ResponseMessageText).Content);
                    }

                    if (targetAsync.MessageHandler.MessageIsRepeated)
                    {
                        sb.AppendLine("消息已去重！！！！！！！！！！！！！！！！！！");
                        repeatedRequestMessage.Add(targetAsync.MessageHandler.RequestMessage);
                        repeatedMessageCount++;
                    }

                    sb.AppendLine("RequestMessage数量：" + targetAsync.MessageHandler.CurrentMessageContext.RequestMessages.Count);
                    sb.AppendLine("OpenId：" + targetAsync.MessageHandler.RequestMessage.FromUserName);

                    sb.AppendLine();
                    finishedThreadsCount++;
                }
            };


            for (int i = 0; i < threadsCount; i++)
            {
                Thread thread = new Thread(task)
                {
                    Name = "线程序列：" + i,
                };
                threadsCollection.Add(thread);
                thread.Start(new object[] { thread.Name, i });
            }

            while (finishedThreadsCount < threadsCount)
            {

            }
            var dt2 = DateTime.Now;

            Console.WriteLine("总耗时：{0}ms", (dt2 - dt1).TotalMilliseconds);
            Console.WriteLine("Empty Content Count：" + emptyContentCount);
            Console.WriteLine("Repeated Request Count：" + repeatedMessageCount);

            //如果测试去重，则重复数量应该为[总数-1]，否则应该为0
            if (testRepeatOmit)
            {
                Assert.AreEqual(threadsCount - 1, repeatedMessageCount);
            }
            else
            {
                Assert.AreEqual(0, repeatedMessageCount);
            }

            Console.WriteLine(sb.ToString());

            Console.WriteLine("============去重消息关键信息===========");
            foreach (var item in repeatedRequestMessage)
            {
                Console.WriteLine(item.CreateTime + "\t" + item.MsgId + "\t" + item.FromUserName);
            }
        }

        #endregion
    }
}
