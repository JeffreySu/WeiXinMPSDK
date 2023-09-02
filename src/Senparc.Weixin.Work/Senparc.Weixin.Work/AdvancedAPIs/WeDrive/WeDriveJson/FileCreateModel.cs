using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 新建文件夹、文档入口参数
    /// </summary>
    public class FileCreateModel
    {
        /// <summary>
        /// 空间spaceid
        /// </summary>
        public string spaceid { get; set; }
        /// <summary>
        /// 父目录fileid, 在根目录时为空间spaceid
        /// </summary>
        public string fatherid { get; set; }
        /// <summary>
        /// 	文件类型, 1:文件夹 3:文档(文档) 4:文档(表格)
        /// </summary>
        public int file_type { get; set; }
        /// <summary>
        /// 文件名字（注意：文件名最多填255个字符, 英文算1个, 汉字算2个）
        /// </summary>
        public string file_name { get; set; }
    }
}
