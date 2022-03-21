using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    /// <summary>
    /// 在腾讯地图中创建门店返回结果
    /// </summary>
    public class CreateMapPoiJsonResult : WxJsonResult
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string error { get; set; }

        public ResultData data { get; set; }
    }
    [Serializable]
    public class ResultData
    {
        public int base_id { get; set; }

        public int rich_id { get; set; }
    }
}
