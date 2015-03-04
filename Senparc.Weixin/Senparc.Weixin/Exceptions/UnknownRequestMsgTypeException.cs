/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：UnknownRequestMsgTypeException.cs
    文件功能描述：未知请求类型
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Exceptions
{
    /// <summary>
    /// 未知请求类型。
    /// </summary>
    public class UnknownRequestMsgTypeException : WeixinException //ArgumentOutOfRangeException
    {
        public UnknownRequestMsgTypeException(string message)
            : base(message, null)
        {
        }

        public UnknownRequestMsgTypeException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
