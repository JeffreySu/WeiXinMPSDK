using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Exceptions
{
    public class WeixinMenuException : WeixinException
    {
        public WeixinMenuException(string message)
            : base(message, null)
        {
        }

        public WeixinMenuException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
