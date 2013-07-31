using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class ResponseMessageText : ResponseMessageBase, IResponseMessageBase
    {
        new public virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Text; }
        }

        public string Content { get; set; }
    }
}
