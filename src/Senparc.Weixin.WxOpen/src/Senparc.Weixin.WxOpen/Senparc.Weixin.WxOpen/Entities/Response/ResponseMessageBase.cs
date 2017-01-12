/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：ResponseMessageBase.cs
    文件功能描述：响应回复消息基类
    
    
    创建标识：Senparc - 20170106
----------------------------------------------------------------*/

using System;
using System.Xml.Linq;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.WxOpen.Entities
{
    public interface IResponseMessageBase : Weixin.Entities.IResponseMessageBase
    {
        ResponseMsgType MsgType { get; }
        //string Content { get; set; }
        //bool FuncFlag { get; set; }
    }

    /// <summary>
    /// 微信公众号响应回复消息
    /// </summary>
    public class ResponseMessageBase : Weixin.Entities.ResponseMessageBase, IResponseMessageBase
    {
        public virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.SuccessResponse; }
        }
    }
}
