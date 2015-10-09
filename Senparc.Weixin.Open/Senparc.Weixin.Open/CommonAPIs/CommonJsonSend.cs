﻿/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：CommonJsonSend.cs
    文件功能描述：向需要AccessToken的API发送消息的公共方法
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Text;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.Open.CommonAPIs
{
    public enum CommonJsonSendType
    {
        GET,
        POST
    }

    public static class CommonJsonSend
    {
        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat"></param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult Send(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = Config.TIME_OUT)
        {
            return Send<WxJsonResult>(accessToken, urlFormat, data, sendType, timeOut);
        }

        /// <summary>
        /// 向需要AccessToken的API发送消息的公共方法
        /// </summary>
        /// <param name="accessToken">这里的AccessToken是通用接口的AccessToken，非OAuth的。如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
        /// <param name="urlFormat">用accessToken参数填充{0}</param>
        /// <param name="data">如果是Get方式，可以为null</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static T Send<T>(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST, int timeOut = Config.TIME_OUT)
        {
            //TODO:此方法可以设定一个日志记录开关

            var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken);
            switch (sendType)
            {
                case CommonJsonSendType.GET:
                    return Get.GetJson<T>(url);
                case CommonJsonSendType.POST:
                    SerializerHelper serializerHelper = new SerializerHelper();
                    var jsonString = serializerHelper.GetJsonString(data);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        var bytes = Encoding.UTF8.GetBytes(jsonString);
                        ms.Write(bytes, 0, bytes.Length);
                        ms.Seek(0, SeekOrigin.Begin);

                        return Post.PostGetJson<T>(url, null, ms, timeOut: timeOut);
                    }
                default:
                    throw new ArgumentOutOfRangeException("sendType");
            }
        }
    }
}