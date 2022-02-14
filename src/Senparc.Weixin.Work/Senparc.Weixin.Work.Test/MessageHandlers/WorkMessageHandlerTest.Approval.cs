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
    /// 审批相关测试
    /// </summary>
    public partial class WorkMessageHandlersTest
    {
        /// <summary>
        /// 系统审批
        /// </summary>
        [TestMethod]
        public void RequestMessageEvent_SysApprovalChangeTest()
        {
            var xml = @"<xml>
  <ToUserName><![CDATA[ww1cSD21f1e9c0caaa]]></ToUserName>
  <FromUserName><![CDATA[sys]]></FromUserName>
  <CreateTime>1571732272</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[sys_approval_change]]></Event>
  <AgentID>3010040</AgentID>
  <ApprovalInfo>
    <SpNo>201910220003</SpNo>
    <SpName><![CDATA[示例模板]]></SpName>
    <SpStatus>1</SpStatus>
    <TemplateId><![CDATA[3TkaH5KFbrG9heEQWLJjhgpFwmqAFB4dLEnapaB7aaa]]></TemplateId>
    <ApplyTime>1571728713</ApplyTime>
    <Applyer>
      <UserId><![CDATA[WuJunJie]]></UserId>
      <Party><![CDATA[1]]></Party>
    </Applyer>
    <SpRecord>
      
    </SpRecord>
    <SpRecord>

    </SpRecord>
    <Notifyer>
      <UserId><![CDATA[ChengLiang]]></UserId>
    </Notifyer>
    <Comments>
      <CommentUserInfo>
        <UserId><![CDATA[LiuZhi]]></UserId>
      </CommentUserInfo>
      <CommentTime>1571732272</CommentTime>
      <CommentContent><![CDATA[这是一个备注]]></CommentContent>
      <CommentId><![CDATA[6750538708562308220]]></CommentId>
    </Comments>
    <StatuChangeEvent>10</StatuChangeEvent>
  </ApprovalInfo>
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
            Assert.IsInstanceOfType(messageHandler.RequestMessage, typeof(RequestMessageEvent_SysApprovalChange));

            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_SysApprovalChange;
            Console.WriteLine(requestMessage.ToJson(true));

            Assert.AreEqual(Event.SYS_APPROVAL_CHANGE, requestMessage.Event);

            Assert.IsNotNull(requestMessage.ApprovalInfo);
            Assert.AreEqual((ulong)201910220003, requestMessage.ApprovalInfo.SpNo);
            Assert.AreEqual("示例模板", requestMessage.ApprovalInfo.SpName);
            //Assert.AreEqual("LiuZhi", requestMessage.ApprovalInfo.Comments.CommentUserInfo.UserId);

            Assert.IsNotNull(requestMessage.ApprovalInfo.SpRecord);
            //Assert.IsNotNull(requestMessage.ApprovalInfo.SpRecord.Approver);
            //Assert.AreEqual("MyName", requestMessage.ApprovalInfo.SpRecord.Details.Approver.UserId);

            /* 实际收到的：
            <?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[wx7618c0a6d9358622]]></ToUserName>
  <FromUserName><![CDATA[sys]]></FromUserName>
  <CreateTime>1644320363</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[sys_approval_change]]></Event>
  <AgentID>3010040</AgentID>
  <ApprovalInfo>
    <SpNo>202202080001</SpNo>
    <SpName><![CDATA[报销]]></SpName>
    <SpStatus>1</SpStatus>
    <TemplateId><![CDATA[1970325096986444_1688852629383336_27728810_1510570226]]></TemplateId>
    <ApplyTime>1644320363</ApplyTime>
    <Applyer>
      <UserId><![CDATA[001]]></UserId>
      <Party><![CDATA[1]]></Party>
    </Applyer>
    <SpRecord>
      <SpStatus>1</SpStatus>
      <ApproverAttr>1</ApproverAttr>
      <Details>
        <Approver>
          <UserId><![CDATA[MyName]]></UserId>
        </Approver>
        <Speech><![CDATA[]]></Speech>
        <SpStatus>1</SpStatus>
        <SpTime>0</SpTime>
      </Details>

    </SpRecord>

    <StatuChangeEvent>1</StatuChangeEvent>
  </ApprovalInfo>
</xml>
             */
        }

        /// <summary>
        /// 自定义审批
        /// </summary>
        [TestMethod]
        public void RequestMessageEvent_OpenApprovalChangeTest()
        {
            var xml = @"<xml>
  <ToUserName>wwd08c8e7c775abaaa</ToUserName>  
  <FromUserName>sys</FromUserName>  
  <CreateTime>1527838022</CreateTime>  
  <MsgType>event</MsgType>  
  <Event>open_approval_change</Event>
  <AgentID>1</AgentID>
  <ApprovalInfo> 
    <ThirdNo>thirdNoxxx</ThirdNo>  
    <OpenSpName>付款</OpenSpName>  
    <OpenTemplateId>1234567111</OpenTemplateId> 
    <OpenSpStatus>1</OpenSpStatus>  
    <ApplyTime>1527837645</ApplyTime>  
    <ApplyUserName>jackiejjwu</ApplyUserName>  
    <ApplyUserId>WuJunJie</ApplyUserId>  
    <ApplyUserParty>产品部</ApplyUserParty>  
    <ApplyUserImage>http://www.qq.com/xxx.png</ApplyUserImage>  
    <ApprovalNodes> 
      <ApprovalNode> 
        <NodeStatus>1</NodeStatus>  
        <NodeAttr>1</NodeAttr> 
        <NodeType>1</NodeType>  
        <Items> 
          <Item> 
            <ItemName>chauvetxiao</ItemName>  
            <ItemUserid>XiaoWen</ItemUserid> 
            <ItemParty>产品部</ItemParty>  
            <ItemImage>http://www.qq.com/xxx.png</ItemImage>  
            <ItemStatus>1</ItemStatus>  
            <ItemSpeech></ItemSpeech>  
            <ItemOpTime>0</ItemOpTime> 
          </Item>
          <Item> 
            <ItemName>chauvetxiao</ItemName>  
            <ItemUserid>XiaoWen</ItemUserid> 
            <ItemParty>产品部</ItemParty>  
            <ItemImage>http://www.qq.com/xxx.png</ItemImage>  
            <ItemStatus>1</ItemStatus>  
            <ItemSpeech></ItemSpeech>  
            <ItemOpTime>0</ItemOpTime> 
          </Item> 
        </Items> 
      </ApprovalNode> 
      <ApprovalNode> 
        <NodeStatus>1</NodeStatus>  
        <NodeAttr>1</NodeAttr> 
        <NodeType>1</NodeType>  
        <Items> 
          <Item> 
            <ItemName>chauvetxiao</ItemName>  
            <ItemUserid>XiaoWen</ItemUserid> 
            <ItemParty>产品部</ItemParty>  
            <ItemImage>http://www.qq.com/xxx.png</ItemImage>  
            <ItemStatus>1</ItemStatus>  
            <ItemSpeech></ItemSpeech>  
            <ItemOpTime>0</ItemOpTime> 
          </Item> 
          <Item> 
            <ItemName>chauvetxiao2</ItemName>  
            <ItemUserid>XiaoWen2</ItemUserid> 
            <ItemParty>产品部2</ItemParty>  
            <ItemImage>http://www.qq.com/xxx2.png</ItemImage>  
            <ItemStatus>1</ItemStatus>  
            <ItemSpeech></ItemSpeech>  
            <ItemOpTime>0</ItemOpTime> 
          </Item> 
        </Items> 
      </ApprovalNode> 
    </ApprovalNodes>  
    <NotifyNodes> 
      <NotifyNode> 
        <ItemName>jinhuiguo</ItemName>  
        <ItemUserid>GuoJinHui</ItemUserid> 
        <ItemParty>行政部</ItemParty>  
        <ItemImage>http://www.qq.com/xxx.png</ItemImage>  
      </NotifyNode> 
      <NotifyNode> 
        <ItemName>jinhuiguo</ItemName>  
        <ItemUserid>GuoJinHui</ItemUserid> 
        <ItemParty>行政部</ItemParty>  
        <ItemImage>http://www.qq.com/xxx.png</ItemImage>  
      </NotifyNode> 
    </NotifyNodes> 
    <ApproverStep>10</ApproverStep>  
  </ApprovalInfo> 
</xml>
";

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
            Assert.IsInstanceOfType(messageHandler.RequestMessage, typeof(RequestMessageEvent_OpenApprovalChange));

            var requestMessage = messageHandler.RequestMessage as RequestMessageEvent_OpenApprovalChange;
            
            Console.WriteLine(requestMessage.ToJson(true));

            Assert.AreEqual(Event.OPEN_APPROVAL_CHANGE, requestMessage.Event);

            Assert.IsNotNull(requestMessage.ApprovalInfo);
            Assert.AreEqual("thirdNoxxx", requestMessage.ApprovalInfo.ThirdNo);
            Assert.AreEqual((uint)1234567111, requestMessage.ApprovalInfo.OpenTemplateId);
            Assert.AreEqual("产品部", requestMessage.ApprovalInfo.ApplyUserParty);


            Assert.IsNotNull(requestMessage.ApprovalInfo.ApprovalNodes);
          
            Assert.IsTrue(requestMessage.ApprovalInfo.ApprovalNodes.Length ==2);
            var firstApprovalNode = requestMessage.ApprovalInfo.ApprovalNodes.First();

            Assert.IsNotNull(firstApprovalNode.Items);
            Assert.IsTrue(firstApprovalNode.Items.Length ==2);
            var firstItem = firstApprovalNode.Items.First();
            Assert.AreEqual("chauvetxiao", firstItem.ItemName);
            Assert.AreEqual("http://www.qq.com/xxx.png", firstItem.ItemImage);

            var secondApprovalNode = requestMessage.ApprovalInfo.ApprovalNodes[1];
            Assert.IsNotNull(secondApprovalNode.Items);
            Assert.IsTrue(secondApprovalNode.Items.Length ==2);
            var secondItem = secondApprovalNode.Items[1];
            Assert.AreEqual("chauvetxiao2", secondItem.ItemName);
            Assert.AreEqual("产品部2", secondItem.ItemParty);
            Assert.AreEqual("http://www.qq.com/xxx2.png", secondItem.ItemImage);

            Assert.IsNotNull(requestMessage.ApprovalInfo.NotifyNodes);
            Assert.IsTrue(requestMessage.ApprovalInfo.NotifyNodes.Length > 0);
            var firstNotifyNode = requestMessage.ApprovalInfo.NotifyNodes.First();
            Assert.AreEqual("jinhuiguo", firstNotifyNode.ItemName);

            Assert.AreEqual(10, requestMessage.ApprovalInfo.ApproverStep);
        }
    }
}
