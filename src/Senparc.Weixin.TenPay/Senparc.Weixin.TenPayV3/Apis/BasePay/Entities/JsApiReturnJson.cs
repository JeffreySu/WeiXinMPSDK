using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay.Entities
{
    public class JsApiReturnJson : ReturnJsonBase
    {
        public string prepay_id { get; set; }
    }
}
