/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：WeixinOpenException.cs
    文件功能描述：微信开放平台异常处理类
    
    
    创建标识：Senparc - 20151004

----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Containers;

namespace Senparc.Weixin.Work.Exceptions
{
    /// <summary>
    /// 企业微信异常
    /// </summary>
    public class WeixinWorkException : WeixinException
    {
        public AccessTokenBag AccessTokenBag { get; set; }

        public WeixinWorkException(string message, AccessTokenBag accessTokenBag = null, Exception inner=null)
            : base(message, inner)
        {
            AccessTokenBag = accessTokenBag;
        }
    }
}
