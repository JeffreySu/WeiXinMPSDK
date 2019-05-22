#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
  
    文件名：TenPayV3PayToWorkerRequestData.cs
    文件功能描述：向员工付款请求参数 https://work.weixin.qq.com/api/doc#90000/90135/90278
    
    创建标识：Senparc - 20180214


    修改标识：Senparc - 20190521
    修改描述：v1.4.0 .NET Core 添加多证书注册功能；添加子商户号设置

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
    public class TenPayV3PayToWorkerRequestData
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 子商户公众账号ID	
        /// </summary>
        public string SubAppId { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }

        /// <summary>
        /// 子商户号，如果没有则不用设置
        /// </summary>
        public string SubMchId { get; set; }
        /// <summary>
        /// 企业微信支付应用secret（参见企业微信管理端支付应用页面）
        /// </summary>
        public string Secret { get; }

        /// <summary>
        /// 随机字符串 [nonce_str]
        /// </summary>
        public string NonceStr { get; }

        /// <summary>
        /// 微信支付分配的终端设备号 [device_info]
        /// </summary>
        public string DeviceInfo { get; set; }

        /// <summary>
        /// 商户订单号，需保持唯一性(只能是字母或者数字，不能包含有符号) [partner_trade_no]
        /// </summary>
        public string PartnerTradeNo { get; set; }

        /// <summary>
        /// 用户openid [openid]
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 校验用户姓名选项(NO_CHECK：不校验真实姓名 FORCE_CHECK：强校验真实姓名) [check_name]
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
        /// 付款消息类型（NORMAL_MSG：普通付款消息 APPROVAL _MSG：审批付款消息） [ww_msg_type]
        /// </summary>
        public string WwMsgType { get; set; }

        /// <summary>
        /// 审批单号（ww_msg_type为APPROVAL _MSG时，需要填写approval_number） [approval_number]
        /// </summary>
        public string ApprovalNumber { get; set; }
        /// <summary>
        /// 审批类型（ww_msg_type为APPROVAL _MSG时，需要填写1） [approval_type]
        /// </summary>
        public string ApprovalType { get; set; }

        /// <summary>
        /// 项目名称，最长50个utf8字符 [act_name]
        /// </summary>
        public string ActName { get; set; }
        /// <summary>
        /// 付款的应用id（以企业应用的名义付款，企业应用id，整型，可在企业微信管理端应用的设置页面查看。） [agentid]
        /// </summary>
        public uint? AgentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;
        public readonly string Sign;

        /// <summary>
        /// 企业付款
        /// </summary>
        /// <param name="appId">微信分配的公众账号ID（企业号corpid即为此appId）</param>
        /// <param name="mchId">商户号</param>
        /// <param name="nonceStr">随机字符串</param>
        /// <param name="deviceInfo">微信支付分配的终端设备号</param>
        /// <param name="partnerTradeNo">商户订单号</param>
        /// <param name="openId">用户openid</param>
        /// <param name="key"></param>
        /// <param name="checkName">校验用户姓名选项(NO_CHECK：不校验真实姓名 FORCE_CHECK：强校验真实姓名) </param>
        /// <param name="reUserName">收款用户姓名</param>
        /// <param name="amount">金额（单位：元，小数点后不要超过2位，否则会被四舍五入到分）</param>
        /// <param name="desc">企业付款描述信息</param>
        /// <param name="spbillCreateIP">Ip地址</param>
        /// <param name="wwMsgType">付款消息类型（NORMAL_MSG：普通付款消息 APPROVAL _MSG：审批付款消息）</param>
        /// <param name="approvalNumber">审批单号</param>
        /// <param name="approvalType">审批类型</param>
        /// <param name="actName">项目名称，最长50个utf8字符</param>
        /// <param name="agentId"></param>
        public TenPayV3PayToWorkerRequestData(string appId, string mchId,string secret, string nonceStr, string partnerTradeNo,
            string openId, string key, string checkName, string reUserName, decimal amount, string desc, string spbillCreateIP,
          string deviceInfo, string actName, string wwMsgType = null, string approvalNumber = null, string approvalType = null, uint? agentId = null,
          string subAppId = null, string subMchId = null)
        {
            AppId = appId;
            SubAppId = subAppId;
            MchId = mchId;
            SubMchId = subMchId;
            Secret = secret;
            NonceStr = nonceStr;
            PartnerTradeNo = partnerTradeNo;
            OpenId = openId;
            CheckName = checkName;
            ReUserName = reUserName;
            Amount = amount;
            Desc = desc;
            SpbillCreateIP = spbillCreateIP;
            DeviceInfo = deviceInfo;
            ActName = actName;
            WwMsgType = wwMsgType;
            ApprovalNumber = approvalNumber;
            ApprovalType = approvalType;
            AgentId = agentId;
            Key = key;

            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();

            //设置package订单参数
            PackageRequestHandler.SetParameter("appid", this.AppId); //微信分配的公众账号ID（企业微信corpid即为此appid）
                                                                     //https://work.weixin.qq.com/api/doc#90000/90135/90278
            PackageRequestHandler.SetParameterWhenNotNull("sub_appid", this.SubAppId); //子商户公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId); //商户号
            PackageRequestHandler.SetParameterWhenNotNull("sub_mch_id", this.SubMchId); //子商户号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr); //随机字符串
            PackageRequestHandler.SetParameterWhenNotNull("device_info", this.DeviceInfo); //微信支付分配的终端设备号
            PackageRequestHandler.SetParameter("partner_trade_no", this.PartnerTradeNo); //商户订单号
            PackageRequestHandler.SetParameter("openid", this.OpenId); //用户openid
            PackageRequestHandler.SetParameter("check_name", this.CheckName); //校验用户姓名选项
            PackageRequestHandler.SetParameter("re_user_name", this.ReUserName); //收款用户姓名
            PackageRequestHandler.SetParameter("amount", ((int)(this.Amount * 100 + 0.5m)).ToString()); //金额
            PackageRequestHandler.SetParameter("desc", this.Desc); //企业付款描述信息
            PackageRequestHandler.SetParameter("spbill_create_ip", this.SpbillCreateIP); //Ip地址
            PackageRequestHandler.SetParameterWhenNotNull("ww_msg_type", this.WwMsgType); //付款消息类型
            PackageRequestHandler.SetParameterWhenNotNull("approval_number", this.ApprovalNumber); //审批单号
            PackageRequestHandler.SetParameterWhenNotNull("approval_type", this.ApprovalType); //审批类型
            PackageRequestHandler.SetParameter("act_name", this.ActName); //项目名称
            PackageRequestHandler.SetParameterWhenNotNull("agentid", this.AgentId.HasValue ? this.AgentId.ToString() : null); //项目名称

            //企业微信签名：https://work.weixin.qq.com/api/doc#90000/90135/90281

            var workWxSign = PackageRequestHandler.CreateMd5Sign("secret", this.Secret, WorkPaySignType.WorkPayApi);
            PackageRequestHandler.SetParameter("workwx_sign", workWxSign); //企业微信签名

            Sign = PackageRequestHandler.CreateMd5Sign("key", this.Key);
            PackageRequestHandler.SetParameter("sign", Sign); //签名

            #endregion
        }
    }
}