/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDriveSpaceJson.cs
    文件功能描述：企业微信微盘空间接口强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐微盘空间管理、权限与安全设置模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive
{
    /// <summary>
    /// 微盘成员或部门授权信息。
    /// </summary>
    public class WeDriveAuthInfo
    {
        /// <summary>
        /// 授权对象类型：1 表示成员，2 表示部门。
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 成员 UserID；<see cref="type"/> 为 1 时填写。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 部门 ID；<see cref="type"/> 为 2 时填写。
        /// </summary>
        public long? departmentid { get; set; }

        /// <summary>
        /// 权限类型，例如 1 仅下载、2 可编辑、4 仅预览、5 可上传下载、7 管理员、200 自定义权限。
        /// </summary>
        public int? auth { get; set; }

        /// <summary>
        /// 自定义权限组合；<see cref="auth"/> 为 200 时填写。
        /// </summary>
        public WeDriveCustomizeAuth customize_auth { get; set; }
    }

    /// <summary>
    /// 微盘自定义权限组合。
    /// </summary>
    public class WeDriveCustomizeAuth
    {
        /// <summary>
        /// 是否允许上传文件。
        /// </summary>
        public bool? enable_operation_upload { get; set; }

        /// <summary>
        /// 是否允许删除文件。
        /// </summary>
        public bool? enable_operation_delete { get; set; }
    }

    /// <summary>
    /// 新建微盘空间请求。
    /// </summary>
    public class WeDriveCreateSpaceRequest
    {
        /// <summary>
        /// 空间标题。
        /// </summary>
        public string space_name { get; set; }

        /// <summary>
        /// 空间成员及其权限；不需要预置成员时可不传。
        /// </summary>
        public IList<WeDriveAuthInfo> auth_info { get; set; }

        /// <summary>
        /// 空间子类型；当前普通空间填写 0。
        /// </summary>
        public int? space_sub_type { get; set; }
    }

    /// <summary>
    /// 新建微盘空间结果。
    /// </summary>
    public class WeDriveCreateSpaceResult : WorkJsonResult
    {
        /// <summary>
        /// 新建空间的 ID。
        /// </summary>
        public string spaceid { get; set; }
    }

    /// <summary>
    /// 重命名微盘空间请求。
    /// </summary>
    public class WeDriveRenameSpaceRequest
    {
        /// <summary>
        /// 空间 ID。
        /// </summary>
        public string spaceid { get; set; }

        /// <summary>
        /// 重命名后的空间标题。
        /// </summary>
        public string space_name { get; set; }
    }

    /// <summary>
    /// 仅包含微盘空间 ID 的请求。
    /// </summary>
    public class WeDriveSpaceIdRequest
    {
        /// <summary>
        /// 空间 ID。
        /// </summary>
        public string spaceid { get; set; }
    }

    /// <summary>
    /// 添加或移除微盘空间成员、部门的请求。
    /// </summary>
    public class WeDriveSpaceAclRequest
    {
        /// <summary>
        /// 空间 ID。
        /// </summary>
        public string spaceid { get; set; }

        /// <summary>
        /// 待添加或移除的成员、部门信息列表。
        /// </summary>
        public IList<WeDriveAuthInfo> auth_info { get; set; }
    }

    /// <summary>
    /// 修改微盘空间安全设置请求。
    /// </summary>
    public class WeDriveSpaceSettingRequest
    {
        /// <summary>
        /// 空间 ID。
        /// </summary>
        public string spaceid { get; set; }

        /// <summary>
        /// 是否启用水印；不修改时不传。
        /// </summary>
        public bool? enable_watermark { get; set; }

        /// <summary>
        /// 是否启用保密模式；不修改时不传。
        /// </summary>
        public bool? enable_confidential_mode { get; set; }

        /// <summary>
        /// 是否允许通过邀请链接直接加入空间而无需审批；不修改时不传。
        /// </summary>
        public bool? share_url_no_approve { get; set; }

        /// <summary>
        /// 免审批邀请链接的默认权限；不修改时不传。
        /// </summary>
        public int? share_url_no_approve_default_auth { get; set; }

        /// <summary>
        /// 文件默认可查看范围：1 仅成员，2 企业内；不修改时不传。
        /// </summary>
        public int? default_file_scope { get; set; }

        /// <summary>
        /// 是否禁止文件分享到企业外；不修改时不传。
        /// </summary>
        public bool? ban_share_external { get; set; }
    }

    /// <summary>
    /// 微盘空间授权列表。
    /// </summary>
    public class WeDriveSpaceAuthList
    {
        /// <summary>
        /// 当前空间成员、部门及权限。
        /// </summary>
        public IList<WeDriveAuthInfo> auth_info { get; set; }

        /// <summary>
        /// 已离职或已退出企业的成员 UserID 列表。
        /// </summary>
        public IList<string> quit_userid { get; set; }
    }

    /// <summary>
    /// 微盘空间安全设置。
    /// </summary>
    public class WeDriveSpaceSecureSetting
    {
        /// <summary>
        /// 是否启用水印。
        /// </summary>
        public bool enable_watermark { get; set; }

        /// <summary>
        /// 是否仅管理员可添加成员。
        /// </summary>
        public bool add_member_only_admin { get; set; }

        /// <summary>
        /// 是否允许使用分享链接。
        /// </summary>
        public bool enable_share_url { get; set; }

        /// <summary>
        /// 是否允许通过邀请链接直接加入而无需审批。
        /// </summary>
        public bool share_url_no_approve { get; set; }

        /// <summary>
        /// 免审批邀请链接的默认权限。
        /// </summary>
        public int share_url_no_approve_default_auth { get; set; }

        /// <summary>
        /// 是否允许分享到企业外。
        /// </summary>
        public bool enable_share_external { get; set; }

        /// <summary>
        /// 是否仅管理员可配置企业外分享。
        /// </summary>
        public bool enable_share_external_admin { get; set; }

        /// <summary>
        /// 是否允许向空间添加外部联系人。
        /// </summary>
        public bool enable_space_add_external_member { get; set; }

        /// <summary>
        /// 是否仅管理员可添加外部联系人。
        /// </summary>
        public bool enable_space_add_external_member_admin { get; set; }

        /// <summary>
        /// 是否启用保密模式。
        /// </summary>
        public bool enable_confidential_mode { get; set; }

        /// <summary>
        /// 文件默认可查看范围。
        /// </summary>
        public int default_file_scope { get; set; }

        /// <summary>
        /// 是否仅管理员可创建文件。
        /// </summary>
        public bool create_file_only_admin { get; set; }

        /// <summary>
        /// 是否禁止文件分享到企业外。
        /// </summary>
        public bool ban_share_external { get; set; }
    }

    /// <summary>
    /// 微盘空间信息。
    /// </summary>
    public class WeDriveSpaceInfo
    {
        /// <summary>
        /// 空间 ID。
        /// </summary>
        public string spaceid { get; set; }

        /// <summary>
        /// 空间标题。
        /// </summary>
        public string space_name { get; set; }

        /// <summary>
        /// 空间成员和权限信息。
        /// </summary>
        public WeDriveSpaceAuthList auth_list { get; set; }

        /// <summary>
        /// 空间子类型。
        /// </summary>
        public int space_sub_type { get; set; }

        /// <summary>
        /// 空间安全设置；调用新版空间信息接口时返回。
        /// </summary>
        public WeDriveSpaceSecureSetting secure_setting { get; set; }
    }

    /// <summary>
    /// 获取微盘空间信息结果。
    /// </summary>
    public class WeDriveSpaceInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 空间信息。
        /// </summary>
        public WeDriveSpaceInfo space_info { get; set; }
    }

    /// <summary>
    /// 获取微盘空间邀请链接结果。
    /// </summary>
    public class WeDriveSpaceShareResult : WorkJsonResult
    {
        /// <summary>
        /// 空间邀请链接。
        /// </summary>
        public string space_share_url { get; set; }
    }
}
