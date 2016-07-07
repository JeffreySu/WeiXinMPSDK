using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.CustomService
{
    /// <summary>
    /// 聊天记录结果
    /// </summary>
    public class GetRecordResult : WxJsonResult
    {
        /// <summary>
        /// 官方文档暂没有说明
        /// </summary>
        public int retcode { get; set; }
        public List<RecordJson> recordlist { get; set; }
    }
}
