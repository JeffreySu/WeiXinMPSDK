using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Entities.Request;
using Senparc.NeuChar.Helpers;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageContexts;
using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Senparc.Weixin.MP.Test.NetCore3.MessageHandlers.TestEntities
{
    public class CustomMessageHandlers : MessageHandler<DefaultMpMessageContext>
    {
        /// <summary>
        /// 为中间件提供生成当前类的委托
        /// </summary>
        public static Func<Stream, PostModel, int, IServiceProvider, CustomMessageHandlers> GenerateMessageHandler = (stream, postModel, maxRecordCount, serviceProvider) => new CustomMessageHandlers(stream, postModel, maxRecordCount, serviceProvider);

        public CustomMessageHandlers(XDocument requestDoc, PostModel postModel = null, int maxRecordCount = 0, IServiceProvider serviceProvider = null)
            : base(requestDoc, postModel, maxRecordCount, serviceProvider: serviceProvider)
        {
        }

        public CustomMessageHandlers(RequestMessageBase requestMessage, PostModel postModel = null, int maxRecordCount = 0, IServiceProvider serviceProvider = null)
            : base(requestMessage, postModel, maxRecordCount, serviceProvider: serviceProvider)
        {
        }

        public CustomMessageHandlers(Stream stream, PostModel postModel = null, int maxRecordCount = 0, IServiceProvider serviceProvider = null)
         : base(stream, postModel, maxRecordCount, serviceProvider: serviceProvider)
        {
        }


        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            Console.WriteLine("OnTextRequest");
            var responseMessage =
               ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(RequestMessage);


            var requestHandler = requestMessage.StartHandler();
                requestHandler.Keyword("代理", () =>
                {
                    responseMessage.Content = "收到关键字：代理";
                    return responseMessage;
                })
                .SelectMenuKeyword("101", () =>
                {
                    responseMessage.Content = $"选择菜单：{requestMessage.bizmsgmenuid}，文字：{requestMessage.Content}";
                    return responseMessage;
                })
                .SelectMenuKeyword("102", () =>
                {
                    responseMessage.Content = $"选择菜单：{requestMessage.bizmsgmenuid}，文字：{requestMessage.Content}";
                    return responseMessage;
                })
                .Regex("^[1][3-9]{1}[0-9]{9}$", () =>
                {
                    responseMessage.Content = $"正则：{requestMessage.Content}";
                    return responseMessage;
                })
                .Default(() =>
                {
                    responseMessage.Content = "文字信息";
                    return responseMessage;
                });
            return requestHandler.ResponseMessage;
        }

        public override IResponseMessageBase OnEvent_LocationSelectRequest(RequestMessageEvent_Location_Select requestMessage)
        {
            var responeMessage = this.CreateResponseMessage<ResponseMessageText>();
            responeMessage.Content = "OnEvent_LocationSelectRequest";
            return responeMessage;
        }

        public override IResponseMessageBase OnFileRequest(RequestMessageFile requestMessage)
        {
            var responeMessage = this.CreateResponseMessage<ResponseMessageText>();
            responeMessage.Content = requestMessage.FileMd5;
            return responeMessage;
        }

        #region 微信认证事件推送

        public override IResponseMessageBase OnEvent_QualificationVerifySuccessRequest(RequestMessageEvent_QualificationVerifySuccess requestMessage)
        {
            return new SuccessResponseMessage();
        }

        public override IResponseMessageBase OnEvent_QualificationVerifyFailRequest(RequestMessageEvent_QualificationVerifyFail requestMessage)
        {
            return new SuccessResponseMessage();
        }

        public override IResponseMessageBase OnEvent_NamingVerifySuccessRequest(RequestMessageEvent_NamingVerifySuccess requestMessage)
        {
            return new SuccessResponseMessage();
        }

        public override IResponseMessageBase OnEvent_NamingVerifyFailRequest(RequestMessageEvent_NamingVerifyFail requestMessage)
        {
            return new SuccessResponseMessage();
        }

        public override IResponseMessageBase OnEvent_AnnualRenewRequest(RequestMessageEvent_AnnualRenew requestMessage)
        {
            return new SuccessResponseMessage();
        }

        public override IResponseMessageBase OnEvent_VerifyExpiredRequest(RequestMessageEvent_VerifyExpired requestMessage)
        {
            return new SuccessResponseMessage();
        }

        #endregion


        #region v1.5之后，所有的OnXX方法均从抽象方法变为虚方法，并都有默认返回消息操作，不需要处理的消息类型无需重写。

        //public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        //public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion


        /// <summary>
        /// 默认消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = $"您发送的消息类型暂未被识别。RequestMessage Type：{requestMessage.GetType().Name}";
            return responseMessage;
        }

        public override IResponseMessageBase OnUnknownTypeRequest(RequestMessageUnknownType requestMessage)
        {
            var msgType = MsgTypeHelper.GetRequestMsgTypeString(requestMessage.RequestDocument);

            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "未知消息类型：" + msgType;

            return responseMessage;
            //return base.OnUnknownTypeRequest(requestMessage);
        }

        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            System.Console.WriteLine("进入重写同步订阅");

            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "欢迎关注";
            return responseMessage;
        }


        #region 卡券回调测试

        public override IResponseMessageBase OnEvent_GiftCard_Pay_DoneRequest(RequestMessageEvent_GiftCard_Pay_Done requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这里是 OnEvent_GiftCard_Pay_DoneRequest";
            return responseMessage;
        }

        public override IResponseMessageBase OnEvent_GiftCard_Send_To_FriendRequest(RequestMessageEvent_GiftCard_Send_To_Friend requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这里是 OnEvent_GiftCard_Send_To_FriendRequest";
            return responseMessage;
        }

        public override IResponseMessageBase OnEvent_GiftCard_User_AcceptRequest(RequestMessageEvent_GiftCard_User_Accept requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这里是 OnEvent_GiftCard_User_AcceptRequest";
            return responseMessage;
        }

        #endregion


        public override IResponseMessageBase OnEvent_View_Miniprogram(RequestMessageEvent_View_Miniprogram requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = $"小程序被访问：{requestMessage.MenuId} - {requestMessage.EventKey}";
            return responseMessage;
        }

    }

}
