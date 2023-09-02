using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 文件进行重命名入口参数
    /// </summary>
    public class FileRenameModel
    {
        /// <summary>
        /// 文件fileid
        /// </summary>
        public string fileid { get; set; }
        /// <summary>
        /// 	重命名后的文件名 （注意：文件名最多填255个字符, 英文算1个, 汉字算2个）
        /// </summary>
        public string new_name { get; set; }
    }
}
