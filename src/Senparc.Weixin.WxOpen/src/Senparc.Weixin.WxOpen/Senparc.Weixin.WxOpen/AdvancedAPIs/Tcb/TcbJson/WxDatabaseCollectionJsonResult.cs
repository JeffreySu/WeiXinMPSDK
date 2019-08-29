using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    public class WxDatabaseCollectionJsonResult : WxJsonResult
    {
        /// <summary>
        /// 集合信息
        /// </summary>
        public Collection[] collections { get; set; }
        /// <summary>
        /// 分页信息
        /// </summary>
        public Pager pager { get; set; }
    }

    public class Pager
    {
        /// <summary>
        /// 偏移
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 单次查询限制
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// 符合查询条件的记录总数
        /// </summary>
        public int Total { get; set; }
    }

    public class Collection
    {
        /// <summary>
        /// 集合名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 表中文档数量
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 表的大小（即表中文档总大小），单位：字节
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 索引数量
        /// </summary>
        public int index_count { get; set; }
        /// <summary>
        /// 索引占用大小，单位：字节
        /// </summary>
        public int index_size { get; set; }
    }

}
