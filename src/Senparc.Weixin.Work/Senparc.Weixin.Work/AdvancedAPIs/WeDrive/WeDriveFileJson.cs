/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeDriveFileJson.cs
    文件功能描述：企业微信微盘文件接口强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐微盘文件、分块上传、权限与安全设置模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive
{
    /// <summary>
    /// 获取微盘文件列表请求。
    /// </summary>
    public class WeDriveFileListRequest
    {
        /// <summary>
        /// 空间 ID。
        /// </summary>
        public string spaceid { get; set; }

        /// <summary>
        /// 当前目录的文件 ID；根目录填写空间 ID。
        /// </summary>
        public string fatherid { get; set; }

        /// <summary>
        /// 排序方式：1 名称升序、2 名称降序、3 大小升序、4 大小降序、5 修改时间升序、6 修改时间降序。
        /// </summary>
        public int sort_type { get; set; }

        /// <summary>
        /// 分页起始游标；首次填写 0，后续填写上次返回的 next_start。
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// 单次拉取数量，最大 1000。
        /// </summary>
        public int limit { get; set; }
    }

    /// <summary>
    /// 上传微盘文件请求；单文件大小上限为 10 MB，较大文件请使用分块上传接口。
    /// </summary>
    public class WeDriveFileUploadRequest
    {
        /// <summary>
        /// 空间 ID；使用 selected_ticket 时不需要填写。
        /// </summary>
        public string spaceid { get; set; }

        /// <summary>
        /// 父目录文件 ID；根目录填写空间 ID，使用 selected_ticket 时不需要填写。
        /// </summary>
        public string fatherid { get; set; }

        /// <summary>
        /// 微盘和文件选择器 JSAPI 返回的 selectedTicket；填写后无需填写 spaceid 和 fatherid。
        /// </summary>
        public string selected_ticket { get; set; }

        /// <summary>
        /// 文件名，最多 255 个字符。
        /// </summary>
        public string file_name { get; set; }

        /// <summary>
        /// 文件内容的 Base64 字符串，不包含 data URI 前缀。
        /// </summary>
        public string file_base64_content { get; set; }
    }

    /// <summary>
    /// 上传微盘文件结果。
    /// </summary>
    public class WeDriveFileUploadResult : WorkJsonResult
    {
        /// <summary>
        /// 新建文件的 ID。
        /// </summary>
        public string fileid { get; set; }
    }

    /// <summary>
    /// 下载微盘文件请求。
    /// </summary>
    public class WeDriveFileDownloadRequest
    {
        /// <summary>
        /// 普通文件 ID；使用 selected_ticket 时不需要填写。不支持直接下载文件夹或在线文档。
        /// </summary>
        public string fileid { get; set; }

        /// <summary>
        /// 微盘和文件选择器 JSAPI 返回的 selectedTicket；填写后无需填写 fileid。
        /// </summary>
        public string selected_ticket { get; set; }
    }

    /// <summary>
    /// 下载微盘文件结果。
    /// </summary>
    public class WeDriveFileDownloadResult : WorkJsonResult
    {
        /// <summary>
        /// 文件下载地址，有效期通常为 2 小时。
        /// </summary>
        public string download_url { get; set; }

        /// <summary>
        /// 下载请求必须携带的 Cookie 名称。
        /// </summary>
        public string cookie_name { get; set; }

        /// <summary>
        /// 下载请求必须携带的 Cookie 值。
        /// </summary>
        public string cookie_value { get; set; }
    }

    /// <summary>
    /// 初始化微盘文件分块上传请求。
    /// </summary>
    public class WeDriveFileUploadInitializeRequest
    {
        /// <summary>
        /// 空间 ID；使用 selected_ticket 时不需要填写。
        /// </summary>
        public string spaceid { get; set; }

        /// <summary>
        /// 当前目录的文件 ID；根目录填写空间 ID，使用 selected_ticket 时不需要填写。
        /// </summary>
        public string fatherid { get; set; }

        /// <summary>
        /// 微盘和文件选择器 JSAPI 返回的 selectedTicket；填写后无需填写 spaceid 和 fatherid。
        /// </summary>
        public string selected_ticket { get; set; }

        /// <summary>
        /// 文件名。
        /// </summary>
        public string file_name { get; set; }

        /// <summary>
        /// 文件总大小（字节），最大支持 20 GB。
        /// </summary>
        public long size { get; set; }

        /// <summary>
        /// 按分块顺序排列的各分块累计 SHA 值。
        /// </summary>
        public IList<string> block_sha { get; set; }

        /// <summary>
        /// 文件创建完成时是否跳过企业微信卡片推送；不传时默认为 false。
        /// </summary>
        public bool? skip_push_card { get; set; }
    }

    /// <summary>
    /// 初始化微盘文件分块上传结果。
    /// </summary>
    public class WeDriveFileUploadInitializeResult : WorkJsonResult
    {
        /// <summary>
        /// 是否命中秒传；命中时无需继续上传分块。
        /// </summary>
        public bool hit_exist { get; set; }

        /// <summary>
        /// 文件上传凭证；未命中秒传时返回。
        /// </summary>
        public string upload_key { get; set; }

        /// <summary>
        /// 文件 ID；命中秒传时返回。
        /// </summary>
        public string fileid { get; set; }
    }

    /// <summary>
    /// 上传微盘文件分块请求。
    /// </summary>
    public class WeDriveFileUploadPartRequest
    {
        /// <summary>
        /// 初始化分块上传返回的上传凭证。
        /// </summary>
        public string upload_key { get; set; }

        /// <summary>
        /// 文件分块序号。
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// 当前文件分块内容的 Base64 字符串，不包含 data URI 前缀。
        /// </summary>
        public string file_base64_content { get; set; }
    }

    /// <summary>
    /// 完成微盘文件分块上传请求。
    /// </summary>
    public class WeDriveFileUploadFinishRequest
    {
        /// <summary>
        /// 初始化分块上传返回的上传凭证。
        /// </summary>
        public string upload_key { get; set; }
    }

    /// <summary>
    /// 完成微盘文件分块上传结果。
    /// </summary>
    public class WeDriveFileUploadFinishResult : WorkJsonResult
    {
        /// <summary>
        /// 上传完成后的文件 ID。
        /// </summary>
        public string fileid { get; set; }
    }

    /// <summary>
    /// 在微盘中新建文件夹或在线文档的请求。
    /// </summary>
    public class WeDriveFileCreateRequest
    {
        /// <summary>
        /// 空间 ID。
        /// </summary>
        public string spaceid { get; set; }

        /// <summary>
        /// 父目录文件 ID；根目录填写空间 ID。
        /// </summary>
        public string fatherid { get; set; }

        /// <summary>
        /// 文件类型：1 文件夹、3 文档、4 表格、6 幻灯片。
        /// </summary>
        public int file_type { get; set; }

        /// <summary>
        /// 文件名，最多 255 个字符。
        /// </summary>
        public string file_name { get; set; }
    }

    /// <summary>
    /// 新建微盘文件夹或在线文档结果。
    /// </summary>
    public class WeDriveFileCreateResult : WorkJsonResult
    {
        /// <summary>
        /// 新建文件的 ID。
        /// </summary>
        public string fileid { get; set; }

        /// <summary>
        /// 在线文档访问链接；仅新建在线文档时返回。
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 重命名微盘文件请求。
    /// </summary>
    public class WeDriveFileRenameRequest
    {
        /// <summary>
        /// 文件 ID。
        /// </summary>
        public string fileid { get; set; }

        /// <summary>
        /// 重命名后的文件名，最多 255 个字符。
        /// </summary>
        public string new_name { get; set; }
    }

    /// <summary>
    /// 移动微盘文件请求。
    /// </summary>
    public class WeDriveFileMoveRequest
    {
        /// <summary>
        /// 目标目录文件 ID；根目录填写空间 ID。
        /// </summary>
        public string fatherid { get; set; }

        /// <summary>
        /// 如果目标目录存在同名文件，是否覆盖；false 表示自动冲突重命名。
        /// </summary>
        public bool replace { get; set; }

        /// <summary>
        /// 待移动的文件 ID 列表。
        /// </summary>
        public IList<string> fileid { get; set; }
    }

    /// <summary>
    /// 删除微盘文件请求。
    /// </summary>
    public class WeDriveFileDeleteRequest
    {
        /// <summary>
        /// 待删除的文件 ID 列表。
        /// </summary>
        public IList<string> fileid { get; set; }
    }

    /// <summary>
    /// 仅包含微盘文件 ID 的请求。
    /// </summary>
    public class WeDriveFileIdRequest
    {
        /// <summary>
        /// 文件 ID。
        /// </summary>
        public string fileid { get; set; }
    }

    /// <summary>
    /// 微盘文件基本信息。
    /// </summary>
    public class WeDriveFileInfo
    {
        /// <summary>
        /// 文件 ID。
        /// </summary>
        public string fileid { get; set; }

        /// <summary>
        /// 文件名。
        /// </summary>
        public string file_name { get; set; }

        /// <summary>
        /// 文件所在空间 ID。
        /// </summary>
        public string spaceid { get; set; }

        /// <summary>
        /// 文件所在目录 ID；位于根目录时为所在空间 ID。
        /// </summary>
        public string fatherid { get; set; }

        /// <summary>
        /// 文件大小（字节）。
        /// </summary>
        public long? file_size { get; set; }

        /// <summary>
        /// 文件创建时间戳（秒）。
        /// </summary>
        public long? ctime { get; set; }

        /// <summary>
        /// 文件最后修改时间戳（秒）。
        /// </summary>
        public long? mtime { get; set; }

        /// <summary>
        /// 文件类型：1 文件夹、2 普通文件、3 文档、4 表格、5 收集表、6 幻灯片。
        /// </summary>
        public int? file_type { get; set; }

        /// <summary>
        /// 文件状态：1 正常，2 已删除。
        /// </summary>
        public int? file_status { get; set; }

        /// <summary>
        /// 创建人 UserID；旧版接口可能返回。
        /// </summary>
        public string create_userid { get; set; }

        /// <summary>
        /// 最后更新人 UserID；旧版接口可能返回。
        /// </summary>
        public string update_userid { get; set; }

        /// <summary>
        /// 文件 SHA 值。
        /// </summary>
        public string sha { get; set; }

        /// <summary>
        /// 文件 MD5 值。
        /// </summary>
        public string md5 { get; set; }

        /// <summary>
        /// 在线文档访问链接；仅在线文档类型返回。
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 微盘文件列表包装对象。
    /// </summary>
    public class WeDriveFileList
    {
        /// <summary>
        /// 文件条目。
        /// </summary>
        public IList<WeDriveFileInfo> item { get; set; }
    }

    /// <summary>
    /// 获取微盘文件列表结果。
    /// </summary>
    public class WeDriveFileListResult : WorkJsonResult
    {
        /// <summary>
        /// 是否还有更多文件。
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 下一次分页请求应填写的 start 值。
        /// </summary>
        public int? next_start { get; set; }

        /// <summary>
        /// 文件列表。
        /// </summary>
        public WeDriveFileList file_list { get; set; }
    }

    /// <summary>
    /// 获取微盘文件信息结果。
    /// </summary>
    public class WeDriveFileInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 文件详细信息。
        /// </summary>
        public WeDriveFileInfo file_info { get; set; }
    }

    /// <summary>
    /// 重命名微盘文件结果。
    /// </summary>
    public class WeDriveFileRenameResult : WorkJsonResult
    {
        /// <summary>
        /// 重命名后的文件信息。
        /// </summary>
        public WeDriveFileInfo file { get; set; }
    }

    /// <summary>
    /// 移动微盘文件结果。
    /// </summary>
    public class WeDriveFileMoveResult : WorkJsonResult
    {
        /// <summary>
        /// 移动后的文件列表。
        /// </summary>
        public WeDriveFileList file_list { get; set; }
    }

    /// <summary>
    /// 添加或移除微盘文件成员、部门的请求。
    /// </summary>
    public class WeDriveFileAclRequest
    {
        /// <summary>
        /// 文件 ID。
        /// </summary>
        public string fileid { get; set; }

        /// <summary>
        /// 待添加或移除的成员、部门信息列表。
        /// </summary>
        public IList<WeDriveAuthInfo> auth_info { get; set; }
    }

    /// <summary>
    /// 修改微盘文件分享设置请求。
    /// </summary>
    public class WeDriveFileSettingRequest
    {
        /// <summary>
        /// 文件 ID。
        /// </summary>
        public string fileid { get; set; }

        /// <summary>
        /// 权限范围：1 指定人、2 企业内、3 企业外、4 企业内需管理员审批、5 企业外需管理员审批。
        /// </summary>
        public int auth_scope { get; set; }

        /// <summary>
        /// 权限类型；不修改权限时不传。
        /// </summary>
        public int? auth { get; set; }
    }

    /// <summary>
    /// 微盘文件水印设置。
    /// </summary>
    public class WeDriveWatermark
    {
        /// <summary>
        /// 水印文字。
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 水印边距或布局类型。
        /// </summary>
        public int? margin_type { get; set; }

        /// <summary>
        /// 是否显示自定义水印文字。
        /// </summary>
        public bool? show_text { get; set; }

        /// <summary>
        /// 是否显示访问者名称。
        /// </summary>
        public bool? show_visitor_name { get; set; }

        /// <summary>
        /// 是否由企业管理员强制启用水印。
        /// </summary>
        public bool? force_by_admin { get; set; }

        /// <summary>
        /// 是否由空间管理员强制启用水印。
        /// </summary>
        public bool? force_by_space_admin { get; set; }
    }

    /// <summary>
    /// 修改微盘文件安全设置请求。
    /// </summary>
    public class WeDriveFileSecureSettingRequest
    {
        /// <summary>
        /// 文件 ID；该接口仅支持在线文档类型。
        /// </summary>
        public string fileid { get; set; }

        /// <summary>
        /// 水印设置；不修改水印时可不传。
        /// </summary>
        public WeDriveWatermark watermark { get; set; }
    }

    /// <summary>
    /// 获取微盘文件分享链接结果。
    /// </summary>
    public class WeDriveFileShareResult : WorkJsonResult
    {
        /// <summary>
        /// 文件分享链接。
        /// </summary>
        public string share_url { get; set; }
    }

    /// <summary>
    /// 微盘文件企业内外分享范围。
    /// </summary>
    public class WeDriveFileShareRange
    {
        /// <summary>
        /// 是否允许企业内访问。
        /// </summary>
        public bool enable_corp_internal { get; set; }

        /// <summary>
        /// 企业内访问权限类型。
        /// </summary>
        public int corp_internal_auth { get; set; }

        /// <summary>
        /// 企业内访问是否仅允许管理员审批。
        /// </summary>
        public bool? corp_internal_approve_only_by_admin { get; set; }

        /// <summary>
        /// 是否允许企业外访问。
        /// </summary>
        public bool enable_corp_external { get; set; }

        /// <summary>
        /// 企业外访问权限类型。
        /// </summary>
        public int corp_external_auth { get; set; }

        /// <summary>
        /// 企业外访问是否仅允许管理员审批。
        /// </summary>
        public bool? corp_external_approve_only_by_admin { get; set; }
    }

    /// <summary>
    /// 微盘文件安全配置。
    /// </summary>
    public class WeDriveFileSecureSetting
    {
        /// <summary>
        /// 是否开启只读副本。
        /// </summary>
        public bool enable_readonly_copy { get; set; }

        /// <summary>
        /// 是否仅允许管理员修改。
        /// </summary>
        public bool modify_only_by_admin { get; set; }

        /// <summary>
        /// 是否允许只读评论。
        /// </summary>
        public bool enable_readonly_comment { get; set; }

        /// <summary>
        /// 是否禁止分享到企业外。
        /// </summary>
        public bool ban_share_external { get; set; }
    }

    /// <summary>
    /// 从父目录继承的微盘文件权限。
    /// </summary>
    public class WeDriveInheritedAuth
    {
        /// <summary>
        /// 继承的授权列表。
        /// </summary>
        public IList<WeDriveAuthInfo> auth_list { get; set; }

        /// <summary>
        /// 是否启用父目录权限继承。
        /// </summary>
        public bool inherit { get; set; }
    }

    /// <summary>
    /// 获取微盘文件权限结果。
    /// </summary>
    public class WeDriveFilePermissionResult : WorkJsonResult
    {
        /// <summary>
        /// 文件分享范围。
        /// </summary>
        public WeDriveFileShareRange share_range { get; set; }

        /// <summary>
        /// 文件安全配置。
        /// </summary>
        public WeDriveFileSecureSetting secure_setting { get; set; }

        /// <summary>
        /// 从父目录继承的权限。
        /// </summary>
        public WeDriveInheritedAuth inherit_father_auth { get; set; }

        /// <summary>
        /// 文件成员授权列表。
        /// </summary>
        public IList<WeDriveAuthInfo> file_member_list { get; set; }

        /// <summary>
        /// 文件协作者授权列表。
        /// </summary>
        public IList<WeDriveAuthInfo> co_auth_list { get; set; }

        /// <summary>
        /// 文件水印设置。
        /// </summary>
        public WeDriveWatermark watermark { get; set; }
    }
}
