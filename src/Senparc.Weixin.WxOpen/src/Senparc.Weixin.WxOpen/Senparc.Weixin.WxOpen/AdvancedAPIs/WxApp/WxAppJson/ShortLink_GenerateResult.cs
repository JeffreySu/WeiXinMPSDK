/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：Short ShortLink_GenerateResult.cs
    文件功能描述：Short Link 生成结果
    官方文档：https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/short-link/shortlink.generate.html
    
    
    创建标识：Senparc - 20210930
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp
{
    /// <summary>
    /// Short Link 生成结果
    /// </summary>
    public class ShortLink_GenerateResult : WxJsonResult
    {
        /// <summary>
        /// Short Link
        /// </summary>
        public string link { get; set; }
    }
}
