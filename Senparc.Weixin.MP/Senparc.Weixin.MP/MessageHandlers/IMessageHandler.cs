/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：IMessageHandler.cs
    文件功能描述：MessageHandler接口
    
    
    创建标识：Senparc - 20150924
    
----------------------------------------------------------------*/
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.MessageHandlers
{

    public interface IMessageHandler : Weixin.MessageHandlers.IMessageHandler<IRequestMessageBase, IResponseMessageBase>
    {
        new IRequestMessageBase RequestMessage { get; set; }
        new IResponseMessageBase ResponseMessage { get; set; }
    }
}
