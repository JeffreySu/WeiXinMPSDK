#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2023 chinanhb & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：DeliveryApi.cs
    文件功能描述：微信小程序物流服务相关接口
    
    
    创建标识：chinanhb - 20230529
----------------------------------------------------------------*/

/*
    API：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/
    
 */
#endregion Apache License Version 2.0

using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery.DeliveryJson.JsonResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Delivery
{
    /// <summary>
    /// 物流服务接口
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_MiniProgram,true)]
    public class DeliveryApi
    {
        #region 同步方法
        /// <summary>
        /// 该接口用于获取支持的快递公司列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetAllDeliveryJsonResult GetAllDelivery(string accessTokenOrAppId,int timeOut=Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/getAllDelivery.html
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/delivery/getall?access_token={0}";
                return CommonJsonSend.Send<GetAllDeliveryJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET,timeOut:timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 该接口用于绑定、解绑物流账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">接口请求参数（</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static BindAccountJsonResult BindAccount(string accessTokenOrAppId,BindAccountModel data, int timeOut=Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/bindAccount.html
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/account/bind?access_token={0}";
                return CommonJsonSend.Send<BindAccountJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 该接口用于获取所有绑定的物流账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">接口请求参数（</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetAllAccountJsonResult GetAllAccount(string accessTokenOrAppId,int timeOut=Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/getAllAccount.html
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/account/getall?access_token={0}";
                return CommonJsonSend.Send<GetAllAccountJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 该接口用于获取打印员。若需要使用微信打单 PC 软件，才需要调用
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetPrinterJsonResult GetPrinter(string accessTokenOrAppId, int timeOut=Config.TIME_OUT)
        {
            ///官方文档：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/getPrinter.html
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/printer/getall?access_token={0}";
                return CommonJsonSend.Send<GetPrinterJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
            },accessTokenOrAppId);
        }
        /// <summary>
        /// 该接口用于配置面单打印员，可以设置多个，若需要使用微信打单 PC 软件，才需要调用。
        /// 注：面单打印员不需要为小程序项目成员
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">接口请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static UpdatePrinterJsonResult UpdatePrinter(string accessTokenOrAppId,UpdatePrinterModel data,int timeOut=Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/updatePrinter.html
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpFileHost + "/cgi-bin/express/business/printer/update?access_token={0}";
                return CommonJsonSend.Send<UpdatePrinterJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 该接口用于获取电子面单余额。仅在使用加盟类快递公司时，才可以调用。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetQuotaJsonResult GetQuota(string accessTokenOrAppId,GetQuotaModel data,int timeOut=Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/getQuota.html
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/quota/get?access_token={0}";
                return CommonJsonSend.Send<GetQuotaJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 生成运单
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AddOrderJsonResult AddOrder(string accessTokenOrAppId,AddOrderModel data,int timeOut=Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/addOrder.html
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/order/add?access_token={0}";
                return CommonJsonSend.Send<AddOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 取消运单
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static CancelOrderJsonResult CancelOrder(string accessTokenOrAppId,CancelOrderModel data,int timeOut=Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/cancelOrder.html
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/order/cancel?access_token={0}";
                return CommonJsonSend.Send<CancelOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取运单数据
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetOrderJsonResult GetOrder(string accessTokenOrAppId,GetOrderModel data,int timeOut=Config.TIME_OUT) 
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/getOrder.html
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/order/get?access_token={0}";
                return CommonJsonSend.Send<GetOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 批量获取运单数据
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static BatchGetOrderJsonResult BatchGetOrder(string accessTokenOrAppId,List<GetOrderModel> data,int timeOut=Config.TIME_OUT) 
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/batchGetOrder.html
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/order/batchget?access_token={0}";
                return CommonJsonSend.Send<BatchGetOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 查询运单轨迹
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetPathJsonResult GetPath(string accessTokenOrAppId,GetPathModel data,int timeOut=Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/getPath.html
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/path/get?access_token={0}";
                return CommonJsonSend.Send<GetPathJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 模拟快递公司更新订单状态, 该接口只能用户测试
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static TestUpdateOrderJsonResult TestUpdateOrder(string accessTokenOrAppId,TestUpdateOrderModel data,int timeOut=Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/testUpdateOrder.html
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/test_update_order?access_token={0}";
                return CommonJsonSend.Send<TestUpdateOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取订单Id
        /// </summary>
        /// <returns></returns>
        public static string GetOrderNO()
        {
            Random rd = new Random();
            string DateStr = DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0') + DateTime.Now.Second.ToString().PadLeft(2, '0') + DateTime.Now.ToString("fff");
            string str = DateStr + rd.Next(10000).ToString().PadLeft(4, '0');
            return str;
        }
        #endregion
        #region 异步方法
        /// <summary>
        /// 该接口用于获取支持的快递公司列表
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetAllDeliveryJsonResult> GetAllDeliveryAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/getAllDelivery.html
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/delivery/getall?access_token={0}";
                return await CommonJsonSend.SendAsync<GetAllDeliveryJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 该接口用于绑定、解绑物流账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">接口请求参数（</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<BindAccountJsonResult> BindAccountAsync(string accessTokenOrAppId, BindAccountModel data, int timeOut = Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/bindAccount.html
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/account/bind?access_token={0}";
                return await CommonJsonSend.SendAsync<BindAccountJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 该接口用于获取所有绑定的物流账号
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">接口请求参数（</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetAllAccountJsonResult> GetAllAccountAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/getAllAccount.html
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/account/getall?access_token={0}";
                return await CommonJsonSend.SendAsync<GetAllAccountJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 该接口用于获取打印员。若需要使用微信打单 PC 软件，才需要调用
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetPrinterJsonResult> GetPrinterAsync(string accessTokenOrAppId, int timeOut = Config.TIME_OUT)
        {
            ///官方文档：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/getPrinter.html
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/printer/getall?access_token={0}";
                return await CommonJsonSend.SendAsync<GetPrinterJsonResult>(accessToken, urlFormat, null, CommonJsonSendType.GET, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 该接口用于配置面单打印员，可以设置多个，若需要使用微信打单 PC 软件，才需要调用。
        /// 注：面单打印员不需要为小程序项目成员
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">接口请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<UpdatePrinterJsonResult> UpdatePrinterAsync(string accessTokenOrAppId, UpdatePrinterModel data, int timeOut = Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/updatePrinter.html
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpFileHost + "/cgi-bin/express/business/printer/update?access_token={0}";
                return await CommonJsonSend.SendAsync<UpdatePrinterJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 该接口用于获取电子面单余额。仅在使用加盟类快递公司时，才可以调用。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetQuotaJsonResult> GetQuotaAsync(string accessTokenOrAppId, GetQuotaModel data, int timeOut = Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/getQuota.html
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/quota/get?access_token={0}";
                return await CommonJsonSend.SendAsync<GetQuotaJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 生成运单
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<AddOrderJsonResult> AddOrderAsync(string accessTokenOrAppId, AddOrderModel data, int timeOut = Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/addOrder.html
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/order/add?access_token={0}";
                return await CommonJsonSend.SendAsync<AddOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 取消运单
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<CancelOrderJsonResult> CancelOrderAsync(string accessTokenOrAppId, CancelOrderModel data, int timeOut = Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/cancelOrder.html
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/order/cancel?access_token={0}";
                return await CommonJsonSend.SendAsync<CancelOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取运单数据
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetOrderJsonResult> GetOrderAsync(string accessTokenOrAppId, GetOrderModel data, int timeOut = Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/getOrder.html
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/order/get?access_token={0}";
                return await CommonJsonSend.SendAsync<GetOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 批量获取运单数据
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<BatchGetOrderJsonResult> BatchGetOrderAsync(string accessTokenOrAppId, List<GetOrderModel> data, int timeOut = Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/batchGetOrder.html
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/order/batchget?access_token={0}";
                return await CommonJsonSend.SendAsync<BatchGetOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        /// <summary>
        /// 查询运单轨迹
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetPathJsonResult> GetPathAsync(string accessTokenOrAppId, GetPathModel data, int timeOut = Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/getPath.html
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/path/get?access_token={0}";
                return await CommonJsonSend.SendAsync<GetPathJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        public static async Task<TestUpdateOrderJsonResult> TestUpdateOrderAsync(string accessTokenOrAppId, TestUpdateOrderModel data, int timeOut = Config.TIME_OUT)
        {
            ///官方文档地址：https://developers.weixin.qq.com/miniprogram/dev/OpenApiDoc/express/express-by-business/testUpdateOrder.html
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var urlFormat = Config.ApiMpHost + "/cgi-bin/express/business/test_update_order?access_token={0}";
                return await CommonJsonSend.SendAsync<TestUpdateOrderJsonResult>(accessToken, urlFormat, data, timeOut: timeOut);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion
    }
}
