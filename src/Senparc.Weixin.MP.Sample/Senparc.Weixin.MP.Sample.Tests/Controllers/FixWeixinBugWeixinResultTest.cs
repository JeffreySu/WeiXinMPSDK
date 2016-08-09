using System;
using System.IO;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.MvcExtension;
using Senparc.Weixin.MP.Sample.Controllers;
using Senparc.Weixin.MP.Sample.Tests.Mock;

namespace Senparc.Weixin.MP.Sample.Tests.Controllers
{
    public partial class TestMessageHandler : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
        public TestMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            WeixinContext.ExpireMinutes = 3;
        }


        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "failed";
            TextResponseMessage = "success";//FixWeixinBugWeixinResult结果会优先选取TextReponseMessage
            return responseMessage;
        }


        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            return responseMessage;
        }
    }


    //已通过测试
    [TestClass]
    public class FixWeixinBugWeixinResultTest : BaseTest
    {
        WeixinController target;
        Stream inputStream;

        string xmlTextFormat = @"<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>{0}</CreateTime>
    <MsgType><![CDATA[text]]></MsgType>
    <Content><![CDATA[{1}]]></Content>
    <MsgId>5832509444155992350</MsgId>
</xml>
";

        /// <summary>
        /// 初始化控制器及相关请求参数
        /// </summary>
        /// <param name="xmlFormat"></param>
        private string InitXml(string xmlFormat, string content)
        {
            return string.Format(xmlTextFormat,
                                 Senparc.Weixin.Helpers.DateTimeHelper.GetWeixinDateTime(DateTime.Now), content);
        }

        [TestMethod]
        public void MvcExtensionTest()
        {
            //同时测试TextResponseMessage是否生效

            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms))
                {

                    string xml = InitXml(xmlTextFormat, "Hi, Senparc!");
                    sw.Write(xml);
                    sw.Flush();
                    ms.Seek(0, SeekOrigin.Begin);

                    var postModel = new PostModel();
                    var messageHandler = new TestMessageHandler(ms, postModel, 10);
                    messageHandler.Execute();

                    var result = new FixWeixinBugWeixinResult(messageHandler);
                    Assert.AreEqual("success",result.Content);
                }
            }
        }
    }
}
