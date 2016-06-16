using Newtonsoft.Json;
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

    public abstract class Product_Brand_Action_Info_Base
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

    public class Product_Brand_Action_Info_Card : Product_Brand_Action_Info_Base
    {
        public Product_Brand_Action_Info_Card()
        {
            type = "card";
        }
        /// <summary>
        /// card id 字段必填,该卡券 为非 自定义 code ( 概念 说明见 微信 卡券接口文档) 。
        /// </summary>
        public string cardid { get; set; }
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
        /// retail_price 字段必填, 表示 商 品 的 建议零售价,以“元”为 单位。
        /// </summary>
        public string retail_price { get; set; }
    }

    public class Product_Brand_Action_Info_Product : Product_Brand_Action_Info_Base
    {
        public Product_Brand_Action_Info_Product()
        {
            type = "product";
        }

        /// <summary>
        /// 例如 “官方商城”
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 字段必填,需填写有效的小店商品
        /// </summary>
        public string productid { get; set; }

        /// <summary>
        /// 字段选填,购买提示信息,例如包邮、限时折扣
        /// </summary>
        public string digest { get; set; }
    }

    public class Product_Brand_Action_Info_Store : Product_Brand_Action_Info_Base
    {
        public Product_Brand_Action_Info_Store()
        {
            type = "store";
        }
        /// <summary>
        /// 例如京东商城
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// link 字段必填,对应电商购买 连接 。
        /// </summary>
        public string link { get; set; }
        /// <summary>
        /// sale_price 字段必填,对应商 品价格,单位:元 。
        /// </summary>
        public string sale_price { get; set; }
    }

    /// <summary>
    /// 注意:被推荐的商品也必须是同一 账号下创建的,并且已经发布。
    /// </summary>
    public class Product_Brand_Action_Info_Recommend : Product_Brand_Action_Info_Base
    {
        public Product_Brand_Action_Info_Recommend()
        {
            type = "recommend";
        }

        public Product_Brand_Action_Info_Recommend_Content recommend { get; set; }
    }

    public class Product_Brand_Action_Info_Recommend_Content
    {
        /// <summary>
        /// 表示 商品 推荐的方式, 目前 只 支持指定 “ appointed”
        /// </summary>
        public string recommend_type { get; set; }

        /// <summary>
        /// 字段 必填 , 表示指定要推荐的商品列表。
        /// </summary>
        public List<Product_Brand_Action_Info_Recommend_Item> recommend_list { get; set; }

    }

    public class Product_Brand_Action_Info_Recommend_Item
    {
        /// <summary>
        /// keystandard 字段 必填, 表示 被推荐的商品编码格式。
        /// </summary>
        public string keystandard { get; set; }

        /// <summary>
        /// keystr 字段 必填, 表示 被推荐 商品的编码内容。
        /// </summary>
        public string keystr { get; set; }
    }
}
