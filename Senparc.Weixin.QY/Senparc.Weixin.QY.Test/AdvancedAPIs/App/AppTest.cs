using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.App;
using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Test.CommonApis;

namespace Senparc.Weixin.QY.Test.AdvancedAPIs
{
    /// <summary>
    /// CommonApiTest 的摘要说明
    /// </summary>
    [TestClass]
    public partial class AppTest : CommonApiTest
    {
        [TestMethod]
        public void GetAppInfoTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);
            var result = AppApi.GetAppInfo(accessToken, 2);

            Assert.IsNotNull(result.agentid);
            Assert.AreEqual(result.agentid, "2");
        }

        [TestMethod]
        public void SetAppTest()
        {
            var accessToken = AccessTokenContainer.GetToken(_corpId);

            SetAppPostData date = new SetAppPostData()
                {
                    agentid = "1",
                    description = "test",
                    isreportenter = 0,
                    isreportuser = 0,
                    logo_mediaid = "1muvdK7W8cjLfNqj0hWP89-CEhZNOVsktCE1JHSTSNpzTf7cGOXyDin_ozluwNZqi",
                    name = "Test",
                    redirect_domain = "www.weiweihi.com"
                };

            var result = AppApi.SetApp(accessToken, date);

            Assert.AreEqual(result.errcode, ReturnCode_QY.请求成功);
        }
    }
}
