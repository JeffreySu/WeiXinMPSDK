using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs.Icp.IcpJson
{
    /// <summary>
    /// 获取证件类型
    /// </summary>
    public class QueryIcpCertificateTypesResultJson : WxJsonResult
    {
        /// <summary>
        /// 证件类型列表
        /// </summary>
        public List<QueryIcpCertificateTypesItemModel> items { get; set; }
    }

    public class QueryIcpCertificateTypesItemModel
    {
        /// <summary>
        /// 服务内容类型id
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 该服务内容类型的父类型id
        /// </summary>
        public int parent_type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
