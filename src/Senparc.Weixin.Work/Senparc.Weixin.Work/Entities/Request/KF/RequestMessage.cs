using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.Entities.Request.KF
{
    public class RequestMessage : RequestBase
    {
        public long MsgId { get; set; }
        public Receiver Receiver { get; set; }
    }
}
