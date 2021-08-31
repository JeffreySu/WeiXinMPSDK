using Senparc.Weixin.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    public class TenpaySecurityException : WeixinException
    {
        public TenpaySecurityException(string message, Exception inner, bool logged = false) : base(message, inner, logged)
        {
        }

        public TenpaySecurityException(string message, bool logged = false) : this(message, null, logged)
        {
        }
    }
}
