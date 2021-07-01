using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    /// <summary>
    /// 拉取门店小程序类目信息结果
    /// </summary>
    public class GetStoreWxaAttrJsonResult : WxJsonResult
    {
        /// <summary>
        /// 等于false表示从来没有申请过类目
        /// </summary>
        public bool is_exist { get; set; }

        public StoreWxaAttr store_wxa_attr { get; set; }

        public WeappCategory weapp_category { get; set; }
    }
    [Serializable]
    public class StoreWxaAttr
    {
        public long appuin { get; set; }

        public long create_time { get; set; }

        public long update_time { get; set; }

        public long owner_uin { get; set; }

        public int owner_type { get; set; }

         public StoreWxaAuditInfo storewxa_audit_info { get; set; }
    }
    [Serializable]
    public class StoreWxaAuditInfo
    {
        /// <summary>
        /// 审核ID
        /// </summary>
        public long audit_id { get; set; }

        /// <summary>
        /// 1 类目审核通过 2 类目审核中 3 类目审核失败
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 审核失败时，返回信息
        /// </summary>
        public string reason { get; set; }
    }
    [Serializable]
    public class WeappCategory
    {
        /// <summary>
        /// 如果数组大小等于2 ，第一个表示之前申请成功的类目，第二个表示当前正在审核中的类目
        /// </summary>
        public List<Categories> categories { get; set; } = new List<Categories>();
    }
    [Serializable]
    public class Categories
    {
        /// <summary>
        /// 一级类目ID
        /// </summary>
        public int first { get; set; }

        /// <summary>
        /// 二级类目ID
        /// </summary>
        public int second { get; set; }

        /// <summary>
        /// 1审核中,2审核失败,3审核通过
        /// </summary>
        public int audit_status { get; set; }

        /// <summary>
        /// 审核ID
        /// </summary>
        public long audit_id { get; set; }
    }
}
