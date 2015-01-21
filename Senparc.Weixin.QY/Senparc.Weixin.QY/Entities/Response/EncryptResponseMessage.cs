using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.QY.Entities.Response
{
    /// <summary>
    /// 返回给服务器的加密消息
    /// </summary>
    public class EncryptResponseMessage
    {
        public string Encrypt { get; set; }
        public string MsgSignature { get; set; }
        public string TimeStamp { get; set; }
        public string Nonce { get; set; }
    }
}
