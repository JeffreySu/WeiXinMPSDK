using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.QY.Entities;

namespace Senparc.Weixin.QY.CommonAPIs
{
    /// <summary>
    /// 通用基础API
    /// </summary>
    public partial class CommonApi
    {
        public const string API_URL = "https://qyapi.weixin.qq.com/cgi-bin";

        /// <summary>
        /// 获取AccessToken
        /// API地址：http://qydev.weixin.qq.com/wiki/index.php?title=%E4%B8%BB%E5%8A%A8%E8%B0%83%E7%94%A8
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        public static AccessTokenResult GetToken(string corpId, string corpSecret)
        {
            #region 主动调用的频率限制
            /*
当你获取到AccessToken时，你的应用就可以成功调用企业号后台所提供的各种接口以管理或访问企业号后台的资源或给企业号成员发消息。

为了防止企业应用的程序错误而引发企业号服务器负载异常，默认情况下，每个企业号调用接口都有一定的频率限制，当超过此限制时，调用对应接口会收到相应错误码。

以下是当前默认的频率限制，企业号后台可能会根据运营情况调整此阈值：

基础频率
每企业调用单个cgi/api不可超过1000次/分，30000次/小时

每ip调用单个cgi/api不可超过2000次/分，60000次/小时

每ip获取AccessToken不可超过300次/小时

发消息频率
每企业不可超过200次/分钟；不可超过帐号上限数*30人次/天

创建帐号频率
每企业创建帐号数不可超过帐号上限数*3/月
*/
            #endregion

            var url = string.Format(API_URL + "/gettoken?corpid={0}&corpsecret={1}", corpId, corpSecret);
            var result = Get.GetJson<AccessTokenResult>(url);
            return result;
        }

        /// <summary>
        /// 获取微信服务器的ip段
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static GetCallBackIpResult GetCallBackIp(string accessToken)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}", accessToken);

            return CommonJsonSend.Send<GetCallBackIpResult>(null, url, null, CommonJsonSendType.GET);
        }
    }
}
