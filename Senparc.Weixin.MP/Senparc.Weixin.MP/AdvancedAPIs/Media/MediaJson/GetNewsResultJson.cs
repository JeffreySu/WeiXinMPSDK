/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：GetNewsResultJson.cs
    文件功能描述：获取图文类型永久素材返回结果
    
    
    创建标识：Senparc - 20150324
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Media
{
    /// <summary>
    /// 获取图文类型永久素材返回结果
    /// </summary>
    public class GetNewsResultJson : WxJsonResult
    {
        public List<NewsModel> news_item { get; set; }
    }
}
