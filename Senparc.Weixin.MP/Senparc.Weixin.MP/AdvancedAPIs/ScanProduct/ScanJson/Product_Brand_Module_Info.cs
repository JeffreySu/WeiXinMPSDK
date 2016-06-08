using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class Product_Brand_Module_Info
    {
    }

    public class Product_Brand_Module_Info_Item_Base
    {
        public string type { get; set; }

    }

    public class Product_Brand_Module_Info_Item : Product_Brand_Module_Info_Item_Base
    {
        public Product_Brand_Module_Info_Item()
        {
            type = "anti-fake";
        }

        public bool native_show { get; set; }
    }
}
