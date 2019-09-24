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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.HttpUtility.Tests
{
    [TestClass]
    public class GetTest
    {
        [TestMethod]
        public void DownloadTest()
        {
            var url = "https://sdk.weixin.senparc.com/images/v2/ewm_01.png";
            using (FileStream fs = new FileStream(string.Format("qr-{0}.jpg", SystemTime.Now.Ticks), FileMode.OpenOrCreate))
            {
                Senparc.CO2NET.HttpUtility.Get.Download(url, fs);//下载
                fs.Flush();//直接保存，无需处理指针
            }

            using (MemoryStream ms = new MemoryStream())
            {
                Senparc.CO2NET.HttpUtility.Get.Download(url, ms);//下载
                ms.Seek(0, SeekOrigin.Begin);//将指针放到流的开始位置
                string base64Img = Convert.ToBase64String(ms.ToArray());//输出图片base64编码
                Console.WriteLine(base64Img);
            }
        }

        [TestMethod]
        public void GetJsonTest()
        {
            return; //已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。 TODO：迁移到 CO2NET

            {
                var url = "http://apistore.baidu.com/microservice/cityinfo?cityname=苏州";
                var result = Senparc.CO2NET.HttpUtility.Get.GetJson<dynamic>(url);
                Assert.IsNotNull(result);
                Assert.AreEqual(0, result["errNum"]);
                Assert.AreEqual("苏州", result["retData"]["cityName"]);

                Console.WriteLine(result.GetType());
            }

            {
                var url =
             Config.ApiMpHost + "/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET";
                try
                {
                    //这里因为参数错误，系统会返回错误信息
                    WxJsonResult resultFail = Senparc.CO2NET.HttpUtility.Get.GetJson<WxJsonResult>(url);
                    Assert.Fail(); //上一步就应该已经抛出异常
                }
                catch (ErrorJsonResultException ex)
                {
                    //实际返回的信息（错误信息）
                    Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.不合法的APPID);
                }

            }

        }

        [TestMethod]
        public void GetJsonAsyncTest()
        {
            //return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            var url =
                Config.ApiMpHost + "/cgi-bin/token?grant_type=client_credential&appid=APPID&secret=APPSECRET";

            var t1 = Task.Factory.StartNew(async delegate { await Run(url); });
            var t2 = Task.Factory.StartNew(delegate { Run(url); });
            var t3 = Task.Factory.StartNew(delegate { Run(url); });
            var t4 = Task.Factory.StartNew(delegate { Run(url); });

            Console.WriteLine("Waiting...");
            Task.WaitAll(t1, t2, t3, t4);
        }

        private async Task Run(string url)
        {
            Console.WriteLine("Start Task.CurrentId：{0}，Time：{1}", Task.CurrentId, SystemTime.Now.Ticks);

            try
            {
                //这里因为参数错误，系统会返回错误信息
                WxJsonResult resultFail = await Senparc.CO2NET.HttpUtility.Get.GetJsonAsync<WxJsonResult>(url);
                Assert.Fail(); //上一步就应该已经抛出异常
            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.不合法的APPID);

                Console.WriteLine("End Task.CurrentId：{0}，Time：{1}", Task.CurrentId, SystemTime.Now.Ticks);
            }
        }
    }
}
