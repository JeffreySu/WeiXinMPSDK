/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ScanApi.cs
    文件功能描述：微信扫一扫
    
    创建描述：增加获取商户信息接口，提交审核/取消发布商品接口，设置测试人员白名单接口，
              批量查询商品信息接口，清除商品信息接口，检查wxticket参数接口
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/

/*
    官方文档：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1419318587&lang=zh_CN
 */

using System.Collections.Generic;
using System.IO;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.AdvancedAPIs.GroupMessage;
using Senparc.Weixin.MP.AdvancedAPIs.Scan;
using Senparc.Weixin.MP.CommonAPIs;


namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微信扫一扫接口
    /// </summary>
    public static class ScanApi
    {
        /// <summary>
        /// 获取商户信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static MerchantInfoGetResultJson MerchantInfoGet(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/scan/merchantinfo/get?access_token={0}", accessToken.AsUrlData());
                 return CommonJsonSend.Send<MerchantInfoGetResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
       
        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <pram name="keyStandard">商品编码标准，暂时只支持ean13和ean8两种标准。</pram>
        /// <pram name="keyStr">商品编码内容。直接填写商品条码，如“6900000000000”；注意：编码标准是ean13时，编码内容必须在商户的号段之下，否则会报错。</pram>
        /// <pram name="baseInfo">商品的基本信息。</pram>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProductCreateResultJson ProductCreate(string accessTokenOrAppId, string keyStandard, string keyStr, BrandInfo brandInfo, int timeOut = Config.TIME_OUT)
      {
         return ApiHandlerWapper.TryCommonApi(accessToken =>
       {
           var urlFormat = string.Format("https://api.weixin.qq.com/scan/product/create?access_token={0}", accessToken.AsUrlData());
           var data = new
            {
                    keystandard = keyStandard,
                    keystr = keyStr,
                    brand_info = brandInfo



            };
           return CommonJsonSend.Send<ProductCreateResultJson>(null, urlFormat, null, CommonJsonSendType.POST, timeOut: timeOut);
          }, accessTokenOrAppId);
       }
        /// <summary>
        ///提交审核/取消发布商品
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="keyStandard">商品编码标准。</param>
        /// <param name="keyStr">商品编码内容。</param>
        /// <param name="status">设置发布状态。on为提交审核，off为取消发布。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult  ModStatus(string accessTokenOrAppId, string keyStandard, string keyStr, string status, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/scan/product/modstatus?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    keystandard = keyStandard,
                    keystr = keyStr,
                    status = status
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        ///设置测试人员白名单
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId">测试人员的openid列表。</param>
        /// <param name="userName">测试人员的微信号列表。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult TestWhiteListSet(string accessTokenOrAppId, string openId, string userName, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/scan/testwhitelist/set?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    openid = openId,
                    userName = userName
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /*
        /// <summary>
        ///查询商品信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="keyStandard">商品编码标准。</param>
        /// <param name="keyStr">商品编码内容。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProductGetJsonResult ProductGet(string accessTokenOrAppId, string keyStandard, string keyStr, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/scan/product/get?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    keystandard = keyStandard,
                    keystr = keyStr
                };
                return CommonJsonSend.Send<ProductGetJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }*/
        /// <summary>
        ///批量查询商品信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="offset">商品编码标准。</param>
        /// <param name="limit">商品编码标准。</param>
        /// <param name="status">商品编码内容。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProductGetListJsonResult ProductGetList(string accessTokenOrAppId,int offset, int limit, string status, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/scan/product/getlist?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    offset=offset,
                    limit = limit,
                    status = status
                };
                return CommonJsonSend.Send<ProductGetListJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        ///清除商品信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="keyStandard">商品编码标准。</param>
        /// <param name="keyStr">商品编码标准。</param>
    
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ProductClear(string accessTokenOrAppId, int keyStandard, string keyStr, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/scan/product/clear?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    keystandard = keyStandard,
                    keystr = keyStr
                    
                };
                return CommonJsonSend.Send<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        ///检查wxticket参数
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>

        /// <param name="ticket">请求URL中带上的wxticket参数。</param>

        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ScanTicketCheckJsonResult ScanTicketCheck(string accessTokenOrAppId, string ticket, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format("https://api.weixin.qq.com/scan/scanticket/check?access_token={0}", accessToken.AsUrlData());
                var data = new
                {

                    ticket = ticket

                };
                return CommonJsonSend.Send<ScanTicketCheckJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
    }
}
