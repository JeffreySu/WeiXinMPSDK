using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.HttpUtility
{
    public static class Post
    {
        /// <summary>
        /// 发起Post请求
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="fileName">要发送的文件名，如果不需要上传则设为null</param>
        /// <returns></returns>
        public static T GetJson<T>(string url, CookieContainer cookieContainer = null, string fileName = null)
        {
            //读取文件
            FileStream fileStream = null;
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                fileStream = new FileStream(fileName, FileMode.Open);
            }

            string returnText = HttpUtility.RequestUtility.HttpPost(url, cookieContainer, fileStream);

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
    }
}
