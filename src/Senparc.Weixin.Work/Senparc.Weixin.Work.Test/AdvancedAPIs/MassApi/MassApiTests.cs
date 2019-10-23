using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Test.CommonApis;

namespace Senparc.Weixin.Work.Test.AdvancedAPIs.MassApi
{
    [TestClass]
    public class MassApiTests : CommonApiTest
    {
        [TestMethod]
        public void SendNewsTest()
        {
            /* 说明：此方法仅用于测试AccessToken是否可以被正确传递：
             * https://github.com/JeffreySu/WeiXinMPSDK/issues/964#issuecomment-351890440 */

            try
            {
                var accessToken =
                    "fM5CruClicaXYpz9vai-nl2lB2V-S25Yed_BDmd8sl6P1vBvExBnfoYbOrYAMEawhZc5bhX1mu0nVJilrBxXyeAs-7y70gkwJIEXVrk4JvfFaDJP6GdWBDYq5l6tqfL8megghLqDHLXPsdokIJ6UmXrb8buPEUgdKPNWXNJKVeq32uqB54OaAnxNkQVK-MtbI_QgNT4grhUhuIbYak_7Gg";

                var result = Work.AdvancedAPIs.MassApi.SendNews(accessToken, "agentId", new List<Article>());
                Assert.Fail();
            }
            catch (Senparc.Weixin.Exceptions.UnRegisterAppIdException e)
            {
                Assert.Fail(); //出现反馈问题中的“AppId未注册”错误
            }
            catch (Senparc.Weixin.Exceptions.ErrorJsonResultException e)
            {
                //请求发生错误！错误代码：40014，说明：invalid access_token”
                Assert.AreEqual(40014, (int) e.JsonResult.errcode);

                //没有发生AppId未注册的错误
            }
            catch (Exception e)
            {
                Assert.Fail();//其他错误
            }
        }
    }
}
