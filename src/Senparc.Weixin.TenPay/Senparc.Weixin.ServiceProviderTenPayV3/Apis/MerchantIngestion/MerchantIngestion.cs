
        #region Apache License Version 2.0
        /*----------------------------------------------------------------

        Copyright 2022 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
            Copyright (C) 2022 Senparc
        
            文件名：MerchantIngestion.cs
            文件功能描述：微信支付V3服务商平台接口
            
            创建标识：Senparc - 20220804

        ----------------------------------------------------------------*/

        using Senparc.CO2NET.Helpers;
        using Senparc.CO2NET.Trace;
        using Senparc.Weixin.Entities;
        // TODO: 引入Entities
        // using Senparc.Weixin.TenPayV3.Apis.BasePay;
        // using Senparc.Weixin.TenPayV3.Apis.Entities;
        // using Senparc.Weixin.TenPayV3.Entities;
        // using Senparc.Weixin.TenPayV3.Helpers;
        using System;
        using System.IO;
        using System.Linq;
        using System.Runtime.InteropServices.ComTypes;
        using System.Security.Cryptography;
        using System.Text;
        using System.Threading.Tasks;

        namespace Senparc.Weixin.ServiceProviderTenPayV3.Apis{
            public class MerchantIngestion{

                private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

                /// <summary>
                /// 构造函数
                /// </summary>
                /// <param name="senparcWeixinSettingForTenpayV3"></param>
                public MerchantIngestion(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
                {
                    _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;

                }

                /// <summary>
                /// 返回可用的微信支付地址（自动判断是否使用沙箱）
                /// </summary>
                /// <param name="urlFormat">如：<code>https://api.mch.weixin.qq.com/pay/unifiedorder</code></param>
                /// <returns></returns>
                internal static string GetPayApiUrl(string urlFormat)
                {
                    //注意：目前微信支付 V3 还没有支持沙箱，此处只是预留
                    return string.Format(urlFormat, Senparc.Weixin.Config.UseSandBoxPay ? "sandboxnew/" : "");
                }
            

        #region 特约商户进件

        

            /// <summary>
            /// 提交申请单
            /// </summary>
            /// <param name="data">提交申请单需要POST的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<ApplymentResultJson> ApplymentAsync(ApplymentRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = ApplymentApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/applyment4sub/applyment/");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<ApplymentResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
            }
        


            /// <summary>
            /// 查询申请单状态:1、通过业务申请编号查询申请状态
            /// </summary>
            /// <param name="data">查询申请单状态:1、通过业务申请编号查询申请状态需要GET的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<GetApplymentResultJson> GetApplymentAsync(GetApplymentRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = GetApplymentApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/applyment4sub/applyment/business_code/{data.business_code} ");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<GetApplymentResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
            }
        

            /// <summary>
            /// 查询申请单状态:2、通过申请单号查询申请状态
            /// </summary>
            /// <param name="data">查询申请单状态:2、通过申请单号查询申请状态需要GET的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<GetApplymentResultJson> GetApplymentAsync(GetApplymentRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = GetApplymentApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/applyment4sub/applyment/applyment_id/{data.applyment_id} ");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<GetApplymentResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
            }
        


            /// <summary>
            /// 修改结算账号
            /// </summary>
            /// <param name="data">修改结算账号需要POST的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<ResultJsonBase> ModifySettlementAsync(ModifySettlementRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = ModifySettlementApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/apply4sub/sub_merchants/{data.sub_mchid}/modify-settlement");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<ResultJsonBase>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
            }
        


            /// <summary>
            /// 查询结算账号
            /// </summary>
            /// <param name="data">查询结算账号需要GET的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<GetSettlementResultJson> GetSettlementAsync(GetSettlementRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = GetSettlementApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/apply4sub/sub_merchants/{data.sub_mchid}/settlement");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<GetSettlementResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
            }
        

        #endregion

    }
}
