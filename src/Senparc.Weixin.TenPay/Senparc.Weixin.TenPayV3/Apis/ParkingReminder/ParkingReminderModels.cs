#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ParkingReminderModels.cs
    文件功能描述：微信支付 V3 停车缴费服务强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.TenPayV3.Apis.ParkingReminder
{
    /// <summary>停车场固定时段计费规则。</summary>
    public class ParkingFixedIntervalRule
    {
        /// <summary>适用日期类型。</summary>
        public string day_type { get; set; }
        /// <summary>计费开始时间，格式 HH:mm。</summary>
        public string start_time { get; set; }
        /// <summary>计费结束时间，格式 HH:mm。</summary>
        public string end_time { get; set; }
        /// <summary>车辆类型。</summary>
        public string vehicle_type { get; set; }
        /// <summary>车牌类型。</summary>
        public string plate_type { get; set; }
        /// <summary>首段时长，单位为分钟。</summary>
        public int? first_duration { get; set; }
        /// <summary>首段金额，单位为分。</summary>
        public long? first_amount { get; set; }
        /// <summary>后续计费间隔，单位为分钟。</summary>
        public int? interval_duration { get; set; }
        /// <summary>每个后续间隔金额，单位为分。</summary>
        public long? interval_amount { get; set; }
        /// <summary>后续间隔累计上限，单位为分。</summary>
        public long? interval_max_amount { get; set; }
        /// <summary>每日最高费用，单位为分。</summary>
        public long? max_fee_per_day { get; set; }
        /// <summary>免费时段计费模式。</summary>
        public string free_period_charging_mode { get; set; }
        /// <summary>免费时段计算模式。</summary>
        public string free_period_calculation_mode { get; set; }
        /// <summary>新能源车辆是否免费停车。</summary>
        public bool? is_green_vehicle_free_parking { get; set; }
        /// <summary>首段时长计算模式。</summary>
        public string first_duration_mode { get; set; }
    }

    /// <summary>停车场分段时长计费规则。</summary>
    public class ParkingDurationSegmentRule
    {
        /// <summary>适用日期类型。</summary>
        public string day_type { get; set; }
        /// <summary>计费开始时间，格式 HH:mm。</summary>
        public string start_time { get; set; }
        /// <summary>计费结束时间，格式 HH:mm。</summary>
        public string end_time { get; set; }
        /// <summary>车辆类型。</summary>
        public string vehicle_type { get; set; }
        /// <summary>车牌类型。</summary>
        public string plate_type { get; set; }
        /// <summary>分段计费方式。</summary>
        public string charge_mode { get; set; }
        /// <summary>分段起始时长，单位为分钟。</summary>
        public int? duration_from { get; set; }
        /// <summary>分段结束时长，单位为分钟。</summary>
        public int? duration_to { get; set; }
        /// <summary>固定金额，单位为分。</summary>
        public long? fixed_amount { get; set; }
        /// <summary>最小计费间隔，单位为分钟。</summary>
        public int? interval_min { get; set; }
        /// <summary>每个计费间隔金额，单位为分。</summary>
        public long? interval_amount { get; set; }
        /// <summary>计费间隔累计上限，单位为分。</summary>
        public long? interval_max_amount { get; set; }
        /// <summary>每日最高费用，单位为分。</summary>
        public long? max_fee_per_day { get; set; }
    }

    /// <summary>停车场预入场计费规则。</summary>
    public class ParkingPreEntryRule
    {
        /// <summary>适用日期类型。</summary>
        public string day_type { get; set; }
        /// <summary>计费开始时间，格式 HH:mm。</summary>
        public string start_time { get; set; }
        /// <summary>计费结束时间，格式 HH:mm。</summary>
        public string end_time { get; set; }
        /// <summary>车辆类型。</summary>
        public string vehicle_type { get; set; }
        /// <summary>车牌类型。</summary>
        public string plate_type { get; set; }
        /// <summary>预入场费用，单位为分。</summary>
        public long? amount { get; set; }
        /// <summary>每日最高费用，单位为分。</summary>
        public long? max_fee_per_day { get; set; }
    }

    /// <summary>停车场计费规则。</summary>
    public class ParkingChargingRule
    {
        /// <summary>计费规则类型。</summary>
        public string rule_type { get; set; }
        /// <summary>费用上限类型。</summary>
        public string fee_limit_type { get; set; }
        /// <summary>每日上限类型。</summary>
        public string daily_limit_type { get; set; }
        /// <summary>停车时长取整方式。</summary>
        public string time_rounding_type { get; set; }
        /// <summary>入场免费时长，单位为分钟。</summary>
        public int? free_entry_duration { get; set; }
        /// <summary>缴费后免费离场时长，单位为分钟。</summary>
        public int? free_exit_duration { get; set; }
        /// <summary>固定时段计费规则列表。</summary>
        public IList<ParkingFixedIntervalRule> fixed_interval_rule { get; set; }
        /// <summary>分段时长计费规则列表。</summary>
        public IList<ParkingDurationSegmentRule> duration_segment_rule { get; set; }
        /// <summary>预入场计费规则列表。</summary>
        public IList<ParkingPreEntryRule> pre_entry_rule { get; set; }
        /// <summary>节假日计费模式。</summary>
        public string holiday_mode { get; set; }
        /// <summary>累计费用上限模式。</summary>
        public string aggregate_limit_mode { get; set; }
        /// <summary>首次计费时间模式。</summary>
        public string first_charge_time_mode { get; set; }
    }

    /// <summary>停车场进件信息。</summary>
    public class ParkingLotApplicationData
    {
        /// <summary>停车场名称。</summary>
        public string parking_lot_name { get; set; }
        /// <summary>服务商停车场 ID。</summary>
        public string out_parking_lot_id { get; set; }
        /// <summary>停车场地址。</summary>
        public string parking_lot_address { get; set; }
        /// <summary>停车场经度。</summary>
        public string longitude { get; set; }
        /// <summary>停车场纬度。</summary>
        public string latitude { get; set; }
        /// <summary>停车场类型。</summary>
        public string parking_lot_type { get; set; }
        /// <summary>停车场联系电话。</summary>
        public string phone_number { get; set; }
        /// <summary>停车场标识图片 URL。</summary>
        public string parking_sign_url { get; set; }
        /// <summary>停车通知文案列表。</summary>
        public IList<string> notification_text_list { get; set; }
        /// <summary>缴费小程序 AppId。</summary>
        public string payment_mini_prog_appid { get; set; }
        /// <summary>缴费页面路径。</summary>
        public string payment_path { get; set; }
        /// <summary>停车订单小程序 AppId。</summary>
        public string parking_order_mini_prog_appid { get; set; }
        /// <summary>停车订单页面路径。</summary>
        public string parking_order_path { get; set; }
        /// <summary>停车计费规则。</summary>
        public ParkingChargingRule charging_rule { get; set; }
    }

    /// <summary>提交停车场进件申请请求。</summary>
    public class ParkingLotApplicationRequestData
    {
        /// <summary>停车场进件信息。</summary>
        public ParkingLotApplicationData parking_lot { get; set; }
    }

    /// <summary>停车场进件申请返回结果。</summary>
    public class ParkingLotApplicationSubmitResultJson : ReturnJsonBase
    {
        /// <summary>停车场进件申请单号。</summary>
        public string parking_lot_audit_no { get; set; }
    }

    /// <summary>停车场进件驳回字段。</summary>
    public class ParkingLotAuditField
    {
        /// <summary>被驳回的字段路径。</summary>
        public string field { get; set; }
        /// <summary>审核意见。</summary>
        public string comment { get; set; }
        /// <summary>修改建议。</summary>
        public string recommendation { get; set; }
    }

    /// <summary>停车场进件审核意见。</summary>
    public class ParkingLotAuditComment
    {
        /// <summary>字段级审核意见列表。</summary>
        public IList<ParkingLotAuditField> fields { get; set; }
    }

    /// <summary>停车场进件申请记录。</summary>
    public class ParkingLotApplication
    {
        /// <summary>停车场进件申请单号。</summary>
        public string parking_lot_audit_no { get; set; }
        /// <summary>审核状态。</summary>
        public string audit_status { get; set; }
        /// <summary>提交时间，Unix 秒级时间戳。</summary>
        public long? submit_time { get; set; }
        /// <summary>停车场进件信息。</summary>
        public ParkingLotApplicationData parking_lot { get; set; }
        /// <summary>审核意见。</summary>
        public ParkingLotAuditComment audit_comment { get; set; }
        /// <summary>微信支付停车场 ID。</summary>
        public string wx_parking_lot_id { get; set; }
    }

    /// <summary>停车场进件申请查询结果。</summary>
    public class ParkingLotApplicationResultJson : ReturnJsonBase
    {
        /// <summary>停车场进件申请记录。</summary>
        public ParkingLotApplication application { get; set; }
    }

    /// <summary>停车场进件申请列表结果。</summary>
    public class ParkingLotApplicationListResultJson : ReturnJsonBase
    {
        /// <summary>停车场进件申请记录列表。</summary>
        public IList<ParkingLotApplication> application_list { get; set; }
    }

    /// <summary>同步车辆入场请求。</summary>
    public class ParkingEntryRequestData
    {
        /// <summary>服务商侧流水号。</summary>
        public string out_serial_number { get; set; }
        /// <summary>停车场侧停车记录 ID。</summary>
        public string parking_id { get; set; }
        /// <summary>车牌号。</summary>
        public string plate_number { get; set; }
        /// <summary>入场时间，Unix 秒级时间戳。</summary>
        public long enter_timestamp { get; set; }
        /// <summary>车牌颜色。</summary>
        public string plate_color { get; set; }
        /// <summary>车辆用途类型。</summary>
        public string car_type { get; set; }
        /// <summary>车辆类型。</summary>
        public string vehicle_type { get; set; }
        /// <summary>入口编号。</summary>
        public string entrance_number { get; set; }
        /// <summary>入口名称。</summary>
        public string entrance_name { get; set; }
        /// <summary>可用优惠模板 ID 列表。</summary>
        public IList<long> discount_template_id { get; set; }
    }

    /// <summary>同步车辆入场结果。</summary>
    public class ParkingEntryResultJson : ReturnJsonBase
    {
        /// <summary>微信支付停车流水号。</summary>
        public string serial_number { get; set; }
    }

    /// <summary>同步车辆离场请求。</summary>
    public class ParkingExitRequestData
    {
        /// <summary>服务商侧流水号。</summary>
        public string out_serial_number { get; set; }
        /// <summary>离场时间，Unix 秒级时间戳。</summary>
        public long exit_timestamp { get; set; }
        /// <summary>车牌号。</summary>
        public string plate_number { get; set; }
        /// <summary>车牌颜色。</summary>
        public string plate_color { get; set; }
        /// <summary>停车场侧停车记录 ID。</summary>
        public string parking_id { get; set; }
        /// <summary>支付状态。</summary>
        public string pay_state { get; set; }
        /// <summary>支付类型。</summary>
        public string pay_type { get; set; }
        /// <summary>服务商订单号。</summary>
        public string out_trade_no { get; set; }
        /// <summary>应付总金额，单位为分。</summary>
        public long? total_amount { get; set; }
        /// <summary>实付金额，单位为分。</summary>
        public long? paid_amount { get; set; }
        /// <summary>用户 OpenId。</summary>
        public string openid { get; set; }
        /// <summary>支付渠道。</summary>
        public string pay_channel { get; set; }
        /// <summary>支付时间，Unix 秒级时间戳。</summary>
        public long? pay_time { get; set; }
        /// <summary>停车缴费令牌。</summary>
        public string token { get; set; }
        /// <summary>微信支付订单号。</summary>
        public string wx_trade_no { get; set; }
    }

    /// <summary>同步停车支付结果请求。</summary>
    public class ParkingPaymentRequestData
    {
        /// <summary>服务商侧流水号。</summary>
        public string out_serial_number { get; set; }
        /// <summary>车牌号。</summary>
        public string plate_number { get; set; }
        /// <summary>停车场侧停车记录 ID。</summary>
        public string parking_id { get; set; }
        /// <summary>停车状态。</summary>
        public string parking_state { get; set; }
        /// <summary>支付类型。</summary>
        public string pay_type { get; set; }
        /// <summary>用户 OpenId。</summary>
        public string openid { get; set; }
        /// <summary>子商户号。</summary>
        public string sub_mchid { get; set; }
        /// <summary>应付总金额，单位为分。</summary>
        public long total_amount { get; set; }
        /// <summary>实付金额，单位为分。</summary>
        public long paid_amount { get; set; }
        /// <summary>服务商订单号。</summary>
        public string out_trade_no { get; set; }
        /// <summary>支付渠道。</summary>
        public string pay_channel { get; set; }
        /// <summary>支付时间，Unix 秒级时间戳。</summary>
        public long? pay_time { get; set; }
        /// <summary>停车缴费令牌。</summary>
        public string token { get; set; }
        /// <summary>微信支付订单号。</summary>
        public string wx_trade_no { get; set; }
    }

    /// <summary>非临停扩展支付费用项。</summary>
    public class ParkingExtensionFeeItem
    {
        /// <summary>费用类型。</summary>
        public string fee_type { get; set; }
        /// <summary>费用金额，单位为分。</summary>
        public long amount { get; set; }
    }

    /// <summary>同步非临停扩展支付请求。</summary>
    public class ParkingExtensionPaymentRequestData
    {
        /// <summary>服务商订单号。</summary>
        public string out_trade_no { get; set; }
        /// <summary>费用明细列表。</summary>
        public IList<ParkingExtensionFeeItem> fee_items { get; set; }
        /// <summary>支付时间，Unix 秒级时间戳。</summary>
        public long pay_time { get; set; }
        /// <summary>微信支付停车场 ID。</summary>
        public string wx_parking_lot_id { get; set; }
        /// <summary>微信支付订单号。</summary>
        public string wx_trade_no { get; set; }
    }

    /// <summary>停车场信息查询结果。</summary>
    public class ParkingLotInfoResultJson : ReturnJsonBase
    {
        /// <summary>微信支付停车场 ID。</summary>
        public string wx_parking_lot_id { get; set; }
        /// <summary>服务商停车场 ID。</summary>
        public string out_parking_lot_id { get; set; }
        /// <summary>停车场名称。</summary>
        public string parking_lot_name { get; set; }
        /// <summary>停车场地址。</summary>
        public string parking_lot_address { get; set; }
        /// <summary>缴费小程序 AppId。</summary>
        public string payment_mini_prog_appid { get; set; }
        /// <summary>缴费小程序页面路径。</summary>
        public string payment_mini_prog_path { get; set; }
        /// <summary>停车订单小程序 AppId。</summary>
        public string parking_order_mini_prog_appid { get; set; }
        /// <summary>停车订单小程序页面路径。</summary>
        public string parking_order_mini_prog_path { get; set; }
        /// <summary>停车场启用状态。</summary>
        public string enabled_state { get; set; }
        /// <summary>未启用或停用原因。</summary>
        public string reason { get; set; }
    }

    /// <summary>查询停车费用请求。</summary>
    public class ParkingFeeRequestData
    {
        /// <summary>服务商侧流水号。</summary>
        public string out_serial_number { get; set; }
        /// <summary>车牌号。</summary>
        public string plate_number { get; set; }
        /// <summary>服务商停车记录 ID。</summary>
        public string out_parking_id { get; set; }
    }

    /// <summary>停车费用查询结果。</summary>
    public class ParkingFeeResultJson : ReturnJsonBase
    {
        /// <summary>应付总金额，单位为分。</summary>
        public long? total_amount { get; set; }
        /// <summary>入场时间，Unix 秒级时间戳。</summary>
        public long? parking_timestamp { get; set; }
        /// <summary>停车状态。</summary>
        public string parking_state { get; set; }
        /// <summary>支付状态。</summary>
        public string pay_state { get; set; }
        /// <summary>允许离场截止时间，Unix 秒级时间戳。</summary>
        public long? allowed_exit_timestamp { get; set; }
        /// <summary>下一次涨价金额，单位为分。</summary>
        public long? next_raise_price { get; set; }
        /// <summary>下一次涨价时间，Unix 秒级时间戳。</summary>
        public long? next_raise_timestamp { get; set; }
        /// <summary>当前可支付金额，单位为分。</summary>
        public long? payable_amount { get; set; }
        /// <summary>已支付金额，单位为分。</summary>
        public long? paid_amount { get; set; }
    }
}
