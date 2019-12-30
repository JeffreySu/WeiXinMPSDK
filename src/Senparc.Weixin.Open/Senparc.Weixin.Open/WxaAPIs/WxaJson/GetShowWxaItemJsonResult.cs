using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class GetShowWxaItemJsonResult : WxJsonResult
    {
        /// <summary>
        /// 是否可以设置 1 可以，0，不可以
        /// </summary>
        public int can_open { get; set; }

        /// <summary>
        /// 是否已经设置，1 已设置，0，未设置
        /// </summary>
        public int is_open { get; set; }

        /// <summary>
        /// 展示的公众号 appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 展示的公众号 nickname
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 展示的公众号头像
        /// </summary>
        public string headimg { get; set; }
    }
}
