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
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.Weixin.Work.Entities.Request.KF;
using Senparc.Weixin.Work.Helpers;

namespace Senparc.Weixin.Work.Test
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
