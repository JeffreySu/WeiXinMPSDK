/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：QueryIcpServiceContentTypesResultJson.cs
    文件功能描述：获取小程序服务内容类型 接口返回消息
    
    
    创建标识：Senparc - 20230905

----------------------------------------------------------------*/

using Senparc.Weixin.Annotations;
using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs.Icp.IcpJson
{
    /// <summary>
    /// 获取小程序服务内容类型
    /// </summary>
    public class QueryIcpServiceContentTypesResultJson :WxJsonResult
    {
        /// <summary>
        /// 服务内容类型列表
        /// </summary>
        public List<QueryIcpServiceCoententTypesItemModel> items { get; set; }
    }

    /// <summary>
    /// 服务内容类型
    /// </summary>
    public class QueryIcpServiceCoententTypesItemModel
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
