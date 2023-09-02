using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 删除文件入口参数
    /// </summary>
    public class FileDeleteModel
    {
        /// <summary>
        /// 文件fileid
        /// </summary>
        public List<string> fileid=new List<string>();
    }
}
