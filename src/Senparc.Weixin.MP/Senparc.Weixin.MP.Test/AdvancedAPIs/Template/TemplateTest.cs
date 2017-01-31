using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            //var accessToken = AccessTokenContainer.GetAccessToken(_appId);
var testData = new //TestTemplateData()
{
    first = new TemplateDataItem("【测试】您好，审核通过。"),
    keyword1 = new TemplateDataItem(openId),
    keyword2 = new TemplateDataItem("单元测试"),
    keyword3 = new TemplateDataItem(DateTime.Now.ToString()),
    remark = new TemplateDataItem("更详细信息，请到Senparc.Weixin SDK官方网站（http://sdk.weixin.senparc.com）查看！")
};
            var result = MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(_appId, openId, templateId, "http://sdk.weixin.senparc.com", testData);

            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }

        [TestMethod]
        public void SendTemplateMessageTestForBook()
        {
var openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//消息目标用户的OpenId
            var templateId = "OYi8VMdCd3uu05lO7c_hNMoP2tCFTwHpChSNxpNJAGs";

//实际生产环境中，用户信息应该从数据库或缓存中读取
var userInfo = UserApi.Info(_appId,openId);

var data = new
{
    first = new TemplateDataItem("您的订单已经支付"),
    keyword1 = new TemplateDataItem(userInfo.nickname),
    keyword2 = new TemplateDataItem("1234567890"),
    keyword3 = new TemplateDataItem(88.ToString("c")),
    keyword4 = new TemplateDataItem("模板消息测试商品"),
    remark = new TemplateDataItem("更详细信息，请到Senparc.Weixin SDK官方网站（http://sdk.weixin.senparc.com）查看！")
};


            var result = TemplateApi.SendTemplateMessage(_appId, openId, templateId, "http://sdk.weixin.senparc.com", data);

            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }


        [TestMethod]
        public void SetIndustryTest()
        {
//var accessToken = AccessTokenContainer.GetAccessToken(_appId);
var result = TemplateApi.SetIndustry(_appId, IndustryCode.IT科技_互联网_电子商务, IndustryCode.IT科技_IT软件与服务);

            Assert.IsNotNull(result);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }

        [TestMethod]
        public void GetIndustryTest()
        {
            //var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = TemplateApi.GetIndustry(_appId);
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
            var result = TemplateApi.Addtemplate(accessToken, "OPENTM207498902");
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
            Assert.IsNotNull(result.template_id);
            Console.WriteLine(result.template_id);
        }
        [TestMethod]
        public void GetPrivateTemplateTest()
        {
            //var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = TemplateApi.GetPrivateTemplate(_appId);

            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
            Assert.IsNotNull(result.template_list);
            Assert.AreEqual("cCh2CTTJIbVZkcycDF08n96FP-oBwyMVrro8C2nfVo4", result.template_list[0].template_id);
        }
        [TestMethod]
        public void DelPrivateTemplateTest()
        {
            //var accessToken = AccessTokenContainer.GetAccessToken(_appId);

            //添加模板
            var addResult = TemplateApi.Addtemplate(_appId, "OPENTM206164559");
            var templateId = addResult.template_id;
            Assert.IsNotNull(templateId);

            //获取模板
            var templates = TemplateApi.GetPrivateTemplate(_appId).template_list;
            Assert.IsTrue(templates.FirstOrDefault(z => z.template_id == templateId) != null);

            //删除模板
            var result = TemplateApi.DelPrivateTemplate(_appId, templateId);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);

            //验证模板已删除
            templates = TemplateApi.GetPrivateTemplate(_appId).template_list;
            Assert.IsTrue(templates.FirstOrDefault(z => z.template_id == templateId) == null);
        }

        #region 异步方法测试
        [TestMethod()]
        public void SendTemplateMessageAsyncTest()
        {
            var openId = base._testOpenId;
            var templateId = "cCh2CTTJIbVZkcycDF08n96FP-oBwyMVrro8C2nfVo4";
            base.TestAyncMethod(2, base._testOpenId, () =>
            {
                /*
                * 详细内容
                {{first.DATA}}
                用户名：{{keyword1.DATA}}
                标题：{{keyword2.DATA}}
                时间：{{keyword3.DATA}}
                {{remark.DATA}}
                */
                var testData = new //TestTemplateData()
                {
                    first = new TemplateDataItem(string.Format("【模板消息测试-{0}】您好，审核通过。", DateTime.Now.ToString("T"))),
                    keyword1 = new TemplateDataItem(openId),
                    keyword2 = new TemplateDataItem("单元测试"),
                    keyword3 = new TemplateDataItem(DateTime.Now.ToString("O")),
                    remark = new TemplateDataItem("更详细信息，请到Senparc.Weixin SDK官方网站（http://sdk.weixin.senparc.com）查看！\r\n运行线程：" + Thread.CurrentThread.GetHashCode())
                };

                var result = TemplateApi.SendTemplateMessageAsync(base._appId, openId, templateId, null, testData).Result;
                Console.WriteLine("线程{0},结果：{1}", Thread.CurrentThread.GetHashCode(), result.errmsg);

                base.AsyncThreadsCollection.Remove(Thread.CurrentThread);//必须要加
            });
        }
        #endregion
    }
}
