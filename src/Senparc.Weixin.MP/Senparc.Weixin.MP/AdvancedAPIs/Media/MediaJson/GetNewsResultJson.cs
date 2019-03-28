/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetNewsResultJson.cs
    文件功能描述：获取图文类型永久素材返回结果
    
    
    创建标识：Senparc - 20150324
 
   
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;

namespace Senparc.Weixin.MP.AdvancedAPIs.Media
{
    /// <summary>
    /// 获取图文类型永久素材返回结果
    /// </summary>
    public class GetNewsResultJson : WxJsonResult
    {
        public List<ForeverNewsItem> news_item { get; set; }
    }

    public class ForeverNewsItem : NewsModel
    {
        /// <summary>
        /// 图文页的URL
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 获取语音识别结果返回结果
    /// </summary>
    public class QueryRecoResultResultJson : WxJsonResult
    {
        public string result { get; set; }
    }

    /// <summary>
    /// 微信翻译返回结果
    /// </summary>
    public class TranslateContentResultJson : WxJsonResult
    {
        public string from_content { get; set; }
        public string to_content { get; set; }
    }
}
