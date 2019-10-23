#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
Copyright(C) 2017 Senparc

文件名：ErrorJsonResultException.cs
文件功能描述：JSON返回错误代码（比如token_access相关操作中使用）。


创建标识：Senparc - 20150211

修改标识：Senparc - 20150303
修改描述：整理接口

修改标识：Senparc - 20161225
修改描述：v4.9.7 完善日志记录
----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Exceptions
{
    /// <summary>
    /// JSON返回错误代码异常（比如access_token相关操作中使用）
    /// </summary>
    public class ErrorJsonResultException : WeixinException
    {
        /// <summary>
        /// JsonResult
        /// </summary>
        public WxJsonResult JsonResult { get; set; }
        /// <summary>
        /// 接口 URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// ErrorJsonResultException
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">内部异常</param>
        /// <param name="jsonResult">WxJsonResult</param>
        /// <param name="url">API地址</param>
        public ErrorJsonResultException(string message, Exception inner, WxJsonResult jsonResult, string url = null)
            : base(message, inner, true)
        {
            JsonResult = jsonResult;
            Url = url;

            WeixinTrace.ErrorJsonResultExceptionLog(this);
        }
    }
}
