using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.Scan
{
    public class BrandInfo_Type
    {
        public List<BrandInfo_ActionList> action_list;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class BrandInfo_Price
    {
       
        /// <summary>
        /// 表示商品的建议零售价，以“元”为单位。
        /// </summary>
        public string retail_price { get; set; }
      }

    public class BrandInfo_link
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string link { get; set; }
    }

    public class BrandInfo_user 
    {
        
    }

    public class BrandInfo_text 
    {
        /// <summary>
        /// 
        /// </summary>
        public string  text { get; set; }
    }
}
