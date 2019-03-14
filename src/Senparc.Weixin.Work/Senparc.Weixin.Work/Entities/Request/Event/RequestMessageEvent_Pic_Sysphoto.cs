/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageEvent_PicSysphoto.cs
    文件功能描述：事件之弹出系统拍照发图(pic_sysphoto)
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 事件之弹出系统拍照发图(pic_sysphoto)
    /// </summary>
    public class RequestMessageEvent_Pic_Sysphoto : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.PIC_SYSPHOTO; }
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 发送的图片信息
        /// </summary>
        public SendPicsInfo SendPicsInfo { get; set; }
    }
}
