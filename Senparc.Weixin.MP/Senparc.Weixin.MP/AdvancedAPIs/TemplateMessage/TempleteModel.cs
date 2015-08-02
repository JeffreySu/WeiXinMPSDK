using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class TempleteModel
    {
        /// <summary>
        /// 目标用户OpenId
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 模板消息顶部颜色（16进制），默认为#FF0000
        /// </summary>
        public string topcolor { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }

        public string url { get; set; }


        public TempleteModel()
        {
            topcolor = "#FF0000";
        }
    }
}
