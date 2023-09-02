using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 指定空间添加成员/部门入口参数
    /// </summary>
    public class SpaceAclAddModel
    {
        /// <summary>
        /// 空间spaceid
        /// </summary>
        public string spaceid { get; set; }
        /// <summary>
        /// 被添加的空间成员信息
        /// </summary>
        public List<AuthInfoModel> auth_info=new List<AuthInfoModel>();
    }
}
