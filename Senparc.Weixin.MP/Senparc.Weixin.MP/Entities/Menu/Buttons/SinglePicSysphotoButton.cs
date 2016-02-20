/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SinglePicSysphotoButton.cs
    文件功能描述：调起系统相机按钮
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// 单个按键
    /// </summary>
    public class SinglePicSysphotoButton : SingleButton
    {
        /// <summary>
        /// 类型为pic_sysphoto时必须。
        /// 用户点击按钮后，微信客户端将调起系统相机，完成拍照操作后，会将拍摄的相片发送给开发者，并推送事件给开发者，同时收起系统相机，随后可能会收到开发者下发的消息。
        /// 仅支持微信iPhone5.4.1以上版本，和Android5.4以上版本的微信用户，旧版本微信用户点击后将没有回应，开发者也不能正常接收到事件推送。
        /// </summary>
        public string key { get; set; }

        public SinglePicSysphotoButton()
            : base(ButtonType.pic_sysphoto.ToString())
        {
        }
    }
}
