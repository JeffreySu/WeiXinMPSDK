/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：Post.cs
    文件功能描述：Post
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20130312
    修改描述：开放代理请求超时时间
 
    修改标识：zhanghao-kooboo - 20130316
    修改描述：增加
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.HttpUtility
{
    public static class Post
    {
        #region 同步方法

        /// <summary>
        /// 获取Post结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnText"></param>
        /// <returns></returns>
        public static T GetResult<T>(string returnText)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            if (returnText.Contains("errcode"))
            {
                //可能发生错误
                WxJsonResult errorResult = js.Deserialize<WxJsonResult>(returnText);
                if (errorResult.errcode != ReturnCode.请求成功)
                {
                    //发生错误
                    throw new ErrorJsonResultException(
                        string.Format("微信Post请求发生错误！错误代码：{0}，说明：{1}",
                                      (int)errorResult.errcode,
                                      errorResult.errmsg),
                        null, errorResult);
                }
            }

            T result = js.Deserialize<T>(returnText);
            return result;
        }

        /// <summary>
        /// 发起Post请求
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static T PostFileGetJson<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> fileDictionary = null, Encoding encoding = null, int timeOut = Config.TIME_OUT)
        {
            string returnText = HttpUtility.RequestUtility.HttpPost(url, cookieContainer, null, fileDictionary, null, encoding, timeOut: timeOut);
            var result = GetResult<T>(returnText);
            return result;
        }

        /// <summary>
        /// 发起Post请求
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="fileStream">文件流</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static T PostGetJson<T>(string url, CookieContainer cookieContainer = null, Stream fileStream = null, Encoding encoding = null, int timeOut = Config.TIME_OUT)
        {
            string returnText = HttpUtility.RequestUtility.HttpPost(url, cookieContainer, fileStream, null, null, encoding, timeOut: timeOut);
            var result = GetResult<T>(returnText);
            return result;
        }

        public static T PostGetJson<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = Config.TIME_OUT)
        {
            string returnText = HttpUtility.RequestUtility.HttpPost(url, cookieContainer, formData, encoding, timeOut: timeOut);
            var result = GetResult<T>(returnText);
            return result;
        }

        #endregion

        //#region 异步方法

        ///// <summary>
        ///// PostFileGetJson的异步版本
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="url"></param>
        ///// <param name="cookieContainer"></param>
        ///// <param name="fileDictionary"></param>
        ///// <param name="encoding"></param>
        ///// <returns></returns>
        //public static async Task<T> PostFileGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> fileDictionary = null, Encoding encoding = null)
        //{
        //    string returnText = await HttpUtility.RequestUtility.HttpPostAsync(url, cookieContainer, null, fileDictionary, null, encoding);
        //    var result = GetResult<T>(returnText);
        //    return result;
        //}

        ///// <summary>
        ///// PostGetJson的异步版本
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="url"></param>
        ///// <param name="cookieContainer"></param>
        ///// <param name="fileStream"></param>
        ///// <param name="encoding"></param>
        ///// <returns></returns>
        //public static async Task<T> PostGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Stream fileStream = null, Encoding encoding = null)
        //{
        //    string returnText = await HttpUtility.RequestUtility.HttpPostAsync(url, cookieContainer, fileStream, null, null, encoding);
        //    var result = GetResult<T>(returnText);
        //    return result;
        //}

        ///// <summary>
        ///// PostGetJson的异步版本
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="url"></param>
        ///// <param name="cookieContainer"></param>
        ///// <param name="formData"></param>
        ///// <param name="encoding"></param>
        ///// <returns></returns>
        //public static async Task<T> PostGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, Encoding encoding = null)
        //{
        //    string returnText = await HttpUtility.RequestUtility.HttpPostAsync(url, cookieContainer, formData, encoding);
        //    var result = GetResult<T>(returnText);
        //    return result;
        //}

        //#endregion
    }
}
