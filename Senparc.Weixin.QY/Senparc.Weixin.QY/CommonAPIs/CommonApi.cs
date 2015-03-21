/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：CommonApi.cs
    文件功能描述：通用基础API
    
    
    创建标识：Senparc - 20130313
    
    修改标识：Senparc - 20130313
    修改描述：整理接口
----------------------------------------------------------------*/

/*
    获取AccessToken API地址：http://qydev.weixin.qq.com/wiki/index.php?title=%E4%B8%BB%E5%8A%A8%E8%B0%83%E7%94%A8
    获取微信服务器ip段 API地址：http://qydev.weixin.qq.com/wiki/index.php?title=%E5%9B%9E%E8%B0%83%E6%A8%A1%E5%BC%8F#.E8.8E.B7.E5.8F.96.E5.BE.AE.E4.BF.A1.E6.9C.8D.E5.8A.A1.E5.99.A8.E7.9A.84ip.E6.AE.B5
    获取调用微信JS接口的临时票据 API地址：http://qydev.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1JS%E6%8E%A5%E5%8F%A3
 */

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

            return Get.GetJson<GetCallBackIpResult>(url);
        }

        /// <summary>
        /// 获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicket(string corpId, string corpSecret)
        {
            var accessToken = GetToken(corpId, corpSecret).access_token;

            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}",
                                    accessToken);

            JsApiTicketResult result = Get.GetJson<JsApiTicketResult>(url);
            return result;
        }
    }
}
