using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.WxOpen
{
    /// <summary>
    /// WxOpenException
    /// </summary>
    public class WxOpenException : WeixinException
    {
        public WxOpenException(string message)
                : base(message)
        {
        }
    }
}
