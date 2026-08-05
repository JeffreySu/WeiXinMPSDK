using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.NeuChar;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageContexts;
using System.Globalization;
using System.Xml.Linq;

namespace Senparc.Weixin.MP.Test.ContextTests
{
    [TestClass]
    public class DefaultMpMessageContextTests
    {
        [TestMethod]
        public void MessageContextJsonConverterShouldFallbackWhenCachedEventFieldIsMissing()
        {
            var cachedJson = @"{
  ""AppId"": ""wx-test"",
  ""UserName"": ""openid"",
  ""RequestMessages"": [
    {
      ""MsgId"": null,
      ""MsgType"": 7,
      ""Encrypt"": null,
      ""ToUserName"": ""gh_test"",
      ""FromUserName"": ""openid""
    }
  ],
  ""ResponseMessages"": [],
  ""MaxRecordCount"": 10
}";

            var result = MessageContextJsonConverter<DefaultMpMessageContext, IRequestMessageBase, IResponseMessageBase>
                .Deserialize(cachedJson);

            Assert.AreEqual(1, result.RequestMessages.Count);
            Assert.IsInstanceOfType(result.RequestMessages[0], typeof(RequestMessageEventBase));
        }

        [TestMethod]
        public void GetRequestEntityMappingResultShouldSupportNumericCachedEventValue()
        {
            var eventValue = ((int)Event.subscribe).ToString(CultureInfo.InvariantCulture);
            var cachedDocument = XDocument.Parse($@"<xml>
  <MsgType>7</MsgType>
  <Event>{eventValue}</Event>
</xml>");

            var result = new DefaultMpMessageContext()
                .GetRequestEntityMappingResult(RequestMsgType.Event, cachedDocument);

            Assert.IsInstanceOfType(result, typeof(RequestMessageEvent_Subscribe));
        }
    }
}
