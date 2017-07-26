using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class GetCategoryResultJson : WxJsonResult
    {
        public List<CategroyInfo> category_list { get; set; }
    }


    [Serializable]
    public class CategroyInfo
    {
        /// <summary>
        /// 一级类目名称
        /// </summary>
        public string first_class { get; set; }

        /// <summary>
        /// 二级类目名称
        /// </summary>
        public string second_class { get; set; }

        /// <summary>
        /// 三级类目名称
        /// </summary>
        public string third_class { get; set; }

        /// <summary>
        /// 一级类目的ID编号
        /// </summary>
        public int first_id { get; set; }

        /// <summary>
        /// 二级类目的ID编号
        /// </summary>
        public int second_id { get; set; }

        /// <summary>
        /// 三级类目的ID编号
        /// </summary>
        public int third_id { get; set; }
    }
}
