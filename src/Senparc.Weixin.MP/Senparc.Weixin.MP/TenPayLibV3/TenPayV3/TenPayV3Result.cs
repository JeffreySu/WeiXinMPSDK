/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
 
    文件名：TenPayV3Result.cs
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

----------------------------------------------------------------*/

using System.Xml.Linq;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    #region 基类
    
    /// <summary>
    /// 基础返回结果
    /// </summary>
    public class TenPayV3Result
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }

        protected XDocument _resultXml;

        public TenPayV3Result(string resultXml)
        {
            _resultXml = XDocument.Parse(resultXml);
            return_code = GetXmlValue("return_code");// res.Element("xml").Element
            if (!IsReturnCodeSuccess())
            {
                return_msg = GetXmlValue("return_msg");// res.Element("xml").Element
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
                || _resultXml.Element("xml").Element(nodeName)==null)
            {
                return null;
            }
            return _resultXml.Element("xml").Element(nodeName).Value;
        }

        public bool IsReturnCodeSuccess()
        {
            return return_code == "SUCCESS";
        }
    }
    /// <summary>
    /// 统一支付接口在 return_code为 SUCCESS的时候有返回
    /// </summary>
    public class Result : TenPayV3Result
    {
        /// <summary>
        /// 微信分配的公众账号ID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string mch_id { get; set; }
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
            result_code = GetXmlValue("result_code");// res.Element("xml").Element

            if (base.IsReturnCodeSuccess())
            {
                appid = GetXmlValue("appid") ?? "";
                mch_id = GetXmlValue("mch_id") ?? "";
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

        public UnifiedorderResult(string resultXml)
            : base(resultXml)
        {
            if (base.IsReturnCodeSuccess())
            {
                device_info = GetXmlValue("device_info") ?? "";

                if (base.IsResultCodeSuccess())
                {
                    trade_type = GetXmlValue("trade_type") ?? "";
                    prepay_id = GetXmlValue("prepay_id") ?? "";
                    code_url = GetXmlValue("code_url") ?? "";
                }
            }
        }
    }

    /// <summary>
    /// 查询订单接口返回结果
    /// </summary>
    public class OrderQueryResult : Result
    {
        public OrderQueryResult(string resultXml)
            : base(resultXml)
        {

        }
    }
}
