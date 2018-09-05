using Senparc.NeuChar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.Entities.Request.KF
{
    public class RequestBase
    {
        public RequestMsgType MsgType { get; protected set; }
        public string FromUserName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
