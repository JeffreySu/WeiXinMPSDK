#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
  
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
 
    修改标识：Senparc - 20160803
    修改描述：将其卡券中的 code_type 定义完整
 
    修改标识：Senparc - 20160901
    修改描述：v14.3.7 增加QrCode_ActionName枚举

    修改标识：Senparc - 20161024
    修改描述：v14.3.1024 添加RedPack_Scene枚举

    修改标识：Senparc - 20161204
    修改描述：v14.3.1025 添加TenPayV3CodeState枚举[统一订单返回状态]

    修改标识：Senparc - 20170106
    修改描述：v14.3.117 ResponseMsgType添加SuccessResponse枚举

    修改标识：Senparc - 20170328
    修改描述：v14.3.139 ButtonType添加小程序类型

    修改标识：Senparc - 20170807
    修改描述：v14.5.7 添加TenPayV3Type.MWEB枚举，支持H5支付

    修改标识：Senparc - 20170826
    修改描述：v14.6.8 添加Event下“微信认证事件推送”一系列事件类型

    修改标识：Senparc - 20171108
    修改描述：v14.8.5 卡券MemberCard_CustomField_NameType枚举添加FIELD_NAME_TYPE_UNKNOW类型

    修改标识：Senparc - 20170225
    修改描述：v14.10.3 增加MessageHandler的file类型处理

    修改标识：Senparc - 20180826
    修改描述：v15.3.0 分离微信支付到 Senparc.Weixin.TenPay.dll，将 RedPack_Scene、TenPayV3Type 枚举迁移过去

----------------------------------------------------------------*/

using System;
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
        File,//文件类型
        Unknown = -1,//未知类型
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
        /// <summary>
        /// 卡券转赠事件推送
        /// </summary>
        user_gifting_card,
        /// <summary>
        /// 微信买单完成
        /// </summary>
        user_pay_from_pay_cell,
        /// <summary>
        /// 会员卡内容更新事件：会员卡积分余额发生变动时
        /// </summary>
        update_member_card,
        /// <summary>
        /// 卡券库存报警事件：当某个card_id的初始库存数大于200且当前库存小于等于100时
        /// </summary>
        card_sku_remind,
        /// <summary>
        /// 券点流水详情事件：当商户朋友的券券点发生变动时
        /// </summary>
        card_pay_order,


        #region 微信认证事件推送

        /// <summary>
        /// 资质认证成功（此时立即获得接口权限）
        /// </summary>
        qualification_verify_success,
        /// <summary>
        /// 名称认证成功（即命名成功）
        /// </summary>
        qualification_verify_fail,
        /// <summary>
        /// 名称认证成功（即命名成功）
        /// </summary>
        naming_verify_success,
        /// <summary>
        /// 名称认证失败（这时虽然客户端不打勾，但仍有接口权限）
        /// </summary>
        naming_verify_fail,
        /// <summary>
        /// 年审通知
        /// </summary>
        annual_renew,
        /// <summary>
        /// 认证过期失效通知
        /// </summary>
        verify_expired,

        #endregion

        #region 小程序审核事件推送

        /// <summary>
        /// 小程序审核成功
        /// </summary>
        weapp_audit_success,
        /// <summary>
        /// 小程序审核失败
        /// </summary>
        weapp_audit_fail

        #endregion
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
        [Description("success")]
        SuccessResponse = 200
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
        /// 小程序
        /// </summary>
        miniprogram,
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
        /// 下发消息（除文本消息）
        /// </summary>
        media_id,
        /// <summary>
        /// 跳转图文消息URL
        /// </summary>
        view_limited
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
        MEETING_TICKET = 10,
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
        ///
        /// 二维码无code显示
        ///
        CODE_TYPE_ONLY_QRCODE = 3,
        ///
        /// 一维码无code显示
        ///
        CODE_TYPE_ONLY_BARCODE = 4,
        ///
        /// 不显示code和条形码类型
        ///
        CODE_TYPE_NONE = 5
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

        /// <summary>
        /// 未知类型（新加入，文档中没有：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1451025272）
        /// </summary>
        FIELD_NAME_TYPE_UNKNOW = -1
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

    /// <summary>
    /// 行业代码
    /// </summary>
    public enum IndustryCode
    {
        IT科技_互联网_电子商务 = 1,
        IT科技_IT软件与服务 = 2,
        IT科技_IT硬件与设备 = 3,
        IT科技_电子技术 = 4,
        IT科技_通信与运营商 = 5,
        IT科技_网络游戏 = 6,
        金融业_银行 = 7,
        金融业_基金_理财_信托 = 8,
        金融业_保险 = 9,
        餐饮_餐饮 = 10,
        酒店旅游_酒店 = 11,
        酒店旅游_旅游 = 12,
        运输与仓储_快递 = 13,
        运输与仓储_物流 = 14,
        运输与仓储_仓储 = 15,
        教育_培训 = 16,
        教育_院校 = 17,
        政府与公共事业_学术科研 = 18,
        政府与公共事业_交警 = 19,
        政府与公共事业_博物馆 = 20,
        政府与公共事业_公共事业_非盈利机构 = 21,
        医药护理_医药医疗 = 22,
        医药护理_护理美容 = 23,
        医药护理_保健与卫生 = 24,
        交通工具_汽车相关 = 25,
        交通工具_摩托车相关 = 26,
        交通工具_火车相关 = 27,
        交通工具_飞机相关 = 28,
        房地产_建筑 = 29,
        房地产_物业 = 30,
        消费品_消费品 = 31,
        商业服务_法律 = 32,
        商业服务_会展 = 33,
        商业服务_中介服务 = 34,
        商业服务_认证 = 35,
        商业服务_审计 = 36,
        文体娱乐_传媒 = 37,
        文体娱乐_体育 = 38,
        文体娱乐_娱乐休闲 = 39,
        印刷_印刷 = 40,
        其它_其它 = 41,
        请求成功
    }

    /// <summary>
    /// 行业代号
    /// </summary>
    public enum IndustryId
    {

        购物_百货商场 = 0101, //不需要资质文件
        购物_超市 = 0102, //需要资质文件
        购物_服饰 = 0103, //需要资质文件
        购物_鞋类箱包 = 0104, //需要资质文件
        购物_运动户外 = 0105, //需要资质文件
        购物_化妆品 = 0106, //《化妆品卫生许可证》或《进口化妆品卫生许可证》
        购物_珠宝配饰 = 0107, //需要资质文件
        购物_钟表眼镜 = 0108, //隐形眼镜上传《医疗器械经营许可证》
        购物_家纺家装 = 0109, //需要资质文件
        购物_数码家电 = 0110, //需要资质文件
        购物_乐器 = 0111, //需要资质文件
        购物_鲜花礼品 = 0112, //需要资质文件
        购物_普通食品 = 0113, //《食品卫生许可证》或《食品流通许可证》
        购物_保健食品 = 0114, //《食品流通许可证》或《食品卫生许可证》或《进口保健食品批准证书》
        购物_酒类 = 0115, //1《食品流通许可证》或《食品卫生许可证》； 2《酒类流通备案登记证》或《酒类零售许可证》或《酒类批发许可证》，生产商可不上传此类证件； 3《酒类生产许可证》或《全国工业品生产许可证》
        购物_药房_药店 = 0116, //《药品经营许可证》，医疗器械上传《医疗器械经营许可证》
        购物_母婴用品 = 0117, //奶粉上传《食品流通许可证》或《食品卫生许可证》
        购物_图书报刊杂志 = 0118, //《出版物经营许可证》
        购物_综合电商 = 0119, //《增值电信业务经营许可证》
        购物_便利店 = 0120, //需要资质文件
        餐饮_餐厅 = 0201, //《餐饮服务许可证》
        餐饮_快餐 = 0202, //《餐饮服务许可证》
        餐饮_自助餐 = 0203, //《餐饮服务许可证》
        餐饮_面包甜点 = 0204, //《餐饮服务许可证》
        餐饮_咖啡厅 = 0205, //《餐饮服务许可证》
        餐饮_休闲小吃 = 0206, //《餐饮服务许可证》
        餐饮_外卖 = 0207, //《食品卫生许可证》
        休闲娱乐_美容美发 = 0301, //《卫生许可证》
        休闲娱乐_宠物美容 = 0302, //需要资质文件
        休闲娱乐_美甲 = 0303, //《卫生许可证》
        休闲娱乐_艺术写真 = 0304, //需要资质文件
        休闲娱乐_酒吧_俱乐部 = 0305, //《娱乐经营许可证》
        休闲娱乐_文化文艺 = 0306, //需要资质文件
        休闲娱乐_展览展出 = 0307, //需要资质文件
        休闲娱乐_会议活动 = 0308, //需要资质文件
        休闲娱乐_培训拓展 = 0309, //需要资质文件
        休闲娱乐_KTV = 0310, //《娱乐经营许可证》
        休闲娱乐_棋牌室 = 0311, //需要资质文件
        休闲娱乐_运动健身 = 0312, //需要资质文件
        休闲娱乐_足疗按摩 = 0313, //1《特种行业许可证》（洗浴桑拿）；《医疗机构执业许可证》（推拿按摩）； 2《卫生许可证》
        休闲娱乐_演出 = 0314, //需要资质文件
        休闲娱乐_电影 = 0315, //需要资质文件
        休闲娱乐_书店 = 0316, //《出版物经营许可证》
        休闲娱乐_网吧 = 0317, //《网络文化经营许可证》
        休闲娱乐_茶馆 = 0318, //《餐饮服务许可证》
        生活服务_婚庆服务 = 0401, //需要资质文件
        生活服务_汽车服务 = 0402, //需要资质文件
        生活服务_家政服务 = 0403, //中介类上传《职业中介许可证》
        生活服务_物业管理 = 0404, //《物业管理企业资质证书》
        生活服务_医疗保健 = 0405, //《医疗机构执业许可证》
        生活服务_宠物医疗 = 0406, //《动物诊疗许可证》
        生活服务_教育学校 = 0407, //《办学许可证》
        生活服务_留学中介 = 0408, //《自费出国留学中介服务机构资格认定书》
        生活服务_快递 = 0409, //《快递业务经营许可证》
        生活服务_货运 = 0410, //《运输经营许可证》，请根据运输方式（如道路、水路等）提交对应资质，且危险品运输暂不对外开放,
        交通运输_航空 = 0501, //《公共航空运输企业经营许可证》
        交通运输_水运 = 0502, //《水路运输业务经营许可证》
        交通运输_道路 = 0503, //《道路运输经营许可证》
        交通运输_公交 = 0504, //需要资质文件
        交通运输_地铁 = 0505, //需要资质文件
        交通运输_铁路 = 0506, //需要资质文件
        旅游_旅游景点 = 0601, //需要资质文件
        旅游_旅行社 = 0602, //《旅行社业务经营许可证》
        旅游_旅游服务平台 = 0603, //需要资质文件
        酒店_星级酒店 = 0701, //《旅馆业特种行业许可证》
        酒店_度假村 = 0702, //《旅馆业特种行业许可证》
        酒店_快捷酒店 = 0703, //《旅馆业特种行业许可证》
        通信_电信运营商 = 0801, //《电信业务经营许可证》
        金融_银行 = 0901, //需要资质文件
        金融_保险 = 0902, //《经营保险业务许可证》
        金融_基金 = 0903, //《基金销售业务资格证》、《基金管理资格证书》
        金融_证券 = 0904, //《证券经营机构营业许可证》
        房地产_房产预售 = 01001, //《建设用地规划许可证》、《建设工程规划许可证》、《建设工程开工证》、《国有土地使用证》、《商品房预售许可证》
        房地产_房屋中介 = 01002, //需要资质文件
        影视传媒_广告商 = 01101, //需要资质文件
        影视传媒_电视台 = 01102, //《广播电视播出机构许可证》
        影视传媒_广播电台 = 01103, //《广播电视播出机构许可证》
        机构组织_行政机关 = 01201, //机关设立的红头文件
        机构组织_事业单位 = 01202, //需要资质文件
        机构组织_社会团体 = 01203, //需要资质文件
        机构组织_其他组织 = 01204, //需要资质文件
        代运营商_代运营商 = 01301, //需要资质文件
        智能硬件_智能硬件 = 01401, //需要资质文件
    }

    /// <summary>
    /// 二维码类型
    /// </summary>
    public enum QrCode_ActionName
    {
        /// <summary>
        /// 临时
        /// </summary>
        QR_SCENE,
        /// <summary>
        /// 永久
        /// </summary>
        QR_LIMIT_SCENE,
        /// <summary>
        /// 永久的字符串
        /// </summary>
        QR_LIMIT_STR_SCENE,
        /// <summary>
        /// 临时的字符串参数值
        /// </summary>
        QR_STR_SCENE
    }


    /// <summary>
    /// 支付类型
    /// </summary>
    
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public enum TenPayV3Type
    {
        /// <summary>
        /// 公众号JS-API支付和小程序支付
        /// </summary>
        JSAPI,
        NATIVE,
        APP,
        MWEB
    }

    #region 过期

    /// <summary>
    /// 红包的场景id（scene_id），最中输出为字符串
    /// </summary>
    [Obsolete("请使用 Senparc.Weixin.TenPay.dll，Senparc.Weixin.TenPay.V3 中的对应方法")]
    public enum RedPack_Scene
    {
        /// <summary>
        /// 商品促销
        /// </summary>
        PRODUCT_1,
        /// <summary>
        /// 抽奖
        /// </summary>
        PRODUCT_2,
        /// <summary>
        /// 虚拟物品兑奖
        /// </summary>
        PRODUCT_3,
        /// <summary>
        /// 企业内部福利
        /// </summary>
        PRODUCT_4,
        /// <summary>
        /// 渠道分润
        /// </summary>
        PRODUCT_5,
        /// <summary>
        /// 保险回馈
        /// </summary>
        PRODUCT_6,
        /// <summary>
        /// 彩票派奖
        /// </summary>
        PRODUCT_7,
        /// <summary>
        /// 税务刮奖
        /// </summary>
        PRODUCT_8
    }

    #endregion
}
