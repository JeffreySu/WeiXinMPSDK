using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessagePicSysphoto : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.PicSysphoto; }
        }

        public string MediaId { get; set; }
        public string PicUrl { get; set; }
    }
}
