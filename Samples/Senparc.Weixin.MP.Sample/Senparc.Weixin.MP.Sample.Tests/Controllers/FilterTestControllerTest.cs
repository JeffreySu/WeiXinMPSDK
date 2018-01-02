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
using System.Web.Mvc;
using System.Web.Mvc.Async;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.Sample.Controllers;
using Senparc.Weixin.MP.Sample.Tests.Mock;

namespace Senparc.Weixin.MP.Sample.Tests.Controllers
{
    [TestClass]
    public class FilterTestControllerTest
    {
        FilterTestController target;

        private string weixinUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.31 (KHTML, like Gecko) MicroMessenger Chrome/26.0.1410.64 Safari/537.31";
        private string outsideUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.64 Safari/537.31";

        private void Init(string userAgent)
        {
            //target = StructureMap.ObjectFactory.GetInstance<FilterTestController>();//使用IoC的在这里必须注入，不要直接实例化
            target = new FilterTestController();
            target.SetFakeControllerContext(userAgent: userAgent);
        }

        [TestMethod]
        public void IndexTest()
        {
            var errorMsg = "访问被拒绝，请通过微信客户端访问！";
            var filter = new WeixinInternalRequestAttribute(errorMsg);

            {
                //模拟微信内置浏览器打开
                Init(weixinUserAgent);//初始化

                ActionDescriptor ad = new ReflectedActionDescriptor(target.GetType().GetMethod("Index"), "Index",
                                                        new ReflectedAsyncControllerDescriptor(target.GetType()));
                ActionExecutingContext aec = new ActionExecutingContext(target.ControllerContext, ad,
                                                                        new Dictionary<string, object>());
                filter.OnActionExecuting(aec);

                Assert.IsNull(aec.Result);
                
                //下面的测试和UserAgent无关，只要Index可以被调用，都会有同样的结果
                ContentResult actual = target.Index();
                Assert.IsTrue(actual.Content.Contains("访问正常"));
            }

            {
                //模拟外部浏览器打开
                Init(outsideUserAgent);//初始化

                ActionDescriptor ad = new ReflectedActionDescriptor(target.GetType().GetMethod("Index"), "Index",
                                                        new ReflectedAsyncControllerDescriptor(target.GetType()));
                ActionExecutingContext aec = new ActionExecutingContext(target.ControllerContext, ad,
                                                                        new Dictionary<string, object>());
                filter.OnActionExecuting(aec);

                Assert.IsNotNull(aec.Result);
                Assert.IsTrue(aec.Result is ContentResult);
                Assert.AreEqual(errorMsg, (aec.Result as ContentResult).Content);
            }
        }
    }
}
