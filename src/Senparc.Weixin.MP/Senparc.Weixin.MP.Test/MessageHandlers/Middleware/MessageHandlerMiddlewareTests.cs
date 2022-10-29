#if !NET462
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.HttpUtility;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageContexts;
using Senparc.Weixin.MP.MessageHandlers.Middleware;
using Senparc.Weixin.MP.Test.MessageHandlers;
using Senparc.Weixin.MP.Test.NetCore3.MessageHandlers.TestEntities;
using Senparc.WeixinTests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.Test.NetCore3.MessageHandlers.Middleware
{
    [TestClass]
    public class MessageHandlerMiddlewareTests : BaseTest
    {
        private Stream GetStream(string xml)
        {
            var ms = new MemoryStream();
            var sr = new StreamWriter(ms);
            sr.Write(xml);
            sr.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }


        [TestMethod]
        public async Task MessageHandlerMiddlewareInvokeTest()
        {
            var ecryptXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <Encrypt><![CDATA[wVQjGs46yqw0j5IhISmROAFui9lgiZB3VM1rYd73dYMzYJSvRbYQe0f+K+mo/4iGgrqnQA0FucnRBj/FNY7PIku/s4FZ+jC5kK/LXCgsdN/57w9qcatKvvRLDaPrLiUZ+AFBkFjSqkRUZWAgVbBk8tpwtt8R22m0BoNqLcV1n2gRjX05b/Lw+fEm//7tX+yu2f66PNN2GiRFGKbvMasVHXKdqIRqW4224C3p0G7YxPEHLSTH1AWMjl9mDvIbtgCIMQ/yZf+Cm27B+pscDD9ocPl5ruc92yRGTtcjYmd0bQxW1eBAJJiIpA9TzZKjIxwIyoJ3jK56GUu25iC6KuBIQi357JhygGLSaoC6TlWMlJFEIxtd2JKHVEzGNJ+LuQrU5jgnMLLhSxsq5u8r/VMbyKroXGpWNvu9irPrcMhC4L0=]]></Encrypt>
</xml>
";
            //var postModel = new PostModel()
            //{
            //    Signature = "330ed3b64e363dc876f35e54a79e59b48739f567",
            //    Msg_Signature = "20f4a1263d198b696e6958e0d65e928aa68f7d96",
            //    Timestamp = "1570032739",
            //    Nonce = "2068872452",

            //    Token = "weixin",
            //    EncodingAESKey = "mNnY5GekpChwqhy2c4NBH90g3hND6GeI4gii2YCvKLY",
            //    AppId = "wx669ef95216eef885"
            //};



            var contextMock = new Moq.Mock<HttpContext>();
            contextMock.Setup(z => z.Request.Query).Returns(() =>
            {
                var dic = new Dictionary<string, StringValues>();
                dic["nonce"] = "863153744";
                dic["timestamp"] = "1570075722";
                dic["msg_signature"] = "71dc359205a4660bc3b3046b643452c994b5897d";
                dic["signature"] = "330ed3b64e363dc876f35e54a79e59b48739f567";

                var query = new QueryCollection(dic);
                return query;
            });

            //TODO：此处并没有完全模拟成功 Post，致使 context.Request.GetRequestMemoryStream() 无法正确获取到数据

            var requestStream = GetStream(ecryptXml);
            contextMock.Setup(z => z.Request.Body).Returns(requestStream);
            contextMock.Setup(z => z.Request.Method).Returns("POST");
            contextMock.Setup(z => z.Features).Returns(new FeatureCollection());

            var messageHandlerMiddleware = new MpMessageHandlerMiddleware<DefaultMpMessageContext>(null, _serviceProvider, CustomMessageHandlers.GenerateMessageHandler, options =>
             {
                 options.DefaultMessageHandlerAsyncEvent = NeuChar.MessageHandlers.DefaultMessageHandlerAsyncEvent.SelfSynicMethod;
                 options.AccountSettingFunc = context => new SenparcWeixinSetting()
                 {
                     Token = "weixin",
                     EncodingAESKey = "YTJkZmVjMzQ5NDU5NDY3MDhiZWI0NTdiMjFiY2I5MmU",
                     WeixinAppId = "wx669ef95216eef885"
                 };
             });

            await messageHandlerMiddleware.Invoke(contextMock.Object);


        }
    }
}
#endif