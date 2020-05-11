/*----------------------------------------------------------------
    Copyright (C) 2020 Senparc
    
    文件名：RequestMessageEvent_Location.cs
    文件功能描述：事件之上报地理位置事件
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 客户群变更事件
    /// </summary>
    public class RequestMessageEvent_Change_External_Chat : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.CHANGE_EXTERNAL_CHAT; }
        }

        /// <summary>
        /// 群ID
        /// </summary>
        public string ChatId  { get; set; }
    }
}
