/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
  
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

using System.ComponentModel;

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

        /// <summary>
        /// 微小店订单付款通知
        /// </summary>
        merchant_order,

        /// <summary>
        /// 接收会员信息事件通知
        /// </summary>
        submit_membercard_user_info,

        /// <summary>
        /// 摇一摇事件通知
        /// </summary>
        ShakearoundUserShake,
    }


    /// <summary>
    /// 发送消息类型
    /// </summary>
    public enum ResponseMsgType
    {
        [Description("文本")]
        Text = 0,
        [Description("单图文")]
        News = 1,
        [Description("音乐")]
        Music = 2,
        [Description("图片")]
        Image = 3,
        [Description("语音")]
        Voice = 4,
        [Description("视频")]
        Video = 5,
        [Description("多客服")]
        Transfer_Customer_Service,
        //transfer_customer_service

        //以下为延伸类型，微信官方并未提供具体的回复类型
        [Description("多图文")]
        MultipleNews = 106,
        [Description("位置")]
        LocationMessage = 107,//
        [Description("无回复")]
        NoResponse = 110,
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
        video = 4,
        /// <summary>
        /// 卡券
        /// </summary>
        wxcard = 5
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
        /// <summary>
        /// 会议门票
        /// </summary>
        MEETING_TICKET=10,
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

    public enum MemberCard_CustomField_NameType
    {
        /// <summary>
        /// 等级
        /// </summary>
        FIELD_NAME_TYPE_LEVEL = 0,
        /// <summary>
        /// 优惠券
        /// </summary>
        FIELD_NAME_TYPE_COUPON = 1,
        /// <summary>
        /// 印花
        /// </summary>
        FIELD_NAME_TYPE_STAMP = 2,
        /// <summary>
        /// 折扣
        /// </summary>
        FIELD_NAME_TYPE_DISCOUNT = 3,
        /// <summary>
        /// 成就
        /// </summary>
        FIELD_NAME_TYPE_ACHIEVEMEN = 4,
        /// <summary>
        /// 里程
        /// </summary>
        FIELD_NAME_TYPE_MILEAGE = 5,
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

    /// <summary>
    /// 卡券使用时间的类型
    /// </summary>
    public enum Card_DateInfo_Type
    {
        /// <summary>
        /// 固定日期区间
        /// </summary>
        DATE_TYPE_FIX_TIME_RANGE = 0,
        /// <summary>
        /// 固定时长（自领取后按天算）
        /// </summary>
        DATE_TYPE_FIX_TERM = 1,
        /// <summary>
        /// 永久有效
        /// </summary>
        DATE_TYPE_PERMANENT = 2
    }

    /// <summary>
    /// 自动回复规则类型
    /// </summary>
    public enum AutoReplyType
    {
        /// <summary>
        /// 文本
        /// </summary>
        text = 0,
        /// <summary>
        /// 图片
        /// </summary>
        img = 1,
        /// <summary>
        /// 语音
        /// </summary>
        voice = 2,
        /// <summary>
        /// 视频
        /// </summary>
        video = 3,
        /// <summary>
        /// 图文消息
        /// </summary>
        news = 4,
    }

    /// <summary>
    /// 自动回复模式
    /// </summary>
    public enum AutoReplyMode
    {
        /// <summary>
        /// 全部回复
        /// </summary>
        reply_all = 0,
        /// <summary>
        /// 随机回复其中一条
        /// </summary>
        random_one = 1,
    }

    /// <summary>
    /// 自动回复匹配模式
    /// </summary>
    public enum AutoReplyMatchMode
    {
        /// <summary>
        /// 消息中含有该关键词即可
        /// </summary>
        contain = 0,
        /// <summary>
        /// 消息内容必须和关键词严格相同
        /// </summary>
        equal = 1,
    }

    /// <summary>
    /// 卡券创建货架 投放页面的场景值
    /// </summary>
    public enum CardShelfCreate_Scene
    {
        /// <summary>
        /// 附近
        /// </summary>
        SCENE_NEAR_BY = 0,
        /// <summary>
        /// 自定义菜单
        /// </summary>
        SCENE_MENU = 1,
        /// <summary>
        /// 二维码
        /// </summary>
        SCENE_QRCODE = 2,
        /// <summary>
        /// 公众号文章
        /// </summary>
        SCENE_ARTICLE = 3,
        /// <summary>
        /// h5页面
        /// </summary>
        SCENE_H5 = 4,
        /// <summary>
        /// 自动回复
        /// </summary>
        SCENE_IVR = 5,
        /// <summary>
        /// 卡券自定义cell
        /// </summary>
        SCENE_CARD_CUSTOM_CELL = 6
    }

    /// <summary>
    /// 当前用户的会员卡状态
    /// </summary>
    public enum UserCardStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        NORMAL,


        /// <summary>
        /// 已过期
        /// </summary>
        EXPIRE,
        /// <summary>
        /// 转赠中
        /// </summary>
        GIFTING,
        /// <summary>
        /// 转赠成功
        /// </summary>
        GIFT_SUCC,
        /// <summary>
        /// 转赠超时
        /// </summary>
        GIFT_TIMEOUT,
        /// <summary>
        /// 已删除
        /// </summary>
        DELETE,
        /// <summary>
        /// 已失效
        /// </summary>
        UNAVAILABLE
    }
}
