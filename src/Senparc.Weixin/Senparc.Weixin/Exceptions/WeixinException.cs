/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：WeixinException.cs
    文件功能描述：微信自定义异常基类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20161225
    修改描述：v4.9.7 完善日志记录

    修改标识：Senparc - 20170101
    修改描述：v4.9.9 优化WeixinTrace
    
    修改标识：Senparc - 20170102
    修改描述：v4.9.10 添加AccessTokenOrAppId属性

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Exceptions
{
    /// <summary>
    /// 微信自定义异常基类
    /// </summary>
    public class WeixinException : ApplicationException
    {
        /// <summary>
        /// 当前正在请求的公众号AccessToken或AppId
        /// </summary>
        public string AccessTokenOrAppId { get; set; }

        /// <summary>
        /// WeixinException
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="logged">是否已经使用WeixinTrace记录日志，如果没有，WeixinException会进行概要记录</param>
        public WeixinException(string message, bool logged = false)
            : this(message, null, logged)
        {
        }

        /// <summary>
        /// WeixinException
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">内部异常信息</param>
        /// <param name="logged">是否已经使用WeixinTrace记录日志，如果没有，WeixinException会进行概要记录</param>
        public WeixinException(string message, Exception inner, bool logged = false)
            : base(message, inner)
        {
            if (!logged)
            {
                //WeixinTrace.Log(string.Format("WeixinException（{0}）：{1}", this.GetType().Name, message));
                WeixinTrace.WeixinExceptionLog(this);
            }
        }
    }
}
