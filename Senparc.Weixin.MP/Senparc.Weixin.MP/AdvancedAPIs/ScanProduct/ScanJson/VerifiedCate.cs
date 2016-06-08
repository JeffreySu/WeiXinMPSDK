using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    /// <summary>
    /// 验证目录
    /// </summary>
    public class VerifiedCate
    {
        /// <summary>
        /// 商品类目Id
        /// </summary>
        public int verified_cate_id { get; set; }

        /// <summary>
        /// 商品类目名称
        /// </summary>
        public string verified_cate_name { get; set; }
    }
}
