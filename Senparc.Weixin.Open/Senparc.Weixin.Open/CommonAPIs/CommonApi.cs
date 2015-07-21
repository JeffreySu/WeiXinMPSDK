/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：CommonApi.cs
    文件功能描述：通用接口(用于和微信服务器通讯，一般不涉及自有网站服务器的通讯)
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/index.php?title=%E6%8E%A5%E5%8F%A3%E6%96%87%E6%A1%A3&oldid=103
    
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.Open.Entities;
using Senparc.Weixin.Open.Helpers;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.Open.CommonAPIs
{
    /// <summary>
    /// 通用接口
    /// 通用接口用于和微信服务器通讯，一般不涉及自有网站服务器的通讯
    /// </summary>
    public partial class CommonApi
    {
        /// <summary>
        /// 获取第三方平台access_token
        /// </summary>
        /// <param name="componentAppId">第三方平台appid</param>
        /// <param name="componentAppSecret">第三方平台appsecret</param>
        /// <param name="componentVerifyTicket">微信后台推送的ticket，此ticket会定时推送，具体请见本页末尾的推送说明</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ComponentAccessTokenResult GetComponentAccessToken(string componentAppId, string componentAppSecret, string componentVerifyTicket, int timeOut = Config.TIME_OUT)
        {
            var url = "https://api.weixin.qq.com/cgi-bin/component/api_component_token";

            var data = new
                {
                    component_appid = componentAppId,
                    component_appsecret = componentAppSecret,
                    component_verify_ticket = componentVerifyTicket
                };

            return CommonJsonSend.Send<ComponentAccessTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取预授权码
        /// </summary>
        /// <param name="componentAppId">第三方平台方appid</param>
        /// <param name="componentAppSecret"></param>
        /// <param name="componentVerifyTicket"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static PreAuthCodeResult GetPreAuthCode(string componentAppId, string componentAppSecret,string componentVerifyTicket, int timeOut = Config.TIME_OUT)
        {
            //获取componentAccessToken
            var componentAccessToken = ComponentAccessTokenContainer.TryGetToken(componentAppId, componentAppSecret,
                                                                                 componentVerifyTicket);

            var url =
                string.Format(
                    "https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?component_access_token={0}",
                    componentAccessToken);

            var data = new
                {
                    component_appid = componentAppId
                };
             
            return CommonJsonSend.Send<PreAuthCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
    }
}
