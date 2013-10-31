using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.HttpUtility;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    //接口详见：http://mp.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96%E7%94%A8%E6%88%B7%E5%9F%BA%E6%9C%AC%E4%BF%A1%E6%81%AF
    public static class User
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static UserInfoJson Info(string accessToken, string openId)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}",
                accessToken, openId);
            return HttpUtility.Get.GetJson<UserInfoJson>(url);

            //错误时微信会返回错误码等信息，JSON数据包示例如下（该示例为AppID无效错误）:
            //{"errcode":40013,"errmsg":"invalid appid"}
        }

        /// <summary>
        /// 获取关注着OpenId信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public static OpenIdResultJson Get(string accessToken, string nextOpenId)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}",
                accessToken);
            if (string.IsNullOrEmpty(nextOpenId))
            {
                url += "&next_openid=" + nextOpenId;
            }
            return HttpUtility.Get.GetJson<OpenIdResultJson>(url);
        }
    }
}
