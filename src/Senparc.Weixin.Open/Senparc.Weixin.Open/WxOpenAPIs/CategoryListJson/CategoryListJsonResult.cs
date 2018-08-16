using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.WxOpenAPIs.CategoryListJson
{
    /// <summary>
    /// 账号可以设置的所有类目
    /// </summary>
    [Serializable]
    public class CategoryListJsonResult : WxJsonResult
    {
        public IList<Category> categories { get; set; }
    }

    public class Category
    {
        /// <summary>
        /// 类目Id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 类目名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 类目父级Id
        /// </summary>
        public int father { get; set; }

        /// <summary>
        /// 子级类目Id
        /// </summary>
        public IList<int> children { get; set; } = new List<int>();

        /// <summary>
        /// 是否为敏感类目（1为敏感类目，需要提供相应资质审核；0为非敏感类目，无需审核
        /// </summary>
        public int sensitive_type { get; set; }

        public Qualify qualify { get; set; }
    }

    public class Qualify
    {
        public Qualify()
        {
            exter_list = new List<Exter>();
        }

        public IList<Exter> exter_list { get; set; }
    }

    public class Exter
    {
        public Exter()
        {
            inner_list = new List<Inner>();
        }

        public IList<Inner> inner_list { get; set; }
    }

    public class Inner
    {
        /// <summary>
        /// Sensitive_type为1的类目需要提供的资质文件名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 资质文件示例
        /// </summary>
        public string url { get; set; }
    }
}