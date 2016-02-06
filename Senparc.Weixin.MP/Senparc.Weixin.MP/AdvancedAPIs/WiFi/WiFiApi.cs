/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：WiFiApi.cs
    文件功能描述：微信连WiFi接口
    
    
    创建标识：Senparc - 20150709
----------------------------------------------------------------*/

/*
    官方文档：http://mp.weixin.qq.com/wiki/10/6232005bdc497f7cf8e19d4e843c70d2.html
 */

using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.WiFi;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public static class WiFiApi
    {
        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="shopId">门店ID</param>
        /// <param name="ssid">无线网络设备的ssid，不能包含中文字符，必需是“WX”开头(“WX”为大写字母)</param>
        /// <param name="password">无线网络设备的密码，大于8个字符，不能包含中文字符</param>
        /// <param name="bssid">无线网络设备无线mac地址，格式冒号分隔，字符长度17个，并且字母小写，例如：00:1f:7a:ad:5c:a8</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult AddDevice(string accessTokenOrAppId, long shopId, string ssid, string password, string bssid, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/bizwifi/device/add?access_token={0}";

                var data = new
                {
                    shop_id = shopId,
                    ssid = ssid,
                    password = password,
                    bssid = bssid,
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);

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
        public static WxJsonResult GetQrcode(string accessTokenOrAppId, long shopId, int imgId, int timeOut = Config.TIME_OUT)
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
        public static WxJsonResult SetHomePage(string accessTokenOrAppId, long shopId, string url = null, int timeOut = Config.TIME_OUT)
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
        public static GetHomePageResult GetHomePage(string accessTokenOrAppId, long shopId, int timeOut = Config.TIME_OUT)
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
        /// 数据统计
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">起始日期时间，格式yyyy-mm-dd，最长时间跨度为30天</param>
        /// <param name="endDate">结束日期时间戳，格式yyyy-mm-dd，最长时间跨度为30天</param>
        /// <param name="shopId">按门店ID搜索，-1为总统计</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetStatisticsResult GetStatistics(string accessTokenOrAppId, string beginDate, string endDate, long shopId = -1,
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
    }
}
