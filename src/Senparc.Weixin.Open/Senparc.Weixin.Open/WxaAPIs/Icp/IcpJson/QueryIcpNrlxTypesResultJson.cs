/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：QueryIcpNrlxTypesResultJson.cs
    文件功能描述：获取前置审批项类型结果 接口返回消息
    
    
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
    /// 获取前置审批项类型结果
    /// </summary>
    public class QueryIcpNrlxTypesResultJson : WxJsonResult
    {
        /// <summary>
        /// 前置审批项类型列表
        /// </summary>
        public List<NrlxItemModel> items { get; set; }
    }

    public class NrlxItemModel
    {
        /// <summary>
        /// 前置审批项类型id
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
    }
}
