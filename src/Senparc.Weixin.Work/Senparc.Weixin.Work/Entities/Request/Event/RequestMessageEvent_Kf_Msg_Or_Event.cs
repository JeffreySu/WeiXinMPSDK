/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc

    文件名：RequestMessageEvent_Kf_Msg_Or_Event.cs
    文件功能描述：微信客服消息与事件回调通知

    创建标识：Senparc - 20251203

    官方文档：https://developer.work.weixin.qq.com/document/path/94670
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 微信客服消息与事件回调通知
    /// </summary>
    public class RequestMessageEvent_Kf_Msg_Or_Event : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型：KF_MSG_OR_EVENT
        /// </summary>
        public override Event Event => Event.KF_MSG_OR_EVENT;

        /// <summary>
        /// 调用拉取消息接口时，需要传此token，用于校验请求的合法性
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 有新消息的客服账号。可通过sync_msg接口指定open_kfid获取对应客服账号的消息
        /// </summary>
        public string OpenKfId { get; set; }
    }
}
