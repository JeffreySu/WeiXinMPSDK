/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：BatchgetCardMmerchantJsonResult.cs
    文件功能描述：拉取子商户列表的返回结果
    
    
    创建标识：Senparc - 20160520
    
    修改标识：Senparc - 20160520
    修改描述：整理接口
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    public class BatchGetCardMerchantJsonResult : WxJsonResult 
    {
        /// <summary>
        /// 
        /// </summary>
        public List<GetCardMerchantJsonResult> list { get; set; }

        /// <summary>
        /// 获取子商户列表，注意最开始时为空。每次拉取20个子商户，下次拉取时填入返回数据中该字段的值，该值无实际意义。
        /// </summary>
        public string next_get { get; set; }
    }
}
