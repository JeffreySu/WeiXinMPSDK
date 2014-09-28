using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP
{
   
    /// <summary>
    /// 菜单按钮类型
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// 点击
        /// </summary>
        click,
        /// <summary>
        /// Url
        /// </summary>
        view,
        /// <summary>
        /// 扫码推事件
        /// </summary>
        scancode_push,
        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        scancode_waitmsg,
        /// <summary>
        /// 弹出系统拍照发图
        /// </summary>
        pic_sysphoto,
        /// <summary>
        /// 弹出拍照或者相册发图
        /// </summary>
        pic_photo_or_album,
        /// <summary>
        /// 弹出微信相册发图器
        /// </summary>
        pic_weixin,
        /// <summary>
        /// 弹出地理位置选择器
        /// </summary>
        location_select
    }

    /// <summary>
    /// 上传媒体文件类型
    /// </summary>
    public enum UploadMediaFileType
    {
        /// <summary>
        /// 图片: 128K，支持JPG格式
        /// </summary>
        image,
        /// <summary>
        /// 语音：256K，播放长度不超过60s，支持AMR\MP3格式
        /// </summary>
        voice,
        /// <summary>
        /// 视频：1MB，支持MP4格式
        /// </summary>
        video,
        /// <summary>
        /// thumb：64KB，支持JPG格式
        /// </summary>
        thumb,
        /// <summary>
        /// 图文消息
        /// </summary>
        news
    }


    ///// <summary>
    ///// 群发消息返回状态
    ///// </summary>
    //public enum GroupMessageStatus
    //{
    //    //高级群发消息的状态
    //    涉嫌广告 = 10001,
    //    涉嫌政治 = 20001,
    //    涉嫌社会 = 20004,
    //    涉嫌色情 = 20002,
    //    涉嫌违法犯罪 = 20006,
    //    涉嫌欺诈 = 20008,
    //    涉嫌版权 = 20013,
    //    涉嫌互推 = 22000,
    //    涉嫌其他 = 21000
    //}
    public enum TenPayV3Type
    {
        JSAPI,
        NATIVE,
        APP
    }
}
