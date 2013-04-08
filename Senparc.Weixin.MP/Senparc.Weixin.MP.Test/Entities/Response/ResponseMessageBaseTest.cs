using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test.Entities.Response
{
    [TestClass]
    public class ResponseMessageBaseTest
    {
        RequestMessageText requestMessage = new RequestMessageText()
                                            {
                                                MsgId = 1,
                                                CreateTime = DateTime.Now,
                                                FromUserName = "TNT2",
                                                ToUserName = "Senparc",
                                                //MsgType = RequestMsgType.Text,
                                                Content = "This is a text message."
                                            };

        [TestMethod]
        public void CreateFromRequestMessageTest()
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage(requestMessage, ResponseMsgType.Text);
            Assert.IsNotNull(responseMessage);
            Assert.AreEqual(ResponseMsgType.Text, responseMessage.MsgType);
            Assert.AreEqual("Senparc", responseMessage.FromUserName);
            Assert.AreEqual("TNT2", responseMessage.ToUserName);
        }

        [TestMethod]
        public void CreateFromRequestMessageGenericTest()
        {
            {
                var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
                Assert.IsNotNull(responseMessage);
                Assert.AreEqual(ResponseMsgType.Text, responseMessage.MsgType);
                Assert.AreEqual("Senparc", responseMessage.FromUserName);
                Assert.AreEqual("TNT2", responseMessage.ToUserName);
            }

            {
                var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);
                Assert.IsNotNull(responseMessage);
                Assert.AreEqual(ResponseMsgType.News, responseMessage.MsgType);
            }

            {
                var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageMusic>(requestMessage);
                Assert.IsNotNull(responseMessage);
                Assert.AreEqual(ResponseMsgType.Music, responseMessage.MsgType);
            }

            {
                try
                {
                    var responseMessage =
                        ResponseMessageBase.CreateFromRequestMessage<ResponseMessageBase>(requestMessage);
                    Assert.Fail();//上一步应该抛出异常，因为没有对应的ResponseMsgType
                }
                catch (WeixinException ex)
                {
                    
                }
            }
        }
    }
}
