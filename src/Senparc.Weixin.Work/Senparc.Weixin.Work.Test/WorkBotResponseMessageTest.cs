using Senparc.Weixin.Work.Entities;
using Senparc.CO2NET.Helpers.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;


namespace Senparc.Weixin.Work.Test
{
    [TestClass]
    public class WorkBotResponseMessageTest
    {
        [TestMethod]
        public void TestResponseMessageText()
        {
            var json = """
            {
  "msgtype": "text",
  "text": {
    "content": "hello\nI'm RobotA\n"
  }
}
""";
            var jsonObject = json.GetObject<BotResponseMessageText>();
            Assert.AreEqual("text", jsonObject.msgtype);
            Assert.AreEqual("hello\nI'm RobotA\n", jsonObject.text.content);
        }
    }
}