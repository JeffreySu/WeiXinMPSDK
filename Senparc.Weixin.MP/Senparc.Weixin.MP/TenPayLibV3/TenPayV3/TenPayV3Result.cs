using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 基础返回结果
    /// </summary>
    public class TenPayV3Result
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }
    }
    /// <summary>
    /// 统一支付接口在 return_code为 SUCCESS的时候有返回
    /// </summary>
    public class Result : TenPayV3Result
    {
        public string appid { get; set; }//微信分配的公众账号ID
        public string mch_id { get; set; }//微信支付分配的商户号
        public string device_info { get; set; }//微信支付分配的终端设备号
        public string nonce_str { get; set; }//随机字符串，不长于32 位
        public string sign { get; set; }//签名
        public string result_code { get; set; }//SUCCESS/FAIL
        public string err_code { get; set; }
        public string err_code_des { get; set; }
    }
    /// <summary>
    /// 统一支付接口在return_code 和result_code 都为SUCCESS 的时候有返回
    /// </summary>
    public class UnifiedorderResult : Result
    {
        public string trade_type { get; set; }//交易类型:JSAPI、NATIVE、APP
        public string prepay_id { get; set; }//微信生成的预支付ID，用于后续接口调用中使用
        public string code_url { get; set; }//trade_type为NATIVE时有返回，此参数可直接生成二维码展示出来进行扫码支付
    }
}
