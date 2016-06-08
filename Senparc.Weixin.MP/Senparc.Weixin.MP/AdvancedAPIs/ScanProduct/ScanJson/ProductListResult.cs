using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class ProductListResult : WxJsonResult
    {
        public int total { get; set; }

        public List<Key> key_list { get; set; }
    }
}
