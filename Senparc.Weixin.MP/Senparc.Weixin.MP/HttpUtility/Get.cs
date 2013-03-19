using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.HttpUtility
{
    public static class Get
    {
        public static T GetJson<T>(string url)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string returnText = HttpUtility.RequestUtility.DownloadString(url);

            if (returnText.Contains("errcode"))
            {
                //可能发生错误
                WxJsonResult errorResult = js.Deserialize<WxJsonResult>(returnText);
                if (errorResult.errcode != ReturnCode.请求成功)
                {
                    //发生错误
                    throw new ErrorJsonResultException(
                        string.Format("微信请求发生错误！错误代码：{0}，说明：{1}",
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
