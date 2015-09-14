/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：RegisterData.cs
    文件功能描述：申请开通功能数据
    
    
    创建标识：Senparc - 20150914
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 申请开通功能数据
    /// </summary>
    public class RegisterData
    {
        public string zhang_san { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string industry_id { get; set; }
        public string[] qualification_cert_urls { get; set; }
        public string apply_reason { get; set; }
    }
}