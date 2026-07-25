#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SendWorkRedPackResult.cs
    文件功能描述：企业红包异步发送结果模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v1.19.0 新增企业红包异步发送结果与请求签名审计信息

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// 企业红包异步发送结果，包含响应以及调用方可用于审计的请求签名信息。
    /// </summary>
    public sealed class SendWorkRedPackResult
    {
        /// <summary>
        /// 企业红包接口响应。
        /// </summary>
        public NormalRedPackResult Response { get; }

        /// <summary>
        /// 请求随机字符串。
        /// </summary>
        public string NonceStr { get; }

        /// <summary>
        /// 微信支付签名。
        /// </summary>
        public string PaySign { get; }

        /// <summary>
        /// 企业微信签名。
        /// </summary>
        public string WorkpaySign { get; }

        /// <summary>
        /// 商户订单号。
        /// </summary>
        public string MchBillNo { get; }

        /// <summary>
        /// 创建企业红包异步发送结果。
        /// </summary>
        public SendWorkRedPackResult(
            NormalRedPackResult response,
            string nonceStr,
            string paySign,
            string workpaySign,
            string mchBillNo)
        {
            Response = response;
            NonceStr = nonceStr;
            PaySign = paySign;
            WorkpaySign = workpaySign;
            MchBillNo = mchBillNo;
        }
    }
}
