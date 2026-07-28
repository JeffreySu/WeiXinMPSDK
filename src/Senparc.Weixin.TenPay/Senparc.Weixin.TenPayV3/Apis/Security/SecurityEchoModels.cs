#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SecurityEchoModels.cs
    文件功能描述：微信支付安全探测接口请求、返回及通知模型


    创建标识：Senparc - 20260728

    修改标识：Senparc - 20260728
    修改描述：v2.6.0 新增安全探测接口和成功通知模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Attributes;

namespace Senparc.Weixin.TenPayV3.Apis.Security
{
    /// <summary>
    /// 微信支付安全探测请求。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4014551946</para>
    /// </summary>
    public class SecurityEchoRequestData
    {
        /// <summary>安全探测结果通知地址，必须为公网可访问的 HTTPS URL。</summary>
        public string notify_url { get; set; }

        /// <summary>由商户自定义、供响应和通知校验的明文消息。</summary>
        public string echo_message { get; set; }

        /// <summary>
        /// 需要加密探测的消息明文；SDK 在发送请求前自动加密。
        /// <para>序列化后的字段名仍为官方要求的 encrypted_echo_message。</para>
        /// </summary>
        [FieldEncrypt]
        public string encrypted_echo_message { get; set; }
    }

    /// <summary>微信支付安全探测响应。</summary>
    public class SecurityEchoReturnJson : ReturnJsonBase
    {
        /// <summary>微信支付原样返回的明文消息。</summary>
        public string echo_message { get; set; }

        /// <summary>使用商户证书加密后返回的探测消息。</summary>
        public string encrypted_echo_message { get; set; }
    }

    /// <summary>安全探测通知事件常量。</summary>
    public static class SecurityEchoNotifyEventTypes
    {
        /// <summary>安全探测成功。</summary>
        public const string Success = "SECURITY_ECHO.SUCCESS";

        /// <summary>安全探测通知资源的 original_type。</summary>
        public const string OriginalType = "security";
    }

    /// <summary>
    /// 安全探测成功通知的解密资源。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4015164042</para>
    /// </summary>
    public class SecurityEchoNotifyJson : ReturnJsonBase
    {
        /// <summary>微信支付原样返回的明文消息。</summary>
        public string echo_message { get; set; }

        /// <summary>
        /// 加密探测消息；属性名遵循官方通知字段 encrypt_echo_message。
        /// </summary>
        public string encrypt_echo_message { get; set; }
    }
}
