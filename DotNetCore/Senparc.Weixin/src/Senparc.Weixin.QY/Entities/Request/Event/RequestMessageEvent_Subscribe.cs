/*----------------------------------------------------------------
    Copyright (C) 2015 LSW
    
    文件名：RequestMessageEvent_Subscribe.cs
    文件功能描述：事件之关注事件的推送（subscribe）
    
    
    创建标识：LSW - 20150313
    
    修改标识：LSW - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 事件之关注事件的推送（subscribe）
    /// </summary>
    public class RequestMessageEvent_Subscribe : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.subscribe; }
        }
    }
}
