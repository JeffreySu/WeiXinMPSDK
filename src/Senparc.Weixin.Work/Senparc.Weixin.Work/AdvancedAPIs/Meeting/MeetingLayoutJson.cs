/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MeetingLayoutJson.cs
    文件功能描述：企业微信会议布局、高级布局和背景图管理请求及结果模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐会议布局、高级布局和背景图管理强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Meeting
{
    /// <summary>
    /// 企业微信会议布局模板。
    /// </summary>
    public class MeetingLayoutTemplate
    {
        /// <summary>获取或设置布局模板 ID。</summary>
        public string layout_template_id { get; set; }

        /// <summary>获取或设置布局模板缩略图地址。</summary>
        public string thumbnail_url { get; set; }

        /// <summary>获取或设置布局模板大图地址。</summary>
        public string picture_url { get; set; }

        /// <summary>获取或设置 JSON 格式的布局渲染规则。</summary>
        public string render_rule { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议布局模板结果。
    /// </summary>
    public class GetMeetingLayoutTemplatesResult : WorkJsonResult
    {
        /// <summary>获取或设置布局模板列表。</summary>
        public IList<MeetingLayoutTemplate> layout_template_list { get; set; }
    }

    /// <summary>
    /// 企业微信会议基础布局中的成员座次。
    /// </summary>
    public class MeetingLayoutSeat
    {
        /// <summary>获取或设置布局宫格 ID。</summary>
        public string grid_id { get; set; }

        /// <summary>获取或设置布局宫格类型。</summary>
        public int grid_type { get; set; }

        /// <summary>获取或设置企业成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>获取或设置参会成员临时 OpenId。</summary>
        public string tmp_openid { get; set; }

        /// <summary>获取或设置参会成员昵称。</summary>
        public string nick_name { get; set; }

        /// <summary>获取或设置扩展应用 ID。</summary>
        public string tool_sdkid { get; set; }
    }

    /// <summary>
    /// 企业微信会议基础布局页面。
    /// </summary>
    public class MeetingLayoutPage
    {
        /// <summary>获取或设置布局模板 ID。</summary>
        public string layout_template_id { get; set; }

        /// <summary>获取或设置页面中的成员座次列表。</summary>
        public IList<MeetingLayoutSeat> user_seat_list { get; set; }
    }

    /// <summary>
    /// 新增企业微信会议基础布局时使用的布局定义。
    /// </summary>
    public class MeetingLayoutDefinition
    {
        /// <summary>获取或设置布局页面列表。</summary>
        public IList<MeetingLayoutPage> page_list { get; set; }
    }

    /// <summary>
    /// 企业微信会议基础布局信息。
    /// </summary>
    public class MeetingLayoutInfo
    {
        /// <summary>获取或设置布局 ID。</summary>
        public string layout_id { get; set; }

        /// <summary>获取或设置布局页面列表。</summary>
        public IList<MeetingLayoutPage> page_list { get; set; }
    }

    /// <summary>
    /// 添加企业微信会议基础布局请求。
    /// </summary>
    public class AddMeetingLayoutsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置待添加的布局列表。</summary>
        public IList<MeetingLayoutDefinition> layout_list { get; set; }

        /// <summary>获取或设置新增布局中需要设为默认布局的序号。</summary>
        public int? default_layout_order { get; set; }
    }

    /// <summary>
    /// 添加企业微信会议基础布局结果。
    /// </summary>
    public class AddMeetingLayoutsResult : WorkJsonResult
    {
        /// <summary>获取或设置会议当前使用的布局 ID。</summary>
        public string selected_layout_id { get; set; }

        /// <summary>获取或设置新增后的布局列表。</summary>
        public IList<MeetingLayoutInfo> layout_list { get; set; }
    }

    /// <summary>
    /// 更新企业微信会议基础布局请求。
    /// </summary>
    public class UpdateMeetingLayoutRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要更新的布局 ID。</summary>
        public string layout_id { get; set; }

        /// <summary>获取或设置更新后的布局页面列表。</summary>
        public IList<MeetingLayoutPage> page_list { get; set; }

        /// <summary>获取或设置是否将此布局设为会议默认布局。</summary>
        public bool? enable_set_default { get; set; }
    }

    /// <summary>
    /// 更新企业微信会议基础布局结果。
    /// </summary>
    public class UpdateMeetingLayoutResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 设置企业微信会议默认基础布局请求。
    /// </summary>
    public class SetDefaultMeetingLayoutRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议需要使用的布局 ID。</summary>
        public string selected_layout_id { get; set; }
    }

    /// <summary>
    /// 设置企业微信会议默认基础布局结果。
    /// </summary>
    public class SetDefaultMeetingLayoutResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 企业微信会议高级布局座次中的成员。
    /// </summary>
    public class MeetingAdvancedLayoutUser
    {
        /// <summary>获取或设置企业成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>获取或设置参会成员临时 OpenId。</summary>
        public string tmp_openid { get; set; }

        /// <summary>获取或设置参会成员昵称。</summary>
        public string nick_name { get; set; }
    }

    /// <summary>
    /// 企业微信会议高级布局中的成员座次。
    /// </summary>
    public class MeetingAdvancedLayoutSeat
    {
        /// <summary>获取或设置布局宫格 ID。</summary>
        public string grid_id { get; set; }

        /// <summary>获取或设置布局宫格类型。</summary>
        public int grid_type { get; set; }

        /// <summary>获取或设置视频画面来源类型。</summary>
        public int? video_type { get; set; }

        /// <summary>获取或设置座次中的成员列表。</summary>
        public IList<MeetingAdvancedLayoutUser> user_list { get; set; }
    }

    /// <summary>
    /// 企业微信会议高级布局轮询设置。
    /// </summary>
    public class MeetingAdvancedLayoutPollingSetting
    {
        /// <summary>获取或设置轮询间隔时间单位类型。</summary>
        public int polling_interval_unit { get; set; }

        /// <summary>获取或设置轮询间隔时间。</summary>
        public int polling_interval { get; set; }

        /// <summary>获取或设置是否忽略未开启视频的成员。</summary>
        public bool ignore_user_novideo { get; set; }

        /// <summary>获取或设置是否忽略未入会成员。</summary>
        public bool ignore_user_absence { get; set; }
    }

    /// <summary>
    /// 企业微信会议高级布局页面。
    /// </summary>
    public class MeetingAdvancedLayoutPage
    {
        /// <summary>获取或设置布局模板 ID。</summary>
        public string layout_template_id { get; set; }

        /// <summary>获取或设置是否开启成员轮询。</summary>
        public bool? enable_polling { get; set; }

        /// <summary>获取或设置成员轮询配置。</summary>
        public MeetingAdvancedLayoutPollingSetting polling_setting { get; set; }

        /// <summary>获取或设置页面中的成员座次列表。</summary>
        public IList<MeetingAdvancedLayoutSeat> user_seat_list { get; set; }
    }

    /// <summary>
    /// 新增企业微信会议高级布局时使用的布局定义。
    /// </summary>
    public class MeetingAdvancedLayoutDefinition
    {
        /// <summary>获取或设置布局名称。</summary>
        public string layout_name { get; set; }

        /// <summary>获取或设置布局页面列表。</summary>
        public IList<MeetingAdvancedLayoutPage> page_list { get; set; }
    }

    /// <summary>
    /// 企业微信会议高级布局信息。
    /// </summary>
    public class MeetingAdvancedLayoutInfo
    {
        /// <summary>获取或设置布局 ID。</summary>
        public string layout_id { get; set; }

        /// <summary>获取或设置布局名称。</summary>
        public string layout_name { get; set; }

        /// <summary>获取或设置布局页面列表。</summary>
        public IList<MeetingAdvancedLayoutPage> page_list { get; set; }
    }

    /// <summary>
    /// 添加企业微信会议高级布局请求。
    /// </summary>
    public class AddMeetingAdvancedLayoutsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置待添加的高级布局列表。</summary>
        public IList<MeetingAdvancedLayoutDefinition> layout_list { get; set; }
    }

    /// <summary>
    /// 添加企业微信会议高级布局结果。
    /// </summary>
    public class AddMeetingAdvancedLayoutsResult : WorkJsonResult
    {
        /// <summary>获取或设置新增后的高级布局列表。</summary>
        public IList<MeetingAdvancedLayoutInfo> layout_list { get; set; }
    }

    /// <summary>
    /// 更新企业微信会议高级布局请求。
    /// </summary>
    public class UpdateMeetingAdvancedLayoutRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要更新的布局 ID。</summary>
        public string layout_id { get; set; }

        /// <summary>获取或设置更新后的布局名称。</summary>
        public string layout_name { get; set; }

        /// <summary>获取或设置更新后的布局页面列表。</summary>
        public IList<MeetingAdvancedLayoutPage> page_list { get; set; }
    }

    /// <summary>
    /// 更新企业微信会议高级布局结果。
    /// </summary>
    public class UpdateMeetingAdvancedLayoutResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 应用企业微信会议高级布局的目标成员。
    /// </summary>
    public class MeetingAdvancedLayoutApplyUser
    {
        /// <summary>获取或设置目标成员的临时 OpenId。</summary>
        public string tmp_openid { get; set; }
    }

    /// <summary>
    /// 向指定成员应用企业微信会议高级布局请求。
    /// </summary>
    public class ApplyMeetingAdvancedLayoutRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要应用的高级布局 ID。</summary>
        public string layout_id { get; set; }

        /// <summary>获取或设置需要应用该布局的成员列表。</summary>
        public IList<MeetingAdvancedLayoutApplyUser> user_list { get; set; }
    }

    /// <summary>
    /// 向指定成员应用企业微信会议高级布局结果。
    /// </summary>
    public class ApplyMeetingAdvancedLayoutResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 获取企业微信会议高级布局列表请求。
    /// </summary>
    public class GetMeetingAdvancedLayoutsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议高级布局列表结果。
    /// </summary>
    public class GetMeetingAdvancedLayoutsResult : WorkJsonResult
    {
        /// <summary>获取或设置会议当前使用的布局 ID。</summary>
        public string selected_layout_id { get; set; }

        /// <summary>获取或设置高级布局列表。</summary>
        public IList<MeetingAdvancedLayoutInfo> layout_list { get; set; }
    }

    /// <summary>
    /// 获取指定成员终端当前会议布局请求。
    /// </summary>
    public class GetMeetingUserLayoutRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置成员临时 OpenId。</summary>
        public string tmp_openid { get; set; }

        /// <summary>获取或设置终端设备类型。</summary>
        public int instance_id { get; set; }
    }

    /// <summary>
    /// 获取指定成员终端当前会议布局结果。
    /// </summary>
    public class GetMeetingUserLayoutResult : WorkJsonResult
    {
        /// <summary>获取或设置成员终端当前使用的布局 ID。</summary>
        public string selected_layout_id { get; set; }

        /// <summary>获取或设置布局名称。</summary>
        public string layout_name { get; set; }

        /// <summary>获取或设置布局类型。</summary>
        public int layout_type { get; set; }

        /// <summary>获取或设置布局页面列表。</summary>
        public IList<MeetingAdvancedLayoutPage> page_list { get; set; }
    }

    /// <summary>
    /// 批量删除企业微信会议高级布局请求。
    /// </summary>
    public class DeleteMeetingAdvancedLayoutsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要删除的布局 ID 列表。</summary>
        public IList<string> layout_id_list { get; set; }
    }

    /// <summary>
    /// 批量删除企业微信会议高级布局结果。
    /// </summary>
    public class DeleteMeetingAdvancedLayoutsResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 待添加的企业微信会议布局背景图片。
    /// </summary>
    public class MeetingLayoutBackgroundImageRequest
    {
        /// <summary>获取或设置背景图片地址。</summary>
        public string image_url { get; set; }

        /// <summary>获取或设置背景图片 MD5 值。</summary>
        public string image_md5 { get; set; }
    }

    /// <summary>
    /// 企业微信会议布局背景图信息。
    /// </summary>
    public class MeetingLayoutBackgroundInfo
    {
        /// <summary>获取或设置背景图 ID。</summary>
        public string background_id { get; set; }

        /// <summary>获取或设置背景图片 MD5 值。</summary>
        public string image_md5 { get; set; }
    }

    /// <summary>
    /// 添加企业微信会议布局背景图请求。
    /// </summary>
    public class AddMeetingLayoutBackgroundsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置待添加的背景图片列表。</summary>
        public IList<MeetingLayoutBackgroundImageRequest> image_list { get; set; }

        /// <summary>获取或设置新增图片中需要设为默认背景图的序号。</summary>
        public int? default_image_order { get; set; }
    }

    /// <summary>
    /// 添加企业微信会议布局背景图结果。
    /// </summary>
    public class AddMeetingLayoutBackgroundsResult : WorkJsonResult
    {
        /// <summary>获取或设置会议当前使用的背景图 ID。</summary>
        public string selected_background_id { get; set; }

        /// <summary>获取或设置新增后的背景图列表。</summary>
        public IList<MeetingLayoutBackgroundInfo> background_list { get; set; }
    }

    /// <summary>
    /// 设置企业微信会议默认布局背景图请求。
    /// </summary>
    public class SetDefaultMeetingLayoutBackgroundRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置会议需要使用的背景图 ID。</summary>
        public string selected_background_id { get; set; }
    }

    /// <summary>
    /// 设置企业微信会议默认布局背景图结果。
    /// </summary>
    public class SetDefaultMeetingLayoutBackgroundResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 获取企业微信会议布局背景图列表请求。
    /// </summary>
    public class GetMeetingLayoutBackgroundsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }
    }

    /// <summary>
    /// 获取企业微信会议布局背景图列表结果。
    /// </summary>
    public class GetMeetingLayoutBackgroundsResult : WorkJsonResult
    {
        /// <summary>获取或设置会议当前使用的背景图 ID。</summary>
        public string selected_background_id { get; set; }

        /// <summary>获取或设置背景图列表。</summary>
        public IList<MeetingLayoutBackgroundInfo> background_list { get; set; }
    }

    /// <summary>
    /// 删除企业微信会议布局背景图请求。
    /// </summary>
    public class DeleteMeetingLayoutBackgroundRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要删除的背景图 ID。</summary>
        public string background_id { get; set; }
    }

    /// <summary>
    /// 删除企业微信会议布局背景图结果。
    /// </summary>
    public class DeleteMeetingLayoutBackgroundResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 批量删除企业微信会议布局背景图请求。
    /// </summary>
    public class DeleteMeetingLayoutBackgroundsRequest
    {
        /// <summary>获取或设置会议 ID。</summary>
        public string meetingid { get; set; }

        /// <summary>获取或设置需要删除的背景图 ID 列表。</summary>
        public IList<string> background_id_list { get; set; }
    }

    /// <summary>
    /// 批量删除企业微信会议布局背景图结果。
    /// </summary>
    public class DeleteMeetingLayoutBackgroundsResult : WorkJsonResult
    {
    }
}
