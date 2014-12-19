using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP
{
    /// <summary>
    /// 接收消息类型
    /// </summary>
    public enum RequestMsgType
    {
        Text, //文本
        Location, //地理位置
        Image, //图片
        Voice, //语音
        Video, //视频
        Link, //连接信息
        Event, //事件推送
    }


    /// <summary>
    /// 当RequestMsgType类型为Event时，Event属性的类型
    /// </summary>
    public enum Event
    {
        /// <summary>
        /// 进入会话（似乎已从官方API中移除）
        /// </summary>
        ENTER,

        /// <summary>
        /// 地理位置（似乎已从官方API中移除）
        /// </summary>
        LOCATION,

        /// <summary>
        /// 订阅
        /// </summary>
        subscribe,

        /// <summary>
        /// 取消订阅
        /// </summary>
        unsubscribe,

        /// <summary>
        /// 自定义菜单点击事件
        /// </summary>
        CLICK,

        /// <summary>
        /// 二维码扫描
        /// </summary>
        scan,

        /// <summary>
        /// URL跳转
        /// </summary>
        VIEW,

        /// <summary>
        /// 事件推送群发结果
        /// </summary>
        MASSSENDJOBFINISH,

        /// <summary>
        /// 模板信息发送完成
        /// </summary>
        TEMPLATESENDJOBFINISH,

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
        location_select,

        /// <summary>
        /// 卡券通过审核
        /// </summary>
        card_pass_check,

        /// <summary>
        /// 卡券未通过审核
        /// </summary>
        card_not_pass_check,

        /// <summary>
        /// 领取卡券
        /// </summary>
        user_get_card,

        /// <summary>
        /// 删除卡券
        /// </summary>
        user_del_card,

    }


    /// <summary>
    /// 发送消息类型
    /// </summary>
    public enum ResponseMsgType
    {
        Text,
        News,
        Music,
        Image,
        Voice,
        Video,
        Transfer_Customer_Service,
        //transfer_customer_service
    }

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

    public enum GroupMessageType
    {
        /// <summary>
        /// 图文消息
        /// </summary>
        mpnews = 0,
        /// <summary>
        /// 文本
        /// </summary>
        text = 1,
        /// <summary>
        /// 语音
        /// </summary>
        voice = 2,
        /// <summary>
        /// 图片
        /// </summary>
        image = 3,
        /// <summary>
        /// 视频
        /// </summary>
        video = 4
    }
    /// <summary>
    /// 卡券类型
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// 通用券
        /// </summary>
        GENERAL_COUPON = 0,
        /// <summary>
        /// 团购券
        /// </summary>
        GROUPON = 1,
        /// <summary>
        /// 折扣券
        /// </summary>
        DISCOUNT = 2,
        /// <summary>
        /// 礼品券
        /// </summary>
        GIFT = 3,
        /// <summary>
        /// 代金券
        /// </summary>
        CASH = 4,
        /// <summary>
        /// 会员卡
        /// </summary>
        MEMBER_CARD = 5,
        /// <summary>
        /// 门票
        /// </summary>
        SCENIC_TICKET = 6,
        /// <summary>
        /// 电影票
        /// </summary>
        MOVIE_TICKET = 7,
        /// <summary>
        /// 飞机票
        /// </summary>
        BOARDING_PASS = 8,
        /// <summary>
        /// 红包
        /// </summary>
        LUCKY_MONEY = 9,
    }
    /// <summary>
    /// 卡券code码展示类型
    /// </summary>
    public enum Card_CodeType
    {
        /// <summary>
        /// 文本
        /// </summary>
        CODE_TYPE_TEXT = 0,
        /// <summary>
        /// 一维码
        /// </summary>
        CODE_TYPE_BARCODE = 1,
        /// <summary>
        /// 二维码
        /// </summary>
        CODE_TYPE_QRCODE = 2,
    }
    /// <summary>
    /// 卡券 商户自定义cell 名称
    /// </summary>
    public enum Card_UrlNameType
    {
        /// <summary>
        /// 外卖
        /// </summary>
        URL_NAME_TYPE_TAKE_AWAY = 0,
        /// <summary>
        /// 在线预订
        /// </summary>
        URL_NAME_TYPE_RESERVATION = 1,
        /// <summary>
        /// 立即使用
        /// </summary>
        URL_NAME_TYPE_USE_IMMEDIATELY = 2,
        /// <summary>
        /// 在线预约
        /// </summary>
        URL_NAME_TYPE_APPOINTMENT = 3,
        /// <summary>
        /// 在线兑换
        /// </summary>
        URL_NAME_TYPE_EXCHANGE = 4,
        /// <summary>
        /// 车辆信息
        /// </summary>
        URL_NAME_TYPE_VEHICLE_INFORMATION = 5,
    }
}
