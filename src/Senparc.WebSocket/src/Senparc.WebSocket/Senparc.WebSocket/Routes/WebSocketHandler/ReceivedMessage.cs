using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.WebSocket
{
    /// <summary>
    /// 接收到消息封装的实体
    /// </summary>
    [Serializable]
    public class ReceivedMessage
    {
        public string Message { get; set; }
        public string SessionId { get; set; }
        public string FormId { get; set; }
    }
}
