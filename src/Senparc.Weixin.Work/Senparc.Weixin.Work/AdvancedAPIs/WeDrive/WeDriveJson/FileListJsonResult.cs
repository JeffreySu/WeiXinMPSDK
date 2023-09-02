using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 文件列表返回参数
    /// </summary>
    public class FileListJsonResult:WorkJsonResult
    {
        /// <summary>
        /// true为列表还有内容, 需要继续分批拉取
        /// </summary>
        public bool has_more { get; set; }
        /// <summary>
        /// 下次分批拉取对应的请求参数start值
        /// </summary>
        public int next_start { get; set; }
        /// <summary>
        /// 文件列表
        /// </summary>
        public FileList file_list { get; set; }
    }
    public class FileList
    {
        public List<FileInfo> item=new List<FileInfo>();
    }
    /// <summary>
    /// 文件信息
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// 文件fileid
        /// </summary>
        public string fileid { get; set; }
        /// <summary>
        /// 	文件名字
        /// </summary>
        public string file_name { get; set; }
        /// <summary>
        /// 文件所在的空间spaceid
        /// </summary>
        public string spaceid { get; set; }
        /// <summary>
        /// 文件所在的目录fileid, 在根目录时为fileid
        /// </summary>
        public string fatherid { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public int file_size { get; set; }
        /// <summary>
        /// 文件创建时间
        /// </summary>
        public int ctime { get; set; }
        /// <summary>
        /// 文件最后修改时间
        /// </summary>
        public int mtime { get; set; }
        /// <summary>
        /// 文件类型, 1:文件夹 2:文件 3:微文档(文档) 4:微文档(表格) 5:微文档(收集表)
        /// </summary>
        public int file_type { get; set; }
        /// <summary>
        /// 文件状态, 1:正常 2:删除
        /// </summary>
        public int file_status { get; set; }
        /// <summary>
        /// 文件sha
        /// </summary>
        public string sha { get; set; }
        /// <summary>
        /// 文件md5
        /// </summary>
        public string md5 { get; set; }
        /// <summary>
        /// 仅微文档类型返回访问链接
        /// </summary>
        public string url { get; set; }
    }
}
