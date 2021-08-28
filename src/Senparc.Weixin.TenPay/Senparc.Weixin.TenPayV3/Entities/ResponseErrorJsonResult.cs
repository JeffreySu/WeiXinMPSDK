using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Entities
{
    /// <summary>
    /// API 返回错误时，附带的错误信息
    /// </summary>
    public class ResponseErrorJsonResult
    {
        public string code { get; set; }
        public Detail detail { get; set; }
        public string message { get; set; }
    }

    public class Detail
    {
        public object location { get; set; }
        public string[] value { get; set; }
    }

}
