using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.HttpUtility;

namespace Senparc.Weixin.MP.CommonAPIs
{
    public enum CommonJsonSendType
    {
        GET,
        POST
    }
}

public static class CommonJsonSend
{
    /// <summary>
    /// 向需要AccessToken的API发送消息的公共方法
    /// </summary>
    /// <param name="accessToken">如果不需要，可以为null，此时urlFormat不要提供{0}参数</param>
    /// <param name="urlFormat"></param>
    /// <param name="data">如果是Get方式，可以为null</param>
    /// <returns></returns>
    public static WxJsonResult Send(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST)
    {
        return Send<WxJsonResult>(accessToken, urlFormat, data, sendType);
    }

    public static T Send<T>(string accessToken, string urlFormat, object data, CommonJsonSendType sendType = CommonJsonSendType.POST)
    {
        var url = string.IsNullOrEmpty(accessToken) ? urlFormat : string.Format(urlFormat, accessToken);
        switch (sendType)
        {
            case CommonJsonSendType.GET:
                return Get.GetJson<T>(url);
            case CommonJsonSendType.POST:
                JavaScriptSerializer js = new JavaScriptSerializer();
                var jsonString = js.Serialize(data);
                using (MemoryStream ms = new MemoryStream())
                {
                    var bytes = Encoding.UTF8.GetBytes(jsonString);
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Seek(0, SeekOrigin.Begin);

                    return Post.PostGetJson<T>(url, null, ms);
                }
            default:
                throw new ArgumentOutOfRangeException("sendType");
        }
    }
}
