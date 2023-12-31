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
    
    文件名：FreePublishBatchGetResultJson.cs
    文件功能描述：获取成功发布列表 返回结果

    
    创建标识：dupeng0811 - 20220227

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.FreePublish.FreePublishJson
{
    /// <summary>
    /// 获取成功发布列表 返回结果
    /// </summary>
    public class FreePublishBatchGetResultJson : BaseFreePublishListResultJson
    {
        public List<FreePublishList_Item> item { get; set; }
    }

    public class BaseFreePublishListResultJson : WxJsonResult
    {
        /// <summary>
        /// 该类型的素材的总数
        /// </summary>
        public int total_count { get; set; }

        /// <summary>
        /// 本次调用获取的素材的数量
        /// </summary>
        public int item_count { get; set; }
    }


    public class FreePublishList_Item
    {
        /// <summary>
        /// 成功发布的图文消息id
        /// </summary>
        public string article_id { get; set; }
        /// <summary>
        /// 图文消息的具体内容，支持HTML标签，必须少于2万字符，小于1M，且此处会去除JS。
        /// </summary>
        public FreePublishGetArticleResultJson content { get; set; }
        /// <summary>
        /// 这篇图文消息素材的最后更新时间
        /// </summary>
        public long update_time { get; set; }
    }
}