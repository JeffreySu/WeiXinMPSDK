#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
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


using Senparc.CO2NET.Exceptions;
using System;

namespace Senparc.Weixin.Exceptions
{
    /// <summary>
    /// 微信自定义异常基类
    /// </summary>
    public class WeixinException : BaseException
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
            : base(message, inner, true/* 标记为日志已记录 */)
        {
            if (!logged)
            {
                //WeixinTrace.Log(string.Format("WeixinException（{0}）：{1}", this.GetType().Name, message));

                WeixinTrace.WeixinExceptionLog(this);
            }
        }
    }
}
