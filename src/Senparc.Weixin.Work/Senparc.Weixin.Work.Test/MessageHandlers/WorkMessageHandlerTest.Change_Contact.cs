using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Test.net6.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.Work.Test.MessageHandlers
{
    /// <summary>
    /// 成员变更通知
    /// </summary>
    public partial class WorkMessageHandlersTest
    {
        /// <summary>
        /// 创建成员
        /// </summary>
        [TestMethod]
        public void RequestMessageEvent_Change_Contact_User_Create()
        {
            //官方提供
            var xml = @"<xml>
	<ToUserName><![CDATA[toUser]]></ToUserName>
	<FromUserName><![CDATA[sys]]></FromUserName> 
	<CreateTime>1403610513</CreateTime>
	<MsgType><![CDATA[event]]></MsgType>
	<Event><![CDATA[change_contact]]></Event>
	<ChangeType>create_user</ChangeType>
	<UserID><![CDATA[zhangsan]]></UserID>
	<Name><![CDATA[张三]]></Name>
	<Department><![CDATA[1,2,3]]></Department>
	<MainDepartment>1</MainDepartment>
	<IsLeaderInDept><![CDATA[1,0,0]]></IsLeaderInDept>
	<DirectLeader><![CDATA[lisi,wangwu]]></DirectLeader>
	<Position><![CDATA[产品经理]]></Position>
	<Mobile>13800000000</Mobile>
	<Gender>1</Gender>
	<Email><![CDATA[zhangsan@gzdev.com]]></Email>
	<BizMail><![CDATA[zhangsan@qyycs2.wecom.work]]></BizMail>
	<Status>1</Status>
	<Avatar><![CDATA[http://wx.qlogo.cn/mmopen/ajNVdqHZLLA3WJ6DSZUfiakYe37PKnQhBIeOQBO4czqrnZDS79FH5Wm5m4X69TBicnHFlhiafvDwklOpZeXYQQ2icg/0]]></Avatar>
	<Alias><![CDATA[zhangsan]]></Alias>
	<Telephone><![CDATA[020-123456]]></Telephone>
	<Address><![CDATA[广州市]]></Address>
	<ExtAttr>
		<Item>
		<Name><![CDATA[爱好]]></Name>
		<Type>0</Type>
		<Text>
			<Value><![CDATA[旅游]]></Value>
		</Text>
		</Item>
		<Item>
		<Name><![CDATA[卡号]]></Name>
		<Type>1</Type>
		<Web>
			<Title><![CDATA[企业微信]]></Title>
			<Url><![CDATA[https://work.weixin.qq.com]]></Url>
		</Web>
		</Item>
	</ExtAttr>
</xml>";

            var postModel = new PostModel()
            {
                Msg_Signature = "22cb38c34ae9ba4bdec938405b931ad3ece7e19e",
                Timestamp = "1644320363",
                Nonce = "1645172247",

                Token = "",
                EncodingAESKey = "",
                CorpId = ""
            };

            var messageHandler = new CustomMessageHandlers(XDocument.Parse(xml), postModel, 10);
            messageHandler.Execute();
            var responseMessage = messageHandler.ResponseDocument;

            Assert.IsNotNull(messageHandler.RequestMessage);
            Assert.AreEqual(RequestMsgType.Event, messageHandler.RequestMessage.MsgType);
            Assert.IsInstanceOfType(messageHandler.RequestMessage, typeof(RequestMessageEvent_Change_Contact_User_Create));

            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_Change_Contact_User_Create;
            Console.WriteLine(requestMessage.ToJson(true));

            Assert.AreEqual(Event.change_contact, requestMessage.Event);
            Assert.AreEqual(ContactChangeType.create_user, requestMessage.ChangeType);

			Assert.AreEqual("张三", requestMessage.Name);
			Assert.AreEqual("1,2,3", requestMessage.Department);
			Assert.AreEqual(3, requestMessage.DepartmentIdList.Count());
			Assert.AreEqual(1, requestMessage.DepartmentIdList[0]);
			Assert.AreEqual(2, requestMessage.DepartmentIdList[1]);
			Assert.AreEqual(3, requestMessage.DepartmentIdList[2]);
			Assert.AreEqual("张三", requestMessage.IsLeader);
			Assert.AreEqual("张三", requestMessage.Name);
			Assert.AreEqual("张三", requestMessage.Name);

		}

    }
}
