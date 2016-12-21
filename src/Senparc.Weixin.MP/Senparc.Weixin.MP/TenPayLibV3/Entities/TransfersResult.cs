using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    public class TransfersResult
    {

        /// <summary>
        /// 返回状态码,SUCCESS/FAIL,此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断 
        /// </summary>
        public bool return_code { get; set; }

        /// <summary>
        /// 返回信息，如非空，为错误原因,签名失败,参数格式校验错误 
        /// </summary>
        public string return_msg { get; set; }

        /// <summary>
        /// 业务结果,SUCCESS/FAIL
        /// </summary>
        public bool result_code { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 微信分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string mch_appid { get; set; }

        /// <summary>
        /// 商户号，微信支付分配的商户号
        /// </summary>
        public string mchid { get; set; }
        /// <summary>
        /// 微信支付分配的终端设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 随机字符串，不长于32位
        /// </summary>
        public string nonce_str { get; set; }
        
        /// <summary>
        /// 商户订单号，需保持唯一性
        /// </summary>
        public string partner_trade_no { get; set; }

        /// <summary>
        /// 企业付款成功，返回的微信订单号
        /// </summary>
        public string payment_no { get; set; }

        /// <summary>
        /// 企业付款成功时间
        /// </summary>
        public string payment_time { get; set; }
    }
}
