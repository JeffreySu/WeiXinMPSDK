using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 修改空间权限入口参数
    /// </summary>
    public class SpaceSettingModel
    {
        /// <summary>
        /// 空间spaceid
        /// </summary>
        public string spaceid { get; set; }
        /// <summary>
        /// （本字段仅专业版企业可设置）启用水印。false:关 true:开 ;如果不填充此字段为保持原有状态
        /// </summary>
        public bool enable_watermark { get; set; }
        /// <summary>
        /// 是否开启保密模式。false:关 true:开 如果不填充此字段为保持原有状态
        /// </summary>
        public bool enable_confidential_mode { get; set; }
        /// <summary>
        /// 文件默认可查看范围。1:仅成员；2:企业内。如果不填充此字段为保持原有状态
        /// </summary>
        public int default_file_scope { get; set; }
        /// <summary>
        /// 是否禁止文件分享到企业外｜false:关 true:开 如果不填充此字段为保持原有状态
        /// </summary>
        public bool ban_share_external { get; set; }
    }
}
