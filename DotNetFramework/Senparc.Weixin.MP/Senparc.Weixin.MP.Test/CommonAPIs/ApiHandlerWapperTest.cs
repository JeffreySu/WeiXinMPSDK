using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.Weixin.MP.Test.CommonAPIs
{
    [TestClass]
    public class ApiHandlerWapperTest : CommonApiTest
    {
        [TestMethod]
        public void TryCommonApiTest()
        {
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
                var appId = MP.CommonAPIs.AccessTokenContainer.GetFirstOrDefaultAppId();
                var accessToken = MP.CommonAPIs.AccessTokenContainer.GetAccessToken(appId);
                Console.WriteLine("当前AccessToken：" + accessToken);

                var result = ApiHandlerWapper.TryCommonApi(Senparc.Weixin.MP.CommonAPIs.CommonApi.GetMenu, "12345678901234567890这是错误的AccessToken");
                Console.WriteLine("当前AccessToken：" + accessToken);

                Assert.IsNull(result);//如果传的是AccessToken，并且AccessToken，不会重新自动获取（因为AppId未知）。
            }
        }
    }
}
