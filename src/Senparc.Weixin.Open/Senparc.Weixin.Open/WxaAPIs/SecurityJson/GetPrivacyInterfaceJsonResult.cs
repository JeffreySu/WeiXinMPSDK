using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 获取隐私接口列表结果
    /// </summary>
    [Serializable]
    public class GetPrivacyInterfaceJsonResult : WxJsonResult
    {
        /// <summary>
        /// 隐私接口
        /// </summary>
        public List<PrivacyInterfaceInfo> interface_list { get; set; }
    }

    [Serializable]
    public class PrivacyInterfaceInfo
    {
        /// <summary>
        /// api 英文名
        /// </summary>
        public string api_name { get; set; }

        /// <summary>
        /// api 中文名
        /// </summary>
        public string api_ch_name { get; set; }

        /// <summary>
        /// api描述
        /// </summary>
        public string api_desc { get; set; }

        /// <summary>
        /// 申请时间 ，该字段发起申请后才会有
        /// </summary>
        public uint apply_time { get; set; }

        /// <summary>
        /// 接口状态，该字段发起申请后才会有
        /// 1-待申请开通,2-无权限,3-申请中,4-申请失败,5-已开通
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 申请单号，该字段发起申请后才会有
        /// </summary>
        public uint audit_id { get; set; }

        /// <summary>
        /// 申请被驳回原因或者无权限，该字段申请驳回时才会有
        /// </summary>
        public string fail_reason { get; set; }

        /// <summary>
        /// api文档链接
        /// </summary>
        public string api_link { get; set; }

        /// <summary>
        /// 分组名
        /// </summary>
        public string group_name { get; set; }
    }

}
