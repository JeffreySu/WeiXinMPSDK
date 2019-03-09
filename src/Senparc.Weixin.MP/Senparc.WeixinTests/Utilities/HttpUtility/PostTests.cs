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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.HttpUtility.Tests
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void PostGetJsonTest()
        {
            return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。  //TODO：迁移到 CO2NET 中测试
            var url = Config.ApiMpHost + "/cgi-bin/media/upload?access_token=TOKEN&type=image";
            try
            {
                //这里因为参数错误，系统会返回错误信息
                WxJsonResult resultFail = CO2NET.HttpUtility.Post.PostGetJson<WxJsonResult>(url, cookieContainer: null, formData: null, encoding: null);
                Assert.Fail();//上一步就应该已经抛出异常
            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.获取access_token时AppSecret错误或者access_token无效);
            }
        }

        [TestMethod()]
        public async Task PostGetJsonAsyncTest()
        {
            //return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            var url = Config.ApiMpHost + "/cgi-bin/media/upload?access_token=TOKEN&type=image";

            try
            {
                WxJsonResult resultFail =
                    await CO2NET.HttpUtility.Post.PostGetJsonAsync<WxJsonResult>(url, cookieContainer: null, formData: null,
                            encoding: null);
                //这里因为参数错误，系统会返回错误信息
                Assert.Fail(); //上一步就应该已经抛出异常

            }
            catch (ErrorJsonResultException ex)
            {
                //实际返回的信息（错误信息）
                Assert.AreEqual(ex.JsonResult.errcode, ReturnCode.获取access_token时AppSecret错误或者access_token无效);
                Console.WriteLine("Success");
            }
        }

        [TestMethod]
        public void PostGetJsonByFormDataTest()
        {
            return;//已经通过，但需要连接远程测试，太耗时，常规测试时暂时忽略。
            var url = "http://localhost:12222/P2P/GetPassport";
            try
            {
                //这里因为参数错误，系统会返回错误信息
                var formData = new Dictionary<string, string>();
                formData["appKey"] = "test";
                formData["secret"] = "test2";
                var resultFail = CO2NET.HttpUtility.Post.PostGetJson<object>(url, formData: formData);
            }
            catch (ErrorJsonResultException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DownLoadTest()
        {

        }


    }
}
