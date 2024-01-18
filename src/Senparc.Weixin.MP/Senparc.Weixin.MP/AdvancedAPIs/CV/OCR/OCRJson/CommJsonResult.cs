/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：CommJsonResult.cs
    文件功能描述：OCR 通用印刷体识别返回结果
    
    
    创建标识：yaofeng - 20231204

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.CV.OCR
{
    /// <summary>
    /// 通用印刷体识别
    /// </summary>
    public class CommJsonResult : WxJsonResult
    {
        /// <summary>
        /// 识别结果
        /// </summary>
        public List<Common_Item> items { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Common_Item
    {
        /// <summary>
        /// 
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Pos pos { get; set; }
    }
}
