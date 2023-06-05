using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WorkBench.WorkBenchJson
{
    /// <summary>
    /// webview型参数，type为"webview"
    /// </summary>
    public class WorkBenchWebViewModel
    {
        /// <summary>
        /// 渲染展示的url
        /// </summary>
        public string url {  get; set; }
        /// <summary>
        /// 点击跳转url。若不填且应用设置了主页url，则跳转到主页url，否则跳到应用会话窗口。如果enable_webview_click为true，则jump_url失效，点击不再跳转。
        /// </summary>
        public string jump_url { get; set; }
        /// <summary>
        /// 	若应用为小程序类型，该字段填小程序pagepath，若未设置，跳到小程序主页
        /// </summary>
        public string pagepath { get; set; }
        /// <summary>
        /// 高度。可以有两种选择：single_row与double_row。当为single_row时，高度与关键数据型一致，当为double_row时，高度固定为170px。默认值为double_row
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// 是否要隐藏展示了应用名称的标题部分，默认值为false
        /// </summary>
        public bool hide_title { get; set; }
        /// <summary>
        /// 是否开启webview内的链接跳转能力，默认值为false。注意：开启之后，会使jump_url失效。链接跳转仅支持以下schema方式：wxwork://openurl?url=xxxx，注意url需要进行编码。
        /// 参考示例：<a href="wxwork://openurl?url=https%3A%2F%2Fwork.weixin.qq.com%2F">今日要闻</a>
        /// </summary>
        public bool enable_webview_click { get; set; }
    }
}
