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

    文件名：MiniDramaMediaJson.cs
    文件功能描述：MiniDramaMediaJson 强类型数据模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.MiniDrama
{
    /// <summary>短剧媒资拉取上传请求。</summary>
    public class MiniDramaPullUploadRequest
    {
        /// <summary>文件名，需采用“剧目名 - 对应剧集数”格式。</summary>
        public string media_name { get; set; }

        /// <summary>待拉取的视频 URL。</summary>
        public string media_url { get; set; }

        /// <summary>可选封面图 URL；为空时微信默认截取视频首帧。</summary>
        public string cover_url { get; set; }

        /// <summary>可选来源上下文，上传完成事件会原样透传。</summary>
        public string source_context { get; set; }
    }

    /// <summary>短剧异步任务查询请求。</summary>
    public class MiniDramaGetTaskRequest
    {
        /// <summary>拉取上传任务 ID。</summary>
        public long task_id { get; set; }
    }

    /// <summary>短剧分片上传申请请求。</summary>
    public class MiniDramaApplyUploadRequest
    {
        /// <summary>文件名，需采用“剧目名 - 对应剧集数”格式。</summary>
        public string media_name { get; set; }

        /// <summary>视频格式，例如 MP4、MOV、AVI 或 MKV。</summary>
        public string media_type { get; set; }

        /// <summary>可选封面格式，例如 JPG、PNG 或 TIFF；为空时默认截取视频首帧。</summary>
        public string cover_type { get; set; }

        /// <summary>可选来源上下文，上传完成事件会原样透传。</summary>
        public string source_context { get; set; }
    }

    /// <summary>短剧分片信息。</summary>
    public class MiniDramaPartInfo
    {
        /// <summary>分片编号，从 1 开始。</summary>
        public int part_number { get; set; }

        /// <summary>上传分片接口返回的 ETag。</summary>
        public string etag { get; set; }
    }

    /// <summary>确认短剧分片上传请求。</summary>
    public class MiniDramaCommitUploadRequest
    {
        /// <summary>申请分片上传接口返回的唯一标识。</summary>
        public string upload_id { get; set; }

        /// <summary>视频分片列表。官方参数表标为 object，官方示例实际为数组。</summary>
        public IList<MiniDramaPartInfo> media_part_infos { get; set; }

        /// <summary>可选封面分片列表。官方参数表标为 object，按官方示例使用数组。</summary>
        public IList<MiniDramaPartInfo> cover_part_infos { get; set; }
    }

    /// <summary>短剧媒资列表查询请求。</summary>
    public class MiniDramaListMediaRequest
    {
        /// <summary>可选剧目 ID。</summary>
        public long? drama_id { get; set; }

        /// <summary>可选媒资文件名，支持精确或模糊匹配。</summary>
        public string media_name { get; set; }

        /// <summary>可选媒资文件名模糊匹配条件，文件较多时推荐使用。</summary>
        public string media_name_fuzzy { get; set; }

        /// <summary>可选上传时间下限，Unix 时间戳。</summary>
        public long? start_time { get; set; }

        /// <summary>可选上传时间上限，Unix 时间戳。</summary>
        public long? end_time { get; set; }

        /// <summary>可选分页大小，最大 100。</summary>
        public int? limit { get; set; }

        /// <summary>可选分页偏移量。</summary>
        public int? offset { get; set; }
    }

    /// <summary>短剧媒资 ID 请求。</summary>
    public class MiniDramaMediaIdRequest
    {
        /// <summary>媒资文件 ID。</summary>
        public long media_id { get; set; }
    }

    /// <summary>短剧媒资播放链接请求。</summary>
    public class MiniDramaGetMediaLinkRequest : MiniDramaMediaIdRequest
    {
        /// <summary>播放地址过期时间戳，最长不能超过当前时间后两小时。</summary>
        public long t { get; set; }

        /// <summary>可选开发者链接标识，微信会追加平台标识。</summary>
        public string us { get; set; }

        /// <summary>可选试看时长，单位为秒。</summary>
        public int? exper { get; set; }

        /// <summary>可选允许播放的不同 IP 数量，最大 9。</summary>
        public int? rlimit { get; set; }

        /// <summary>可选允许访问的域名列表，使用半角逗号分隔。</summary>
        public string whref { get; set; }

        /// <summary>可选禁止访问的域名列表，使用半角逗号分隔。</summary>
        public string bkref { get; set; }
    }

    /// <summary>短剧媒资审核信息。</summary>
    public class MiniDramaMediaAuditDetail
    {
        /// <summary>审核状态：0 无效、1 审核中、2 驳回、3 通过、4 驳回重填。</summary>
        public int status { get; set; }

        /// <summary>提审时间戳。</summary>
        public long create_time { get; set; }

        /// <summary>审核时间戳。</summary>
        public long audit_time { get; set; }

        /// <summary>审核备注，可能为空。</summary>
        public string reason { get; set; }

        /// <summary>审核证据截图素材 ID 列表。</summary>
        public IList<string> evidence_material_id_list { get; set; }
    }

    /// <summary>短剧媒资信息。</summary>
    public class MiniDramaMediaInfo
    {
        /// <summary>媒资文件 ID。</summary>
        public long media_id { get; set; }

        /// <summary>上传时间戳。</summary>
        public long create_time { get; set; }

        /// <summary>过期时间戳。</summary>
        public long expire_time { get; set; }

        /// <summary>所属剧目 ID。</summary>
        public long drama_id { get; set; }

        /// <summary>文件大小，单位为字节。官方定义为字符串以避免大数精度损失。</summary>
        public string file_size { get; set; }

        /// <summary>播放时长，单位为秒。</summary>
        public int duration { get; set; }

        /// <summary>媒资文件名。</summary>
        public string name { get; set; }

        /// <summary>媒资描述。</summary>
        public string description { get; set; }

        /// <summary>封面图临时链接。</summary>
        public string cover_url { get; set; }

        /// <summary>原始视频临时链接。</summary>
        public string original_url { get; set; }

        /// <summary>MP4 格式临时链接。</summary>
        public string mp4_url { get; set; }

        /// <summary>HLS 格式临时链接。</summary>
        public string hls_url { get; set; }

        /// <summary>审核信息。</summary>
        public MiniDramaMediaAuditDetail audit_detail { get; set; }
    }

    /// <summary>短剧媒资播放信息。</summary>
    public class MiniDramaMediaPlayInfo
    {
        /// <summary>媒资文件 ID。</summary>
        public long media_id { get; set; }

        /// <summary>播放时长，单位为秒。</summary>
        public int duration { get; set; }

        /// <summary>媒资文件名。</summary>
        public string name { get; set; }

        /// <summary>媒资描述。</summary>
        public string description { get; set; }

        /// <summary>封面图临时链接。</summary>
        public string cover_url { get; set; }

        /// <summary>带鉴权参数的 MP4 临时播放链接。</summary>
        public string mp4_url { get; set; }

        /// <summary>带鉴权参数的 HLS 临时播放链接。</summary>
        public string hls_url { get; set; }
    }

    /// <summary>短剧异步任务信息。</summary>
    public class MiniDramaTaskInfo
    {
        /// <summary>任务 ID。</summary>
        public long id { get; set; }

        /// <summary>任务类型，当前 1 表示拉取上传任务。</summary>
        public int task_type { get; set; }

        /// <summary>任务状态：1 等待、2 处理中、3 完成、4 失败。</summary>
        public int status { get; set; }

        /// <summary>任务错误码，0 表示成功。</summary>
        public int errcode { get; set; }

        /// <summary>任务错误原因。</summary>
        public string errmsg { get; set; }

        /// <summary>创建时间戳。</summary>
        public long create_time { get; set; }

        /// <summary>完成时间戳。</summary>
        public long finish_time { get; set; }

        /// <summary>上传完成后的媒资文件 ID。</summary>
        public long media_id { get; set; }
    }

    /// <summary>短剧媒资上传结果。</summary>
    public class MiniDramaMediaIdJsonResult : WxJsonResult
    {
        /// <summary>媒资文件 ID。</summary>
        public long media_id { get; set; }
    }

    /// <summary>短剧拉取上传结果。</summary>
    public class MiniDramaPullUploadJsonResult : WxJsonResult
    {
        /// <summary>拉取上传任务 ID。</summary>
        public long task_id { get; set; }
    }

    /// <summary>短剧异步任务查询结果。</summary>
    public class MiniDramaGetTaskJsonResult : WxJsonResult
    {
        /// <summary>任务信息。</summary>
        public MiniDramaTaskInfo task_info { get; set; }
    }

    /// <summary>短剧分片上传申请结果。</summary>
    public class MiniDramaApplyUploadJsonResult : WxJsonResult
    {
        /// <summary>本次分片上传的唯一标识。</summary>
        public string upload_id { get; set; }
    }

    /// <summary>短剧分片上传结果。</summary>
    public class MiniDramaUploadPartJsonResult : WxJsonResult
    {
        /// <summary>根据分片内容生成的 ETag。</summary>
        public string etag { get; set; }
    }

    /// <summary>短剧媒资列表结果。</summary>
    public class MiniDramaListMediaJsonResult : WxJsonResult
    {
        /// <summary>媒资信息列表。</summary>
        public IList<MiniDramaMediaInfo> media_info_list { get; set; }
    }

    /// <summary>短剧媒资详情结果。</summary>
    public class MiniDramaGetMediaJsonResult : WxJsonResult
    {
        /// <summary>媒资信息。</summary>
        public MiniDramaMediaInfo media_info { get; set; }
    }

    /// <summary>短剧媒资播放链接结果。</summary>
    public class MiniDramaGetMediaLinkJsonResult : WxJsonResult
    {
        /// <summary>媒资播放信息。</summary>
        public MiniDramaMediaPlayInfo media_info { get; set; }
    }
}
