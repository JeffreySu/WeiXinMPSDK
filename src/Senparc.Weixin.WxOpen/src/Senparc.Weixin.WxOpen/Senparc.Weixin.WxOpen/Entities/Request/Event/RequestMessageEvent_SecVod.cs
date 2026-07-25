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

    文件名：RequestMessageEvent_SecVod.cs
    文件功能描述：RequestMessageEvent_SecVod 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>短剧媒资上传完成事件。</summary>
    /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/event_push/mini_drama/asset_upload_completion_event.html"/>。</remarks>
    public class RequestMessageEvent_SecVodUpload : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>事件类型，固定为 <see cref="WxOpen.Event.secvod_upload_event"/>。</summary>
        public override Event Event => Event.secvod_upload_event;

        /// <summary>上传结果。</summary>
        public SecVodUploadEvent upload_event { get; set; }
    }

    /// <summary>短剧媒资上传结果。</summary>
    public class SecVodUploadEvent
    {
        /// <summary>上传成功后的媒资 ID。</summary>
        public long media_id { get; set; }

        /// <summary>上传接口中设置的来源上下文。</summary>
        public string source_context { get; set; }

        /// <summary>上传错误码，0 表示成功。</summary>
        public int errcode { get; set; }

        /// <summary>上传错误提示。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>短剧剧目审核状态事件。</summary>
    /// <remarks>微信官方文档：<see href="https://developers.weixin.qq.com/miniprogram/dev/server/event_push/mini_drama/drama_review_status_event.html"/>。</remarks>
    public class RequestMessageEvent_SecVodAudit : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>事件类型，固定为 <see cref="WxOpen.Event.secvod_audit_event"/>。</summary>
        public override Event Event => Event.secvod_audit_event;

        /// <summary>剧目审核结果。</summary>
        public SecVodAuditEvent audit_event { get; set; }
    }

    /// <summary>短剧剧目审核结果。</summary>
    public class SecVodAuditEvent
    {
        /// <summary>剧目 ID。</summary>
        public long drama_id { get; set; }

        /// <summary>审核详情。</summary>
        public SecVodDramaAuditDetail audit_detail { get; set; }
    }

    /// <summary>短剧剧目审核详情。</summary>
    public class SecVodDramaAuditDetail
    {
        /// <summary>审核状态：0 无效、1 审核中、2 最终失败、3 通过、4 驳回重填。</summary>
        public int status { get; set; }

        /// <summary>审核类型：0 首次提审、1 再次提审、2 替换剧集、3 修改基本信息。</summary>
        public int audit_type { get; set; }

        /// <summary>提审时间戳。</summary>
        public long create_time { get; set; }

        /// <summary>审核时间戳。</summary>
        public long audit_time { get; set; }
    }
}
