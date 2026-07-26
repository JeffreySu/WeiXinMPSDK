/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_Security.cs
    文件功能描述：RequestMessageEvent_Security 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 安全管理事件。当前官方用于 change_domain_ip 域名 IP 变更通知。
    /// </summary>
    public class RequestMessageEvent_Security : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event => Event.security;

        /// <summary>安全事件类型，例如 change_domain_ip。</summary>
        public string ChangeType { get; set; }
    }
}
