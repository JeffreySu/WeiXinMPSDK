using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// 单个按键
    /// </summary>
    public class SinglePicPhotoOrAlbumButton : SingleButton
    {
        /// <summary>
        /// 类型为pic_photo_or_album时必须。
        /// 用户点击按钮后，微信客户端将弹出选择器供用户选择“拍照”或者“从手机相册选择”。用户选择后即走其他两种流程。
        /// </summary>
        public string key { get; set; }

        public SinglePicPhotoOrAlbumButton()
            : base(ButtonType.pic_photo_or_album.ToString())
        {
        }
    }
}
