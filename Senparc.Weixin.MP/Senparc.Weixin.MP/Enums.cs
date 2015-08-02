/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
  
    文件名：Enums.cs
    文件功能描述：枚举类型
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150306
    修改描述：添加多客服事件

    修改标识：Senparc - 20150313
    修改描述：添加语言类型

    修改标识：Senparc - 20150323
    修改描述：卡券新增会议门票类型

    修改标识：Senparc - 20150327
    修改描述：接收消息类型添加小视频类型

    修改标识：马鑫 - 20150331
    修改描述：修改返回错误码

    修改标识：Senparc - 20150331
    修改描述：应用授权作用域移至此处

    修改标识：Senparc - 20150512
    修改描述：添加摇一摇周边【关联操作标志位】、【新增操作标志位】枚举类型
----------------------------------------------------------------*/

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
        ShortVideo,//小视频
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

        /// <summary>
        /// 多客服接入会话
        /// </summary>
        kf_create_session,

        /// <summary>
        /// 多客服关闭会话
        /// </summary>
        kf_close_session,

        /// <summary>
        /// 多客服转接会话
        /// </summary>
        kf_switch_session,

        /// <summary>
        /// 审核结果事件推送
        /// </summary>
        poi_check_notify,

        /// <summary>
        /// Wi-Fi连网成功
        /// </summary>
        WifiConnected,

        /// <summary>
        /// 卡券核销
        /// </summary>
        user_consume_card,

        /// <summary>
        /// 进入会员卡
        /// </summary>
        user_view_card,

        /// <summary>
        /// 从卡券进入公众号会话
        /// </summary>
        user_enter_session_from_card,
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
        WXCard,
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
        mpnews,
        /// <summary>
        /// 文本
        /// </summary>
        text,
        /// <summary>
        /// 语音
        /// </summary>
        voice,
        /// <summary>
        /// 图片
        /// </summary>
        image,
        /// <summary>
        /// 视频
        /// </summary>
        video,
        /// <summary>
        /// 卡券
        /// </summary>
        wxcard
    }
    /// <summary>
    /// 卡券类型
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// 通用券
        /// </summary>
        GENERAL_COUPON ,
        /// <summary>
        /// 团购券
        /// </summary>
        GROUPON ,
        /// <summary>
        /// 折扣券
        /// </summary>
        DISCOUNT ,
        /// <summary>
        /// 礼品券
        /// </summary>
        GIFT ,
        /// <summary>
        /// 代金券
        /// </summary>
        CASH ,
        /// <summary>
        /// 会员卡
        /// </summary>
        MEMBER_CARD,
        /// <summary>
        /// 门票
        /// </summary>
        SCENIC_TICKET,
        /// <summary>
        /// 电影票
        /// </summary>
        MOVIE_TICKET,
        /// <summary>
        /// 飞机票
        /// </summary>
        BOARDING_PASS,
        /// <summary>
        /// 红包
        /// </summary>
        LUCKY_MONEY,
        /// <summary>
        /// 会议门票
        /// </summary>
        MEETING_TICKET,
    }
    /// <summary>
    /// 卡券code码展示类型
    /// </summary>
    public enum Card_CodeType
    {
        /// <summary>
        /// 文本
        /// </summary>
        CODE_TYPE_TEXT,
        /// <summary>
        /// 一维码
        /// </summary>
        CODE_TYPE_BARCODE,
        /// <summary>
        /// 二维码
        /// </summary>
        CODE_TYPE_QRCODE,

        /// <summary>
        /// 二维码无code显
        /// </summary>
        CODE_TYPE_ONLY_QRCODE,

        /// <summary>
        /// 一维码无code显示
        /// </summary>
        CODE_TYPE_ONLY_BARCODE,
    }
    /// <summary>
    /// 卡券 商户自定义cell 名称
    /// </summary>
    public enum Card_UrlNameType
    {
        /// <summary>
        /// 外卖
        /// </summary>
        URL_NAME_TYPE_TAKE_AWAY ,
        /// <summary>
        /// 在线预订
        /// </summary>
        URL_NAME_TYPE_RESERVATION ,
        /// <summary>
        /// 立即使用
        /// </summary>
        URL_NAME_TYPE_USE_IMMEDIATELY,
        /// <summary>
        /// 在线预约
        /// </summary>
        URL_NAME_TYPE_APPOINTMENT,
        /// <summary>
        /// 在线兑换
        /// </summary>
        URL_NAME_TYPE_EXCHANGE,
        /// <summary>
        /// 车辆信息
        /// </summary>
        URL_NAME_TYPE_VEHICLE_INFORMATION,
    }

    public enum MemberCard_CustomField_NameType
    {
        /// <summary>
        /// 等级
        /// </summary>
        FIELD_NAME_TYPE_LEVEL,
        /// <summary>
        /// 优惠券
        /// </summary>
        FIELD_NAME_TYPE_COUPON,
        /// <summary>
        /// 印花
        /// </summary>
        FIELD_NAME_TYPE_STAMP,
        /// <summary>
        /// 折扣
        /// </summary>
        FIELD_NAME_TYPE_DISCOUNT ,
        /// <summary>
        /// 成就
        /// </summary>
        FIELD_NAME_TYPE_ACHIEVEMEN,
        /// <summary>
        /// 里程
        /// </summary>
        FIELD_NAME_TYPE_MILEAGE,
    }

    /// <summary>
    /// 应用授权作用域
    /// </summary>
    public enum OAuthScope
    {
        /// <summary>
        /// 不弹出授权页面，直接跳转，只能获取用户openid
        /// </summary>
        snsapi_base,
        /// <summary>
        /// 弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息
        /// </summary>
        snsapi_userinfo
    }

    /// <summary>
    /// 关联操作标志位， 0为解除关联关系，1为建立关联关系
    /// </summary>
    public enum ShakeAroundBindType
    {
        解除关联关系 = 0,
        建立关联关系 = 1
    }

    /// <summary>
    /// 新增操作标志位， 0为覆盖，1为新增
    /// </summary>
    public enum ShakeAroundAppendType
    {
        覆盖 = 0,
        新增 = 1
    }
}
