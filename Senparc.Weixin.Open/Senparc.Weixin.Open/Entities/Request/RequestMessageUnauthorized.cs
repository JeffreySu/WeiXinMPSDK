/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：RequestMessageUnauthorized.cs
    文件功能描述：推送取消授权通知
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Open
{
    public class RequestMessageUnauthorized : RequestMessageBase
    {
        public string AuthorizerAppid { get; set; }
    }
}
