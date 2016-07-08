/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：StoreApi.cs
    文件功能描述：门店管理接口
    
    
    创建标识：Senparc - 20150513
    
    修改标识：Senparc - 201501018
    修改描述：修改UploadImage()方法bug

----------------------------------------------------------------*/

/* 
   开发文档下载地址：https://mp.weixin.qq.com/zh_CN/htmledition/comm_htmledition/res/store_manage/store_manage_file.zip
*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.Poi;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    #region 商户在使用门店管理接口时需注意以下几个问题：

    /*
     *      门店信息全部需要经过审核方能生效，门店创建完成后，只会返回创建成功提示，并不能
        获得poi_id，只有经过审核后才能获取poi_id，收到微信推送的审核结果通知，并使用在微
        信各个业务场景中；
     *      为保证在审核通过后，获取到的poi_id 能与商户自身数据做对应，将会允许商户在创建时
        提交自己内部或自定义的sid(字符串格式，微信不会对唯一性进行校验，请商户自己保证)，
        用于后续获取poi_id 后对数据进行对应；
     *      门店的可用状态available_state，将标记门店相应审核状态，只有审核通过状态，才能进行
        更新，更新字段仅限扩展字段（表1 中前11 个字段）；
     *      扩展字段属于公共编辑信息，提交更新后将由微信进行审核采纳，但扩展字段更新并不影
        响门店的可用状态（即available_state 仍为审核通过），但update_status 状态变为1，更新中
        状态，此时不可再次对门店进行更新，直到微信审核采纳后；
     *      在update_status 为1，更新中状态下的门店，此时调用getpoi 接口，获取到的扩展字段为更
        新的最新字段，但并不是最终结果，仍需等待微信编辑对扩展字段的建议进行采纳后，最
        终决定是否生效（有可能更新字段不被采纳）；
     */

    #endregion


    /// <summary>
    /// 门店管理接口
    /// </summary>
    public static class PoiApi
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="file">文件路径</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static UploadImageResultJson UploadImage(string accessTokenOrAppId, string file, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token={0}", accessToken.AsUrlData());

                var fileDictionary = new Dictionary<string, string>();
                fileDictionary["media"] = file;
                return Post.PostFileGetJson<UploadImageResultJson>(url, null, fileDictionary, null, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建门店
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="createStoreData"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult AddPoi(string accessTokenOrAppId, CreateStoreData createStoreData,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("http://api.weixin.qq.com/cgi-bin/poi/addpoi?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<WxJsonResult>(null, url, createStoreData, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询门店信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="poiId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetStoreResultJson GetPoi(string accessTokenOrAppId, string poiId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("http://api.weixin.qq.com/cgi-bin/poi/getpoi?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    poi_id = poiId
                };

                return CommonJsonSend.Send<GetStoreResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询门店列表
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="begin">开始位置，0 即为从第一条开始查询</param>
        /// <param name="limit">返回数据条数，最大允许50，默认为20</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetStoreListResultJson GetPoiList(string accessTokenOrAppId, int begin, int limit = 20, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/poi/getpoilist?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    begin = begin,
                    limit = limit
                };

                return CommonJsonSend.Send<GetStoreListResultJson>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除门店
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="poiId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult DeletePoi(string accessTokenOrAppId, string poiId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/poi/delpoi?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    poi_id = poiId
                };

                return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 修改门店服务信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="updateStoreData">修改门店服务信息需要Post的数据</param>
        /// 商户可以通过该接口，修改门店的服务信息，包括：图片列表、营业时间、推荐、特色服务、简介、人均价格、电话7 个字段。目前基础字段包括（名称、坐标、地址等不可修改）
        /// 若有填写内容则为覆盖更新，若无内容则视为不修改，维持原有内容。
        /// photo_list 字段为全列表覆盖，若需要增加图片，需将之前图片同样放入list 中，在其后增加新增图片。如：已有A、B、C 三张图片，又要增加D、E 两张图，则需要调用该接口，photo_list 传入A、B、C、D、E 五张图片的链接。
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult UpdatePoi(string accessTokenOrAppId, UpdateStoreData updateStoreData, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("https://api.weixin.qq.com/cgi-bin/poi/updatepoi?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<WxJsonResult>(null, url, updateStoreData, CommonJsonSendType.POST, timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取门店类目表
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <returns></returns>
        public static GetCategoryResult GetCategory(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("http://api.weixin.qq.com/cgi-bin/api_getwxcategory?access_token={0}", accessToken.AsUrlData());

                return CommonJsonSend.Send<GetCategoryResult>(null, url, null, CommonJsonSendType.GET);

            }, accessTokenOrAppId);
        }
    }
}
