#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：AbnormalRefundRequestData.cs
    文件功能描述：AbnormalRefundRequestData 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    /// <summary>
    /// 发起异常退款请求。bank_account、real_name 应先使用微信支付公钥或平台证书公钥加密。
    /// </summary>
    public class AbnormalRefundRequestData
    {
        /// <summary>服务商模式下的子商户号。</summary>
        public string sub_mchid { get; set; }
        /// <summary>商户退款单号。</summary>
        public string out_refund_no { get; set; }
        /// <summary>异常退款类型。</summary>
        public string type { get; set; }
        /// <summary>收款银行类型。</summary>
        public string bank_type { get; set; }
        /// <summary>使用微信支付公钥或平台证书公钥加密后的银行卡号。</summary>
        public string bank_account { get; set; }
        /// <summary>使用微信支付公钥或平台证书公钥加密后的开户人姓名。</summary>
        public string real_name { get; set; }
    }
}
