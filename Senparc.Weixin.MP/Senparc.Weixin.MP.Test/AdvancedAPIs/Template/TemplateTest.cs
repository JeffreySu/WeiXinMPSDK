using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Test.CommonAPIs;

namespace Senparc.Weixin.MP.Test.AdvancedAPIs.Template
{
    public class TestTemplateData
    {
        public TemplateDataItem first { get; set; }
        public TemplateDataItem time { get; set; }
        public TemplateDataItem ip_list { get; set; }
        public TemplateDataItem sec_type { get; set; }
        public TemplateDataItem remark { get; set; }

    }

    [TestClass]
    public class TemplateTest : CommonApiTest
    {
        [TestMethod]
        public void SendTemplateMessageTest()
        {
            var openId = "oUzG0jopq4-CpPiOMjFFw7pfYhlQ";//换成已经关注用户的openId
            var templateId = "7y88eQNY_231KgUhPum-_9BiuiMTd_1kBOsNHL_I5bA";//换成已经在微信后台添加的模板Id
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var testData = new TestTemplateData()
            {
                first = new TemplateDataItem("【测试】您好，我们发现您在微微嗨上的应用存在安全问题。"),
                time = new TemplateDataItem(DateTime.Now.ToString()),
                ip_list = new TemplateDataItem("172.23.1.1"),
                sec_type = new TemplateDataItem("重试密码次数太多"),
                remark = new TemplateDataItem("更详细信息，请到微微嗨官方网站（http://www.weiweihi.com）查看！")
            };
            var result = MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(accessToken, openId, templateId, "#FF0000", "http://www.weiweihi.com", testData);

            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }
    }
}
