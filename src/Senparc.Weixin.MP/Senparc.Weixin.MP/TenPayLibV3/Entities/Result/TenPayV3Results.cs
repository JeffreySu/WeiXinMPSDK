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
 
    文件名：TenPayV3Results.cs
    文件功能描述：微信支付V3返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20161202
    修改描述：v14.3.109 命名空间由Senparc.Weixin.MP.AdvancedAPIs改为Senparc.Weixin.MP.TenPayLibV3
    
    修改标识：Senparc - 20161205
    修改描述：v14.3.110 完善XML转换信息

    修改标识：Senparc - 20161206
    修改描述：v14.3.111 处理UnifiedorderResult数据处理问题

    修改标识：Senparc - 20161226
    修改描述：v14.3.112 增加OrderQueryResult,CloseOrderResult,RefundQuery,ShortUrlResult,ReverseResult,MicropayResult

    修改标识：Senparc - 20170215
    修改描述：v14.3.126 增加TransfersResult类

    修改标识：Senparc - 20170215
    修改描述：v14.3.126 增加GetTransferInfoResult类

    修改标识：Senparc - 20170316
    修改描述：v14.3.132 完善UnifiedorderResult 服务商统一订单接口

    修改标识：Senparc - 20170322
    修改描述：v14.3.132 完善OrderQueryResult 服务商查询订单接口
    
    修改标识：jiehanlin & Senparc - 20180309
    修改描述：v14.10.5 TenPayV3Result 增加 ResultXML 只读属性 & 优化代码

    修改标识：jiehanlin & Senparc - 20180309
    修改描述：v14.10.12 新增 TenpayV3GetSignKeyResult

    修改标识：Senparc - 20171129
    修改描述：添加PayBankResult（付款到银行卡）
    
    修改标识：Senparc - 20180409
    修改描述：将 TenPayV3Result.cs 改名为 TenPayV3Results.cs
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.TenPayLibV3
{

    #region 基类

    /// <summary>
    /// 基础返回结果（微信支付返回结果基类）
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class TenPayV3Result
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }

        protected XDocument _resultXml;

        /// <summary>
        /// XML内容
        /// </summary>
        public string ResultXml
        {
            get
            {
                return _resultXml.ToString();

                //StringWriter sw = new StringWriter();
                //XmlTextWriter xmlTextWriter = new XmlTextWriter(sw);
                //_resultXml.WriteTo(xmlTextWriter);
                //return sw.ToString();
            }
        }

        public TenPayV3Result(string resultXml)
        {
            _resultXml = XDocument.Parse(resultXml);
            return_code = GetXmlValue("return_code"); // res.Element("xml").Element
            if (!IsReturnCodeSuccess())
            {
                return_msg = GetXmlValue("return_msg"); // res.Element("xml").Element
            }
        }

        /// <summary>
        /// 获取Xml结果中对应节点的值
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public string GetXmlValue(string nodeName)
        {
            if (_resultXml == null || _resultXml.Element("xml") == null
                || _resultXml.Element("xml").Element(nodeName) == null)
            {
                return "";
            }
            return _resultXml.Element("xml").Element(nodeName).Value;
        }

        /// <summary>
        /// 获取Xml结果中对应节点的集合值
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public IList<T> GetXmlValues<T>(string nodeName)
        {
            var result = new List<T>();
            try
            {
                if (_resultXml != null)
                {
                    var xElement = _resultXml.Element("xml");
                    if (xElement != null)
                    {
                        var nodeList = xElement.Elements().Where(z => z.Name.ToString().StartsWith(nodeName));
                        result = nodeList.Cast<T>().ToList();
                    }
                }
            }
            catch (System.Exception)
            {
                result = null;
            }
            return result;
        }


        public bool IsReturnCodeSuccess()
        {
            return return_code == "SUCCESS";
        }
    }

    /// <summary>
    /// 统一支付接口在 return_code为 SUCCESS的时候有返回
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class Result : TenPayV3Result
    {
        /// <summary>
        /// 微信分配的公众账号ID（付款到银行卡接口，此字段不提供）
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string mch_id { get; set; }

        #region 服务商
        /// <summary>
        /// 子商户公众账号ID
        /// </summary>
        public string sub_appid { get; set; }

        /// <summary>
        /// 子商户号
        /// </summary>
        public string sub_mch_id { get; set; }

        #endregion

        /// <summary>
        /// 随机字符串，不长于32 位
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }

        public string err_code { get; set; }
        public string err_code_des { get; set; }

        public Result(string resultXml)
            : base(resultXml)
        {
            result_code = GetXmlValue("result_code"); // res.Element("xml").Element

            if (base.IsReturnCodeSuccess())
            {
                appid = GetXmlValue("appid") ?? "";
                mch_id = GetXmlValue("mch_id") ?? "";

                #region 服务商
                sub_appid = GetXmlValue("sub_appid") ?? "";
                sub_mch_id = GetXmlValue("sub_mch_id") ?? "";
                #endregion

                nonce_str = GetXmlValue("nonce_str") ?? "";
                sign = GetXmlValue("sign") ?? "";
                err_code = GetXmlValue("err_code") ?? "";
                err_code_des = GetXmlValue("err_code_des") ?? "";
            }
        }

        /// <summary>
        /// result_code == "SUCCESS"
        /// </summary>
        /// <returns></returns>
        public bool IsResultCodeSuccess()
        {
            return result_code == "SUCCESS";
        }
    }

    #endregion

    /// <summary>
    /// 统一支付接口在return_code 和result_code 都为SUCCESS 的时候有返回详细信息
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class UnifiedorderResult : Result
    {
        /// <summary>
        /// 微信支付分配的终端设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 交易类型:JSAPI、NATIVE、APP
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /// 微信生成的预支付ID，用于后续接口调用中使用
        /// </summary>
        public string prepay_id { get; set; }

        /// <summary>
        /// trade_type为NATIVE时有返回，此参数可直接生成二维码展示出来进行扫码支付
        /// </summary>
        public string code_url { get; set; }

        /// <summary>
        /// 在H5支付时返回
        /// </summary>
        public string mweb_url { get; set; }

        ///// <summary>
        ///// 子商户公众账号ID
        ///// </summary>
        //public string sub_appid { get; set; }

        ///// <summary>
        ///// 子商户号
        ///// </summary>
        //public string sub_mch_id { get; set; }

        public UnifiedorderResult(string resultXml)
            : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {
                device_info = GetXmlValue("device_info") ?? "";
                //sub_appid = GetXmlValue("sub_appid") ?? "";
                //sub_mch_id = GetXmlValue("sub_mch_id") ?? "";

                if (base.IsResultCodeSuccess())
                {
                    trade_type = GetXmlValue("trade_type") ?? "";
                    prepay_id = GetXmlValue("prepay_id") ?? "";
                    code_url = GetXmlValue("code_url") ?? "";
                    mweb_url = GetXmlValue("mweb_url") ?? "";
                }
            }
        }
    }

    /// <summary>
    /// 查询订单接口返回结果
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class OrderQueryResult : Result
    {
        /// <summary>
        /// 微信支付分配的终端设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 用户在商户appid下的唯一标识
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
        /// </summary>
        public string is_subscribe { get; set; }

        /// <summary>
        /// 用户子标识[服务商]
        /// </summary>
        public string sub_openid { get; set; }

        /// <summary>
        /// 是否关注子公众账号[服务商]
        /// </summary>
        public string sub_is_subscribe { get; set; }

        /// <summary>
        /// 调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP，MICROPAY
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        ///SUCCESS—支付成功
        ///REFUND—转入退款
        ///NOTPAY—未支付
        ///CLOSED—已关闭
        ///REVOKED—已撤销（刷卡支付）
        ///USERPAYING--用户支付中
        ///PAYERROR--支付失败(其他原因，如银行返回失败)
        /// </summary>
        public string trade_state { get; set; }

        /// <summary>
        /// 银行类型，采用字符串类型的银行标识
        /// </summary>
        public string bank_type { get; set; }

        /// <summary>
        /// 商品详情[服务商]
        /// </summary>
        public string detail { get; set; }

        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        public string total_fee { get; set; }

        /// <summary>
        /// 应结订单金额=订单金额-非充值代金券金额，应结订单金额<=订单金额
        /// </summary>
        public string settlement_total_fee { get; set; }

        /// <summary>
        /// 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string fee_type { get; set; }

        /// <summary>
        /// 现金支付金额订单现金支付金额
        /// </summary>
        public string cash_fee { get; set; }

        /// <summary>
        /// 货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string cash_fee_type { get; set; }

        /// <summary>
        /// “代金券”金额<=订单金额，订单金额-“代金券”金额=现金支付金额
        /// </summary>
        public string coupon_fee { get; set; }

        /// <summary>
        /// 代金券使用数量
        /// </summary>
        public string coupon_count { get; set; }

        /// <summary>
        /// CASH--充值代金券 
        ///NO_CASH---非充值代金券
        ///订单使用代金券时有返回（取值：CASH、NO_CASH）。$n为下标,从0开始编号，举例：coupon_type_$0
        ///coupon_type_$n
        /// </summary>
        public IList<string> coupon_type_values { get; set; }

        /// <summary>
        /// 代金券ID, $n为下标，从0开始编号
        /// coupon_id_$n
        /// </summary>
        public IList<string> coupon_id_values { get; set; }

        /// <summary>
        /// 单个代金券支付金额, $n为下标，从0开始编号
        /// coupon_fee_$n
        /// </summary>
        public IList<int> coupon_fee_values { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户系统的订单号，与请求一致。
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 附加数据，原样返回
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 订单支付时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010
        /// </summary>
        public string time_end { get; set; }

        /// <summary>
        /// 对当前查询订单状态的描述和下一步操作的指引
        /// </summary>
        public string trade_state_desc { get; set; }

        public OrderQueryResult(string resultXml)
            : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {
                if (base.IsResultCodeSuccess())
                {
                    device_info = GetXmlValue("device_info") ?? "";

                    openid = GetXmlValue("openid") ?? "";
                    is_subscribe = GetXmlValue("is_subscribe") ?? "";

                    sub_openid = GetXmlValue("sub_openid") ?? "";               //用户子标识[服务商]
                    sub_is_subscribe = GetXmlValue("sub_is_subscribe") ?? "";   //是否关注子公众账号[服务商]

                    trade_type = GetXmlValue("trade_type") ?? "";
                    trade_state = GetXmlValue("trade_state") ?? "";
                    bank_type = GetXmlValue("bank_type") ?? "";

                    detail = GetXmlValue("detail") ?? "";                       //商品详情[服务商]

                    total_fee = GetXmlValue("total_fee") ?? "";

                    settlement_total_fee = GetXmlValue("settlement_total_fee") ?? "";

                    fee_type = GetXmlValue("fee_type") ?? "";
                    cash_fee = GetXmlValue("cash_fee") ?? "";
                    cash_fee_type = GetXmlValue("cash_fee_type") ?? "";
                    coupon_fee = GetXmlValue("coupon_fee") ?? "";
                    coupon_count = GetXmlValue("coupon_count") ?? "";

                    #region 特殊"$n"

                    coupon_type_values = GetXmlValues<string>("coupon_type_") ?? new List<string>();
                    coupon_id_values = GetXmlValues<string>("coupon_id_") ?? new List<string>();
                    coupon_fee_values = GetXmlValues<int>("coupon_fee_") ?? new List<int>();

                    #endregion

                    transaction_id = GetXmlValue("transaction_id") ?? "";
                    out_trade_no = GetXmlValue("out_trade_no") ?? "";
                    attach = GetXmlValue("attach") ?? "";
                    time_end = GetXmlValue("time_end") ?? "";
                    trade_state_desc = GetXmlValue("trade_state_desc") ?? "";
                }
            }
        }
    }

    /// <summary>
    /// 关闭订单接口
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class CloseOrderResult : Result
    {
        /// <summary>
        /// 对于业务执行的详细描述
        /// </summary>
        public string result_msg { get; set; }

        public CloseOrderResult(string resultXml) : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {
                result_msg = GetXmlValue("result_msg") ?? "";
            }
        }
    }

    /// <summary>
    /// 申请退款接口
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class RefundResult : Result
    {
        #region 错误代码
        /*
            名称  描述 原因  解决方案
            SYSTEMERROR 接口返回错误 系统超时等   请不要更换商户退款单号，请使用相同参数再次调用API。
        TRADE_OVERDUE 订单已经超过退款期限  订单已经超过可退款的最大期限(支付后一年内可退款)   请选择其他方式自行退款
            ERROR   业务错误 申请退款业务发生错误  该错误都会返回具体的错误原因，请根据实际返回做相应处理。
        USER_ACCOUNT_ABNORMAL 退款请求失败  用户帐号注销 此状态代表退款申请失败，商户可自行处理退款。
        INVALID_REQ_TOO_MUCH 无效请求过多  连续错误请求数过多被系统短暂屏蔽 请检查业务是否正常，确认业务正常后请在1分钟后再来重试
            NOTENOUGH   余额不足 商户可用退款余额不足  此状态代表退款申请失败，商户可根据具体的错误提示做相应的处理。
        INVALID_TRANSACTIONID 无效transaction_id    请求参数未按指引进行填写 请求参数错误，检查原交易号是否存在或发起支付交易接口返回失败
            PARAM_ERROR 参数错误 请求参数未按指引进行填写    请求参数错误，请重新检查再调用退款申请
            APPID_NOT_EXIST APPID不存在 参数中缺少APPID  请检查APPID是否正确
            MCHID_NOT_EXIST MCHID不存在 参数中缺少MCHID  请检查MCHID是否正确
            APPID_MCHID_NOT_MATCH   appid和mch_id不匹配 appid和mch_id不匹配 请确认appid和mch_id是否匹配
            REQUIRE_POST_METHOD 请使用post方法 未使用post传递参数     请检查请求参数是否通过post方法提交
            SIGNERROR   签名错误 参数签名结果不正确   请检查签名参数和方法是否都符合签名算法要求
            XML_FORMAT_ERROR    XML格式错误 XML格式错误 请检查XML参数格式是否正确
            FREQUENCY_LIMITED   频率限制	2个月之前的订单申请退款有频率限制 该笔退款未受理，请降低频率后重试
         */

        #endregion


        /// <summary>
        /// 	微信支付分配的终端设备号，与下单一致
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string transaction_id { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 商户退款单号	
        /// </summary>
        public string out_refund_no { get; set; }
        /// <summary>
        /// 微信退款单号
        /// </summary>
        public string refund_id { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public string refund_fee { get; set; }
        /// <summary>
        /// 应结退款金额
        /// </summary>
        public string settlement_refund_fee { get; set; }
        /// <summary>
        /// 标价金额
        /// </summary>
        public string total_fee { get; set; }
        /// <summary>
        /// 应结订单金额
        /// </summary>
        public string settlement_total_fee { get; set; }
        /// <summary>
        /// 标价币种
        /// </summary>
        public string fee_type { get; set; }
        /// <summary>
        /// 现金支付金额
        /// </summary>
        public string cash_fee { get; set; }
        /// <summary>
        /// 现金支付币种
        /// </summary>
        public string cash_fee_type { get; set; }
        /// <summary>
        /// 现金退款金额	
        /// </summary>
        public string cash_refund_fee { get; set; }
        /// <summary>
        /// 代金券退款总金额
        /// </summary>
        public string coupon_refund_fee { get; set; }
        /// <summary>
        /// 退款代金券使用数量
        /// </summary>
        public string coupon_refund_count { get; set; }


        #region 带下标参数

        /// <summary>
        /// 代金券类型
        /// </summary>
        public IList<string> coupon_type_n { get; set; }
        /// <summary>
        /// 单个代金券退款金额
        /// </summary>
        public IList<int> coupon_refund_fee_n { get; set; }
        /// <summary>
        /// 退款代金券ID	
        /// </summary>
        public IList<string> coupon_refund_id_n { get; set; }

        #endregion


        public RefundResult(string resultXml) : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {
                result_code = GetXmlValue("result_code");
                err_code = GetXmlValue("err_code");
                err_code_des = GetXmlValue("err_code_des");
                appid = GetXmlValue("appid");
                mch_id = GetXmlValue("mch_id");
                device_info = GetXmlValue("device_info");
                nonce_str = GetXmlValue("nonce_str");
                sign = GetXmlValue("sign");
                transaction_id = GetXmlValue("transaction_id");
                out_trade_no = GetXmlValue("out_trade_no");
                out_refund_no = GetXmlValue("out_refund_no");
                refund_id = GetXmlValue("refund_id");
                refund_fee = GetXmlValue("refund_fee");
                settlement_refund_fee = GetXmlValue("settlement_refund_fee");
                total_fee = GetXmlValue("total_fee");
                settlement_total_fee = GetXmlValue("settlement_total_fee");
                fee_type = GetXmlValue("fee_type");
                cash_fee = GetXmlValue("cash_fee");
                cash_fee_type = GetXmlValue("cash_fee_type");
                cash_refund_fee = GetXmlValue("cash_refund_fee");
                coupon_refund_fee = GetXmlValue("coupon_refund_fee");
                coupon_refund_count = GetXmlValue("coupon_refund_count");
                coupon_type_n = GetXmlValues<string>("coupon_type_n");
                coupon_refund_fee_n = GetXmlValues<int>("coupon_refund_fee_n");
                coupon_refund_id_n = GetXmlValues<string>("coupon_refund_id_n");
            }
        }
    }

    /// <summary>
    /// 退款查询接口
    /// </summary>
    public class RefundQueryResult : Result
    {
        /// <summary>
        /// 终端设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户系统内部的订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 订单总金额，单位为分，只能为整数
        /// </summary>
        public string total_fee { get; set; }

        /// <summary>
        /// 应结订单金额=订单金额-非充值代金券金额，应结订单金额<=订单金额
        /// </summary>
        public string settlement_total_fee { get; set; }

        /// <summary>
        /// 订单金额货币类型，符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string fee_type { get; set; }

        /// <summary>
        /// 现金支付金额，单位为分，只能为整数
        /// </summary>
        public string cash_fee { get; set; }

        /// <summary>
        /// 退款记录数
        /// </summary>
        public string refund_count { get; set; }

        /// <summary>
        /// 商户退款单号
        /// </summary>
        ///public string out_refund_no_$n { get; set; }
        /// <summary>
        /// 微信退款单号
        /// </summary>
        //public string refund_id_$n { get; set; }
        /// <summary>
        /// ORIGINAL—原路退款
        //BALANCE—退回到余额
        /// </summary>
        //public string refund_channel_$n { get; set; }
        /// <summary>
        /// 退款总金额,单位为分,可以做部分退款
        /// </summary>
        //public string refund_fee_$n { get; set; }
        /// <summary>
        /// 退款金额=申请退款金额-非充值代金券退款金额，退款金额<=申请退款金额
        /// </summary>
        //public string settlement_refund_fee_$n { get; set; }
        /// <summary>
        /// REFUND_SOURCE_RECHARGE_FUNDS---可用余额退款/基本账户
        //REFUND_SOURCE_UNSETTLED_FUNDS---未结算资金退款
        /// </summary>
        public string refund_account { get; set; }

        /// <summary>
        /// CASH--充值代金券 
        //NO_CASH---非充值代金券
        //订单使用代金券时有返回（取值：CASH、NO_CASH）。$n为下标,从0开始编号，举例：coupon_type_$0
        /// </summary>
        //public string coupon_type_$n { get; set; }
        /// <summary>
        /// 代金券退款金额<=退款金额，退款金额-代金券或立减优惠退款金额为现金
        /// </summary>
        //public string coupon_refund_fee_$n { get; set; }
        /// <summary>
        /// 退款代金券使用数量 ,$n为下标,从0开始编号
        /// </summary>
        //public string coupon_refund_count_$n { get; set; }
        /// <summary>
        /// 退款代金券ID, $n为下标，$m为下标，从0开始编号
        /// </summary>
        //public string coupon_refund_id_$n_$m { get; set; }
        /// <summary>
        /// 单个退款代金券支付金额, $n为下标，$m为下标，从0开始编号
        /// </summary>
        ///public string coupon_refund_fee_$n_$m { get; set; }
        /// <summary>
        /// 退款状态：
        ///SUCCESS—退款成功
        ///FAIL—退款失败
        ///PROCESSING—退款处理中
        ///CHANGE—转入代发，退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，资金回流到商户的现金帐号，需要商户人工干预，通过线下或者财付通转账的方式进行退款。
        /// </summary>
        //public string refund_status_$n { get; set; }
        /// <summary>
        /// 取当前退款单的退款入账方
        ///1）退回银行卡：
        ///{银行名称
        ///    }{卡类型
        ///}{卡尾号}
        ///2）退回支付用户零钱:
        ///支付用户零钱
        /// </summary>
        //public string refund_recv_accout_$n { get; set; }
        public RefundQueryResult(string resultXml) : base(resultXml)
        {
            if (base.IsResultCodeSuccess())
            {
                device_info = GetXmlValue("device_info") ?? "";
                transaction_id = GetXmlValue("transaction_id") ?? "";
                out_trade_no = GetXmlValue("out_trade_no") ?? "";
                total_fee = GetXmlValue("total_fee") ?? "";
                settlement_total_fee = GetXmlValue("settlement_total_fee") ?? "";
                fee_type = GetXmlValue("fee_type") ?? "";
                cash_fee = GetXmlValue("cash_fee") ?? "";
                refund_count = GetXmlValue("refund_count") ?? "";
                //out_refund_no_$n = GetXmlValue("out_refund_no_$n") ?? "";
                //refund_id_$n = GetXmlValue("refund_id_$n") ?? "";
                //refund_channel_$n = GetXmlValue("refund_channel_$n") ?? "";
                //refund_fee_$n = GetXmlValue("refund_fee_$n") ?? "";
                //settlement_refund_fee_$n = GetXmlValue("settlement_refund_fee_$n") ?? "";
                refund_account = GetXmlValue("refund_account") ?? "";
                //coupon_type_$n = GetXmlValue("coupon_type_$n") ?? "";
                //coupon_refund_fee_$n = GetXmlValue("coupon_refund_fee_$n") ?? "";
                //coupon_refund_count_$n = GetXmlValue("coupon_refund_count_$n") ?? "";
                //coupon_refund_id_$n_$m = GetXmlValue("coupon_refund_id_$n_$m") ?? "";
                //coupon_refund_fee_$n_$m = GetXmlValue("coupon_refund_fee_$n_$m") ?? "";
                //refund_status_$n = GetXmlValue("refund_status_$n") ?? "";
                //refund_recv_accout_$n = GetXmlValue("refund_recv_accout_$n") ?? "";
            }
        }
    }

    /// <summary>
    /// 短链接转换接口
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class ShortUrlResult : Result
    {
        /// <summary>
        /// 转换后的URL
        /// </summary>
        public string short_url { get; set; }

        public ShortUrlResult(string resultXml) : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {
                short_url = GetXmlValue("short_url") ?? "";
            }
        }
    }

    ///// <summary>
    ///// 对账单接口
    ///// </summary>
    //public class DownloadBillResult : TenPayV3Result
    //{
    //    public DownloadBillResult(string resultXml) : base(resultXml)
    //    {

    //    }
    //}

    /// <summary>
    /// 撤销订单接口
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class ReverseResult : Result
    {
        /// <summary>
        /// 是否需要继续调用撤销，Y-需要，N-不需要
        /// </summary>
        public string recall { get; set; }

        public ReverseResult(string resultXml) : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {
                recall = GetXmlValue("recall") ?? "";
            }
        }
    }

    /// <summary>
    /// 刷卡支付
    /// 提交被扫支付
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class MicropayResult : Result
    {
        /// <summary>
        /// 调用接口提交的终端设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 用户在商户appid 下的唯一标识
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 用户是否关注公众账号，仅在公众账号类型支付有效，取值范围：Y或N;Y-关注;N-未关注
        /// </summary>
        public string is_subscribe { get; set; }

        /// <summary>
        /// 支付类型为MICROPAY(即扫码支付)
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /// 银行类型，采用字符串类型的银行标识
        /// </summary>
        public string bank_type { get; set; }

        /// <summary>
        /// 符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string fee_type { get; set; }

        /// <summary>
        /// 订单总金额，单位为分，只能为整数
        /// </summary>
        public string total_fee { get; set; }

        /// <summary>
        /// 应结订单金额=订单金额-非充值代金券金额，应结订单金额<=订单金额
        /// </summary>
        public string settlement_total_fee { get; set; }

        /// <summary>
        /// “代金券”金额<=订单金额，订单金额-“代金券”金额=现金支付金额
        /// </summary>
        public string coupon_fee { get; set; }

        /// <summary>
        /// 符合ISO 4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string cash_fee_type { get; set; }

        /// <summary>
        /// 订单现金支付金额
        /// </summary>
        public string cash_fee { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户系统的订单号，与请求一致
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 商家数据包，原样返回
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 订单生成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010
        /// </summary>
        public string time_end { get; set; }

        public MicropayResult(string resultXml) : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {
                device_info = GetXmlValue("device_info") ?? "";
                if (base.IsResultCodeSuccess())
                {
                    openid = GetXmlValue("openid") ?? "";
                    is_subscribe = GetXmlValue("is_subscribe") ?? "";
                    trade_type = GetXmlValue("trade_type") ?? "";
                    bank_type = GetXmlValue("bank_type") ?? "";
                    fee_type = GetXmlValue("fee_type") ?? "";
                    total_fee = GetXmlValue("total_fee") ?? "";
                    settlement_total_fee = GetXmlValue("settlement_total_fee") ?? "";
                    coupon_fee = GetXmlValue("coupon_fee") ?? "";
                    cash_fee_type = GetXmlValue("cash_fee_type") ?? "";
                    cash_fee = GetXmlValue("cash_fee") ?? "";
                    transaction_id = GetXmlValue("transaction_id") ?? "";
                    out_trade_no = GetXmlValue("out_trade_no") ?? "";
                    attach = GetXmlValue("attach") ?? "";
                    time_end = GetXmlValue("time_end") ?? "";
                }
            }
        }
    }


    /// <summary>
    /// 企业付款
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class TransfersResult : TenPayV3Result
    {
        /// <summary>
        /// 商户appid
        /// </summary>
        public string mch_appid { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string device_info { get; set; }


        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        ///业务结果 SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        ///商户订单号
        /// </summary>
        public string partner_trade_no { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string payment_no { get; set; }

        /// <summary>
        /// 微信支付成功时间 
        /// </summary>
        public string payment_time { get; set; }

        public TransfersResult(string resultXml) : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {
                mch_appid = GetXmlValue("mch_appid") ?? "";
                mchid = GetXmlValue("mchid") ?? "";
                device_info = GetXmlValue("device_info") ?? "";
                nonce_str = GetXmlValue("nonce_str") ?? "";
                result_code = GetXmlValue("result_code") ?? "";
                err_code = GetXmlValue("err_code") ?? "";
                err_code_des = GetXmlValue("err_code_des") ?? "";
                if (IsResultCodeSuccess())
                {
                    partner_trade_no = GetXmlValue("partner_trade_no") ?? "";
                    payment_no = GetXmlValue("payment_no") ?? "";
                    payment_time = GetXmlValue("payment_time") ?? "";
                }
            }
        }
        public bool IsResultCodeSuccess()
        {
            return result_code == "SUCCESS";
        }
    }

    /// <summary>
    /// 商户的企业付款操作进行结果查询，返回付款操作详细结果
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class GetTransferInfoResult : TenPayV3Result
    {

        /// <summary>
        ///业务结果 SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        ///商户单号
        /// </summary>
        public string partner_trade_no { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 付款单号
        /// </summary>
        public string detail_id { get; set; }

        /// <summary>
        ///转账状态
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string reason { get; set; }

        /// <summary>
        /// 收款用户openid
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 收款用户姓名
        /// </summary>
        public string transfer_name { get; set; }

        /// <summary>
        ///付款金额
        /// </summary>
        public int payment_amount { get; set; }

        /// <summary>
        /// 转账时间
        /// </summary>
        public string transfer_time { get; set; }

        /// <summary>
        /// 付款描述
        /// </summary>
        public string desc { get; set; }

        public GetTransferInfoResult(string resultXml) : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {
                result_code = GetXmlValue("result_code") ?? "";
                err_code = GetXmlValue("err_code") ?? "";
                err_code_des = GetXmlValue("err_code_des") ?? "";
                if (IsResultCodeSuccess())
                {
                    partner_trade_no = GetXmlValue("partner_trade_no") ?? "";
                    mch_id = GetXmlValue("mch_id") ?? "";
                    detail_id = GetXmlValue("detail_id") ?? "";
                    status = GetXmlValue("status") ?? "";
                    reason = GetXmlValue("reason") ?? "";
                    openid = GetXmlValue("openid") ?? "";
                    transfer_name = GetXmlValue("transfer_name") ?? "";
                    payment_amount = int.Parse(GetXmlValue("payment_amount"));
                    transfer_time = GetXmlValue("transfer_time") ?? "";
                    desc = GetXmlValue("desc") ?? "";
                }
            }
        }
        public bool IsResultCodeSuccess()
        {
            return result_code == "SUCCESS";
        }
    }



    /// <summary>
    /// 获取验签秘钥API 返回结果
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public class TenpayV3GetSignKeyResult : TenPayV3Result
    {
        ///// <summary>
        ///// SUCCESS/FAIL 此字段是通信标识，非交易标识
        ///// </summary>
        //public string return_code { get; set; }

        ///// <summary>
        ///// 返回信息，如非空，为错误原因 ，签名失败 ，参数格式校验错误
        ///// </summary>
        //public string return_msg { get; set; }

        /// <summary>
        /// 微信支付分配的微信商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 返回的沙箱密钥
        /// </summary>
        public string sandbox_signkey { get; set; }

        /// <summary>
        /// 获取验签秘钥API 返回结果 构造函数
        /// </summary>
        /// <param name="resultXml"></param>
        public TenpayV3GetSignKeyResult(string resultXml) : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {
                mch_id = GetXmlValue("mch_id") ?? "";
                sandbox_signkey = GetXmlValue("sandbox_signkey") ?? "";
            }
        }
    }
}
