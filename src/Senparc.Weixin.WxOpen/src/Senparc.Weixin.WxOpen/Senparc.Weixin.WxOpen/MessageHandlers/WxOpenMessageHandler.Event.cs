/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：MessageHandler.Event.cs
    文件功能描述：微信请求的集中处理方法：Event相关
    
    
    创建标识：Senparc - 20150924
    
----------------------------------------------------------------*/

using Senparc.Weixin.Exceptions;
using Senparc.Weixin.WxOpen;
using Senparc.Weixin.WxOpen.Entities;

namespace Senparc.Weixin.WxOpen.MessageHandlers
{
    public abstract partial class WxOpenMessageHandler<TC>
    {
        /// <summary>
        /// Event事件类型请求
        /// </summary>
        public virtual IResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        {
            var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
            IResponseMessageBase responseMessage = null;
            switch (strongRequestMessage.Event)
            {
                case Event.user_enter_tempsession:
                    responseMessage = OnEvent_UserEnterTempSessionRequest(RequestMessage as RequestMessageEvent_UserEnterTempSession);
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
            }
            return responseMessage;
        }

        #region Event 下属分类


        /// <summary>
        /// 进入客服会话事件
        /// </summary>
        public virtual IResponseMessageBase OnEvent_UserEnterTempSessionRequest(RequestMessageEvent_UserEnterTempSession requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        #endregion
    }
}
