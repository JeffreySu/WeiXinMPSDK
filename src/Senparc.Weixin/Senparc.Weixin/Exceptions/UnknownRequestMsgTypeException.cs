/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：UnknownRequestMsgTypeException.cs
    文件功能描述：未知请求类型
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20170101
    修改描述：1、统一构造函数调用（将第一个构造函数的base改为this）
              2、修改基类为MessageHandlerException
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Exceptions
{
    /// <summary>
    /// 未知请求类型。
    /// </summary>
    public class UnknownRequestMsgTypeException : MessageHandlerException //ArgumentOutOfRangeException
    {
        public UnknownRequestMsgTypeException(string message)
            : this(message, null)
        {
        }

        public UnknownRequestMsgTypeException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
