/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingRecordCurrentJson.cs
    文件功能描述：企业微信会议录制文件地址与转写接口强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐录制文件、下载地址和转写请求结果模型

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 获取单个会议录制文件详情请求。
    /// </summary>
    public class GetMeetingRecordFileRequest
    {
        /// <summary>获取或设置会议录制文件 ID。</summary>
        public string record_file_id { get; set; }

        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }
    }

    /// <summary>
    /// 会议纪要或智能转写下载文件。
    /// </summary>
    public class MeetingRecordDownloadFile
    {
        /// <summary>获取或设置下载地址；官方说明的有效期通常为一小时。</summary>
        public string download_address { get; set; }

        /// <summary>获取或设置下载文件类型，例如 txt、pdf 或 docx。</summary>
        public string file_type { get; set; }
    }

    /// <summary>
    /// 获取单个会议录制文件详情结果。
    /// </summary>
    public class GetMeetingRecordFileResult : WorkJsonResult
    {
        /// <summary>获取或设置会议录制文件 ID。</summary>
        public string record_file_id { get; set; }

        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议入会码。</summary>
        public string meeting_code { get; set; }

        /// <summary>获取或设置录制文件播放地址。</summary>
        public string view_address { get; set; }

        /// <summary>获取或设置视频下载地址；官方说明的默认有效期为六小时。</summary>
        public string download_address { get; set; }

        /// <summary>获取或设置视频文件类型，例如 mp4。</summary>
        public string download_address_file_type { get; set; }

        /// <summary>获取或设置音频下载地址；官方说明的默认有效期为六小时。</summary>
        public string audio_address { get; set; }

        /// <summary>获取或设置音频文件类型，例如 m4a。</summary>
        public string audio_address_file_type { get; set; }

        /// <summary>
        /// 获取或设置会议纪要下载文件。官方参数表定义为数组，但示例也可能返回单个对象，
        /// SDK 同时兼容两种 JSON 形状。
        /// </summary>
        [JsonConverter(typeof(MeetingRecordDownloadFileListConverter))]
        public IList<MeetingRecordDownloadFile> meeting_summary { get; set; }

        /// <summary>获取或设置智能优化版会议录制转写下载文件列表。</summary>
        public IList<MeetingRecordDownloadFile> ai_meeting_transcripts { get; set; }

        /// <summary>获取或设置录制文件名。</summary>
        public string record_name { get; set; }

        /// <summary>获取或设置录制开始时间戳，单位为毫秒。</summary>
        public long start_time { get; set; }

        /// <summary>获取或设置录制结束时间戳，单位为毫秒。</summary>
        public long end_time { get; set; }

        /// <summary>获取或设置会议录制名称。</summary>
        public string meeting_record_name { get; set; }
    }

    /// <summary>
    /// 获取会议录制文件地址列表请求。
    /// </summary>
    public class GetMeetingRecordFileListRequest
    {
        /// <summary>获取或设置会议录制 ID。</summary>
        public string meeting_record_id { get; set; }

        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }
    }

    /// <summary>
    /// 会议录制文件播放、视频、音频和纪要地址。
    /// </summary>
    public class MeetingRecordPlaybackFile
    {
        /// <summary>获取或设置会议录制文件 ID。</summary>
        public string record_file_id { get; set; }

        /// <summary>获取或设置录制文件播放地址。</summary>
        public string view_address { get; set; }

        /// <summary>获取或设置视频下载地址；官方说明的默认有效期为六小时。</summary>
        public string download_address { get; set; }

        /// <summary>获取或设置视频文件类型，例如 mp4。</summary>
        public string download_address_file_type { get; set; }

        /// <summary>获取或设置音频下载地址；官方说明的默认有效期为六小时。</summary>
        public string audio_address { get; set; }

        /// <summary>获取或设置音频文件类型，例如 m4a。</summary>
        public string audio_address_file_type { get; set; }

        /// <summary>获取或设置会议纪要下载文件列表。</summary>
        [JsonConverter(typeof(MeetingRecordDownloadFileListConverter))]
        public IList<MeetingRecordDownloadFile> meeting_summary { get; set; }
    }

    /// <summary>
    /// 获取会议录制文件地址列表结果。
    /// </summary>
    public class GetMeetingRecordFileListResult : WorkJsonResult
    {
        /// <summary>获取或设置会议录制 ID。</summary>
        public string meeting_record_id { get; set; }

        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议入会码。</summary>
        public string meeting_code { get; set; }

        /// <summary>获取或设置会议主题。</summary>
        public string title { get; set; }

        /// <summary>获取或设置会议录制文件地址列表。</summary>
        public IList<MeetingRecordPlaybackFile> record_files { get; set; }
    }

    /// <summary>
    /// 获取会议录制转写段落列表请求。
    /// </summary>
    public class GetMeetingRecordTranscriptParagraphListRequest
    {
        /// <summary>获取或设置会议录制文件 ID。</summary>
        public string record_file_id { get; set; }

        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }
    }

    /// <summary>
    /// 会议录制转写段落摘要。
    /// </summary>
    public class MeetingRecordTranscriptParagraphSummary
    {
        /// <summary>获取或设置段落 ID。</summary>
        public string pid { get; set; }

        /// <summary>获取或设置段落在录制文件中的开始时间，单位为毫秒。</summary>
        public long start_time { get; set; }

        /// <summary>获取或设置段落在录制文件中的结束时间，单位为毫秒。</summary>
        public long end_time { get; set; }
    }

    /// <summary>
    /// 获取会议录制转写段落列表结果。
    /// </summary>
    public class GetMeetingRecordTranscriptParagraphListResult : WorkJsonResult
    {
        /// <summary>获取或设置声纹识别状态：0 未完成，1 已完成或无需识别。</summary>
        public int audio_detect { get; set; }

        /// <summary>获取或设置录制转写段落列表。</summary>
        public IList<MeetingRecordTranscriptParagraphSummary> paragraphs { get; set; }
    }

    /// <summary>
    /// 获取会议录制转写详情请求。
    /// </summary>
    public class GetMeetingRecordTranscriptDetailRequest
    {
        /// <summary>获取或设置会议录制文件 ID。</summary>
        public string record_file_id { get; set; }

        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置查询起始段落 ID；接口包含该段落，未填写时从头开始。</summary>
        public string pid { get; set; }

        /// <summary>获取或设置查询段落数；未填写时查询全量数据。</summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 会议录制转写发言人信息。
    /// </summary>
    public class MeetingRecordTranscriptSpeaker
    {
        /// <summary>获取或设置同企业发言人的成员 ID；非同企业发言人不返回。</summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 会议录制转写词信息。
    /// </summary>
    public class MeetingRecordTranscriptWord
    {
        /// <summary>获取或设置词 ID。</summary>
        public string wid { get; set; }

        /// <summary>获取或设置词在录制文件中的开始时间，单位为毫秒。</summary>
        public long start_time { get; set; }

        /// <summary>获取或设置词在录制文件中的结束时间，单位为毫秒。</summary>
        public long end_time { get; set; }

        /// <summary>获取或设置转写文本。</summary>
        public string text { get; set; }
    }

    /// <summary>
    /// 会议录制转写句子信息。
    /// </summary>
    public class MeetingRecordTranscriptSentence
    {
        /// <summary>获取或设置句子 ID。</summary>
        public string sid { get; set; }

        /// <summary>获取或设置句子在录制文件中的开始时间，单位为毫秒。</summary>
        public long start_time { get; set; }

        /// <summary>获取或设置句子在录制文件中的结束时间，单位为毫秒。</summary>
        public long end_time { get; set; }

        /// <summary>获取或设置句子中的词列表。</summary>
        public IList<MeetingRecordTranscriptWord> words { get; set; }
    }

    /// <summary>
    /// 会议录制转写段落详情。
    /// </summary>
    public class MeetingRecordTranscriptParagraph
    {
        /// <summary>获取或设置段落 ID。</summary>
        public string pid { get; set; }

        /// <summary>获取或设置段落在录制文件中的开始时间，单位为毫秒。</summary>
        public long start_time { get; set; }

        /// <summary>获取或设置段落在录制文件中的结束时间，单位为毫秒。</summary>
        public long end_time { get; set; }

        /// <summary>获取或设置发言人信息。</summary>
        public MeetingRecordTranscriptSpeaker speaker_info { get; set; }

        /// <summary>获取或设置段落中的句子列表。</summary>
        public IList<MeetingRecordTranscriptSentence> sentences { get; set; }
    }

    /// <summary>
    /// 会议录制转写详情内容。
    /// </summary>
    public class MeetingRecordTranscriptDetail
    {
        /// <summary>获取或设置转写段落列表。</summary>
        public IList<MeetingRecordTranscriptParagraph> paragraphs { get; set; }

        /// <summary>获取或设置转写关键词列表。</summary>
        public IList<string> keywords { get; set; }

        /// <summary>获取或设置声纹识别状态：0 未完成，1 已完成或无需识别。</summary>
        public int audio_detect { get; set; }
    }

    /// <summary>
    /// 获取会议录制转写详情结果。
    /// </summary>
    public class GetMeetingRecordTranscriptDetailResult : WorkJsonResult
    {
        /// <summary>获取或设置是否仍有更多段落。</summary>
        public bool has_more { get; set; }

        /// <summary>获取或设置录制转写详情。</summary>
        public MeetingRecordTranscriptDetail transcripts { get; set; }
    }

    /// <summary>
    /// 搜索会议录制转写请求。
    /// </summary>
    public class SearchMeetingRecordTranscriptRequest
    {
        /// <summary>获取或设置会议录制文件 ID。</summary>
        public string record_file_id { get; set; }

        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要搜索的文本。</summary>
        public string text { get; set; }
    }

    /// <summary>
    /// 会议录制转写搜索命中位置。
    /// </summary>
    public class MeetingRecordTranscriptSearchHit
    {
        /// <summary>获取或设置命中内容所在的段落 ID。</summary>
        public string pid { get; set; }

        /// <summary>获取或设置命中内容所在的句子 ID。</summary>
        public string sid { get; set; }

        /// <summary>获取或设置搜索文本相对词的偏移。</summary>
        public int offset { get; set; }

        /// <summary>获取或设置匹配文本长度。</summary>
        public int length { get; set; }
    }

    /// <summary>
    /// 会议录制转写搜索时间轴位置。
    /// </summary>
    public class MeetingRecordTranscriptTimeline
    {
        /// <summary>获取或设置搜索结果所在的段落 ID。</summary>
        public string pid { get; set; }

        /// <summary>获取或设置搜索结果所在的句子 ID。</summary>
        public string sid { get; set; }

        /// <summary>获取或设置搜索结果在录制文件中的开始时间，单位为毫秒。</summary>
        public long start_time { get; set; }
    }

    /// <summary>
    /// 搜索会议录制转写结果。
    /// </summary>
    public class SearchMeetingRecordTranscriptResult : WorkJsonResult
    {
        /// <summary>获取或设置搜索命中位置列表。</summary>
        public IList<MeetingRecordTranscriptSearchHit> hits { get; set; }

        /// <summary>获取或设置用于时间轴预览的搜索结果位置列表。</summary>
        public IList<MeetingRecordTranscriptTimeline> timelines { get; set; }
    }

    internal sealed class MeetingRecordDownloadFileListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
            => typeof(IList<MeetingRecordDownloadFile>).IsAssignableFrom(objectType);

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            if (token.Type == JTokenType.Null || token.Type == JTokenType.Undefined)
            {
                return null;
            }

            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<MeetingRecordDownloadFile>>(serializer);
            }

            return new List<MeetingRecordDownloadFile>
            {
                token.ToObject<MeetingRecordDownloadFile>(serializer)
            };
        }

        public override void WriteJson(JsonWriter writer, object value,
            JsonSerializer serializer)
            => serializer.Serialize(writer, value);
    }
}
