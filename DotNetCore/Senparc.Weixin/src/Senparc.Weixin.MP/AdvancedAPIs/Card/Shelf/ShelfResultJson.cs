/*----------------------------------------------------------------
    Copyright (C) 2016 LSW
    
    文件名：ShelfResultJson.cs
    文件功能描述：创建货架返回结果
    
    
    创建标识：LSW - 20150907
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 创建货架返回结果
    /// </summary>
    public class ShelfCreateResultJson : WxJsonResult
    {
        /// <summary>
        /// 货架链接。
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 货架ID。货架的唯一标识。
        /// </summary>
        public int page_id { get; set; }
    }
}
