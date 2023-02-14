
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
        
            文件名：BusinessCapabilityApis.cs
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
            public class BusinessCapabilityApis{

                private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

                /// <summary>
                /// 构造函数
                /// </summary>
                /// <param name="senparcWeixinSettingForTenpayV3"></param>
                public BusinessCapabilityApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
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
            

        #region 支付即服务 

        

            /// <summary>
            /// 服务人员注册
            /// </summary>
            /// <param name="data">服务人员注册需要POST的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<GuidesRegisterResultJson> GuidesRegisterAsync(GuidesRegisterRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = GuidesRegisterApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/smartguide/guides");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<GuidesRegisterResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
            }
        


            /// <summary>
            /// 服务人员分配
            /// </summary>
            /// <param name="data">服务人员分配需要POST的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<ResultJsonBase> GuidesAssignAsync(GuidesAssignRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = GuidesAssignApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/smartguide/guides/{data.guide_id}/assign");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<ResultJsonBase>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
            }
        


            /// <summary>
            /// 服务人员查询
            /// </summary>
            /// <param name="data">服务人员查询需要GET的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<GuidesQueruResultJson> GuidesQueruAsync(GuidesQueruRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = GuidesQueruApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/smartguide/guides");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<GuidesQueruResultJson>(url, data, timeOut, ApiRequestMethod.GET, checkSign: false);
            }
        


            /// <summary>
            /// 服务人员信息更新
            /// </summary>
            /// <param name="data">服务人员信息更新需要PATCH的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<ResultJsonBase> GuidesUpdateAsync(GuidesUpdateRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = GuidesUpdateApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/smartguide/guides/{data.guide_id}");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<ResultJsonBase>(url, data, timeOut, ApiRequestMethod.PATCH, checkSign: false);
            }
        

        #endregion

    
        #region 点金计划 

        

            /// <summary>
            /// 点金计划管理
            /// </summary>
            /// <param name="data">点金计划管理需要POST的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<GoldPlanManagementResultJson> GoldPlanManagementAsync(GoldPlanManagementRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = GoldPlanManagementApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/goldplan/merchants/changegoldplanstatus");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<GoldPlanManagementResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
            }
        


            /// <summary>
            /// 商家小票管理
            /// </summary>
            /// <param name="data">商家小票管理需要POST的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<ReceiptsManagementResultJson> ReceiptsManagementAsync(ReceiptsManagementRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = ReceiptsManagementApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/goldplan/merchants/changecustompagestatus");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<ReceiptsManagementResultJson>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
            }
        


            /// <summary>
            /// 同业过滤标签管理
            /// </summary>
            /// <param name="data">同业过滤标签管理需要POST的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<ResultJsonBase> SetAdvertisingIndustryFilterAsync(SetAdvertisingIndustryFilterRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = SetAdvertisingIndustryFilterApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/goldplan/merchants/set-advertising-industry-filter");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<ResultJsonBase>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
            }
        


            /// <summary>
            /// 开通广告展示
            /// </summary>
            /// <param name="data">开通广告展示需要PATCH的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<ResultJsonBase> OpenAdvertisingShowAsync(OpenAdvertisingShowRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = OpenAdvertisingShowApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/goldplan/merchants/open-advertising-show");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<ResultJsonBase>(url, data, timeOut, ApiRequestMethod.PATCH, checkSign: false);
            }
        


            /// <summary>
            /// 关闭广告展示
            /// </summary>
            /// <param name="data">关闭广告展示需要POST的Data数据</param>
            /// <param name="timeOut"></param>
            /// <returns></returns>
            public async Task<ResultJsonBase> CloseAdvertisingShowAsync(CloseAdvertisingShowRequestData data, int timeOut = Config.TIME_OUT)
            {
                var url = CloseAdvertisingShowApis.GetPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + $"/v3/goldplan/merchants/close-advertising-show");
                TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
                return await tenPayApiRequest.RequestAsync<ResultJsonBase>(url, data, timeOut, ApiRequestMethod.POST, checkSign: false);
            }
        

        #endregion

    }
}
