using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class SubmitAuditPageInfo
    {
        /// <summary>
        /// 小程序的页面
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 小程序的标签，多个标签用空格分隔，标签不能多于10个，标签长度不超过20
        /// </summary>
        public string tag { get; set; }

        /// <summary>
        /// 一级类目
        /// </summary>
        public string first_class { get; set; }

        /// <summary>
        /// 二级类目
        /// </summary>
        public string second_class { get; set; }

        /// <summary>
        /// 三级类目
        /// </summary>
        public string third_class { get; set; }

        /// <summary>
        /// 一级类目的ID
        /// </summary>
        public int first_id { get; set; }

        /// <summary>
        /// 二级类目的ID
        /// </summary>
        public int second_id { get; set; }

        /// <summary>
        /// 三级类目的ID
        /// </summary>
        public int third_id { get; set; }

        /// <summary>
        /// 小程序页面的标题,标题长度不超过32
        /// </summary>
        public string title { get; set; }
    }
}
