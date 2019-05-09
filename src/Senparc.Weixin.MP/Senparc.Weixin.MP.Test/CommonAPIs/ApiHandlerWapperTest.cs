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
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs.User;

namespace Senparc.Weixin.MP.Test.CommonAPIs
{
    [TestClass]
    public class ApiHandlerWapperTest : CommonApiTest
    {
        [TestMethod]
        public void TryCommonApiTest()
        {
            //测试前先清空缓存

            //正确的测试
            {
                var result = ApiHandlerWapper.TryCommonApi(Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenu, base._appId);

                Assert.IsTrue(result.menu.button.Count > 0);
                Console.WriteLine(result.menu.button.Count);
            }

            //忽略appId
            {
                var result = ApiHandlerWapper.TryCommonApi(Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenu);
                Assert.IsTrue(result.menu.button.Count > 0);
                Console.WriteLine(result.menu.button.Count);
            }

            //错误的AccessToken
            {
                var appId = MP.Containers.AccessTokenContainer.GetFirstOrDefaultAppId(PlatformType.MP);
                var accessToken = MP.Containers.AccessTokenContainer.GetAccessToken(appId);
                Console.WriteLine("当前AccessToken：" + accessToken);

                try
                {
                    var result = ApiHandlerWapper.TryCommonApi(Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenu, "12345678901234567890这是错误的AccessToken");
                    Assert.Fail();//不应该执行到这里
                    Console.WriteLine("当前AccessToken：" + accessToken);
                }
                catch (WeixinMenuException ex)
                {
                    //应该执行到这里
                    Console.WriteLine(ex.Message);//如果传的是AccessToken，并且AccessToken，不会重新自动获取（因为AppId未知）。
                }
            }
        }

        [TestMethod]
        public void TryCommonApiAsyncTest()
        {
            //测试前先清空缓存

            //正确的测试
            {
                var finished = false;

                Task.Factory.StartNew(async () =>
                {
                    Func<string,Task<OpenIdResultJson>> func = appId => Senparc.Weixin.MP.AdvancedAPIs.UserApi.GetAsync(appId, null);

                        var result = await ApiHandlerWapper.TryCommonApiAsync(func, base._appId);

                    Assert.IsTrue(result.count> 0);
                    Console.WriteLine(result.count);

                    finished = true;

                    });

                while (!finished)
                {
                    Thread.Sleep(100);
                }

            }

            ////忽略appId
            //{
            //    var result = ApiHandlerWapper.TryCommonApi(Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenu);
            //    Assert.IsTrue(result.menu.button.Count > 0);
            //    Console.WriteLine(result.menu.button.Count);
            //}

            ////错误的AccessToken
            //{
            //    var appId = MP.Containers.AccessTokenContainer.GetFirstOrDefaultAppId();
            //    var accessToken = MP.Containers.AccessTokenContainer.GetAccessToken(appId);
            //    Console.WriteLine("当前AccessToken：" + accessToken);

            //    try
            //    {
            //        var result = ApiHandlerWapper.TryCommonApi(Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenu, "12345678901234567890这是错误的AccessToken");
            //        Assert.Fail();//不应该执行到这里
            //        Console.WriteLine("当前AccessToken：" + accessToken);
            //    }
            //    catch (WeixinMenuException ex)
            //    {
            //        //应该执行到这里
            //        Console.WriteLine(ex.Message);//如果传的是AccessToken，并且AccessToken，不会重新自动获取（因为AppId未知）。
            //    }
            //}
        }
    }
}
