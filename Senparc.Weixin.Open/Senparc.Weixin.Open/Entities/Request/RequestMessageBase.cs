/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：RequestMessageBase.cs
    文件功能描述：第三方应用授权回调消息服务
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Open
{
    public class RequestMessageBase
    {
       public string AppId { get; set; }
       public DateTime CreateTime { get; set; }
       public InfoType InfoType { get; set; }
    }
}
