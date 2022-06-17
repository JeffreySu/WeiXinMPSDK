using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Helpers;
using Senparc.Weixin.Work.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.Work.Test.net6.MessageHandlers
{
    public class CustomMessageHandlers : WorkMessageHandler<MessageContexts.DefaultWorkMessageContext>
    {
        public CustomMessageHandlers(XDocument requestDoc, PostModel postModel, int maxRecordCount = 0)
            : base(requestDoc, postModel, maxRecordCount)
        {
        }

        public override IWorkResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = RequestMessage.CreateResponseMessage<ResponseMessageText>();

            responseMessage.Content = "文字信息";
            return responseMessage;
        }


        /// <summary>
        /// 默认消息
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IWorkResponseMessageBase DefaultResponseMessage(IWorkRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这是一条默认消息。";
            return responseMessage;
        }

        public override IWorkResponseMessageBase OnEvent_Sys_Approval_Change_Status_ChangeRequest(RequestMessageEvent_SysApprovalChange requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "收到了审批消息：" + requestMessage.ApprovalInfo.SpName;
            return responseMessage;
        }

        //public override Task BuildResponseMessageAsync(CancellationToken cancellationToken)
        //{
        //    throw new NotImplementedException();
        //}
    }

}
