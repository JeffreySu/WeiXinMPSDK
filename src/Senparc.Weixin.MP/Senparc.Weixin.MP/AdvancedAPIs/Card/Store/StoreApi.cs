#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
 
    文件名：StoreApi.cs
    文件功能描述：门店接口
    
    
    创建标识：Senparc - 20180927

    
----------------------------------------------------------------*/


using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 门店接口
    /// </summary>
    public static class StoreApi
    {
        #region 同步方法

        /// <summary>
        /// 拉取门店小程序类目
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.GetMerchantCategory", true)]
        public static GetMerchantCategoryResultJson GetMerchantCategory(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/get_merchant_category?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<GetMerchantCategoryResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建门店小程序
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.ApplyMerchant", true)]
        public static WxJsonResult ApplyMerchant(string accessTokenOrAppId, ApplyMerchantData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/apply_merchant?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询门店小程序审核结果
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.GetMerchantAuditInfo", true)]
        public static GetMerchantAuditInfoResultJson GetMerchantAuditInfo(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/get_merchant_audit_info?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<GetMerchantAuditInfoResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 修改门店小程序信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="headingMediaid"></param>
        /// <param name="intro"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.ModifyMerchant", true)]
        public static WxJsonResult ModifyMerchant(string accessTokenOrAppId, string headingMediaid, string intro, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/modify_merchant?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    headimg_mediaid = headingMediaid,
                    intro
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 从腾讯地图拉取省市区信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.GetDistrict", true)]
        public static GetDistrictResultJson GetDistrict(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/get_district?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<GetDistrictResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 在腾讯地图中搜索门店
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="districtId"></param>
        /// <param name="keyword"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.SearchMapPoi", true)]
        public static SearchMapPoiResultJson SearchMapPoi(string accessTokenOrAppId, string districtId, string keyword, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/search_map_poi?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    districtid = districtId,
                    keyword
                };
                return CommonJsonSend.Send<SearchMapPoiResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 在腾讯地图中创建门店
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.CreateMapPoi", true)]
        public static CreateMapPoiResultJson CreateMapPoi(string accessTokenOrAppId, CreateMapPoiData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/create_map_poi?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<CreateMapPoiResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 添加门店
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.AddStore", true)]
        public static AddStoreResultJson AddStore(string accessTokenOrAppId, AddStoreData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/add_store?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<AddStoreResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更新门店信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.UpdateStore", true)]
        public static UpdateStoreResultJson UpdateStore(string accessTokenOrAppId, UpdateStoreData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/update_store?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<UpdateStoreResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取单个门店信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="poiId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.GetStoreInfo", true)]
        public static GetStoreInfoResultJson GetStoreInfo(string accessTokenOrAppId, string poiId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/get_store_info?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    poi_id = poiId
                };
                return CommonJsonSend.Send<GetStoreInfoResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取门店信息列表
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.GetStoreList", true)]
        public static GetStoreListResultJson GetStoreList(string accessTokenOrAppId, int offset, int limit, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/get_store_list?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    offset = offset,
                    limit = limit
                };
                return CommonJsonSend.Send<GetStoreListResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="poiId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.DelStore", true)]
        public static WxJsonResult DelStore(string accessTokenOrAppId, string poiId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/del_store?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    poi_id = poiId
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取门店小程序配置的卡券
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="poiId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.GetStoreCard", true)]
        public static GetStoreCardResultJson GetStoreCard(string accessTokenOrAppId, string poiId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/storewxa/get?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    poi_id = poiId
                };
                return CommonJsonSend.Send<GetStoreCardResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置门店小程序配置的卡券
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="poiId"></param>
        /// <param name="cardId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.SetStoreCard", true)]
        public static WxJsonResult SetStoreCard(string accessTokenOrAppId, string poiId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/storewxa/set?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    poi_id = poiId,
                    card_id = cardId
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】拉取门店小程序类目
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.GetMerchantCategoryAsync", true)]
        public static async Task<GetMerchantCategoryResultJson> GetMerchantCategoryAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/get_merchant_category?access_token={0}", accessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<GetMerchantCategoryResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】创建门店小程序
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.ApplyMerchantAsync", true)]
        public static async Task<WxJsonResult> ApplyMerchantAsync(string accessTokenOrAppId, ApplyMerchantData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/apply_merchant?access_token={0}", accessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】查询门店小程序审核结果
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.GetMerchantAuditInfoAsync", true)]
        public static async Task<GetMerchantAuditInfoResultJson> GetMerchantAuditInfoAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/get_merchant_audit_info?access_token={0}", accessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<GetMerchantAuditInfoResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】修改门店小程序信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="headingMediaid"></param>
        /// <param name="intro"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.ModifyMerchantAsync", true)]
        public static async Task<WxJsonResult> ModifyMerchantAsync(string accessTokenOrAppId, string headingMediaid, string intro, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/modify_merchant?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    headimg_mediaid = headingMediaid,
                    intro
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】从腾讯地图拉取省市区信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.GetDistrictAsync", true)]
        public static async Task<GetDistrictResultJson> GetDistrictAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/get_district?access_token={0}", accessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<GetDistrictResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】在腾讯地图中搜索门店
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="districtId"></param>
        /// <param name="keyword"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.SearchMapPoiAsync", true)]
        public static async Task<SearchMapPoiResultJson> SearchMapPoiAsync(string accessTokenOrAppId, string districtId, string keyword, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/search_map_poi?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    districtid = districtId,
                    keyword
                };
                return await CommonJsonSend.SendAsync<SearchMapPoiResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】在腾讯地图中创建门店
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.CreateMapPoiAsync", true)]
        public static async Task<CreateMapPoiResultJson> CreateMapPoiAsync(string accessTokenOrAppId, CreateMapPoiData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/create_map_poi?access_token={0}", accessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<CreateMapPoiResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】添加门店
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.AddStoreAsync", true)]
        public static async Task<AddStoreResultJson> AddStoreAsync(string accessTokenOrAppId, AddStoreData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/add_store?access_token={0}", accessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<AddStoreResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】更新门店信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.UpdateStoreAsync", true)]
        public static async Task<UpdateStoreResultJson> UpdateStoreAsync(string accessTokenOrAppId, UpdateStoreData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/update_store?access_token={0}", accessToken.AsUrlData());

                return await CommonJsonSend.SendAsync<UpdateStoreResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取单个门店信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="poiId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.GetStoreInfoAsync", true)]
        public static async Task<GetStoreInfoResultJson> GetStoreInfoAsync(string accessTokenOrAppId, string poiId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/get_store_info?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    poi_id = poiId
                };
                return await CommonJsonSend.SendAsync<GetStoreInfoResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取门店信息列表
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.GetStoreListAsync", true)]
        public static async Task<GetStoreListResultJson> GetStoreListAsync(string accessTokenOrAppId, int offset, int limit, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/get_store_list?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    offset = offset,
                    limit = limit
                };
                return await CommonJsonSend.SendAsync<GetStoreListResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】删除门店
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="poiId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.DelStoreAsync", true)]
        public static async Task<WxJsonResult> DelStoreAsync(string accessTokenOrAppId, string poiId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/wxa/del_store?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    poi_id = poiId
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取门店小程序配置的卡券
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="poiId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.GetStoreCardAsync", true)]
        public static async Task<GetStoreCardResultJson> GetStoreCardAsync(string accessTokenOrAppId, string poiId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/storewxa/get?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    poi_id = poiId
                };
                return await CommonJsonSend.SendAsync<GetStoreCardResultJson>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置门店小程序配置的卡券
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="poiId"></param>
        /// <param name="cardId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "StoreApi.SetStoreCardAsync", true)]
        public static async Task<WxJsonResult> SetStoreCardAsync(string accessTokenOrAppId, string poiId, string cardId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/card/storewxa/set?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    poi_id = poiId,
                    card_id = cardId
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion
#endif

    }
}
