/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：GenerateNFCSchemeJsonResult.cs
    文件功能描述：GenerateNFCScheme() 接口返回参数
    
    
    创建标识：Senparc - 20241114

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.UrlScheme
{
    /// <summary>
    /// GenerateNFCScheme() 接口返回参数
    /// </summary>
    public class GenerateNFCSchemeJsonResult : WxJsonResult
    {
        /// <summary>
        /// 小程序scheme码
        /// </summary>
        public string openlink { get; set; }
    }
}
