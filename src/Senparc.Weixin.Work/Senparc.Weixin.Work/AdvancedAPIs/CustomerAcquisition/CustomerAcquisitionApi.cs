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

    文件名：CustomerAcquisitionApi.cs
    文件功能描述：获客链接管理Api
    文档地址: https://developer.work.weixin.qq.com/document/path/97297

    创建标识：IcedMango - 20240809

----------------------------------------------------------------*/
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.CustomerAcquisition.CustomerAcquisitionJson;

namespace Senparc.Weixin.Work.AdvancedAPIs.CustomerAcquisition
{
    /// <summary>
    /// 获客链接管理Api
    /// </summary>
    public class CustomerAcquisitionApi
    {
        #region 同步方法

        /// <summary>
        ///   获取获客链接列表
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="limit">返回的最大记录数，整型，最大值100</param>
        /// <param name="cursor">用于分页查询的游标，字符串类型，由上一次调用返回，首次调用可不填</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [NcApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerAcquisitionApi.GetLinkList", true)]
        public static GetCustomerAcquisitionLinkListResult GetLinkList(string accessTokenOrAppKey,
            int limit = 100, string cursor = "", int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(
                    Config.ApiWorkHost + "/cgi-bin/externalcontact/customer_acquisition/list_link?access_token={0}",
                    accessToken.AsUrlData());

                var postData = new
                {
                    limit = limit,
                    cursor = cursor
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetCustomerAcquisitionLinkListResult>(accessToken,
                    url,
                    postData, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        ///     获取获客链接详情
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="link_id">获客链接id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [NcApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerAcquisitionApi.GetLinkDetail", true)]
        public static GetCustomerAcquisitionLinkDetailResult GetLinkDetail(string accessTokenOrAppKey,
            string link_id = "", int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(
                    Config.ApiWorkHost + "/cgi-bin/externalcontact/customer_acquisition/get?access_token={0}",
                    accessToken.AsUrlData());

                var postData = new
                {
                    link_id = link_id
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetCustomerAcquisitionLinkDetailResult>(
                    accessToken,
                    url,
                    postData, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        ///     创建获客链接
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="link_id">获客链接id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [NcApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerAcquisitionApi.CreateLink", true)]
        public static CreateCustomerAcquisitionLinkResult CreateLink(string accessTokenOrAppKey,
            CreateCustomerAcquisitionLinkRequest param, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(
                    Config.ApiWorkHost + "/cgi-bin/externalcontact/customer_acquisition/create_link?access_token={0}",
                    accessToken.AsUrlData());

                if (param.skip_verify == null) param.skip_verify = true;

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<CreateCustomerAcquisitionLinkResult>(accessToken,
                    url,
                    param, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }


        /// <summary>
        ///     编辑获客链接
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="link_id">获客链接id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [NcApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerAcquisitionApi.ModifyLink", true)]
        public static CreateCustomerAcquisitionLinkResult ModifyLink(string accessTokenOrAppKey,
            ModifyCustomerAcquisitionLinkRequest param, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(
                    Config.ApiWorkHost + "/cgi-bin/externalcontact/customer_acquisition/create_link?access_token={0}",
                    accessToken.AsUrlData());

                if (param.skip_verify == null) param.skip_verify = true;

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<CreateCustomerAcquisitionLinkResult>(accessToken,
                    url,
                    param, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        ///     删除获客链接
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="link_id">获客链接id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [NcApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerAcquisitionApi.DeleteLink", true)]
        public static WorkJsonResult DeleteLink(string accessTokenOrAppKey, string link_id,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(
                    Config.ApiWorkHost + "/cgi-bin/externalcontact/customer_acquisition/delete_link?access_token={0}",
                    accessToken.AsUrlData());

                var param = new
                {
                    link_id = link_id
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(accessToken,
                    url,
                    param, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 异步方法

        /// <summary>
        ///   获取获客链接列表
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="limit">返回的最大记录数，整型，最大值100</param>
        /// <param name="cursor">用于分页查询的游标，字符串类型，由上一次调用返回，首次调用可不填</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [NcApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerAcquisitionApi.GetLinkListAsync", true)]
        public static async Task<GetCustomerAcquisitionLinkListResult> GetLinkListAsync(string accessTokenOrAppKey,
            int limit = 100, string cursor = "", int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(
                    Config.ApiWorkHost + "/cgi-bin/externalcontact/customer_acquisition/list_link?access_token={0}",
                    accessToken.AsUrlData());

                var postData = new
                {
                    limit = limit,
                    cursor = cursor
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend
                    .SendAsync<GetCustomerAcquisitionLinkListResult>(accessToken, url, postData, timeOut: timeOut)
                    .ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        ///     获取获客链接详情
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="link_id">获客链接id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [NcApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerAcquisitionApi.GetLinkDetailAsync", true)]
        public static async Task<GetCustomerAcquisitionLinkDetailResult> GetLinkDetailAsync(string accessTokenOrAppKey,
            string link_id = "", int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                {
                    var url = string.Format(
                        Config.ApiWorkHost + "/cgi-bin/externalcontact/customer_acquisition/get?access_token={0}",
                        accessToken.AsUrlData());

                    var postData = new
                    {
                        link_id = link_id
                    };

                    return await Senparc.Weixin.CommonAPIs.CommonJsonSend
                        .SendAsync<GetCustomerAcquisitionLinkDetailResult>(
                            accessToken,
                            url,
                            postData, CommonJsonSendType.POST, timeOut)
                        .ConfigureAwait(false);
                }, accessTokenOrAppKey)
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     创建获客链接
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="link_id">获客链接id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [NcApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerAcquisitionApi.CreateLinkAsync", true)]
        public static async Task<CreateCustomerAcquisitionLinkResult> CreateLinkAsync(string accessTokenOrAppKey,
            CreateCustomerAcquisitionLinkRequest param, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                {
                    var url = string.Format(
                        Config.ApiWorkHost +
                        "/cgi-bin/externalcontact/customer_acquisition/create_link?access_token={0}",
                        accessToken.AsUrlData());

                    if (param.skip_verify == null) param.skip_verify = true;

                    return await Senparc.Weixin.CommonAPIs.CommonJsonSend
                        .SendAsync<CreateCustomerAcquisitionLinkResult>(
                            accessToken,
                            url,
                            param, CommonJsonSendType.POST, timeOut)
                        .ConfigureAwait(false);
                }, accessTokenOrAppKey)
                .ConfigureAwait(false);
        }


        /// <summary>
        ///     编辑获客链接
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="link_id">获客链接id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [NcApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerAcquisitionApi.ModifyLinkAsync", true)]
        public static async Task<CreateCustomerAcquisitionLinkResult> ModifyLinkAsync(string accessTokenOrAppKey,
            ModifyCustomerAcquisitionLinkRequest param, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                {
                    var url = string.Format(
                        Config.ApiWorkHost +
                        "/cgi-bin/externalcontact/customer_acquisition/create_link?access_token={0}",
                        accessToken.AsUrlData());

                    if (param.skip_verify == null) param.skip_verify = true;

                    return await Senparc.Weixin.CommonAPIs.CommonJsonSend
                        .SendAsync<CreateCustomerAcquisitionLinkResult>(
                            accessToken,
                            url,
                            param, CommonJsonSendType.POST, timeOut)
                        .ConfigureAwait(false);
                }, accessTokenOrAppKey)
                .ConfigureAwait(false);
        }

        /// <summary>
        ///     删除获客链接
        /// </summary>
        /// <param name="accessTokenOrAppKey"></param>
        /// <param name="link_id">获客链接id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [NcApiBind(NeuChar.PlatformType.WeChat_Work, "CustomerAcquisitionApi.DeleteLinkAsync", true)]
        public static async Task<WorkJsonResult> DeleteLinkAsync(string accessTokenOrAppKey, string link_id,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                {
                    var url = string.Format(
                        Config.ApiWorkHost +
                        "/cgi-bin/externalcontact/customer_acquisition/delete_link?access_token={0}",
                        accessToken.AsUrlData());

                    var param = new
                    {
                        link_id = link_id
                    };

                    return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken,
                            url,
                            param, CommonJsonSendType.POST, timeOut)
                        .ConfigureAwait(false);
                }, accessTokenOrAppKey)
                .ConfigureAwait(false);
        }

        #endregion
    }
}