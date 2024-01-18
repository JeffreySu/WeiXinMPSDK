#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
  
    文件名：Enums.cs
    文件功能描述：枚举类型
    
    
    创建标识：Senparc - 20170106

    修改标识：mc7246 - 20220504
    修改描述：v3.15.2 添加小程序隐私权限审核结果推送

    修改标识：mc7246 - 20230119
    修改描述：v3.15.12 添加小程序类目审核结果事件推送，增加 Event.wxa_category_audit 枚举值
    
    修改标识：chinanhb - 20230529
    修改描述：运单轨迹更新事件推送，增加 Event.add_express_path 枚举值

    修改标识：Senparc - 20231202
    修改描述：v3.17.2 Event 添加“小程序虚拟支付”相关枚举

----------------------------------------------------------------*/

using System.ComponentModel;

namespace Senparc.Weixin.WxOpen
{
    ///// <summary>
    ///// 接收消息类型
    ///// </summary>
    //public enum RequestMsgType
    //{
    //    Text, //文本
    //    Image, //图片
    //    Event, //事件推送
    //}

    /// <summary>
    /// 当RequestMsgType类型为Event时，Event属性的类型
    /// </summary>
    public enum Event
    {
        /// <summary>
        /// 进入会话事件
        /// </summary>
        user_enter_tempsession,
        add_nearby_poi_audit_info,
        nearby_category_audit_info,
        create_map_poi_audit_info,
        wxa_nickname_audit, //名称审核事件
        weapp_audit_success,
        weapp_audit_fail,
        weapp_audit_delay,
        wxa_illegal_record, //小程序违规记录事件
        wxa_appeal_record, //小程序申诉记录推送
        wxa_privacy_apply, //隐私权限审核结果推送
        /// <summary>
        /// mediaCheckAsync 异步检测结果
        /// </summary>
        wxa_media_check,
        /// <summary>
        /// 类目审核结果事件推送
        /// </summary>
        wxa_category_audit,
        /// <summary>
        /// 运单轨迹更新事件
        /// </summary>
        add_express_path,
        /// <summary>
        /// 提醒接入发货信息管理服务API事件
        /// </summary>
        trade_manage_remind_access_api,
        /// <summary>
        /// 提醒需要上传发货信息事件
        /// </summary>
        trade_manage_remind_shipping,
        /// <summary>
        /// 订单将要结算或已经结算事件
        /// </summary>
        trade_manage_order_settlement,
        /// <summary>
        /// 小程序微信认证支付成功事件
        /// </summary>
        wx_verify_pay_succ,
        /// <summary>
        /// 小程序微信认证派单事件
        /// </summary>
        wx_verify_dispatch,

        #region 小程序虚拟支付
        /// <summary>
        /// 道具发货推送
        /// </summary>
        xpay_goods_deliver_notify,

        /// <summary>
        /// 代币支付推送
        /// </summary>
        xpay_coin_pay_notify,

        /// <summary>
        /// 退款推送
        /// </summary>
        xpay_refund_notify,
        #endregion
    }

    ///// <summary>
    ///// 发送消息类型
    ///// </summary>
    //public enum ResponseMsgType
    //{
    //    [Description("文本")]
    //    Text = 0,
    //    [Description("图片")]
    //    Image = 3,

    //    //以下为延伸类型，微信官方并未提供具体的回复类型
    //    [Description("无回复")]
    //    NoResponse = 110,
    //    [Description("success")]
    //    SuccessResponse = 200
    //}
}
