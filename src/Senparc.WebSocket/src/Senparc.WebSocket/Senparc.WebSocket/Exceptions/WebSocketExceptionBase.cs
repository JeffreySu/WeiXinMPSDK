using Senparc.CO2NET.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.WebSocket.Exceptions
{
    /// <summary>
    /// WebSocket 自定义异常基类
    /// </summary>
    public class WebSocketExceptionBase : BaseException
    {
        public WebSocketExceptionBase(string message, bool logged = false) : base(message, logged)
        {
        }

        public WebSocketExceptionBase(string message, Exception inner, bool logged = false) : base(message, inner, logged)
        {
        }
    }
}
