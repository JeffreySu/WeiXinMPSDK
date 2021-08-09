using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    /// <summary>
    /// 拉取门店小程序类目结果
    /// </summary>
    public class GetMerchantCategoryJsonResult : WxJsonResult
    {
        public CategoriesList all_category_info { get; set; }
    }
    [Serializable]
    public class CategoriesList
    {
        public List<Category> categories { get; set; }
    }
    [Serializable]
    public class Category
    {
        /// <summary>
        /// 类目id
        /// </summary>
        public int id { get; set; }

        public string name { get; set; }

        /// <summary>
        /// 类目的级别
        /// </summary>
        public int level { get; set; }

        public List<int> children { get; set; } = new List<int>();

        public int father { get; set; }

        public Qualify qualify { get; set; }

        public int scene { get; set; }

        /// <summary>
        /// 0或者1 sensitive_type=1：在申请类目时需要上传相关证件
        /// </summary>
        public int sensitive_type { get; set; }
    }
    [Serializable]
    public class Qualify
    {
        public Qualify()
        {
            exter_list = new List<Exter>();
        }

        public List<Exter> exter_list { get; set; }
    }
    [Serializable]
    public class Exter
    {
        public Exter()
        {
            inner_list = new List<Inner>();
        }

        public List<Inner> inner_list { get; set; }
    }
    [Serializable]
    public class Inner
    {
        /// <summary>
        /// Sensitive_type为1的类目需要提供的资质文件名称
        /// </summary>
        public string name { get; set; }
    }
}
