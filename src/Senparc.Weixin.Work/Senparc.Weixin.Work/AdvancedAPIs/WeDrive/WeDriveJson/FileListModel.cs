using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 指定地址下的文件列表入口参数
    /// </summary>
    public class FileListModel
    {
        /// <summary>
        /// 空间spaceid
        /// </summary>
        public string spaceid { get; set; }
        /// <summary>
        /// 当前目录的fileid,根目录时为空间spaceid
        /// </summary>
        public string fatherid { get; set; }
        /// <summary>
        /// 列表排序方式 1:名字升序；2:名字降序；3:大小升序；4:大小降序；5:修改时间升序；6:修改时间降序
        /// </summary>
        public int sort_type { get; set; }
        /// <summary>
        /// 首次填0, 后续填上一次请求返回的next_start
        /// </summary>
        public int start { get; set; }
        /// <summary>
        /// 	分批拉取最大文件数, 不超过1000
        /// </summary>
        public int limit { get; set; }
    }
}
