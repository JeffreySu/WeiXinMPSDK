/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
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
----------------------------------------------------------------*/

/*
    官方文档：http://mp.weixin.qq.com/wiki/10/6232005bdc497f7cf8e19d4e843c70d2.html
 */

using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs.WiFi;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public static class WiFiApi
    {
        /// <summary>
        /// 获取Wi-Fi门店列表
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
       
        /// <param name="pageIndex">分页下标，默认从1开始</param>
        /// <param name="pageSize">每页的个数，默认10个，最大20个</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WiFiShopListJsonResult ShopList(string accessTokenOrAppId, int pageIndex = 1, int pageSize = 10, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/shop/list?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId">门店ID</param>
        /// <param name="pageindex">分页下标，默认从1开始</param>
        /// <param name="pagesize">每页的个数，默认10个，最大20个</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WiFiShopGetJsonResult ShopGet(string accessTokenOrAppId, long shopId, int pageindex=1, int pagesize=10, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/shop/get?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    pageindex=pageindex,
                    pagesize = pagesize
                 };
                return CommonJsonSend.Send<WiFiShopGetJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 修改门店网络信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId">门店ID</param>
        /// <param name="oldSsid">需要修改的ssid，当门店下有多个ssid时，必填</param>
        /// <param name="ssid">无线网络设备的ssid。32个字符以内；ssid支持中文，但可能因设备兼容性问题导致显示乱码，或无法连接等问题，相关风险自行承担！当门店下是portal型设备时，ssid必填；当门店下是密码型设备时，ssid选填，且ssid和密码必须有一个以大写字母“WX”开头</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ShopUpdate(string accessTokenOrAppId, long shopId, string oldSsid,string ssid,int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/shop/update?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId">门店ID</param>
        /// <param name="ssid">无线网络设备的ssid。若不填写ssid，默认为清空门店下所有设备；填写ssid则为清空该ssid下的所有设备</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult ShopClean(string accessTokenOrAppId, long shopId, string ssid,int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/shop/clean?access_token={0}";
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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId">门店ID</param>
        /// <param name="ssid">无线网络设备的ssid，不能包含中文字符，必需是“WX”开头(“WX”为大写字母)</param>
        /// <param name="password">无线网络设备的密码，大于8个字符，不能包含中文字符</param>
        ///// <param name="bssid">无线网络设备无线mac地址，格式冒号分隔，字符长度17个，并且字母小写，例如：00:1f:7a:ad:5c:a8</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult AddDevice(string accessTokenOrAppId, long shopId, string ssid, string password,
            /*string bssid,*/ int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/device/add?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId">门店ID</param>
        /// <param name="ssid">无线网络设备的ssid，限30个字符以内。ssid支持中文，但可能因设备兼容性问题导致显示乱码，或无法连接等问题，相关风险自行承担！</param>
        /// <param name="reset">重置secretkey，false-不重置，true-重置，默认为false</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WiFiRegisterJsonResult WifeRegister(string accessTokenOrAppId, long shopId, string ssid, string reset,
           int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/apportal/register?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="pageIndex">分页下标，默认从1开始</param>
        /// <param name="pageSize">每页的个数，默认10个，最大20个</param>
        /// <param name="shopId">根据门店id查询</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetDeviceListResult GetDeviceList(string accessTokenOrAppId, int pageIndex = 1, int pageSize = 10,
            long? shopId = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/device/list?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="bssid">需要删除的无线网络设备无线mac地址，格式冒号分隔，字符长度17个，并且字母小写，例如：00:1f:7a:ad:5c:a8</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult DeleteDevice(string accessTokenOrAppId, string bssid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/device/delete?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId"></param>
        /// <param name="imgId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult GetQrcode(string accessTokenOrAppId, long shopId, int imgId,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/qrcode/get?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    img_id = imgId
                };

                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 设置商家主页
        /// 传入自定义链接则是使用自定义链接，否则使用默认模板
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId">门店ID</param>
        /// <param name="url">自定义链接（选择传入）</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SetHomePage(string accessTokenOrAppId, long shopId, string url = null,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/homepage/set?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId">查询的门店id</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetHomePageResult GetHomePage(string accessTokenOrAppId, long shopId,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/homepage/get?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId">门店ID</param>
        /// <param name="barType">微信首页欢迎语的文本内容：0--欢迎光临+公众号名称；1--欢迎光临+门店名称；2--已连接+公众号名称+WiFi；3--已连接+门店名称+Wi-Fi。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SetBar(string accessTokenOrAppId, long shopId, int barType,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/bar/set?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId">门店ID</param>
        /// <param name="finishPageUrl">连网完成页URL。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SetFinishpage(string accessTokenOrAppId, long shopId, string finishPageUrl,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/finishpage/set?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">起始日期时间，格式yyyy-mm-dd，最长时间跨度为30天</param>
        /// <param name="endDate">结束日期时间戳，格式yyyy-mm-dd，最长时间跨度为30天</param>
        /// <param name="shopId">按门店ID搜索，-1为总统计</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetStatisticsResult GetStatistics(string accessTokenOrAppId, string beginDate, string endDate,
            long shopId = -1,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/statistics/list?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId">门店ID，可设置为0，表示所有门店</param>
        /// <param name="cardId">卡券ID</param>
        /// <param name="cardDescribe">卡券描述，不能超过18个字符</param>
        ///<param name="starTime">卡券投放开始时间（单位是秒）</param>
        /// <param name="endTime">卡券投放结束时间（单位是秒）注：不能超过卡券的有效期时间</param>
        /// <param name="cardQuantity">卡券库存</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SetCouponPut(string accessTokenOrAppId, long shopId, string cardId, string cardDescribe, string starTime, string endTime, int cardQuantity,
            int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/couponput/set?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId">门店ID，可设置为0，表示所有门店</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WiFiGetCouponPutJsonResult GetCouponPut(string accessTokenOrAppId, long shopId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/couponput/get?access_token={0}";

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
        /// <param name="accessTokenOrAppId"></param>
        /// <returns></returns>
        public static WiFiConnectUrlResultJson GetConnectUrl(string accessTokenOrAppId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/account/get_connecturl?access_token={0}";

                return CommonJsonSend.Send<WiFiConnectUrlResultJson>(accessToken, urlFormat, null,
                    CommonJsonSendType.GET);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 第三方平台获取开插件wifi_token
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="callBackUrl">回调URL，开通插件成功后的跳转页面。注：该参数域名必须与跳转进开通插件页面的页面域名保持一致，建议均采用第三方平台域名。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WiFiOpenPluginTokenJsonResult OpenPluginToken(string accessTokenOrAppId, string callBackUrl, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/openplugin/token?access_token={0}";

                var data = new
                {
                    callback_url = callBackUrl
                };


                return CommonJsonSend.Send<WiFiOpenPluginTokenJsonResult>(accessToken, urlFormat, data, CommonJsonSendType.GET, timeOut: timeOut);

            }, accessTokenOrAppId); 
        }

    }
}
