using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 
    /// </summary>
    public class AddMsgTemplateResult : WorkJsonResult
    {
        public List<string> fail_list { get; set; }
        public string msgid { get; set; }
    }
}