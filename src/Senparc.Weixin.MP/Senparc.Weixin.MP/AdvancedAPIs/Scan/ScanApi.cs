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
    
    文件名：ScanApi.cs
    文件功能描述：微信扫一扫
    
    创建描述：增加获取商户信息接口，提交审核/取消发布商品接口，设置测试人员白名单接口，
              批量查询商品信息接口，清除商品信息接口，检查wxticket参数接口
    创建标识：Senparc - 20160520
 
    修改标识：Senparc - 20160719
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

    修改标识：Senparc - 20170810
    修改描述：v14.5.10 增加“获取商品二维码”接口（ScanApi.GetQrCode()），同时提供配套异步方法

----------------------------------------------------------------*/

/*
    官方文档：https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=open1419318587&lang=zh_CN
 */

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;
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
        #region 同步方法


        /// <summary>
        /// 获取商户信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static MerchantInfoGetResultJson MerchantInfoGet(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/merchantinfo/get?access_token={0}", accessToken.AsUrlData());
                return CommonJsonSend.Send<MerchantInfoGetResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <pram name="keyStandard">商品编码标准，暂时只支持ean13和ean8两种标准。</pram>
        /// <pram name="keyStr">商品编码内容。直接填写商品条码，如“6900000000000”；注意：编码标准是ean13时，编码内容必须在商户的号段之下，否则会报错。</pram>
        /// <pram name="baseInfo">商品的基本信息。</pram>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProductCreateResultJson ProductCreate(string accessTokenOrAppId, string keyStandard, string keyStr, BrandInfo brandInfo, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
          {
              var urlFormat = string.Format(Config.ApiMpHost + "/scan/product/create?access_token={0}", accessToken.AsUrlData());
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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="keyStandard">商品编码标准。</param>
        /// <param name="keyStr">商品编码内容。</param>
        /// <param name="status">设置发布状态。on为提交审核，off为取消发布。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ModStatus(string accessTokenOrAppId, string keyStandard, string keyStr, string status, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/product/modstatus?access_token={0}", accessToken.AsUrlData());
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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">测试人员的openid列表。</param>
        /// <param name="userName">测试人员的微信号列表。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult TestWhiteListSet(string accessTokenOrAppId, string openId, string userName, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/testwhitelist/set?access_token={0}", accessToken.AsUrlData());
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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="keyStandard">商品编码标准。</param>
        /// <param name="keyStr">商品编码内容。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProductGetJsonResult ProductGet(string accessTokenOrAppId, string keyStandard, string keyStr, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/product/get?access_token={0}", accessToken.AsUrlData());
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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">商品编码标准。</param>
        /// <param name="limit">商品编码标准。</param>
        /// <param name="status">商品编码内容。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProductGetListJsonResult ProductGetList(string accessTokenOrAppId, int offset, int limit, string status, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/product/getlist?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    offset = offset,
                    limit = limit,
                    status = status
                };
                return CommonJsonSend.Send<ProductGetListJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        ///清除商品信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="keyStandard">商品编码标准。</param>
        /// <param name="keyStr">商品编码标准。</param>

        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ProductClear(string accessTokenOrAppId, int keyStandard, string keyStr, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/product/clear?access_token={0}", accessToken.AsUrlData());
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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="ticket">请求URL中带上的wxticket参数。</param>

        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ScanTicketCheckJsonResult ScanTicketCheck(string accessTokenOrAppId, string ticket, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/scanticket/check?access_token={0}", accessToken.AsUrlData());
                var data = new
                {

                    ticket = ticket

                };
                return CommonJsonSend.Send<ScanTicketCheckJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取商品二维码
        /// 官方文档地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1455872062
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="keystr">商品编码内容</param>
        /// <param name="extinfo">（非必填）由商户自定义传入，建议仅使用大小写字母、数字及-_().*这6个常用字符</param>
        /// <param name="keystandard">商品编码标准</param>
        /// <param name="qrcode_size">二维码的尺寸（整型），数值代表边长像素数，默认为100</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProductGetQrCodeJsonResult GetQrCode(string accessTokenOrAppId, string keystr, string extinfo = null, string keystandard = "ean13", int qrcode_size = 100, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/product/getqrcode?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    keystandard,
                    keystr,
                    extinfo,
                    qrcode_size
                };

                return CommonJsonSend.Send<ProductGetQrCodeJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 【异步方法】获取商户信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<MerchantInfoGetResultJson> MerchantInfoGetAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/merchantinfo/get?access_token={0}", accessToken.AsUrlData());
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<MerchantInfoGetResultJson>(null, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】创建商品
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <pram name="keyStandard">商品编码标准，暂时只支持ean13和ean8两种标准。</pram>
        /// <pram name="keyStr">商品编码内容。直接填写商品条码，如“6900000000000”；注意：编码标准是ean13时，编码内容必须在商户的号段之下，否则会报错。</pram>
        /// <pram name="baseInfo">商品的基本信息。</pram>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ProductCreateResultJson> ProductCreateAsync(string accessTokenOrAppId, string keyStandard, string keyStr, BrandInfo brandInfo, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
          {
              var urlFormat = string.Format(Config.ApiMpHost + "/scan/product/create?access_token={0}", accessToken.AsUrlData());
              var data = new
              {
                  keystandard = keyStandard,
                  keystr = keyStr,
                  brand_info = brandInfo



              };
              return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<ProductCreateResultJson>(null, urlFormat, null, CommonJsonSendType.POST, timeOut: timeOut);
          }, accessTokenOrAppId);
        }
        /// <summary>
        ///【异步方法】提交审核/取消发布商品
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="keyStandard">商品编码标准。</param>
        /// <param name="keyStr">商品编码内容。</param>
        /// <param name="status">设置发布状态。on为提交审核，off为取消发布。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ModStatusAsync(string accessTokenOrAppId, string keyStandard, string keyStr, string status, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/product/modstatus?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    keystandard = keyStandard,
                    keystr = keyStr,
                    status = status
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        ///【异步方法】设置测试人员白名单
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="openId">测试人员的openid列表。</param>
        /// <param name="userName">测试人员的微信号列表。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> TestWhiteListSetAsync(string accessTokenOrAppId, string openId, string userName, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/testwhitelist/set?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    openid = openId,
                    userName = userName
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /*
        /// <summary>
        ///【异步方法】查询商品信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="keyStandard">商品编码标准。</param>
        /// <param name="keyStr">商品编码内容。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProductGetJsonResult ProductGet(string accessTokenOrAppId, string keyStandard, string keyStr, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/product/get?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    keystandard = keyStandard,
                    keystr = keyStr
                };
                return CommonJsonSend.Send<ProductGetJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }*/
        /// <summary>
        ///【异步方法】批量查询商品信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="offset">商品编码标准。</param>
        /// <param name="limit">商品编码标准。</param>
        /// <param name="status">商品编码内容。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ProductGetListJsonResult> ProductGetListAsync(string accessTokenOrAppId, int offset, int limit, string status, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/product/getlist?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    offset = offset,
                    limit = limit,
                    status = status
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<ProductGetListJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        ///【异步方法】清除商品信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="keyStandard">商品编码标准。</param>
        /// <param name="keyStr">商品编码标准。</param>

        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> ProductClearAsync(string accessTokenOrAppId, int keyStandard, string keyStr, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/product/clear?access_token={0}", accessToken.AsUrlData());
                var data = new
                {
                    keystandard = keyStandard,
                    keystr = keyStr

                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        ///【异步方法】检查wxticket参数
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>

        /// <param name="ticket">请求URL中带上的wxticket参数。</param>

        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ScanTicketCheckJsonResult> ScanTicketCheckAsync(string accessTokenOrAppId, string ticket, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/scanticket/check?access_token={0}", accessToken.AsUrlData());
                var data = new
                {

                    ticket = ticket

                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<ScanTicketCheckJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取商品二维码
        /// 官方文档地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1455872062
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="keystr">商品编码内容</param>
        /// <param name="extinfo">（非必填）由商户自定义传入，建议仅使用大小写字母、数字及-_().*这6个常用字符</param>
        /// <param name="keystandard">商品编码标准</param>
        /// <param name="qrcode_size">二维码的尺寸（整型），数值代表边长像素数，默认为100</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ProductGetQrCodeJsonResult> GetQrCodeAsync(string accessTokenOrAppId, string keystr, string extinfo = null, string keystandard = "ean13", int qrcode_size = 100, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = string.Format(Config.ApiMpHost + "/scan/product/getqrcode?access_token={0}", accessToken.AsUrlData());

                var data = new
                {
                    keystandard,
                    keystr,
                    extinfo,
                    qrcode_size
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<ProductGetQrCodeJsonResult>(null, urlFormat, data, CommonJsonSendType.POST, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        #endregion
#endif
    }
}
