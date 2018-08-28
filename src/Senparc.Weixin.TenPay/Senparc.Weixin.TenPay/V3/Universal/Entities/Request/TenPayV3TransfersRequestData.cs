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
  
    文件名：TenPayV3TransfersRequestData.cs
    文件功能描述：微信支付企业付款请求参数
    
    创建标识：Senparc - 20170215
      
    修改标识：Senparc - 20170404
    修改描述：14.3.141 修改amount为decimal类型

----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// 微信支付提交的XML Data数据[企业付款]
    /// </summary>
    public class TenPayV3TransfersRequestData
    {
        /// <summary>
        /// 微信分配的公众账号ID（企业号corpid即为此appId） [mch_appid]
        /// </summary>
        public string MchAppId { get; set; }

        /// <summary>
        /// 商户号 [mchid]
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 微信支付分配的终端设备号 [device_info]
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 随机字符串 [nonce_str]
        /// </summary>
        public string NonceStr { get; }

        /// <summary>
        /// 商家订单号 [partner_trade_no]
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 用户openid [openid]
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 校验用户姓名选项 [check_name]
        /// </summary>
        public string CheckName { get; set; }

        /// <summary>
        /// 收款用户姓名 [re_user_name]
        /// </summary>
        public string ReUserName { get; set; }

        /// <summary>
        /// 金额 [amount]
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 企业付款描述信息 [desc]
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// Ip地址 [spbill_create_ip]
        /// </summary>
        public string SpbillCreateIP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;
        public readonly string Sign;

        /// <summary>
        /// 企业付款
        /// </summary>
        /// <param name="mchAppid">公众账号appid</param>
        /// <param name="mchId">商户号</param>
        /// <param name="deviceInfo">设备号</param>
        /// <param name="nonceStr">随机字符串</param>
        /// <param name="outTradeNo">商户订单号</param>
        /// <param name="openId">用户openid</param>
        /// <param name="key"></param>
        /// <param name="checkName">校验用户姓名选项</param>
        /// <param name="reUserName">收款用户姓名</param>
        /// <param name="amount">金额（单位：元，小数点后不要超过2位，否则会被四舍五入到分）</param>
        /// <param name="desc">企业付款描述信息</param>
        /// <param name="spbillCreateIP">Ip地址</param>
        public TenPayV3TransfersRequestData(string mchAppid, string mchId, string deviceInfo, string nonceStr, string outTradeNo, string openId, string key, string checkName, string reUserName, decimal amount, string desc, string spbillCreateIP)
        {
            MchAppId = mchAppid;
            MchId = mchId;
            DeviceInfo = deviceInfo;
            NonceStr = nonceStr;
            OutTradeNo = outTradeNo;
            OpenId = openId;
            CheckName = checkName;
            ReUserName = reUserName;
            Amount = amount;
            Desc = desc;
            SpbillCreateIP = spbillCreateIP;
            Key = key;

            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();

            //设置package订单参数
            PackageRequestHandler.SetParameter("mch_appid", this.MchAppId); //公众账号appid
            PackageRequestHandler.SetParameter("mchid", this.MchId); //商户号
            PackageRequestHandler.SetParameter("device_info", this.DeviceInfo); //终端设备号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr); //随机字符串
            PackageRequestHandler.SetParameter("partner_trade_no", this.OutTradeNo); //商户订单号
            PackageRequestHandler.SetParameter("openid", this.OpenId); //用户openid
            PackageRequestHandler.SetParameter("check_name", this.CheckName); //校验用户姓名选项
            PackageRequestHandler.SetParameter("re_user_name", this.ReUserName); //收款用户姓名
            PackageRequestHandler.SetParameter("amount", ((int)(this.Amount * 100 + 0.5m)).ToString()); //金额
            PackageRequestHandler.SetParameter("desc", this.Desc); //企业付款描述信息
            PackageRequestHandler.SetParameter("spbill_create_ip", this.SpbillCreateIP); //Ip地址
            Sign = PackageRequestHandler.CreateMd5Sign("key", this.Key);
            PackageRequestHandler.SetParameter("sign", Sign); //签名

            #endregion
        }
    }
}