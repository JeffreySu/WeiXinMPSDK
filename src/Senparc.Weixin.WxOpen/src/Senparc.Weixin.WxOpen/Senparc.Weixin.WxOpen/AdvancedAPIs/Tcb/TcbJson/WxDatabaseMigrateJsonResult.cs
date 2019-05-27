using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    public class WxDatabaseMigrateJsonResult : WxJsonResult
    {
        /// <summary>
        /// 任务ID，可使用数据库迁移进度查询 API 查询进度及结果
        /// </summary>
        public int job_id { get; set; }
    }

    public class WxDatabaseMigrateQueryInfoJsonResult : WxJsonResult
    {
        /// <summary>
        /// 导出状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 导出成功记录数
        /// </summary>
        public int record_success { get; set; }
        /// <summary>
        /// 导出失败记录数
        /// </summary>
        public int record_fail { get; set; }
        /// <summary>
        /// 导出错误信息
        /// </summary>
        public string err_msg { get; set; }
        /// <summary>
        /// 导出文件下载地址
        /// </summary>
        public string file_url { get; set; }
    }
}
