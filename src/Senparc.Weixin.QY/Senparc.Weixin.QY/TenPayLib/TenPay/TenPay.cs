/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
 
    文件名：TenPay.cs
    文件功能描述：企业号微信支付接口
    
    
    创建标识：Senparc - 20150722
----------------------------------------------------------------*/

/*
    官方API：https://pay.weixin.qq.com/wiki/doc/api/mch_pay.php?chapter=14_2
 */

using System.IO;
using System.Text;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    /// <summary>
    /// 企业号微信支付接口
    /// </summary>
    public static class TenPay
    {
        /// <summary>
        /// 用于企业向微信用户个人付款 
        /// 目前支持向指定微信用户的openid付款
        /// </summary>
        /// <param name="data">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string Transfers(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms, timeOut: timeOut);
        }

        /// <summary>
        /// 用于商户的企业付款操作进行结果查询，返回付款操作详细结果。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static string GetTransferInfo(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gettransferinfo";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms, timeOut: timeOut);
        }
    }
}
