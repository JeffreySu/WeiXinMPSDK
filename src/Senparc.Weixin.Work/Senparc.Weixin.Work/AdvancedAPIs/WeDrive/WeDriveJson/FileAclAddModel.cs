using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 指定文件添加成员入口参数
    /// </summary>
    public class FileAclAddModel
    {
        /// <summary>
        /// 文件fileid
        /// </summary>
        public string fileid { get; set; }
        /// <summary>
        /// 添加成员的信息
        /// </summary>
        public List<AuthInfoModel> auth_info=new List<AuthInfoModel>();
    }
}
