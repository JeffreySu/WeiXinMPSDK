using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Base
{
    /// <summary>
    /// 提交批量导入上下游联系人任务 响应参数
    /// </summary>
    public class ImportChainContactResult : WorkJsonResult
    {
        /// <summary>
        /// 异步任务id，最大长度为64字节
        /// </summary>
        public string jobid { get; set; }
    }
}
