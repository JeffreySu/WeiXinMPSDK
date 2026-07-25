#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：VehicleParkingApis.P1.cs
    文件功能描述：VehicleParkingApis.P1 相关功能


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.TenPayV3.Apis
{
    /// <summary>
    /// 微信支付分停车服务补充接口。
    /// </summary>
    public partial class VehicleParkingApis
    {
        /// <summary>微信垫资还款小程序 AppId。</summary>
        public const string RepaymentMiniProgramAppId = "wx5e73c65404eee268";

        /// <summary>微信垫资还款小程序原始 ID。</summary>
        public const string RepaymentMiniProgramUserName = "gh_5e259b7a73b1";

        /// <summary>微信垫资还款小程序页面路径。</summary>
        public const string RepaymentMiniProgramPath = "pages/invest_list/invest_list";

        /// <summary>
        /// 生成微信垫资还款小程序 extraData。
        /// </summary>
        /// <param name="appId">小程序 AppId。</param>
        /// <param name="mchId">微信支付商户号。</param>
        /// <param name="openId">用户 OpenId。</param>
        /// <param name="nonceStr">通知解密随机字符串。</param>
        /// <returns>微信接口返回结果。</returns>
        public static VehicleParkingRepaymentData CreateRepaymentData(string appId, string mchId,
            string openId, string nonceStr = null)
        {
            return new VehicleParkingRepaymentData
            {
                appid = appId,
                mchid = mchId,
                openid = openId,
                nonce_str = nonceStr ?? Guid.NewGuid().ToString("N")
            };
        }

        /// <summary>
        /// 生成 App 拉起微信垫资还款小程序时使用的带参路径。
        /// </summary>
        /// <param name="data">接口业务数据。</param>
        /// <returns>微信接口返回结果。</returns>
        public static string CreateRepaymentPath(VehicleParkingRepaymentData data)
        {
            _ = data ?? throw new ArgumentNullException(nameof(data));
            return $"{RepaymentMiniProgramPath}?mchid={Uri.EscapeDataString(data.mchid ?? "")}" +
                   $"&appid={Uri.EscapeDataString(data.appid ?? "")}" +
                   $"&nonce_str={Uri.EscapeDataString(data.nonce_str ?? "")}" +
                   $"&openid={Uri.EscapeDataString(data.openid ?? "")}";
        }
    }

    /// <summary>
    /// VehicleParkingRepayment 数据。
    /// </summary>
    public class VehicleParkingRepaymentData
    {
        /// <summary>商户对应的 AppId。</summary>
        public string appid { get; set; }

        /// <summary>微信支付商户号。</summary>
        public string mchid { get; set; }

        /// <summary>通知解密随机字符串。</summary>
        public string nonce_str { get; set; }

        /// <summary>用户 OpenId。</summary>
        public string openid { get; set; }
    }
}
