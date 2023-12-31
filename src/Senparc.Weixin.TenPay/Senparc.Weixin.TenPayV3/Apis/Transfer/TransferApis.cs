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
  
    文件名：TransferApis.cs
    文件功能描述：微信支付 V3 资金应用 - 发起商家转账 API
    
    
    创建标识：Senparc - 20220629

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Transfer
{
    /// <summary>
    /// 微信支付 V3 资金应用 - 发起商家转账 API
    /// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter4_3_1.shtml 下的【发起商家转账 API】所有接口
    /// </summary>
    public partial class TransferApis
    {
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public TransferApis(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {

            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }


        /// <summary>
        /// 发起商家转账API
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter4_3_1.shtml</para>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public async Task<BatchesReturnJson> BatchesAsync(BatchesRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = BasePayApis.GetPayApiUrl($"{Senparc.Weixin.Config.TenPayV3Host}/{{0}}v3/transfer/batches");
            TenPayApiRequest tenPayApiRequest = new(_tenpayV3Setting);
            return await tenPayApiRequest.RequestAsync<BatchesReturnJson>(url, data, timeOut);
        }
    }
}
