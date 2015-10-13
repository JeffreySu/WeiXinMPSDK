/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：WeixinOpenException.cs
    文件功能描述：微信开放平台异常处理类
    
    
    创建标识：Senparc - 20151004

----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.CommonAPIs;
using Senparc.Weixin.Open.ComponentAPIs;

namespace Senparc.Weixin.Open.Exceptions
{
    public class WeixinOpenException : WeixinException
    {
        public ComponentBag ComponentBag { get; set; }

        public WeixinOpenException(string message, ComponentBag componentBag = null, Exception inner=null)
            : base(message, inner)
        {
            ComponentBag = ComponentBag;
        }
    }
}
