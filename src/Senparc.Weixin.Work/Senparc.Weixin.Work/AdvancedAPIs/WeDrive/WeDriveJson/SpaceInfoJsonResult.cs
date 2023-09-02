using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 空间信息返回参数
    /// </summary>
    public class SpaceInfoJsonResult : WorkJsonResult
    {
        public SpaceInfo space_info { get; set; }
    }
    public class SpaceInfo
    {
        /// <summary>
        /// 空间spaceid
        /// </summary>
        public string spaceid { get; set; }
        /// <summary>
        /// 空间名称
        /// </summary>
        public string spacename { get; set; }
        /// <summary>
        /// 空间成员列表
        /// </summary>
        public AuthList auth_list { get; set; }
        /// <summary>
        /// 空间类型 0:普通
        /// </summary>
        public int space_sub_type { get; set; } = 0;
        public SecureSetting secure_setting { get; set; }
    }
    public class AuthList
    {
        /// <summary>
        /// 空间成员信息
        /// </summary>
        public List<AuthInfoModel> auth_info = new List<AuthInfoModel>();
        /// <summary>
        /// 空间无权限成员userid (成员在一个有权限的部门中, 自己退出空间或者被移除权限)
        /// </summary>
        public List<string> quit_userid = new List<string>();
    }
    public class SecureSetting
    {
        public bool enable_watermark { get; set; }
        public bool add_member_only_admin { get; set; }
        public bool enable_share_url { get; set; }
        public bool share_url_no_approve { get; set; }
        public int share_url_no_approve_default_auth { get; set; }
        public bool enable_share_external { get; set; }
        public bool enable_share_external_admin { get; set; }
        public bool enable_space_add_external_member { get; set; }
        public bool enable_space_add_external_member_admin { get; set; }
        public bool enable_confidential_mode { get; set; }
        public int default_file_scope { get; set; }
        public bool create_file_only_admin { get; set; }
    }
}
