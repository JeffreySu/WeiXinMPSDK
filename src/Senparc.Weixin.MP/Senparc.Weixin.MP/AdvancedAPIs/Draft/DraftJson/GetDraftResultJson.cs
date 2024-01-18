#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetDraftResultJson.cs
    文件功能描述：获取草稿列表返回结果
    
    
    创建标识：dupeng0811 - 20220227

    修改标识：Senparc - 20220730
    修改描述：v16.18.4 完善 GetDraftResultJson 字段

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.Draft.DraftJson;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 获取草稿列表返回结果
    /// https://developers.weixin.qq.com/doc/offiaccount/Draft_Box/Get_draft.html
    /// </summary>
    public class GetDraftResultJson : WxJsonResult
    {
        public List<DraftItem> news_item { get; set; }
    }  
    
    public class GetDraftCountResultJson : WxJsonResult
    {
        public int total_count { get; set; }
    }

    public class DraftItem : DraftModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 图文消息的摘要，仅有单图文消息才有摘要，多图文此处为空。
        /// </summary>
        public string digest { get; set; }
        /// <summary>
        /// 图文消息的具体内容，支持 HTML 标签，必须少于2万字符，小于1M，且此处会去除JS。
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 图文消息的原文地址，即点击“阅读原文”后的URL
        /// </summary>
        public string content_source_url { get; set; }
        /// <summary>
        /// 图文消息的封面图片素材id（一定是永久MediaID）
        /// </summary>
        public string thumb_media_id { get; set; }
        /// <summary>
        /// 是否在正文显示封面。平台已不支持此功能，因此默认为0，即不展示
        /// </summary>
        public int show_cover_pic { get; set; }
        /// <summary>
        /// Uint32 是否打开评论，0不打开(默认)，1打开
        /// </summary>
        public uint need_open_comment { get; set; }
        /// <summary>
        /// Uint32 是否粉丝才可评论，0所有人可评论(默认)，1粉丝才可评论
        /// </summary>
        public uint only_fans_can_comment { get; set; }
        /// <summary>
        /// 草稿的临时链接
        /// </summary>
        public string url { get; set; }
    }

    
}
