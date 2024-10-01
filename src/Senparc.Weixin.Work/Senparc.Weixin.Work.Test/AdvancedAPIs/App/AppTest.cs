#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.App;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Test.CommonApis;
using System;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class AppTest : CommonApiTest
    {
        const int AGENT_ID = 1000009;

        [TestMethod]
        public void GetAppInfoTest()
        {
            {
                //使用AppKey测试
                //常规AccessToken测试
                var appKey = AccessTokenContainer.BuildingKey(_corpId, base._corpSecret);
                var result = AppApi.GetAppInfo(appKey, AGENT_ID);

                Assert.IsNotNull(result.agentid);
                Assert.AreEqual(result.agentid, AGENT_ID.ToString());
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }

            {
                //常规AccessToken测试
                var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
                var result = AppApi.GetAppInfo(accessToken, AGENT_ID);

                Assert.IsNotNull(result.agentid);
                Assert.AreEqual(result.agentid, AGENT_ID.ToString());
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }
        }

        [TestMethod]
        public async Task GetAppInfoAsyncTest()
        {
            {
                //使用AppKey测试
                //常规AccessToken测试
                var appKey = AccessTokenContainer.BuildingKey(_corpId, base._corpSecret);
                var result = await AppApi.GetAppInfoAsync(appKey, AGENT_ID);

                Assert.IsNotNull(result.agentid);
                Assert.AreEqual(result.agentid, AGENT_ID.ToString());

                Console.WriteLine("1.Ticket:" + SystemTime.Now.Ticks);
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }

            {
                //常规AccessToken测试
                var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);
                var result = await AppApi.GetAppInfoAsync(accessToken, AGENT_ID);

                Assert.IsNotNull(result.agentid);
                Assert.AreEqual(result.agentid, AGENT_ID.ToString());

                Console.WriteLine("2.Ticket:" + SystemTime.Now.Ticks);
                Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }
        }

        [TestMethod]
        public void SetAppTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId, base._corpSecret);

            SetAppPostData date = new SetAppPostData()
            {
                agentid = AGENT_ID.ToString(),//"100" + SystemTime.Now.ToString("yyMMddHHMM"),
                report_location_flag = "1",
                //logo_mediaid = "1muvdK7W8cjLfNqj0hWP89-CEhZNOVsktCE1JHSTSNpzTf7cGOXyDin_ozluwNZqi",
                name = "微信 SDK 单元测试添加" + SystemTime.Now.ToString("yyyyMMdd HH:mm"),
                description = "微信 SDK 单元测试",
                redirect_domain = "sdk.weixin.senparc.com",
                //isreportenter = 0,
                isreportuser = 1,
                home_url = "https://weixin.senparc.com",
            };

            var result = AppApi.SetApp(accessToken, date);

            Assert.AreEqual(result.errcode, ReturnCode_Work.请求成功);
        }
    }
}
