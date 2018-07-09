/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：CommonJsonSend.cs
    文件功能描述：向需要AccessToken的API发送消息的公共方法
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Text;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.Open.CommonAPIs
{
    //public enum CommonJsonSendType
    //{
    //    GET,
    //    POST
    //}

    public static class CommonJsonSend
    {
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
#if NET45
        [Obsolete("此方法已过期，请使用Senparc.Weixin.CommonAPIs.CommonJsonSend.Send()方法")]
        public static WxJsonResult Send(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, 
            int timeOut = Config.TIME_OUT, bool checkValidationResult = false, JsonSetting jsonSetting = null)
        {
            return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send(accessToken, urlFormat, data, sendType, timeOut, checkValidationResult, jsonSetting);
        }
#else
        [Obsolete("此方法已过期，请使用Senparc.Weixin.CommonAPIs.CommonJsonSend.Send()方法")]
        public static WxJsonResult Send(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = Config.TIME_OUT, bool checkValidationResult = false/*, JsonSetting jsonSetting = null*/)
        {
            return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send(accessToken, urlFormat, data, sendType, timeOut, checkValidationResult/*, jsonSetting*/);
        }
#endif

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
#if NET45
        [Obsolete("此方法已过期，请使用Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<T>()方法")]
        public static T Send<T>(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST,
            int timeOut = Config.TIME_OUT, bool checkValidationResult = false, JsonSetting jsonSetting = null)
        {
            return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<T>(accessToken, urlFormat, data, sendType, timeOut, checkValidationResult, jsonSetting);
        }
#else
        [Obsolete("此方法已过期，请使用Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<T>()方法")]
        public static T Send<T>(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST,
            int timeOut = Config.TIME_OUT, bool checkValidationResult = false/*, JsonSetting jsonSetting = null*/)
        {
            return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<T>(accessToken, urlFormat, data, sendType, timeOut, checkValidationResult/*, jsonSetting*/);
        }
#endif
    }
}