/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：Enums.cs
    文件功能描述：枚举类型


    创建标识：Senparc - 20150313

    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20150507
    修改描述：添加 事件 异步任务完成事件推送 枚举类型

    修改标识：zeje - 20150507
    修改描述：v3.3.5 添加Login_User_Type枚举
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY
{
    /// <summary>
    /// 接收消息类型
    /// </summary>
    public enum RequestMsgType
    {
        DEFAULT,//默认
        Text, //文本
        Location, //地理位置
        Image, //图片
        Voice, //语音
        Video, //视频
        Link, //连接信息
        Event, //事件推送
        ShortVideo, //小视频
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
        /// 地理位置
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
        SCANCODE_PUSH,

        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        SCANCODE_WAITMSG,

        /// <summary>
        /// 弹出系统拍照发图
        /// </summary>
        PIC_SYSPHOTO,

        /// <summary>
        /// 弹出拍照或者相册发图
        /// </summary>
        PIC_PHOTO_OR_ALBUM,

        /// <summary>
        /// 弹出微信相册发图器
        /// </summary>
        PIC_WEIXIN,

        /// <summary>
        /// 弹出地理位置选择器
        /// </summary>
        LOCATION_SELECT,

        /// <summary>
        /// 用户进入应用的事件推送
        /// </summary>
        ENTER_AGENT,

        /// <summary>
        /// 异步任务完成事件推送
        /// </summary>
        BATCH_JOB_RESULT,
    }

    public enum ThirdPartyInfo
    {
        /// <summary>
        /// 推送suite_ticket协议
        /// </summary>
        SUITE_TICKET,

        /// <summary>
        /// 变更授权的通知
        /// </summary>
        CHANGE_AUTH,

        /// <summary>
        /// 取消授权的通知
        /// </summary>
        CANCEL_AUTH,
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
        MpNews,

        //以下类型为Senparc.Weixin自用类型
        NoResponse
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
        /// 图片: 1MB，支持JPG格式
        /// </summary>
        image,
        /// <summary>
        /// 语音：2MB，播放长度不超过60s，支持AMR格式
        /// </summary>
        voice,
        /// <summary>
        /// 视频：10MB，支持MP4格式
        /// </summary>
        video,
        /// <summary>
        /// 普通文件：10MB
        /// </summary>
        file
    }

    ///// <summary>
    ///// 返回码（JSON）
    ///// </summary>
    //public enum ReturnCode
    //{
    //    系统繁忙 = -1,
    //    请求成功 = 0,
    //    验证失败 = 40001,
    //    不合法的凭证类型 = 40002,
    //    不合法的OpenID = 40003,
    //    不合法的媒体文件类型 = 40004,
    //    不合法的文件类型 = 40005,
    //    不合法的文件大小 = 40006,
    //    不合法的媒体文件id = 40007,
    //    不合法的消息类型 = 40008,
    //    不合法的图片文件大小 = 40009,
    //    不合法的语音文件大小 = 40010,
    //    不合法的视频文件大小 = 40011,
    //    不合法的缩略图文件大小 = 40012,
    //    不合法的APPID = 40013,
    //    //不合法的access_token      =             40014,
    //    不合法的access_token = 40014,
    //    不合法的菜单类型 = 40015,
    //    //不合法的按钮个数             =          40016,
    //    //不合法的按钮个数              =         40017,
    //    不合法的按钮个数1 = 40016,
    //    不合法的按钮个数2 = 40017,
    //    不合法的按钮名字长度 = 40018,
    //    不合法的按钮KEY长度 = 40019,
    //    不合法的按钮URL长度 = 40020,
    //    不合法的菜单版本号 = 40021,
    //    不合法的子菜单级数 = 40022,
    //    不合法的子菜单按钮个数 = 40023,
    //    不合法的子菜单按钮类型 = 40024,
    //    不合法的子菜单按钮名字长度 = 40025,
    //    不合法的子菜单按钮KEY长度 = 40026,
    //    不合法的子菜单按钮URL长度 = 40027,
    //    不合法的自定义菜单使用用户 = 40028,
    //    不合法的oauth_code = 40029,
    //    不合法的refresh_token = 40030,
    //    缺少access_token参数 = 41001,
    //    缺少appid参数 = 41002,
    //    缺少refresh_token参数 = 41003,
    //    缺少secret参数 = 41004,
    //    缺少多媒体文件数据 = 41005,
    //    缺少media_id参数 = 41006,
    //    缺少子菜单数据 = 41007,
    //    access_token超时 = 42001,
    //    需要GET请求 = 43001,
    //    需要POST请求 = 43002,
    //    需要HTTPS请求 = 43003,
    //    多媒体文件为空 = 44001,
    //    POST的数据包为空 = 44002,
    //    图文消息内容为空 = 44003,
    //    多媒体文件大小超过限制 = 45001,
    //    消息内容超过限制 = 45002,
    //    标题字段超过限制 = 45003,
    //    描述字段超过限制 = 45004,
    //    链接字段超过限制 = 45005,
    //    图片链接字段超过限制 = 45006,
    //    语音播放时间超过限制 = 45007,
    //    图文消息超过限制 = 45008,
    //    接口调用超过限制 = 45009,
    //    创建菜单个数超过限制 = 45010,
    //    不存在媒体数据 = 46001,
    //    不存在的菜单版本 = 46002,
    //    不存在的菜单数据 = 46003,
    //    解析JSON_XML内容错误 = 47001,
    //    api功能未授权 = 48001,
    //    用户未授权该api = 50001,

    //    //新加入的一些类型，以下文字根据P2P项目格式组织，非官方文字
    //    发送消息失败_48小时内用户未互动 = 10706,
    //    发送消息失败_该用户已被加入黑名单_无法向此发送消息 = 62751,
    //    发送消息失败_对方关闭了接收消息 = 10703,
    //    对方不是粉丝 = 10700
    //}

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


    /// <summary>
    /// 语言
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// 中文简体
        /// </summary>
        zh_CN,
        /// <summary>
        /// 中文繁体
        /// </summary>
        zh_TW,
        /// <summary>
        /// 英文
        /// </summary>
        en
    }

    public enum SetAgent_IsReportUser
    {
        不接受 = 0,
        接收 = 1
    }

    ///// <summary>
    ///// 异步任务类型
    ///// </summary>
    //public enum Asynchronous_Type
    //{
    //    /// <summary>
    //    /// 增量更新成员
    //    /// </summary>
    //    sync_user = 0,
    //    /// <summary>
    //    /// 全量覆盖成员
    //    /// </summary>
    //    replace_user = 1,
    //    /// <summary>
    //    /// 邀请成员关注
    //    /// </summary>
    //    invite_user = 2,
    //    /// <summary>
    //    /// 全量覆盖部门
    //    /// </summary>
    //    replace_party = 3,
    //}

    /// <summary>
    /// 群聊类型
    /// </summary>
    public enum Chat_Type
    {
        single = 0,
        group = 1,
    }

    /// <summary>
    /// 群聊发送消息类型
    /// </summary>
    public enum ChatMsgType
    {
        text = 0,
        image = 1,
        file = 2
    }

    /// <summary>
    /// 免打扰状态
    /// </summary>
    public enum Mute_Status
    {
        关闭 = 0,
        打开 = 1
    }

    /// <summary>
    /// 客服消息用户类型
    /// </summary>
    public enum KF_User_Type
    {
        /// <summary>
        /// 客服
        /// </summary>
        kf,
        /// <summary>
        /// 客户，企业员工userid
        /// </summary>
        userid,
        /// <summary>
        /// 客户，企业员工openid
        /// </summary>
        openid
    }

    /// <summary>
    /// 客服类型
    /// </summary>
    public enum KF_Type
    {
        /// <summary>
        /// 内部客服
        /// </summary>
        @internal,
        /// <summary>
        /// 外部客服
        /// </summary>
        external
    }
    /// <summary>redirect_uri支持登录的类型
    /// </summary>
    public enum Login_User_Type
    {
        /// <summary>成员登录
        /// </summary>
        member,
        /// <summary>管理员登录
        /// </summary>
        admin,
        /// <summary>成员或管理员皆可登录
        /// </summary>
        all
    }
}
