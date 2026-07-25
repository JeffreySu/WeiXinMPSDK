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

    文件名：LiveBroadcastRoleSubscriptionJson.cs
    文件功能描述：LiveBroadcastRoleSubscriptionJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.LiveBroadcast
{
    /// <summary>直播成员角色操作请求。</summary>
    public class LiveBroadcastRoleRequest
    {
        /// <summary>用户微信号。</summary>
        public string username { get; set; }

        /// <summary>角色：1 管理员，2 主播，3 运营者。</summary>
        public int role { get; set; }
    }

    /// <summary>设置直播成员角色结果。</summary>
    public class LiveBroadcastRoleJsonResult : WxJsonResult
    {
        /// <summary>主播未实名认证时返回的实名认证小程序码 URL。</summary>
        public string codeurl { get; set; }
    }

    /// <summary>直播成员信息。</summary>
    public class LiveBroadcastRoleInfo
    {
        /// <summary>微信用户头像 URL。</summary>
        public string headingimg { get; set; }

        /// <summary>微信用户昵称。</summary>
        public string nickname { get; set; }

        /// <summary>用户 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>角色列表：0 超级管理员，1 管理员，2 主播，3 运营者。</summary>
        public IList<int> roleList { get; set; }

        /// <summary>更新时间戳；官方示例以字符串返回。</summary>
        public string updateTimestamp { get; set; }

        /// <summary>脱敏微信号。</summary>
        public string username { get; set; }
    }

    /// <summary>直播成员列表结果。</summary>
    public class LiveBroadcastRoleListJsonResult : WxJsonResult
    {
        /// <summary>成员总数。</summary>
        public int total { get; set; }

        /// <summary>成员角色列表。</summary>
        public IList<LiveBroadcastRoleInfo> list { get; set; }
    }

    /// <summary>向长期订阅用户发送开播事件请求。</summary>
    public class LiveBroadcastPushMessageRequest
    {
        /// <summary>直播间 ID。</summary>
        public long room_id { get; set; }

        /// <summary>接收开播事件的订阅用户 OpenId 列表。</summary>
        public IList<string> user_openid { get; set; }
    }

    /// <summary>发送开播事件结果。</summary>
    public class LiveBroadcastPushMessageJsonResult : WxJsonResult
    {
        /// <summary>群发消息标识，用于关联长期订阅群发结果回调。</summary>
        public string message_id { get; set; }
    }

    /// <summary>分页获取长期订阅用户请求。</summary>
    public class LiveBroadcastGetFollowersRequest
    {
        /// <summary>拉取数量，默认 200，最大 2000。</summary>
        public int? limit { get; set; }

        /// <summary>翻页标记；第一页可不设置，后续使用上次返回值。</summary>
        public long? page_break { get; set; }
    }

    /// <summary>长期订阅用户信息。</summary>
    public class LiveBroadcastFollowerInfo
    {
        /// <summary>订阅用户 OpenId。</summary>
        public string openid { get; set; }

        /// <summary>订阅 Unix 时间戳。</summary>
        public long subscribe_time { get; set; }

        /// <summary>用户订阅时所在直播间 ID。</summary>
        public long room_id { get; set; }

        /// <summary>用户订阅时的直播间状态。</summary>
        public int room_status { get; set; }
    }

    /// <summary>长期订阅用户列表结果。</summary>
    public class LiveBroadcastGetFollowersJsonResult : WxJsonResult
    {
        /// <summary>长期订阅用户列表。</summary>
        public IList<LiveBroadcastFollowerInfo> followers { get; set; }

        /// <summary>下一页翻页标记。</summary>
        public long? page_break { get; set; }
    }
}
