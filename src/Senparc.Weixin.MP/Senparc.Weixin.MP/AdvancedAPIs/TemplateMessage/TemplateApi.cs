#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：TemplateAPI.cs
    文件功能描述：模板消息接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
 
    修改标识：Senparc - 20160719
    修改描述：增加其接口的异步方法
 
    修改标识：Senparc - 20160808
    修改描述：去掉SendTemplateMessage，SendTemplateMessageAsync中的topcolor参数

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

    修改标识：Senparc - 20170707
    修改描述：v14.5.4 添加“一次性订阅消息”相关接口

----------------------------------------------------------------*/

/*
    API：http://mp.weixin.qq.com/wiki/17/304c1885ea66dbedf7dc170d84999a9d.html
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 模板消息接口
    /// </summary>
    public static class TemplateApi
    {
        /// <summary>
        /// 获取URL：一次性订阅消息，第一步引导用户打开链接进行授权
        /// 文档地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1500374289_66bvB
        /// </summary>
        /// <param name="appId">公众号的唯一标识</param>
        /// <param name="scene">重定向后会带上scene参数，开发者可以填0-10000的整形值，用来标识订阅场景值</param>
        /// <param name="templateId">订阅消息模板ID，登录公众平台后台，在接口权限列表处可查看订阅模板ID</param>
        /// <param name="redirectUrl">授权后重定向的回调地址，请使用UrlEncode对链接进行处理。注：要求redirect_url的域名要跟登记的业务域名一致，且业务域名不能带路径</param>
        /// <param name="reserved">（非必填）用于保持请求和回调的状态，授权请后原样带回给第三方。该参数可用于防止csrf攻击（跨站请求伪造攻击），建议第三方带上该参数，可设置为简单的随机数加session进行校验，开发者可以填写a-zA-Z0-9的参数值，最多128字节</param>
        /// <param name="action">直接填get_confirm即可，保留默认值</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.GetSubscribeMsgUrl", true)]
        public static string GetSubscribeMsgUrl(string appId, int scene, string templateId, string redirectUrl, string reserved = null, string action = "get_confirm")
        {
            //无论直接打开还是做页面302重定向时，必须带#wechat_redirect参数
            return string.Format("https://mp.weixin.qq.com/mp/subscribemsg?action={0}&appid={1}&scene={2}&template_id={3}&redirect_url={4}&reserved={5}#wechat_redirect",
                action, appId, scene, templateId, redirectUrl, reserved);
        }

        #region 同步方法

        /// <summary>
        /// 模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">填接收消息的用户openid</param>
        /// <param name="templateId">订阅消息模板ID</param>
        /// <param name="url">（非必须）点击消息跳转的链接，需要有ICP备案</param>
        /// <param name="data">消息正文，value为消息内容文本（200字以内），没有固定格式，可用\n换行，color为整段消息内容的字体颜色（目前仅支持整段消息为一种颜色）</param>
        /// <param name="miniProgram">（非必须）跳小程序所需数据，不需跳小程序可不用传该数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.SendTemplateMessage", true)]
        public static SendTemplateMessageResult SendTemplateMessage(string accessTokenOrAppId, string openId, string templateId, string url, object data, TempleteModel_MiniProgram miniProgram = null, int timeOut = Config.TIME_OUT)
        {
            //文档：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1500374289_66bvB

            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/template/send?access_token={0}";
                var msgData = new TempleteModel()
                {
                    touser = openId,
                    template_id = templateId,
                    // topcolor = topcolor,
                    url = url,
                    miniprogram = miniProgram,
                    data = data,
                };
                return CommonJsonSend.Send<SendTemplateMessageResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="templateMessageData"></param>
        /// <param name="miniProgram">跳小程序所需数据，不需跳小程序可不用传该数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.SendTemplateMessage", true)]
        public static SendTemplateMessageResult SendTemplateMessage(string accessTokenOrAppId, string openId, ITemplateMessageBase templateMessageData, TempleteModel_MiniProgram miniProgram = null, int timeOut = Config.TIME_OUT)
        {
            return SendTemplateMessage(accessTokenOrAppId, openId, templateMessageData.TemplateId,
                templateMessageData.Url, templateMessageData, miniProgram, timeOut);
        }

        /// <summary>
        /// 设置所属行业
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="industry_id1">公众号模板消息所属行业编号</param>
        /// <param name="industry_id2">公众号模板消息所属行业编号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.SetIndustry", true)]
        public static WxJsonResult SetIndustry(string accessTokenOrAppId, IndustryCode industry_id1, IndustryCode industry_id2, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/template/api_set_industry?access_token={0}";
                var msgData = new
                {
                    industry_id1 = ((int)industry_id1).ToString(),
                    industry_id2 = ((int)industry_id2).ToString()
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取设置的行业信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.GetIndustry", true)]
        public static GetIndustryJsonResult GetIndustry(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/template/get_industry?access_token={0}";
                return CommonJsonSend.Send<GetIndustryJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 添加模板并获得模板ID
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="template_id_short">模板库中模板的编号，有“TM**”和“OPENTMTM**”等形式</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.Addtemplate", true)]
        public static AddtemplateJsonResult Addtemplate(string accessTokenOrAppId, string template_id_short, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/template/api_add_template?access_token={0}";
                var msgData = new
                {
                    template_id_short = template_id_short

                };
                return CommonJsonSend.Send<AddtemplateJsonResult>(accessToken, urlFormat, msgData, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>

        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.GetPrivateTemplate", true)]
        public static GetPrivateTemplateJsonResult GetPrivateTemplate(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/template/get_all_private_template?access_token={0}";
                return CommonJsonSend.Send<GetPrivateTemplateJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="template_id">公众帐号下模板消息ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.DelPrivateTemplate", true)]
        public static WxJsonResult DelPrivateTemplate(string accessTokenOrAppId, string template_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/template/del_private_template?access_token={0}";
                var msgData = new
                {
                    template_id = template_id
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, msgData, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 通过API推送订阅模板消息给到授权微信用户
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="toUserOpenId">填接收消息的用户openid</param>
        /// <param name="templateId">订阅消息模板ID</param>
        /// <param name="scene">订阅场景值</param>
        /// <param name="title">消息标题，15字以内</param>
        /// <param name="data">消息正文，value为消息内容，color为颜色，200字以内</param>
        /// <param name="url">点击消息跳转的链接，需要有ICP备案</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.Subscribe", true)]
        public static WxJsonResult Subscribe(string accessTokenOrAppId, string toUserOpenId, string templateId, string scene, string title, object data, string url = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/template/subscribe?access_token={0}";

                var msgData = new SubscribeMsgTempleteModel()
                {
                    touser = toUserOpenId,
                    template_id = templateId,
                    url = url,
                    scene = scene,
                    title = title,
                    data = data
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, msgData, CommonJsonSendType.POST, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">填接收消息的用户openid</param>
        /// <param name="templateId">订阅消息模板ID</param>
        /// <param name="url">（非必须）点击消息跳转的链接，需要有ICP备案</param>
        /// <param name="data">消息正文，value为消息内容文本（200字以内），没有固定格式，可用\n换行，color为整段消息内容的字体颜色（目前仅支持整段消息为一种颜色）</param>
        /// <param name="miniProgram">（非必须）跳小程序所需数据，不需跳小程序可不用传该数据</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.SendTemplateMessageAsync", true)]
        public static async Task<SendTemplateMessageResult> SendTemplateMessageAsync(string accessTokenOrAppId, string openId, string templateId, string url, object data, TempleteModel_MiniProgram miniProgram = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/template/send?access_token={0}";
                var msgData = new TempleteModel()
                {
                    touser = openId,
                    template_id = templateId,
                    // topcolor = topcolor,
                    url = url,
                    miniprogram = miniProgram,
                    data = data,
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<SendTemplateMessageResult>(accessToken, urlFormat, msgData, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】模板消息接口
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId"></param>
        /// <param name="miniProgram">跳小程序所需数据，不需跳小程序可不用传该数据</param>
        /// <param name="templateMessageData"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.SendTemplateMessageAsync", true)]
        public static async Task<SendTemplateMessageResult> SendTemplateMessageAsync(string accessTokenOrAppId, string openId, ITemplateMessageBase templateMessageData, TempleteModel_MiniProgram miniProgram = null, int timeOut = Config.TIME_OUT)
        {
            return await SendTemplateMessageAsync(accessTokenOrAppId, openId, templateMessageData.TemplateId,
                templateMessageData.Url, templateMessageData, miniProgram, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】设置所属行业
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="industry_id1">公众号模板消息所属行业编号</param>
        /// <param name="industry_id2">公众号模板消息所属行业编号</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>

        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.SetIndustryAsync", true)]
        public static async Task<WxJsonResult> SetIndustryAsync(string accessTokenOrAppId, IndustryCode industry_id1, IndustryCode industry_id2, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/template/api_set_industry?access_token={0}";
                var msgData = new
                {
                    industry_id1 = ((int)industry_id1).ToString(),
                    industry_id2 = ((int)industry_id2).ToString()
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, msgData, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取设置的行业信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>

        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.GetIndustryAsync", true)]
        public static async Task<GetIndustryJsonResult> GetIndustryAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/template/get_industry?access_token={0}";
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetIndustryJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】获得模板ID
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="template_id_short">模板库中模板的编号，有“TM**”和“OPENTMTM**”等形式</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.AddtemplateAsync", true)]
        public static async Task<AddtemplateJsonResult> AddtemplateAsync(string accessTokenOrAppId, string template_id_short, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/template/api_add_template?access_token={0}";
                var msgData = new
                {
                    template_id_short = template_id_short

                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AddtemplateJsonResult>(accessToken, urlFormat, msgData, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        ///【异步办法】 获取模板列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>

        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.GetPrivateTemplateAsync", true)]
        public static async Task<GetPrivateTemplateJsonResult> GetPrivateTemplateAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/template/get_all_private_template?access_token={0}";
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetPrivateTemplateJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】删除模板
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="template_id">公众帐号下模板消息ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.DelPrivateTemplateAsync", true)]
        public static async Task<WxJsonResult> DelPrivateTemplateAsync(string accessTokenOrAppId, string template_id, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/template/del_private_template?access_token={0}";
                var msgData = new
                {
                    template_id = template_id
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, msgData, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】通过API推送订阅模板消息给到授权微信用户
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="toUserOpenId">填接收消息的用户openid</param>
        /// <param name="templateId">订阅消息模板ID</param>
        /// <param name="scene">订阅场景值</param>
        /// <param name="title">消息标题，15字以内</param>
        /// <param name="data">消息正文，value为消息内容，color为颜色，200字以内</param>
        /// <param name="url">点击消息跳转的链接，需要有ICP备案</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "TemplateApi.SubscribeAsync", true)]
        public static async Task<WxJsonResult> SubscribeAsync(string accessTokenOrAppId, string toUserOpenId, string templateId, string scene, string title, object data, string url = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/cgi-bin/message/template/subscribe?access_token={0}";

                var msgData = new SubscribeMsgTempleteModel()
                {
                    touser = toUserOpenId,
                    template_id = templateId,
                    url = url,
                    scene = scene,
                    title = title,
                    data = data
                };

                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, msgData, CommonJsonSendType.POST, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

    }
}