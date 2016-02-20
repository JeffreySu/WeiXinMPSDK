/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageEvent_Scan.cs
    文件功能描述：事件之二维码扫描（关注微信）
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之二维码扫描（关注微信）
    /// </summary>
    public class RequestMessageEvent_Scan : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.scan; }
        }

        public string Ticket { get; set; }

        /// <summary>
        /// 事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id
        /// </summary>
        public string EventKey { get; set; }
    }
}
