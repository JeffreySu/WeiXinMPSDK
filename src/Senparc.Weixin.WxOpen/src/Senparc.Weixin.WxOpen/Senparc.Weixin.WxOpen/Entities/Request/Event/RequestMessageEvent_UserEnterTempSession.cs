/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：RequestMessageEvent_Scan.cs
    文件功能描述：事件之二维码扫描（关注微信）
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.Weixin.WxOpen;
using Senparc.Weixin.WxOpen.Entities;

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 事件之二维码扫描（关注微信）
    /// </summary>
    public class RequestMessageEvent_UserEnterTempSession : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.user_enter_tempsession; }
        }

        /// <summary>
        /// 开发者在客服会话按钮设置的sessionFrom参数
        /// </summary>
        public string SessionFrom { get; set; }

    }
}
