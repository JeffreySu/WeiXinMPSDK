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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Entities.TemplateMessage;
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

    /// <summary>
    /// “订单支付成功通知”模板消息数据定义
    /// </summary>
    public class TemplateMessage_PaySuccessNotice : TemplateMessageBase
    {
        public TemplateDataItem first { get; set; }
        public TemplateDataItem keyword1 { get; set; }
        public TemplateDataItem keyword2 { get; set; }
        public TemplateDataItem keyword3 { get; set; }
        public TemplateDataItem keyword4 { get; set; }
        public TemplateDataItem remark { get; set; }

        /// <summary>
        /// “订单支付成功通知”模板消息数据定义 构造函数
        /// </summary>
        /// <param name="_first">first.Data头部信息</param>
        /// <param name="userName">用户名</param>
        /// <param name="orderNumber">订单号</param>
        /// <param name="orderAmount">订单金额</param>
        /// <param name="productInfo">商品信息</param>
        /// <param name="_remark">remark.Data备注</param>
        /// <param name="url"></param>
        /// <param name="templateId"></param>
        public TemplateMessage_PaySuccessNotice(string _first, string userName,
            string orderNumber, string orderAmount, string productInfo,
            string _remark,
            string templateId = "OYi8VMdCd3uu05lO7c_hNMoP2tCFTwHpChSNxpNJAGs",
            string url = null)
            : base(templateId, url, "订单支付成功通知")
        {
            /* 模板格式
                {{first.DATA}}
                用户名：{{keyword1.DATA}}
                订单号：{{keyword2.DATA}}
                订单金额：{{keyword3.DATA}}
                商品信息：{{keyword4.DATA}}
                {{remark.DATA}}
                */

            first = new TemplateDataItem(_first);
            keyword1 = new TemplateDataItem(userName);
            keyword2 = new TemplateDataItem(orderNumber);
            keyword3 = new TemplateDataItem(orderAmount, "#ff0000");//显示为红色
            keyword4 = new TemplateDataItem(productInfo);
            remark = new TemplateDataItem(_remark);
        }
    }

    [TestClass]
    public class TemplateTest : CommonApiTest
    {
        #region SendTemplateMessage API

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
                keyword3 = new TemplateDataItem(SystemTime.Now.DateTime.ToString()),
                remark = new TemplateDataItem("更详细信息，请到Senparc.Weixin SDK官方网站（https://sdk.weixin.senparc.com）查看！")
            };
            var result = MP.AdvancedAPIs.TemplateApi.SendTemplateMessage(_appId, openId, templateId, "https://sdk.weixin.senparc.com", testData);

            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }


        [TestMethod]
        public void SendTemplateMessageTestForBook()
        {
            var openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//消息目标用户的OpenId
            var templateId = "OYi8VMdCd3uu05lO7c_hNMoP2tCFTwHpChSNxpNJAGs";

            //实际生产环境中，用户信息应该从数据库或缓存中读取
            var userInfo = UserApi.Info(_appId, openId);

            var data = new
            {
                first = new TemplateDataItem("您的订单已经支付"),
                keyword1 = new TemplateDataItem(userInfo.nickname),
                keyword2 = new TemplateDataItem("1234567890"),
                keyword3 = new TemplateDataItem(88.ToString("c"), "#ff0000"),//显示为红色
                keyword4 = new TemplateDataItem("模板消息测试商品"),
                remark = new TemplateDataItem("更详细信息，请到Senparc.Weixin SDK官方网站（https://sdk.weixin.senparc.com）查看！")
            };


            var result = TemplateApi.SendTemplateMessage(_appId, openId, templateId, "https://sdk.weixin.senparc.com", data);

            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }

        [TestMethod]
        public void SendTemplateMessageTestForBookOptmize()
        {
            var openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//消息目标用户的OpenId

            //实际生产环境中，用户信息应该从数据库或缓存中读取
            var userInfo = UserApi.Info(_appId, openId);

            var data = new TemplateMessage_PaySuccessNotice(
                "您的订单已经支付", userInfo.nickname,
                "1234567890", 88.ToString("c"),
                "模板消息测试商品",
                "更详细信息，请到Senparc.Weixin SDK官方网站（https://sdk.weixin.senparc.com）查看！\r\n这条消息使用的是优化过的方法，且不带Url参数。");

            var result = TemplateApi.SendTemplateMessage(_appId, openId, data);

            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }

        [TestMethod]
        public void AsyncSendTemplateMessageTestForBookOptmize()
        {
            WxJsonResult finalResult = null;
            Task.Factory.StartNew(async () =>
            {
                var openId = "olPjZjsXuQPJoV0HlruZkNzKc91E";//消息目标用户的OpenId

                //实际生产环境中，用户信息应该从数据库或缓存中读取
                var userInfo = await UserApi.InfoAsync(_appId, openId);

                var data = new TemplateMessage_PaySuccessNotice(
                    "您的订单已经支付", userInfo.nickname,
                    "1234567890", 88.ToString("c"),
                    "模板消息测试商品",
                    "更详细信息，请到Senparc.Weixin SDK官方网站（https://sdk.weixin.senparc.com）查看！\r\n这条消息使用的是优化过的方法，且不带Url参数。使用异步方法。");

                var result = await TemplateApi.SendTemplateMessageAsync(_appId, openId, data);

                //调用客服接口显示msgId
                finalResult = await CustomApi.SendTextAsync(_appId, openId, "上一条模板消息的MsgID：" + result.msgid);

                Assert.AreEqual(ReturnCode.请求成功, result.errcode);

            });
            while (finalResult == null)
            {

            }
        }

        [TestMethod]
        public void SetIndustryTest()
        {
            //var accessToken = AccessTokenContainer.GetAccessToken(_appId);
            var result = TemplateApi.SetIndustry(_appId, IndustryCode.IT科技_互联网_电子商务, IndustryCode.IT科技_IT软件与服务);

            Assert.IsNotNull(result);
            Assert.AreEqual(ReturnCode.请求成功, result.errcode);
        }

        #endregion


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
                    first = new TemplateDataItem(string.Format("【模板消息测试-{0}】您好，审核通过。", SystemTime.Now.ToString("T"))),
                    keyword1 = new TemplateDataItem(openId),
                    keyword2 = new TemplateDataItem("单元测试"),
                    keyword3 = new TemplateDataItem(SystemTime.Now.Ticks.ToString("O")),
                    remark = new TemplateDataItem("更详细信息，请到Senparc.Weixin SDK官方网站（https://sdk.weixin.senparc.com）查看！\r\n运行线程：" + Thread.CurrentThread.GetHashCode())
                };

                var result = TemplateApi.SendTemplateMessageAsync(base._appId, openId, templateId, null, testData).Result;
                Console.WriteLine("线程{0},结果：{1}", Thread.CurrentThread.GetHashCode(), result.errmsg);

                base.AsyncThreadsCollection.Remove(Thread.CurrentThread);//必须要加
            });
        }
        #endregion
    }
}
