/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：WeixinException.cs
    文件功能描述：微信菜单异常处理类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Exceptions
{
    public class WeixinMenuException : WeixinException
    {
        public WeixinMenuException(string message)
            : base(message, null)
        {
        }

        public WeixinMenuException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
