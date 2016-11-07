using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs.Tests
{
    [TestClass()]
    public class TemplateApiTests: CommonApiTest
    {
        [TestMethod()]
        public void SendTemplateMessageAsyncTest()
        {
            List<Thread> threadList = new List<Thread>();
            var openId = base._testOpenId;
            var templateId = "";
            int finishThreadsCount = 0;
            for (int i = 0; i < 3; i++)
            {
                var data = new {};
                Thread thread = new Thread(() =>
                {
                    var result = TemplateApi.SendTemplateMessageAsync(base._appId, openId, templateId, null, data).Result;
                    finishThreadsCount++;
                    Console.WriteLine("线程{0},结果：{1}",Thread.CurrentThread.GetHashCode(),result.errmsg);
                });
            }

            threadList.ForEach(z=>z.Start());

            while (finishThreadsCount<threadList.Count)
            {
                Thread.Sleep(100);
            }
        }
    }
}