using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.Wxa.MerchantJson;
using Senparc.Weixin.MP.CommonAPIs;
using System.Threading.Tasks;
namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 门店小程序接口
    /// </summary>
    public static class WxaApi
    {

        //门店小程序是公众平台向商户提供的对其线下门店相关功能的管理能力。
        //门店小程序可设置到公众号介绍页、自定义菜单和图文消息中，通过附近关联导入出现在“附近的小程序”，也可应用在卡券、广告、WIFI等业务使用。
        //门店小程序接口是为商户提供批量新增、查询、修改、删除门店等主要功能，包括创建小程序商家账号，方便商户快速高效进行门店管理和操作。
        //备注：原门店管理权限可通过升级为门店小程序使用相关权限
        //门店小程序权限开放给所有非个人公众号；拥有旧门店管理权限的公众号可通过升级获取门店小程序权限。
        //曾授权第三方旧门店管理权限集的公众号，门店小程序权限集默认授权原第三方。
        #region 同步方法

        /// <summary>
        /// 拉取门店小程序类目
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns>小程序类目</returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.GetMerchantCategoryResult", true)]
        public static GetMerchantCategoryResult GetMerchantCategoryResult(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/get_merchant_category?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<GetMerchantCategoryResult>(null, url, null, CommonJsonSendType.GET, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建门店小程序
        /// <para>创建门店小程序提交后需要公众号管理员确认通过后才可进行审核。如果主管理员24小时超时未确认，才能再次提交。</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">申请数据</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.ApplyMerchant", true)]
        public static WxJsonResult ApplyMerchant(string accessTokenOrAppId, ApplyMerchantData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/apply_merchant?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询门店小程序审核结果
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.GetMerchantAuditInfo", true)]
        public static GetMerchantAuditInfoJson GetMerchantAuditInfo(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/get_merchant_audit_info?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<GetMerchantAuditInfoJson>(null, url, null, CommonJsonSendType.GET, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 修改门店小程序信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="headimg_mediaid">头像 --- 临时素材mediaid 用MediaApi.UploadTemporaryMedia接口得到的</param>
        /// <param name="intro">门店小程序的介绍,如果不想改，参数传空值</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.ModifyMerchant", true)]
        public static WxJsonResult ModifyMerchant(string accessTokenOrAppId, string headimg_mediaid, string intro, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/modify_merchant?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<WxJsonResult>(null, url, new { headimg_mediaid, intro }, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 在腾讯地图中搜索门店
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="districtid">对应 拉取省市区信息接口 中的id字段</param>
        /// <param name="keyword">搜索的关键词</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.SearchMapPoi", true)]
        public static SearchMapPoiJson SearchMapPoi(string accessTokenOrAppId, int districtid, string keyword, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/search_map_poi?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<SearchMapPoiJson>(null, url, new { districtid, keyword }, CommonJsonSendType.GET, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 添加门店
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">门店数据</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.Addstore", true)]
        public static AddStoreJsonResult Addstore(string accessTokenOrAppId, AddStoreJsonData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/add_store?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<AddStoreJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 更新门店信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">门店数据</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.UpdateStore", true)]
        public static UpdateStoreJsonResult UpdateStore(string accessTokenOrAppId, UpdateStoreData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/update_store?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<UpdateStoreJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取单个门店信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="poi_id">为门店小程序添加门店，审核成功后返回的门店id</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.GetStoreInfo", true)]
        public static StoreJsonResult GetStoreInfo(string accessTokenOrAppId, string poi_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/get_store_info?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<StoreJsonResult>(null, url, new { poi_id }, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取门店信息列表
        /// <para>假如某个门店小程序有10个门店，那么offset最大是9。limit参数最大不能超过50，并且如果传入的limit参数是0，那么按默认值20处理。</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="limit">获取门店个数</param>
        /// <param name="offset">获取门店列表的初始偏移位置，从0开始计数</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.GetStoreInfo", true)]
        public static StoreListJsonResult GetStoreInfo(string accessTokenOrAppId, int offset, int limit, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/get_store_info?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<StoreListJsonResult>(null, url, new { offset, limit }, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="poi_id">为门店小程序添加门店，审核成功后返回的门店id</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.DeleteStore", true)]
        public static WxJsonResult DeleteStore(string accessTokenOrAppId, string poi_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/del_store?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<StoreJsonResult>(null, url, new { poi_id }, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }


        #endregion
        #region 异步方法

                /// <summary>
        /// 拉取门店小程序类目
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns>小程序类目</returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.GetMerchantCategoryResult", true)]
        public static async Task<GetMerchantCategoryResult> GetMerchantCategoryResultAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/get_merchant_category?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<GetMerchantCategoryResult>(null, url, null, CommonJsonSendType.GET, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建门店小程序
        /// <para>创建门店小程序提交后需要公众号管理员确认通过后才可进行审核。如果主管理员24小时超时未确认，才能再次提交。</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">申请数据</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.ApplyMerchant", true)]
        public static async Task<WxJsonResult> ApplyMerchantGetMerchantCategoryResultAsync(string accessTokenOrAppId, ApplyMerchantData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/apply_merchant?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 查询门店小程序审核结果
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.GetMerchantAuditInfo", true)]
        public static async Task<GetMerchantAuditInfoJson> GetMerchantAuditInfoAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/get_merchant_audit_info?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<GetMerchantAuditInfoJson>(null, url, null, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 修改门店小程序信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="headimg_mediaid">头像 --- 临时素材mediaid 用MediaApi.UploadTemporaryMedia接口得到的</param>
        /// <param name="intro">门店小程序的介绍,如果不想改，参数传空值</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.ModifyMerchant", true)]
        public static async Task<WxJsonResult> ModifyMerchantAsync(string accessTokenOrAppId, string headimg_mediaid, string intro, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/modify_merchant?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, new { headimg_mediaid, intro }, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 在腾讯地图中搜索门店
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="districtid">对应 拉取省市区信息接口 中的id字段</param>
        /// <param name="keyword">搜索的关键词</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.SearchMapPoi", true)]
        public static async Task<SearchMapPoiJson> SearchMapPoiAsync(string accessTokenOrAppId, int districtid, string keyword, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/search_map_poi?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<SearchMapPoiJson>(null, url, new { districtid, keyword }, CommonJsonSendType.GET, timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 添加门店
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">门店数据</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.Addstore", true)]
        public static async Task<AddStoreJsonResult> AddstoreAsync(string accessTokenOrAppId, AddStoreJsonData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/add_store?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<AddStoreJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 更新门店信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">门店数据</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.UpdateStore", true)]
        public static async Task<UpdateStoreJsonResult> UpdateStoreAsync(string accessTokenOrAppId, UpdateStoreData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/update_store?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<UpdateStoreJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取单个门店信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="poi_id">为门店小程序添加门店，审核成功后返回的门店id</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.GetStoreInfo", true)]
        public static async Task<StoreJsonResult> GetStoreInfoAsync(string accessTokenOrAppId, string poi_id, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/get_store_info?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<StoreJsonResult>(null, url, new { poi_id }, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取门店信息列表
        /// <para>假如某个门店小程序有10个门店，那么offset最大是9。limit参数最大不能超过50，并且如果传入的limit参数是0，那么按默认值20处理。</para>
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="limit">获取门店个数</param>
        /// <param name="offset">获取门店列表的初始偏移位置，从0开始计数</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.GetStoreInfo", true)]
        public static async Task<StoreListJsonResult> GetStoreInfoAsync(string accessTokenOrAppId, int offset, int limit, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/get_store_info?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<StoreListJsonResult>(null, url, new { offset, limit }, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="poi_id">为门店小程序添加门店，审核成功后返回的门店id</param>
        /// <param name="timeOut">请求超时时长</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WxaApi.DeleteStore", true)]
        public static async Task<WxJsonResult> DeleteStoreAsync(string accessTokenOrAppId, string poi_id, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiMpHost + "/wxa/del_store?access_token={0}", accessToken.AsUrlData());
                return await CommonJsonSend.SendAsync<StoreJsonResult>(null, url, new { poi_id }, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion
    }
}
