#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
  
    文件名：SmartGuideApis.cs
    文件功能描述：微信支付 V3 智能导购 API
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.SmartGuide
{
    /// <summary>
    /// 微信支付 V3 智能导购 API
    /// <para>智能导购服务人员管理相关的所有接口</para>
    /// </summary>
    public partial class SmartGuideApis
    {
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public SmartGuideApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        #region 服务人员管理API

        /// <summary>
        /// 服务人员注册API
        /// <para>注册服务人员，为用户分配专属服务</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_1.shtml</para>
        /// </summary>
        /// <param name="data">服务人员注册数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<RegisterSmartGuideReturnJson> RegisterSmartGuideAsync(RegisterSmartGuideRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/smartguide/guides");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<RegisterSmartGuideReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 服务人员分配API
        /// <para>为指定订单分配服务人员</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_2.shtml</para>
        /// </summary>
        /// <param name="data">服务人员分配数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<AssignSmartGuideReturnJson> AssignSmartGuideAsync(AssignSmartGuideRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/smartguide/guides/{data.guide_id}/assign");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<AssignSmartGuideReturnJson>(url, data, timeOut);
        }

        /// <summary>
        /// 服务人员查询API
        /// <para>查询已注册的服务人员信息</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_3.shtml</para>
        /// </summary>
        /// <param name="data">服务人员查询数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<QuerySmartGuideReturnJson> QuerySmartGuideAsync(QuerySmartGuideRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/smartguide/guides");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<QuerySmartGuideReturnJson>(url, data, timeOut, ApiRequestMethod.GET);
        }

        /// <summary>
        /// 服务人员信息更新API
        /// <para>更新服务人员的基本信息</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_4.shtml</para>
        /// </summary>
        /// <param name="data">服务人员信息更新数据</param>
        /// <param name="timeOut">超时时间，单位为ms</param>
        /// <returns></returns>
        public async Task<UpdateSmartGuideReturnJson> UpdateSmartGuideAsync(UpdateSmartGuideRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/smartguide/guides/{data.guide_id}");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<UpdateSmartGuideReturnJson>(url, data, timeOut, ApiRequestMethod.PATCH);
        }

        #endregion
    }
}


