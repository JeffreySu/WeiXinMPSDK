using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.MP.QYAPIs
{
    //官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E4%B8%BB%E5%8A%A8%E8%B0%83%E7%94%A8

    public static class AccessToken
    {
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="corpid">企业Id </param>
        /// <param name="corpsecret">管理组的凭证密钥 </param>
        /// <returns></returns>
        public static AccessTokenResult GetAccessToken(string corpid, string corpsecret)
        {
            var url =
                string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}",
                                corpid, corpsecret);

            return CommonJsonSend.Send<AccessTokenResult>(null, url, null, CommonJsonSendType.GET);
        }
    }
}
