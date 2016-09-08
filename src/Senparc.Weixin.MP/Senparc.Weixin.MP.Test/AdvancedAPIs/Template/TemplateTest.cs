using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
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
            var openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//换成已经关注用户的openId
            var templateId = "cCh2CTTJIbVZkcycDF08n96FP-oBwyMVrro8C2nfVo4";//换成已经在微信后台添加的模板Id
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var testData = new //TestTemplateData()
            {
                first = new TemplateDataItem("【测试】您好，审核通过。"),
                keyword1 = new TemplateDataItem(openId),
                keyword2 = new TemplateDataItem("单元测试"),
                keyword3 = new TemplateDataItem(DateTime.Now.ToString()),
                remark = new TemplateDataItem("更详细信息，请到Senparc.Weixin SDK官方网站（http://sdk.weixin.senparc.com）查看！")
            };
            var result = MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(accessToken, openId, templateId, "http://sdk.weixin.senparc.com", testData);

            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }


        [TestMethod]
        public void SetIndustryTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = TemplateApi.SetIndustry(accessToken, IndustryCode.IT科技_互联网_电子商务, IndustryCode.IT科技_IT软件与服务);

            Assert.IsNotNull(result);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }

        [TestMethod]
        public void GetIndustryTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = TemplateApi.GetIndustry(accessToken);
            Assert.AreEqual("IT科技", result.primary_industry.first_class);
            Assert.AreEqual("互联网|电子商务", result.primary_industry.second_class);
            Assert.AreEqual("IT科技", result.secondary_industry.first_class);
            Assert.AreEqual("IT软件与服务", result.secondary_industry.second_class);

            Assert.AreEqual(IndustryCode.IT科技_互联网_电子商务, result.primary_industry.ConvertToIndustryCode());
            Assert.AreEqual(IndustryCode.IT科技_IT软件与服务, result.secondary_industry.ConvertToIndustryCode());

        }

        [TestMethod]
        public void AddtemplateTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = TemplateApi.Addtemplate(accessToken, "OPENTM206164559");
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
            Assert.IsNotNull(result.template_id);
            Console.WriteLine(result.template_id);
        }
        [TestMethod]
        public void GetPrivateTemplateTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = TemplateApi.GetPrivateTemplate(accessToken);

            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
            Assert.IsNotNull(result.template_list);
            Assert.AreEqual("cCh2CTTJIbVZkcycDF08n96FP-oBwyMVrro8C2nfVo4", result.template_list[0].template_id);
        }
        [TestMethod]
        public void DelPrivateTemplateTest()
        {
            var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            //添加模板
            var addResult = TemplateApi.Addtemplate(accessToken, "OPENTM206164559");
            var templateId = addResult.template_id;
            Assert.IsNotNull(templateId);

            //获取模板
            var templates = TemplateApi.GetPrivateTemplate(accessToken).template_list;
            Assert.IsTrue(templates.FirstOrDefault(z => z.template_id == templateId) != null);

            //删除模板
            var result = TemplateApi.DelPrivateTemplate(accessToken, templateId);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);

            //验证模板已删除
            templates = TemplateApi.GetPrivateTemplate(accessToken).template_list;
            Assert.IsTrue(templates.FirstOrDefault(z => z.template_id == templateId) == null);
        }

    }
}
