using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.GenerateSchemeJson
{
    /// <summary>
    /// GenerateScheme() 接口返回参数
    /// </summary>
    public class GenerateSchemeJsonResult : WxJsonResult
    {
        /// <summary>
        /// openlink
        /// </summary>
        public string openlink { get; set; }
    }
}
