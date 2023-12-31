/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：ApplyIcpFilingResultJson.cs
    文件功能描述：申请小程序备案 接口返回消息
    
    
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
    /// 申请小程序备案 接口返回消息
    /// </summary>
    public class ApplyIcpFilingResultJson : WxJsonResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<HintsModel> hints { get; set; }
    }

    public class HintsModel
    {
        /// <summary>
        /// 字段校验的错误码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 校验失败的字段
        /// </summary>
        public string err_field { get; set; }

        /// <summary>
        /// 校验失败提示信息，示例值：`"缺少必填字段"`
        /// </summary>
        public string errmsg { get; set; }
    }
}
