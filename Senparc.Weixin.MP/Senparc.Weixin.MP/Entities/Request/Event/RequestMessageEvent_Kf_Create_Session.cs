/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageEvent_Kf_Create_Session.cs
    文件功能描述：事件之多客服接入会话(kf_create_session)
    
    
    创建标识：Senparc - 20150309
    
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之多客服接入会话(kf_create_session)
    /// </summary>
    public class RequestMessageEvent_Kf_Create_Session : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.kf_create_session; }
        }

        /// <summary>
        /// 完整客服账号，格式为：账号前缀@公众号微信号
        /// </summary>
        public string KfAccount { get; set; }
    }
}
