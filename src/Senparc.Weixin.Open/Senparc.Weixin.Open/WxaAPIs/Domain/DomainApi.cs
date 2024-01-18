#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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

    文件名：DomainApi.cs
    文件功能描述：小程序域名管理


    创建标识：Yaofeng - 20220809
        
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Open.WxaAPIs.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 三方平台API - 小程序域名管理
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class DomainApi
    {
        #region 同步方法
        /// <summary>
        /// 修改服务器地址 接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="requestdomain">request合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="wsrequestdomain">socket合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="uploaddomain">uploadFile合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="downloaddomain">downloadFile合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="udpdomain">udp 合法域名；当 action 是 get 时不需要此字段</param>
        /// <param name="tcpdomain">tcp 合法域名；当 action 是 get 时不需要此字段</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ModifyDomainResultJson ModifyDomain(string accessToken, ModifyDomainAction action,
            List<string> requestdomain,
            List<string> wsrequestdomain,
            List<string> uploaddomain,
            List<string> downloaddomain,
            List<string> udpdomain,
            List<string> tcpdomain,
            int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/modify_domain?access_token={0}", accessToken.AsUrlData());

            object data;

            if (action == ModifyDomainAction.get)
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
                    requestdomain = requestdomain,
                    wsrequestdomain = wsrequestdomain,
                    uploaddomain = uploaddomain,
                    downloaddomain = downloaddomain,
                    udpdomain = udpdomain,
                    tcpdomain = tcpdomain
                };
            }

            return CommonJsonSend.Send<ModifyDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 修改服务器地址 接口
        /// <para>https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/Mini_Programs/setwebviewdomain.html</para>
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="webviewdomain">小程序业务域名，当 action 参数是 get 时不需要此字段</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SetWebViewDomainResultJson SetWebViewDomain(string accessToken, SetWebViewDomainAction action,
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

            return CommonJsonSend.Send<SetWebViewDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 快速配置小程序服务器域名
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/modifyServerDomainDirectly.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="requestdomain">request 合法域名，当action参数是get时不需要此字段</param>
        /// <param name="wsrequestdomain">socket 合法域名，当action参数是get时不需要此字段</param>
        /// <param name="uploaddomain">uploadFile 合法域名，当action参数是get时不需要此字段</param>
        /// <param name="downloaddomain">downloadFile 合法域名，当action参数是get时不需要此字段</param>
        /// <param name="udpdomain">udp 合法域名；当 action 是 get 时不需要此字段</param>
        /// <param name="tcpdomain">tcp 合法域名；当 action 是 get 时不需要此字段</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ModifyDomainDirectlyResultJson ModifyDomainDirectly(string accessToken, ModifyDomainAction action,
            List<string> requestdomain,
            List<string> wsrequestdomain,
            List<string> uploaddomain,
            List<string> downloaddomain,
            List<string> udpdomain,
            List<string> tcpdomain,
            int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/modify_domain_directly?access_token={0}", accessToken.AsUrlData());

            object data;

            if (action == ModifyDomainAction.get)
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
                    requestdomain = requestdomain,
                    wsrequestdomain = wsrequestdomain,
                    uploaddomain = uploaddomain,
                    downloaddomain = downloaddomain,
                    udpdomain = udpdomain,
                    tcpdomain = tcpdomain
                };
            }

            return CommonJsonSend.Send<ModifyDomainDirectlyResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取业务域名校验文件
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/getJumpDomainConfirmFile.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetWebViewDomainConfirmFileResultJson GetWebViewDomainConfirmFile(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_webviewdomain_confirmfile?access_token={0}", accessToken.AsUrlData());
            object data = new { };
            return CommonJsonSend.Send<GetWebViewDomainConfirmFileResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 快速配置小程序服务器域名
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/modifyServerDomainDirectly.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="webviewdomain">小程序业务域名，当 action 参数是 get 时不需要此字段</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SetWebViewDomainDirectlyResultJson SetWebViewDomainDirectly(string accessToken, SetWebViewDomainAction action,
            List<string> webviewdomain,
            int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/setwebviewdomain_directly?access_token={0}", accessToken.AsUrlData());

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

            return CommonJsonSend.Send<SetWebViewDomainDirectlyResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取发布后生效服务器域名列表
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/getEffectiveServerDomain.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetEffectiveDomainResultJson GetEffectiveDomain(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_effective_domain?access_token={0}", accessToken.AsUrlData());
            object data = new { };
            return CommonJsonSend.Send<GetEffectiveDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取发布后生效业务域名列表
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/getEffectiveJumpDomain.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetEffectiveWebViewDomainResultJson GetEffectiveWebViewDomain(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_effective_webviewdomain?access_token={0}", accessToken.AsUrlData());
            object data = new { };
            return CommonJsonSend.Send<GetEffectiveWebViewDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取DNS预解析域名
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/getPrefetchDomain.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetPrefetchDNSDomainResultJson GetPrefetchDNSDomain(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_prefetchdnsdomain?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<GetPrefetchDNSDomainResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 设置DNS预解析域名
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/setPrefetchDomain.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="prefetch_dns_domain">预解析域名</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static SetPrefetchDNSDomainResultJson SetPrefetchDNSDomain(string accessToken, List<SetPrefetchDNSDomainData> prefetch_dns_domain, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/set_prefetchdnsdomain?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                prefetch_dns_domain
            };
            return CommonJsonSend.Send<SetPrefetchDNSDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】修改服务器地址 接口
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="requestdomain">request合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="wsrequestdomain">socket合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="uploaddomain">uploadFile合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="downloaddomain">downloadFile合法域名，当action参数是get时不需要此字段。</param>
        /// <param name="udpdomain">udp 合法域名；当 action 是 get 时不需要此字段</param>
        /// <param name="tcpdomain">tcp 合法域名；当 action 是 get 时不需要此字段</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ModifyDomainResultJson> ModifyDomainAsync(string accessToken, ModifyDomainAction action,
            List<string> requestdomain,
            List<string> wsrequestdomain,
            List<string> uploaddomain,
            List<string> downloaddomain,
            List<string> udpdomain,
            List<string> tcpdomain,
            int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/modify_domain?access_token={0}", accessToken.AsUrlData());

            object data;

            if (action == ModifyDomainAction.get)
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
                    requestdomain = requestdomain,
                    wsrequestdomain = wsrequestdomain,
                    uploaddomain = uploaddomain,
                    downloaddomain = downloaddomain,
                    udpdomain = udpdomain,
                    tcpdomain = tcpdomain,
                };
            }

            return await CommonJsonSend.SendAsync<ModifyDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】修改服务器地址 接口
        /// <para>https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/Mini_Programs/setwebviewdomain.html</para>
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="webviewdomain">小程序业务域名，当 action 参数是 get 时不需要此字段</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<SetWebViewDomainResultJson> SetWebViewDomainAsync(string accessToken, SetWebViewDomainAction action,
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

            return await CommonJsonSend.SendAsync<SetWebViewDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 快速配置小程序服务器域名
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/modifyServerDomainDirectly.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="requestdomain">request 合法域名，当action参数是get时不需要此字段</param>
        /// <param name="wsrequestdomain">socket 合法域名，当action参数是get时不需要此字段</param>
        /// <param name="uploaddomain">uploadFile 合法域名，当action参数是get时不需要此字段</param>
        /// <param name="downloaddomain">downloadFile 合法域名，当action参数是get时不需要此字段</param>
        /// <param name="udpdomain">udp 合法域名；当 action 是 get 时不需要此字段</param>
        /// <param name="tcpdomain">tcp 合法域名；当 action 是 get 时不需要此字段</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ModifyDomainDirectlyResultJson> ModifyDomainDirectlyAsync(string accessToken, ModifyDomainAction action,
            List<string> requestdomain,
            List<string> wsrequestdomain,
            List<string> uploaddomain,
            List<string> downloaddomain,
            List<string> udpdomain,
            List<string> tcpdomain,
            int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/modify_domain_directly?access_token={0}", accessToken.AsUrlData());

            object data;

            if (action == ModifyDomainAction.get)
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
                    requestdomain = requestdomain,
                    wsrequestdomain = wsrequestdomain,
                    uploaddomain = uploaddomain,
                    downloaddomain = downloaddomain,
                    udpdomain = udpdomain,
                    tcpdomain = tcpdomain
                };
            }

            return await CommonJsonSend.SendAsync<ModifyDomainDirectlyResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取业务域名校验文件
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/getJumpDomainConfirmFile.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetWebViewDomainConfirmFileResultJson> GetWebViewDomainConfirmFileAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_webviewdomain_confirmfile?access_token={0}", accessToken.AsUrlData());
            object data = new { };
            return await CommonJsonSend.SendAsync<GetWebViewDomainConfirmFileResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 快速配置小程序服务器域名
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/modifyServerDomainDirectly.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="action">操作类型</param>
        /// <param name="webviewdomain">小程序业务域名，当 action 参数是 get 时不需要此字段</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<SetWebViewDomainDirectlyResultJson> SetWebViewDomainDirectlyAsync(string accessToken, SetWebViewDomainAction action,
            List<string> webviewdomain,
            int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/setwebviewdomain_directly?access_token={0}", accessToken.AsUrlData());

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

            return await CommonJsonSend.SendAsync<SetWebViewDomainDirectlyResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取发布后生效服务器域名列表
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/getEffectiveServerDomain.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetEffectiveDomainResultJson> GetEffectiveDomainAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_effective_domain?access_token={0}", accessToken.AsUrlData());
            object data = new { };
            return await CommonJsonSend.SendAsync<GetEffectiveDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取发布后生效业务域名列表
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/getEffectiveJumpDomain.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetEffectiveWebViewDomainResultJson> GetEffectiveWebViewDomainAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_effective_webviewdomain?access_token={0}", accessToken.AsUrlData());
            object data = new { };
            return await CommonJsonSend.SendAsync<GetEffectiveWebViewDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取DNS预解析域名
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/getPrefetchDomain.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetPrefetchDNSDomainResultJson> GetPrefetchDNSDomainAsync(string accessToken, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/get_prefetchdnsdomain?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<GetPrefetchDNSDomainResultJson>(null, url, null, CommonJsonSendType.GET, timeOut);
        }

        /// <summary>
        /// 设置DNS预解析域名
        /// https://developers.weixin.qq.com/doc/oplatform/openApi/OpenApiDoc/miniprogram-management/domain-management/setPrefetchDomain.html
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="prefetch_dns_domain">预解析域名</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<SetPrefetchDNSDomainResultJson> SetPrefetchDNSDomainAsync(string accessToken, List<SetPrefetchDNSDomainData> prefetch_dns_domain, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/set_prefetchdnsdomain?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                prefetch_dns_domain
            };
            return await CommonJsonSend.SendAsync<SetPrefetchDNSDomainResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        #endregion
    }
}
