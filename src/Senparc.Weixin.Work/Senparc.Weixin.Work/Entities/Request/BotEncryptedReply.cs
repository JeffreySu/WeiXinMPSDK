/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BotEncryptedReply.cs
    文件功能描述：BotEncryptedReply 相关功能


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Tencent
{
    /// <summary>智能机器人加密 JSON 回复。</summary>
    public class BotEncryptedReply
    {
        /// <summary>加密后的回复消息。</summary>
        public string encrypt { get; set; }

        /// <summary>消息签名。</summary>
        public string msgsignature { get; set; }

        /// <summary>生成签名使用的时间戳。</summary>
        public long timestamp { get; set; }

        /// <summary>生成签名使用的随机字符串。</summary>
        public string nonce { get; set; }
    }
}
