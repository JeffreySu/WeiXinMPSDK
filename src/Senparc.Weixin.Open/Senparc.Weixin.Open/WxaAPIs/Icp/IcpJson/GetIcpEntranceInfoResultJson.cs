/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetIcpEntranceInfoResultJson.cs
    文件功能描述：注销小程序备案 接口返回消息
    
    
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
    public class GetIcpEntranceInfoResultJson : WxJsonResult
    {
        public GetIcpEntranceInfoModel info { get; set; }
    }

    /// <summary>
    /// 备案状态信息
    /// </summary>
    public class GetIcpEntranceInfoModel
    {
        /// <summary>
        /// 备案状态，取值参考备案状态枚举，示例值：`1024`
        /// </summary>
        public StatusCode status { get; set; }

        /// <summary>
        /// 是否正在注销备案
        /// </summary>
        public bool is_canceling { get; set; }

        /// <summary>
        /// 驳回原因，备案不通过时返回
        /// </summary>
        public List<AuditDataModel> audit_data { get; set; }
    }

    public enum StatusCode
    {
        平台审核中=2,
        平台审核驳回=3,
        管局审核中 = 4,
        管局审核驳回 = 5,
        已备案 = 6,
        未备案 = 1024,
        未备案_小程序基本信息未填 = 1025,
        未备案_小程序类目未填 = 1026,
        未备案_小程序基本信息未填_小程序类目未填 = 1027
    }


    /// <summary>
    /// 驳回原因，备案不通过时返回
    /// </summary>
    public class AuditDataModel
    {
        /// <summary>
        /// 审核不通过的字段中文名
        /// </summary>
        public string key_name { get; set; }

        /// <summary>
        /// 字段不通过的原因
        /// </summary>
        public string error { get; set; }
        /// <summary>
        /// 修改建议
        /// </summary>
        public string suggest { get; set; }
    }
}
