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
    Copyright (C) 2019 Senparc

    文件名：CommonJsonSend.cs
    文件功能描述：通过CommonJsonSend中的方法调用接口


    创建标识：Senparc - 20151012
           
    修改标识：Senparc - 20170606
    修改描述：v14.4.11 完善CommonJsonSend.SendAsync()方法参数
  
    修改标识：Senparc - 20190129
    修改描述：v6.3.8 修复 CommonJsonSend.Send() 方法中的异常请求结果自动抛出 

    修改标识：Senparc - 20190602
    修改描述：v6.4.8 根据 Config.ThrownWhenJsonResultFaild 优化 getFailAction 和 postFailAction 抛出异常逻辑

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
using Senparc.CO2NET.HttpUtility;

namespace Senparc.Weixin.CommonAPIs
{
    /// <summary>
    /// 所有高级接口共用的向微信服务器发送 API 请求的方法
    /// </summary>
    public static class CommonJsonSend
    {
        #region 公共私有方法

        /// <summary>
        /// 设定条件，当API结果没有返回成功信息时抛出异常
        /// </summary>
        static Action<string, string> getFailAction = (apiUrl, returnText) =>
         {
             WeixinTrace.SendApiLog(apiUrl, returnText);

             if (returnText.Contains("errcode"))
             {
                 //可能发生错误
                 WxJsonResult errorResult = SerializerHelper.GetObject<WxJsonResult>(returnText);

                 ErrorJsonResultException ex = null;
                 if (errorResult.errcode != ReturnCode.请求成功)
                 {
                     //发生错误，记录异常
                     ex = new ErrorJsonResultException(
                          string.Format("微信 GET 请求发生错误！错误代码：{0}，说明：{1}",
                                          (int)errorResult.errcode, errorResult.errmsg), null, errorResult, apiUrl);
                 }

                 if (Config.ThrownWhenJsonResultFaild && ex != null)
                 {
                     throw ex;//抛出异常
                 }
             }
         };

        /// <summary>
        /// 设定条件，当API结果没有返回成功信息时抛出异常
        /// </summary>
        static Action<string, string> postFailAction = (apiUrl, returnText) =>
        {
             if (returnText.Contains("errcode"))
             {
                 //可能发生错误
                 WxJsonResult errorResult = SerializerHelper.GetObject<WxJsonResult>(returnText);
                 ErrorJsonResultException ex = null;
                 if (errorResult.errcode != ReturnCode.请求成功)
                 {
                     //发生错误，记录异常
                     throw new ErrorJsonResultException(
                          string.Format("微信 POST 请求发生错误！错误代码：{0}，说明：{1}",
                                        (int)errorResult.errcode,
                                        errorResult.errmsg),
                          null, errorResult, apiUrl);
                 }

                 if (Config.ThrownWhenJsonResultFaild && ex != null)
                 {
                     throw ex;//抛出异常
                 }
             }
        };

        #endregion

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
        public static WxJsonResult Send(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = CO2NET.Config.TIME_OUT, bool checkValidationResult = false, JsonSetting jsonSetting = null)
        {
            return Send<WxJsonResult>(accessToken, urlFormat, data, sendType, timeOut, checkValidationResult, jsonSetting);
        }

        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat">用accessToken参数填充{0}</param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="sendType"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <param name="jsonSetting"></param>
        /// <returns></returns>
        public static T Send<T>(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST,
            int timeOut = CO2NET.Config.TIME_OUT, bool checkValidationResult = false, JsonSetting jsonSetting = null)
        {
            //TODO:此方法可以设定一个日志记录开关

            try
            {
                var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken.AsUrlData());

                switch (sendType)
                {
                    case CommonJsonSendType.GET:
                        return Get.GetJson<T>(url, afterReturnText: getFailAction);
                    case CommonJsonSendType.POST:
                        var jsonString = SerializerHelper.GetJsonString(data, jsonSetting);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            var bytes = Encoding.UTF8.GetBytes(jsonString);
                            ms.Write(bytes, 0, bytes.Length);
                            ms.Seek(0, SeekOrigin.Begin);

                            WeixinTrace.SendApiPostDataLog(url, jsonString);//记录Post的Json数据

                            //PostGetJson方法中将使用WeixinTrace记录结果
                            return Post.PostGetJson<T>(url, null, ms,
                                timeOut: timeOut,
                                afterReturnText: postFailAction,
                                checkValidationResult: checkValidationResult);
                        }

                    //TODO:对于特定的错误类型自动进行一次重试，如40001（目前的问题是同样40001会出现在不同的情况下面）
                    default:
                        throw new ArgumentOutOfRangeException("sendType");
                }
            }
            catch (ErrorJsonResultException ex)
            {
                ex.Url = urlFormat;
                throw;
            }
        }

        #endregion


        #region 异步方法

        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat"></param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="sendType"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <param name="jsonSetting"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SendAsync(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = CO2NET.Config.TIME_OUT, bool checkValidationResult = false, JsonSetting jsonSetting = null)
        {
            return await SendAsync<WxJsonResult>(accessToken, urlFormat, data, sendType, timeOut, checkValidationResult, jsonSetting).ConfigureAwait(false);
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
        public static async Task<T> SendAsync<T>(string accessToken, string urlFormat, object data,
            CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = CO2NET.Config.TIME_OUT,
            bool checkValidationResult = false, JsonSetting jsonSetting = null)
        {
            try
            {
                var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken.AsUrlData());

                switch (sendType)
                {
                    case CommonJsonSendType.GET:
                        return await Get.GetJsonAsync<T>(url, afterReturnText: getFailAction).ConfigureAwait(false);
                    case CommonJsonSendType.POST:
                        var jsonString = SerializerHelper.GetJsonString(data, jsonSetting);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            var bytes = Encoding.UTF8.GetBytes(jsonString);
                            await ms.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
                            ms.Seek(0, SeekOrigin.Begin);

                            WeixinTrace.SendApiPostDataLog(url, jsonString);//记录Post的Json数据

                            //PostGetJson方法中将使用WeixinTrace记录结果
                            return await Post.PostGetJsonAsync<T>(url, null, ms,
                                timeOut: timeOut,
                                afterReturnText: postFailAction,
                                checkValidationResult: checkValidationResult).ConfigureAwait(false);
                        }
                    default:
                        throw new ArgumentOutOfRangeException("sendType");
                }
            }
            catch (ErrorJsonResultException ex)
            {
                ex.Url = urlFormat;
                throw;
            }
        }

        #endregion
    }
}
