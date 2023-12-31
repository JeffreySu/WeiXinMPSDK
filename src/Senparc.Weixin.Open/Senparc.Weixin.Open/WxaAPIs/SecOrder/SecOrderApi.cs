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

    文件名：SecOrderApi.cs
    文件功能描述：小程序发货信息管理服务


    创建标识：Yaofeng - 20231026
        
----------------------------------------------------------------*/

//文档：https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%B8%80%E3%80%81%E5%8F%91%E8%B4%A7%E4%BF%A1%E6%81%AF%E5%BD%95%E5%85%A5%E6%8E%A5%E5%8F%A3

using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Open.WxaAPIs.SearchStatus;
using Senparc.Weixin.Open.WxaAPIs.SecOrder;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 小程序发货信息管理服务
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Open, true)]
    public class SecOrderApi
    {
        #region 同步方法
        /// <summary>
        /// 发货信息录入接口
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%B8%80%E3%80%81%E5%8F%91%E8%B4%A7%E4%BF%A1%E6%81%AF%E5%BD%95%E5%85%A5%E6%8E%A5%E5%8F%A3
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult UploadShippingInfo(string accessToken, UploadShippingInfo info, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/upload_shipping_info?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<WxJsonResult>(null, url, info, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 发货信息合单录入接口
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%BA%8C%E3%80%81%E5%8F%91%E8%B4%A7%E4%BF%A1%E6%81%AF%E5%90%88%E5%8D%95%E5%BD%95%E5%85%A5%E6%8E%A5%E5%8F%A3
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult UploadCombinedShippingInfo(string accessToken, UploadCombinedShippingInfo info, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/upload_combined_shipping_info?access_token={0}", accessToken.AsUrlData());
            return CommonJsonSend.Send<WxJsonResult>(null, url, info, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询订单发货状态
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%B8%89%E3%80%81%E6%9F%A5%E8%AF%A2%E8%AE%A2%E5%8D%95%E5%8F%91%E8%B4%A7%E7%8A%B6%E6%80%81
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="transaction_id">原支付交易对应的微信订单号</param>
        /// <param name="merchant_id">支付下单商户的商户号，由微信支付生成并下发</param>
        /// <param name="sub_merchant_id">二级商户号</param>
        /// <param name="merchant_trade_no">商户系统内部订单号，只能是数字、大小写字母`_-*`且在同一个商户号下唯一</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetOrderJsonResult GetOrder(string accessToken, string transaction_id = "", string merchant_id = "", string sub_merchant_id = "", string merchant_trade_no = "", int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/get_order?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                transaction_id,
                merchant_id,
                sub_merchant_id,
                merchant_trade_no,
            };
            return CommonJsonSend.Send<GetOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询订单列表
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%B8%89%E3%80%81%E6%9F%A5%E8%AF%A2%E8%AE%A2%E5%8D%95%E5%8F%91%E8%B4%A7%E7%8A%B6%E6%80%81
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="pay_time_range">支付时间所属范围。</param>
        /// <param name="order_state">订单状态枚举：(1) 待发货；(2) 已发货；(3) 确认收货；(4) 交易完成；(5) 已退款。</param>
        /// <param name="openid">支付者openid。</param>
        /// <param name="last_index">翻页时使用，获取第一页时不用传入，如果查询结果中 has_more 字段为 true，则传入该次查询结果中返回的 last_index 字段可获取下一页。</param>
        /// <param name="page_size">翻页时使用，返回列表的长度，默认为100。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetOrderListJsonResult GetOrderList(string accessToken, PayTimeRange pay_time_range = null, int? order_state = null, string openid = "", string last_index = "", int? page_size = null, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/get_order_list?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                pay_time_range,
                order_state,
                openid,
                last_index,
                page_size,
            };
            return CommonJsonSend.Send<GetOrderListJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 确认收货提醒接口
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%BA%94%E3%80%81%E7%A1%AE%E8%AE%A4%E6%94%B6%E8%B4%A7%E6%8F%90%E9%86%92%E6%8E%A5%E5%8F%A3
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="received_time">快递签收时间，时间戳形式。</param>
        /// <param name="transaction_id">原支付交易对应的微信订单号</param>
        /// <param name="merchant_id">支付下单商户的商户号，由微信支付生成并下发</param>
        /// <param name="sub_merchant_id">二级商户号</param>
        /// <param name="merchant_trade_no">商户系统内部订单号，只能是数字、大小写字母`_-*`且在同一个商户号下唯一</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult NotifyConfirmReceive(string accessToken, long received_time, string transaction_id = "", string merchant_id = "", string sub_merchant_id = "", string merchant_trade_no = "", int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/notify_confirm_receive?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                transaction_id,
                merchant_id,
                sub_merchant_id,
                merchant_trade_no,
                received_time
            };
            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 消息跳转路径设置接口
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E5%85%AD%E3%80%81%E6%B6%88%E6%81%AF%E8%B7%B3%E8%BD%AC%E8%B7%AF%E5%BE%84%E8%AE%BE%E7%BD%AE%E6%8E%A5%E5%8F%A3
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="path">商户自定义跳转路径。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SetMsgJumpPath(string accessToken, string path, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/set_msg_jump_path?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                path
            };
            return CommonJsonSend.Send<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询小程序是否已开通发货信息管理服务
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%B8%83%E3%80%81%E6%9F%A5%E8%AF%A2%E5%B0%8F%E7%A8%8B%E5%BA%8F%E6%98%AF%E5%90%A6%E5%B7%B2%E5%BC%80%E9%80%9A%E5%8F%91%E8%B4%A7%E4%BF%A1%E6%81%AF%E7%AE%A1%E7%90%86%E6%9C%8D%E5%8A%A1
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="path">商户自定义跳转路径。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static IsTradeManagedJsonResult IsTradeManaged(string accessToken, string appid, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/is_trade_managed?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                appid
            };
            return CommonJsonSend.Send<IsTradeManagedJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 发货信息录入接口
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%B8%80%E3%80%81%E5%8F%91%E8%B4%A7%E4%BF%A1%E6%81%AF%E5%BD%95%E5%85%A5%E6%8E%A5%E5%8F%A3
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> UploadShippingInfoAsync(string accessToken, UploadShippingInfo info, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/upload_shipping_info?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, info, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 发货信息合单录入接口
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%BA%8C%E3%80%81%E5%8F%91%E8%B4%A7%E4%BF%A1%E6%81%AF%E5%90%88%E5%8D%95%E5%BD%95%E5%85%A5%E6%8E%A5%E5%8F%A3
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> UploadCombinedShippingInfoAsync(string accessToken, UploadCombinedShippingInfo info, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/upload_combined_shipping_info?access_token={0}", accessToken.AsUrlData());
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, info, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询订单发货状态
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%B8%89%E3%80%81%E6%9F%A5%E8%AF%A2%E8%AE%A2%E5%8D%95%E5%8F%91%E8%B4%A7%E7%8A%B6%E6%80%81
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="transaction_id">原支付交易对应的微信订单号</param>
        /// <param name="merchant_id">支付下单商户的商户号，由微信支付生成并下发</param>
        /// <param name="sub_merchant_id">二级商户号</param>
        /// <param name="merchant_trade_no">商户系统内部订单号，只能是数字、大小写字母`_-*`且在同一个商户号下唯一</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetOrderJsonResult> GetOrderAsync(string accessToken, string transaction_id = "", string merchant_id = "", string sub_merchant_id = "", string merchant_trade_no = "", int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/get_order?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                transaction_id,
                merchant_id,
                sub_merchant_id,
                merchant_trade_no,
            };
            return await CommonJsonSend.SendAsync<GetOrderJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询订单列表
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%B8%89%E3%80%81%E6%9F%A5%E8%AF%A2%E8%AE%A2%E5%8D%95%E5%8F%91%E8%B4%A7%E7%8A%B6%E6%80%81
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="pay_time_range">支付时间所属范围。</param>
        /// <param name="order_state">订单状态枚举：(1) 待发货；(2) 已发货；(3) 确认收货；(4) 交易完成；(5) 已退款。</param>
        /// <param name="openid">支付者openid。</param>
        /// <param name="last_index">翻页时使用，获取第一页时不用传入，如果查询结果中 has_more 字段为 true，则传入该次查询结果中返回的 last_index 字段可获取下一页。</param>
        /// <param name="page_size">翻页时使用，返回列表的长度，默认为100。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetOrderListJsonResult> GetOrderListAsync(string accessToken, PayTimeRange pay_time_range = null, int? order_state = null, string openid = "", string last_index = "", int? page_size = null, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/get_order_list?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                pay_time_range,
                order_state,
                openid,
                last_index,
                page_size,
            };
            return await CommonJsonSend.SendAsync<GetOrderListJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 确认收货提醒接口
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%BA%94%E3%80%81%E7%A1%AE%E8%AE%A4%E6%94%B6%E8%B4%A7%E6%8F%90%E9%86%92%E6%8E%A5%E5%8F%A3
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="received_time">快递签收时间，时间戳形式。</param>
        /// <param name="transaction_id">原支付交易对应的微信订单号</param>
        /// <param name="merchant_id">支付下单商户的商户号，由微信支付生成并下发</param>
        /// <param name="sub_merchant_id">二级商户号</param>
        /// <param name="merchant_trade_no">商户系统内部订单号，只能是数字、大小写字母`_-*`且在同一个商户号下唯一</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> NotifyConfirmReceiveAsync(string accessToken, long received_time, string transaction_id = "", string merchant_id = "", string sub_merchant_id = "", string merchant_trade_no = "", int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/notify_confirm_receive?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                transaction_id,
                merchant_id,
                sub_merchant_id,
                merchant_trade_no,
                received_time
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 消息跳转路径设置接口
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E5%85%AD%E3%80%81%E6%B6%88%E6%81%AF%E8%B7%B3%E8%BD%AC%E8%B7%AF%E5%BE%84%E8%AE%BE%E7%BD%AE%E6%8E%A5%E5%8F%A3
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="path">商户自定义跳转路径。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SetMsgJumpPathAsync(string accessToken, string path, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/set_msg_jump_path?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                path
            };
            return await CommonJsonSend.SendAsync<WxJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 查询小程序是否已开通发货信息管理服务
        /// https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#%E4%B8%83%E3%80%81%E6%9F%A5%E8%AF%A2%E5%B0%8F%E7%A8%8B%E5%BA%8F%E6%98%AF%E5%90%A6%E5%B7%B2%E5%BC%80%E9%80%9A%E5%8F%91%E8%B4%A7%E4%BF%A1%E6%81%AF%E7%AE%A1%E7%90%86%E6%9C%8D%E5%8A%A1
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="path">商户自定义跳转路径。</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<IsTradeManagedJsonResult> IsTradeManagedAsync(string accessToken, string appid, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format(Config.ApiMpHost + "/wxa/sec/order/is_trade_managed?access_token={0}", accessToken.AsUrlData());
            var data = new
            {
                appid
            };
            return await CommonJsonSend.SendAsync<IsTradeManagedJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion
    }
}
