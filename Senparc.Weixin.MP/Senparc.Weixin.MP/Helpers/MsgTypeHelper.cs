using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Senparc.Weixin.MP.Helpers
{
    public static class MsgTypeHelper
    {

        public static RequestMsgType GetMsgType(XDocument doc)
        {
            return GetMsgType(doc.Root.Element("MsgType").Value);
        }

        public static RequestMsgType GetMsgType(string str)
        {
            return (RequestMsgType)Enum.Parse(typeof(RequestMsgType), str, true);
        }
    }
}
