#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：TenPayV3.Customs.cs
    文件功能描述：微信支付海关报关 V2 同步与异步接口


    创建标识：Senparc - 20260728

    修改标识：Senparc - 20260728
    修改描述：v1.20.0 新增报关、报关查询及重新申报接口

----------------------------------------------------------------*/

using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.CommonAPIs;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPay.V3
{
    public static partial class TenPayV3
    {
        private const string CustomsDeclareOrderPath =
            "/cgi-bin/mch/customs/customdeclareorder";
        private const string CustomsDeclareQueryPath =
            "/cgi-bin/mch/customs/customdeclarequery";
        private const string CustomsRedeclarePath =
            "/cgi-bin/mch/newcustoms/customdeclareredeclare";

        /// <summary>
        /// 提交支付订单海关报关。
        /// <para>该接口使用 XML 和 MD5 签名，不需要商户证书。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4011985151</para>
        /// </summary>
        /// <param name="data">报关订单、海关及订购人信息。</param>
        /// <param name="key">商户 API 密钥。</param>
        /// <param name="timeOut">超时时间，单位为毫秒。</param>
        public static CustomsDeclareOrderResult CustomsDeclareOrder(
            CustomsDeclareOrderRequestData data, string key,
            int timeOut = Config.TIME_OUT)
        {
            RequireCustomsData(data);
            return new CustomsDeclareOrderResult(PostCustoms(
                GetCustomsUrl(CustomsDeclareOrderPath), data.ToXml(key), timeOut));
        }

        /// <summary>异步提交支付订单海关报关。</summary>
        public static async Task<CustomsDeclareOrderResult>
            CustomsDeclareOrderAsync(CustomsDeclareOrderRequestData data,
                string key, int timeOut = Config.TIME_OUT)
        {
            RequireCustomsData(data);
            var xml = await PostCustomsAsync(GetCustomsUrl(CustomsDeclareOrderPath),
                data.ToXml(key), timeOut).ConfigureAwait(false);
            return new CustomsDeclareOrderResult(xml);
        }

        /// <summary>
        /// 查询海关报关状态。
        /// <para>该接口使用 XML 和 MD5 签名，不需要商户证书。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4011985273</para>
        /// </summary>
        public static CustomsDeclareQueryResult CustomsDeclareQuery(
            CustomsDeclareQueryRequestData data, string key,
            int timeOut = Config.TIME_OUT)
        {
            RequireCustomsData(data);
            return new CustomsDeclareQueryResult(PostCustoms(
                GetCustomsUrl(CustomsDeclareQueryPath), data.ToXml(key), timeOut));
        }

        /// <summary>异步查询海关报关状态。</summary>
        public static async Task<CustomsDeclareQueryResult>
            CustomsDeclareQueryAsync(CustomsDeclareQueryRequestData data,
                string key, int timeOut = Config.TIME_OUT)
        {
            RequireCustomsData(data);
            var xml = await PostCustomsAsync(GetCustomsUrl(CustomsDeclareQueryPath),
                data.ToXml(key), timeOut).ConfigureAwait(false);
            return new CustomsDeclareQueryResult(xml);
        }

        /// <summary>
        /// 对支付订单重新进行海关申报。
        /// <para>该接口使用 XML 和 MD5 签名，不需要商户证书。</para>
        /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4011985318</para>
        /// </summary>
        public static CustomsRedeclareResult CustomsRedeclare(
            CustomsRedeclareRequestData data, string key,
            int timeOut = Config.TIME_OUT)
        {
            RequireCustomsData(data);
            return new CustomsRedeclareResult(PostCustoms(
                GetCustomsUrl(CustomsRedeclarePath), data.ToXml(key), timeOut));
        }

        /// <summary>异步对支付订单重新进行海关申报。</summary>
        public static async Task<CustomsRedeclareResult> CustomsRedeclareAsync(
            CustomsRedeclareRequestData data, string key,
            int timeOut = Config.TIME_OUT)
        {
            RequireCustomsData(data);
            var xml = await PostCustomsAsync(GetCustomsUrl(CustomsRedeclarePath),
                data.ToXml(key), timeOut).ConfigureAwait(false);
            return new CustomsRedeclareResult(xml);
        }

        private static void RequireCustomsData(CustomsRequestDataBase data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
        }

        private static string GetCustomsUrl(string path)
        {
            return Senparc.Weixin.Config.TenPayV3Host.TrimEnd('/') + path;
        }

        private static string PostCustoms(string url, string xml, int timeOut)
        {
            var bytes = Encoding.UTF8.GetBytes(xml);
            using (var stream = new MemoryStream(bytes))
            {
                return RequestUtility.HttpPost(CommonDI.CommonSP, url, null,
                    stream, timeOut: timeOut);
            }
        }

        private static async Task<string> PostCustomsAsync(string url,
            string xml, int timeOut)
        {
            var bytes = Encoding.UTF8.GetBytes(xml);
            using (var stream = new MemoryStream(bytes))
            {
                return await RequestUtility.HttpPostAsync(CommonDI.CommonSP,
                    url, null, stream, timeOut: timeOut).ConfigureAwait(false);
            }
        }
    }
}
