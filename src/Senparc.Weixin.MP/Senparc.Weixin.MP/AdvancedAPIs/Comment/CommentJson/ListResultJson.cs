using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.Comment.CommentJson
{
    public class ListResultJson:WxJsonResult
    {
        /// <summary>
        /// 总数，非comment的size around
        /// </summary>
        public int total { get; set; }

    }
}
