using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 将文件移动到指定位置入口参数
    /// </summary>
    public class FileMoveModel
    {
        /// <summary>
        /// 当前目录的fileid,根目录时为空间spaceid
        /// </summary>
        public string fatherid { get; set; }
        /// <summary>
        /// 如果移动到的目标目录与需要移动的文件重名时，是否覆盖。true:重名文件覆盖 false:重名文件进行冲突重命名处理（移动后文件名格式如xxx(1).txt xxx(1).doc等）
        /// </summary>
        public bool replace { get; set; }
        /// <summary>
        /// 文件fileid
        /// </summary>
        public List<string> fileid { get; set; } = new List<string>();
    }
}
