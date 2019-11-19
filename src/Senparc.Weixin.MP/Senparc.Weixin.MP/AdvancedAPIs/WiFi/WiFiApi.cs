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
    
    文件名：WiFiApi.cs
    文件功能描述：微信连WiFi接口
    
    
    创建标识：Senparc - 20150709
 
    修改标识：Senparc - 20160506
    修改描述：添加“获取公众号连网URL”接口（GetConnectUrl）
 
    修改标识：Senparc - 20160511
    修改描述：WiFiApi.AddDevice去掉bssid参数
 
    修改标识：Senparc - 20160520
    修改描述：添加获取Wi-Fi门店列表接口，查询门店Wi-Fi信息接口，修改门店网络信息接口，清空门店网络及设备接口，
              添加portal型设备接口，设置微信首页欢迎语接口，设置连网完成页接口，设置门店卡券投放信息接口，
              查询门店卡券投放信息接口，第三方平台获取开插件wifi_token接口
 
    修改标识：Senparc - 20160719
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await
----------------------------------------------------------------*/

/*
    官方文档：http://mp.weixin.qq.com/wiki/10/6232005bdc497f7cf8e19d4e843c70d2.html
 */

using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs.WiFi;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 
    /// </summary>
    public static class WiFiApi
    {
        #region 同步方法
        
      
        /// <summary>
        /// 获取Wi-Fi门店列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="pageIndex">分页下标，默认从1开始</param>
        /// <param name="pageSize">每页的个数，默认10个，最大20个</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.ShopList", true)]
        public static WiFiShopListJsonResult ShopList(string accessTokenOrAppId, int pageIndex = 1, int pageSize = 10, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/shop/list?access_token={0}";

                var data = new
                {
                    pageindex = pageIndex,
                    pagesize = pageSize
                };
                return CommonJsonSend.Send<WiFiShopListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 查询门店Wi-Fi信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="pageindex">分页下标，默认从1开始</param>
        /// <param name="pagesize">每页的个数，默认10个，最大20个</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.ShopGet", true)]
        public static WiFiShopGetJsonResult ShopGet(string accessTokenOrAppId, long shopId, int pageindex = 1, int pagesize = 10, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/shop/get?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    pageindex = pageindex,
                    pagesize = pagesize
                };
                return CommonJsonSend.Send<WiFiShopGetJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 修改门店网络信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="oldSsid">需要修改的ssid，当门店下有多个ssid时，必填</param>
        /// <param name="ssid">无线网络设备的ssid。32个字符以内；ssid支持中文，但可能因设备兼容性问题导致显示乱码，或无法连接等问题，相关风险自行承担！当门店下是portal型设备时，ssid必填；当门店下是密码型设备时，ssid选填，且ssid和密码必须有一个以大写字母“WX”开头</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.ShopUpdate", true)]
        public static WxJsonResult ShopUpdate(string accessTokenOrAppId, long shopId, string oldSsid, string ssid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/shop/update?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    old_ssid = oldSsid,
                    ssid = ssid

                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 清空门店网络及设备
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="ssid">无线网络设备的ssid。若不填写ssid，默认为清空门店下所有设备；填写ssid则为清空该ssid下的所有设备</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.ShopClean", true)]
        public static WxJsonResult ShopClean(string accessTokenOrAppId, long shopId, string ssid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/shop/clean?access_token={0}";
                var data = new object();
                if (string.IsNullOrEmpty(ssid))
                {
                    data = new
                    {
                        shop_id = shopId

                    };
                }
                else
                {
                    data = new
                    {
                        shop_id = shopId,
                        ssid = ssid

                    };
                }

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="ssid">无线网络设备的ssid，不能包含中文字符，必需是“WX”开头(“WX”为大写字母)</param>
        /// <param name="password">无线网络设备的密码，大于8个字符，不能包含中文字符</param>
        ///// <param name="bssid">无线网络设备无线mac地址，格式冒号分隔，字符长度17个，并且字母小写，例如：00:1f:7a:ad:5c:a8</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.AddDevice", true)]
        public static WxJsonResult AddDevice(string accessTokenOrAppId, long shopId, string ssid, string password,
            /*string bssid,*/ int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/device/add?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    ssid = ssid,
                    password = password,
                    //bssid = bssid,
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 添加portal型设备
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="ssid">无线网络设备的ssid，限30个字符以内。ssid支持中文，但可能因设备兼容性问题导致显示乱码，或无法连接等问题，相关风险自行承担！</param>
        /// <param name="reset">重置secretkey，false-不重置，true-重置，默认为false</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.WifeRegister", true)]
        public static WiFiRegisterJsonResult WifeRegister(string accessTokenOrAppId, long shopId, string ssid, string reset,
           int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/apportal/register?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    ssid = ssid,
                    reset = reset,
                };
                return CommonJsonSend.Send<WiFiRegisterJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 查询设备
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="pageIndex">分页下标，默认从1开始</param>
        /// <param name="pageSize">每页的个数，默认10个，最大20个</param>
        /// <param name="shopId">根据门店id查询</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.GetDeviceList", true)]
        public static GetDeviceListResult GetDeviceList(string accessTokenOrAppId, int pageIndex = 1, int pageSize = 10,
            long? shopId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/device/list?access_token={0}";

                object data = new object();

                if (shopId.HasValue)
                {
                    data = new
                    {
                        pageindex = pageIndex,
                        pagesize = pageSize,
                        shop_id = shopId,
                    };
                }
                else
                {
                    data = new
                    {
                        pageindex = pageIndex,
                        pagesize = pageSize
                    };
                }

                return CommonJsonSend.Send<GetDeviceListResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="bssid">需要删除的无线网络设备无线mac地址，格式冒号分隔，字符长度17个，并且字母小写，例如：00:1f:7a:ad:5c:a8</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.DeleteDevice", true)]
        public static WxJsonResult DeleteDevice(string accessTokenOrAppId, string bssid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/device/delete?access_token={0}";

                var data = new
                {
                    bssid = bssid
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取物料二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId"></param>
        /// <param name="imgId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.GetQrcode", true)]
        public static GetQrcodeResult GetQrcode(string accessTokenOrAppId, long shopId, int imgId,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/qrcode/get?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    img_id = imgId
                };

                return CommonJsonSend.Send<GetQrcodeResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置商家主页
        /// 传入自定义链接则是使用自定义链接，否则使用默认模板
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="url">自定义链接（选择传入）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.SetHomePage", true)]
        public static WxJsonResult SetHomePage(string accessTokenOrAppId, long shopId, string url = null,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/homepage/set?access_token={0}";

                var data = new object();

                if (string.IsNullOrEmpty(url))
                {
                    data = new
                    {
                        shop_id = shopId,
                        template_id = 0
                    };
                }
                else
                {
                    data = new
                    {
                        shop_id = shopId,
                        template_id = 1,
                        @struct = new
                        {
                            url = url
                        }
                    };
                }

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询商家主页
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">查询的门店id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.GetHomePage", true)]
        public static GetHomePageResult GetHomePage(string accessTokenOrAppId, long shopId,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/homepage/get?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                };

                return CommonJsonSend.Send<GetHomePageResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置微信首页欢迎语
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="barType">微信首页欢迎语的文本内容：0--欢迎光临+公众号名称；1--欢迎光临+门店名称；2--已连接+公众号名称+WiFi；3--已连接+门店名称+Wi-Fi。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.SetBar", true)]
        public static WxJsonResult SetBar(string accessTokenOrAppId, long shopId, int barType,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/bar/set?access_token={0}";

                var data = new
                    {
                        shop_id = shopId,
                        bar_type = barType
                    };


                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 设置连网完成页
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="finishPageUrl">连网完成页URL。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.SetFinishpage", true)]
        public static WxJsonResult SetFinishpage(string accessTokenOrAppId, long shopId, string finishPageUrl,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/finishpage/set?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    finishpage_url = finishPageUrl
                };


                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 数据统计
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">起始日期时间，格式yyyy-mm-dd，最长时间跨度为30天</param>
        /// <param name="endDate">结束日期时间戳，格式yyyy-mm-dd，最长时间跨度为30天</param>
        /// <param name="shopId">按门店ID搜索，-1为总统计</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.GetStatistics", true)]
        public static GetStatisticsResult GetStatistics(string accessTokenOrAppId, string beginDate, string endDate,
            long shopId = -1,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/statistics/list?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate,
                    shop_id = shopId,
                };

                return CommonJsonSend.Send<GetStatisticsResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 设置门店卡券投放信息

        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID，可设置为0，表示所有门店</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="cardDescribe">卡券描述，不能超过18个字符</param>
        ///<param name="starTime">卡券投放开始时间（单位是秒）</param>
        /// <param name="endTime">卡券投放结束时间（单位是秒）注：不能超过卡券的有效期时间</param>
        /// <param name="cardQuantity">卡券库存</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.SetCouponPut", true)]
        public static WxJsonResult SetCouponPut(string accessTokenOrAppId, long shopId, string cardId, string cardDescribe, string starTime, string endTime, int cardQuantity,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/couponput/set?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    card_id = cardId,
                    card_describe = cardDescribe,
                    start_time = starTime,
                    end_time = endTime,
                    card_quantity = cardQuantity
                };


                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 查询门店卡券投放信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID，可设置为0，表示所有门店</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.GetCouponPut", true)]
        public static WiFiGetCouponPutJsonResult GetCouponPut(string accessTokenOrAppId, long shopId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/couponput/get?access_token={0}";

                var data = new
                {
                    shop_id = shopId
                };


                return CommonJsonSend.Send<WiFiGetCouponPutJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取公众号连网URL
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.GetConnectUrl", true)]
        public static WiFiConnectUrlResultJson GetConnectUrl(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/account/get_connecturl?access_token={0}";

                return CommonJsonSend.Send<WiFiConnectUrlResultJson>(accessToken, urlFormat, null,
                    CommonJsonSendType.GET);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 第三方平台获取开插件wifi_token
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="callBackUrl">回调URL，开通插件成功后的跳转页面。注：该参数域名必须与跳转进开通插件页面的页面域名保持一致，建议均采用第三方平台域名。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.OpenPluginToken", true)]
        public static WiFiOpenPluginTokenJsonResult OpenPluginToken(string accessTokenOrAppId, string callBackUrl, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/openplugin/token?access_token={0}";

                var data = new
                {
                    callback_url = callBackUrl
                };


                return CommonJsonSend.Send<WiFiOpenPluginTokenJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】获取Wi-Fi门店列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="pageIndex">分页下标，默认从1开始</param>
        /// <param name="pageSize">每页的个数，默认10个，最大20个</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.ShopListAsync", true)]
        public static async Task<WiFiShopListJsonResult> ShopListAsync(string accessTokenOrAppId, int pageIndex = 1, int pageSize = 10, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/shop/list?access_token={0}";

                var data = new
                {
                    pageindex = pageIndex,
                    pagesize = pageSize
                };
                return await Senparc .Weixin .CommonAPIs .CommonJsonSend.SendAsync<WiFiShopListJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】查询门店Wi-Fi信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="pageindex">分页下标，默认从1开始</param>
        /// <param name="pagesize">每页的个数，默认10个，最大20个</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.ShopGetAsync", true)]
        public static async Task<WiFiShopGetJsonResult> ShopGetAsync(string accessTokenOrAppId, long shopId, int pageindex = 1, int pagesize = 10, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/shop/get?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    pageindex = pageindex,
                    pagesize = pagesize
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WiFiShopGetJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】修改门店网络信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="oldSsid">需要修改的ssid，当门店下有多个ssid时，必填</param>
        /// <param name="ssid">无线网络设备的ssid。32个字符以内；ssid支持中文，但可能因设备兼容性问题导致显示乱码，或无法连接等问题，相关风险自行承担！当门店下是portal型设备时，ssid必填；当门店下是密码型设备时，ssid选填，且ssid和密码必须有一个以大写字母“WX”开头</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.ShopUpdateAsync", true)]
        public static async Task<WxJsonResult> ShopUpdateAsync(string accessTokenOrAppId, long shopId, string oldSsid, string ssid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/shop/update?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    old_ssid = oldSsid,
                    ssid = ssid

                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】清空门店网络及设备
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="ssid">无线网络设备的ssid。若不填写ssid，默认为清空门店下所有设备；填写ssid则为清空该ssid下的所有设备</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.ShopCleanAsync", true)]
        public static async Task<WxJsonResult> ShopCleanAsync(string accessTokenOrAppId, long shopId, string ssid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/shop/clean?access_token={0}";
                var data = new object();
                if (string.IsNullOrEmpty(ssid))
                {
                    data = new
                    {
                        shop_id = shopId

                    };
                }
                else
                {
                    data = new
                    {
                        shop_id = shopId,
                        ssid = ssid

                    };
                }

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        ///  【异步方法】添加设备
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="ssid">无线网络设备的ssid，不能包含中文字符，必需是“WX”开头(“WX”为大写字母)</param>
        /// <param name="password">无线网络设备的密码，大于8个字符，不能包含中文字符</param>
        ///// <param name="bssid">无线网络设备无线mac地址，格式冒号分隔，字符长度17个，并且字母小写，例如：00:1f:7a:ad:5c:a8</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.AddDeviceAsync", true)]
        public static async Task<WxJsonResult> AddDeviceAsync(string accessTokenOrAppId, long shopId, string ssid, string password,
            /*string bssid,*/ int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/device/add?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    ssid = ssid,
                    password = password,
                    //bssid = bssid,
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        ///  【异步方法】添加portal型设备
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="ssid">无线网络设备的ssid，限30个字符以内。ssid支持中文，但可能因设备兼容性问题导致显示乱码，或无法连接等问题，相关风险自行承担！</param>
        /// <param name="reset">重置secretkey，false-不重置，true-重置，默认为false</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.WifeRegisterAsync", true)]
        public static async Task<WiFiRegisterJsonResult> WifeRegisterAsync(string accessTokenOrAppId, long shopId, string ssid, string reset,
           int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/apportal/register?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    ssid = ssid,
                    reset = reset,
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WiFiRegisterJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }


        /// <summary>
        ///  【异步方法】查询设备
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="pageIndex">分页下标，默认从1开始</param>
        /// <param name="pageSize">每页的个数，默认10个，最大20个</param>
        /// <param name="shopId">根据门店id查询</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.GetDeviceListAsync", true)]
        public static async Task<GetDeviceListResult> GetDeviceListAsync(string accessTokenOrAppId, int pageIndex = 1, int pageSize = 10,
            long? shopId = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/device/list?access_token={0}";

                object data = new object();

                if (shopId.HasValue)
                {
                    data = new
                    {
                        pageindex = pageIndex,
                        pagesize = pageSize,
                        shop_id = shopId,
                    };
                }
                else
                {
                    data = new
                    {
                        pageindex = pageIndex,
                        pagesize = pageSize
                    };
                }

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetDeviceListResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】删除设备
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="bssid">需要删除的无线网络设备无线mac地址，格式冒号分隔，字符长度17个，并且字母小写，例如：00:1f:7a:ad:5c:a8</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.DeleteDeviceAsync", true)]
        public static async Task<WxJsonResult> DeleteDeviceAsync(string accessTokenOrAppId, string bssid, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/device/delete?access_token={0}";

                var data = new
                {
                    bssid = bssid
                };

                return await Senparc .Weixin .CommonAPIs .CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取物料二维码
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId"></param>
        /// <param name="imgId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.GetQrcodeAsync", true)]
        public static async Task<GetQrcodeResult> GetQrcodeAsync(string accessTokenOrAppId, long shopId, int imgId,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/qrcode/get?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    img_id = imgId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetQrcodeResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】设置商家主页
        /// 传入自定义链接则是使用自定义链接，否则使用默认模板
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="url">自定义链接（选择传入）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.SetHomePageAsync", true)]
        public static async Task<WxJsonResult> SetHomePageAsync(string accessTokenOrAppId, long shopId, string url = null,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/homepage/set?access_token={0}";

                var data = new object();

                if (string.IsNullOrEmpty(url))
                {
                    data = new
                    {
                        shop_id = shopId,
                        template_id = 0
                    };
                }
                else
                {
                    data = new
                    {
                        shop_id = shopId,
                        template_id = 1,
                        @struct = new
                        {
                            url = url
                        }
                    };
                }

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        ///【异步方法】 查询商家主页
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">查询的门店id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.GetHomePageaAsync", true)]
        public static async Task<GetHomePageResult> GetHomePageaAsync(string accessTokenOrAppId, long shopId,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/homepage/get?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetHomePageResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        ///【异步方法】 设置微信首页欢迎语

        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="barType">微信首页欢迎语的文本内容：0--欢迎光临+公众号名称；1--欢迎光临+门店名称；2--已连接+公众号名称+WiFi；3--已连接+门店名称+Wi-Fi。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.SetBarAsync", true)]
        public static async Task<WxJsonResult> SetBarAsync(string accessTokenOrAppId, long shopId, int barType,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/bar/set?access_token={0}";

                var data = new
                    {
                        shop_id = shopId,
                        bar_type = barType
                    };


                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        ///【异步方法】 设置连网完成页

        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID</param>
        /// <param name="finishPageUrl">连网完成页URL。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.SetFinishpageAsync", true)]
        public static async Task<WxJsonResult> SetFinishpageAsync(string accessTokenOrAppId, long shopId, string finishPageUrl,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/finishpage/set?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    finishpage_url = finishPageUrl
                };


                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】数据统计
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">起始日期时间，格式yyyy-mm-dd，最长时间跨度为30天</param>
        /// <param name="endDate">结束日期时间戳，格式yyyy-mm-dd，最长时间跨度为30天</param>
        /// <param name="shopId">按门店ID搜索，-1为总统计</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.GetStatisticsAsync", true)]
        public static async Task<GetStatisticsResult> GetStatisticsAsync(string accessTokenOrAppId, string beginDate, string endDate,
            long shopId = -1,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/statistics/list?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate,
                    shop_id = shopId,
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetStatisticsResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        ///【异步方法】 设置门店卡券投放信息

        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID，可设置为0，表示所有门店</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="cardDescribe">卡券描述，不能超过18个字符</param>
        ///<param name="starTime">卡券投放开始时间（单位是秒）</param>
        /// <param name="endTime">卡券投放结束时间（单位是秒）注：不能超过卡券的有效期时间</param>
        /// <param name="cardQuantity">卡券库存</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.SetCouponPutAsync", true)]
        public static async Task<WxJsonResult> SetCouponPutAsync(string accessTokenOrAppId, long shopId, string cardId, string cardDescribe, string starTime, string endTime, int cardQuantity,
            int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/couponput/set?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    card_id = cardId,
                    card_describe = cardDescribe,
                    start_time = starTime,
                    end_time = endTime,
                    card_quantity = cardQuantity
                };


                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】查询门店卡券投放信息
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="shopId">门店ID，可设置为0，表示所有门店</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.GetCouponPutAsync", true)]
        public static async Task<WiFiGetCouponPutJsonResult> GetCouponPutAsync(string accessTokenOrAppId, long shopId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/couponput/get?access_token={0}";

                var data = new
                {
                    shop_id = shopId
                };


                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WiFiGetCouponPutJsonResult>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        ///【异步方法】 获取公众号连网URL
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.GetConnectUrlAsync", true)]
        public static async Task<WiFiConnectUrlResultJson> GetConnectUrlAsync(string accessTokenOrAppId)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/account/get_connecturl?access_token={0}";

                return await  Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WiFiConnectUrlResultJson>(accessToken, urlFormat, null,
                    CommonJsonSendType.GET).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】第三方平台获取开插件wifi_token
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="callBackUrl">回调URL，开通插件成功后的跳转页面。注：该参数域名必须与跳转进开通插件页面的页面域名保持一致，建议均采用第三方平台域名。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "WiFiApi.OpenPluginTokenAsync", true)]
        public static async Task<WiFiOpenPluginTokenJsonResult> OpenPluginTokenAsync(string accessTokenOrAppId, string callBackUrl, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/bizwifi/openplugin/token?access_token={0}";

                var data = new
                {
                    callback_url = callBackUrl
                };


                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WiFiOpenPluginTokenJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.GET, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion

    }
}
