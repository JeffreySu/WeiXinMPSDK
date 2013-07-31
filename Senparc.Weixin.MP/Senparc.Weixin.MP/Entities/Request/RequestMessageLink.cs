using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageLink : RequestMessageBase, IRequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Link; }
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
