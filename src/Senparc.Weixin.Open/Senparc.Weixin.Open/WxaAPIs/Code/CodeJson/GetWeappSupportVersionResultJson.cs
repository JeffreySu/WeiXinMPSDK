using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    [Serializable]
    public class GetWeappSupportVersionResultJson : CodeResultJson
    {
        public string now_version { get; set; }

        public UvInfo uv_info { get; set; }
    }

    [Serializable]
    public class UvInfo
    {
        public List<UvItemInfo> items { get; set; }
    }

    [Serializable]
    public class UvItemInfo
    {
        /// <summary>
        /// 受影响比例
        /// </summary>
        public int percentage { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string version { get; set; }
    }
}
