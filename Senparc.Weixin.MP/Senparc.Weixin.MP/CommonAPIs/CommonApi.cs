using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.HttpUtility;

namespace Senparc.Weixin.MP.CommonAPIs
{
    /// <summary>
    /// 通用接口
    /// 通用接口用于和微信服务器通讯，一般不涉及自有网站服务器的通讯
    /// 见 http://mp.weixin.qq.com/wiki/index.php?title=%E9%80%9A%E7%94%A8%E6%8E%A5%E5%8F%A3%E6%96%87%E6%A1%A3
    /// </summary>
    public class CommonApi
    {
        /// <summary>
        /// 获取凭证
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static AccessTokenResult GetToken(string appid, string secret, string grant_type = "client_credential")
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}",
                                    grant_type, appid, secret);

            AccessTokenResult result = Get.GetJson<AccessTokenResult>(url);
            return result;
        }

        public static object GetUserInfo(string accessToken,string openId)
        {

        }
    }
}
