using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WorkBench.WorkBenchJson
{
    /// <summary>
    /// 指定应用自定义模版类型
    /// </summary>
    public class SetWorkBenchTemplateModel
    {
        /// <summary>
        /// 模版类型，目前支持的自定义类型包括 "keydata"、 "image"、 "list"、 "webview" 。若设置的type为 "normal",则相当于从自定义模式切换为普通宫格或者列表展示模式
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 应用id
        /// </summary>
        public int agentid { get; set; }
        /// <summary>
        /// 若type指定为 "keydata"，且需要设置企业级别默认数据，则需要设置关键数据型模版数据,数据结构参考“关键数据型”
        /// </summary>
        public WorkBenchKeyDataModel keydata { get; set; }
        /// <summary>
        /// 若type指定为 "image"，且需要设置企业级别默认数据，则需要设置图片型模版数据,数据结构参考“图片型”
        /// </summary>
        public WorkBenchImageModel image { get; set; }
        /// <summary>
        /// 	若type指定为 "list"，且需要设置企业级别默认数据，则需要设置列表型模版数据,数据结构参考“列表型”
        /// </summary>
        public WorkBenchListModel list { get; set; }
        /// <summary>
        /// 若type指定为 "webview"，且需要设置企业级别默认数据，则需要设置webview型模版数据,数据结构参考“webview型”
        /// </summary>
        public WorkBenchWebViewModel webview { get; set; }
        /// <summary>
        /// 是否覆盖用户工作台的数据。设置为true的时候，会覆盖企业所有用户当前设置的数据。若设置为false,则不会覆盖用户当前设置的所有数据。默认为false
        /// </summary>
        public bool replace_user_data { get; set; }
    }
}
