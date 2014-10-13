using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class ResponseMessagePicSysphoto : ResponseMessageBase, IResponseMessageBase
    {
        new public virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.PicSysphoto; }
        }

        public string MediaId { get; set; }
        public string PicUrl { get; set; }
    }
}
