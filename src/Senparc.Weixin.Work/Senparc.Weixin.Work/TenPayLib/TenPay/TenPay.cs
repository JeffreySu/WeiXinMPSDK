/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
 
    文件名：TenPay.cs
    文件功能描述：企业号微信支付接口
    
    
    创建标识：Senparc - 20150722
 
    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    修改标识：Ritazh - 20161207
    修改描述：v14.3.112 迁移企业支付方法
----------------------------------------------------------------*/

/*
    官方API：https://pay.weixin.qq.com/wiki/doc/api/mch_pay.php?chapter=14_2
 */

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.HttpUtility;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 企业号微信支付接口
    /// </summary>

    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public static class TenPay
    {
        #region 同步方法

        /// <summary>
        /// 用于企业向微信用户个人付款 
        /// 目前支持向指定微信用户的openid付款
        /// </summary>
        /// <param name="data">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("请使用Senparc.Weixin.MP.TenPayLibV3.Transfers()")]
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
        [Obsolete("请使用Senparc.Weixin.MP.TenPayLibV3.GetTransferInfo()")]
        public static string GetTransferInfo(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gettransferinfo";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return RequestUtility.HttpPost(urlFormat, null, ms, timeOut: timeOut);
        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        ///【异步方法】 用于企业向微信用户个人付款 
        /// 目前支持向指定微信用户的openid付款
        /// </summary>
        /// <param name="data">微信支付需要post的xml数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("请使用Senparc.Weixin.MP.TenPayLibV3.TransfersAsync()")]
        public static async Task<string> TransfersAsync(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync( urlFormat, null, ms, timeOut: timeOut);
        }

        /// <summary>
        /// 【异步方法】用于商户的企业付款操作进行结果查询，返回付款操作详细结果。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [Obsolete("请使用Senparc.Weixin.MP.TenPayLibV3.GetTransferInfoAsync()")]
        public static async Task<string> GetTransferInfoAsync(string data, int timeOut = Config.TIME_OUT)
        {
            var urlFormat = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gettransferinfo";

            var formDataBytes = data == null ? new byte[0] : Encoding.UTF8.GetBytes(data);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return await RequestUtility.HttpPostAsync( urlFormat, null, ms, timeOut: timeOut);
        }
        #endregion
#endif
    }
}
