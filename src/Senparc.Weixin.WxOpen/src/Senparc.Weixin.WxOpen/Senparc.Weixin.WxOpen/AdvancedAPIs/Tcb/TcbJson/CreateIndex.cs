using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    /// <summary>
    /// 新增索引
    /// </summary>
    public class CreateIndex
    {
        /// <summary>
        /// 索引名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 是否唯一
        /// </summary>
        public bool unique { get; set; }
        /// <summary>
        /// 索引字段
        /// </summary>
        public Key[] keys { get; set; }
    }
    /// <summary>
    /// 索引字段
    /// </summary>
    public class Key
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 字段排序
        /// "1"        升序
        /// "-1"        降序
        /// "2dsphere"        地理位置
        /// </summary>
        public string direction { get; set; }
    }
    /// <summary>
    /// 删除索引
    /// </summary>
    public class DropIndex
    {
        /// <summary>
        /// 索引名
        /// </summary>
        public string name { get; set; }
    }

}
