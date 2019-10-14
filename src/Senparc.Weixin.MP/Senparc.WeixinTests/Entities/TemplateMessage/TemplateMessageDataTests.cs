using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Entities.TemplateMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.WeixinTests.NetCore3.Entities.TemplateMessage
{
    [TestClass]
    public class TemplateMessageDataTests
    {
        [TestMethod]
        public void TemplateMessageDataTest()
        {
            var data = new TemplateMessageData();
            data.Add("Key1", new TemplateMessageDataValue("this is value1"));
            data.Add("Key2", new TemplateMessageDataValue("this is value2"));
            data.Add("Key3", new TemplateMessageDataValue("this is value3"));
            data.Add("Key4", new TemplateMessageDataValue("this is value4"));

            var json = data.ToJson(true);
            Console.WriteLine(json);
            var exceptStr= @"{
  ""Key1"": {
    ""value"": ""this is value1""
  },
  ""Key2"": {
    ""value"": ""this is value2""
  },
  ""Key3"": {
    ""value"": ""this is value3""
  },
  ""Key4"": {
    ""value"": ""this is value4""
  }
}";

            Assert.AreEqual(exceptStr, json);
        }
    }
}
