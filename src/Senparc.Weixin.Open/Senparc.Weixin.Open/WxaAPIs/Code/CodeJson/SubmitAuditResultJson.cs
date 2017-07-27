/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：SubmitAuditResultJson.cs
    文件功能描述：审核ID
    
    
    创建标识：Senparc - 20170726


----------------------------------------------------------------*/


using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class SubmitAuditResultJson : WxJsonResult
    {
        public int auditid { get; set; }
    }
}
