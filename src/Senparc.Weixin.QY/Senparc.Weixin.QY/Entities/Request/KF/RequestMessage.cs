using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.QY.Entities.Request.KF
{
    public class RequestMessage : RequestBase
    {
        public long MsgId { get; set; }
        public Receiver Receiver { get; set; }
    }
}
