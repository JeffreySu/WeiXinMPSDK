using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.Wxa.MerchantJson
{
    /// <summary>
    /// 门店小程序类目信息
    /// </summary>
    public class GetMerchantCategoryResult : WxJsonResult
    {
        /// <summary>
        /// 所有类目数据
        /// </summary>
        public AllCategoryInfo data { get; set; }
    }
    /// <summary>
    /// 所有类目数据
    /// </summary>
    public class AllCategoryInfo
    {
        /// <summary>
        /// 所有类目数据
        /// </summary>
        public CateGoryCollection all_category_info { get; set; }
    }
    /// <summary>
    /// 类目信息集合
    /// </summary>
    public class CateGoryCollection
    {
        /// <summary>
        /// 类目信息集合
        /// </summary>
        public IEnumerable<MerchantCategory> categories { get; set; }
    }
    /// <summary>
    /// 类目信息
    /// </summary>
    public class MerchantCategory
    {
        /// <summary>
        /// 类目id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 类目的级别，一级或者二级类目
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 子级类目id
        /// </summary>
        public IEnumerable<int> children { get; set; }
        /// <summary>
        /// 父级类目Id
        /// </summary>
        public int? father { get; set; }
        /// <summary>
        /// 所需证件
        /// </summary>
        public QualifyExter_list qualify { get; set; }

        public int scene { get; set; }
        /// <summary>
        /// 0或者1， 0表示不用特殊处理 1表示创建该类目的门店小程序时，需要添加相关证件
        /// </summary>
        public int sensitive_type { get; set; }
    }

    public class QualifyValue
    {
        /// <summary>
        /// 证件名
        /// </summary>
        public string name { get; set; }
    }

    public class QualifyInner_list
    {
        public IEnumerable<QualifyValue> inner_list { get; set; }
    }

    public class QualifyExter_list
    {
        public IEnumerable<QualifyInner_list> exter_list { get; set; }
    }


}
