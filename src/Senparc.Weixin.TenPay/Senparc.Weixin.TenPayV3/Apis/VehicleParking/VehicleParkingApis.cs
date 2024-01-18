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
  
    文件名：VehicleParkingApis.cs
    文件功能描述：微信支付V3支付分停车接口
    
    
    创建标识：Senparc - 20210926
    
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Apis.VehicleParking;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付V3支付分停车接口
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_1.shtml 下的【支付分停车】所有接口
    /// </summary>
    public partial class VehicleParkingApis
    {

        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public VehicleParkingApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {

            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 返回可用的微信支付地址（自动判断是否使用沙箱）
        /// </summary>
        /// <param name="urlFormat">如：<code>https://api.mch.weixin.qq.com/{0}pay/unifiedorder</code></param>
        /// <returns></returns>
        // TODO: 重复使用
        private static string ReurnPayApiUrl(string urlFormat)
        {
            return string.Format(urlFormat, Senparc.Weixin.Config.UseSandBoxPay ? "sandboxnew/" : "");
        }

        /// <summary>
        /// 查询车牌服务开通信息接口
        /// <para>该接口仅支持停车场景，商户首先请求查询车牌服务开通信息接口，确认该车牌，是否被该用户开通车主服务。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_1.shtml </para>
        /// </summary>
        /// <param name="appid">appid是商户在微信申请公众号或移动应用成功后分配的账号ID，登录平台为mp.weixin.qq.com或open.weixin.qq.com</param>
        /// <param name="plate_number">车牌号，仅包括省份+车牌，不包括特殊字符。</param>
        /// <param name="plate_color">车牌颜色，枚举值：BLUE：蓝色 GREEN：绿色 YELLOW：黄色 BLACK：黑色 WHITE：白色 LIMEGREEN：黄绿色</param>
        /// <param name="openid">用户在商户对应appid下的唯一标识</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryServiceReturnJson> QueryServiceAsync(string appid, string plate_number, string plate_color, string openid, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/vehicle/parking/services/find?appid={appid}&plate_number={plate_number}&plate_color={plate_color}&openid={openid}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryServiceReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 创建停车入场接口
        /// <para>车辆入场以后，商户调用该接口，创建停车入场信息</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_2.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<CreateParkingReturnJson> CreateParkingAsync(CreateParkingRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/vehicle/parking/parkings");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<CreateParkingReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 扣费受理接口
        /// <para>商户请求扣费受理接口，会完成订单受理。微信支付进行异步扣款，支付完成后，会将订单支付结果发送给商户。</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_3.shtml </para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<PayParkingReturnJson> PayParkingAsync(PayParkingRequestData data, int timeOut = Config.TIME_OUT)
        {
            //TODO: 方法名是否恰当?
            var url = ReurnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/vehicle/transactions/parking");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<PayParkingReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 查询订单接口
        /// <para>商户通过商户订单号，来查询订单信息</para>
        /// <para>更多详细请参考 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_8_4.shtml </para>
        /// </summary>
        /// <param name="out_trade_no">商户系统内部订单号，只能是数字、大小写字母，且在同一个商户号下唯一</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QueryParkingReturnJson> QueryParkingAsync(string out_trade_no, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/vehicle/transactions/out-trade-no/{out_trade_no}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QueryParkingReturnJson>(url, null, timeOut, ApiRequestMethod.GET);
        }
    }
}
