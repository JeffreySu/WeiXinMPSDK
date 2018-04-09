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
 
    文件名：TenPayV3Results.PayBank.cs
    文件功能描述：微信支付V3返回结果 - 付款到银行卡
    
    
    创建标识：Senparc - 20180409
    
    修改标识：Senparc - 20180409
    修改描述：TenPayV3Result.PayBank.cs 更名为 TenPayV3Results.PayBank.cs

----------------------------------------------------------------*/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 付款到银行卡返回结果
    /// </summary>
    public class PayBankResult : TenPayV3Result
    {
        /// <summary>
        /// <para>业务结果</para>
        /// <para>SUCCESS/FAIL，注意：当状态为FAIL时，存在业务结果未明确的情况，所以如果状态y为FAIL，请务必通过查询接口确认此次付款的结果（关注错误码err_code字段）。如果要继续进行这笔付款，请务必用原商户订单号和原参数来重入此接口。</para>
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// <para>错误代码</para>
        /// <para>错误码信息，注意：出现未明确的错误码时，如（SYSTEMERROR）等，请务必用原商户订单号重试，或通过查询接口确认此次付款的结果</para>
        /// </summary>
        public string err_code { get; set; }
        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 商户企业付款单号，需要保持唯一
        /// </summary>
        public string partner_trade_no { get; set; }
        /// <summary>
        /// 代付金额RMB:分
        /// </summary>
        public int amount { get; set; }
        /// <summary>
        /// 随机字符串，长度小于32位
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 签名。返回包携带签名给商户
        /// </summary>
        public string sign { get; set; }

        #region 以下字段在return_code 和result_code都为SUCCESS的时候有返回

        /// <summary>
        /// 微信企业付款单号。代付成功后，返回的内部业务单号
        /// </summary>
        public string payment_no { get; set; }
        /// <summary>
        /// 手续费金额 RMB：分
        /// </summary>
        public int cmms_amt { get; set; }

        #endregion


        /*
         *  错误码
        错误代码	原因	解决方案
        INVALID_REQUEST	无效的请求，商户系统异常导致，商户权限异常、证书错误、频率限制等	使用原单号以及原请求参数重试
        SYSTEMERROR	业务错误导致交易失败	请先调用查询接口，查询此次付款结果，如结果为不明确状态（如订单号不存在），请务必使用原商户订单号及原请求参数重试
        PARAM_ERROR	参数错误，商户系统异常导致	商户检查请求参数是否合法，证书，签名
        SIGNERROR	签名错误	按照文档签名算法进行签名值计算
        AMOUNT_LIMIT	超额；已达到今日付款金额上限或已达到今日银行卡收款金额上限	今天暂停该商户发起代付请求或今日暂停向该银行卡转账
        ORDERPAID	受理失败，订单已存在	请通过查询接口确认订单信息
        FATAL_ERROR	已存在该单，并且订单信息不一致；或订单太老	核定订单信息
        NOTENOUGH	账号余额不足	请用户充值或更换支付卡后再支付
        FREQUENCY_LIMITED	超过每分钟600次的频率限制	稍后用原单号重试
        SUCCESS	Wx侧受理成功	
        */

        /// <summary>
        /// 付款到银行卡返回结果 构造函数
        /// </summary>
        /// <param name="resultXml"></param>
        public PayBankResult(string resultXml) : base(resultXml)
        {
            err_code_des = GetXmlValue("err_code_des") ?? "";
            mch_id = GetXmlValue("mch_id") ?? "";
            partner_trade_no = GetXmlValue("partner_trade_no") ?? "";
            amount = int.Parse(GetXmlValue("amount"));//必填
            nonce_str = GetXmlValue("nonce_str") ?? "";
            sign = GetXmlValue("sign") ?? "";


            if (base.IsReturnCodeSuccess() && this.IsResultCodeSuccess())
            {
                payment_no = GetXmlValue("payment_no") ?? "";
                cmms_amt = int.Parse(GetXmlValue("cmms_amt"));//必填
            }

        }

        public bool IsResultCodeSuccess()
        {
            return result_code == "SUCCESS";
        }
    }


    /// <summary>
    /// 查询付款到银行卡返回结果
    /// </summary>
    public class QueryBankResult : TenPayV3Result
    {
        #region 以下字段在return_code为SUCCESS的时候有返回

        /// <summary>
        /// <para>业务结果</para>
        /// <para>SUCCESS/FAIL，注意：当状态为FAIL时，存在业务结果未明确的情况，所以如果状态y为FAIL，请务必通过查询接口确认此次付款的结果（关注错误码err_code字段）。如果要继续进行这笔付款，请务必用原商户订单号和原参数来重入此接口。</para>
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// <para>错误代码</para>
        /// <para>错误码信息，注意：出现未明确的错误码时，如（SYSTEMERROR）等，请务必用原商户订单号重试，或通过查询接口确认此次付款的结果</para>
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        #endregion

        #region 以下字段在return_code 和result_code都为SUCCESS的时候有返回

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 商户企业付款单号，需要保持唯一
        /// </summary>
        public string partner_trade_no { get; set; }
        /// <summary>
        /// 微信企业付款单号
        /// </summary>
        public string payment_no { get; set; }
        /// <summary>
        /// 收款用户银行卡号(MD5加密)
        /// </summary>
        public string bank_no_md5 { get; set; }
        /// <summary>
        /// 收款人真实姓名（MD5加密）
        /// </summary>
        public string true_name_md5 { get; set; }
        /// <summary>
        /// 代付订单金额RMB：分
        /// </summary>
        public int amount { get; set; }
        /// <summary>
        /// <para>代付单状态</para>
        /// <para>代付订单状态：PROCESSING（处理中，如有明确失败，则返回额外失败原因；否则没有错误原因）</para>
        /// <para>SUCCESS（付款成功）</para>
        /// <para>FAILED（付款失败,需要替换付款单号重新发起付款）</para>
        /// <para>BANK_FAIL（银行退票，订单状态由付款成功流转至退票,退票时付款金额和手续费会自动退还）</para>
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 手续费订单金额 RMB：分
        /// </summary>
        public int cmms_amt { get; set; }
        /// <summary>
        /// 商户下单时间（微信侧订单创建时间）
        /// </summary>
        public string create_time { get; set; }
        /// <summary>
        /// 成功付款时间（但无法保证银行不会退票）
        /// </summary>
        public string pay_succ_time { get; set; }
        /// <summary>
        /// 订单失败原因（如：余额不足）
        /// </summary>
        public string reason { get; set; }

        #endregion

        /*
         * 错误码
        错误代码	描述	解决方案
        INVALID_REQUEST	无效的请求，商户系统异常导致，商户权限异常、证书错误、频率限制等	使用原单号以及原请求参数重试
        PARAM_ERROR	参数错误	按照err_msg指定参数错误信息，修改相应参数
        SIGNERROR	签名错误	签名前没有按照要求进行排序。没有使用商户平台设置的密钥进行签名，参数有空格或者进行了encode后进行签名
        ORDERNOTEXIST	订单不存在	确认订单号是否发起过请求
        SYSTEMERROR	系统繁忙，请稍后重试	使用原单号以及原请求参数重试
        SUCCESS	Wx侧查询成功	 
        */

        /// <summary>
        /// 付款到银行卡返回结果 构造函数
        /// </summary>
        /// <param name="resultXml"></param>
        public QueryBankResult(string resultXml) : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {
                result_code = GetXmlValue("result_code") ?? "";
                err_code = GetXmlValue("err_code") ?? "";
                err_code_des = GetXmlValue("err_code_des") ?? "";

                if (this.IsResultCodeSuccess())
                {
                    mch_id = GetXmlValue("mch_id") ?? "";
                    partner_trade_no = GetXmlValue("partner_trade_no") ?? "";
                    payment_no = GetXmlValue("payment_no") ?? "";
                    bank_no_md5 = GetXmlValue("bank_no_md5") ?? "";
                    true_name_md5 = GetXmlValue("true_name_md5") ?? "";
                    amount = int.Parse(GetXmlValue("amount"));//必填
                    status = GetXmlValue("status") ?? "";
                    cmms_amt = int.Parse(GetXmlValue("cmms_amt"));//必填
                    create_time = GetXmlValue("create_time") ?? "";
                    pay_succ_time = GetXmlValue("pay_succ_time") ?? "";
                    reason = GetXmlValue("reason") ?? "";
                }
            }
        }

        public bool IsResultCodeSuccess()
        {
            return result_code == "SUCCESS";
        }
    }

    /// <summary>
    /// 获取 RSA 加密公钥接口 返回结果
    /// </summary>
    public class GetPublicKeyResult : TenPayV3Result
    {
        #region 以下字段在return_code为SUCCESS的时候有返回
        /// <summary>
        /// <para>业务结果</para>
        /// <para>SUCCESS/FAIL，注意：当状态为FAIL时，存在业务结果未明确的情况，所以如果状态y为FAIL，请务必通过查询接口确认此次付款的结果（关注错误码err_code字段）。如果要继续进行这笔付款，请务必用原商户订单号和原参数来重入此接口。</para>
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// <para>错误代码</para>
        /// <para>错误码信息，注意：出现未明确的错误码时，如（SYSTEMERROR）等，请务必用原商户订单号重试，或通过查询接口确认此次付款的结果</para>
        /// </summary>
        public string err_code { get; set; }
        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        #endregion

        #region 以下字段在return_code 和result_code都为SUCCESS的时候有返回

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 密钥（RSA 公钥）
        /// </summary>
        public string pub_key { get; set; }

        #endregion


        /// <summary>
        /// 获取 RSA 加密公钥接口 返回结果 构造函数
        /// </summary>
        /// <param name="resultXml"></param>
        public GetPublicKeyResult(string resultXml) : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {

                result_code = GetXmlValue("result_code") ?? "";
                err_code = GetXmlValue("err_code") ?? "";
                err_code_des = GetXmlValue("err_code_des") ?? "";

                if (this.IsResultCodeSuccess())
                {
                    mch_id = GetXmlValue("mch_id") ?? "";
                    pub_key = GetXmlValue("pub_key") ?? "";
                }
            }
        }

        public bool IsResultCodeSuccess()
        {
            return result_code == "SUCCESS";
        }
    }
}
