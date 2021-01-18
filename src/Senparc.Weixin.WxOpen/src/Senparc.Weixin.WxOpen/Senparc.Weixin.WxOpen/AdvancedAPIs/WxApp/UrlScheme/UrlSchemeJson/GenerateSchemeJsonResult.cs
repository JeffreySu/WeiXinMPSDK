using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.UrlScheme
{
    /// <summary>
    /// GenerateScheme() 接口返回参数
    /// </summary>
    public class GenerateSchemeJsonResult : WxJsonResult
    {
        /// <summary>
        /// 小程序scheme码
        /// </summary>
        public string openlink { get; set; }
    }
}
