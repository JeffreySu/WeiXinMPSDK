/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：OrderApi.cs
    文件功能描述：微小店订单接口
    
    
    创建标识：Senparc - 20150827
----------------------------------------------------------------*/

/* 
   微小店接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E5%BE%AE%E4%BF%A1%E5%B0%8F%E5%BA%97%E6%8E%A5%E5%8F%A3
*/

using System;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs.MerChant
{
    /// <summary>
    /// 微小店订单接口
    /// </summary>
    public static class OrderApi
    {
        /// <summary>
        /// 根据订单ID获取订单详情
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="orderId">订单Id</param>
        /// <returns></returns>
        public static GetByIdOrderResult GetByIdOrder(string accessToken, string orderId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/order/getbyid?access_token={0}";

            var data = new
            {
                order_id = orderId
            };

            return CommonJsonSend.Send<GetByIdOrderResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 根据订单状态/创建时间获取订单详情
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="status">订单状态(不带该字段-全部状态, 2-待发货, 3-已发货, 5-已完成, 8-维权中, )</param>
        /// <param name="beginTime">订单创建时间起始时间(不带该字段则不按照时间做筛选)</param>
        /// <param name="endTime">订单创建时间终止时间(不带该字段则不按照时间做筛选)</param>
        /// <returns></returns>
        public static GetByFilterResult GetByFilterOrder(string accessToken, int? status, DateTime? beginTime, DateTime? endTime)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/order/getbyfilter?access_token={0}";

            var data = new
            {
                status = status,
                begintime = beginTime.HasValue ? DateTimeHelper.GetWeixinDateTime(beginTime.Value) : (long?)null,
                endtime = endTime.HasValue ? DateTimeHelper.GetWeixinDateTime(endTime.Value) : (long?)null
            };

            return CommonJsonSend.Send<GetByFilterResult>(accessToken, urlFormat, data,jsonSetting:new JsonSetting(true));
        }

        /// <summary>
        /// 设置订单发货信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="orderId">订单ID</param>
        /// <param name="deliveryCompany">物流公司ID(参考《物流公司ID》；当need_delivery为0时，可不填本字段；当need_delivery为1时，该字段不能为空；当need_delivery为1且is_others为1时，本字段填写其它物流公司名称)</param>
        /// <param name="deliveryTrackNo">运单ID(当need_delivery为0时，可不填本字段；当need_delivery为1时，该字段不能为空；)</param>
        /// <param name="needDelivery">商品是否需要物流(0-不需要，1-需要，无该字段默认为需要物流)</param>
        /// <param name="isOthers">是否为其它物流公司(0-否，1-是，无该字段默认为不是其它物流公司)</param>
        /// <returns></returns>
        /// 物流公司    Id
        /// 邮政EMS	    Fsearch_code
        /// 申通快递	002shentong
        /// 中通速递	066zhongtong
        /// 圆通速递	056yuantong
        /// 天天快递	042tiantian
        /// 顺丰速运	003shunfeng
        /// 韵达快运	059Yunda
        /// 宅急送	    064zhaijisong
        /// 汇通快运	020huitong
        /// 易迅快递	zj001yixun
        public static WxJsonResult SetdeliveryOrder(string accessToken, string orderId, string deliveryCompany, string deliveryTrackNo, int needDelivery = 1, int isOthers = 0)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/order/setdelivery?access_token={0}";

            var data = new
            {
                order_id = orderId,
                delivery_company = deliveryCompany,
                delivery_track_no = deliveryTrackNo,
                need_delivery = needDelivery,
                is_others = isOthers
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public static WxJsonResult CloseOrder(string accessToken, string orderId)
        {
            var urlFormat = "https://api.weixin.qq.com/merchant/order/close?access_token={0}";

            var data = new
            {
                order_id = orderId
            };

            return CommonJsonSend.Send<WxJsonResult>(accessToken, urlFormat, data);
        }
    }
}
