/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SetPrizeBucketResultJson.cs
    文件功能描述：录入红包消息的返回结果
    
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 录入红包消息的返回结果
    /// </summary>
    public class SetPrizeBucketResultJson : WxJsonResult 
    {
        public List<SetPrizeBucket_RepeatTicketList> repeat_ticket_list { get; set; }

       
    }
    public class SetPrizeBucket_RepeatTicketList
    {
        /// <summary>
        /// ticket解析失败，可能有错别字符或不完整 
        /// </summary>
        public string ticket { get; set; }
    }
}
