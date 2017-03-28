/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：MessageHandler.Event.cs
    文件功能描述：微信请求的集中处理方法：Message相关
    
    
    创建标识：Senparc - 20170106
    
----------------------------------------------------------------*/

using Senparc.Weixin.WxOpen.Entities;

namespace Senparc.Weixin.WxOpen.MessageHandlers
{
    public abstract partial class WxOpenMessageHandler<TC>
    {
        #region 接收消息方法

        /// <summary>
        /// 默认返回消息（当任何OnXX消息没有被重写，都将自动返回此默认消息）
        /// </summary>
        public abstract IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage);
        //{
        //    例如可以这样实现：
        //    var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "您发送的消息类型暂未被识别。";
        //    return responseMessage;
        //}

     
        /// <summary>
        /// 文字类型请求
        /// </summary>
        public virtual IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

     
        /// <summary>
        /// 图片类型请求
        /// </summary>
        public virtual IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }



        
        #endregion

    }
}
