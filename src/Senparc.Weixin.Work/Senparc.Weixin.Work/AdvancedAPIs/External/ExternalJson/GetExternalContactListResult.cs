/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetExternalContactInfoBatchResult.cs
    文件功能描述：批量获取客户详情 返回结果
    
    
    创建标识：gokeiyou - 20201013
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 获取客户列表返回实体
    /// </summary>
    public class GetExternalContactListResult : WorkJsonResult
    {
        /// <summary>
        /// 外部联系人的userid列表
        /// </summary>
        public List<string> external_userid { get; set; }
    }
}
