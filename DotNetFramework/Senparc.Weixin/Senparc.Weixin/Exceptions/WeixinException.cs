/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：WeixinException.cs
    文件功能描述：微信自定义异常基类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Exceptions
{
    /// <summary>
    /// 微信自定义异常基类
    /// </summary>
    public class WeixinException : ApplicationException
    {
        public WeixinException(string message)
            : base(message, null)
        {
        }

        public WeixinException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
