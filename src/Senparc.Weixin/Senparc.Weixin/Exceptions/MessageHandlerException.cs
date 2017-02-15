/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：MessageHandlerException.cs
    文件功能描述：微信消息异常处理类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20170101
    修改描述：统一构造函数调用（将第一个构造函数的base改为this）

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Exceptions
{
    /// <summary>
    /// MessageHandler异常
    /// </summary>
    public class MessageHandlerException : WeixinException
    {
          public MessageHandlerException(string message)
            : this(message, null)
        {
        }

          public MessageHandlerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
