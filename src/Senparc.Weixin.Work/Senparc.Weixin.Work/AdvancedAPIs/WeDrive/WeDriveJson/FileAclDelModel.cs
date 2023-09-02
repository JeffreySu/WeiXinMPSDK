using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 删除成员入口参数
    /// </summary>
    public class FileAclDelModel
    {
        /// <summary>
        /// 文件fileid
        /// </summary>
        public string fileid { get; set; }
        /// <summary>
        /// 被移除的成员信息
        /// </summary>
        public List<AuthInfoModel> auth_info=new List<AuthInfoModel>();
    }
}
