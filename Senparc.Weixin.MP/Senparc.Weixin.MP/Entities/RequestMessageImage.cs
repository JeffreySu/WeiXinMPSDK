using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageImage : RequestMessageBase, IRequestMessageBase
    {
        public string PicUrl { get; set; }
    }
}
