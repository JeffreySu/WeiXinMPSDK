using Senparc.Weixin.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    public class TenpayApiRequestException : WeixinException
    {
        public TenpayApiRequestException(string message, Exception inner, bool logged = false) : base(message, inner, logged)
        {
        }

        public TenpayApiRequestException(string message, bool logged = false) : this(message, null, logged)
        {
        }
    }
}
