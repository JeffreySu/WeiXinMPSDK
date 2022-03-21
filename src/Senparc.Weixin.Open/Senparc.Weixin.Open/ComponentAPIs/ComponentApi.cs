/*----------------------------------------------------------------
    Copyright (C) 2022 Senparc
    
    文件名：OAuthJoinAPI.cs
    文件功能描述：公众号授权给第三方平台
    
    修改标识：Senparc - 20160520
    修改描述：添加“确认授权”接口
    
    创建标识：Senparc - 20150430
 
    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法
 
    修改标识：Senparc - 20161027
    修改描述：v2.3.2 修复：GetAuthorizerOption方法中option_name需要传入字符串。 感谢 @bingohanet

    修改标识：Senparc - 20170119
    修改描述：v2.3.7 修复：ApiConfirmAuth的URL中带空格

    修改标识：Senparc - 20180505
    修改描述：修改 ApiAuthorizerToken() 方法注释

    修改标识：Senparc - 20190615
    修改描述：修复帐号类型参数错误

    修改标识：mc7246 - 20211107
    修改描述：新增快速创建个人小程序接口：FastRegisterPersonalWeApp

    修改标识：mojinxun - 20211116
    修改描述：v4.13 实现“小程序用户隐私指引接口”

    修改标识：mc7246 - 20211121
    修改描述：v4.13.1 配置小程序用户隐私保护指引接口增加privacy_ver参数

----------------------------------------------------------------*/

/*
    官方文档：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1419318587&lang=zh_CN
 */

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.ComponentAPIs.RequestData;
using Senparc.Weixin.Open.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// ComponentApi
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public static class ComponentApi
    {
        #region 同步方法

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
            var url = Config.ApiMpHost + "/cgi-bin/component/api_component_token";

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
        /// <param name="componentAccessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static PreAuthCodeResult GetPreAuthCode(string componentAppId, string componentAccessToken, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_create_preauthcode?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId
            };

            return CommonJsonSend.Send<PreAuthCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /*此接口不提供异步方法*/
        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <param name="componentAppId">第三方平台方appid</param>
        /// <param name="preAuthCode">预授权码</param>
        /// <param name="redirectUrl">回调URL</param>
        /// <param name="authType">要授权的帐号类型</param>
        /// <param name="bizAppId">指定授权唯一的小程序或公众号</param>
        /// <returns></returns>
        public static string GetComponentLoginPageUrl(string componentAppId, string preAuthCode, string redirectUrl, LoginAuthType authType = LoginAuthType.默认, string bizAppId = "")
        {
            /*
             * 授权流程完成后，会进入回调URI，并在URL参数中返回授权码和过期时间(redirect_url?auth_code=xxx&expires_in=600)
             */

            var url =
                string.Format(
                    "https://mp.weixin.qq.com/cgi-bin/componentloginpage?component_appid={0}&pre_auth_code={1}&redirect_uri={2}",
                    componentAppId.AsUrlData(), preAuthCode.AsUrlData(), redirectUrl.AsUrlData());

            if (authType != LoginAuthType.默认)
                url = string.Format("{0}&auth_type={1}", url, (int)authType);

            if (!string.IsNullOrEmpty(bizAppId))
                url = string.Format("{0}&biz_appid={1}", url, bizAppId);

            return url;
        }

        /// <summary>
        /// 使用授权码换取公众号的授权信息
        /// </summary>
        /// <param name="componentAppId">服务开发方的appid</param>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="authorizationCode">授权code,会在授权成功时返回给第三方平台，详见第三方平台授权流程说明</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static QueryAuthResult QueryAuth(string componentAccessToken, string componentAppId, string authorizationCode, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_query_auth?component_access_token={0}", componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorization_code = authorizationCode
            };

            return CommonJsonSend.Send<QueryAuthResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 确认授权
        /// </summary>
        /// <param name="componentAppId">服务开发方的appid</param>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="authorizerAppid">授权code,会在授权成功时返回给第三方平台，详见第三方平台授权流程说明</param>
        /// <param name="funscopeCategoryId">服务开发方的access_token</param>
        /// <param name="confirmValue">服务开发方的access_token</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult ApiConfirmAuth(string componentAccessToken, string componentAppId, string authorizerAppid, int funscopeCategoryId, int confirmValue, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_confirm_authorization?component_access_token={0}", componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppid,
                funscope_category_id = funscopeCategoryId,
                confirm_value = confirmValue

            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取（刷新）授权公众号的令牌
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppId"></param>
        /// <param name="authorizerRefreshToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static RefreshAuthorizerTokenResult ApiAuthorizerToken(string componentAccessToken, string componentAppId, string authorizerAppId, string authorizerRefreshToken = null, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_authorizer_token?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
                authorizer_refresh_token = authorizerRefreshToken
            };

            return CommonJsonSend.Send<RefreshAuthorizerTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取授权方信息
        /// 注意：此方法返回的JSON中，authorization_info.authorizer_appid等几个参数通常为空（哪怕公众号有权限）
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetAuthorizerInfoResult GetAuthorizerInfo(string componentAccessToken, string componentAppId, string authorizerAppId, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_get_authorizer_info?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
            };

            return CommonJsonSend.Send<GetAuthorizerInfoResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 获取授权方的选项设置信息
        /// </summary>
        /// <param name="componentAppId">服务开发商的appid</param>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="authorizerAppId">授权公众号appid</param>
        /// <param name="optionName">选项名称</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AuthorizerOptionResult GetAuthorizerOption(string componentAccessToken, string componentAppId, string authorizerAppId, OptionName optionName, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_get_authorizer_option?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
                option_name = optionName.ToString()
            };

            return CommonJsonSend.Send<AuthorizerOptionResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 设置授权方的选项信息
        /// </summary>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="componentAppId">服务开发商的appid</param>
        /// <param name="authorizerAppId">授权公众号appid</param>
        /// <param name="optionName">选项名称</param>
        /// <param name="optionValue">设置的选项值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult SetAuthorizerOption(string componentAccessToken, string componentAppId, string authorizerAppId, OptionName optionName, int optionValue, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_set_authorizer_option?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
                option_name = optionName,
                option_value = optionValue
            };

            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 文档：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1421823488&token=&lang=zh_CN
        /// 获取调用微信JS接口的临时票据 OPEN
        /// </summary>
        /// <param name="authorizerAccessToken">authorizer_access_token</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static JsApiTicketResult GetJsApiTicket(string authorizerAccessToken, string type = "jsapi")
        {
            //获取第三方平台的授权公众号token（公众号授权给第三方平台后，第三方平台通过“接口说明”中的api_authorizer_token接口得到的token）
            var url = string.Format(Config.ApiMpHost + "/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                    authorizerAccessToken.AsUrlData(), type.AsUrlData());

            JsApiTicketResult result = CommonJsonSend.Send<JsApiTicketResult>(null, url, null, CommonJsonSendType.GET);
            return result;
        }

        /// <summary>
        /// 文档：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=21538208049W8uwq&token=&lang=zh_CN
        /// 创建(查询)小程序接口
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="entName">企业名（需与工商部门登记信息一致）</param>
        /// <param name="legalPersonaWechat">法人微信号</param>
        /// <param name="legalPersonaName">法人姓名（绑定银行卡）</param>
        /// <param name="action">动作类型：create或search，当为search时，entCode,codeType,componentPhone可不传参</param>
        /// <param name="entCode">企业代码</param>
        /// <param name="codeType">企业代码类型 1：统一社会信用代码（18位） 2：组织机构代码（9位xxxxxxxx-x） 3：营业执照注册号(15位)</param>
        /// <param name="componentPhone">第三方联系电话（方便法人与第三方联系）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult FastRegisterWeApp(string componentAccessToken, string entName, string legalPersonaWechat, string legalPersonaName, string action = "create", string entCode = "", CodeType codeType = CodeType.统一社会信用代码, string componentPhone = "", int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(
                Config.ApiMpHost + "/cgi-bin/component/fastregisterweapp?action={0}&component_access_token={1}",
                action.AsUrlData(),
                componentAccessToken.AsUrlData());

            //var data;
            object data;
            if (action == "create")
            {
                data = new
                {
                    name = entName,
                    code = entCode,
                    code_type = codeType,
                    legal_persona_wechat = legalPersonaWechat,
                    legal_persona_name = legalPersonaName,
                    component_phone = componentPhone
                };
            }
            else
            {
                data = new
                {
                    name = entName,
                    legal_persona_wechat = legalPersonaWechat,
                    legal_persona_name = legalPersonaName,
                };
            }

            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 快速创建个人小程序
        /// 文档：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/Register_Mini_Programs/fastregisterpersonalweapp.html
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="idName">个人用户名字</param>
        /// <param name="wxUser">个人用户微信号</param>
        /// <param name="taskid">任务id</param>
        /// <param name="action">动作类型：create或query，当为create时，taskid可不传参，当为query时，idName,wxUser可不传参</param>
        /// <param name="componentPhone">第三方联系电话（方便法人与第三方联系）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static FastRegisterPersonalWeAppResult FastRegisterPersonalWeApp(string componentAccessToken, string idName = "", string wxUser = "", string taskid = "", string action = "create", string componentPhone = "", int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(
                Config.ApiMpHost + "/cgi-bin/component/fastregisterpersonalweapp?action={0}&component_access_token={1}",
                action.AsUrlData(),
                componentAccessToken.AsUrlData());

            //var data;
            object data;
            if (action == "create")
            {
                data = new
                {
                    idname = idName,
                    wxuser = wxUser,
                    component_phone = componentPhone
                };
            }
            else
            {
                data = new
                {
                    taskid = taskid
                };
            }

            return CommonJsonSend.Send<FastRegisterPersonalWeAppResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 拉取所有已授权的帐号信息
        /// 文档：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/api/api_get_authorizer_list.html
        /// </summary>
        /// <param name="componentAppId">第三方平台 APPID</param>
        /// <param name="componentAccessToken">	令牌</param>
        /// <param name="offset">偏移位置/起始位置</param>
        /// <param name="count">拉取数量，最大为 500</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AuthorizerListResult GetAuthorizerList(string componentAppId, string componentAccessToken, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/cgi-bin/component/api_get_authorizer_list?component_access_token={0}", componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                component_access_token = componentAccessToken,
                offset = offset,
                count = count
            };

            return CommonJsonSend.Send<AuthorizerListResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 配置小程序用户隐私保护指引
        /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/privacy_config/set_privacy_setting.html
        /// </summary>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="ownerSetting">收集方（开发者）信息配置</param>
        /// <param name="settingList">要收集的用户信息配置，可选择的用户信息类型参考下方详情，当privacy_ver传2或者不传是必填；当privacy_ver传1时，该参数不可传，否则会报错</param>
        /// <param name="privacy_ver">用户隐私保护指引的版本，1表示现网版本；2表示开发版。默认是2开发版。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WxJsonResult SetPrivacySetting(string componentAccessToken, SetPrivacySettingData_OwnerSetting ownerSetting, List<SetPrivacySettingData_SettingList> settingList, int privacy_ver = 2, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/setprivacysetting?access_token={0}",
                    componentAccessToken.AsUrlData());

            object data;
            if (privacy_ver == 2)
            {
                data = new
                {
                    privacy_ver = privacy_ver,
                    owner_setting = ownerSetting,
                    setting_list = settingList
                };
            }
            else
            {
                data = new
                {
                    privacy_ver = privacy_ver,
                    owner_setting = ownerSetting
                };
            }

            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询小程序用户隐私保护指引
        /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/privacy_config/get_privacy_setting.html
        /// </summary>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="privacy_ver">1表示现网版本，即，传1则该接口返回的内容是现网版本的；2表示开发版，即，传2则该接口返回的内容是开发版本的。默认是2。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetPrivacySettingResult GetPrivacySetting(string componentAccessToken, int privacy_ver = 2, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/getprivacysetting?access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                privacy_ver
            };

            return CommonJsonSend.Send<GetPrivacySettingResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 上传小程序用户隐私保护指引
        /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/privacy_config/upload_privacy_exfile.html
        /// </summary>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="file"></param>
        /// <param name="serviceProvider">ServiceProvider</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static UploadPrivacyExtFileResult UploadPrivacyExtFile(string componentAccessToken, string file, IServiceProvider serviceProvider = null, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/uploadprivacyextfile?access_token={0}",
                    componentAccessToken.AsUrlData());

            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["file"] = file;
            return CO2NET.HttpUtility.Post.PostFileGetJson<UploadPrivacyExtFileResult>(serviceProvider ?? CommonDI.CommonSP, url, null, fileDictionary, null, timeOut: timeOut);
        }
        #endregion


        #region 异步方法
        /// <summary>
        /// 【异步方法】获取第三方平台access_token
        /// </summary>
        /// <param name="componentAppId">第三方平台appid</param>
        /// <param name="componentAppSecret">第三方平台appsecret</param>
        /// <param name="componentVerifyTicket">微信后台推送的ticket，此ticket会定时推送，具体请见本页末尾的推送说明</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ComponentAccessTokenResult> GetComponentAccessTokenAsync(string componentAppId, string componentAppSecret, string componentVerifyTicket, int timeOut = Config.TIME_OUT)
        {
            var url = Config.ApiMpHost + "/cgi-bin/component/api_component_token";

            var data = new
            {
                component_appid = componentAppId,
                component_appsecret = componentAppSecret,
                component_verify_ticket = componentVerifyTicket
            };

            return await CommonJsonSend.SendAsync<ComponentAccessTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取预授权码
        /// </summary>
        /// <param name="componentAppId">第三方平台方appid</param>
        /// <param name="componentAccessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<PreAuthCodeResult> GetPreAuthCodeAsync(string componentAppId, string componentAccessToken, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_create_preauthcode?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId
            };

            return await CommonJsonSend.SendAsync<PreAuthCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】使用授权码换取公众号的授权信息
        /// </summary>
        /// <param name="componentAppId">服务开发方的appid</param>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="authorizationCode">授权code,会在授权成功时返回给第三方平台，详见第三方平台授权流程说明</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<QueryAuthResult> QueryAuthAsync(string componentAccessToken, string componentAppId, string authorizationCode, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_query_auth?component_access_token={0}", componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorization_code = authorizationCode
            };

            return await CommonJsonSend.SendAsync<QueryAuthResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】确认授权
        /// </summary>
        /// <param name="componentAppId">服务开发方的appid</param>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="authorizerAppid">授权code,会在授权成功时返回给第三方平台，详见第三方平台授权流程说明</param>
        /// <param name="funscopeCategoryId">服务开发方的access_token</param>
        /// <param name="confirmValue">服务开发方的access_token</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ApiConfirmAuthAsync(string componentAccessToken, string componentAppId, string authorizerAppid, int funscopeCategoryId, int confirmValue, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/ cgi-bin/component/api_confirm_authorization?component_access_token={0}", componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppid,
                funscope_category_id = funscopeCategoryId,
                confirm_value = confirmValue

            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取（刷新）授权公众号的令牌
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppId"></param>
        /// <param name="authorizerRefreshToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<RefreshAuthorizerTokenResult> ApiAuthorizerTokenAsync(string componentAccessToken, string componentAppId, string authorizerAppId, string authorizerRefreshToken = null, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_authorizer_token?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
                authorizer_refresh_token = authorizerRefreshToken
            };

            return await CommonJsonSend.SendAsync<RefreshAuthorizerTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取授权方信息
        /// 注意：此方法返回的JSON中，authorization_info.authorizer_appid等几个参数通常为空（哪怕公众号有权限）
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetAuthorizerInfoResult> GetAuthorizerInfoAsync(string componentAccessToken, string componentAppId, string authorizerAppId, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_get_authorizer_info?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
            };

            return await CommonJsonSend.SendAsync<GetAuthorizerInfoResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取授权方的选项设置信息
        /// </summary>
        /// <param name="componentAppId">服务开发商的appid</param>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="authorizerAppId">授权公众号appid</param>
        /// <param name="optionName">选项名称</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<AuthorizerOptionResult> GetAuthorizerOptionAsync(string componentAccessToken, string componentAppId, string authorizerAppId, OptionName optionName, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_get_authorizer_option?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
                option_name = optionName
            };

            return await CommonJsonSend.SendAsync<AuthorizerOptionResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】设置授权方的选项信息
        /// </summary>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="componentAppId">服务开发商的appid</param>
        /// <param name="authorizerAppId">授权公众号appid</param>
        /// <param name="optionName">选项名称</param>
        /// <param name="optionValue">设置的选项值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SetAuthorizerOptionAsync(string componentAccessToken, string componentAppId, string authorizerAppId, OptionName optionName, int optionValue, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/api_set_authorizer_option?component_access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                authorizer_appid = authorizerAppId,
                option_name = optionName,
                option_value = optionValue
            };

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        //////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 文档：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1421823488&token=&lang=zh_CN
        /// 【异步方法】获取调用微信JS接口的临时票据 OPEN
        /// </summary>
        /// <param name="authorizerAccessToken">authorizer_access_token</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static async Task<JsApiTicketResult> GetJsApiTicketAsync(string authorizerAccessToken, string type = "jsapi")
        {
            //获取第三方平台的授权公众号token（公众号授权给第三方平台后，第三方平台通过“接口说明”中的api_authorizer_token接口得到的token）
            var url = string.Format(Config.ApiMpHost + "/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                    authorizerAccessToken.AsUrlData(), type.AsUrlData());

            JsApiTicketResult result = await CommonJsonSend.SendAsync<JsApiTicketResult>(null, url, null, CommonJsonSendType.GET).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// 【异步方法】创建小程序接口
        /// 文档：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=21538208049W8uwq&token=&lang=zh_CN
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="entName">企业名（需与工商部门登记信息一致）</param>
        /// <param name="entCode">企业代码</param>
        /// <param name="codeType">企业代码类型 1：统一社会信用代码（18位） 2：组织机构代码（9位xxxxxxxx-x） 3：营业执照注册号(15位)</param>
        /// <param name="legalPersonaWechat">法人微信号</param>
        /// <param name="legalPersonaName">法人姓名（绑定银行卡）</param>
        /// <param name="componentPhone">第三方联系电话（方便法人与第三方联系）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> FastRegisterWeAppAsync(string componentAccessToken, string entName, string legalPersonaWechat, string legalPersonaName, string action = "create", string entCode = "", CodeType codeType = CodeType.统一社会信用代码, string componentPhone = "", int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(
                Config.ApiMpHost + "/cgi-bin/component/fastregisterweapp?action={0}&component_access_token={1}",
                action.AsUrlData(),
                componentAccessToken.AsUrlData());

            //var data;
            object data;
            if (action == "create")
            {
                data = new
                {
                    name = entName,
                    code = entCode,
                    code_type = codeType,
                    legal_persona_wechat = legalPersonaWechat,
                    legal_persona_name = legalPersonaName,
                    component_phone = componentPhone
                };
            }
            else
            {
                data = new
                {
                    name = entName,
                    legal_persona_wechat = legalPersonaWechat,
                    legal_persona_name = legalPersonaName,
                };
            }

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】快速创建个人小程序
        /// 文档：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/Register_Mini_Programs/fastregisterpersonalweapp.html
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="idName">个人用户名字</param>
        /// <param name="wxUser">个人用户微信号</param>
        /// <param name="taskid">任务id</param>
        /// <param name="action">动作类型：create或query，当为create时，taskid可不传参，当为query时，idName,wxUser可不传参</param>
        /// <param name="componentPhone">第三方联系电话（方便法人与第三方联系）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<FastRegisterPersonalWeAppResult> FastRegisterPersonalWeAppAsync(string componentAccessToken, string idName = "", string wxUser = "", string taskid = "", string action = "create", string componentPhone = "", int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(
                Config.ApiMpHost + "/cgi-bin/component/fastregisterpersonalweapp?action={0}&component_access_token={1}",
                action.AsUrlData(),
                componentAccessToken.AsUrlData());

            //var data;
            object data;
            if (action == "create")
            {
                data = new
                {
                    idname = idName,
                    wxuser = wxUser,
                    component_phone = componentPhone
                };
            }
            else
            {
                data = new
                {
                    taskid = taskid
                };
            }

            return await CommonJsonSend.SendAsync<FastRegisterPersonalWeAppResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】拉取所有已授权的帐号信息
        /// 文档：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/api/api_get_authorizer_list.html
        /// </summary>
        /// <param name="componentAppId">第三方平台 APPID</param>
        /// <param name="componentAccessToken">	令牌</param>
        /// <param name="offset">偏移位置/起始位置</param>
        /// <param name="count">拉取数量，最大为 500</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<AuthorizerListResult> GetAuthorizerListAsync(string componentAppId, string componentAccessToken, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/cgi-bin/component/api_get_authorizer_list?component_access_token={0}", componentAccessToken.AsUrlData());

            var data = new
            {
                component_appid = componentAppId,
                component_access_token = componentAccessToken,
                offset = offset,
                count = count
            };

            return await CommonJsonSend.SendAsync<AuthorizerListResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
        }



        /// <summary>
        /// 【异步方法】配置小程序用户隐私保护指引
        /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/privacy_config/set_privacy_setting.html
        /// </summary>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="ownerSetting">收集方（开发者）信息配置</param>
        /// <param name="settingList">要收集的用户信息配置，可选择的用户信息类型参考下方详情，当privacy_ver传2或者不传是必填；当privacy_ver传1时，该参数不可传，否则会报错</param>
        /// <param name="privacy_ver">用户隐私保护指引的版本，1表示现网版本；2表示开发版。默认是2开发版。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SetPrivacySettingAsync(string componentAccessToken, SetPrivacySettingData_OwnerSetting ownerSetting, List<SetPrivacySettingData_SettingList> settingList, int privacy_ver = 2, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/setprivacysetting?access_token={0}",
                    componentAccessToken.AsUrlData());

            object data;
            if (privacy_ver == 2)
            {
                data = new
                {
                    privacy_ver = privacy_ver,
                    owner_setting = ownerSetting,
                    setting_list = settingList
                };
            }
            else
            {
                data = new
                {
                    privacy_ver = privacy_ver,
                    owner_setting = ownerSetting
                };
            }

            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 【异步方法】查询小程序用户隐私保护指引
        /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/privacy_config/get_privacy_setting.html
        /// </summary>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="privacy_ver">1表示现网版本，即，传1则该接口返回的内容是现网版本的；2表示开发版，即，传2则该接口返回的内容是开发版本的。默认是2。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetPrivacySettingResult> GetPrivacySettingAsync(string componentAccessToken, int privacy_ver = 2, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/getprivacysetting?access_token={0}",
                    componentAccessToken.AsUrlData());

            var data = new
            {
                privacy_ver
            };

            return await CommonJsonSend.SendAsync<GetPrivacySettingResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 【异步方法】上传小程序用户隐私保护指引
        /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/privacy_config/upload_privacy_exfile.html
        /// </summary>
        /// <param name="componentAccessToken">服务开发方的access_token</param>
        /// <param name="file"></param>
        /// <param name="serviceProvider">ServiceProvider</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<UploadPrivacyExtFileResult> UploadPrivacyExtFileAsync(string componentAccessToken, string file, IServiceProvider serviceProvider = null, int timeOut = Config.TIME_OUT)
        {
            var url =
                string.Format(
                    Config.ApiMpHost + "/cgi-bin/component/uploadprivacyextfile?access_token={0}",
                    componentAccessToken.AsUrlData());

            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["file"] = file;
            return await CO2NET.HttpUtility.Post.PostFileGetJsonAsync<UploadPrivacyExtFileResult>(serviceProvider ?? CommonDI.CommonSP, url, null, fileDictionary, null, timeOut: timeOut);
        }
        #endregion
    }
}
