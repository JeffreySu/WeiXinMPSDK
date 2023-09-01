using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 微盘内新建空间参数
    /// </summary>
    public class SpaceCreateModel
    {
        /// <summary>
        /// 空间标题
        /// </summary>
        public string space_name { get; set; }
        public List<AuthInfoModel> auth_info = new List<AuthInfoModel>();
        /// <summary>
        /// 区分创建空间类型, 0:普通（目前只支持0）
        /// </summary>
        public int space_sub_type { get; set; }
    }
    /// <summary>
    /// 空间其他成员信息
    /// </summary>
    public class AuthInfoModel
    {
        /// <summary>
        /// 成员类型 1:个人 2:部门
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 成员userid,字符串
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 部门departmentid, 32位整型范围是[0, 2^32)
        /// </summary>
        public int departmentid { get; set; }
        /// <summary>
        /// 成员权限 1:仅下载 4:可预览（仅专业版微盘企业可设置） 7:应用空间管理员(最多可指定3个，不支持设置部门)
        /// </summary>
        public int auth { get; set; }
    }
}
