/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：AddContactWayResult.cs
    文件功能描述：配置客户联系「联系我」方式 返回数据
    
    
    创建标识：Senparc - 20210321
    
----------------------------------------------------------------*/


using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 配置客户联系「联系我」方式 返回数据
    /// </summary>
    public class AddContactWayResult : WorkJsonResult
    {
        /// <summary>
        /// 新增联系方式的配置id
        /// </summary>
        public string config_id { get; set; }
        /// <summary>
        /// 联系我二维码链接，仅在scene为2时返回
        /// </summary>
        public string qr_code { get; set; }
    }
}
