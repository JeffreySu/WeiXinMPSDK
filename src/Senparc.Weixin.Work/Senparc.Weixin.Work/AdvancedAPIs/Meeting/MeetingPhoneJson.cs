/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingPhoneJson.cs
    文件功能描述：企业微信会议电话外呼与临时 OpenId 强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议电话外呼、状态查询和临时 OpenId 模型

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 会议电话外呼目标。
    /// </summary>
    public class MeetingPhoneCalloutTarget
    {
        /// <summary>获取或设置国家或地区代码。</summary>
        [JsonConverter(typeof(MeetingStringOrNumberJsonConverter))]
        public string area { get; set; }

        /// <summary>获取或设置电话号码。</summary>
        public string phone { get; set; }

        /// <summary>获取或设置分机号。</summary>
        public string extension_number { get; set; }
    }

    /// <summary>
    /// 电话外呼结果项。
    /// </summary>
    public class MeetingPhoneCalloutItem
    {
        /// <summary>获取或设置国家或地区代码。</summary>
        [JsonConverter(typeof(MeetingStringOrNumberJsonConverter))]
        public string area { get; set; }

        /// <summary>获取或设置电话号码。</summary>
        public string phone { get; set; }

        /// <summary>获取或设置分机号。</summary>
        public string extension_number { get; set; }

        /// <summary>获取或设置外呼状态。</summary>
        public string status { get; set; }
    }

    /// <summary>
    /// 会议电话外呼请求。
    /// </summary>
    public class CalloutMeetingPhonesRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要外呼的电话号码列表。</summary>
        public IList<MeetingPhoneCalloutTarget> phone_numbers { get; set; }
    }

    /// <summary>
    /// 会议电话外呼结果。
    /// </summary>
    public class CalloutMeetingPhonesResult : WorkJsonResult
    {
        /// <summary>获取或设置成功提交的外呼号码列表。</summary>
        public IList<MeetingPhoneCalloutItem> phone_numbers { get; set; }

        /// <summary>获取或设置不合法的外呼号码列表。</summary>
        public IList<MeetingPhoneCalloutItem> invalid_phone_numbers { get; set; }
    }

    /// <summary>
    /// 获取会议电话外呼状态请求。
    /// </summary>
    public class GetMeetingPhoneCalloutStatusRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置分页游标。</summary>
        public string cursor { get; set; }

        /// <summary>获取或设置每页数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>
    /// 会议电话外呼状态项。
    /// </summary>
    public class MeetingPhoneCalloutStatusItem
    {
        /// <summary>获取或设置国家或地区代码。</summary>
        [JsonConverter(typeof(MeetingStringOrNumberJsonConverter))]
        public string area { get; set; }

        /// <summary>获取或设置电话号码。</summary>
        public string phone { get; set; }

        /// <summary>获取或设置分机号。</summary>
        public string extension_number { get; set; }

        /// <summary>获取或设置外呼状态。</summary>
        public string status { get; set; }

        /// <summary>获取或设置会议成员临时 OpenId。</summary>
        public string tmp_openid { get; set; }
    }

    /// <summary>
    /// 获取会议电话外呼状态结果。
    /// </summary>
    public class GetMeetingPhoneCalloutStatusResult : WorkJsonResult
    {
        /// <summary>获取或设置电话外呼状态列表。</summary>
        public IList<MeetingPhoneCalloutStatusItem> phone_numbers { get; set; }

        /// <summary>获取或设置是否还有更多数据。</summary>
        public bool has_more { get; set; }

        /// <summary>获取或设置下一页游标。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// 根据电话号码获取会议成员临时 OpenId 请求。
    /// </summary>
    public class GetMeetingPhoneTempOpenIdsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要查询的电话号码列表。</summary>
        public IList<MeetingPhoneCalloutTarget> phone_numbers { get; set; }
    }

    /// <summary>
    /// 电话号码与会议成员临时 OpenId 的对应关系。
    /// </summary>
    public class MeetingPhoneTempOpenIdItem
    {
        /// <summary>获取或设置国家或地区代码。</summary>
        [JsonConverter(typeof(MeetingStringOrNumberJsonConverter))]
        public string area { get; set; }

        /// <summary>获取或设置电话号码。</summary>
        public string phone { get; set; }

        /// <summary>获取或设置分机号。</summary>
        public string extension_number { get; set; }

        /// <summary>获取或设置会议成员临时 OpenId。</summary>
        public string tmp_openid { get; set; }
    }

    /// <summary>
    /// 根据电话号码获取会议成员临时 OpenId 结果。
    /// </summary>
    public class GetMeetingPhoneTempOpenIdsResult : WorkJsonResult
    {
        /// <summary>获取或设置电话号码与临时 OpenId 对应关系列表。</summary>
        public IList<MeetingPhoneTempOpenIdItem> tmp_openid_list { get; set; }
    }

    internal sealed class MeetingStringOrNumberJsonConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString();
            }

            if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out var integer))
            {
                return integer.ToString(CultureInfo.InvariantCulture);
            }

            throw new JsonException("Expected a string or integer JSON value.");
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
            => writer.WriteStringValue(value);
    }
}
