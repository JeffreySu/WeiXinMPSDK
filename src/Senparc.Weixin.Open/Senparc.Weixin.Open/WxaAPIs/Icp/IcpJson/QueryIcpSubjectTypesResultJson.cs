/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：QueryIcpSubjectTypesResultJson.cs
    文件功能描述：获取单位性质 接口返回消息
    
    
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
    /// 获取单位性质
    /// </summary>
    public class QueryIcpSubjectTypesResultJson : WxJsonResult
    {
        /// <summary>
        /// 单位性质列表
        /// </summary>
        public List<SubjectTypesItemModel> items { get; set; }
    }

    public class SubjectTypesItemModel
    {
        /// <summary>
        /// 单位性质类型id
        /// </summary>
        public int type { get; set; }
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
