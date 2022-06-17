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
            //官方提供
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
      <SpStatus>1</SpStatus>
      <ApproverAttr>2</ApproverAttr>
      <Details>
        <Approver>
          <UserId><![CDATA[WangXiaoMing]]></UserId>
        </Approver>
        <Speech><![CDATA[]]></Speech>
        <SpStatus>1</SpStatus>
        <SpTime>0</SpTime>
      </Details>
      <Details>
        <Approver>
          <UserId><![CDATA[XiaoGangHuang]]></UserId>
        </Approver>
        <Speech><![CDATA[]]></Speech>
        <SpStatus>1</SpStatus>
        <SpTime>0</SpTime>
      </Details>
    </SpRecord>
    <SpRecord>
      <SpStatus>1</SpStatus>
      <ApproverAttr>1</ApproverAttr>
      <Details>
        <Approver>
          <UserId><![CDATA[XiaoHongLiu]]></UserId>
        </Approver>
        <Speech><![CDATA[]]></Speech>
        <SpStatus>1</SpStatus>
        <SpTime>0</SpTime>
      </Details>
    </SpRecord>
    <Notifyer>
      <UserId><![CDATA[ChengLiang]]></UserId>
    </Notifyer>
    <Notifyer>
      <UserId><![CDATA[ChengLiang2]]></UserId>
    </Notifyer>
    <Comments>
      <CommentUserInfo>
        <UserId><![CDATA[LiuZhi]]></UserId>
      </CommentUserInfo>
      <CommentTime>1571732272</CommentTime>
      <CommentContent><![CDATA[这是一个备注]]></CommentContent>
      <CommentId><![CDATA[6750538708562308220]]></CommentId>
    </Comments>
    <Comments>
      <CommentUserInfo>
        <UserId><![CDATA[LiuZhi2]]></UserId>
      </CommentUserInfo>
      <CommentTime>15717322723</CommentTime>
      <CommentContent><![CDATA[这又是一个备注]]></CommentContent>
      <CommentId><![CDATA[6750538708562308221]]></CommentId>
      <Attach>MediaId</Attach>
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
            Assert.AreEqual((byte)1, requestMessage.ApprovalInfo.SpStatus);

            Assert.IsNotNull(requestMessage.ApprovalInfo.SpRecords);
            Assert.AreEqual(2, requestMessage.ApprovalInfo.SpRecords.Length);
            Assert.AreEqual(2, requestMessage.ApprovalInfo.SpRecords[0].ApproverAttr);
            Assert.AreEqual(2, requestMessage.ApprovalInfo.SpRecords[0].Details.Length);
            Assert.AreEqual(1, requestMessage.ApprovalInfo.SpRecords[0].Details[0].SpStatus);

            Assert.AreEqual(2, requestMessage.ApprovalInfo.Notifyers.Length);
            Assert.AreEqual("ChengLiang2", requestMessage.ApprovalInfo.Notifyers[1].UserId);

            Assert.AreEqual(2, requestMessage.ApprovalInfo.Comments.Length);
            Assert.AreEqual("LiuZhi2", requestMessage.ApprovalInfo.Comments[1].CommentUserInfo.UserId);
            Assert.AreEqual("这又是一个备注", requestMessage.ApprovalInfo.Comments[1].CommentContent);
            Assert.AreEqual("6750538708562308221", requestMessage.ApprovalInfo.Comments[1].CommentId);
            Assert.AreEqual("MediaId", requestMessage.ApprovalInfo.Comments[1].Attach);


            /* 实际收到的：
            <?xml version="1.0" encoding="utf-8"?>
<xml>
  <ToUserName><![CDATA[ww8533bff007c0b489]]></ToUserName>
  <FromUserName><![CDATA[sys]]></FromUserName>
  <CreateTime>1645070366</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[sys_approval_change]]></Event>
  <AgentID>1000002</AgentID>
  <ApprovalInfo>
    <SpNo>202202170001</SpNo>
    <SpName><![CDATA[SYS微信SDK审批]]></SpName>
    <SpStatus>1</SpStatus>
    <TemplateId><![CDATA[C4NxepvGj51gbkeGXHQgYRArW96WrxRinNfyCxo7N]]></TemplateId>
    <ApplyTime>1645070220</ApplyTime>
    <Applyer>
      <UserId><![CDATA[SuZhenWei]]></UserId>
      <Party><![CDATA[1]]></Party>
    </Applyer>
    <SpRecord>
      <SpStatus>1</SpStatus>
      <ApproverAttr>2</ApproverAttr>
      <Details>
        <Approver>
          <UserId><![CDATA[FuYunKun]]></UserId>
        </Approver>
        <Speech><![CDATA[]]></Speech>
        <SpStatus>1</SpStatus>
        <SpTime>0</SpTime>
      </Details>
      <Details>
        <Approver>
          <UserId><![CDATA[LiShiJie]]></UserId>
        </Approver>
        <Speech><![CDATA[]]></Speech>
        <SpStatus>1</SpStatus>
        <SpTime>0</SpTime>
      </Details>
    </SpRecord>
    <Notifyer>
      <UserId><![CDATA[FuYunKun]]></UserId>
    </Notifyer>
    <Comments>
      <CommentUserInfo>
        <UserId><![CDATA[SuZhenWei]]></UserId>
      </CommentUserInfo>
      <CommentTime>1645070277</CommentTime>
      <CommentContent><![CDATA[添加附件的备注]]></CommentContent>
      <CommentId><![CDATA[7065523039889409558]]></CommentId>
      <Attach><![CDATA[WWCISP_HKLFyKUsme6wsuH4ZZm6qEqVOO0MkefA9pby86N7D4emHdGDnOrVnC7epnwvEwxTis83LEFPyPs4fDFuAoSQHJVs_OBl4OCeg0Hblv17oE5HcW7YebDNWB0pi_zopBcqDfmPHnpb_BG36mBhqhzrDQEe0DjAvzGeOsmCPK7S1mxUF9s_8-gyax2Z3HiSeKwLYlX-JgAQMKiawoDVDf3TY_EvzAo7Xlu1Jnd4aeBxLUWpjnXwpXSxlTaM680Yj-UmPVStbqQHTjHO8oL7B-3lmaoKqVbrFoDVuS0-JhkHPYs8kQO4IU7BUKKZ7y1gRRVcU9x0eVyOzWysujcrF44XXMMt6_kZ1NtL-eCbQCD4JRdzdZRXhMPlGJwPTFAxC3Idab9S-q1RgVqWThUdvoGs_Ykw82IzGOVBXEgobS1Bu_doXgSkBp_YQuL23vAt1ETlirNEQSpK1Fk3GdnYcBfsaJ-Q35FGMQ6_zlGGDLCzGmVLbGTIXiCe2nyELMBAgIzy7PI56YYtbCjJ-et8biXrLMvugAx3W0GHGTiz5uHEhcTwMAa288yMQm1-Ez9bDo8kPuNA3QJEMZ05kE40rxQxVXGmDiFqa8VSAooQmhYUqOsBYk7ZAYvGSxbchMVVr7Epk6RzOM9DSMjXLTCKfuSmovkHs8p_A4LeAdVxoZtcrUg]]></Attach>
    </Comments>
    <Comments>
      <CommentUserInfo>
        <UserId><![CDATA[SuZhenWei]]></UserId>
      </CommentUserInfo>
      <CommentTime>1645070366</CommentTime>
      <CommentContent><![CDATA[第二条备注]]></CommentContent>
      <CommentId><![CDATA[7065523422141540548]]></CommentId>
      <Attach><![CDATA[WWCISP_HKLFyKUsme6wsuH4ZZm6qEqVOO0MkefA9pby86N7D4emHdGDnOrVnC7epnwvEwxTis83LEFPyPs4fDFuAoSQHJVs_OBl4OCeg0Hblv17oE5x75XL8TRkcDWGiB_h9OidJp_VuR7kou5pSfo01qVecYUqgz6HElWdivjIrZX6U_w6xHGZ_8IvB6KJodYQ27IMh4Za2UdxuEjBqnLBJBDPyNqOV66i8JgYmKq2imHJlpAhqwQKUDNzHJ2J53yPbj_UlzaRDnGa3pS6d8D617F9s5hEPggdQuVXxShGRZLlFLweU2o5HInA_LX8Ch4KIYtvP1GxBzboAOOiBvLv4rnwbcAQwcugsgvCJ7Cd5Hz_g_U1JsA1-4FGXNPNNdYnlwzuCVeBiIr48mV0DebXHqul7Vfnw6_9jaOqgif1uO28ZaumhmKHmw7B0-Frz3zOqJuZZSdw2c8ofgU10ALomcVkchuDdDx3Md9NLPtpBWKV4RqAx4p4Udc782TAmMxvjKNSWhj3Acseto59ZVGOtBI_-R6uTFT2t2JyQeh64h-77saFkTfbe1PsJh6nMXWfpMGg8RyZL78IX3N8z1aGxBQYPbWuIOEO80l3rMaRI7JCSjNQ5zLTQ8tXZnZdugqk_HUEhhkK0h-mN6HppUarOopJFwTfSokKHDp7Yrm0OHf01O0]]></Attach>
    </Comments>
    <StatuChangeEvent>10</StatuChangeEvent>
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

            Assert.IsTrue(requestMessage.ApprovalInfo.ApprovalNodes.Length == 2);
            var firstApprovalNode = requestMessage.ApprovalInfo.ApprovalNodes.First();

            Assert.IsNotNull(firstApprovalNode.Items);
            Assert.IsTrue(firstApprovalNode.Items.Length == 2);
            var firstItem = firstApprovalNode.Items.First();
            Assert.AreEqual("chauvetxiao", firstItem.ItemName);
            Assert.AreEqual("http://www.qq.com/xxx.png", firstItem.ItemImage);

            var secondApprovalNode = requestMessage.ApprovalInfo.ApprovalNodes[1];
            Assert.IsNotNull(secondApprovalNode.Items);
            Assert.IsTrue(secondApprovalNode.Items.Length == 2);
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
