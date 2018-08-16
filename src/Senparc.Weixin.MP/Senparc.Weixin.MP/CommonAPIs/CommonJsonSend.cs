﻿#region Apache License Version 2.0
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
    
    文件名：CommonJsonSend.cs
    文件功能描述：向需要AccessToken的API发送消息的公共方法
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.CommonAPIs
{
    /// <summary>
    /// 通用 JSON 数据获取
    /// </summary>
    public static class CommonJsonSend
    {
        #region 同步方法

        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat"></param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="sendType"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="checkValidationResult"></param>
        /// <param name="jsonSetting"></param>
        /// <returns></returns>
        //[Obsolete("此方法已过期，请使用Senparc.Weixin.CommonAPIs.CommonJsonSend.Send()方法")]
        public static WxJsonResult Send(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST,
            int timeOut = Config.TIME_OUT, bool checkValidationResult = false, JsonSetting jsonSetting = null/*, int retry40001ErrorTimes = 0*/)
        {
            WxJsonResult result = null;
            try
            {
                result = Senparc.Weixin.CommonAPIs.CommonJsonSend.Send(accessToken, urlFormat, data, sendType, timeOut,
                    checkValidationResult, jsonSetting);
            }
            //catch (ErrorJsonResultException ex)
            //{
            //    if (ex.JsonResult.errcode == ReturnCode.获取access_token时AppSecret错误或者access_token无效 /*40001*/)
            //    {
            //        if (retry40001ErrorTimes == 0)
            //        {
            //            result = Send(accessToken, urlFormat, data, sendType, timeOut, checkValidationResult,
            //                jsonSetting, retry40001ErrorTimes + 1);
            //        }
            //        else if (retry40001ErrorTimes == 1)
            //        {
                        
            //        }

            //    }
            //}
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat">用accessToken参数填充{0}</param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="sendType"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="checkValidationResult"></param>
        /// <param name="jsonSetting"></param>
        /// <returns></returns>
        //[Obsolete("此方法已过期，请使用Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<T>()方法")]
        public static T Send<T>(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = Config.TIME_OUT, bool checkValidationResult = false, JsonSetting jsonSetting = null)
        {
            return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<T>(accessToken, urlFormat, data, sendType, timeOut, checkValidationResult, jsonSetting);
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat"></param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [Obsolete("此方法已过期，请使用Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync()方法")]
        public static async Task<WxJsonResult> SendAsync(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = Config.TIME_OUT)
        {
            return await SendAsync<WxJsonResult>(accessToken, urlFormat, data, sendType, timeOut);
        }

        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat"></param>
        /// <param name="data">如果是Get方式，可以为null。在POST方式中将被转为JSON字符串提交</param>
        /// <param name="sendType">发送类型，POST或GET，默认为POST</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <param name="jsonSetting">JSON字符串生成设置</param>
        /// <returns></returns>
        [Obsolete("此方法已过期，请使用Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<T>()方法")]
        public static async Task<T> SendAsync<T>(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = Config.TIME_OUT, bool checkValidationResult = false,
            JsonSetting jsonSetting = null
            )
        {
            var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken.AsUrlData());

            switch (sendType)
            {
                case CommonJsonSendType.GET:
                    return await Get.GetJsonAsync<T>(url);
                case CommonJsonSendType.POST:
                    var jsonString = SerializerHelper.GetJsonString(data, jsonSetting);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        var bytes = Encoding.UTF8.GetBytes(jsonString);
                        await ms.WriteAsync(bytes, 0, bytes.Length);
                        ms.Seek(0, SeekOrigin.Begin);

                        return await Post.PostGetJsonAsync<T>(url, null, ms, timeOut: timeOut, checkValidationResult: checkValidationResult);
                    }
                default:
                    throw new ArgumentOutOfRangeException("sendType");
            }
        }

        #endregion
#endif
    }
}