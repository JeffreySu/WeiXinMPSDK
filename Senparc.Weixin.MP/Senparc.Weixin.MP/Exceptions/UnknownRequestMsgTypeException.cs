using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP
{
    /// <summary>
    /// 未知请求类型。
    /// </summary>
    public class UnknownRequestMsgTypeException : WeixinException //ArgumentOutOfRangeException
    {
        public UnknownRequestMsgTypeException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
