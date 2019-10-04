using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Tencent;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.WeixinTests.NetCore3.Tencent
{
    [TestClass]
    public class WXBizMsgCryptTests
    {
        [TestMethod]
        public void EncryptRequestMsgTest()
        {
            var xml = @"<xml>
  <MsgType>Text</MsgType>
  <Content><![CDATA[123]]></Content>
  <bizmsgmenuid><![CDATA[]]></bizmsgmenuid>
  <MsgId>637058302725818000</MsgId>
  <Encrypt><![CDATA[]]></Encrypt>
  <ToUserName><![CDATA[ToUserNameValue]]></ToUserName>
  <FromUserName><![CDATA[FromUserName(OpenId)]]></FromUserName>
  <CreateTime>1570204886</CreateTime>
</xml>";
            var token = "weixin";
            var encodingAESKey = "YTJkZmVjMzQ5NDU5NDY3MDhiZWI0NTdiMjFiY2I5MmU";
            var appId = "wx669ef95216eef885";

            var timeStamp = SystemTime.NowTicks.ToString();
            var nonce = (SystemTime.NowTicks * 2).ToString();
            WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(token, encodingAESKey, appId);
            var toUserName = "ToUserNameValue";
            string finalResponseXml = null, msgSigature = null;
            var ret = msgCrype.EncryptRequestMsg(xml, timeStamp, nonce, toUserName, ref finalResponseXml, ref msgSigature);
            Assert.AreEqual(0, ret);
            Console.WriteLine(finalResponseXml);
            Console.WriteLine(msgSigature);
            Console.WriteLine();

            //进行解密
            string decryptXml = null;
            ret = msgCrype.DecryptMsg(msgSigature, timeStamp, nonce, finalResponseXml, ref decryptXml);
            Assert.AreEqual(0, ret);
            Console.WriteLine(decryptXml);
        }
    }
}
