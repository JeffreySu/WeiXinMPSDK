#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2020 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc

    文件名：SetWebViewDomainApi.cs
    文件功能描述：设置业务域名接口


    创建标识：Yoafeng - 20201224
        
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Open.WxaAPIs.SetWebViewDomain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    [NcApiBind(NeuChar.PlatformType.WeChat_Open,true)]
    public class SetWebViewDomainApi
    {
        #region 同步方法
        /// <summary>
        /// 修改服务器地址 接口
        /// <para>https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/Mini_Programs/setwebviewdomain.html</para>
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="webviewdomain">小程序业务域名，当 action 参数是 get 时不需要此字段</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("请使用DomainApi.SetWebViewDomain")]
        public static SetWebViewDomainJsonResult SetWebViewDomain(string accessToken, SetWebViewDomainAction action,
            List<string> webviewdomain,
            int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/setwebviewdomain?access_token={0}", accessToken.AsUrlData());

            object data;

            if (action == SetWebViewDomainAction.get)
            {
                data = new
                {
                    action = action.ToString()
                };
            }
            else
            {
                data = new
                {
                    action = action.ToString(),
                    webviewdomain = webviewdomain
                };
            }

            return CommonJsonSend.Send<SetWebViewDomainJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】修改服务器地址 接口
        /// <para>https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/Mini_Programs/setwebviewdomain.html</para>
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="webviewdomain">小程序业务域名，当 action 参数是 get 时不需要此字段</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("请使用DomainApi.SetWebViewDomainAsync")]
        public static async Task<SetWebViewDomainJsonResult> SetWebViewDomainAsync(string accessToken, SetWebViewDomainAction action,
            List<string> webviewdomain,
            int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/setwebviewdomain?access_token={0}", accessToken.AsUrlData());

            object data;

            if (action == SetWebViewDomainAction.get)
            {
                data = new
                {
                    action = action.ToString()
                };
            }
            else
            {
                data = new
                {
                    action = action.ToString(),
                    webviewdomain = webviewdomain
                };
            }

            return await CommonJsonSend.SendAsync<SetWebViewDomainJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }
        #endregion
    }
}
