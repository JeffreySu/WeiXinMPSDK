using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    public class FilePermissionJsonResult:WorkJsonResult
    {
        public ShareRange share_range { get; set; }
        public FileSecureSetting secure_setting { get; set; }
        public InheritFatherAuth inherit_father_auth { get; set; }
        public List<AuthInfoModel> file_member_list=new List<AuthInfoModel>();
        public Watermark watermark { get; set; }

    }
    /// <summary>
    /// 文件分享设置
    /// </summary>
    public class ShareRange
    {
        /// <summary>
        /// 是否为企业内可访问
        /// </summary>
        public bool enable_corp_internal { get; set; }
        /// <summary>
        /// 企业内权限信息   普通文档： 1:仅浏览（可下载) 4:仅预览（仅专业版企业可设置）255:无权限或需要审批；如果不填充此字段为保持原有状态     微文档： 1:仅浏览（可下载）；如果不填充此字段为保持原有状态
        /// </summary>
        public int corp_internal_auth { get; set; }
        /// <summary>
        /// 是否为企业外可访问
        /// </summary>
        public bool enable_corp_external { get; set; }
        /// <summary>
        /// 企业外权限信息  普通文档： 1:仅浏览（可下载) 4:仅预览（仅专业版企业可设置） 255:无权限或需要审批；如果不填充此字段为保持原有状态   微文档： 1:仅浏览（可下载）；如果不填充此字段为保持原有状态
        /// </summary>
        public int corp_external_auth { get; set; }
    }
    public class FileSecureSetting
    {
        /// <summary>
        /// 是否开启只读备份
        /// </summary>
        public bool enable_readonly_copy { get; set; }
        /// <summary>
        /// 是否只允许管理员进行修改
        /// </summary>
        public bool modify_only_by_admin { get; set; }
        /// <summary>
        /// 	是否开启只读评论
        /// </summary>
        public bool enable_readonly_comment { get; set; }
        /// <summary>
        /// 是否禁止分享到企业外部
        /// </summary>
        public bool ban_share_external { get; set; }
    }
    public class InheritFatherAuth
    {
        public List<AuthInfoModel> auth_list { get; set; } = new List<AuthInfoModel>();
        public bool inherit { get; set; }
    }
    public class Watermark
    {
        public string text { get; set; }
        public int margin_type { get; set; }
        public bool show_visitor_name { get; set; }
        public bool force_by_admin { get; set; }
        public bool show_text { get; set; }
        public bool force_by_space_admin { get; set; }
    }
}
