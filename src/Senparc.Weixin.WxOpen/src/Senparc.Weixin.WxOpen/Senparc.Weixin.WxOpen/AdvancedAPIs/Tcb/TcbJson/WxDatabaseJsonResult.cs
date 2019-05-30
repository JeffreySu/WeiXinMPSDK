using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    public class WxDatabaseAddJsonResult : WxJsonResult
    {
        /// <summary>
        /// 插入成功的数据集合主键_id。
        /// </summary>
        public string[] id_list { get; set; }
    }

    public class WxDatabaseDeleteJsonResult : WxJsonResult
    {
        /// <summary>
        /// 删除记录数量
        /// </summary>
        public int deleted { get; set; }
    }

    public class WxDatabaseUpdateJsonResult : WxJsonResult
    {
        /// <summary>
        /// 更新条件匹配到的结果数
        /// </summary>
        public int matched { get; set; }
        /// <summary>
        /// 修改的记录数，注意：使用set操作新插入的数据不计入修改数目
        /// </summary>
        public int modified { get; set; }
        /// <summary>
        /// 新插入记录的id，注意：只有使用set操作新插入数据时这个字段会有值
        /// </summary>
        public string id { get; set; }
    }

    public class WxDatabaseQueryJsonResult : WxJsonResult
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public Pager pager { get; set; }
        /// <summary>
        /// 记录数组
        /// </summary>
        public string[] data { get; set; }
    }

    public class WxDatabaseCountJsonResult : WxJsonResult
    {
        /// <summary>
        /// 记录数量
        /// </summary>
        public int count { get; set; }
    }
}
