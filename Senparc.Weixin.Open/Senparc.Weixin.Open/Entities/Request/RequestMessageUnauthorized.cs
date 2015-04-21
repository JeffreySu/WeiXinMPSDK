using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Open
{
    public class RequestMessageUnauthorized : RequestMessageBase
    {
        public string AuthorizerAppid { get; set; }
    }
}
