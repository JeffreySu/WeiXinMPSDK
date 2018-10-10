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

    文件名：Get.cs
    文件功能描述：Get


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：zeje - 20160422
    修改描述：v4.5.19 为GetJson方法添加maxJsonLength参数

    修改标识：zeje - 20170305
    修改描述：MP v14.3.132 添加Get.DownloadAsync(string url, string dir)方法

    修改标识：Senparc - 20170409
    修改描述：v4.11.9 修改Download方法

    修改标识：Senparc - 20171101
    修改描述：v4.18.1 修改Get.Download()方法

    修改标识：Senparc - 20180114
    修改描述：v4.18.13  修改 HttpUtility.Get.Download() 方法，
                        根据 Content-Disposition 中的文件名储存文件

    修改标识：Senparc - 20180407
    修改描述：v14.10.13 优化 Get.Download() 方法，完善对 FileName 的判断
  
    修改标识：Senparc - 20180928
    修改描述：将 CO2NET 已经移植的方法标记为过期
----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
#if !NET35 && !NET40
using System.Net.Http;
using System.Threading.Tasks;
#endif
using System.Text;
#if NET35 || NET40 || NET45
using System.Web.Script.Serialization;
#endif
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using System.Text.RegularExpressions;

namespace Senparc.Weixin.HttpUtility
{
    /// <summary>
    /// Get 请求处理
    /// </summary>
    public static class Get
    {
        /// <summary>
        /// 获取随机文件名
        /// </summary>
        /// <returns></returns>
        private static string GetRandomFileName()
        {
            return DateTime.Now.ToString("yyyyMMdd-HHmmss") + Guid.NewGuid().ToString("n").Substring(0, 6);
        }

        /// <summary>
        /// 获得JSON文本结果之后、序列化之前进行的操作
        /// </summary>
        internal static Action<string, string> AfterReturnText = (_url, returnText) =>
        {
            WeixinTrace.SendApiLog(_url, returnText);

            if (returnText.Contains("errcode"))
            {
                //可能发生错误
                WxJsonResult errorResult = CO2NET.Helpers.SerializerHelper.GetObject<WxJsonResult>(returnText);

                if (errorResult.errcode != ReturnCode.请求成功)
                {
                    //发生错误
                    throw new ErrorJsonResultException(
                         string.Format("微信请求发生错误！错误代码：{0}，说明：{1}",
                                         (int)errorResult.errcode, errorResult.errmsg), null, errorResult, _url);
                }
            }
        };


        #region 同步方法

        /// <summary>
        /// GET方式请求URL，并返回T类型
        /// </summary>
        /// <typeparam name="T">接收JSON的数据类型</typeparam>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <param name="maxJsonLength">允许最大JSON长度</param>
        /// <returns></returns>
        [Obsolete("请使用 CO2NET.HttpUtility.Get.GetJson<T>() 方法")]
        public static T GetJson<T>(string url, Encoding encoding = null)
        {
            var result = CO2NET.HttpUtility.Get.GetJson<T>(url, encoding, AfterReturnText);
            return result;
        }

        /// <summary>
        /// 从Url下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        [Obsolete("请使用 CO2NET.HttpUtility.Get.Download() 方法")]
        public static void Download(string url, Stream stream)
        {
            CO2NET.HttpUtility.Get.Download(url, stream);
        }

        //#if !NET35 && !NET40
        /// <summary>
        /// 从Url下载，并保存到指定目录
        /// </summary>
        /// <param name="url">需要下载文件的Url</param>
        /// <param name="filePathName">保存文件的路径，如果下载文件包含文件名，按照文件名储存，否则将分配Ticks随机文件名</param>
        /// <returns></returns>
        [Obsolete("请使用 CO2NET.HttpUtility.Get.Download() 方法")]
        public static string Download(string url, string filePathName, int timeOut = Config.TIME_OUT)
        {
            return CO2NET.HttpUtility.Get.Download(url, filePathName, timeOut);
        }
        //#endif
        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】异步GetJson
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ErrorJsonResultException"></exception>
        [Obsolete("请使用 CO2NET.HttpUtility.Get.GetJsonAsync<T>() 方法")]
        public static async Task<T> GetJsonAsync<T>(string url, Encoding encoding = null)
        {
            var result = await CO2NET.HttpUtility.Get.GetJsonAsync<T>(url, encoding, AfterReturnText);
            return result;
        }

        /// <summary>
        /// 【异步方法】异步从Url下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        [Obsolete("请使用 CO2NET.HttpUtility.Get.DownloadAsync() 方法")]
        public static async Task DownloadAsync(string url, Stream stream)
        {
            await CO2NET.HttpUtility.Get.DownloadAsync(url, stream);
        }

        /// <summary>
        /// 【异步方法】从Url下载，并保存到指定目录
        /// </summary>
        /// <param name="url">需要下载文件的Url</param>
        /// <param name="filePathName"></param>
        /// <returns></returns>
        [Obsolete("请使用 CO2NET.HttpUtility.Get.DownloadAsync() 方法")]
        public static async Task<string> DownloadAsync(string url, string filePathName, int timeOut = Config.TIME_OUT)
        {
            return await CO2NET.HttpUtility.Get.DownloadAsync(url, filePathName);//TODO：增加timeOut
        }
        #endregion
#endif

    }
}
