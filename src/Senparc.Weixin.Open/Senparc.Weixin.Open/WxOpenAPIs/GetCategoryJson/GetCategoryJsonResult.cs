using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.WxOpenAPIs.GetCategoryJson
{
    /// <summary>
    /// 账号已经设置的所有类目
    /// </summary>
    [Serializable]
    public class GetCategoryJsonResult : WxJsonResult
    {
        public GetCategoryJsonResult()
        {
            categories = new List<Category>();
        }
        /// <summary>
        /// 一个更改周期内可以设置类目的次数
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 本更改周期内还可以设置类目的次数
        /// </summary>
        public int quota { get; set; }

        /// <summary>
        /// 最多可以设置的类目数量
        /// </summary>
        public int category_limit { get; set; }

        public IList<Category> categories { get; set; }


    }

    public class Category
    {
        /// <summary>
        /// 一级类目ID
        /// </summary>
        public int first { get; set; }

        /// <summary>
        /// 一级类目名称
        /// </summary>
        public string first_name { get; set; }

        /// <summary>
        /// 二级类目ID
        /// </summary>
        public int second { get; set; }

        /// <summary>
        /// 二级类目名称
        /// </summary>
        public string second_name { get; set; }

        /// <summary>
        /// 审核状态（1审核中 2审核不通过 3审核通过）
        /// </summary>
        public AuditStatus audit_status { get; set; }

        /// <summary>
        /// 审核不通过原因
        /// </summary>
        public string audit_reason { get; set; }
    }
}