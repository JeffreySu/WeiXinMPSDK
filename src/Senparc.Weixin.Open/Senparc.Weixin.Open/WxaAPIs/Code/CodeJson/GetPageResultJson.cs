using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class GetPageResultJson : WxJsonResult
    {
        public List<PageInfo> page_list { get; set; }
    }


    [Serializable]
    public class PageInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string index { get; set; }

        public string page_list { get; set; }

        public string page_detail { get; set; }
    }
}
