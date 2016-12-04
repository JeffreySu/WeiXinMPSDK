using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    ///  微信支付ReturnData[统一下单]
    /// </summary>
    public class TenPayV3ReturnData
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public TenPayV3ReturnData_ReturnCode ReturnCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string ReturnMsg { get; set; }

        public TenPayV3ResultCode ResultData { get; set; }


        public TenPayV3ReturnData(string resultXml)
        {
            var res = XDocument.Parse(resultXml);
            ReturnCode = res.Element("xml").Element("return_code").Value =="SUCCESS"
                        ? TenPayV3ReturnData_ReturnCode.SUCCESS
                        : TenPayV3ReturnData_ReturnCode.FAIL;

            if (!IsSuccess())
            {
                return;
            }

            var appId = res.Element("xml").Element("appid").Value;
            var mchId = res.Element("xml").Element("mch_id").Value;
            var deviceInfo = res.Element("xml").Element("device_info").Value ?? "";
            var nonceStr = res.Element("xml").Element("nonce_str").Value;
            var sign = res.Element("xml").Element("sign").Value;
            var errCode = res.Element("xml").Element("err_code").Value ?? "";
            var errCodeDes = res.Element("xml").Element("err_code_des").Value ?? "";

            var resultCode = (TenPayV3CodeState)Enum.Parse(typeof(TenPayV3CodeState), res.Element("xml").Element("result_code").Value);
            var result = new TenPayV3ResultCode(appId, mchId, nonceStr, sign, resultCode, errCode, errCodeDes, deviceInfo);
            if (returnCode == TenPayV3CodeState.SUCCESS)
            {
                var tradetype =
           (TenPayV3Type)Enum.Parse(typeof(TenPayV3Type), res.Element("xml").Element("trade_type").Value);
                var prepayId = res.Element("xml").Element("prepay_id").Value;
                var codeurl = tradetype == TenPayV3Type.NATIVE ? res.Element("xml").Element("code_url").Value : "";
                result.SuccessData = new TenPayV3SuccessData(tradetype, prepayId, codeurl);
            }
            data.ResultData = result;
        }

        public TenPayV3ReturnData(string returnCode, string returnMsg = null, TenPayV3ResultCode resultData = null)
        {
            ReturnCode = returnCode;
            ReturnMsg = returnMsg;
            ResultData = resultData;
        }

        public bool IsSuccess()
        {
            return ReturnCode == "SUCCESS";
        }
    }
    /// <summary>
    ///  微信支付ResultData[统一下单]
    /// </summary>
    public class TenPayV3ReturnData_ResultData
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }
        /// <summary>
        /// 终端设备号[非必填]
        /// </summary>
        public string DeviceInfo { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }
        /// <summary>
        /// 业务结果[SUCCESS/FAIL]
        /// </summary>
        public TenPayV3CodeState ResultCode { get; set; }
        /// <summary>
        /// 错误代码[非必填]
        /// </summary>
        public string ErrCode { get; set; }
        /// <summary>
        /// 错误代码描述[非必填]
        /// </summary>
        public string ErrCodeDes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TenPayV3SuccessData SuccessData { get; set; }
        public TenPayV3ResultCode(string appId, string mchId, string nonceStr, string sign, TenPayV3CodeState resultCode, string errCode = null, string errCodeDes = null, string deviceInfo = null, TenPayV3SuccessData successData = null)
        {
            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            DeviceInfo = deviceInfo;
            Sign = sign;
            ResultCode = resultCode;
            ErrCode = errCode;
            ErrCodeDes = errCodeDes;
            SuccessData = successData;
        }
    }
    /// <summary>
    /// 微信支付Success数据[统一下单]
    /// </summary>
    public class TenPayV3SuccessData
    {
        /// <summary>
        /// 交易类型
        /// </summary>
        public TenPayV3Type TradeType { get; set; }
        /// <summary>
        /// 微信生成的预支付回话标识
        /// </summary>
        public string PrepayId { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public string CodeUrl { get; set; }

        public TenPayV3SuccessData(TenPayV3Type tradeType, string prepayId, string codeUrl = null)
        {
            TradeType = tradeType;
            PrepayId = prepayId;
            CodeUrl = codeUrl;
        }
    }

}
