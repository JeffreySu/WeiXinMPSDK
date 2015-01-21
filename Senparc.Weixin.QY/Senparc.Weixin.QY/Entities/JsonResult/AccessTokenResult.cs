﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// GetToken请求后的JSON返回格式
    /// </summary>
    public class AccessTokenResult:WxJsonResult
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string access_token { get; set; }
    }
}
