#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：MiniDramaPlayerJson.cs
    文件功能描述：MiniDramaPlayerJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.MiniDrama
{
    /// <summary>短剧播放器开关设置请求。</summary>
    public class MiniDramaPlayerSwitchRequest
    {
        /// <summary>入口类型；2002 表示播放原始视频，1/2/3 表示对应推荐位。</summary>
        public int entry_type { get; set; }

        /// <summary>是否打开对应能力。</summary>
        public bool switch_status { get; set; }
    }

    /// <summary>短剧播放器剧目标识。</summary>
    public class MiniDramaPlayerDramaIdentity
    {
        /// <summary>提审方小程序 AppId。</summary>
        public string src_appid { get; set; }

        /// <summary>提审剧目 ID。播放器接口按官方定义使用字符串。</summary>
        public string drama_id { get; set; }
    }

    /// <summary>刷剧剧目设置项。</summary>
    public class MiniDramaFlushDramaItem : MiniDramaPlayerDramaIdentity
    {
        /// <summary>短剧名称。</summary>
        public string drama_name { get; set; }
    }

    /// <summary>刷剧剧目设置请求。</summary>
    public class MiniDramaSetFlushDramaRequest
    {
        /// <summary>刷剧剧目列表。</summary>
        public IList<MiniDramaFlushDramaItem> list { get; set; }
    }

    /// <summary>推荐剧目设置请求。</summary>
    public class MiniDramaSetRecommendedDramaRequest : MiniDramaPlayerDramaIdentity
    {
        /// <summary>推荐入口：1 剧结束、2 选集最右侧、3 剧集 Profile 页。</summary>
        public int entry_type { get; set; }

        /// <summary>推荐剧目列表。</summary>
        public IList<MiniDramaPlayerDramaIdentity> list { get; set; }
    }

    /// <summary>短剧上架设置项。</summary>
    public class MiniDramaPublishDramaItem : MiniDramaFlushDramaItem
    {
        /// <summary>上架时间戳，单位为秒。</summary>
        public long publish_time { get; set; }
    }

    /// <summary>短剧上架请求。</summary>
    public class MiniDramaPublishDramaRequest
    {
        /// <summary>待设置上架时间的短剧列表。</summary>
        public IList<MiniDramaPublishDramaItem> list { get; set; }
    }

    /// <summary>已上架短剧信息。</summary>
    public class MiniDramaPublishedDrama : MiniDramaPlayerDramaIdentity
    {
        /// <summary>上架时间戳，单位为秒。</summary>
        public long publish_time { get; set; }
    }

    /// <summary>已上架短剧查询结果。</summary>
    public class MiniDramaGetPublishedDramaJsonResult : WxJsonResult
    {
        /// <summary>已上架短剧列表。</summary>
        public IList<MiniDramaPublishedDrama> list { get; set; }
    }

    /// <summary>短剧变现类型设置项。</summary>
    public class MiniDramaMonetizationItem : MiniDramaPlayerDramaIdentity
    {
        /// <summary>变现类型：1 纯 IAA、2 纯 IAP、3 IAAP 混合变现。</summary>
        public int iaa_type { get; set; }

        /// <summary>是否存在会员功能：1 有，2 无。</summary>
        public int vip_flag { get; set; }
    }

    /// <summary>设置短剧变现类型请求。</summary>
    public class MiniDramaSetMonetizationRequest
    {
        /// <summary>短剧变现设置列表。</summary>
        public IList<MiniDramaMonetizationItem> list { get; set; }
    }

    /// <summary>查询短剧变现类型请求。</summary>
    public class MiniDramaGetMonetizationRequest
    {
        /// <summary>待查询短剧列表。</summary>
        public IList<MiniDramaPlayerDramaIdentity> list { get; set; }
    }

    /// <summary>短剧变现类型查询结果。</summary>
    public class MiniDramaGetMonetizationJsonResult : WxJsonResult
    {
        /// <summary>短剧变现信息列表。</summary>
        public IList<MiniDramaMonetizationItem> list { get; set; }
    }

    /// <summary>批处理短剧合作推广计划请求。</summary>
    public class MiniDramaPromotionRequest
    {
        /// <summary>操作类型：1 加入、2 查询、3 退出。</summary>
        public int action_type { get; set; }

        /// <summary>待处理短剧列表。</summary>
        public IList<MiniDramaPlayerDramaIdentity> list { get; set; }
    }

    /// <summary>短剧合作推广计划状态。</summary>
    public class MiniDramaPromotionStatus : MiniDramaPlayerDramaIdentity
    {
        /// <summary>状态：0 未加入、1 审核中、2 通过、3 拒绝、4 退出中、5 已下架。</summary>
        public int status { get; set; }
    }

    /// <summary>批处理短剧合作推广计划结果。</summary>
    public class MiniDramaPromotionJsonResult : WxJsonResult
    {
        /// <summary>查询计划时返回的短剧状态列表。</summary>
        public IList<MiniDramaPromotionStatus> list { get; set; }
    }

    /// <summary>获取短剧合作推广活动请求。</summary>
    public class MiniDramaGetFinderEventRequest
    {
        /// <summary>可选加密活动 ID 列表；空数组表示全量读取。</summary>
        public IList<string> event_id_list { get; set; }
    }

    /// <summary>短剧合作推广活动。</summary>
    public class MiniDramaFinderEvent : MiniDramaPlayerDramaIdentity
    {
        /// <summary>加密活动 ID。</summary>
        public string encrypted_event_id { get; set; }

        /// <summary>活动名称。</summary>
        public string event_name { get; set; }

        /// <summary>活动链接。</summary>
        public string event_url { get; set; }
    }

    /// <summary>短剧合作推广活动查询结果。</summary>
    public class MiniDramaGetFinderEventJsonResult : WxJsonResult
    {
        /// <summary>推广活动列表。</summary>
        public IList<MiniDramaFinderEvent> finder_event_list { get; set; }
    }
}
