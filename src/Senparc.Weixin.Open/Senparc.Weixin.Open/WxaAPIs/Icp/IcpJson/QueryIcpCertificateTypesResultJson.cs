/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：QueryIcpCertificateTypesResultJson.cs
    文件功能描述：获取证件类型 接口返回消息
    
    
    创建标识：Senparc - 20230905

----------------------------------------------------------------*/

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
        /// 证件类型id
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 该证件类型对应的单位性质，在该单位性质下可以选择该证件类型
        /// </summary>
        public int subject_type { get; set; }

        /// <summary>
        /// 证件名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
