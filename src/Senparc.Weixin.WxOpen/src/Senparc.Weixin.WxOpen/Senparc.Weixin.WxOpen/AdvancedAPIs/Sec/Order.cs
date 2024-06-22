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
    
    文件名：SnsApi.cs
    文件功能描述：小程序 Sec下接口
    
    创建标识：mc7246 - 20230831

    修改标识：Senparc - 20230905
    修改描述：v4.15.0 完善“第三方服务商小程序备案”接口

    修改标识：Guili95 - 20240623
    修改描述：v3.19.0 添加小程序发货信息管理服务-查询小程序是否已完成交易结算管理确认接口

----------------------------------------------------------------*/


using Senparc.NeuChar;
using Senparc.NeuChar.Helpers;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Sec
{
    /// <summary>
    /// WxApp接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram, true)]
    public static class Order
    {
        #region 同步方法

        /// <summary>
        /// 发货信息录入接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="postBody"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult UploadShippingInfo(string accessTokenOrAppId, UploadShippingInfoModel postBody, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/upload_shipping_info?access_token={0}";
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 发货信息合单录入接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="postBody"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult UploadCombinedShippingInfo(string accessTokenOrAppId, UploadCombinedShippingInfoModel postBody, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/upload_combined_shipping_info?access_token={0}";
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询订单发货状态
        /// 你可以通过交易单号或商户号+商户单号来查询该支付单的发货状态。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="transaction_id">原支付交易对应的微信订单号。</param>
        /// <param name="merchant_id">支付下单商户的商户号，由微信支付生成并下发。</param>
        /// <param name="sub_merchant_id">二级商户号。</param>
        /// <param name="merchant_trade_no">商户系统内部订单号，只能是数字、大小写字母`_-*`且在同一个商户号下唯一</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetOrderJsonResult GetOrder(string accessTokenOrAppId, string transaction_id="", string merchant_id="", string sub_merchant_id="", string merchant_trade_no="", int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/get_order?access_token={0}";
                var postBody = new
                {
                    transaction_id,
                    merchant_id,
                    sub_merchant_id,
                    merchant_trade_no
                };
                return CommonJsonSend.Send<GetOrderJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询订单列表
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="postBody"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetOrderListJsonResult GetOrderList(string accessTokenOrAppId, GetOrderListInfoModel postBody, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/get_order_list?access_token={0}";
                return CommonJsonSend.Send<GetOrderListJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 确认收货提醒接口
        /// 如你已经从你的快递物流服务方获知到用户已经签收相关商品，可以通过该接口提醒用户及时确认收货，以提高资金结算效率，每个订单仅可调用一次
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="receive_time">快递签收时间，时间戳形式。</param>
        /// <param name="transaction_id">原支付交易对应的微信订单号</param>
        /// <param name="merchant_id">支付下单商户的商户号，由微信支付生成并下发</param>
        /// <param name="sub_merchant_id">二级商户号</param>
        /// <param name="merchant_trade_no">商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult NotifyConfirmReceive(string accessTokenOrAppId, long received_time, string transaction_id="", string merchant_id="", string sub_merchant_id="", string merchant_trade_no="", int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/notify_confirm_receive?access_token={0}";
                var postBody = new
                {
                    received_time,
                    transaction_id,
                    merchant_id,
                    sub_merchant_id,
                    merchant_trade_no
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 消息跳转路径设置接口
        /// 如你已经在小程序内接入平台提供的确认收货组件，可以通过该接口设置发货消息及确认收货消息的跳转动作，用户点击发货消息时会直接进入你的小程序订单列表页面或详情页面进行确认收货，进一步优化用户体验
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="path">商户自定义跳转路径</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WxJsonResult SetMsgJumpPath(string accessTokenOrAppId, string path, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/set_msg_jump_path?access_token={0}";
                var postBody = new
                {
                    path
                };
                return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询小程序是否已开通发货信息管理服务
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="appid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static IsTradeManagedJsonResult IsTradeManaged(string accessTokenOrAppId, string appid, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/is_trade_managed?access_token={0}";
                var postBody = new
                {
                    appid
                };
                return CommonJsonSend.Send<IsTradeManagedJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 查询小程序是否已完成交易结算管理确认
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="appid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static IsTradeManagementConfirmationCompletedJsonResult IsTradeManagementConfirmationCompleted(string accessTokenOrAppId, string appid, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/is_trade_management_confirmation_completed?access_token={0}";
                var postBody = new
                {
                    appid
                };
                return CommonJsonSend.Send<IsTradeManagementConfirmationCompletedJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 【异步方法】发货信息录入接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="postBody"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> UploadShippingInfoAsync(string accessTokenOrAppId, UploadShippingInfoModel postBody, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/upload_shipping_info?access_token={0}";
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】发货信息合单录入接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="postBody"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> UploadCombinedShippingInfoAsync(string accessTokenOrAppId, UploadCombinedShippingInfoModel postBody, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/upload_combined_shipping_info?access_token={0}";
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】查询订单发货状态
        /// 你可以通过交易单号或商户号+商户单号来查询该支付单的发货状态。
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="transaction_id">原支付交易对应的微信订单号。</param>
        /// <param name="merchant_id">支付下单商户的商户号，由微信支付生成并下发。</param>
        /// <param name="sub_merchant_id">二级商户号。</param>
        /// <param name="merchant_trade_no">商户系统内部订单号，只能是数字、大小写字母`_-*`且在同一个商户号下唯一</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetOrderJsonResult> GetOrderAsync(string accessTokenOrAppId, string transaction_id = "", string merchant_id = "", string sub_merchant_id = "", string merchant_trade_no = "", int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/get_order?access_token={0}";
                var postBody = new
                {
                    transaction_id,
                    merchant_id,
                    sub_merchant_id,
                    merchant_trade_no
                };
                return await CommonJsonSend.SendAsync<GetOrderJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】查询订单列表
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="postBody"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetOrderListJsonResult> GetOrderListAsync(string accessTokenOrAppId, GetOrderListInfoModel postBody, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/get_order_list?access_token={0}";
                return await CommonJsonSend.SendAsync<GetOrderListJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】确认收货提醒接口
        /// 如你已经从你的快递物流服务方获知到用户已经签收相关商品，可以通过该接口提醒用户及时确认收货，以提高资金结算效率，每个订单仅可调用一次
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="receive_time">快递签收时间，时间戳形式。</param>
        /// <param name="transaction_id">原支付交易对应的微信订单号</param>
        /// <param name="merchant_id">支付下单商户的商户号，由微信支付生成并下发</param>
        /// <param name="sub_merchant_id">二级商户号</param>
        /// <param name="merchant_trade_no">商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> NotifyConfirmReceiveAsync(string accessTokenOrAppId, long receive_time, string transaction_id = "", string merchant_id = "", string sub_merchant_id = "", string merchant_trade_no = "", int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/notify_confirm_receive?access_token={0}";
                var postBody = new
                {
                    receive_time,
                    transaction_id,
                    merchant_id,
                    sub_merchant_id,
                    merchant_trade_no
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】消息跳转路径设置接口
        /// 如你已经在小程序内接入平台提供的确认收货组件，可以通过该接口设置发货消息及确认收货消息的跳转动作，用户点击发货消息时会直接进入你的小程序订单列表页面或详情页面进行确认收货，进一步优化用户体验
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="path">商户自定义跳转路径</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WxJsonResult> SetMsgJumpPathAsync(string accessTokenOrAppId, string path, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/set_msg_jump_path?access_token={0}";
                var postBody = new
                {
                    path
                };
                return await CommonJsonSend.SendAsync<WxJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】查询小程序是否已开通发货信息管理服务
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="appid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<IsTradeManagedJsonResult> IsTradeManagedAsync(string accessTokenOrAppId, string appid, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/is_trade_managed?access_token={0}";
                var postBody = new
                {
                    appid
                };
                return await CommonJsonSend.SendAsync<IsTradeManagedJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】查询小程序是否已完成交易结算管理确认
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="appid"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<IsTradeManagementConfirmationCompletedJsonResult> IsTradeManagementConfirmationCompletedAsync(string accessTokenOrAppId, string appid, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/wxa/sec/order/is_trade_management_confirmation_completed?access_token={0}";
                var postBody = new
                {
                    appid
                };
                return await CommonJsonSend.SendAsync<IsTradeManagementConfirmationCompletedJsonResult>(accessToken, urlFormat, postBody, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
