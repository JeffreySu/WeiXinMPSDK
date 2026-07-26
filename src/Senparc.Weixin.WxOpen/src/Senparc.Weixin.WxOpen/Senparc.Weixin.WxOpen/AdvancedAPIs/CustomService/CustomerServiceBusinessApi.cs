/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：CustomerServiceBusinessApi.cs
    文件功能描述：CustomerServiceBusinessApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.28.1 补齐小程序物流、交易、直播、短剧、小说和行业能力接口及事件

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.CustomService
{
    /// <summary>
    /// RegisterBusiness 接口返回结果。
    /// </summary>
    public class RegisterBusinessJsonResult : WxJsonResult
    {
        public object business_id { get; set; }
    }

    /// <summary>
    /// CustomerServiceBusiness 信息。
    /// </summary>
    public class CustomerServiceBusinessInfo
    {
        public object business_id { get; set; }
        public string account_name { get; set; }
        public string nickname { get; set; }
        public string icon_media_id { get; set; }
        public string icon_url { get; set; }
    }

    /// <summary>
    /// GetBusiness 接口返回结果。
    /// </summary>
    public class GetBusinessJsonResult : WxJsonResult
    {
        public CustomerServiceBusinessInfo business_info { get; set; }
    }

    /// <summary>
    /// ListBusiness 接口返回结果。
    /// </summary>
    public class ListBusinessJsonResult : WxJsonResult
    {
        public List<CustomerServiceBusinessInfo> list { get; set; }
    }

    /// <summary>
    /// 小程序客服子商户管理
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class CustomerServiceBusinessApi
    {
        /// <summary>
        /// 注册客服业务账号。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="accountName">客服业务账号名称。</param>
        /// <param name="nickname">客服业务账号昵称。</param>
        /// <param name="iconMediaId">头像素材 MediaId。</param>
        /// <param name="transferToCommonKf">是否允许转接普通客服。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static RegisterBusinessJsonResult Register(string accessTokenOrAppId, string accountName, string nickname,
            string iconMediaId, bool transferToCommonKf = false, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new
                {
                    account_name = accountName,
                    nickname,
                    icon_media_id = iconMediaId,
                    transfer_to_commkf = transferToCommonKf
                };
                return CommonJsonSend.Send<RegisterBusinessJsonResult>(accessToken,
                    Config.ApiMpHost + "/cgi-bin/business/register?access_token={0}", data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更新客服业务账号。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="businessId">客服业务账号 ID。</param>
        /// <param name="nickname">客服业务账号昵称。</param>
        /// <param name="iconMediaId">头像素材 MediaId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WxJsonResult Update(string accessTokenOrAppId, string businessId, string nickname = null,
            string iconMediaId = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send(accessToken, Config.ApiMpHost + "/cgi-bin/business/update?access_token={0}",
                    new { business_id = businessId, nickname, icon_media_id = iconMediaId }, timeOut: timeOut,
                    jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting { IgnoreNulls = true }), accessTokenOrAppId);
        }

        /// <summary>
        /// 获取二维码跳转规则。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="businessId">客服业务账号 ID。</param>
        /// <param name="accountName">客服业务账号名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetBusinessJsonResult Get(string accessTokenOrAppId, string businessId = null,
            string accountName = null, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<GetBusinessJsonResult>(accessToken,
                    Config.ApiMpHost + "/cgi-bin/business/get?access_token={0}",
                    new { business_id = businessId, account_name = accountName }, timeOut: timeOut,
                    jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting { IgnoreNulls = true }), accessTokenOrAppId);
        }

        /// <summary>
        /// 获取客服业务账号列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="offset">分页偏移量。</param>
        /// <param name="count">本次拉取数量。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ListBusinessJsonResult List(string accessTokenOrAppId, int offset, int count,
            int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<ListBusinessJsonResult>(accessToken,
                    Config.ApiMpHost + "/cgi-bin/business/list?access_token={0}", new { offset, count }, timeOut: timeOut),
                accessTokenOrAppId);
        }

        /// <summary>
        /// 异步注册客服业务账号。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="accountName">客服业务账号名称。</param>
        /// <param name="nickname">客服业务账号昵称。</param>
        /// <param name="iconMediaId">头像素材 MediaId。</param>
        /// <param name="transferToCommonKf">是否允许转接普通客服。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<RegisterBusinessJsonResult> RegisterAsync(string accessTokenOrAppId, string accountName, string nickname,
            string iconMediaId, bool transferToCommonKf = false, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
            {
                var data = new
                {
                    account_name = accountName,
                    nickname,
                    icon_media_id = iconMediaId,
                    transfer_to_commkf = transferToCommonKf
                };
                return CommonJsonSend.SendAsync<RegisterBusinessJsonResult>(accessToken,
                    Config.ApiMpHost + "/cgi-bin/business/register?access_token={0}", data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步更新客服业务账号。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="businessId">客服业务账号 ID。</param>
        /// <param name="nickname">客服业务账号昵称。</param>
        /// <param name="iconMediaId">头像素材 MediaId。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<WxJsonResult> UpdateAsync(string accessTokenOrAppId, string businessId, string nickname = null,
            string iconMediaId = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync(accessToken, Config.ApiMpHost + "/cgi-bin/business/update?access_token={0}",
                    new { business_id = businessId, nickname, icon_media_id = iconMediaId }, timeOut: timeOut,
                    jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting { IgnoreNulls = true }), accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步获取二维码跳转规则。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="businessId">客服业务账号 ID。</param>
        /// <param name="accountName">客服业务账号名称。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<GetBusinessJsonResult> GetAsync(string accessTokenOrAppId, string businessId = null,
            string accountName = null, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<GetBusinessJsonResult>(accessToken,
                    Config.ApiMpHost + "/cgi-bin/business/get?access_token={0}",
                    new { business_id = businessId, account_name = accountName }, timeOut: timeOut,
                    jsonSetting: new CO2NET.Helpers.Serializers.JsonSetting { IgnoreNulls = true }), accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步获取客服业务账号列表。
        /// </summary>
        /// <param name="accessTokenOrAppId">接口调用凭证或已注册的 AppId。</param>
        /// <param name="offset">分页偏移量。</param>
        /// <param name="count">本次拉取数量。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static async Task<ListBusinessJsonResult> ListAsync(string accessTokenOrAppId, int offset, int count,
            int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(accessToken =>
                CommonJsonSend.SendAsync<ListBusinessJsonResult>(accessToken,
                    Config.ApiMpHost + "/cgi-bin/business/list?access_token={0}", new { offset, count }, timeOut: timeOut),
                accessTokenOrAppId).ConfigureAwait(false);
        }
    }
}
