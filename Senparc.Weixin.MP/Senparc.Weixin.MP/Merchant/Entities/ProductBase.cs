using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Merchant.Entities
{
    public class ProductBase
    {
        public List<string> category_id { get; set; }
        public ProductBase_Property property { get; set; }
        public string name { get; set; }
    }

    public class ProductBase_Property
    {
        public string id { get; set; }
        public string vid { get; set; }
    }
}   
