using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// CustomApi.SendMenu() 方法参数
    /// </summary>
    public class SendMenuContent
    {
        public string id { get; set; }
        public string content { get; set; }

        public SendMenuContent(string mid, string mContent)
        {
            this.id = mid;
            this.content = mContent;
        }
    }
}
