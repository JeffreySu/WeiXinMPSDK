using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class Product_Brand_Detail_Info
    {
        public List<Product_Brand_Detail_Info_Link> banner_list { get; set; }
        public List<Product_Brand_Detail_Info_Desc> detail_list { get; set; }


    }

    public class Product_Brand_Detail_Info_Link
    {
        public string link { get; set; }
        //public string desc { get; set; }
    }

    public class Product_Brand_Detail_Info_Desc
    {
        public string title { get; set; }
        public string desc { get; set; }
    }

}
