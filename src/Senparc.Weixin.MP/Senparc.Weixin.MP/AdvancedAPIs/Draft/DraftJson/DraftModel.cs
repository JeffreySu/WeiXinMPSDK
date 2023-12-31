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
    
    文件名：DraftModel.cs
    文件功能描述：草稿提交参数
    
    
    创建标识：dupeng0811 - 20220227

----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Draft.DraftJson
{
    /// <summary>
    /// 
    /// </summary>
    public class DraftModel
    {
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 图文消息的作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 图文消息的描述
        /// </summary>
        public string digest { get; set; }

        /// <summary>
        /// 图文消息页面的内容，支持HTML标签
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 在图文消息页面点击“阅读原文”后的页面
        /// </summary>
        public string content_source_url { get; set; }

        /// <summary>
        /// 图文消息缩略图的media_id，可以在基础支持上传多媒体文件接口中获得
        /// </summary>
        public string thumb_media_id { get; set; }

        /// <summary>
        /// 是否显示封面，0为false，即不显示，1为true，即显示(默认)
        /// </summary>
        public string show_cover_pic { get; set; }

        /// <summary>
        /// 是否打开评论，0不打开，1打开
        /// </summary>
        public int need_open_comment { get; set; }
        /// <summary>
        /// 是否粉丝才可评论，0所有人可评论，1粉丝才可评论
        /// </summary>
        public int only_fans_can_comment { get; set; }
    }
}