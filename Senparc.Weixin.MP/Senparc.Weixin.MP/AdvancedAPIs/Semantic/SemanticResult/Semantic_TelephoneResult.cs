using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 常用电话服务（telephone）
    /// </summary>
    public class Semantic_TelephoneResult : BaseSemanticResultJson
    {
        public Semantic_Telephone semantic { get; set; }
    }

    public class Semantic_Telephone : BaseSemanticIntent
    {
        public Details_Telephone details { get; set; }
    }

    public class Details_Telephone
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string telephone { get; set; }
    }
}
