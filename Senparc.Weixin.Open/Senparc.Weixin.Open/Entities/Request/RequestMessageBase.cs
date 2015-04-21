using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Open
{
    public class RequestMessageBase
    {
       public string AppId { get; set; }
       public DateTime CreateTime { get; set; }
       public InfoType InfoType { get; set; }
    }
}
