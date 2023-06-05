using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WorkBench.WorkBenchJson
{
    /// <summary>
    /// 图片型参数 type为"image"
    /// </summary>
    public class WorkBenchImageModel
    {
        /// <summary>
        /// 图片url。图片的最佳比例为3.35:1
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 点击跳转url。若不填且应用设置了主页url，则跳转到主页url，否则跳到应用会话窗口
        /// </summary>
        public string jump_url { get; set; }
        /// <summary>
        /// 若应用为小程序类型，该字段填小程序pagepath，若未设置，跳到小程序主页
        /// </summary>
        public string pagepath { get; set; }
    }
}
