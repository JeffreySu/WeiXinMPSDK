#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.Work.AdvancedAPIs.External;
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
        
        
        //https://work.weixin.qq.com/api/doc/90000/90135/92122
        [TestMethod]
        public void GroupChatGetResultJsonTest()
        {
            var json = "{     \"errcode\": 0,     \"errmsg\": \"ok\",     \"group_chat\": {         \"chat_id\": \"wrOgQhDgAAMYQiS5ol9G7gK9JVAAAA\",         \"name\": \"销售客服群\",         \"owner\": \"ZhuShengBen\",         \"create_time\": 1572505490,         \"notice\": \"文明沟通，拒绝脏话\",         \"member_list\": [{             \"userid\": \"abel\",             \"type\": 1,             \"join_time\": 1572505491,             \"join_scene\": 1,             \"invitor\": {                 \"userid\": \"jack\"             }         }, {             \"userid\": \"sam\",             \"type\": 1,             \"join_time\": 1572505491,             \"join_scene\": 1         }, {             \"userid\": \"wmOgQhDgAAuXFJGwbve4g4iXknfOAAAA\",             \"type\": 2,             \"unionid\": \"ozynqsulJFCZ2z1aYeS8h-nuasdAAA\",             \"join_time\": 1572505491,             \"join_scene\": 1         }],         \"admin_list\": [{             \"userid\": \"sam\"         }, {             \"userid\": \"pony\"         }]     } }";
            
            var result=SerializerHelper.GetObject<GroupChatGetResult>(json);

            Assert.IsTrue(result.group_chat.member_list.Length == 3);
            Assert.IsTrue(result.group_chat.admin_list .Length== 2);
            Assert.IsTrue(result.group_chat.member_list[0] .invitor.userid=="jack");
            
            Assert.IsTrue(result.group_chat.member_list[2] .unionid=="ozynqsulJFCZ2z1aYeS8h-nuasdAAA");
            
            Assert.IsTrue(result.group_chat.admin_list[1].userid=="pony");
        }
    }
}
