using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.Weixin.QY.Test
{
    [TestClass]
    public class RequestMessageFactoryTest
    {
        [TestMethod]
        public void GetEncryptPostDataTest()
        {
            var xml = @"<xml>
<ToUserName><![CDATA[wx7618c0a6d9358622]]></ToUserName>
<Encrypt><![CDATA[P/xe9JLq4RJd3AFnjwRsttpyBrTVAGZ49JspjVI65EL7ra73u5TNn3EXHngx1TQ1gnfuFpbRovJdVZ8WrqZ4y0PI9ncA9GR95TboQWK/RGbDpW/Rkq0il1lpw+c/NTk1abwH1C/2siKATSJTbGQ4mWSyhOME7vINBHeW7EjmGEZSaPxC60z1qcLYgMYAiEL/xrU484V6X6BG/jV2uF76+C7HWGMLVmu4DOHVW+UfqQo9SnpAqZx0KRcvT/8XxGUsGwgNWhYyuzUHxu1VuZK16IiHS494tjWrXs08dKQzcpwyID7dthqQDTdIVe0tiOwPAlXvv7jQ5iMtYoQlT32HOjNTn5o/hz9wFZNnC6TFi2Y0ocEWxEMNwDHsyK85ytryTZzL+OmZ7heB72ABNhx9uGhrLoA5M68/ZXwlmfJVx8M=]]></Encrypt>
<AgentID><![CDATA[2]]></AgentID>
</xml>";
            var encryptPostData = RequestMessageFactory.GetEncryptPostData(xml);
            Assert.IsNotNull(encryptPostData);
            Assert.AreEqual("wx7618c0a6d9358622", encryptPostData.ToUserName);
            Assert.AreEqual("P/xe9JLq4RJd3AFnjwRsttpyBrTVAGZ49JspjVI65EL7ra73u5TNn3EXHngx1TQ1gnfuFpbRovJdVZ8WrqZ4y0PI9ncA9GR95TboQWK/RGbDpW/Rkq0il1lpw+c/NTk1abwH1C/2siKATSJTbGQ4mWSyhOME7vINBHeW7EjmGEZSaPxC60z1qcLYgMYAiEL/xrU484V6X6BG/jV2uF76+C7HWGMLVmu4DOHVW+UfqQo9SnpAqZx0KRcvT/8XxGUsGwgNWhYyuzUHxu1VuZK16IiHS494tjWrXs08dKQzcpwyID7dthqQDTdIVe0tiOwPAlXvv7jQ5iMtYoQlT32HOjNTn5o/hz9wFZNnC6TFi2Y0ocEWxEMNwDHsyK85ytryTZzL+OmZ7heB72ABNhx9uGhrLoA5M68/ZXwlmfJVx8M=", encryptPostData.Encrypt);
            Assert.AreEqual(2, encryptPostData.AgentID);
        }
    }
}
