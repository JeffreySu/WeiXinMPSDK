using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Open.WxOpenAPIs.AddCategoryJson
{
    /// <summary>
    /// 添加栏目接口请求数据
    /// </summary>
    public class AddCategoryData
    {
        /// <summary>
        /// 一级类目ID
        /// </summary>
        public uint first { get; set; }
        /// <summary>
        /// 二级类目ID
        /// </summary>
        public uint second { get; set; }
        /// <summary>
        /// key：资质名称，value：资质图片
        /// </summary>
        public IList<KeyValuePair<string, string>> certicates { get; set; }
    }
}
