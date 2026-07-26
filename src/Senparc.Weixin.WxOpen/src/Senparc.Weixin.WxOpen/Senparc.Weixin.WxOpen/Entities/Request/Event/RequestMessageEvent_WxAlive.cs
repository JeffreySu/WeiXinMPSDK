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

    文件名：RequestMessageEvent_WxAlive.cs
    文件功能描述：RequestMessageEvent_WxAlive 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>小程序直播长期订阅状态通知。</summary>
    /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/event_push/liveplayer/status_synchronization.html"/>。</remarks>
    public class RequestMessageEvent_WxAliveFollowNotify : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>事件类型，固定为 <see cref="WxOpen.Event.wxalive_follow_notify"/>。</summary>
        public override Event Event => Event.wxalive_follow_notify;

        /// <summary>用户订阅或取消订阅的详情。</summary>
        /// <remarks>官方参数表将字段平铺展示，但 XML 示例使用 <c>FollowNotify</c> 外层节点。</remarks>
        public WxAliveFollowNotifyInfo FollowNotify { get; set; }
    }

    /// <summary>小程序直播长期订阅状态详情。</summary>
    public class WxAliveFollowNotifyInfo
    {
        /// <summary>用户操作订阅时所在的直播间 ID。</summary>
        public long room_id { get; set; }

        /// <summary>订阅用户的 OpenId。</summary>
        public string user_openid { get; set; }

        /// <summary>订阅或取消订阅的 Unix 时间戳。</summary>
        public long time { get; set; }

        /// <summary>操作时的直播间状态：101 直播中，102 未开始，103 已结束。</summary>
        public int live_status { get; set; }

        /// <summary>订阅行为：<c>add_follow</c> 订阅，<c>del_follow</c> 取消订阅。</summary>
        public string action { get; set; }
    }

    /// <summary>小程序直播长期订阅群发结果通知。</summary>
    /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/event_push/liveplayer/longterm_subscription.html"/>。</remarks>
    public class RequestMessageEvent_WxAlivePushMessageNotify : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>事件类型，固定为 <see cref="WxOpen.Event.wxalive_push_message_notify"/>。</summary>
        public override Event Event => Event.wxalive_push_message_notify;

        /// <summary>长期订阅群发最终结果。</summary>
        /// <remarks>官方参数表将字段平铺展示，但 XML 示例使用 <c>PushMessageApiNotify</c> 外层节点。</remarks>
        public WxAlivePushMessageNotifyInfo PushMessageApiNotify { get; set; }
    }

    /// <summary>小程序直播长期订阅群发最终结果。</summary>
    public class WxAlivePushMessageNotifyInfo
    {
        /// <summary>调用发送直播开始事件接口时返回的群发消息标识。</summary>
        public string message_id { get; set; }

        /// <summary>直播间 ID。</summary>
        public long room_id { get; set; }

        /// <summary>本次群发提交的 OpenId 总数。</summary>
        public int total_count { get; set; }

        /// <summary>群发成功数量。</summary>
        public int success_count { get; set; }

        /// <summary>OpenId 错误数量。</summary>
        public int openid_error_count { get; set; }

        /// <summary>用户未关注小程序导致的失败数量。</summary>
        public int relation_error_count { get; set; }

        /// <summary>用户接收消息超出限制导致的失败数量。</summary>
        public int user_recv_limit_count { get; set; }

        /// <summary>其他内部错误数量。</summary>
        public int internal_error_count { get; set; }
    }
}
