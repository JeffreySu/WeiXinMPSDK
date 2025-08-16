/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotResponseMessageBase.cs
    文件功能描述：企业微信机器人响应回复消息基类
    
    
    创建标识：Wang Qian - 20250804
----------------------------------------------------------------*/
using Senparc.NeuChar;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work.Entities
{
    public interface IWorkBotResponseMessageBase : IResponseMessageBase, IMessageBase
    {
        //ResponseMsgType MsgType { get; set; }
    }

    public class WorkBotResponseMessageBase : ResponseMessageBase, IWorkBotResponseMessageBase
    {
        public override ResponseMsgType MsgType => ResponseMsgType.Text;

				 
        //不需要CreateFromRequestMessage()方法，因为机器人响应消息不需要FromUserName和ToUserName

    }
}