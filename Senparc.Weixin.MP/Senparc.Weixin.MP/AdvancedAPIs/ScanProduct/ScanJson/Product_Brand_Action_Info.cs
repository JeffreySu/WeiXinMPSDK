using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class Product_Brand_Action_Info
    {
        public List<Product_Brand_Action_Info_Base> action_list { get; set; }
    }


    public class Product_Brand_Action_Info_Base
    {
        public string type { get; set; }
    }

    public class Product_Brand_Action_Info_Media : Product_Brand_Action_Info_Base
    {
        public Product_Brand_Action_Info_Media()
        {
            type = "media";
        }

        public string type { get; set; }
        public string link { get; set; }
        public string image { get; set; }
    }

    public class Product_Brand_Action_Info_Text : Product_Brand_Action_Info_Base
    {
        public Product_Brand_Action_Info_Text()
        {
            type = "text";
        }
        /// <summary>
        /// 对应介绍
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 选填
        /// </summary>
        public string name { get; set; }
    }

    public class Product_Brand_Action_Info_Link : Product_Brand_Action_Info_Base
    {
        public Product_Brand_Action_Info_Link()
        {
            type = "link";
        }
        public string name { get; set; }
        public string link { get; set; }
        public string image { get; set; }
        public string showtype { get; set; }
    }

    public class Product_Brand_Action_Info_User : Product_Brand_Action_Info_Base
    {
        public Product_Brand_Action_Info_User()
        {
            type = "user";
        }

        public string appid { get; set; }
    }

    /// <summary>
    /// 建议零售价
    /// </summary>
    public class Product_Brand_Action_Info_Price : Product_Brand_Action_Info_Base
    {
        public Product_Brand_Action_Info_Price()
        {
            type = "price";
        }
        /// <summary>
        /// 建议零售价
        /// </summary>
        public string retail_price { get; set; }
    }

    public class Product_Brand_Action_Info_Store : Product_Brand_Action_Info_Base
    {
        public Product_Brand_Action_Info_Store()
        {
            type = "store";
        }
        public string type { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string sale_price { get; set; }
    }

    public class Product_Brand_Action_Info_Recommend : Product_Brand_Action_Info_Base
    {
        public Product_Brand_Action_Info_Recommend()
        {
            type = "store";
        }

        public string recommend { get; set; }
        public string recommend_list { get; set; }
        public string keystr { get; set; }
        public string keystr { get; set; }
    }
}
