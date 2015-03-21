﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：ErrorJsonResultException.cs
    文件功能描述：JSON返回错误代码（比如token_access相关操作中使用）。
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Exceptions
{
    /// <summary>
    /// JSON返回错误代码（比如token_access相关操作中使用）。
    /// </summary>
    public class ErrorJsonResultException : WeixinException
    {
        public WxJsonResult JsonResult { get; set; }
        public ErrorJsonResultException(string message, Exception inner, WxJsonResult jsonResult)
            : base(message, inner)
        {
            JsonResult = jsonResult;
        }
    }
}
