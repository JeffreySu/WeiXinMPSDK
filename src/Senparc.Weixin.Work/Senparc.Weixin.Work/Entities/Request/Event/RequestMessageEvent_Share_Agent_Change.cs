/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_Share_Agent_Change.cs
    文件功能描述：企业互联共享应用事件回调 事件


    创建标识：lijian3828940 - 20261104
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 企业互联共享应用事件回调 <see href="https://developer.work.weixin.qq.com/document/path/90240#%E4%BC%81%E4%B8%9A%E4%BA%92%E8%81%94%E5%85%B1%E4%BA%AB%E5%BA%94%E7%94%A8%E4%BA%8B%E4%BB%B6%E5%9B%9E%E8%B0%83">官方文档</see>
    /// </summary>
    public class RequestMessageEvent_Share_Agent_Change : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.SHARE_AGENT_CHANGE; }
        }
    }
}

