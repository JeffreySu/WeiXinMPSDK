using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    public class AddMessageTemplateResult : WorkJsonResult
    {
        /// <summary>
        /// 无效或无法发送的external_userid列表
        /// </summary>
        public string[] fail_list { get; set; }
        /// <summary>
        /// 企业群发消息的id，可用于<see href="https://developer.work.weixin.qq.com/document/path/92135#25429/%E8%8E%B7%E5%8F%96%E4%BC%81%E4%B8%9A%E7%BE%A4%E5%8F%91%E6%88%90%E5%91%98%E6%89%A7%E8%A1%8C%E7%BB%93%E6%9E%9C">获取群发消息发送结果</see>
        public string msgid { get; set; }
    }

}
