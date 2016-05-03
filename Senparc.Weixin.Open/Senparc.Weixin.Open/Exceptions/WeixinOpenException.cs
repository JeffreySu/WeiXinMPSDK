/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
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
    /// <summary>
    /// 第三方平台异常
    /// </summary>
    public class WeixinOpenException : WeixinException
    {
        /// <summary>
        /// ComponentBag
        /// </summary>
        public ComponentBag ComponentBag { get; set; }

        public WeixinOpenException(string message, ComponentBag componentBag = null, Exception inner=null)
            : base(message, inner)
        {
            ComponentBag = ComponentBag;
        }
    }
}
