/*----------------------------------------------------------------
    Copyright (C) 2022 Senparc
    
    文件名：GetDraftCountResult.cs
    文件功能描述：获取草稿总数返回结果
    
    
    创建标识：zhongmeng - 2022224
 
   
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Draft
{
    /// <summary>
    /// 获取草稿总数返回结果
    /// </summary>
    public class GetDraftCountResult : WxJsonResult
    {
        /// <summary>
        /// 草稿总数
        /// </summary>
        public int total_count { get; set; }
       
    }
}
