using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.HttpUtility.Tests
{
    [TestClass]
    public class GetTest
    {
        [TestMethod]
        public void GetJsonTest()
        {
            return; //已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            var url =
                "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET";
            try
            {
                //这里因为参数错误，系统会返回错误信息
                WxJsonResult resultFail = Get.GetJson<WxJsonResult>(url);
                Assert.Fail(); //上一步就应该已经抛出异常
            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.不合法的APPID);
            }
        }

        [TestMethod]
        public void GetJsonAsyncTest()
        {
            //return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            var url =
                "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET";

            var t1 = Task.Factory.StartNew(async delegate { await Run(url); });
            var t2 = Task.Factory.StartNew(delegate { Run(url); });
            var t3 = Task.Factory.StartNew(delegate { Run(url); });
            var t4 = Task.Factory.StartNew(delegate { Run(url); });

            Console.WriteLine("Waiting...");
            Task.WaitAll(t1, t2, t3, t4);
        }

        private async Task Run(string url)
        {
            Console.WriteLine("Start Task.CurrentId：{0}，Time：{1}", Task.CurrentId, DateTime.Now.Ticks);

            try
            {
                //这里因为参数错误，系统会返回错误信息
                WxJsonResult resultFail = await Get.GetJsonAsync<WxJsonResult>(url);
                Assert.Fail(); //上一步就应该已经抛出异常
            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.不合法的APPID);

                Console.WriteLine("End Task.CurrentId：{0}，Time：{1}", Task.CurrentId, DateTime.Now.Ticks);
            }
        }
    }
}
