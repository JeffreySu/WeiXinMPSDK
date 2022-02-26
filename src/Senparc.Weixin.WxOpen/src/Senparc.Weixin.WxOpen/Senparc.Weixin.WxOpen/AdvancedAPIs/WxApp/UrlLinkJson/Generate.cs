using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.UrlLinkJson
{
    public class Generate_CloudBase
    {
        /// <summary>
        /// （必填）云开发环境
        /// </summary>
        public string env { get; set; }
        /// <summary>
        /// 静态网站自定义域名，不填则使用默认域名
        /// </summary>
        public string domain { get; set; }
        /// <summary>
        /// 云开发静态网站 H5 页面路径，不可携带 query
        /// </summary>
        public string path { get; set; } = "/";
        /// <summary>
        /// 云开发静态网站 H5 页面 query 参数，最大 1024 个字符，只支持数字，大小写英文以及部分特殊字符：`!#$&'()*+,/:;=?@-._~%``
        /// </summary>
        public string query { get; set; }
        /// <summary>
        /// 第三方批量代云开发时必填，表示创建该 env 的 appid （小程序/第三方平台）
        /// </summary>
        public string resource_appid { get; set; }
    }

    /// <summary>
    /// UrlLinkApi.Generate() 接口返回结果
    /// </summary>
    public class GenerateResultJson : WxJsonResult
    {
        /// <summary>
        /// 生成的小程序 URL Link
        /// </summary>
        public string url_link { get; set; }
    }
}
