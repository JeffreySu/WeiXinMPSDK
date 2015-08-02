using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 聊天记录结果
    /// </summary>
    public class GetRecordResult : WxJsonResult
    {
        public List<RecordJson> recordlist { get; set; }
    }
}
