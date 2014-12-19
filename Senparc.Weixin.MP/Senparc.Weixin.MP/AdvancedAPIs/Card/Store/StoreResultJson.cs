using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 批量导入门店数据返回结果
    /// </summary>
    public class StoreResultJson : WxJsonResult
    {
        /// <summary>
        /// 门店ID。插入失败的门店返回数值“-1”，请核查必填字段后单独调用接口导入。
        /// </summary>
        public string location_id { get; set; }
    }

    /// <summary>
    /// 拉取门店列表返回结果
    /// </summary>
    public class StoreGetResultJson : WxJsonResult
    {
        public List<SingleStoreResult> location_list { get; set; }
        /// <summary>
        /// 拉取门店数量
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    /// 单条门店返回结果
    /// </summary>
    public class SingleStoreResult
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public string location_id { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string latitude { get; set; }
    }
}
