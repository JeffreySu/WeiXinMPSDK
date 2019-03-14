/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：WeixinOpenException.cs
    文件功能描述：微信开放平台异常处理类
    
    
    创建标识：Senparc - 20151004

    修改标识：Senparc - 20160808
    修改描述：v2.2.0 将Container统一移到Containers命名空间下

----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Open.CommonAPIs;
using Senparc.Weixin.Open.ComponentAPIs;
using Senparc.Weixin.Open.Containers;

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
