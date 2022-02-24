/*----------------------------------------------------------------
    Copyright (C) 2022 Senparc
    
    文件名：CreateDraftResult.cs
    文件功能描述：新建草稿返回结果
    
    
    创建标识：zhongmeng - 2022224
 
   
----------------------------------------------------------------*/
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Draft
{
  

    /// <summary>
    /// 新建草稿返回结果
    /// </summary>
    public class CreateDraftResult : WxJsonResult
    {
        /// <summary>
        /// 新增的草稿的media_id
        /// </summary>
        public string media_id { get; set; }

    }
}
