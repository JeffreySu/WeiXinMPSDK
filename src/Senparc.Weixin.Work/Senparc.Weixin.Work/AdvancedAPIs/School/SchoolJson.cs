/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SchoolJson.cs
    文件功能描述：企业微信家校沟通基础与学校部门强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增家校沟通基础与学校部门强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.School
{
    /// <summary>“学校通知”二维码结果。</summary>
    public class SchoolSubscribeQrCodeResult : WorkJsonResult
    {
        /// <summary>大尺寸二维码 URL。</summary>
        public string qrcode_big { get; set; }

        /// <summary>中尺寸二维码 URL。</summary>
        public string qrcode_middle { get; set; }

        /// <summary>小尺寸二维码 URL。</summary>
        public string qrcode_thumb { get; set; }
    }

    /// <summary>设置家长关注“学校通知”的模式。</summary>
    public class SchoolSubscribeModeRequest
    {
        /// <summary>关注模式：0 表示可扫码填写资料加入，1 表示禁止扫码填写资料加入。</summary>
        public int subscribe_mode { get; set; }
    }

    /// <summary>家长关注“学校通知”的模式。</summary>
    public class SchoolSubscribeModeResult : WorkJsonResult
    {
        /// <summary>关注模式。</summary>
        public int subscribe_mode { get; set; }
    }

    /// <summary>外部联系人 ID 转微信 OpenId 请求。</summary>
    public class SchoolConvertToOpenIdRequest
    {
        /// <summary>微信外部联系人的 external_userid。</summary>
        public string external_userid { get; set; }
    }

    /// <summary>外部联系人 ID 转微信 OpenId 结果。</summary>
    public class SchoolConvertToOpenIdResult : WorkJsonResult
    {
        /// <summary>转换后的微信 OpenId。</summary>
        public string openid { get; set; }
    }

    /// <summary>发送“学校通知”请求。</summary>
    public class SchoolNotificationRequest
    {
        /// <summary>接收消息的外部联系人 ID 列表。</summary>
        public IList<string> to_external_user { get; set; }

        /// <summary>接收消息的家长 ID 列表。</summary>
        public IList<string> to_parent_userid { get; set; }

        /// <summary>接收消息的学生 ID 列表。</summary>
        public IList<string> to_student_userid { get; set; }

        /// <summary>接收消息的学校部门 ID 列表。</summary>
        public IList<long> to_party { get; set; }

        /// <summary>是否发送给学校全部家长，使用协议定义的 0/1。</summary>
        public int? toall { get; set; }

        /// <summary>消息类型。</summary>
        public string msgtype { get; set; }

        /// <summary>文本消息。</summary>
        public SchoolTextMessage text { get; set; }

        /// <summary>图片消息。</summary>
        public SchoolMediaMessage image { get; set; }

        /// <summary>语音消息。</summary>
        public SchoolMediaMessage voice { get; set; }

        /// <summary>视频消息。</summary>
        public SchoolVideoMessage video { get; set; }

        /// <summary>文件消息。</summary>
        public SchoolMediaMessage file { get; set; }

        /// <summary>文本卡片消息。</summary>
        public SchoolTextCardMessage textcard { get; set; }

        /// <summary>图文消息。</summary>
        public SchoolNewsMessage news { get; set; }

        /// <summary>图文消息（mpnews）。</summary>
        public SchoolMpNewsMessage mpnews { get; set; }

        /// <summary>小程序消息。</summary>
        public SchoolMiniProgramMessage miniprogram { get; set; }

        /// <summary>企业应用 ID。</summary>
        public int? agentid { get; set; }

        /// <summary>是否开启 ID 转译，使用协议定义的 0/1。</summary>
        public int? enable_id_trans { get; set; }

        /// <summary>是否开启重复消息检查，使用协议定义的 0/1。</summary>
        public int? enable_duplicate_check { get; set; }

        /// <summary>重复消息检查间隔，单位为秒。</summary>
        public int? duplicate_check_interval { get; set; }
    }

    /// <summary>文本消息内容。</summary>
    public class SchoolTextMessage
    {
        /// <summary>文本内容。</summary>
        public string content { get; set; }
    }

    /// <summary>图片、语音或文件的素材内容。</summary>
    public class SchoolMediaMessage
    {
        /// <summary>素材 MediaId。</summary>
        public string media_id { get; set; }
    }

    /// <summary>视频消息内容。</summary>
    public class SchoolVideoMessage : SchoolMediaMessage
    {
        /// <summary>视频标题。</summary>
        public string title { get; set; }

        /// <summary>视频描述。</summary>
        public string description { get; set; }
    }

    /// <summary>文本卡片消息内容。</summary>
    public class SchoolTextCardMessage
    {
        /// <summary>标题。</summary>
        public string title { get; set; }

        /// <summary>描述。</summary>
        public string description { get; set; }

        /// <summary>点击后跳转的 URL。</summary>
        public string url { get; set; }

        /// <summary>按钮文字。</summary>
        public string btntxt { get; set; }
    }

    /// <summary>图文消息。</summary>
    public class SchoolNewsMessage
    {
        /// <summary>图文条目。</summary>
        public IList<SchoolNewsArticle> articles { get; set; }
    }

    /// <summary>图文消息条目。</summary>
    public class SchoolNewsArticle
    {
        /// <summary>标题。</summary>
        public string title { get; set; }

        /// <summary>描述。</summary>
        public string description { get; set; }

        /// <summary>点击后跳转的 URL。</summary>
        public string url { get; set; }

        /// <summary>封面图片 URL。</summary>
        public string picurl { get; set; }

        /// <summary>按钮文字。</summary>
        public string btntxt { get; set; }
    }

    /// <summary>mpnews 图文消息。</summary>
    public class SchoolMpNewsMessage
    {
        /// <summary>图文条目。</summary>
        public IList<SchoolMpNewsArticle> articles { get; set; }
    }

    /// <summary>mpnews 图文消息条目。</summary>
    public class SchoolMpNewsArticle
    {
        /// <summary>标题。</summary>
        public string title { get; set; }

        /// <summary>缩略图素材 MediaId。</summary>
        public string thumb_media_id { get; set; }

        /// <summary>作者。</summary>
        public string author { get; set; }

        /// <summary>原文链接。</summary>
        public string content_source_url { get; set; }

        /// <summary>正文。</summary>
        public string content { get; set; }

        /// <summary>摘要。</summary>
        public string digest { get; set; }
    }

    /// <summary>小程序消息内容。</summary>
    public class SchoolMiniProgramMessage
    {
        /// <summary>小程序 AppId。</summary>
        public string appid { get; set; }

        /// <summary>小程序页面路径。</summary>
        public string pagepath { get; set; }

        /// <summary>小程序标题。</summary>
        public string title { get; set; }

        /// <summary>封面素材 MediaId。</summary>
        public string thumb_media_id { get; set; }
    }

    /// <summary>发送“学校通知”结果。</summary>
    public class SchoolNotificationResult : WorkJsonResult
    {
        /// <summary>无效的外部联系人 ID。</summary>
        public IList<string> invalid_external_user { get; set; }

        /// <summary>无效的家长 ID。</summary>
        public IList<string> invalid_parent_userid { get; set; }

        /// <summary>无效的学生 ID。</summary>
        public IList<string> invalid_student_userid { get; set; }

        /// <summary>无效的学校部门 ID。</summary>
        public IList<long> invalid_party { get; set; }
    }

    /// <summary>学校部门管理员。</summary>
    public class SchoolDepartmentAdministrator
    {
        /// <summary>管理员成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>管理员类型。</summary>
        public int type { get; set; }

        /// <summary>教师科目。</summary>
        public string subject { get; set; }
    }

    /// <summary>创建学校部门请求。</summary>
    public class SchoolDepartmentCreateRequest
    {
        /// <summary>指定的部门 ID；不填时由企业微信生成。</summary>
        public long? id { get; set; }

        /// <summary>部门名称。</summary>
        public string name { get; set; }

        /// <summary>部门类型。</summary>
        public int type { get; set; }

        /// <summary>上级部门 ID。</summary>
        public long parentid { get; set; }

        /// <summary>标准年级。</summary>
        public int? standard_grade { get; set; }

        /// <summary>入学年份。</summary>
        public int? register_year { get; set; }

        /// <summary>部门排序值。</summary>
        public long? order { get; set; }

        /// <summary>部门管理员列表。</summary>
        public IList<SchoolDepartmentAdministrator> department_admins { get; set; }
    }

    /// <summary>创建学校部门结果。</summary>
    public class SchoolDepartmentCreateResult : WorkJsonResult
    {
        /// <summary>部门 ID。</summary>
        public long id { get; set; }
    }

    /// <summary>学校部门管理员更新操作。</summary>
    public class SchoolDepartmentAdministratorOperation
    {
        /// <summary>操作类型。</summary>
        public int op { get; set; }

        /// <summary>管理员成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>管理员类型。</summary>
        public int? type { get; set; }

        /// <summary>教师科目。</summary>
        public string subject { get; set; }
    }

    /// <summary>更新学校部门请求。</summary>
    public class SchoolDepartmentUpdateRequest
    {
        /// <summary>当前部门 ID。</summary>
        public long id { get; set; }

        /// <summary>新的部门 ID。</summary>
        public long? new_id { get; set; }

        /// <summary>部门名称。</summary>
        public string name { get; set; }

        /// <summary>上级部门 ID。</summary>
        public long? parentid { get; set; }

        /// <summary>标准年级。</summary>
        public int? standard_grade { get; set; }

        /// <summary>入学年份。</summary>
        public int? register_year { get; set; }

        /// <summary>部门排序值。</summary>
        public long? order { get; set; }

        /// <summary>部门管理员增删改操作列表。</summary>
        public IList<SchoolDepartmentAdministratorOperation> department_admins { get; set; }
    }

    /// <summary>学校部门信息。</summary>
    public class SchoolDepartment
    {
        /// <summary>部门 ID。</summary>
        public long id { get; set; }

        /// <summary>部门名称。</summary>
        public string name { get; set; }

        /// <summary>部门类型。</summary>
        public int type { get; set; }

        /// <summary>上级部门 ID。</summary>
        public long parentid { get; set; }

        /// <summary>标准年级。</summary>
        public int? standard_grade { get; set; }

        /// <summary>入学年份。</summary>
        public int? register_year { get; set; }

        /// <summary>部门排序值。</summary>
        public long? order { get; set; }

        /// <summary>部门管理员列表。</summary>
        public IList<SchoolDepartmentAdministrator> department_admins { get; set; }

        /// <summary>是否已毕业，使用协议定义的 0/1。</summary>
        public int? is_graduated { get; set; }

        /// <summary>是否开启班级群，使用协议定义的 0/1。</summary>
        public int? open_group_chat { get; set; }

        /// <summary>班级群 ID。</summary>
        public string group_chat_id { get; set; }
    }

    /// <summary>获取学校部门列表结果。</summary>
    public class SchoolDepartmentListResult : WorkJsonResult
    {
        /// <summary>学校部门列表。</summary>
        public IList<SchoolDepartment> departments { get; set; }
    }

    /// <summary>设置自动升年级请求。</summary>
    public class SchoolUpgradeInfoRequest
    {
        /// <summary>是否开启自动升年级，使用协议定义的 0/1。</summary>
        public int upgrade_switch { get; set; }

        /// <summary>自动升年级时间戳；协议只使用其中的月和日。</summary>
        public long? upgrade_time { get; set; }
    }
}
