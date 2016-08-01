/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SinglePicPhotoOrAlbumButton.cs
    文件功能描述：微信客户端将弹出选择器供用户选择“拍照”或者“从手机相册选择”按钮
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities.Menu
{
    /// <summary>
    /// 单个按键
    /// </summary>
    public class SinglePicPhotoOrAlbumButton : SingleButton
    {
        /// <summary>
        /// 类型为pic_photo_or_album时必须。
        /// 用户点击按钮后，微信客户端将弹出选择器供用户选择“拍照”或者“从手机相册选择”。用户选择后即走其他两种流程。
        /// 仅支持微信iPhone5.4.1以上版本，和Android5.4以上版本的微信用户，旧版本微信用户点击后将没有回应，开发者也不能正常接收到事件推送。
        /// </summary>
        public string key { get; set; }

        public SinglePicPhotoOrAlbumButton()
            : base(ButtonType.pic_photo_or_album.ToString())
        {
        }
    }
}
