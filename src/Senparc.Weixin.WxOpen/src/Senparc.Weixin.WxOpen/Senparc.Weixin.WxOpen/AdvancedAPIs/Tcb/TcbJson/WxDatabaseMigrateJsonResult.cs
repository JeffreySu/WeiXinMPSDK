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
}
