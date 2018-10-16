#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
  
    文件名：TenPayV3PayBankRequestData.cs
    文件功能描述：企业付款到银行卡接口 请求参数
    
    创建标识：Senparc - 20171129

    修改标识：Senparc - 20180409
    修改描述：v14.11.0 添加“付款到银行卡”接口

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 付款到银行卡提交数据
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class TenPayV3PayBankRequestData
    {
        ///// <summary>
        ///// 公众账号ID
        ///// </summary>
        //public string AppId { get; set; }


        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 商户订单号，需保持唯一（只允许数字[0~9]或字母[A~Z]和[a~z]，最短8位，最长32位）
        /// </summary>
        public string PartnerTradeNumber { get; set; }

        /// <summary>
        /// <para>收款方银行卡号（采用标准RSA算法，公钥由微信侧提供）,详见获取RSA加密公钥API</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/api/tools/mch_pay.php?chapter=24_7</para>
        /// </summary>
        public string EncBankNumber { get; set; }

        /// <summary>
        /// <para>收款方用户名（采用标准RSA算法，公钥由微信侧提供）详见获取RSA加密公钥API</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/api/tools/mch_pay.php?chapter=24_7</para>
        /// </summary>
        public string EncTrueName { get; set; }

        /// <summary>
        /// <para>收款方开户行。银行卡所在开户行编号,详见银行编号列表</para>
        /// <para>https://pay.weixin.qq.com/wiki/doc/api/tools/mch_pay.php?chapter=24_4</para>
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// 付款金额：RMB分（支付总额，不含手续费）  注：大于0的整数
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 【非必填】付款说明。企业付款到银行卡付款说明,即订单备注（UTF8编码，允许100个字符以内）
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }

        /// <summary>
        /// 商家订单号
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public string SignType { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;

        /// <summary>
        /// 通过MD5签名算法计算得出的签名值，详见MD5签名生成算法
        /// </summary>
        public readonly string Sign;

        public TenPayV3PayBankRequestData(string mchId, string nonceStr, string key, string partnerTradeNumber, string encBankNumber, string encTrueName, string bankCode, string amount, string desc = "")
        {
            this.MchId = mchId;
            this.NonceStr = nonceStr;
            this.Key = key;
            this.PartnerTradeNumber = partnerTradeNumber;
            this.EncBankNumber = encBankNumber;
            this.EncTrueName = encTrueName;
            this.BankCode = bankCode;
            this.Amount = int.Parse(amount);
            this.Desc = desc;

            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();
            //设置package订单参数
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr); //随机字符串
            PackageRequestHandler.SetParameter("partner_trade_no", this.PartnerTradeNumber); //商户订单号
            PackageRequestHandler.SetParameter("mch_id", this.MchId); //商户号
            PackageRequestHandler.SetParameter("enc_bank_no", this.EncBankNumber);
            PackageRequestHandler.SetParameter("enc_true_name", this.EncTrueName);
            PackageRequestHandler.SetParameter("bank_code", this.BankCode);
            PackageRequestHandler.SetParameter("amount", this.Amount.ToString());
            PackageRequestHandler.SetParameterWhenNotNull("desc", this.Desc);



            Sign = PackageRequestHandler.CreateMd5Sign("key", this.Key);
            PackageRequestHandler.SetParameter("sign", Sign); //签名

            #endregion
        }
    }
}