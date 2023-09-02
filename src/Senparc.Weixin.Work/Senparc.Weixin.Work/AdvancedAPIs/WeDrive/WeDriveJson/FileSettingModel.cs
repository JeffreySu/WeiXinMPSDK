using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 文件的分享设置入口参数
    /// </summary>
    public class FileSettingModel
    {
        /// <summary>
        /// 文件fileid
        /// </summary>
        public string fileid { get; set; }
        /// <summary>
        /// 权限范围：1:指定人 2:企业内 3:企业外 4: 企业内需管理员审批（仅有管理员时可设置） 5: 企业外需管理员审批（仅有管理员时可设置）
        /// </summary>
        public int auth_scope { get; set; }
        /// <summary>
        /// 权限信息  普通文档： 1:仅浏览（可下载) 4:仅预览（仅专业版企业可设置）；如果不填充此字段为保持原有状态   微文档： 1:仅浏览（可下载）；如果不填充此字段为保持原有状态
        /// </summary>
        public int auth { get; set; }
    }
}
