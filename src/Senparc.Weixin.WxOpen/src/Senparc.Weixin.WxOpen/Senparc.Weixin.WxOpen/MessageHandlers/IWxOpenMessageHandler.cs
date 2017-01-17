/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：IMessageHandler.cs
    文件功能描述：MessageHandler接口
    
    
    创建标识：Senparc - 20170106
    
----------------------------------------------------------------*/

using Senparc.Weixin.MessageHandlers;
using Senparc.Weixin.WxOpen.Entities;

namespace Senparc.Weixin.WxOpen.MessageHandlers
{

    public interface IWxOpenMessageHandler : IMessageHandler<IRequestMessageBase, IResponseMessageBase>
    {
        new IRequestMessageBase RequestMessage { get; set; }
        new IResponseMessageBase ResponseMessage { get; set; }
    }
}
