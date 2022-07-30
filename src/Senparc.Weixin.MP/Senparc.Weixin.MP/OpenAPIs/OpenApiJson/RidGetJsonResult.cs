using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.OpenAPIs.OpenApiJson
{
    public class RidGetJsonResult: WxJsonResult
    {
        public Request request { get; set; }
    }

    public class Request
    {
        /// <summary>
        /// 发起请求的时间戳
        /// </summary>
        public long invoke_time { get; set; }
        /// <summary>
        /// 请求毫秒级耗时
        /// </summary>
        public int cost_in_ms { get; set; }
        /// <summary>
        /// 请求的 URL 参数
        /// </summary>
        public string request_url { get; set; }
        /// <summary>
        /// post请求的请求参数
        /// </summary>
        public string request_body { get; set; }
        /// <summary>
        /// 接口请求返回参数
        /// </summary>
        public string response_body { get; set; }
        /// <summary>
        /// 接口请求的客户端ip
        /// </summary>
        public string client_ip { get; set; }
    }



}
