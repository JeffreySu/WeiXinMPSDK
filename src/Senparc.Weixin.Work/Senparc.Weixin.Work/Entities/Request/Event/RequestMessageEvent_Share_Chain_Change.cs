/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_Share_Chain_Change.cs
    文件功能描述：上下游共享应用事件回调 事件


    创建标识：lijian3828940 - 20261104
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 上下游共享应用事件回调 <see href="https://developer.work.weixin.qq.com/document/path/90240#%E4%B8%8A%E4%B8%8B%E6%B8%B8%E5%85%B1%E4%BA%AB%E5%BA%94%E7%94%A8%E4%BA%8B%E4%BB%B6%E5%9B%9E%E8%B0%83">官方文档</see>
    /// <para>本事件触发时机为：</para>
    /// <list type="number">
    /// <item>
    ///     <description>上游企业把自建应用共享给下游企业使用</description>
    ///   </item>
    ///   <item>
    ///     <description>上游企业把下游企业从共享应用中移除</description>
    ///   </item>   
    /// </list>
    /// </summary>
    public class RequestMessageEvent_Share_Chain_Change : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.SHARE_CHAIN_CHANGE; }
        }
    }
}

