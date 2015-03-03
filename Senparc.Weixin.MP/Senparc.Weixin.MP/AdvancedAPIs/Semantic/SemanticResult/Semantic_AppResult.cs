using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 应用服务（app）
    /// </summary>
    public class Semantic_AppResult : BaseSemanticResultJson
    {
        public Semantic_App semantic { get; set; }
    }

    public class Semantic_App : BaseSemanticIntent
    {
        public Semantic_Details_App details { get; set; }
    }

    public class Semantic_Details_App
    {
        /// <summary>
        /// app名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// app类别
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 排序方式：0（按质量从高到低），1（按时间从新到旧）
        /// </summary>
        public string sort { get; set; }
        /// <summary>
        /// 查看的类型：install（已安装），buy（已购买），update（可更新），latest（最近运行的），home（主页）
        /// </summary>
        public string type { get; set; }
    }
}