using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class Product_Brand_Module_Info
    {
        public List<Product_Brand_Module_Info_Item_Base> module_list { get; set; }
    }

    public class Product_Brand_Module_Info_Item_Base
    {
        public string type { get; set; }

    }

    public class Product_Brand_Module_Info_Item_AntiFake : Product_Brand_Module_Info_Item_Base
    {
        public Product_Brand_Module_Info_Item_AntiFake()
        {
            type = "anti_fake";
        }

        public string native_show { get; set; }
    }
}
