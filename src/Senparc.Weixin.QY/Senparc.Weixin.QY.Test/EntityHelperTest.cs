using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.QY.Entities.Request.KF;
using Senparc.Weixin.QY.Helpers;

namespace Senparc.Weixin.QY.Test
{
    [TestClass]
    public class EntityHelperTest
    {
        [TestMethod]
        public void TestFillEntityWithXml()
        {
            //客服回调
            var doc = XDocument.Parse(@"
<xml>
   <AgentType>kf_internal</AgentType>
   <ToUserName>CORPID</ToUserName>
   <ItemCount>3</ItemCount>
   <PackageId>3156175696255</PackageId>
   <Item>
       <FromUserName><![CDATA[UserID]]></FromUserName>
       <CreateTime>1348831860</CreateTime>
       <MsgType><![CDATA[text]]></MsgType>
       <Content><![CDATA[test message]]></Content>
       <MsgId>1234567890123456</MsgId>
       <Receiver>
           <Type>userid</Type>
           <Id>lisi</Id>
       </Receiver>
   </Item>
   <Item>
       <FromUserName><![CDATA[UserID]]></FromUserName>
       <CreateTime>1348831860</CreateTime>
       <MsgType><![CDATA[image]]></MsgType>
       <PicUrl><![CDATA[this is a url]]></PicUrl>
       <MediaId><![CDATA[media_id]]></MediaId>
       <MsgId>1234567890123456</MsgId>
       <Receiver>
           <Type>userid</Type>
           <Id>lisi</Id>
       </Receiver>
   </Item>
   <Item>
       <FromUserName><![CDATA[UserID]]></FromUserName>
       <CreateTime>1348831860</CreateTime>
       <MsgType><![CDATA[event]]></MsgType>
       <Event><![CDATA[subscribe]]></Event>
   </Item>
</xml>");
            var reqPack = new RequestPack();
            reqPack.FillEntityWithXml(doc);
            Assert.AreEqual(AgentType.kf_internal, reqPack.AgentType);
            Assert.AreEqual("CORPID", reqPack.ToUserName);
            Assert.AreEqual(3, reqPack.ItemCount);
            Assert.AreEqual(3156175696255, reqPack.PackageId);
            Assert.IsNotNull(reqPack.Items);
            Assert.AreEqual(3, reqPack.Items.Count);

        }
    }
}
